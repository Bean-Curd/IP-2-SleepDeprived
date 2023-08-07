/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 01/08/2023
 * Description: IP2 - Data for Collectible Icons
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Create New Item")] //To store in the project easier (Under the Assets/Scripts/Collectibles folder)

public class CollectibleItems : ScriptableObject //To store the item information
{
    /// <summary>
    /// ID for each item: Meat is 1, Packages 1-5 are 2-6 respectively, Normal water is 7, Touched water is 8, Normal pill bottle is 9, Empty pill bottle is 10, Glass is 11
    /// </summary>
    public int id;
    /// <summary>
    /// Sprite that shows in the inventory
    /// </summary>
    public Sprite icon;
    /// <summary>
    /// Sprite that shows when item is selected in the inventory
    /// </summary>
    public Sprite selectedImage;
    /// <summary>
    /// Description for selected object
    /// </summary>
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
