/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 03/08/2023
 * Description: IP2 - NPC Trigger Area
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
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
            NPC.instance.inRange = true;
        }
    }

    /// <summary>
    /// Trigger when player leaving
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit(Collider collision)
    {
        NPC.instance.inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}