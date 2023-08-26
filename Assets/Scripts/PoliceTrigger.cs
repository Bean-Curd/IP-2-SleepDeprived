/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 04/08/2023
 * Description: IP2 - Police Trigger Area
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceTrigger : MonoBehaviour
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
            parent.GetComponent<Police>().inRange = true;
        }
    }

    /// <summary>
    /// Trigger when player leaving
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit(Collider collision)
    {
        parent.GetComponent<Police>().inRange = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
