/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 30/07/2023
 * Description: IP2 - Player Spawn
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSpot : MonoBehaviour
{
    /// <summary>
    /// Set the player's spawn locations as an instance
    /// </summary>
    public static PlayerSpawnSpot instance;

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
