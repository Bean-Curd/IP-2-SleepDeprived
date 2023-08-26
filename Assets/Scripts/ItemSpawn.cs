/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 19/08/2023
 * Description: IP2 - Item spawns
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSpawn : MonoBehaviour
{
    /// <summary>
    /// ID for item
    /// </summary>
    public int idItem;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (idItem == 9) //For the Pill Bottle
            {
                if (GameManager.gameManager.pillBottle) //If already obtained, destroy
                {
                    Destroy(gameObject);
                }
            }
            else if (idItem == 11) //For the Glass
            {
                if (GameManager.gameManager.glass) //If already obtained, destroy
                {
                    Destroy(gameObject);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 && GameManager.gameManager.dayCount == 5)
        {
            gameObject.SetActive(true);
            GameObject sun = GameObject.Find("Directional Light");
            Destroy(sun);
            Debug.Log("Raining");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2 && idItem == 40 && GameManager.gameManager.dayCount != 5)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
