/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 03/08/2023
 * Description: IP2 - Dog AI and States
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEditor.Rendering;

public class Dog : MonoBehaviour
{
    /// <summary>
    /// Is the player in Dog's range
    /// </summary>
    public bool inRange;
    /// <summary>
    /// Is the Dog touching the player
    /// </summary>
    public bool isTouching;
    /// <summary>
    /// Dog cooldown before he can continue chasing
    /// </summary>
    public bool cooldown;
    /// <summary>
    /// Is the player feeding the Dog
    /// </summary>
    public bool feeding;
    /// <summary>
    /// Has the Dog been fed
    /// </summary>
    public bool isFed;
    
    /// <summary>
    /// Current state of the Dog
    /// </summary>
    private string currentState;
    /// <summary>
    /// Next state for the Dog to change to
    /// </summary>
    private string nextState;

    /// <summary>
    /// Duration of delay before Dog continues
    /// </summary>
    private float currentDuration;

    /// <summary>
    /// Dog Agent NavMesh
    /// </summary>
    NavMeshAgent agentComponent;
    /// <summary>
    /// What the Dog follows
    /// </summary>
    Transform followPlayer;

    /// <summary>
    /// Where the Dog goes
    /// </summary>
    public Transform[] checkpoints;
    /// <summary>
    /// Checkpoint index
    /// </summary>
    private int currentCheckpointIndex;

    /// <summary>
    /// Set the Dog as an instance
    /// </summary>
    public static Dog instance;

    private void Awake()
    {
        instance = this;
        agentComponent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
    /// Fed state of Dog
    /// </summary>
    IEnumerator Fed()
    {
        Debug.Log("Fed");
        agentComponent.speed = 1.5f;
        agentComponent.stoppingDistance = 1.5f;

        while (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (followPlayer != null && currentState == "Fed")
            {
                agentComponent.SetDestination(followPlayer.position);
            }
            else if (SceneManager.GetActiveScene().buildIndex != 4) //Destroy if player left the park 
            {
                Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Chasing state of Dog
    /// </summary>
    IEnumerator Chasing()
    {
        Debug.Log("Chasing");
        agentComponent.speed = 3;

        while (isFed != true)
        {
            if (feeding != true)
            {
                agentComponent.SetDestination(followPlayer.position);

                if (isTouching && cooldown != true) //If Dog touched the player, stop for a moment, deal damage
                {
                    agentComponent.speed = 0;
                    HealthBar.instance.Damage(2000);
                    cooldown = true;
                }
            }
            else 
            {
                nextState = "Fed";
                isFed = true; 
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Normal state of Dog (Wandering)
    /// </summary>
    IEnumerator Normal()
    {
        Debug.Log("Normal");
        agentComponent.SetDestination(checkpoints[currentCheckpointIndex].position);
        bool hasReached = false;

        while (currentState == "Normal")
        {
            if (inRange)
            {
                nextState = "Chasing";
            }
            else
            {
                if (hasReached != true)
                {
                    if (agentComponent.remainingDistance <= agentComponent.stoppingDistance) //If Dog is close/on position, cosider it reached
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
    /// Delay before Dog can continue to chase
    /// </summary>
    private void TimeDelay()
    {
        if (cooldown)
        {
            if (currentDuration < 1.0f)
            {
                currentDuration += Time.deltaTime;
            }
            else if (currentDuration >= 1.0f)
            {
                agentComponent.speed = 3;
                cooldown = false;
                currentDuration = 0;
            }
        }
    }

    /// <summary>
    /// To change the state of the Dog
    /// </summary>
    private void SwitchState()
    {
        StartCoroutine(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer = GameObject.FindWithTag("Player").transform;

        if (GameManager.gameManager.pause || GameManager.gameManager.dead || Player.instance.inDialogue)
        {
            agentComponent.isStopped = true;
        }
        else
        {
            agentComponent.isStopped = false;
        }

        if (currentState == "Chasing")
        {
            TimeDelay();
        }

        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }
}
