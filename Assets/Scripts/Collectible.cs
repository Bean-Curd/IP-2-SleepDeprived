/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 01/08/2023
 * Description: IP2 - Connecting the item to the Collectible
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    /// <summary>
    /// To make the object appear on the screen
    /// </summary>
    public CollectibleItems item;

    /// <summary>
    /// Set the collectible items as an instance
    /// </summary>
    public static Collectible instance;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
