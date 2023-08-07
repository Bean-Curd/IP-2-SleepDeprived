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
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

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
    /// Is the player caught
    /// </summary>
    public bool isCaught;

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
            Police.instance.escapeCount += 1;
            Debug.Log(Police.instance.escapeCount);
        }
    }

    /*void OnFire()
    {
        HealthBar.instance.Damage(500);
    }*/

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

        if (sprint) //If sprinting, decrease energy bar
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
                        GameManager.gameManager.ItemCollected(item);
                        Destroy(hitInfo.collider.gameObject);
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
                    else if (hitInfo.collider.gameObject.GetComponent<NPC>() != null) //If object is a NPC
                    {
                        if (inDialogue != true) //So cannot trigger during dialogue
                        {
                            inDialogue = true;

                            if (hitInfo.collider.gameObject.tag == "NPC")
                            {
                                //NPC.instance.isInteracting = true;
                                idNPC = hitInfo.collider.gameObject.GetComponent<NPC>().idNPC;

                                if (idNPC == "smallchild1" && SceneManager.GetActiveScene().buildIndex != 3) //If talking to the smallchild1 outside the butchery
                                {
                                    PlayerCanvas.instance.convoNPC = "smallchild1meet";
                                    PlayerCanvas.instance.playerBars.SetActive(false);
                                    PlayerCanvas.instance.dialogueBox.SetActive(true);
                                    PlayerCanvas.instance.smallchild1T1L1.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
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