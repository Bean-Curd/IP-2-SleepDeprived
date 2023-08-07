/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 30/07/2023
 * Description: IP2 - Player Death Animation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    /// <summary>
    /// Set the player's death sequence as an instance
    /// </summary>
    public static PlayerDeath instance;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// What happens after player dies
    /// </summary>
    void KillPlayer()
    {
        Debug.Log("Kill Player");

        GameManager.gameManager.dead = true;
        HealthBar.instance.Damage(-10000); //Restore HP so doesn't trigger player death again
    }

    /// <summary>
    /// Play the player death animation
    /// </summary>
    public void PlayerDeathSequence()
    {
        Debug.Log("Player Dies");
        GetComponent<Animator>().enabled = true;
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
