/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 11/08/2023
 * Description: IP2 - Minigame 2 (Rock Paper Scissors)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Threading;
using UnityEngine.SceneManagement;

public class Minigame2 : MonoBehaviour
{
    /// <summary>
    /// Number of player wins
    /// </summary>
    public int numPlayerWins = 0;
    /// <summary>
    /// Number of child wins
    /// </summary>
    public int numChildWins = 0;
    /// <summary>
    /// The icon the player selected
    /// </summary>
    public string playerIcon;
    /// <summary>
    /// The icon smallchild2 selected
    /// </summary>
    public string childIcon;
    /// <summary>
    /// For smallchild2's icon
    /// </summary>
    private int num;
    /// <summary>
    /// For points: 1 -> player win, 2 -> tie, 3 -> player lose 
    /// </summary>
    private int score;

    /// <summary>
    /// To start point check
    /// </summary>
    private bool pointCheck;
    /// <summary>
    /// Is player's turn done
    /// </summary>
    private bool playerDone;
    /// <summary>
    /// To start card check
    /// </summary>
    private bool timeDelayStart;
    /// <summary>
    /// Duration of delay for code
    /// </summary>
    private float currentDuration;
    /// <summary>
    /// To end time delay
    /// </summary>
    private bool delayDone;

    /// <summary>
    /// To access each icon for minigame2
    /// </summary>
    public Transform IconsContent;
    /// <summary>
    /// To access the player's selection box
    /// </summary>
    public GameObject playerSelect;
    /// <summary>
    /// To access smallchild2 selection box
    /// </summary>
    public GameObject childSelect;
    /// <summary>
    /// Empty selection box
    /// </summary>
    public Sprite emptyScreen;
    /// <summary>
    /// Dialogue for player's turn
    /// </summary>
    public GameObject playerPick;
    /// <summary>
    /// Dialogue for player's win
    /// </summary>
    public GameObject playerPoint;
    /// <summary>
    /// Dialogue for smallchild1's win
    /// </summary>
    public GameObject childPoint;

    /// <summary>
    /// Set the minigame2 canvas as an instance
    /// </summary>
    public static Minigame2 instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pointCheck = false;
        playerDone = false;
        timeDelayStart = false;
        if (SceneManager.GetActiveScene().buildIndex == 4) //if in the park
        {
            playerPick.SetActive(true);
        }
    }

    /// <summary>
    /// To choose the icon when player clicks
    /// </summary>
    public void SelectIcon()
    {
        playerIcon = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name; 
        playerSelect.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite;

        Cursor.lockState = CursorLockMode.Locked; //To lock the mouse

        playerPick.SetActive(false);
        playerDone = true;
        timeDelayStart = true;
    }

    /// <summary>
    /// To show smallchild2's icon
    /// </summary>
    public void ChildIcon()
    {
        System.Random random1 = new System.Random(); //Get a random icon
        num = random1.Next(0, 3);

        if (num == 0)
        {
            childIcon = "Rock";
        }
        else if (num == 1)
        {
            childIcon = "Paper";
        }
        else if (num == 2)
        {
            childIcon = "Scissors";
        }

        childSelect.GetComponent<Image>().sprite = IconsContent.GetChild(num).GetChild(0).GetComponent<Image>().sprite;

        if (playerIcon == "Rock")
        {
            if (childIcon == "Rock")
            {
                //Tie
                score = 2;
            }
            else if (childIcon == "Paper")
            {
                //Lose
                score = 3;
            }
            else if (childIcon == "Scissors")
            {
                //Win
                score = 1;
            }
        }
        else if (playerIcon == "Paper")
        {
            if (childIcon == "Rock")
            {
                //Win
                score = 1;
            }
            else if (childIcon == "Paper")
            {
                //Tie
                score = 2;
            }
            else if (childIcon == "Scissors")
            {
                //Lose
                score = 3;
            }
        }
        else if (playerIcon == "Scissors")
        {
            if (childIcon == "Rock")
            {
                //Lose
                score = 3;
            }
            else if (childIcon == "Paper")
            {
                //Win
                score = 1;
            }
            else if (childIcon == "Scissors")
            {
                //Tie
                score = 2;
            }
        }

        if (score == 1) //If the player wins, set score and text
        {
            numPlayerWins += 1;

            playerPoint.SetActive(true);
        }
        else if (score == 3) //If the player loses, set score and text
        {
            numChildWins += 1;

            childPoint.SetActive(true);
        }

        pointCheck = true;
        timeDelayStart = true;
    }

    /// <summary>
    /// What happens when game is over
    /// </summary>
    public void Minigame2Check()
    {
        playerPoint.SetActive(false);
        childPoint.SetActive(false);
        playerPick.SetActive(true);

        playerSelect.GetComponent<Image>().sprite = emptyScreen;
        childSelect.GetComponent<Image>().sprite = emptyScreen;

        playerDone = false;

        if (numPlayerWins == 3 || numChildWins == 3) //If there are no more pairs of cards remaining, end game
        {
            playerDone = true;
            if (numPlayerWins == 3)
            {
                PlayerCanvas.instance.minigame2win.SetActive(true); //Win :D
                Player.instance.doneMinigame2 = true;
            }
            else if (numChildWins == 3)
            {
                PlayerCanvas.instance.minigame2lose.SetActive(true); //Redo game
                Player.instance.playerWin = true;

                numPlayerWins = 0;
                numChildWins = 0;
            }

            Cursor.lockState = CursorLockMode.Locked; //To lock the mouse
            PlayerCanvas.instance.minigame2.SetActive(false);
        }

        if (playerDone != true)
        {
            Cursor.lockState = CursorLockMode.None; //To unlock the mouse
        }
    }

    /// <summary>
    /// Delay before code continues
    /// </summary>
    private void TimeDelay()
    {
        if (currentDuration < 1.5f)
        {
            currentDuration += Time.deltaTime;
        }
        else if (currentDuration >= 1.5f)
        {
            currentDuration = 0;
            delayDone = true;
            timeDelayStart = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDelayStart) //To start the timed delay
        {
            TimeDelay();
        }

        if (playerDone && delayDone) //To start smallchild2's turn
        {
            ChildIcon();

            delayDone = false;
            playerDone = false;
        }
        else if (pointCheck && delayDone) //To check for points
        {
            Minigame2Check();

            delayDone = false;
            pointCheck = false;
        }
    }
}
