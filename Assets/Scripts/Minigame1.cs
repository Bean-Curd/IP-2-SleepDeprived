/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 09/08/2023
 * Description: IP2 - Minigame 1 (Matching Cards)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Threading;

public class Minigame1 : MonoBehaviour
{
    /// <summary>
    /// Number of cards flipped
    /// </summary>
    public int numCardsFlipped = 0;
    /// <summary>
    /// Number of cards smallchild1 flipped
    /// </summary>
    public int childCardsFlipped = 0;
    /// <summary>
    /// Total cards remaining
    /// </summary>
    public int remainingCards = 12;
    /// <summary>
    /// Number of pairs the player completed
    /// </summary>
    public int numCardPairs = 0;
    /// <summary>
    /// For smallchild1's card1
    /// </summary>
    private int num1;
    /// <summary>
    /// For smallchild1's card2
    /// </summary>
    private int num2;
    /// <summary>
    /// Card 1
    /// </summary>
    private Transform card1;
    /// <summary>
    /// Card 2
    /// </summary>
    private Transform card2;
    /// <summary>
    /// To end the game
    /// </summary>
    private bool endGame;
    /// <summary>
    /// Can you start smallchild1's turn
    /// </summary>
    private bool canStartChildTurn;
    /// <summary>
    /// Is it smallchild1's turn
    /// </summary>
    private bool isChildTurn;
    /// <summary>
    /// To start card check
    /// </summary>
    private bool minigameCheck;
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
    /// Back of the card
    /// </summary>
    public Sprite cardBack;
    /// <summary>
    /// To access each card for minigame1
    /// </summary>
    public Transform CardsContent;

    /// <summary>
    /// Dialogue for when player gets a pair of cards
    /// </summary>
    public GameObject playerPair;
    /// <summary>
    /// Dialogue for when smallchild1 gets a pair of cards
    /// </summary>
    public GameObject childPair;
    /// <summary>
    /// Dialogue for player's turn
    /// </summary>
    public GameObject playerTurn;
    /// <summary>
    /// Dialogue for smallchild1's turn
    /// </summary>
    public GameObject childTurn;

    /// <summary>
    /// Set the minigame1 canvas as an instance
    /// </summary>
    public static Minigame1 instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        endGame = false;
        isChildTurn = false;
        minigameCheck = false;
        timeDelayStart = false;
        playerTurn.SetActive(true);
    }

    /// <summary>
    /// To flip the cards over when player clicks
    /// </summary>
    public void PlayerFlipCards()
    {
        childPair.SetActive(false);

        if (numCardsFlipped < 2 && isChildTurn == false && EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite == cardBack) //Cannot flip cards if more than 2 or it is smallchild1's turn or if the card has been selected/used 
        {
            numCardsFlipped += 1;

            if (numCardsFlipped == 1) //If only 1 card over, save name of card
            {
                card1 = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
                Transform cardImage1 = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
                cardImage1.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.transform.GetComponent<Minigame1Cards>().cardSymbol;
            }
            else if (numCardsFlipped == 2) //If 2 cards over, save name of card and check if they are the same
            {
                card2 = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
                Transform cardImage2 = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
                cardImage2.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.transform.GetComponent<Minigame1Cards>().cardSymbol;

                minigameCheck = true;
                timeDelayStart = true;
                Cursor.lockState = CursorLockMode.Locked; //To lock the mouse
            }
        }
    }

    /// <summary>
    /// For smallchild1 to flip the cards over
    /// </summary>
    public void ChildFlipCards()
    {
        playerTurn.SetActive(false);
        childTurn.SetActive(true);
        playerPair.SetActive(false);
        isChildTurn = true;

        childCardsFlipped += 1;

        if (childCardsFlipped == 1) //If only 1 card over, generate card
        {
            System.Random random1 = new System.Random(); //Get a random card
            num1 = random1.Next(0, 12);

            while (CardsContent.GetChild(num1).GetChild(0).GetComponent<Image>().sprite == PlayerCanvas.instance.voidMenu) //If the random card is already a found pair, +1 to the num
            {
                num1 += 1;

                if (num1 == 12) //If it is position 12, reset
                {
                    num1 = 0;
                }
            }

            card1 = CardsContent.GetChild(num1).GetChild(0);
            Transform cardImage1 = CardsContent.GetChild(num1).GetChild(0);
            cardImage1.GetComponent<Image>().sprite = CardsContent.GetChild(num1).GetComponent<Minigame1Cards>().cardSymbol;
        }

        if (childCardsFlipped == 2) //If 2 cards over, save name of card and check if they are the same
        {
            System.Random random2 = new System.Random(); //Get a random card
            num2 = random2.Next(0, 12);

            while (num2 == num1 || CardsContent.GetChild(num2).GetChild(0).GetComponent<Image>().sprite == PlayerCanvas.instance.voidMenu) //If the random card is already a found pair, +1 to the num
            {
                num2 += 1;

                if (num2 == 12) //If it is position 12, reset
                {
                    num2 = 0;
                }
            }

            card2 = CardsContent.GetChild(num2).GetChild(0);
            Transform cardImage2 = CardsContent.GetChild(num2).GetChild(0);
            cardImage2.GetComponent<Image>().sprite = CardsContent.GetChild(num2).GetComponent<Minigame1Cards>().cardSymbol;

            minigameCheck = true;
            timeDelayStart = true;
        }
    }

    /// <summary>
    /// What happens when 2 cards are shown
    /// </summary>
    public void Minigame1Check()
    {
        //Thread.Sleep(3000);

        if (card1.name == card2.name) //If the cards are the same, remove the cards and add points
        {
            if (isChildTurn != true) //If it is the player's turn
            {
                playerPair.SetActive(true);
                numCardPairs += 1;
            }
            else //If it is smallchild1's turn
            {
                childPair.SetActive(true);
            }

            remainingCards -= 2;
            card1.GetComponent<Image>().sprite = PlayerCanvas.instance.voidMenu;
            card2.GetComponent<Image>().sprite = PlayerCanvas.instance.voidMenu;
        }
        else
        {
            card1.GetComponent<Image>().sprite = cardBack;
            card2.GetComponent<Image>().sprite = cardBack;
        }

        if (remainingCards == 0) //If there are no more pairs of cards remaining, end game
        {
            endGame = true;
        }
        else if (isChildTurn == false && numCardsFlipped == 2 && remainingCards > 0) //If the player completed their turn and there are cards left, let smallchild1 play
        {
            numCardsFlipped = 0;
            timeDelayStart = true;
            canStartChildTurn = true;
        }

        if (isChildTurn)
        {
            Cursor.lockState = CursorLockMode.None; //To unlock the mouse
        }
    }

    /// <summary>
    /// What happens when minigame1 ends
    /// </summary>
    public void Minigame1End()
    {
        playerPair.SetActive(false);
        childPair.SetActive(false);
        isChildTurn = false;
        Player.instance.doneMinigame1 = true;

        if (numCardPairs > 3)
        {
            PlayerCanvas.instance.minigame1win.SetActive(true);
        }
        else if (numCardPairs == 3)
        {
            PlayerCanvas.instance.minigame1tie.SetActive(true);
        }
        else if (numCardPairs < 3)
        {
            PlayerCanvas.instance.minigame1lose.SetActive(true);
        }

        endGame = false;
        Cursor.lockState = CursorLockMode.Locked; //To lock the mouse
        PlayerCanvas.instance.minigame1.SetActive(false);
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
        if (endGame) //To end the game
        {
            Minigame1End();
        }

        if (timeDelayStart) //To start the timed delay
        {
            TimeDelay();
        }

        if (minigameCheck && delayDone) //To check the cards
        {
            Minigame1Check();

            if (isChildTurn)
            {
                childTurn.SetActive(false);
                playerTurn.SetActive(true);
                isChildTurn = false;
            }

            delayDone = false;
            minigameCheck = false;
        }
        else if (canStartChildTurn && delayDone) //To check if smallchild1 can start
        {
            ChildFlipCards();
            ChildFlipCards();

            num1 = 0;
            num2 = 0;
            childCardsFlipped = 0;

            delayDone = false;
            canStartChildTurn = false;
        }
    }
}
