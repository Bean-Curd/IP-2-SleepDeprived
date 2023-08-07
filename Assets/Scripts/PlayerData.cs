/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 06/08/2023
 * Description: IP2 - Data for Player to save
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject //To store the player information
{
    /// <summary>
    /// Player data variables
    /// </summary>
    [System.Serializable] 
    public class PlayerSaveData //For event bool: After the ':' is what is the last thing before it is true
    {
        /// <summary>
        /// The number of the scene the player is in
        /// </summary>
        public int sceneIndex;
        /// <summary>
        /// What day the player has progressed to
        /// </summary>
        public int dayCount;
        /// <summary>
        /// The player's health at the point of saving
        /// </summary>
        public int playerHealth;
        /// <summary>
        /// The player's inventory
        /// </summary>
        public List<CollectibleItems> Items;
        /// <summary>
        /// Has the player left the boy's house (The player cannot go back)
        /// </summary>
        public bool leaveBoyHouse;
        /// <summary>
        /// Has the player first spoken to the butcher: The butcher brings the boy into the house, dialogue plays, butcher tells boy to go play
        /// </summary>
        public bool introToButcher;
        /// <summary>
        /// Has the player met smallchild1
        /// </summary>
        public bool meetSmallChild1;
        /// <summary>
        /// Has the player spoken to smallchild1 in butcher house (Start minigame1)
        /// </summary>
        public bool startMinigame1;
        /// <summary>
        /// Has the player completed minigame1: Butcher sends smallchild1 back
        /// </summary>
        public bool endMinigame1;
        /// <summary>
        /// Has the player waited for the butcher: Boy eating day1 dinner screen
        /// </summary>
        public bool dinner1;
        /// <summary>
        /// Has the player seen day1 cutscene: End of cutscene, start of day 2 dialogue, boy feels lonely and wants to go park
        /// </summary>
        public bool cutscene1;
        /// <summary>
        /// Has the player met smallchild2: Minigame2 starts
        /// </summary>
        public bool meetSmallChild2;
        /// <summary>
        /// Has the player completed minigame2: Smallchild2 agrees to follow player to butchery
        /// </summary>
        public bool endMinigame2;
        /// <summary>
        /// Has the player entered the butchery (Start minigame3): Boy calls for butcher, dialogue plays, decide to play hide-and-seek
        /// </summary>
        public bool startMinigame3;
        /// <summary>
        /// Has the player completed minigame3: Butcher stops boy, tells boy to go up
        /// </summary>
        public bool endMinigame3;
        /// <summary>
        /// Has the player waited for the butcher: Boy eating day2 dinner screen
        /// </summary>
        public bool dinner2;
        /// <summary>
        /// Has the player seen day2 cutscene: End of cutscene, start of day 3 dialogue, boy has to deliver packages (package*5 added to inventory)
        /// </summary>
        public bool cutscene2;
        /// <summary>
        /// Has the player delivered the 1st package
        /// </summary>
        public bool neighbour1;
        /// <summary>
        /// Has the player delivered the 2nd package
        /// </summary>
        public bool neighbour2;
        /// <summary>
        /// Has the player delivered the 3rd package
        /// </summary>
        public bool neighbour3;
        /// <summary>
        /// Has the player delivered the 4th package
        /// </summary>
        public bool neighbour4;
        /// <summary>
        /// Has the player delivered the 5th package
        /// </summary>
        public bool neighbour5;
        /// <summary>
        /// Has the player returned to the butchery after delivering all the packages: Boy eating day3 dinner screen 
        /// </summary>
        public bool dinner3;
        /// <summary>
        /// Has the player seen day3 cutscene: End of cutscene, start of day 4 dialogue, boy has to deliver water (water*5 added to inventory)
        /// </summary>
        public bool cutscene3;
        /// <summary>
        /// Has the player delivered the 1st drink
        /// </summary>
        public bool drink1;
        /// <summary>
        /// Has the player delivered the 2nd drink
        /// </summary>
        public bool drink2;
        /// <summary>
        /// Has the player delivered the 3rd drink
        /// </summary>
        public bool drink3;
        /// <summary>
        /// Has the player delivered the 4th drink
        /// </summary>
        public bool drink4;
        /// <summary>
        /// Has the player delivered the 5th drink
        /// </summary>
        public bool drink5;
        /// <summary>
        /// Has the player returned to the butchery after delivering all the water: Boy eating day4 dinner screen 
        /// </summary>
        public bool dinner4;
        /// <summary>
        /// Has the player seen day3 cutscene: End of cutscene, start of day 5 dialogue, boy wants to help butcher
        /// </summary>
        public bool cutscene4;
        /// <summary>
        /// Has the player obtained the pill bottle (pill bottle in inventory)
        /// </summary>
        public bool pillBottle;
        /// <summary>
        /// Has the player obtained the glass (glass in inventory)
        /// </summary>
        public bool glass;
        /// <summary>
        /// Has the player given the glass+pills to the butcher: Boy leaves butcher to rest
        /// </summary>
        public bool gaveButcher;
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
