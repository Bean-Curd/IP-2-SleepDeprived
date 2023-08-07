/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 02/08/2023
 * Description: IP2 - NPC AI and States
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;

public class NPC : MonoBehaviour
{
    /// <summary>
    /// Name for specific NPC
    /// </summary>
    public string idNPC;
    /// <summary>
    /// Is the player in NPC's range
    /// </summary>
    public bool inRange;
    /// <summary>
    /// Did the player click on the NPC
    /// </summary>
    public bool isInteracting;
    /// <summary>
    /// Can that NPC follow the player
    /// </summary>
    public bool canFollow;

    /// <summary>
    /// Current state of the NPC
    /// </summary>
    private string currentState;
    /// <summary>
    /// Next state for the NPC to change to
    /// </summary>
    private string nextState;

    /// <summary>
    /// NPC Agent NavMesh
    /// </summary>
    NavMeshAgent agentComponent;
    /// <summary>
    /// What the NPC follows
    /// </summary>
    Transform followPlayer;

    /// <summary>
    /// Set the NPC as an instance
    /// </summary>
    public static NPC instance;

    private void Awake()
    {
        instance = this;
        agentComponent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        isInteracting = false;
        canFollow = false;

        currentState = "Normal";
        nextState = currentState;
        SwitchState();
    }

    /// <summary>
    /// Following state of NPC
    /// </summary>
    IEnumerator Follow()
    {
        Debug.Log("Following");
        canFollow = false;

        while (SceneManager.GetActiveScene().buildIndex == 1/*2*/ || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (followPlayer != null && currentState == "Follow")
            {
                agentComponent.SetDestination(followPlayer.position);
            }
            else if (SceneManager.GetActiveScene().buildIndex != 1/*2*/ && SceneManager.GetActiveScene().buildIndex != 4) //Destroy if player left the street or park 
            {
                Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Talking state of NPC
    /// </summary>
    IEnumerator Talking()
    {
        Debug.Log("Talking");

        while (Player.instance.inDialogue)
        {
            if (canFollow)
            {
                nextState = "Follow";
                Player.instance.inDialogue = false;
            }
            else
            {
                nextState = "Normal";
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Normal state of NPC
    /// </summary>
    IEnumerator Normal()
    {
        Debug.Log("Normal");

        while (isInteracting != true)
        {
            if (inRange != true) //If player not in range, walk around
            {
                // Walk stuff here
            }
            else if (Player.instance.inDialogue) //Change state when player clicks on NPC
            {
                nextState = "Talking";
                isInteracting = true;
            }
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// To change the state of the NPC
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

        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }
}
