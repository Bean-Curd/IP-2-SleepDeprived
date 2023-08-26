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
    public string currentState;
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

        if (idNPC == "smallchild2" && Player.instance.child2Follow)
        {
            currentState = "Follow";
            nextState = currentState;
            SwitchState();
        }
        else
        {
            currentState = "Normal";
            nextState = currentState;
            SwitchState();
        }
    }

    /// <summary>
    /// Following state of NPC
    /// </summary>
    IEnumerator Follow()
    {
        Debug.Log("Following" + idNPC);
        isInteracting = false;
        canFollow = false;

        if (idNPC == "smallchild2" && Player.instance.child2Follow)
        {
            Player.instance.child2Follow = false;
        }

        while (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (followPlayer != null && currentState == "Follow")
            {
                agentComponent.SetDestination(followPlayer.position);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Talking state of NPC
    /// </summary>
    IEnumerator Talking()
    {
        Debug.Log("Talking" + idNPC);
        isInteracting = false;

        while (Player.instance.inDialogue)
        {
            if (canFollow && currentState == "Talking")
            {
                nextState = "Follow";
                Player.instance.inDialogue = false;
            }
            else if (resumeNormal && currentState == "Talking")
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
        Debug.Log("Normal" + idNPC);
        resumeNormal = false;

        while (isInteracting != true)
        {
            if (inRange != true) //If player not in range, walk around
            {
                // Walk stuff here
            }
            else if (Player.instance.inDialogue && currentState != "Follow") //Change state when player clicks on NPC
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
    public void SpawnCheck()
    {
        if (idNPC == "butcher")
        {
            if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 2) //Butcher location on the first meeting on the street
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed outside the butchery
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 3) //Butcher location on the first meeting in the butchery
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed at the counter
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 1 && Player.instance.doneMinigame1 && GameManager.gameManager.endMinigame1 != true && SceneManager.GetActiveScene().buildIndex == 3) //Butcher location in the butchery after completing minigame1
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn2"); //Placed above at the stairs
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.endMinigame1 && PlayerCanvas.instance.triggerText == "butcheryEntranceTrigger" && SceneManager.GetActiveScene().buildIndex == 3) //Butcher location in the butchery after finishing minigame1
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn3"); //Placed at the entrance
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 60, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.meetSmallChild2 != true && SceneManager.GetActiveScene().buildIndex == 3) //If have not met smallchild2 on day2 in the butchery
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed at the counter
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame3 != true && Minigame3.instance.numCaught == 2 && SceneManager.GetActiveScene().buildIndex == 3) //To show the butcher at the end of minigame3
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn4"); //Placed inside, near the bottom of the stairs
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame3 && PlayerCanvas.instance.cutsceneDay2.activeSelf != true && SceneManager.GetActiveScene().buildIndex == 3) //Butcher location in the butchery after completing minigame3
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                GameObject place = GameObject.Find("NPC_butcher_spawn2"); //Placed above at the stairs
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
            else if (PlayerCanvas.instance.cutsceneDay2.activeSelf && SceneManager.GetActiveScene().buildIndex == 3) //For day3 in the butchery
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed at the counter
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
            else if (GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 3) //For day3 in the butchery
            {
                GameObject place = GameObject.Find("NPC_butcher_spawn1"); //Placed at the counter
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 4 && PlayerCanvas.instance.overlay5.activeSelf != true && SceneManager.GetActiveScene().buildIndex == 3) //For day4 in the butchery
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                GameObject place = GameObject.Find("NPC_butcher_spawn5"); //Placed at the kitchen counter
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                gameObject.transform.position = place.transform.position;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
            else if ((GameManager.gameManager.dayCount == 5 || PlayerCanvas.instance.overlay5.activeSelf) && SceneManager.GetActiveScene().buildIndex == 3) //For day5 in the butchery
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                GameObject place = GameObject.Find("NPC_butcher_spawn6"); //Placed at the sofa
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 180));
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "smallchild1")
        {
            if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher && SceneManager.GetActiveScene().buildIndex == 2) //Smallchild1 location on the first meeting on the street
            {
                GameObject place = GameObject.Find("NPC_smallchild1_spawn1"); //Placed outside 
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.meetSmallChild1 && GameManager.gameManager.endMinigame1 != true && SceneManager.GetActiveScene().buildIndex == 3) //Smallchild1 location after heading to the butchery
            {
                GameObject place = GameObject.Find("NPC_smallchild1_spawn1"); //Placed on top
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "smallchild2")
        {
            if (GameManager.gameManager.entered && SceneManager.GetActiveScene().buildIndex != 3)
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame2 != true && SceneManager.GetActiveScene().buildIndex == 4) //Smallchild2 location on the first meeting in the park
            {
                GameObject place = GameObject.Find("NPC_smallchild2_spawn1"); //Placed outside 
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame2 && GameManager.gameManager.endMinigame3 != true && SceneManager.GetActiveScene().buildIndex == 2) //Smallchild2 location when following in the street
            {
                GameObject place = GameObject.Find("Child2FollowStreet"); //Placed near the player spawn
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                Player.instance.child2Follow = true;
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame2 && GameManager.gameManager.endMinigame3 != true && SceneManager.GetActiveScene().buildIndex == 4) //Smallchild2 location when following in the park
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                GameObject place = GameObject.Find("Child2FollowPark"); //Placed near the player spawn
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                Player.instance.child2Follow = true;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
            else if (GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame2 && GameManager.gameManager.endMinigame3 != true && SceneManager.GetActiveScene().buildIndex == 3) //Smallchild2 location in the butchery 
            {
                GameManager.gameManager.entered = true;
                GameObject place = GameObject.Find("NPC_smallchild2_spawn1"); //Placed outside 
                gameObject.transform.position = place.transform.position;
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "neighbour1")
        {
            if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour1 location on the first meeting on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour1_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour1 location on the second day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour1_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour1 location on the third day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour1_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour1 location on the fourth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour1_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 5 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour1 location on the fifth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour1_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "neighbour2")
        {
            if (GameManager.gameManager.dayCount == 1 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour2 location on the first meeting on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour2_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour2 location on the second day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour2_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 4) //Neighbour2 location on the second day in the park
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                GameObject place = GameObject.Find("NPC_neighbour2_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
            else if (GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour2 location on the third day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour2_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour2 location on the fourth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour2_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (GameManager.gameManager.dayCount == 5 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour2 location on the fifth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour2_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "neighbour3")
        {
            if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour3 location on the first meeting on the street (after butcher)
            {
                GameObject place = GameObject.Find("NPC_neighbour3_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
            }
            else if (GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour3 location on the second day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour3_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
            }
            else if (GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour3 location on the third day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour3_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
            }
            else if (GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour3 location on the fourth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour3_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
            }
            else 
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "neighbour4")
        {
            if (GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour4 location on the first meeting on the street (after butcher)
            {
                GameObject place = GameObject.Find("NPC_neighbour4_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour4 location on the second day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour4_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour4 location on the third day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour4_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour4 location on the fourth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour4_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (GameManager.gameManager.dayCount == 5 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour4 location on the fifth day on the street
            {
                GameObject place = GameObject.Find("NPC_neighbour4_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
                gameObject.transform.position = place.transform.position;
            }
        }
        else if (idNPC == "neighbour5")
        {
            if (GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //Neighbour5 location on the street when delivering packages on the 3rd day
            {
                GameObject place = GameObject.Find("NPC_neighbour5_spawn1"); //Placed outside
                gameObject.transform.position = place.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else
            {
                GameObject place = GameObject.Find("VoidSpawn"); //To hide it
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
            if (idNPC == "butcher" && GameManager.gameManager.dayCount == 5)
            {

            }
            else if ((idNPC != "butcher" && GameManager.gameManager.dayCount != 5))
            {
                agentComponent.isStopped = true;
            }
        }
        else
        {
            if (idNPC == "butcher" && GameManager.gameManager.dayCount == 5)
            {

            }
            else if ((idNPC != "butcher" && GameManager.gameManager.dayCount != 5))
            {
                agentComponent.isStopped = false;
            }
        }

        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }
}
