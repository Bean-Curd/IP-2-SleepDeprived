/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 30/07/2023
 * Description: IP2 - Player's Canvas
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Reflection;

public class PlayerCanvas : MonoBehaviour
{
    #region Player Menu Related Variables

    /// <summary>
    /// Pause menu
    /// </summary>
    public GameObject pauseMenu;
    /// <summary>
    /// Death menu
    /// </summary>
    public GameObject deathMenu;
    /// <summary>
    /// Death menu
    /// </summary>
    public GameObject loadingMenu;
    /// <summary>
    /// Empty image
    /// </summary>
    public Sprite voidMenu;
    /// <summary>
    /// Inventory menu
    /// </summary>
    public GameObject inventoryMenu;
    /// <summary>
    /// To access the data in each item for the inventory
    /// </summary>
    public Transform InventoryContent;
    /// <summary>
    /// To access the data for the selected item
    /// </summary>
    public Transform SelectedItem;

    /// <summary>
    /// Loading1 as game object
    /// </summary>
    public GameObject loading1;

    #endregion

    #region Trigger Related Variables

    /// <summary>
    /// When interacting with the kitchen door in the boy's house
    /// </summary>
    public GameObject boyKitchenDoor;
    /// <summary>
    /// When interacting with the boy's mother's door in the boy's house
    /// </summary>
    public GameObject boyMotherDoor;
    /// <summary>
    /// When interacting with the boy's room door in the boy's house
    /// </summary>
    public GameObject boyRoomDoor;
    /// <summary>
    /// When interacting with the meat when player has 3 in inventory
    /// </summary>
    public GameObject meatLimit;
    /// <summary>
    /// Butchery Door during minigame3
    /// </summary>
    public GameObject butcheryDoorMinigame3;
    /// <summary>
    /// Freezer door normally
    /// </summary>
    public GameObject freezerDoorNormal;
    /// <summary>
    /// Freezer door during minigame3/line 1
    /// </summary>
    public GameObject freezerDoorMinigame3L1;
    /// <summary>
    /// Freezer door during minigame3/line 2
    /// </summary>
    public GameObject freezerDoorMinigame3L2;
    /// <summary>
    /// Freezer door during minigame3/line 3
    /// </summary>
    public GameObject freezerDoorMinigame3L3;
    /// <summary>
    /// Freezer door during minigame3/line 4
    /// </summary>
    public GameObject freezerDoorMinigame3L4;
    /// <summary>
    /// Freezer door during minigame3/line 5
    /// </summary>
    public GameObject freezerDoorMinigame3L5;
    /// <summary>
    /// Freezer door during minigame3/line 6
    /// </summary>
    public GameObject freezerDoorMinigame3L6;

    #endregion

    #region Dialogue Related Variables

    /// <summary>
    /// To store the conversation for the NPC the player is interacting with
    /// </summary>
    public string convoNPC;
    /// <summary>
    /// To store the conversation for the trigger the player is interacting with
    /// </summary>
    public string triggerText;
    /// <summary>
    /// How many clicks in the dialogue so far
    /// </summary>
    public int dialogueClick;

    /// <summary>
    /// Health and Stamina Bar
    /// </summary>
    public GameObject playerBars;
    /// <summary>
    /// Dialogue box
    /// </summary>
    public GameObject dialogueBox;

    /// <summary>
    /// To play the intro once
    /// </summary>
    public bool introTime;
    /// <summary>
    /// Dialogue for intro/line 1
    /// </summary>
    public GameObject introL1;
    /// <summary>
    /// Dialogue for intro/line 2
    /// </summary>
    public GameObject introL2;


    /// <summary>
    /// Dialogue for interaction with neighbour1/line 1 before meeting the butcher
    /// </summary>
    public GameObject neighbour1askfoodL1;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 2 before meeting the butcher
    /// </summary>
    public GameObject neighbour1askfoodL2;

    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 before meeting the butcher
    /// </summary>
    public GameObject neighbour2askfoodL1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 before meeting the butcher
    /// </summary>
    public GameObject neighbour2askfoodL2;

    /// <summary>
    /// Dialogue for interaction with butcher/line 1 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL1;
    /// <summary>
    /// Dialogue for interaction with butcher/line 2 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL2;
    /// <summary>
    /// Dialogue for interaction with butcher/line 3 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL3;


    /// <summary>
    /// Dialogue for 1st interaction/line 1 with smallchild1
    /// </summary>
    public GameObject smallchild1T1L1;
    /// <summary>
    /// Dialogue for 1st interaction/line 2 with smallchild1
    /// </summary>
    public GameObject smallchild1T1L2;
    /// <summary>
    /// Dialogue for 1st interaction/line 3 with smallchild1
    /// </summary>
    public GameObject smallchild1T1L3;
    /// <summary>
    /// Dialogue for 1st interaction/line 4 with smallchild1
    /// </summary>
    public GameObject smallchild1T1L4;


    /// <summary>
    /// Dialogue for interaction with smallchild1 to start minigame1
    /// </summary>
    public GameObject startminigame1;

    /// <summary>
    /// Dialogue for winning minigame1
    /// </summary>
    public GameObject minigame1win;
    /// <summary>
    /// Dialogue for tied minigame1
    /// </summary>
    public GameObject minigame1tie;
    /// <summary>
    /// Dialogue for losing minigame1
    /// </summary>
    public GameObject minigame1lose;

    /// <summary>
    /// Dialogue for completing minigame1/line 1
    /// </summary>
    public GameObject minigame1L1;
    /// <summary>
    /// Dialogue for completing minigame1/line 2
    /// </summary>
    public GameObject minigame1L2;
    /// <summary>
    /// Dialogue for completing minigame1/line 3
    /// </summary>
    public GameObject minigame1L3;
    /// <summary>
    /// Dialogue for completing minigame1/line 4
    /// </summary>
    public GameObject minigame1L4;
    /// <summary>
    /// Dialogue for completing minigame1/line 5
    /// </summary>
    public GameObject minigame1L5;
    /// <summary>
    /// Dialogue for completing minigame1/line 6
    /// </summary>
    public GameObject minigame1L6;
    /// <summary>
    /// Dialogue for completing minigame1/line 7
    /// </summary>
    public GameObject minigame1L7;
    /// <summary>
    /// Dialogue for completing minigame1/line 8
    /// </summary>
    public GameObject minigame1L8;


    /// <summary>
    /// Dialogue for interaction with smallchild2/line 1 to start minigame2
    /// </summary>
    public GameObject startminigame2L1;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 2 to start minigame2
    /// </summary>
    public GameObject startminigame2L2;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 3 to start minigame2
    /// </summary>
    public GameObject startminigame2L3;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 4 to start minigame2
    /// </summary>
    public GameObject startminigame2L4;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 5 to start minigame2
    /// </summary>
    public GameObject startminigame2L5;

    /// <summary>
    /// Dialogue for winning minigame2
    /// </summary>
    public GameObject minigame2win;
    /// <summary>
    /// Dialogue for losing minigame2
    /// </summary>
    public GameObject minigame2lose;
    /// <summary>
    /// Dialogue for completing minigame2/line 1
    /// </summary>
    public GameObject minigame2L1;


    /// <summary>
    /// Dialogue for interaction with smallchild2/line 1 to start minigame3
    /// </summary>
    public GameObject startminigame3L1;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 2 to start minigame3
    /// </summary>
    public GameObject startminigame3L2;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 3 to start minigame3
    /// </summary>
    public GameObject startminigame3L3;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 4 to start minigame3
    /// </summary>
    public GameObject startminigame3L4;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 5 to start minigame3
    /// </summary>
    public GameObject startminigame3L5;

    /// <summary>
    /// Dialogue for interaction with smallchild2/line 1 when caught first in minigame3
    /// </summary>
    public GameObject minigame3Caught1L1;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 2 when caught first in minigame3
    /// </summary>
    public GameObject minigame3Caught1L2;

    /// <summary>
    /// Dialogue for interaction with smallchild2/line 1 when caught second in minigame3
    /// </summary>
    public GameObject minigame3Caught2L1;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 2 when caught second in minigame3
    /// </summary>
    public GameObject minigame3Caught2L2;
    /// <summary>
    /// Dialogue for interaction with smallchild2/line 3 when caught second in minigame3
    /// </summary>
    public GameObject minigame3Caught2L3;

    #endregion

    #region Minigame Related Variables

    /// <summary>
    /// To start Minigame1
    /// </summary>
    public GameObject minigame1;
    /// <summary>
    /// To start Minigame2
    /// </summary>
    public GameObject minigame2;
    /// <summary>
    /// To start Minigame3
    /// </summary>
    public GameObject minigame3;

    #endregion

    /// <summary>
    /// Set the player's canvas as an instance
    /// </summary>
    public static PlayerCanvas instance;

    private void Awake()
    {
        instance = this;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Return the player to start menu
    /// </summary>
    public void Leave()
    {
        GameManager.gameManager.pause = false;
        GameManager.gameManager.dead = false;
        GameManager.gameManager.LoadingScreen();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Return the player back to the game after a pause
    /// </summary>
    public void PauseReturn()
    {
        Cursor.lockState = CursorLockMode.Locked; //To lock the mouse
        GameManager.gameManager.pause = false;
    }

    /// <summary>
    /// Respawns the player back after they died
    /// </summary>
    public void DeathRespawn()
    {
        Debug.Log("Respawning");

        Cursor.lockState = CursorLockMode.Locked; //To lock the mouse
        GameManager.gameManager.dead = false;
        GameManager.gameManager.respawn = true;
    }

    /// <summary>
    /// To trigger the selected item from the canvas
    /// </summary>
    public void ItemSelected()
    {
        GameManager.gameManager.SelectedItem();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueClick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.dead) //If currently dead, show death menu
        {
            Cursor.lockState = CursorLockMode.None; //To unlock the mouse
            deathMenu.SetActive(true);
        }
        else if (GameManager.gameManager.dead != true) //If not dead, hide death menu
        {
            deathMenu.SetActive(false);
        }

        if (GameManager.gameManager.inventory) //If in inventory, show inventory menu
        {
            inventoryMenu.SetActive(true);
        }
        else if (GameManager.gameManager.pause) //If currently paused, show pause menu
        {
            pauseMenu.SetActive(true);
        }
        else if (GameManager.gameManager.pause != true) //If not paused, hide paused menu and inventory menu
        {
            inventoryMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }

        if (GameManager.gameManager.pause != true && Input.GetMouseButtonDown(0)) //If game is not paused and left click is pressed, change dialogue
        {
            if (Player.instance.inDialogue) //If player currently talking/has dialogue
            {
                if (SceneManager.GetActiveScene().buildIndex == 1 && introTime) //To play the intro
                {
                    introL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        introL2.SetActive(false);
                        introTime = false;
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 1)
                    {
                        introL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour1askfood" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true) //If talking to neighbour1 on the first day before meeting the butcher
                {
                    neighbour1askfoodL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour1askfoodL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour1");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour1askfoodL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2askfood" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true) //If talking to neighbour2 on the first day before meeting the butcher
                {
                    neighbour2askfoodL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour2askfoodL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2askfoodL2.SetActive(true);
                    }
                }
                else if (convoNPC == "butcheraskfood" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 2) //If talking to butcher on the first day in the street to ask for food
                {
                    butcheraskfoodL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 2)
                    {
                        butcheraskfoodL3.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcheraskfoodL2.SetActive(false);
                        butcheraskfoodL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcheraskfoodL2.SetActive(true);
                    }
                }
                else if (convoNPC == "smallchild1meet") //If meeting smallchild1 for the first time
                {
                    smallchild1T1L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        smallchild1T1L4.SetActive(false);
                        GameManager.gameManager.meetSmallChild1 = true;
                        GameObject npc = GameObject.Find("NPC_smallchild1");
                        npc.GetComponent<NPC>().canFollow = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        smallchild1T1L3.SetActive(false);
                        smallchild1T1L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        smallchild1T1L2.SetActive(false);
                        smallchild1T1L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        smallchild1T1L2.SetActive(true);
                    }
                }
                else if (convoNPC == "startminigame1" && minigame1.activeSelf != true && Player.instance.doneMinigame1 != true) //If starting minigame1 and not already active and not done with game, start game 
                {
                    startminigame1.SetActive(false);
                    Cursor.lockState = CursorLockMode.None; //To unlock the mouse
                    minigame1.SetActive(true);
                }
                else if (Player.instance.doneMinigame1 && convoNPC == "startminigame1") //If done with minigame1
                {
                    minigame1.SetActive(false);
                    minigame1win.SetActive(false);
                    minigame1tie.SetActive(false);
                    minigame1lose.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 8)
                    {
                        minigame1L8.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_smallchild1");
                        npc.GetComponent<NPC>().resumeNormal = true;
                        GameManager.gameManager.endMinigame1 = true;
                    }
                    else if (dialogueClick == 8)
                    {
                        minigame1L7.SetActive(false);
                        minigame1L8.SetActive(true);
                    }
                    else if (dialogueClick == 7)
                    {
                        minigame1L6.SetActive(false);
                        minigame1L7.SetActive(true);
                    }
                    else if (dialogueClick == 6)
                    {
                        minigame1L5.SetActive(false);
                        minigame1L6.SetActive(true);
                    }
                    else if (dialogueClick == 5)
                    {
                        minigame1L4.SetActive(false);
                        minigame1L5.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        minigame1L3.SetActive(false);
                        minigame1L4.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        minigame1L2.SetActive(false);
                        minigame1L3.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        minigame1L1.SetActive(false);
                        minigame1L2.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        minigame1L1.SetActive(true);
                    }
                }
                else if (convoNPC == "startminigame2" && minigame2.activeSelf != true && Player.instance.doneMinigame2 != true && Player.instance.playerWin != true) //If starting minigame2 and not already active and not done with game, start game 
                {
                    startminigame2L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        startminigame2L5.SetActive(false);
                        Cursor.lockState = CursorLockMode.None; //To unlock the mouse
                        minigame2.SetActive(true);
                        dialogueClick = 0;
                    }
                    else if (dialogueClick == 4)
                    {
                        startminigame2L4.SetActive(false);
                        startminigame2L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        startminigame2L3.SetActive(false);
                        startminigame2L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        startminigame2L2.SetActive(false);
                        startminigame2L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        startminigame2L2.SetActive(true);
                    }
                }
                else if (convoNPC == "startminigame2" && minigame2.activeSelf != true && Player.instance.doneMinigame2 != true && Player.instance.playerWin) //If player lost, replay minigame2
                {
                    minigame2.SetActive(false);
                    minigame2win.SetActive(false);
                    minigame2lose.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        startminigame2L5.SetActive(false);
                        Cursor.lockState = CursorLockMode.None; //To unlock the mouse
                        minigame2.SetActive(true);
                        dialogueClick = 0;
                    }
                    else if (dialogueClick == 1)
                    {
                        startminigame2L5.SetActive(true);
                    }
                }
                else if (Player.instance.doneMinigame2 && convoNPC == "startminigame2") //If done with minigame2
                {
                    minigame2.SetActive(false);
                    minigame2win.SetActive(false);
                    minigame2lose.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        minigame2L1.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_smallchild2");
                        npc.GetComponent<NPC>().canFollow = true;
                        GameManager.gameManager.endMinigame2 = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        minigame2L1.SetActive(true);
                    }
                }
                else if (convoNPC == "startminigame3" && SceneManager.GetActiveScene().buildIndex == 3) //If talking to smallchild2 in the butchery
                {
                    startminigame3L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        startminigame3L5.SetActive(false);
                        minigame3.SetActive(true);
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        player.transform.position = GameManager.gameManager.spawn1.transform.position;
                        Minigame3.instance.round = 1;
                        Minigame3.instance.timeDelayStart = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        startminigame3L4.SetActive(false);
                        startminigame3L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        startminigame3L3.SetActive(false);
                        startminigame3L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        startminigame3L2.SetActive(false);
                        startminigame3L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        startminigame3L2.SetActive(true);
                    }
                }
                else if (convoNPC == "minigame3Caught1") //If talking to smallchild2 during minigame3 after getting caught once
                {
                    minigame3Caught1L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        minigame3Caught1L2.SetActive(false);
                        minigame3.SetActive(true);
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        player.transform.position = GameManager.gameManager.spawn1.transform.position;
                        Minigame3.instance.round = 2;
                        Minigame3.instance.numCaught = 1;
                        Minigame3.instance.timeDelayStart = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        minigame3Caught1L2.SetActive(true);
                    }
                }
                else if (convoNPC == "minigame3Caught2") //If talking to smallchild2 during minigame3 after getting caught twice
                {
                    minigame3Caught2L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 2)
                    {
                        minigame3Caught2L3.SetActive(false);
                        minigame3.SetActive(true);
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        player.transform.position = GameManager.gameManager.spawn1.transform.position;
                        Minigame3.instance.round = 3;
                        Minigame3.instance.numCaught = 2;
                        Minigame3.instance.timeDelayStart = true;
                    }
                    else if (dialogueClick == 2)
                    {
                        minigame3Caught2L2.SetActive(false);
                        minigame3Caught2L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        minigame3Caught2L2.SetActive(true);
                    }
                }
                else if (triggerText == "boyKitchenDoor") //If approaching the kitchen door in the boy's house
                {
                    boyKitchenDoor.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "boyMotherDoor") //If approaching the boy's mother's door in the boy's house
                {
                    boyMotherDoor.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "boyRoomDoor") //If approaching the boy's room door in the boy's house
                {
                    boyRoomDoor.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "meatLimit") //If have 3 meat,
                {
                    meatLimit.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "butcheryDoorMinigame3") //If approaching the butchery door during minigame3
                {
                    butcheryDoorMinigame3.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "freezerDoorNormal") //If approaching the freezer door
                {
                    freezerDoorNormal.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "freezerDoorMinigame3" && Minigame3.instance.round == 3) //If approaching the freezer during minigame3 after getting caught twice
                {
                    freezerDoorMinigame3L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 5)
                    {
                        freezerDoorMinigame3L6.SetActive(false);
                        Minigame3.instance.round = 0;
                        GameManager.gameManager.endMinigame3 = true;
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 5)
                    {
                        freezerDoorMinigame3L5.SetActive(false);
                        freezerDoorMinigame3L6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        freezerDoorMinigame3L4.SetActive(false);
                        freezerDoorMinigame3L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        freezerDoorMinigame3L3.SetActive(false);
                        freezerDoorMinigame3L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        freezerDoorMinigame3L2.SetActive(false);
                        freezerDoorMinigame3L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        freezerDoorMinigame3L2.SetActive(true);
                    }
                }
            }
        }


        if (Player.instance.inDialogue != true) //If player is no longer talking, clear variables
        {
            dialogueBox.SetActive(false);
            playerBars.SetActive(true);

            if (dialogueClick != 0) //To check if variables have not been cleared before
            {
                Player.instance.idNPC = "";
                Player.instance.idTrigger = "";
                convoNPC = "";
                dialogueClick = 0;
            }
        }
    }
}
