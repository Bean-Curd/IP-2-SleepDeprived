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
using System.Text.RegularExpressions;
using UnityEngine.AI;

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
    /// To show that an item has been collected
    /// </summary>
    public GameObject itemCollected;

    /// <summary>
    /// For overlay1 
    /// </summary>
    public GameObject overlay1;
    /// <summary>
    /// For overlay2
    /// </summary>
    public GameObject overlay2;
    /// <summary>
    /// For overlay3 
    /// </summary>
    public GameObject overlay3;
    /// <summary>
    /// For overlay4 
    /// </summary>
    public GameObject overlay4;
    /// <summary>
    /// For overlay5 
    /// </summary>
    public GameObject overlay5;

    /// <summary>
    /// Loading1 as game object
    /// </summary>
    public GameObject loading1;

    /// <summary>
    /// For when the player gets caught by the police
    /// </summary>
    public GameObject spaceToEscape;

    #endregion

    #region Trigger Related Variables

    /// <summary>
    /// To wait for dinner
    /// </summary>
    public bool dinnerWait;


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

    /// <summary>
    /// When interacting with the butcher's door in the butcher's house
    /// </summary>
    public GameObject butcherRoomDoor;
    /// <summary>
    /// When interacting with the butcher's guest door in the butcher's house
    /// </summary>
    public GameObject butcherGuestDoorDay1Normal;
    /// <summary>
    /// When near the entrance to wait for the butcher
    /// </summary>
    public GameObject butcheryEntranceTriggerL1;

    /// <summary>
    /// For day 1 dinner loading screen
    /// </summary>
    public GameObject dinner1;
    /// <summary>
    /// For day 1 dinner/line 1
    /// </summary>
    public GameObject dinner1L1;
    /// <summary>
    /// For day 1 dinner/line 2
    /// </summary>
    public GameObject dinner1L2;
    /// <summary>
    /// For day 1 dinner/line 3
    /// </summary>
    public GameObject dinner1L3;
    /// <summary>
    /// For day 1 dinner/line 4
    /// </summary>
    public GameObject dinner1L4;
    /// <summary>
    /// For day 1 dinner/line 5
    /// </summary>
    public GameObject dinner1L5;


    /// <summary>
    /// Day1's cutscene
    /// </summary>
    public GameObject cutsceneDay1;

    /// <summary>
    /// Day2's intro/line 1
    /// </summary>
    public GameObject day2StartL1;
    /// <summary>
    /// Day2's intro/line 2
    /// </summary>
    public GameObject day2StartL2;
    /// <summary>
    /// Day2's intro/line 3
    /// </summary>
    public GameObject day2StartL3;


    /// <summary>
    /// Day2 trigger to prevent player from leaving the butchery after minigame 3
    /// </summary>
    public GameObject day2GoUpTriggerL1;
    /*/// <summary>
    /// Check if day2 trigger can be on
    /// </summary>
    public bool day2triggercheck;*/
    /// <summary>
    /// When at the top of the butchery to wait for the butcher
    /// </summary>
    public GameObject day2CutsceneTriggerL1;

    /// <summary>
    /// For day 2 dinner loading screen
    /// </summary>
    public GameObject dinner2;
    /// <summary>
    /// For day 2 dinner/line 1
    /// </summary>
    public GameObject dinner2L1;
    /// <summary>
    /// For day 2 dinner/line 2
    /// </summary>
    public GameObject dinner2L2;
    /// <summary>
    /// For day 2 dinner/line 3
    /// </summary>
    public GameObject dinner2L3;
    /// <summary>
    /// For day 2 dinner/line 4
    /// </summary>
    public GameObject dinner2L4;

    /// <summary>
    /// Day2's cutscene
    /// </summary>
    public GameObject cutsceneDay2;

    /// <summary>
    /// Day3's intro/line 1
    /// </summary>
    public GameObject day3StartL1;
    /// <summary>
    /// Day3's intro/line 2
    /// </summary>
    public GameObject day3StartL2;
    /// <summary>
    /// Day3's intro/line 3
    /// </summary>
    public GameObject day3StartL3;


    /// <summary>
    /// Day3's police warning/line 1
    /// </summary>
    public GameObject policeWarningDay3L1;
    /// <summary>
    /// Day3's police warning/line 2
    /// </summary>
    public GameObject policeWarningDay3L2;

    /// <summary>
    /// Day4's police warning/line 1
    /// </summary>
    public GameObject policeWarningDay4L1;
    /// <summary>
    /// Day4's police warning/line 2
    /// </summary>
    public GameObject policeWarningDay4L2;


    /// <summary>
    /// For day 3 dinner loading screen
    /// </summary>
    public GameObject dinner3;
    /// <summary>
    /// For day 3 dinner alternate loading screen
    /// </summary>
    public GameObject ALTdinner3;
    /// <summary>
    /// For day 3 dinner/line 1
    /// </summary>
    public GameObject butcherDay3DoneL1;
    /// <summary>
    /// For day 3 dinner/line 2
    /// </summary>
    public GameObject butcherDay3DoneL2;
    /// <summary>
    /// For day 3 dinner/line 3
    /// </summary>
    public GameObject butcherDay3DoneL3;
    /// <summary>
    /// For day 3 dinner/line 4
    /// </summary>
    public GameObject butcherDay3DoneL4;

    /// <summary>
    /// Day3's cutscene
    /// </summary>
    public GameObject cutsceneDay3;

    /// <summary>
    /// Day4's intro/line 1
    /// </summary>
    public GameObject day4StartL1;
    /// <summary>
    /// Day4's intro/line 2
    /// </summary>
    public GameObject day4StartL2;
    /// <summary>
    /// Day4's intro/line 3
    /// </summary>
    public GameObject day4StartL3;


    /// <summary>
    /// For day 4 dinner loading screen
    /// </summary>
    public GameObject dinner4;
    /// <summary>
    /// For day 4 dinner/line 1
    /// </summary>
    public GameObject butcherDay4DoneL1;
    /// <summary>
    /// For day 4 dinner/line 2
    /// </summary>
    public GameObject butcherDay4DoneL2;
    /// <summary>
    /// For day 4 dinner/line 3
    /// </summary>
    public GameObject butcherDay4DoneL3;
    /// <summary>
    /// For day 4 dinner/line 4
    /// </summary>
    public GameObject butcherDay4DoneL4;

    /// <summary>
    /// Day4's cutscene
    /// </summary>
    public GameObject cutsceneDay4;

    /// <summary>
    /// Day5's intro/line 1
    /// </summary>
    public GameObject day5StartL1;
    /// <summary>
    /// Day5's intro/line 2
    /// </summary>
    public GameObject day5StartL2;
    /// <summary>
    /// Day5's intro/line 3
    /// </summary>
    public GameObject day5StartL3;
    /// <summary>
    /// Day5's intro/line 4
    /// </summary>
    public GameObject day5StartL4;


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
    /// Dialogue for interaction with butcher/line 4 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL4;
    /// <summary>
    /// Dialogue for interaction with butcher/line 5 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL5;
    /// <summary>
    /// Dialogue for interaction with butcher/line 6 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL6;
    /// <summary>
    /// Dialogue for interaction with butcher/line 7 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL7;
    /// <summary>
    /// Dialogue for interaction with butcher/line 8 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL8;
    /// <summary>
    /// Dialogue for interaction with butcher/line 9 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL9;
    /// <summary>
    /// Dialogue for interaction with butcher/line 10 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL10;
    /// <summary>
    /// Dialogue for interaction with butcher/line 11 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL11;
    /// <summary>
    /// Dialogue for interaction with butcher/line 12 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL12;
    /// <summary>
    /// Dialogue for interaction with butcher/line 13 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL13;
    /// <summary>
    /// Dialogue for interaction with butcher/line 14 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL14;
    /// <summary>
    /// Dialogue for interaction with butcher/line 15 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL15;
    /// <summary>
    /// Dialogue for interaction with butcher/line 16 to ask for food in the street
    /// </summary>
    public GameObject butcheraskfoodL16;


    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 after meeting the butcher
    /// </summary>
    public GameObject neighbour2askplayL1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 after meeting the butcher
    /// </summary>
    public GameObject neighbour2askplayL2;

    /// <summary>
    /// Dialogue for interaction with neighbour3/line 1 after meeting the butcher
    /// </summary>
    public GameObject neighbour3askplayL1;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 2 after meeting the butcher
    /// </summary>
    public GameObject neighbour3askplayL2;

    /// <summary>
    /// Dialogue for interaction with neighbour4/line 1 after meeting the butcher
    /// </summary>
    public GameObject neighbour4askplayL1;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 2 after meeting the butcher
    /// </summary>
    public GameObject neighbour4askplayL2;


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
    /// Dialogue for talking to the butcher at the start of day2/line 1
    /// </summary>
    public GameObject butcherDay2WakeL1;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day2/line 2
    /// </summary>
    public GameObject butcherDay2WakeL2;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day2/line 3
    /// </summary>
    public GameObject butcherDay2WakeL3;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day2/line 4
    /// </summary>
    public GameObject butcherDay2WakeL4;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day2/line 5
    /// </summary>
    public GameObject butcherDay2WakeL5;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day2/line 6
    /// </summary>
    public GameObject butcherDay2WakeL6;


    /// <summary>
    /// Dialogue for interaction with neighbour1/line 1 in day2
    /// </summary>
    public GameObject neighbour1helloL1;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 2 in day2
    /// </summary>
    public GameObject neighbour1helloL2;

    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 in day2
    /// </summary>
    public GameObject neighbour2helloL1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 in day2
    /// </summary>
    public GameObject neighbour2helloL2;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 3 in day2
    /// </summary>
    public GameObject neighbour2helloL3;

    /// <summary>
    /// Dialogue for interaction with neighbour3/line 1 in day2
    /// </summary>
    public GameObject neighbour3helloL1;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 2 in day2
    /// </summary>
    public GameObject neighbour3helloL2;

    /// <summary>
    /// Dialogue for interaction with neighbour4/line 1 in day2
    /// </summary>
    public GameObject neighbour4helloL1;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 2 in day2
    /// </summary>
    public GameObject neighbour4helloL2;


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


    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 in the park
    /// </summary>
    public GameObject neighbour2parkL1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 in the park
    /// </summary>
    public GameObject neighbour2parkL2;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 3 in the park
    /// </summary>
    public GameObject neighbour2parkL3;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 4 in the park
    /// </summary>
    public GameObject neighbour2parkL4;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 5 in the park
    /// </summary>
    public GameObject neighbour2parkL5;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 6 in the park
    /// </summary>
    public GameObject neighbour2parkL6;


    /// <summary>
    /// Dialogue for talking to the butcher at the start of day3/line 1
    /// </summary>
    public GameObject butcherDay3WakeL1;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day3/line 2
    /// </summary>
    public GameObject butcherDay3WakeL2;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day3/line 3
    /// </summary>
    public GameObject butcherDay3WakeL3;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day3/line 4
    /// </summary>
    public GameObject butcherDay3WakeL4;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day3/line 5
    /// </summary>
    public GameObject butcherDay3WakeL5;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day3/line 6
    /// </summary>
    public GameObject butcherDay3WakeL6;


    /// <summary>
    /// Dialogue for interaction with neighbour1/line 1 in day3
    /// </summary>
    public GameObject neighbour1packageL1;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 2 in day3
    /// </summary>
    public GameObject neighbour1packageL2;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 3 in day3
    /// </summary>
    public GameObject neighbour1packageL3;

    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 in day3
    /// </summary>
    public GameObject neighbour2packageL1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 in day3
    /// </summary>
    public GameObject neighbour2packageL2;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 3 in day3
    /// </summary>
    public GameObject neighbour2packageL3;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 4 in day3
    /// </summary>
    public GameObject neighbour2packageL4;

    /// <summary>
    /// Dialogue for interaction with neighbour3/line 1 in day3
    /// </summary>
    public GameObject neighbour3packageL1;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 2 in day3
    /// </summary>
    public GameObject neighbour3packageL2;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 3 in day3
    /// </summary>
    public GameObject neighbour3packageL3;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 4 in day3
    /// </summary>
    public GameObject neighbour3packageL4;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 5 in day3
    /// </summary>
    public GameObject neighbour3packageL5;

    /// <summary>
    /// Dialogue for interaction with neighbour4/line 1 in day3
    /// </summary>
    public GameObject neighbour4packageL1;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 2 in day3
    /// </summary>
    public GameObject neighbour4packageL2;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 3 in day3
    /// </summary>
    public GameObject neighbour4packageL3;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 4 in day3
    /// </summary>
    public GameObject neighbour4packageL4;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 5 in day3
    /// </summary>
    public GameObject neighbour4packageL5;

    /// <summary>
    /// Dialogue for interaction with neighbour5/line 1 in day3
    /// </summary>
    public GameObject neighbour5packageL1;
    /// <summary>
    /// Dialogue for interaction with neighbour5/line 2 in day3
    /// </summary>
    public GameObject neighbour5packageL2;
    /// <summary>
    /// Dialogue for interaction with neighbour5/line 3 in day3
    /// </summary>
    public GameObject neighbour5packageL3;
    /// <summary>
    /// Dialogue for interaction with neighbour5/line 4 in day3
    /// </summary>
    public GameObject neighbour5packageL4;


    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 1
    /// </summary>
    public GameObject butcherDay4WakeL1;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 2
    /// </summary>
    public GameObject butcherDay4WakeL2;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 3
    /// </summary>
    public GameObject butcherDay4WakeL3;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 4
    /// </summary>
    public GameObject butcherDay4WakeL4;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 5
    /// </summary>
    public GameObject butcherDay4WakeL5;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 6
    /// </summary>
    public GameObject butcherDay4WakeL6;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 7
    /// </summary>
    public GameObject butcherDay4WakeL7;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 8
    /// </summary>
    public GameObject butcherDay4WakeL8;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day4/line 9
    /// </summary>
    public GameObject butcherDay4WakeL9;


    /// <summary>
    /// Dialogue for interaction with neighbour1/line 1 in day4
    /// </summary>
    public GameObject neighbour1day4L1;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 2 in day4
    /// </summary>
    public GameObject neighbour1day4L2;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 3 in day4
    /// </summary>
    public GameObject neighbour1day4L3;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 4 in day4
    /// </summary>
    public GameObject neighbour1day4L4;

    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 in day4
    /// </summary>
    public GameObject neighbour2day4L1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 in day4
    /// </summary>
    public GameObject neighbour2day4L2;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 3 in day4
    /// </summary>
    public GameObject neighbour2day4L3;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 4 in day4
    /// </summary>
    public GameObject neighbour2day4L4;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 5 in day4
    /// </summary>
    public GameObject neighbour2day4L5;

    /// <summary>
    /// Dialogue for interaction with neighbour3/line 1 in day4
    /// </summary>
    public GameObject neighbour3day4L1;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 2 in day4
    /// </summary>
    public GameObject neighbour3day4L2;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 3 in day4
    /// </summary>
    public GameObject neighbour3day4L3;
    /// <summary>
    /// Dialogue for interaction with neighbour3/line 4 in day4
    /// </summary>
    public GameObject neighbour3day4L4;

    /// <summary>
    /// Dialogue for interaction with neighbour4/line 1 in day4
    /// </summary>
    public GameObject neighbour4day4L1;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 2 in day4
    /// </summary>
    public GameObject neighbour4day4L2;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 3 in day4
    /// </summary>
    public GameObject neighbour4day4L3;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 4 in day4
    /// </summary>
    public GameObject neighbour4day4L4;


    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 1
    /// </summary>
    public GameObject butcherDay5WakeL1;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 2
    /// </summary>
    public GameObject butcherDay5WakeL2;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 3
    /// </summary>
    public GameObject butcherDay5WakeL3;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 4
    /// </summary>
    public GameObject butcherDay5WakeL4;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 5
    /// </summary>
    public GameObject butcherDay5WakeL5;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 6
    /// </summary>
    public GameObject butcherDay5WakeL6;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 7
    /// </summary>
    public GameObject butcherDay5WakeL7;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 8
    /// </summary>
    public GameObject butcherDay5WakeL8;
    /// <summary>
    /// Dialogue for talking to the butcher at the start of day5/line 9
    /// </summary>
    public GameObject butcherDay5WakeL9;


    /// <summary>
    /// Dialogue for talking to the butcher to give him the drink on day5/line 1
    /// </summary>
    public GameObject butcherDay5MixedL1;
    /// <summary>
    /// Dialogue for talking to the butcher to give him the drink on day5/line 2
    /// </summary>
    public GameObject butcherDay5MixedL2;
    /// <summary>
    /// Dialogue for talking to the butcher to give him the drink on day5/line 3
    /// </summary>
    public GameObject butcherDay5MixedL3;
    /// <summary>
    /// Dialogue for talking to the butcher to give him the drink on day5/line 4
    /// </summary>
    public GameObject butcherDay5MixedL4;
    /// <summary>
    /// Dialogue for talking to the butcher to give him the drink on day5/line 5
    /// </summary>
    public GameObject butcherDay5MixedL5;


    /// <summary>
    /// Dialogue for interaction with neighbour1/line 1 in day5
    /// </summary>
    public GameObject neighbour1day5L1;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 2 in day5
    /// </summary>
    public GameObject neighbour1day5L2;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 3 in day5
    /// </summary>
    public GameObject neighbour1day5L3;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 4 in day5
    /// </summary>
    public GameObject neighbour1day5L4;
    /// <summary>
    /// Dialogue for interaction with neighbour1/line 5 in day5
    /// </summary>
    public GameObject neighbour1day5L5;

    /// <summary>
    /// Dialogue for interaction with neighbour2/line 1 in day5
    /// </summary>
    public GameObject neighbour2day5L1;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 2 in day5
    /// </summary>
    public GameObject neighbour2day5L2;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 3 in day5
    /// </summary>
    public GameObject neighbour2day5L3;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 4 in day5
    /// </summary>
    public GameObject neighbour2day5L4;
    /// <summary>
    /// Dialogue for interaction with neighbour2/line 5 in day5
    /// </summary>
    public GameObject neighbour2day5L5;

    /// <summary>
    /// Dialogue for interaction with neighbour4/line 1 in day5
    /// </summary>
    public GameObject neighbour4day5L1;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 2 in day5
    /// </summary>
    public GameObject neighbour4day5L2;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 3 in day5
    /// </summary>
    public GameObject neighbour4day5L3;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 4 in day5
    /// </summary>
    public GameObject neighbour4day5L4;
    /// <summary>
    /// Dialogue for interaction with neighbour4/line 5 in day5
    /// </summary>
    public GameObject neighbour4day5L5;


    /// <summary>
    /// For day 5 dinner loading screen
    /// </summary>
    public GameObject dinner5;
    /// <summary>
    /// For day 5 dinner alternate loading screen
    /// </summary>
    public GameObject ALTdinner5;

    /// <summary>
    /// For day 5 dinner/line 1
    /// </summary>
    public GameObject butcherDay5DoneL1;
    /// <summary>
    /// For day 5 dinner/line 2
    /// </summary>
    public GameObject butcherDay5DoneL2;
    /// <summary>
    /// For day 5 dinner/line 3
    /// </summary>
    public GameObject butcherDay5DoneL3;
    /// <summary>
    /// For day 5 dinner/line 4
    /// </summary>
    public GameObject butcherDay5DoneL4;
    /// <summary>
    /// For day 5 dinner/line 5
    /// </summary>
    public GameObject butcherDay5DoneL5;
    /// <summary>
    /// For day 5 dinner/line 6
    /// </summary>
    public GameObject butcherDay5DoneL6;
    /// <summary>
    /// For day 5 dinner/line 7
    /// </summary>
    public GameObject butcherDay5DoneL7;
    /// <summary>
    /// For day 5 dinner/line 8
    /// </summary>
    public GameObject butcherDay5DoneL8;

    /// <summary>
    /// Day5's red screen
    /// </summary>
    public GameObject redScreen;
    /// <summary>
    /// Day5's cutscene/part 1
    /// </summary>
    public GameObject cutsceneDay5P1;
    /// <summary>
    /// Day5's cutscene/part 2
    /// </summary>
    public GameObject cutsceneDay5P2;
    /// <summary>
    /// Day5's cutscene/part 3
    /// </summary>
    public GameObject cutsceneDay5P3;
    /// <summary>
    /// Day5's cutscene/part 4
    /// </summary>
    public GameObject cutsceneDay5P4;
    /// <summary>
    /// Day5's cutscene/part 5
    /// </summary>
    public GameObject cutsceneDay5P5;
    /// <summary>
    /// Day5's cutscene/part 6
    /// </summary>
    public GameObject cutsceneDay5P6;
    /// <summary>
    /// Day5's cutscene/part 7
    /// </summary>
    public GameObject cutsceneDay5P7;
    /// <summary>
    /// Day5's cutscene/part 8
    /// </summary>
    public GameObject cutsceneDay5P8;


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
        GameManager.gameManager.ResetGame();
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
                else if (convoNPC == "butcheraskfood" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to butcher on the first day in the butchery to ask for food
                {
                    butcheraskfoodL4.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 12)
                    {
                        butcheraskfoodL16.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                        GameManager.gameManager.introToButcher = true;
                    }
                    else if (dialogueClick == 12)
                    {
                        butcheraskfoodL15.SetActive(false);
                        butcheraskfoodL16.SetActive(true);
                    }
                    else if (dialogueClick == 11)
                    {
                        butcheraskfoodL14.SetActive(false);
                        butcheraskfoodL15.SetActive(true);
                    }
                    else if (dialogueClick == 10)
                    {
                        butcheraskfoodL13.SetActive(false);
                        butcheraskfoodL14.SetActive(true);
                    }
                    else if (dialogueClick == 9)
                    {
                        butcheraskfoodL12.SetActive(false);
                        butcheraskfoodL13.SetActive(true);
                    }
                    else if (dialogueClick == 8)
                    {
                        butcheraskfoodL11.SetActive(false);
                        butcheraskfoodL12.SetActive(true);
                    }
                    else if (dialogueClick == 7)
                    {
                        butcheraskfoodL10.SetActive(false);
                        butcheraskfoodL11.SetActive(true);
                    }
                    else if (dialogueClick == 6)
                    {
                        butcheraskfoodL9.SetActive(false);
                        butcheraskfoodL10.SetActive(true);
                    }
                    else if (dialogueClick == 5)
                    {
                        butcheraskfoodL8.SetActive(false);
                        butcheraskfoodL9.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        butcheraskfoodL7.SetActive(false);
                        butcheraskfoodL8.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcheraskfoodL6.SetActive(false);
                        butcheraskfoodL7.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcheraskfoodL5.SetActive(false);
                        butcheraskfoodL6.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcheraskfoodL5.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2askplay" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher) //If talking to neighbour2 on the first day after meeting the butcher
                {
                    neighbour2askplayL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour2askplayL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2askplayL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour3askplay" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher) //If talking to neighbour3 on the first day after meeting the butcher
                {
                    neighbour3askplayL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour3askplayL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour3");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour3askplayL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour4askplay" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher) //If talking to neighbour4 on the first day after meeting the butcher
                {
                    neighbour4askplayL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour4askplayL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour4");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour4askplayL2.SetActive(true);
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
                    GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To summon the butcher behind
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

                        loadingMenu.SetActive(true); //Show the loading screen and hide the butcher and smallchild1
                        loading1.SetActive(true);
                        GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To hide the butcher
                        GameObject.Find("NPC_smallchild1").GetComponent<NPC>().SpawnCheck(); //To hide smallchild1
                        Player.instance.timeDelayStart = true;
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
                else if (triggerText == "butcherRoomDoor") //If approaching the butcher's door in the butcher's house
                {
                    butcherRoomDoor.SetActive(false);
                    Player.instance.inDialogue = false;
                }
                else if (triggerText == "butcherGuestDoorDay1Normal") //If approaching the butcher's guest door in the butcher's house on day1 normally
                {
                    butcherGuestDoorDay1Normal.SetActive(false);
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
                else if (triggerText == "butcheryEntranceTrigger") //If approaching the entrance in the butchery after completing minigame1
                {
                    butcheryEntranceTriggerL1.SetActive(false);
                    Player.instance.inDialogue = false;
                    loadingMenu.SetActive(true); //Show the loading screen and show the butcher
                    loading1.SetActive(true);
                    dinner1L1.SetActive(true);
                    GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To show the butcher
                    Player.instance.timeDelayStart = true;
                }
                else if (dinnerWait && GameManager.gameManager.dayCount == 1 & GameManager.gameManager.dinner1 != true && triggerText != "cutsceneDay1") //If done waiting for the butcher to come back after minigame1
                {
                    dinner1L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        dinner1L5.SetActive(false);
                        dinner1.SetActive(true);
                        dinnerWait = false;
                        dialogueClick = 0;
                        Player.instance.timeDelayStart = true;
                        GameManager.gameManager.dinner1 = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        dinner1L4.SetActive(false);
                        dinner1L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        dinner1L3.SetActive(false);
                        dinner1L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        dinner1L2.SetActive(false);
                        dinner1L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        dinner1L2.SetActive(true);
                    }
                }
                else if (triggerText == "cutsceneDay1" && dinnerWait != true && GameManager.gameManager.dayCount == 1) //To trigger day1's cutscene
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = GameManager.gameManager.spawn2.transform.position;
                    overlay2.SetActive(true);
                    Player.instance.inDialogue = false;
                    Player.instance.timeDelayStart = true;
                }
                else if (dinnerWait && GameManager.gameManager.dayCount == 1 & GameManager.gameManager.dinner1) //To start day2
                {
                    cutsceneDay1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        day2StartL3.SetActive(false);
                        dinnerWait = false;
                        GameManager.gameManager.cutscene1 = true;
                        GameManager.gameManager.dayCount = 2;
                        GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To show the butcher
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 3)
                    {
                        day2StartL2.SetActive(false);
                        day2StartL3.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        day2StartL1.SetActive(false);
                        day2StartL2.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        day2StartL1.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday2wake") //If talking to the butcher when player first wakes up on day2 in the butchery
                {
                    butcherDay2WakeL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 5)
                    {
                        butcherDay2WakeL6.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 5)
                    {
                        butcherDay2WakeL5.SetActive(false);
                        butcherDay2WakeL6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        butcherDay2WakeL4.SetActive(false);
                        butcherDay2WakeL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay2WakeL3.SetActive(false);
                        butcherDay2WakeL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay2WakeL2.SetActive(false);
                        butcherDay2WakeL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay2WakeL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour1hello" && GameManager.gameManager.dayCount == 2) //If talking to neighbour1 on the second day 
                {
                    neighbour1helloL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour1helloL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour1");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour1helloL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2hello" && GameManager.gameManager.dayCount == 2) //If talking to neighbour2 on the second day 
                {
                    neighbour2helloL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 2)
                    {
                        neighbour2helloL3.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour2helloL2.SetActive(false);
                        neighbour2helloL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2helloL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour3hello" && GameManager.gameManager.dayCount == 2) //If talking to neighbour3 on the second day 
                {
                    neighbour3helloL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour3helloL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour3");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour3helloL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour4hello" && GameManager.gameManager.dayCount == 2) //If talking to neighbour4 on the second day 
                {
                    neighbour4helloL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        neighbour4helloL2.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour4");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour4helloL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2park" && GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 4) //If talking to neighbour4 on the second day in the park
                {
                    neighbour2parkL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 5)
                    {
                        neighbour2parkL6.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 5)
                    {
                        neighbour2parkL5.SetActive(false);
                        neighbour2parkL6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour2parkL4.SetActive(false);
                        neighbour2parkL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour2parkL3.SetActive(false);
                        neighbour2parkL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour2parkL2.SetActive(false);
                        neighbour2parkL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2parkL2.SetActive(true);
                    }
                }
                else if (convoNPC == "day2GoUpConvo" && GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame3) //If talking to the butcher after minigame 3
                {
                    day2GoUpTriggerL1.SetActive(false);
                    GameObject npc = GameObject.Find("NPC_butcher");
                    npc.GetComponent<NPC>().resumeNormal = true;
                }
                else if (triggerText == "day2CutsceneTrigger") //If heading to the top of butchery after completing minigame3
                {
                    day2CutsceneTriggerL1.SetActive(false);
                    Player.instance.inDialogue = false;
                    loadingMenu.SetActive(true); //Show the loading screen and show the butcher
                    loading1.SetActive(true);
                    dinner2L1.SetActive(true);
                    GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To show the butcher
                    Player.instance.timeDelayStart = true;
                }
                else if (dinnerWait && GameManager.gameManager.dayCount == 2 & GameManager.gameManager.dinner2 != true && triggerText != "cutsceneDay2") //If done waiting for the butcher to come back after minigame3
                {
                    dinner2L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        dinner2L4.SetActive(false);
                        dinner2.SetActive(true);
                        Minigame3.instance.round = 0;
                        dinnerWait = false;
                        dialogueClick = 0;
                        Player.instance.timeDelayStart = true;
                        GameManager.gameManager.dinner2 = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        dinner2L3.SetActive(false);
                        dinner2L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        dinner2L2.SetActive(false);
                        dinner2L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        dinner2L2.SetActive(true);
                    }
                }
                else if (triggerText == "cutsceneDay2" && dinnerWait != true && GameManager.gameManager.dayCount == 2) //To trigger day2's cutscene
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = GameManager.gameManager.spawn2.transform.position;
                    overlay3.SetActive(true);
                    GameObject npc = GameObject.Find("NPC_butcher");
                    npc.GetComponent<NPC>().resumeNormal = true;
                    GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To show the butcher
                    Player.instance.timeDelayStart = true;
                }
                else if (dinnerWait && GameManager.gameManager.dayCount == 2 & GameManager.gameManager.dinner2) //To start day3
                {
                    cutsceneDay2.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        day3StartL3.SetActive(false);
                        dinnerWait = false;
                        GameManager.gameManager.cutscene2 = true;
                        GameManager.gameManager.dayCount = 3;
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 3)
                    {
                        day3StartL2.SetActive(false);
                        day3StartL3.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        day3StartL1.SetActive(false);
                        day3StartL2.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        day3StartL1.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday3wake") //If talking to the butcher when player first wakes up on day3 in the butchery
                {
                    butcherDay3WakeL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 5)
                    {
                        butcherDay3WakeL6.SetActive(false);
                        if (GameManager.gameManager.addPackagesOnce != true)
                        {
                            GameManager.gameManager.ItemCollected(Player.instance.item1);
                            GameManager.gameManager.ItemCollected(Player.instance.item2);
                            GameManager.gameManager.ItemCollected(Player.instance.item3);
                            GameManager.gameManager.ItemCollected(Player.instance.item4);
                            GameManager.gameManager.ItemCollected(Player.instance.item5);
                            GameManager.gameManager.addPackagesOnce = true;
                        }
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 5)
                    {
                        butcherDay3WakeL5.SetActive(false);
                        butcherDay3WakeL6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        butcherDay3WakeL4.SetActive(false);
                        butcherDay3WakeL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay3WakeL3.SetActive(false);
                        butcherDay3WakeL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay3WakeL2.SetActive(false);
                        butcherDay3WakeL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay3WakeL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour1package" && GameManager.gameManager.dayCount == 3) //If talking to neighbour1 on the third day 
                {
                    neighbour1packageL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 2)
                    {
                        neighbour1packageL3.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour1");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour1packageL2.SetActive(false);
                        neighbour1packageL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour1packageL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2package" && GameManager.gameManager.dayCount == 3) //If talking to neighbour2 on the third day 
                {
                    neighbour2packageL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        neighbour2packageL4.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour2packageL3.SetActive(false);
                        neighbour2packageL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour2packageL2.SetActive(false);
                        neighbour2packageL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2packageL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour3package" && GameManager.gameManager.dayCount == 3) //If talking to neighbour3 on the third day 
                {
                    neighbour3packageL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        neighbour3packageL5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour3");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour3packageL4.SetActive(false);
                        neighbour3packageL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour3packageL3.SetActive(false);
                        neighbour3packageL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour3packageL2.SetActive(false);
                        neighbour3packageL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour3packageL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour4package" && GameManager.gameManager.dayCount == 3) //If talking to neighbour4 on the third day 
                {
                    neighbour4packageL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        neighbour4packageL5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour4");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour4packageL4.SetActive(false);
                        neighbour4packageL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour4packageL3.SetActive(false);
                        neighbour4packageL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour4packageL2.SetActive(false);
                        neighbour4packageL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour4packageL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour5package" && GameManager.gameManager.dayCount == 3) //If talking to neighbour5 on the third day 
                {
                    neighbour5packageL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        neighbour5packageL4.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour5");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour5packageL3.SetActive(false);
                        neighbour5packageL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour5packageL2.SetActive(false);
                        neighbour5packageL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour5packageL2.SetActive(true);
                    }
                }
                else if (triggerText == "policeWarningDay3") //If on the streets on day3 and there are police around
                {
                    policeWarningDay3L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        policeWarningDay3L2.SetActive(false);
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 1)
                    {
                        policeWarningDay3L2.SetActive(true);
                    }
                }
                else if (triggerText == "policeWarningDay4") //If on the streets on day4 and there are more police around
                {
                    policeWarningDay4L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 1)
                    {
                        policeWarningDay4L2.SetActive(false);
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 1)
                    {
                        policeWarningDay4L2.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday3done" && GameManager.gameManager.dayCount == 3 & GameManager.gameManager.dinner3 != true) //If talking to the butcher after delivering packages
                {
                    butcherDay3DoneL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        butcherDay3DoneL4.SetActive(false);
                        dinner3.SetActive(true);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                        dinnerWait = false;
                        dialogueClick = 0;
                        Player.instance.timeDelayStart = true;
                        GameManager.gameManager.dinner3 = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay3DoneL3.SetActive(false);
                        butcherDay3DoneL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay3DoneL2.SetActive(false);
                        butcherDay3DoneL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay3DoneL2.SetActive(true);
                    }
                }
                else if (triggerText == "cutsceneDay3" && dinnerWait != true && GameManager.gameManager.dayCount == 3) //To trigger day3's cutscene
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = GameManager.gameManager.spawn2.transform.position;
                    overlay4.SetActive(true);
                    GameObject npc = GameObject.Find("NPC_butcher");
                    npc.GetComponent<NPC>().resumeNormal = true;
                    Player.instance.timeDelayStart = true;
                }
                else if (dinnerWait && GameManager.gameManager.dayCount == 3 & GameManager.gameManager.dinner3) //To start day4
                {
                    cutsceneDay3.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        day4StartL3.SetActive(false);
                        dinnerWait = false;
                        GameManager.gameManager.cutscene3 = true;
                        GameManager.gameManager.dayCount = 4;
                        GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To show the butcher
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 3)
                    {
                        day4StartL2.SetActive(false);
                        day4StartL3.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        day4StartL1.SetActive(false);
                        day4StartL2.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        day4StartL1.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday4wake") //If talking to the butcher when player first wakes up on day4 in the butchery
                {
                    butcherDay4WakeL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 8)
                    {
                        butcherDay4WakeL9.SetActive(false);
                        if (GameManager.gameManager.addBottlesOnce != true)
                        {
                            GameManager.gameManager.ItemCollected(Player.instance.bottle1);
                            GameManager.gameManager.ItemCollected(Player.instance.bottle1);
                            GameManager.gameManager.ItemCollected(Player.instance.bottle2);
                            GameManager.gameManager.addBottlesOnce = true;
                        }
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 8)
                    {
                        butcherDay4WakeL8.SetActive(false);
                        butcherDay4WakeL9.SetActive(true);
                    }
                    else if (dialogueClick == 7)
                    {
                        butcherDay4WakeL7.SetActive(false);
                        butcherDay4WakeL8.SetActive(true);
                    }
                    else if (dialogueClick == 6)
                    {
                        butcherDay4WakeL6.SetActive(false);
                        butcherDay4WakeL7.SetActive(true);
                    }
                    else if (dialogueClick == 5)
                    {
                        butcherDay4WakeL5.SetActive(false);
                        butcherDay4WakeL6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        butcherDay4WakeL4.SetActive(false);
                        butcherDay4WakeL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay4WakeL3.SetActive(false);
                        butcherDay4WakeL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay4WakeL2.SetActive(false);
                        butcherDay4WakeL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay4WakeL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour1day4" && GameManager.gameManager.dayCount == 4) //If talking to neighbour1 on the fourth day 
                {
                    neighbour1day4L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        neighbour1day4L4.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour1");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour1day4L3.SetActive(false);
                        neighbour1day4L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour1day4L2.SetActive(false);
                        neighbour1day4L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour1day4L2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2day4" && GameManager.gameManager.dayCount == 4) //If talking to neighbour2 on the fourth day 
                {
                    neighbour2day4L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        neighbour2day4L5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour2day4L4.SetActive(false);
                        neighbour2day4L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour2day4L3.SetActive(false);
                        neighbour2day4L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour2day4L2.SetActive(false);
                        neighbour2day4L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2day4L2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour3day4" && GameManager.gameManager.dayCount == 4) //If talking to neighbour3 on the fourth day 
                {
                    neighbour3day4L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        neighbour3day4L4.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour3");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour3day4L3.SetActive(false);
                        neighbour3day4L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour3day4L2.SetActive(false);
                        neighbour3day4L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour3day4L2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour4day4" && GameManager.gameManager.dayCount == 4) //If talking to neighbour4 on the fourth day 
                {
                    neighbour4day4L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        neighbour4day4L4.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour4");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour4day4L3.SetActive(false);
                        neighbour4day4L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour4day4L2.SetActive(false);
                        neighbour4day4L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour4day4L2.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday4done" && GameManager.gameManager.dayCount == 4 & GameManager.gameManager.dinner4 != true) //If talking to the butcher after delivering bottless
                {
                    butcherDay4DoneL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        butcherDay4DoneL4.SetActive(false);
                        dinner4.SetActive(true);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                        dinnerWait = false;
                        dialogueClick = 0;
                        Player.instance.timeDelayStart = true;
                        GameManager.gameManager.dinner4 = true;
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay4DoneL3.SetActive(false);
                        butcherDay4DoneL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay4DoneL2.SetActive(false);
                        butcherDay4DoneL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay4DoneL2.SetActive(true);
                    }
                }
                else if (triggerText == "cutsceneDay4" && dinnerWait != true && GameManager.gameManager.dayCount == 4) //To trigger day4's cutscene
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = GameManager.gameManager.spawn2.transform.position;
                    overlay5.SetActive(true);
                    GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To show the butcher
                    GameObject npc = GameObject.Find("NPC_butcher");
                    npc.GetComponent<NPC>().resumeNormal = true;
                    Player.instance.timeDelayStart = true;
                }
                else if (dinnerWait && GameManager.gameManager.dayCount == 4 & GameManager.gameManager.dinner4) //To start day5
                {
                    cutsceneDay4.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        day5StartL4.SetActive(false);
                        dinnerWait = false;
                        GameManager.gameManager.cutscene4 = true;
                        GameManager.gameManager.dayCount = 5;
                        Player.instance.inDialogue = false;
                    }
                    else if (dialogueClick == 4)
                    {
                        day5StartL3.SetActive(false);
                        day5StartL4.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        day5StartL2.SetActive(false);
                        day5StartL3.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        day5StartL1.SetActive(false);
                        day5StartL2.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        day5StartL1.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday5wake") //If talking to the butcher when player first wakes up on day5 in the butchery
                {
                    butcherDay5WakeL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 8)
                    {
                        butcherDay5WakeL9.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 8)
                    {
                        butcherDay5WakeL8.SetActive(false);
                        butcherDay5WakeL9.SetActive(true);
                    }
                    else if (dialogueClick == 7)
                    {
                        butcherDay5WakeL7.SetActive(false);
                        butcherDay5WakeL8.SetActive(true);
                    }
                    else if (dialogueClick == 6)
                    {
                        butcherDay5WakeL6.SetActive(false);
                        butcherDay5WakeL7.SetActive(true);
                    }
                    else if (dialogueClick == 5)
                    {
                        butcherDay5WakeL5.SetActive(false);
                        butcherDay5WakeL6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        butcherDay5WakeL4.SetActive(false);
                        butcherDay5WakeL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay5WakeL3.SetActive(false);
                        butcherDay5WakeL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay5WakeL2.SetActive(false);
                        butcherDay5WakeL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay5WakeL2.SetActive(true);
                    }
                }
                else if (convoNPC == "butcherday5mixed") //If talking to the butcher when player mixes the drink on day5 in the butchery
                {
                    butcherDay5MixedL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        butcherDay5MixedL5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        butcherDay5MixedL4.SetActive(false);
                        butcherDay5MixedL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay5MixedL3.SetActive(false);
                        butcherDay5MixedL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay5MixedL2.SetActive(false);
                        butcherDay5MixedL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay5MixedL2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour1day5" && GameManager.gameManager.dayCount == 5) //If talking to neighbour1 on the fifth day 
                {
                    neighbour1day5L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        neighbour1day5L5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour1");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour1day5L4.SetActive(false);
                        neighbour1day5L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour1day5L3.SetActive(false);
                        neighbour1day5L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour1day5L2.SetActive(false);
                        neighbour1day5L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour1day5L2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour2day5" && GameManager.gameManager.dayCount == 5) //If talking to neighbour2 on the fifth day 
                {
                    neighbour2day5L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        neighbour2day5L5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour2");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour2day5L4.SetActive(false);
                        neighbour2day5L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour2day5L3.SetActive(false);
                        neighbour2day5L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour2day5L2.SetActive(false);
                        neighbour2day5L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour2day5L2.SetActive(true);
                    }
                }
                else if (convoNPC == "neighbour4day5" && GameManager.gameManager.dayCount == 5) //If talking to neighbour4 on the fifth day 
                {
                    neighbour4day5L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 4)
                    {
                        neighbour4day5L5.SetActive(false);
                        GameObject npc = GameObject.Find("NPC_neighbour4");
                        npc.GetComponent<NPC>().resumeNormal = true;
                    }
                    else if (dialogueClick == 4)
                    {
                        neighbour4day5L4.SetActive(false);
                        neighbour4day5L5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        neighbour4day5L3.SetActive(false);
                        neighbour4day5L4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        neighbour4day5L2.SetActive(false);
                        neighbour4day5L3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        neighbour4day5L2.SetActive(true);
                    }
                }



                else if (convoNPC == "butcherday5done" && GameManager.gameManager.dayCount == 5 & GameManager.gameManager.finalCutscene != true) //If talking to the butcher when player returns to the butchery on day5 
                {
                    butcherDay5DoneL1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 7)
                    {
                        butcherDay5DoneL8.SetActive(false);
                        cutsceneDay5P1.SetActive(true);
                        GameObject npc = GameObject.Find("NPC_butcher");
                        npc.GetComponent<NPC>().resumeNormal = true;
                        dinnerWait = false;
                        dialogueClick = 0;
                        Player.instance.timeDelayStart = true;
                        GameManager.gameManager.finalCutscene = true;
                    }
                    else if (dialogueClick == 7)
                    {
                        butcherDay5DoneL7.SetActive(false);
                        butcherDay5DoneL8.SetActive(true);
                    }
                    else if (dialogueClick == 6)
                    {
                        butcherDay5DoneL6.SetActive(false);
                        butcherDay5DoneL7.SetActive(true);
                    }
                    else if (dialogueClick == 5)
                    {
                        butcherDay5DoneL5.SetActive(false);
                        butcherDay5DoneL6.SetActive(true);
                    }
                    else if (dialogueClick == 4)
                    {
                        butcherDay5DoneL4.SetActive(false);
                        butcherDay5DoneL5.SetActive(true);
                    }
                    else if (dialogueClick == 3)
                    {
                        butcherDay5DoneL3.SetActive(false);
                        butcherDay5DoneL4.SetActive(true);
                    }
                    else if (dialogueClick == 2)
                    {
                        butcherDay5DoneL2.SetActive(false);
                        butcherDay5DoneL3.SetActive(true);
                    }
                    else if (dialogueClick == 1)
                    {
                        butcherDay5DoneL2.SetActive(true);
                    }
                }
            }
        }

        if (Player.instance.inDialogue != true) //If player is no longer talking, clear variables
        {
            dialogueBox.SetActive(false);
            playerBars.SetActive(true);
            convoNPC = "";
            triggerText = "";
            Player.instance.idNPC = "";
            Player.instance.idTrigger = "";

            if (dialogueClick != 0) //To check if variables have not been cleared before
            {
                dialogueClick = 0;
            }
        }
    }
}
