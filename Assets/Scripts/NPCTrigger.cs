/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 03/08/2023
 * Description: IP2 - NPC Trigger Area
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCTrigger : MonoBehaviour
{
    /// <summary>
    /// To access the parent object
    /// </summary>
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Trigger when player in range
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player") //If player in range
        {
            parent.GetComponent<NPC>().inRange = true;
            if (parent.name == "NPC_smallchild2" && Minigame3.instance.round != 0 && SceneManager.GetActiveScene().buildIndex == 3) //If in the butchery and not the first time talking
            {
                parent.GetComponent<Minigame3>().isCaught = true;
            }
        }
    }

    /// <summary>
    /// Trigger when player leaving
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit(Collider collision)
    {
        parent.GetComponent<NPC>().inRange = false;
        parent.GetComponent<Minigame3>().isCaught = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
