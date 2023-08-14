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
    /// Can NPC continue walking
    /// </summary>
    public bool resumeNormal;

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
        SpawnCheck();

        inRange = false;
        isInteracting = false;
        canFollow = false;
        resumeNormal = false;

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

        while (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (followPlayer != null && currentState == "Follow")
            {
                agentComponent.SetDestination(followPlayer.position);
            }
            else if (SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 4) //Destroy if player left the street or park 
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
        isInteracting = false;

        while (Player.instance.inDialogue)
        {
            if (canFollow)
            {
                Debug.Log("here");
                nextState = "Follow";
                Player.instance.inDialogue = false;
            }
            else if (resumeNormal)
            {
                nextState = "Normal";
                Player.instance.inDialogue = false;
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
        resumeNormal = false;

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

    /// <summary>
    /// To check how to spawn the npc
    /// </summary>
    private void SpawnCheck()
    {
        if (idNPC == "butcher")
        {
            if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 2) //Butcher location on the first meeting on the street
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed outside the butchery
                gameObject.transform.position = place.transform.position;
            }
            else if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 3) //Butcher location on the first meeting in the butchery
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed at the counter
                gameObject.transform.position = place.transform.position;
            }
        }
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
