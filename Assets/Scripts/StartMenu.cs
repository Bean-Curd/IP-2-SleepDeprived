/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 28/07/2023
 * Description: IP2 - Start Page
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// To start the game -> Changes the scene to the 1st scene 
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// To exit the game when the player says yes
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; //So it works in play mode for testing
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
