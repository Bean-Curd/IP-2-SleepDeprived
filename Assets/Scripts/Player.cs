/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 28/07/2023
 * Description: IP2 - Player
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// New vector with (0,0,0), for WASD movement
    /// </summary>
    Vector3 moveData = Vector3.zero;
    /// <summary>
    /// Movement speed
    /// </summary>
    public float moveSpeed = 3.0f;
    /// <summary>
    /// New vextor for mouse/camera movement
    /// </summary>
    Vector3 rotationInput = Vector3.zero;
    /// <summary>
    /// Rotation speed
    /// </summary>
    public float rotationSpeed = 0.25f;

    /// <summary>
    /// Is player trying to jump
    /// </summary>
    bool jump;
    /// <summary>
    /// Check if player is on the floor -> Prevents double jumping
    /// </summary>
    bool onFloor;
    /// <summary>
    /// Is player trying to sprint
    /// </summary>
    public bool sprint;
    /// <summary>
    /// Is player trying to interact with something
    /// </summary>
    bool interact;
    /// <summary>
    /// Choose the camera for the head so it can rotate the camera instead of the whole body
    /// </summary>
    public Transform head;
    /// <summary>
    /// Raycast ray distance
    /// </summary>
    public float InteractionDistance = 1.0f;
    /// <summary>
    /// Is there a interactible in range
    /// </summary>
    public bool interactInRange;
    /// <summary>
    /// Object player is looking at
    /// </summary>
    public GameObject selectedObject;
    /// <summary>
    /// To set as main camera
    /// </summary>
    Camera cam;

    /// <summary>
    /// Is the player talking
    /// </summary>
    public bool inDialogue;
    /// <summary>
    /// To store the name for the NPC the player is interacting with
    /// </summary>
    public string idNPC;
    /// <summary>
    /// To store the name of the trigger the player is interacting with
    /// </summary>
    public string idTrigger;
    /// <summary>
    /// Is the player caught
    /// </summary>
    public bool isCaught;

    /// <summary>
    /// Has minigame1 been completed yet
    /// </summary>
    public bool doneMinigame1;
    /// <summary>
    /// Did the player win minigame2
    /// </summary>
    public bool playerWin;
    /// <summary>
    /// Has minigame2 been completed yet
    /// </summary>
    public bool doneMinigame2;
    /// <summary>
    /// To call the follow state immediaately for smallchild2
    /// </summary>
    public bool child2Follow;
    /// <summary>
    /// To add package1
    /// </summary>
    public CollectibleItems item1;
    /// <summary>
    /// To add package2
    /// </summary>
    public CollectibleItems item2;
    /// <summary>
    /// To add package3
    /// </summary>
    public CollectibleItems item3;
    /// <summary>
    /// To add package4
    /// </summary>4
    public CollectibleItems item4;
    /// <summary>
    /// To add package5
    /// </summary>
    public CollectibleItems item5;
    /// <summary>
    /// Is the player done delievring all the packages
    /// </summary>
    public bool deliverDone;
    /// <summary>
    /// Number of the police 
    /// </summary>
    public int policeIDSelected;
    /// <summary>
    /// To play the police warning once
    /// </summary>
    public bool policeWarningOnce;
    /// <summary>
    /// To add normal bottle 
    /// </summary>
    public CollectibleItems bottle1;
    /// <summary>
    /// To add tampered bottle
    /// </summary>
    public CollectibleItems bottle2;
    /// <summary>
    /// Is the player done delievring all the bottles
    /// </summary>
    public bool bottlesDone;
    /// <summary>
    /// To add normal pill bottle 
    /// </summary>
    public CollectibleItems pillBottleNormal;
    /// <summary>
    /// To add empty pill bottle
    /// </summary>
    public CollectibleItems pillBottleEmpty;
    /// <summary>
    /// To add glass of water
    /// </summary>
    public CollectibleItems glass;
    /// <summary>
    /// To add tampered glass of water
    /// </summary>
    public CollectibleItems glassMixed;
    /// <summary>
    /// Is the player mix the pills and the water
    /// </summary>
    public bool mixtureDone;

    /// <summary>
    /// To start card check
    /// </summary>
    public bool timeDelayStart;
    /// <summary>
    /// Duration of delay for code
    /// </summary>
    private float currentDuration;
    /// <summary>
    /// To end time delay
    /// </summary>
    public bool delayDone;

    /// <summary>
    /// Set the player as an instance
    /// </summary>
    public static Player instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //To lock the mouse to the center of the screen
    }

    /// <summary>
    /// Triggers when WASD pressed -> Retrieving the input
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        moveData = value.Get<Vector2>();
    }

    /// <summary>
    /// For mouse/camera movement
    /// </summary>
    /// <param name="value"></param>
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x; //For left right movement
        rotationInput.x = -value.Get<Vector2>().y; //For up down movement
    }

    /// <summary>
    /// So player can jump
    /// </summary>
    void OnSpaceKey()
    {
        if (onFloor && isCaught != true) //If player is on the floor, can jump 
        {
            jump = true;
        }

        if (isCaught)
        {
            GameObject police = GameObject.Find("Police"+policeIDSelected);
            police.transform.GetChild(0).GetComponent<Police>().escapeCount += 1;
            Debug.Log(police.transform.GetChild(0).GetComponent<Police>().escapeCount);
        }
    }
    
    void OnPKey() //TO TEST STUFF
    {

    }

    /// <summary>
    /// To pause the game
    /// </summary>
    void OnPause() //When the escape key is pressed, stop time and bring up pause screen
    {
        if (GameManager.gameManager.pause != true) //If not paused, pause 
        {
            Cursor.lockState = CursorLockMode.None; //To unlock the mouse
            GameManager.gameManager.pause = true;
        }
    }

    /// <summary>
    /// To access the inventory
    /// </summary>
    void OnBKey() //When the b key is pressed, stop time and bring up inventory menu
    {
        if (GameManager.gameManager.inventory) //If currently in inventory, exit
        {
            Cursor.lockState = CursorLockMode.Locked; //To lock the mouse
            GameManager.gameManager.inventory = false;
            GameManager.gameManager.pause = false;
        }
        else if (GameManager.gameManager.inventory != true) //If not in inventory, show inventory menu 
        {
            Cursor.lockState = CursorLockMode.None; //To unlock the mouse
            GameManager.gameManager.ListItems();
            GameManager.gameManager.inventory = true;
            GameManager.gameManager.pause = true;
        }
    }

    /// <summary>
    /// For sprinting
    /// </summary>
    /// <param name="value"></param>
    void OnShiftKey(InputValue value)
    {
        float input = value.Get<float>();

        if (input == 0f) //If left shift is lifted, cannot sprint
        {
            sprint = false;
        }
        else //If left shift is pressed, can sprint
        {
            sprint = true;
        }
    }

    /// <summary>
    /// For interactions with objects
    /// </summary>
    /// <param name="value"></param>
    void OnFKey(InputValue value)
    {
        float input = value.Get<float>();

        if (input == 0f) //If 'F' is lifted, cannot interact 
        {
            interact = false;
        }
        else //If 'F' is pressed, can interact 
        {
            interact = true;
        }
    }
    
    /// <summary>
    /// Trigger when interactible object in range
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.GetComponent<Interactible>() != null) //If an object has the interactible script
        {
            interactInRange = true;
            selectedObject = collision.gameObject; //The object player is near is the one the player can interact with
        }
    }

    /// <summary>
    /// Trigger when leaving an object
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit(Collider collision)
    {
        interactInRange = false;

        if (collision.gameObject == selectedObject) //If the player exited a object it is interacting with, clear the object it is interacting with 
        {
            selectedObject = null;
        }
    }

    /// <summary>
    /// What happens when player enters an object
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            HealthBar.instance.Damage(10000);
        }

        if (collision.gameObject.name == "BoyHouseDoor") //If touching the boy's door, go out to street
        {
            GameManager.gameManager.leaveBoyHouse = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.gameObject.name == "BoyKitchenDoor")
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "boyKitchenDoor";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.boyKitchenDoor.SetActive(true);
        }
        else if (collision.gameObject.name == "BoyMotherDoor")
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "boyMotherDoor";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.boyMotherDoor.SetActive(true);
        }
        else if (collision.gameObject.name == "BoyRoomDoor")
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "boyRoomDoor";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.boyRoomDoor.SetActive(true);
        }
        else if (collision.gameObject.name == "StreetButcheryDoor") //If touching the butchery door from the street, leave to butchery (scene 2 to 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.gameObject.name == "ButcheryDoor" && Minigame3.instance.round == 0) //If touching the butchery door, leave to street (scene 3 to 2)
        {
            if (GameManager.gameManager.canComeBack)
            {
                GameManager.gameManager.comeBack = true;
                GameManager.gameManager.canComeBack = false;
            }

            if (GameManager.gameManager.endMinigame3 && GameManager.gameManager.dayCount == 2)
            {
                
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        else if (collision.gameObject.name == "ButcheryDoor" && Minigame3.instance.round > 0) //If touching the butchery door during minigame3, say should not leave
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "butcheryDoorMinigame3";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.butcheryDoorMinigame3.SetActive(true);
        }
        else if (collision.gameObject.name == "FreezerDoor" && Minigame3.instance.numCaught != 2)
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "freezerDoorNormal";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.freezerDoorNormal.SetActive(true);
        }
        else if (collision.gameObject.name == "FreezerDoor" && Minigame3.instance.numCaught == 2)
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "freezerDoorMinigame3";
            GameObject.Find("NPC_butcher").GetComponent<NPC>().SpawnCheck(); //To summon the butcher behind
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.freezerDoorMinigame3L1.SetActive(true);
        }
        else if (collision.gameObject.name == "ButcherRoomDoor")
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "butcherRoomDoor";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.butcherRoomDoor.SetActive(true);
        }
        else if (collision.gameObject.name == "ButcherGuestDoor" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher)
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "butcherGuestDoorDay1Normal";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.butcherGuestDoorDay1Normal.SetActive(true);
        }
        else if (collision.gameObject.name == "ButcheryEntranceTrigger" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.endMinigame1)
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "butcheryEntranceTrigger";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.butcheryEntranceTriggerL1.SetActive(true);
        }
        else if (collision.gameObject.name == "EnterParkTrigger" && GameManager.gameManager.dayCount != 1) //Send player to the park from the street
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        else if (collision.gameObject.name == "ExitParkTrigger" && GameManager.gameManager.dayCount != 1) //Send player to the street from the park
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
        else if (collision.gameObject.name == "Day2CutsceneTrigger" && GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame3) //To end day2
        {
            inDialogue = true;

            PlayerCanvas.instance.triggerText = "day2CutsceneTrigger";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.day2CutsceneTriggerL1.SetActive(true);
        }
        else if (collision.gameObject.name == "PoliceWarningTrigger" && GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2 && policeWarningOnce != true) //If there are police in the street on day3
        {
            inDialogue = true;
            policeWarningOnce = true;

            PlayerCanvas.instance.triggerText = "policeWarningDay3";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.policeWarningDay3L1.SetActive(true);
        }
        else if (collision.gameObject.name == "PoliceWarningTrigger" && GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2 && policeWarningOnce != true) //If there are police in the street on day4
        {
            inDialogue = true;
            policeWarningOnce = true;

            PlayerCanvas.instance.triggerText = "policeWarningDay4";
            PlayerCanvas.instance.playerBars.SetActive(false);
            PlayerCanvas.instance.dialogueBox.SetActive(true);
            PlayerCanvas.instance.policeWarningDay4L1.SetActive(true);
        }
    }

    /// <summary>
    /// What happens when player is on an object
    /// </summary>
    private void OnCollisionStay()
    {
        onFloor = true; //Confirm the player is on the floor 
    }

    /// <summary>
    /// What happens when you exit an object
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        onFloor = false; //Once player is off an object they are no longer touching floor
    }

    // Update is called once per frame
    void Update()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (jump && onFloor) //If player is trying to jump and is on the floor
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            onFloor = false; //Prevents player from jumping immediately after
            jump = false; //Resets jump
        }

        if (sprint && PlayerCanvas.instance.playerBars.activeSelf) //If sprinting and bars are present, decrease energy bar
        {
            EnergyBar.instance.Sprint(20);
        }
        else if (sprint != true) //If no longer sprinting, reset movement speed
        {
            moveSpeed = 3.0f; //Reset movement speed
        }

        if (interact) //If pressing F on an interactible object -> perform an action
        {
            /// <summary>
            /// Ray sent on left click
            /// </summary>
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractionDistance))
            {
                if (hitInfo.collider.gameObject.GetComponent<Interactible>() != null) //If an object has the interactible script
                {
                    if (hitInfo.collider.gameObject.GetComponent<Collectible>() != null) //If an object has the collectible script
                    {
                        //Debug.Log(hitInfo.collider.gameObject.tag);
                        
                        var collectScript = hitInfo.collider.gameObject.GetComponent<Collectible>();
                        var item = collectScript.item;
                        Debug.Log(item);

                        int meatNum = 0;

                        foreach (var itemInInventory in GameManager.gameManager.Items)
                        {
                            if (itemInInventory.id == 1)
                            {
                                meatNum += 1;
                            }
                        }

                        if (item.id == 1 && meatNum == 3)
                        {
                            inDialogue = true;

                            PlayerCanvas.instance.triggerText = "meatLimit";
                            PlayerCanvas.instance.playerBars.SetActive(false);
                            PlayerCanvas.instance.dialogueBox.SetActive(true);
                            PlayerCanvas.instance.meatLimit.SetActive(true);
                        }
                        else
                        {
                            if (item.id == 9)
                            {
                                if (GameManager.gameManager.dayCount == 5) //So it only works on day5
                                {
                                    GameManager.gameManager.pillBottle = true;
                                    GameManager.gameManager.ItemCollected(item);
                                    PlayerCanvas.instance.itemCollected.SetActive(true);
                                    Destroy(hitInfo.collider.gameObject);
                                    interact = false;
                                    timeDelayStart = true;
                                }
                            }
                            else if (item.id == 11)
                            {
                                if (GameManager.gameManager.dayCount == 5) //So it only works on day5
                                {
                                    GameManager.gameManager.glass = true;
                                    GameManager.gameManager.ItemCollected(item);
                                    PlayerCanvas.instance.itemCollected.SetActive(true);
                                    Destroy(hitInfo.collider.gameObject);
                                    interact = false;
                                    timeDelayStart = true;
                                }
                            }
                            else
                            {
                                GameManager.gameManager.ItemCollected(item);
                                PlayerCanvas.instance.itemCollected.SetActive(true);

                                if (item.id != 1)
                                {
                                    Destroy(hitInfo.collider.gameObject);
                                }

                                interact = false;
                                timeDelayStart = true;
                            }
                        }

                    }
                    else if (hitInfo.collider.gameObject.GetComponent<Dog>() != null) //If object is a dog
                    {
                        if (hitInfo.collider.gameObject.GetComponent<Dog>().isFed != true && (GameManager.gameManager.Items.Find(x => x.id == 1)) != null) //If dog has not been fed and player has meat in inventory (lambda -> returns a value)
                        {
                            foreach (var item in GameManager.gameManager.Items)
                            {
                                if (item.id == 1)
                                {
                                    hitInfo.collider.gameObject.GetComponent<Dog>().feeding = true;
                                    GameManager.gameManager.ItemUsed(item);
                                    break;
                                }
                            }
                        }
                        else if (hitInfo.collider.gameObject.GetComponent<Dog>().isFed != true && (GameManager.gameManager.Items.Find(x => x.id == 1)) == null)
                        {
                            Debug.Log("Meatless?");
                        }
                    }
                    else if (hitInfo.collider.gameObject.GetComponent<NPC>() != null && hitInfo.collider.gameObject.GetComponent<NPC>().currentState != "Follow") //If object is a NPC and is not following
                    {
                        if (inDialogue != true) //So cannot trigger during dialogue
                        {
                            inDialogue = true;

                            if (hitInfo.collider.gameObject.tag == "NPC")
                            {
                                idNPC = hitInfo.collider.gameObject.GetComponent<NPC>().idNPC;

                                if (idNPC == "neighbour1" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true) //If talking to neighbour1 on the first day before meeting the butcher
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour1askfood";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour1askfoodL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour1" && GameManager.gameManager.dayCount == 2) //If talking to neighbour1 on the second day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour1hello";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour1helloL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour1" && GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour1 on the third day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 2)
                                        {
                                            GameManager.gameManager.ItemUsed(item1);
                                            GameManager.gameManager.neighbour1 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour1package";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour1packageL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour1" && GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour1 on the fourth day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 7)
                                        {
                                            GameManager.gameManager.ItemUsed(bottle1);
                                            GameManager.gameManager.drink1 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour1day4";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour1day4L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour1" && GameManager.gameManager.dayCount == 5 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour1 on the fifth day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour1day5";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour1day5L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true) //If talking to neighbour2 on the first day before meeting the butcher
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour2askfood";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2askfoodL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher) //If talking to neighbour2 on the first day after meeting the butcher
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour2askplay";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2askplayL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour2 on the second day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour2hello";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2helloL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 2 && SceneManager.GetActiveScene().buildIndex == 4) //If talking to neighbour2 on the second day in the park
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour2park";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2parkL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour2 on the third day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 3)
                                        {
                                            GameManager.gameManager.ItemUsed(item2);
                                            GameManager.gameManager.neighbour2 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour2package";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2packageL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour2 on the fourth day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour2day4";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2day4L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour2" && GameManager.gameManager.dayCount == 5 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour2 on the fifth day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour2day5";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour2day5L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour3" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher) //If talking to neighbour3 on the first day after meeting the butcher
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour3askplay";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour3askplayL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour3" && GameManager.gameManager.dayCount == 2) //If talking to neighbour3 on the second day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour3hello";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour3helloL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour3" && GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour3 on the third day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 4)
                                        {
                                            GameManager.gameManager.ItemUsed(item3);
                                            GameManager.gameManager.neighbour3 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour3package";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour3packageL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour3" && GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour3 on the fourth day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 8)
                                        {
                                            GameManager.gameManager.ItemUsed(bottle2);
                                            GameManager.gameManager.drink3 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour3day4";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour3day4L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour4" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher) //If talking to neighbour4 on the first day after meeting the butcher
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour4askplay";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour4askplayL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour4" && GameManager.gameManager.dayCount == 2) //If talking to neighbour4 on the second day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour4hello";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour4helloL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour4" && GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour4 on the third day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 5)
                                        {
                                            GameManager.gameManager.ItemUsed(item4);
                                            GameManager.gameManager.neighbour4 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour4package";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour4packageL1.SetActive(true);
                                }
                                else if (idNPC == "neighbour4" && GameManager.gameManager.dayCount == 4 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour4 on the fourth day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 7)
                                        {
                                            GameManager.gameManager.ItemUsed(bottle1);
                                            GameManager.gameManager.drink2 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour4day4";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour4day4L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour4" && GameManager.gameManager.dayCount == 5 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour4 on the fifth day 
                                {
                                    PlayerCanvas.instance.convoNPC = "neighbour4day5";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour4day5L1.SetActive(true);
                                }
                                else if (idNPC == "neighbour5" && GameManager.gameManager.dayCount == 3 && SceneManager.GetActiveScene().buildIndex == 2) //If talking to neighbour5 on the third day 
                                {
                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 6)
                                        {
                                            GameManager.gameManager.ItemUsed(item5);
                                            GameManager.gameManager.neighbour5 = true;
                                            break;
                                        }
                                    }
                                    PlayerCanvas.instance.convoNPC = "neighbour5package";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.neighbour5packageL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 2) //If talking to the butcher on the first day to ask for food on the street
                                {
                                    PlayerCanvas.instance.convoNPC = "butcheraskfood";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcheraskfoodL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.introToButcher != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher on the first day to ask for food inside the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "butcheraskfood";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcheraskfoodL4.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 2 && GameManager.gameManager.meetSmallChild2 != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player first wakes up on day2 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday2wake";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay2WakeL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 2 && GameManager.gameManager.endMinigame3) //If talking to the butcher after he asks you to go up
                                {
                                    PlayerCanvas.instance.convoNPC = "day2GoUpConvo";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.day2GoUpTriggerL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 3 && deliverDone != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player first wakes up on day3 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday3wake";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay3WakeL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 3 && deliverDone && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player is done delivering packages
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday3done";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay3DoneL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 4 && bottlesDone != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player first wakes up on day4 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday4wake";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay4WakeL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 4 && bottlesDone && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player is done delivering bottles
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday4done";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay4DoneL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 5 && mixtureDone != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player first wakes up on day5 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday5wake";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay5WakeL1.SetActive(true);
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 5 && mixtureDone && GameManager.gameManager.comeBack != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player is done mixing the glass on day5 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday5mixed";
                                    GameManager.gameManager.canComeBack = true;
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay5MixedL1.SetActive(true);

                                    foreach (var item in GameManager.gameManager.Items) //Generates the item from the list
                                    {
                                        if (item.id == 12)
                                        {
                                            GameManager.gameManager.ItemUsed(glassMixed);
                                            break;
                                        }
                                    }
                                }
                                else if (idNPC == "butcher" && GameManager.gameManager.dayCount == 5 && GameManager.gameManager.comeBack && SceneManager.GetActiveScene().buildIndex == 3) //If talking to the butcher when player returns to the butchery on day5 
                                {
                                    PlayerCanvas.instance.convoNPC = "butcherday5done";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.butcherDay5DoneL1.SetActive(true);
                                }
                                else if (idNPC == "smallchild1" && GameManager.gameManager.dayCount == 1 && GameManager.gameManager.meetSmallChild1 != true && SceneManager.GetActiveScene().buildIndex != 3) //If talking to smallchild1 outside the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "smallchild1meet";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.smallchild1T1L1.SetActive(true);
                                }
                                else if (idNPC == "smallchild1" && GameManager.gameManager.dayCount == 1 && doneMinigame1 != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to smallchild1 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "startminigame1";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.startminigame1.SetActive(true);
                                }
                                else if (idNPC == "smallchild2" && doneMinigame2 != true && SceneManager.GetActiveScene().buildIndex == 4) //If talking to smallchild2 before minigame2 is done
                                {
                                    GameManager.gameManager.meetSmallChild2 = true;
                                    PlayerCanvas.instance.convoNPC = "startminigame2";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.startminigame2L1.SetActive(true);
                                }
                                else if (idNPC == "smallchild2" && Minigame3.instance.isCaught != true && SceneManager.GetActiveScene().buildIndex == 3) //If talking to smallchild2 in the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "startminigame3";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.startminigame3L1.SetActive(true);
                                }
                                else if (idNPC == "smallchild2" && Minigame3.instance.isCaught && SceneManager.GetActiveScene().buildIndex == 3 && Minigame3.instance.numCaught == 0) //When smallchild2 is caught for the first time
                                {
                                    PlayerCanvas.instance.convoNPC = "minigame3Caught1";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.minigame3Caught1L1.SetActive(true);
                                }
                                else if (idNPC == "smallchild2" && Minigame3.instance.isCaught && SceneManager.GetActiveScene().buildIndex == 3 && Minigame3.instance.numCaught == 1) //When smallchild2 is caught for the second time
                                {
                                    PlayerCanvas.instance.convoNPC = "minigame3Caught2";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.minigame3Caught2L1.SetActive(true);
                                }
                                else
                                {
                                    GameObject npc = hitInfo.collider.gameObject;
                                    npc.GetComponent<NPC>().resumeNormal = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (timeDelayStart)
        {
            if (currentDuration < 1f)
            {
                currentDuration += Time.deltaTime;

                if (PlayerCanvas.instance.dinner3.activeSelf) //To make it glitch a little
                {
                    if (currentDuration > 0.5f && currentDuration < 0.6f)
                    {
                        PlayerCanvas.instance.ALTdinner3.SetActive(true);
                    }
                    else
                    {
                        PlayerCanvas.instance.ALTdinner3.SetActive(false);
                    }
                }
                else if (PlayerCanvas.instance.dinner5.activeSelf) //To make it glitch a little
                {
                    if (currentDuration > 0.5f && currentDuration < 0.6f)
                    {
                        PlayerCanvas.instance.ALTdinner5.SetActive(true);
                    }
                    else
                    {
                        PlayerCanvas.instance.ALTdinner5.SetActive(false);
                    }
                }
            }
            else if (currentDuration >= 1f)
            {
                currentDuration = 0;
                delayDone = true;
                timeDelayStart = false;
            }
        }

        if (delayDone) 
        {
            PlayerCanvas.instance.itemCollected.SetActive(false);
            PlayerCanvas.instance.loadingMenu.SetActive(false);
            PlayerCanvas.instance.loading1.SetActive(false);

            if (GameManager.gameManager.endMinigame1 && GameManager.gameManager.dayCount == 1 && PlayerCanvas.instance.dinner1L1.activeSelf) //After day1 dinner
            {
                PlayerCanvas.instance.dinnerWait = true;
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
            }
            else if (GameManager.gameManager.dinner1 && GameManager.gameManager.dayCount == 1 && PlayerCanvas.instance.dinner1.activeSelf) //After day1 cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.triggerText = "cutsceneDay1";
                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.dinner1.SetActive(false);
                PlayerCanvas.instance.cutsceneDay1.SetActive(true);
            }
            else if (GameManager.gameManager.dinner1 && GameManager.gameManager.dayCount == 1 && PlayerCanvas.instance.cutsceneDay1.activeSelf) //After day1 cutscene
            {
                PlayerCanvas.instance.dinnerWait = true;
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
            }

            else if (GameManager.gameManager.endMinigame3 && GameManager.gameManager.dayCount == 2 && PlayerCanvas.instance.dinner2L1.activeSelf) //After day2 dinner
            {
                PlayerCanvas.instance.dinnerWait = true;
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
            }
            else if (GameManager.gameManager.dinner2 && GameManager.gameManager.dayCount == 2 && PlayerCanvas.instance.dinner2.activeSelf) //After day2 cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.triggerText = "cutsceneDay2";
                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.dinner2.SetActive(false);
                PlayerCanvas.instance.cutsceneDay2.SetActive(true);
            }
            else if (GameManager.gameManager.dinner2 && GameManager.gameManager.dayCount == 2 && PlayerCanvas.instance.cutsceneDay2.activeSelf) //After day2 cutscene
            {
                PlayerCanvas.instance.dinnerWait = true;
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
            }

            else if (GameManager.gameManager.dinner3 && GameManager.gameManager.dayCount == 3 && PlayerCanvas.instance.dinner3.activeSelf) //After day3 cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.triggerText = "cutsceneDay3";
                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.dinner3.SetActive(false);
                PlayerCanvas.instance.cutsceneDay3.SetActive(true);
            }
            else if (GameManager.gameManager.dinner3 && GameManager.gameManager.dayCount == 3 && PlayerCanvas.instance.cutsceneDay3.activeSelf) //After day3 cutscene
            {
                PlayerCanvas.instance.dinnerWait = true;
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
            }

            else if (GameManager.gameManager.dinner4 && GameManager.gameManager.dayCount == 4 && PlayerCanvas.instance.dinner4.activeSelf) //After day4 cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.triggerText = "cutsceneDay4";
                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.dinner4.SetActive(false);
                PlayerCanvas.instance.cutsceneDay4.SetActive(true);
            }
            else if (GameManager.gameManager.dinner4 && GameManager.gameManager.dayCount == 4 && PlayerCanvas.instance.cutsceneDay4.activeSelf) //After day4 cutscene
            {
                PlayerCanvas.instance.dinnerWait = true;
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
            }




            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P1.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P1.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P2.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P2.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P2.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P3.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P3.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P3.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P4.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P4.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P4.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P5.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P5.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P5.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P6.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P6.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P6.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P7.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P7.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P7.SetActive(false);
                PlayerCanvas.instance.cutsceneDay5P8.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.cutsceneDay5P8.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.cutsceneDay5P8.SetActive(false);
                PlayerCanvas.instance.dinner5.SetActive(true);

                timeDelayStart = true;
            }
            else if (GameManager.gameManager.finalCutscene && GameManager.gameManager.dayCount == 5 && PlayerCanvas.instance.dinner5.activeSelf) //For the final cutscene
            {
                inDialogue = true;

                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.dinner5.SetActive(false);
                PlayerCanvas.instance.redScreen.SetActive(false);
                Debug.Log("Game end");

                GameManager.gameManager.pause = false;
                GameManager.gameManager.dead = false;
                GameManager.gameManager.ResetGame();
                GameManager.gameManager.LoadingScreen();
                Cursor.lockState = CursorLockMode.None; //To unlock the mouse
                SceneManager.LoadScene(0);
            }

            delayDone = false;
        }

        if (GameManager.gameManager.dayCount == 1) //Day1 filter
        {
            PlayerCanvas.instance.overlay1.SetActive(true);
        }
        else if (GameManager.gameManager.dayCount == 2) //Day2 filter
        {
            PlayerCanvas.instance.overlay2.SetActive(true);
        }
        else if (GameManager.gameManager.dayCount == 3) //Day3 filter
        {
            PlayerCanvas.instance.overlay3.SetActive(true);
        }
        else if (GameManager.gameManager.dayCount == 4) //Day4 filter
        {
            PlayerCanvas.instance.overlay4.SetActive(true);
        }
        else if (GameManager.gameManager.dayCount == 5) //Day5 filter
        {
            PlayerCanvas.instance.overlay5.SetActive(true);
        }

        /// <summary>
        /// Turning the input into movement -> forward/back movement
        /// </summary>
        Vector3 forwardMove = transform.forward; //Vector3(0,0,1)
        /// <summary>
        /// Turning the input into movement -> left/right movement
        /// </summary>
        Vector3 rightMove = transform.right; //Vector3(1,0,0)

        if (GameManager.gameManager.pause != true && GameManager.gameManager.dead != true && inDialogue != true && isCaught != true)
        {
            /// <summary>
            /// For the player to move
            /// </summary>
            GetComponent<Rigidbody>().MovePosition(transform.position + (forwardMove * moveData.y
            + rightMove * moveData.x) * moveSpeed * Time.deltaTime);

        }

        /// <summary>
        /// For the player to rotate
        /// </summary>
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationInput.y) * rotationSpeed);

        /// <summary>
        /// Setting limits so the camera can rotate like how a head would
        /// </summary>
        var rot = head.rotation.eulerAngles + new Vector3(rotationInput.x, 0) * rotationSpeed; //For camera to rotate 

        // Setting limits so the camera can rotate like how a head would
        while (rot.x > 180f)
        {
            rot.x -= 360f;
        }

        while (rot.x < -180f)
        {
            rot.x += 360f;
        }

        if (rot.x > 60f)
        {
            rot.x = 60f;
        }

        if (rot.x < -60f)
        {
            rot.x = -60f;
        }

        /// <summary>
        /// Applying the limits to the head rotation
        /// </summary>
        head.rotation = Quaternion.Euler(rot);
    }
}
