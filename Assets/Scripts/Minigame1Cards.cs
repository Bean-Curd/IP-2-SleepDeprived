/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 08/08/2023
 * Description: IP2 - Minigame 1 Cards (Matching Cards)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Minigame1Cards : MonoBehaviour
{
    /// <summary>
    /// Face of the card
    /// </summary>
    public Sprite cardSymbol;

    /// <summary>
    /// Set the minigame1cards as an instance
    /// </summary>
    public static Minigame1Cards instance;

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
