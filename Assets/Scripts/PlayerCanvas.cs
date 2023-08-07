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

    #region Dialogue Related Variables

    /// <summary>
    /// To store the conversation for the NPC the player is interacting with
    /// </summary>
    public string convoNPC;
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
                if (convoNPC == "smallchild1meet") //If meeting smallchild1 for the first time
                {
                    smallchild1T1L1.SetActive(false);
                    dialogueClick += 1;

                    if (dialogueClick > 3)
                    {
                        smallchild1T1L4.SetActive(false);
                        NPC.instance.canFollow = true;
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
            }
        }


        if (Player.instance.inDialogue != true) //If player is no longer talking, clear variables
        {
            dialogueBox.SetActive(false);
            playerBars.SetActive(true);

            if (dialogueClick != 0) //To check if variables have not been cleared before
            {
                Player.instance.idNPC = "";
                convoNPC = "";
                dialogueClick = 0;
            }
        }
    }
}
