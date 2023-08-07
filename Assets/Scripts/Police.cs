/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 04/08/2023
 * Description: IP2 - Police AI and States
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEditor.Rendering;


public class Police : MonoBehaviour
{
    /// <summary>
    /// Is the player in Police's range
    /// </summary>
    public bool inRange;
    /// <summary>
    /// Is the player in Police's FOV
    /// </summary>
    public bool inFOV;
    /// <summary>
    /// Is player in Police's line of sight
    /// </summary>
    public bool inSight;
    /// <summary>
    /// Is the Police touching the player
    /// </summary>
    public bool isTouching;
    /// <summary>
    /// Is the Police Alerted
    /// </summary>
    public bool isAlert;
    /// <summary>
    /// How many times did player tap space
    /// </summary>
    public int escapeCount;
    /// <summary>
    /// Did the player break free
    /// </summary>
    public bool breakFree;
    /// <summary>
    /// Police cooldown before he can continue chasing
    /// </summary>
    public bool cooldown;
    /// <summary>
    /// Can Police continue chasing
    /// </summary>
    public bool canChase;

    /// <summary>
    /// Current state of the Police
    /// </summary>
    private string currentState;
    /// <summary>
    /// Next state for the Police to change to
    /// </summary>
    private string nextState;

    /// <summary>
    /// Duration of delay before Police can catch
    /// </summary>
    private float catchDuration;
    /// <summary>
    /// Duration of delay before Police continues
    /// </summary>
    private float currentDuration;

    /// <summary>
    /// Police Agent NavMesh
    /// </summary>
    NavMeshAgent agentComponent;
    /// <summary>
    /// What the Police follows
    /// </summary>
    Transform followPlayer;

    /// <summary>
    /// Where the Police goes
    /// </summary>
    public Transform[] checkpoints;
    /// <summary>
    /// Checkpoint index
    /// </summary>
    private int currentCheckpointIndex;

    /// <summary>
    /// Set the Police as an instance
    /// </summary>
    public static Police instance;

    private void Awake()
    {
        instance = this;
        agentComponent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        escapeCount = 0;

        currentState = "Normal";
        nextState = currentState;
        SwitchState();
    }


    /// <summary>
    /// If touching player
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = true;
        }
    }

    /// <summary>
    /// If not touching player
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        isTouching = false;
    }

    /// <summary>
    /// Delayed state of Police before it can continue chasing
    /// </summary>
    IEnumerator Delayed()
    {
        Debug.Log("Delayed");
        cooldown = true;
        breakFree = false;
        escapeCount = 0;

        while (cooldown)
        {
            if (canChase)
            {
                agentComponent.speed = 3.5f;
                cooldown = false;
                nextState = "Normal";
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Captured state of Police
    /// </summary>
    IEnumerator Captured()
    {
        Debug.Log("Captured");
        Player.instance.isCaught = true;
        escapeCount = 0;

        while (breakFree != true)
        {
            if (escapeCount >= 10)
            {
                Player.instance.isCaught = false;
                Player.instance.moveSpeed = 3;
                catchDuration = 0;
                nextState = "Delayed";
                breakFree = true;
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Alert state of Police
    /// </summary>
    IEnumerator Alerted()
    {
        Debug.Log("Alerted");

        while (isAlert) //While Police has not touched Player, keep running
        {
            agentComponent.SetDestination(followPlayer.position);

            if (inRange != true || inSight != true) 
            {
                nextState = "Normal";
                isAlert = false;
            }

            if (isTouching) //If Police touched the player, stop both the Player and the Police
            {
                agentComponent.speed = 0;
                Player.instance.moveSpeed = 0;
                nextState = "Captured";
                isAlert = false;
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Normal state of Police (Wandering)
    /// </summary>
    IEnumerator Normal()
    {
        Debug.Log("Normal");
        agentComponent.SetDestination(checkpoints[currentCheckpointIndex].position);
        bool hasReached = false;
        canChase = false;

        while (currentState == "Normal")
        {
            if (inRange && inFOV)
            {
                isAlert = true;
                nextState = "Alerted";
            }
            else
            {
                if (hasReached != true)
                {
                    if (agentComponent.remainingDistance <= agentComponent.stoppingDistance) //If police is close/on position, cosider it reached
                    {
                        hasReached = true;
                        ++currentCheckpointIndex; //Move to next checkpoint

                        if (currentCheckpointIndex >= checkpoints.Length) //Reset list when at last checkpoint
                        {
                            currentCheckpointIndex = 0;
                        }
                    }
                    hasReached = false;
                    agentComponent.SetDestination(checkpoints[currentCheckpointIndex].position);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Delay before Police catches the player
    /// </summary>
    private void CatchTime()
    {
        if (breakFree != true)
        {
            if (catchDuration < 5.0f)
            {
                catchDuration += Time.deltaTime;
            }
            else if (catchDuration >= 5.0f)
            {
                HealthBar.instance.Damage(10000);
                catchDuration = 0;
            }
        }
    }

    /// <summary>
    /// Delay before Police can continue to chase
    /// </summary>
    private void TimeDelay()
    {
        if (cooldown)
        {
            if (currentDuration < 3.0f)
            {
                currentDuration += Time.deltaTime;
            }
            else if (currentDuration >= 3.0f)
            {
                canChase = true;
                currentDuration = 0;
            }
        }
    }

    /// <summary>
    /// To change the state of the Police
    /// </summary>
    private void SwitchState()
    {
        StartCoroutine(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer = GameObject.FindWithTag("Player").transform;
        Vector3 direction = followPlayer.position - transform.position;

        if (Mathf.Abs(Vector3.Angle(transform.forward, direction)) < 45) //Fancy math -> find degree between player and Police's foward, and check if it is < 45 (in range). Mathf.Abs so it works on both sides (45+45=90)
        {
            inFOV = true;
        }
        else
        {
            inFOV = false;
        }

        if (currentState == "Alerted")
        {
            RaycastHit hit;
            if (Vector3.Distance(transform.position, followPlayer.position) < 5) //If player in range
            {
                if (Physics.Raycast(transform.position, (followPlayer.position - transform.position), out hit, 5))
                {
                    if (hit.transform == followPlayer) //If player is first in line of sight, continue targeting, else, change state to normal
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                        inSight = true;
                    }
                    else
                    {
                        inSight = false;
                    }
                }
            }
            /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
            }*/
        }

        if (GameManager.gameManager.pause || GameManager.gameManager.dead || Player.instance.inDialogue)
        {
            agentComponent.isStopped = true;
        }
        else
        {
            agentComponent.isStopped = false;
        }

        if (currentState == "Delayed")
        {
            TimeDelay();
        }

        if (currentState == "Captured")
        {
            CatchTime();
        }

        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }
}
