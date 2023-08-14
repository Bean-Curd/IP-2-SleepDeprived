/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 08/08/2023
 * Description: IP2 - Day Night Cycle
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    [SerializeField] private Material skybox;
    /// <summary>
    /// How much time has passed
    /// </summary>
    private float elapsedTime = 0f;
    /// <summary>
    /// To get the skybox's exposure to change the color
    /// </summary>
    private static readonly int exposure = Shader.PropertyToID("_Exposure");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += 0.001f;
        skybox.SetFloat(exposure, Mathf.Clamp(Mathf.Sin(elapsedTime), 0.5f, 1f)); //Changes the lighting (Clamp -> to fix it between the 2 numbers)
    }
}
