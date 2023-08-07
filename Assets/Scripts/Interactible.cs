/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 28/07/2023
 * Description: IP2 - Interactible Objects
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

//Constraint -> Highlight doesn't work with multiple interactibles in range
public class Interactible : MonoBehaviour
{
    /// <summary>
    /// To make the object appear on the screen
    /// </summary>
    new Renderer renderer;

    /// <summary>
    /// Save the color of the object first -> to convert back after highlighted
    /// </summary>
    Color color;

    /// <summary>
    /// If mouse is over the object
    /// </summary>
    public bool mouseOver;
    /// <summary>
    /// If player near enough
    /// </summary>
    public bool playerNear;

    /// <summary>
    /// Set the interactible objects as an instance
    /// </summary>
    public static Interactible instance;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        color = renderer.material.color;
    }

    /// <summary>
    /// When player's mouse is over an interactible object
    /// </summary>
    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    /// <summary>
    /// When player's mouse is no longer over an interactible object
    /// </summary>
    private void OnMouseExit()
    {
        mouseOver = false;
    }

    /// <summary>
    /// To change the color of an object when hovered in range
    /// </summary>
    private void HighlightObject()
    {
        renderer.material.EnableKeyword("_EMISSION");
        renderer.material.SetColor("_EmissionColor", new Color(0, 1, 1, 1));
        //PlayerUI.instance.interactibleText.SetActive(true);
    }

    /// <summary>
    /// To change the color of an object back when not hovered over
    /// </summary>
    private void ResetObject()
    {
        renderer.material.DisableKeyword("_EMISSION");
        renderer.material.color = color;
        //PlayerUI.instance.interactibleText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.selectedObject == gameObject)
        {
            playerNear = true;
        }
        else
        {
            playerNear = false;
        }

        if (playerNear && mouseOver && Player.instance.interactInRange)
        {
            HighlightObject();
        }
        else if (mouseOver != true || Player.instance.interactInRange != true)
        {
            ResetObject();
        }
    }
}
