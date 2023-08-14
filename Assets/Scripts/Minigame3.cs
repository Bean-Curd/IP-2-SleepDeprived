/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 12/08/2023
 * Description: IP2 - Minigame 3 (Hide-and-seek)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame3 : MonoBehaviour
{
    /// <summary>
    /// Number of rounds
    /// </summary>
    public int round = 0;
    /// <summary>
    /// Number of times player caught smallchild2
    /// </summary>
    public int numCaught = 0;
    /// <summary>
    /// Has the player found smallchild2
    /// </summary>
    public bool isCaught;
    /// <summary>
    /// To start delay
    /// </summary>
    public bool timeDelayStart;
    /// <summary>
    /// Duration of delay for code
    /// </summary>
    private float currentDuration;
    /// <summary>
    /// To end time delay
    /// </summary>
    private bool delayDone;

    /// <summary>
    /// The first place smallchild2 hides
    /// </summary>
    public GameObject place1;
    /// <summary>
    /// The second place smallchild2 hides
    /// </summary>
    public GameObject place2;
    /// <summary>
    /// The third place smallchild2 hides
    /// </summary>
    public GameObject place3;

    /// <summary>
    /// Set the minigame3 canvas as an instance
    /// </summary>
    public static Minigame3 instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeDelayStart = false;
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

        if (delayDone)
        {
            if (numCaught == 0)
            {
                gameObject.transform.position = place1.transform.position;
            }
            else if (numCaught == 1) 
            {
                gameObject.transform.position = place2.transform.position;
            }
            else if (numCaught == 2)
            {
                gameObject.transform.position = place3.transform.position; //Kick to an unreachable corner of the navmesh
            }

            gameObject.GetComponent<NPC>().resumeNormal = true;
            PlayerCanvas.instance.minigame3.SetActive(false);

            delayDone = false;
        }
    }
}
