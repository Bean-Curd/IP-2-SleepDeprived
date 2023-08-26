/*
 * Author: SleepDeprived - Ashley, Su Mon, Yixin
 * Date: 29/07/2023
 * Description: IP2 - Game Manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using static PlayerData;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Set the game manager as an instance
    /// </summary>
    public static GameManager gameManager { get; private set; }

    #region Spawn Related Variables

    /// Player prefab
    /// </summary>
    public GameObject playerPrefab;
    /// <summary>
    /// Canvas prefab
    /// </summary>
    public GameObject canvasPrefab;

    /// <summary>
    /// Current player
    /// </summary>
    private GameObject activePlayer;
    /// <summary>
    /// Current canvas
    /// </summary>
    private GameObject activeCanvas;

    /// <summary>
    /// Scene
    /// </summary>
    private Scene scene;
    /// <summary>
    /// Build Index of the previous scene
    /// </summary>
    private int previousBuildIndex;
    /// <summary>
    /// Build Index of the current scene
    /// </summary>
    private int buildIndex;

    /// <summary>
    /// Player spawn 1 
    /// </summary>
    public GameObject spawn1;
    /// <summary>
    /// Spawn 1 Location
    /// </summary>
    private Vector3 spawn1Location;
    /// <summary>
    /// Player spawn 2
    /// </summary>
    public GameObject spawn2;
    /// <summary>
    /// Spawn 2 Location
    /// </summary>
    private Vector3 spawn2Location;
    /// <summary>
    /// Player spawn 3
    /// </summary>
    public GameObject spawn3;
    /// <summary>
    /// Spawn 3 Location
    /// </summary>
    private Vector3 spawn3Location;

    #endregion

    #region Player/Collectible Related Variables

    /// <summary>
    /// What day is it
    /// </summary>
    public int dayCount;
    /// <summary>
    /// Player health to carry between scenes (*Changing it does not change the player's health)
    /// </summary>
    public int playerHealth;

    /// <summary>
    /// Pause the game
    /// </summary>
    public bool pause;
    /// <summary>
    /// Player is dead
    /// </summary>
    public bool dead;
    /// <summary>
    /// What to do when player respawns
    /// </summary>
    public bool respawn;
    /// <summary>
    /// What to do when player is in inventory
    /// </summary>
    public bool inventory;
    /// <summary>
    /// Did the player enter the butchery after completing minigame2
    /// </summary>
    public bool entered;
    /// <summary>
    /// To not add the packages to the inventory more than once
    /// </summary>
    public bool addPackagesOnce;
    /// <summary>
    /// To not add the bottles to the inventory more than once
    /// </summary>
    public bool addBottlesOnce;
    /// <summary>
    /// So the player mixes the pills and the water once
    /// </summary>
    public bool mixOnce;
    /// <summary>
    /// To check if the player returning to the butchery after giving the butcher the glass will trigger
    /// </summary>
    public bool canComeBack;
    /// <summary>
    /// Is the player returning to the butchery after giving the butcher the glass
    /// </summary>
    public bool comeBack;

    /// <summary>
    /// To store items Player collects
    /// </summary>
    public List<CollectibleItems> Items = new List<CollectibleItems>();
    /// <summary>
    /// The prefab of the item icon in the inventory
    /// </summary>
    public GameObject InventoryItem;

    #endregion

    #region Event Related Variables

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
    /// Has the player interacted with the butcher again afterwards: Long final cutscene :/ (If true, delete save file)
    /// </summary>
    public bool finalCutscene;

    #endregion

    #region Coroutine Related Variables

    /* /// <summary>
    /// For the loading screen
    /// </summary>
    private Coroutine loadingScreenWait; */

    #endregion

    private void Awake()
    {
        if (gameManager != null && gameManager != this) //If there is another game manager, destroy this one
        {
            Destroy(gameObject);

            Debug.Log("GameManager destroyed");

            return;
        }
        else
        {
            gameManager = this;

            Debug.Log("GameManager not destroyed");
        }

        DontDestroyOnLoad(gameObject);

        activePlayer = GameObject.FindGameObjectWithTag("Player");
        activeCanvas = GameObject.FindGameObjectWithTag("Canvas");

        SceneManager.activeSceneChanged += SpawnPlayerOnSceneLoad;
    }

    /// <summary>
    /// Spawns the player when the scene is loaded
    /// </summary>
    private void SpawnPlayerOnSceneLoad(Scene currentScene, Scene nextScene)
    {
        buildIndex = nextScene.buildIndex;

        spawn1 = GameObject.FindGameObjectWithTag("Spawn1");
        spawn1Location = new Vector3(spawn1.transform.position.x, spawn1.transform.position.y, spawn1.transform.position.z);

        if (SceneManager.GetActiveScene().buildIndex == 3) //The butchery has 1 other spawn
        {
            spawn2 = GameObject.FindGameObjectWithTag("Spawn2");
            spawn2Location = new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) //The street has 2 other spawns
        {
            spawn2 = GameObject.FindGameObjectWithTag("Spawn2");
            spawn2Location = new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z);

            spawn3 = GameObject.FindGameObjectWithTag("Spawn3");
            spawn3Location = new Vector3(spawn3.transform.position.x, spawn3.transform.position.y, spawn3.transform.position.z);
        }

        if (activePlayer != null || activeCanvas != null) //If there is a player originally in the scene, kill it
        {
            Destroy(activePlayer);
            Destroy(activeCanvas);
            Debug.Log("Original player destroyed: " + activePlayer);
            activePlayer = null;
            activeCanvas = null;
        }

        if (activePlayer == null && buildIndex != 0) //If there is no player and not in start menu, spawn player prefab at spawn point
        {
            activeCanvas = Instantiate(canvasPrefab);

            if (buildIndex == 4) //In specific scenes, have specific rotations
            {
                activePlayer = Instantiate(playerPrefab, spawn1Location, Quaternion.Euler(new Vector3(0, 90, 0)));
            }
            else if (buildIndex == 3) //In specific scenes, have specific rotations
            {
                activePlayer = Instantiate(playerPrefab, spawn1Location, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (buildIndex == 2) //In specific scenes, have specific rotations
            {
                if (previousBuildIndex == 3) //If starting from the butchery, spawn outside it
                {
                    activePlayer = Instantiate(playerPrefab, spawn2Location, Quaternion.Euler(new Vector3(0, 90, 0)));
                }
                else if (previousBuildIndex == 4) //If starting from the park, spawn outside it
                {
                    activePlayer = Instantiate(playerPrefab, spawn3Location, Quaternion.Euler(new Vector3(0, 270, 0)));
                }
                else if (dayCount == 3)
                {
                    activePlayer = Instantiate(playerPrefab, spawn2Location, Quaternion.Euler(new Vector3(0, 90, 0)));
                }
                else if (dayCount == 4)
                {
                    activePlayer = Instantiate(playerPrefab, spawn2Location, Quaternion.Euler(new Vector3(0, 90, 0)));
                }
                else
                {
                    activePlayer = Instantiate(playerPrefab, spawn1Location, Quaternion.Euler(new Vector3(0, 180, 0)));
                }
            }
            else if (buildIndex == 1) //In specific scenes, have specific rotations
            {
                activePlayer = Instantiate(playerPrefab, spawn1Location, Quaternion.Euler(new Vector3(0, 0, 0)));

                Player.instance.inDialogue = true;
                PlayerCanvas.instance.introTime = true;
                PlayerCanvas.instance.playerBars.SetActive(false);
                PlayerCanvas.instance.dialogueBox.SetActive(true);
                PlayerCanvas.instance.introL1.SetActive(true);

                dayCount = 1;
            }

            Debug.Log("Active player spawned: " + activePlayer);
            previousBuildIndex = SceneManager.GetActiveScene().buildIndex;

            if ((dayCount == 3 || dayCount == 4) && SceneManager.GetActiveScene().buildIndex == 2) //Spawn the police on day3 + 4
            {
                GameObject police1 = GameObject.Find("Police1");
                if (dayCount == 3 && police1 != null)
                {
                    police1.SetActive(true);
                    GameObject police2 = GameObject.Find("Police2");
                    police2.SetActive(false);
                    GameObject police3 = GameObject.Find("Police3");
                    police3.SetActive(false);
                }
                else if (dayCount == 4 && police1 != null)
                {
                    police1.SetActive(true);
                    GameObject police2 = GameObject.Find("Police2");
                    police2.SetActive(true);
                    GameObject police3 = GameObject.Find("Police3");
                    police3.SetActive(true);
                }
            }
            else
            {
                GameObject police1 = GameObject.Find("Police1");

                if (police1 != null)
                {
                    police1.SetActive(false);
                    GameObject police2 = GameObject.Find("Police2");
                    police2.SetActive(false);
                    GameObject police3 = GameObject.Find("Police3");
                    police3.SetActive(false);
                }
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Loading Screen between scenes
    /// </summary>
    public void LoadingScreen()
    {
        if (dead) //If dead hide the player death page
        {
            PlayerCanvas.instance.deathMenu.SetActive(false);
            dead = false;
        }

        PlayerCanvas.instance.loadingMenu.SetActive(true);
        PlayerCanvas.instance.loading1.SetActive(true);
    }

    /// <summary>
    /// Spawns the player after death
    /// </summary>
    private void RespawnPlayer()
    {
        PlayerCanvas.instance.deathMenu.SetActive(false);
        HealthBar.instance.Damage(-10000); //Restore HP

        LoadingScreen();

        Destroy(activePlayer); //Destroy player and canvas, respawn them at spawn location
        Destroy(activeCanvas);
        Debug.Log("Original player destroyed: " + activePlayer);
        activePlayer = null;
        activeCanvas = null;

        activeCanvas = Instantiate(canvasPrefab);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the scene

        Debug.Log("Active player spawned: " + activePlayer);
    }

    /* //No time cher, sorry :(
    /// <summary>
    /// Convert PlayerSaveData to Json
    /// </summary>
    public string ConvertClassToJson()
    {
        PlayerSaveData data = new PlayerSaveData
        {
            sceneIndex = buildIndex, 
            dayCount = dayCount, 
            playerHealth = playerHealth, 
            Items = Items, 
            leaveBoyHouse = leaveBoyHouse, 
            introToButcher = introToButcher, 
            meetSmallChild1 = meetSmallChild1, 
            endMinigame1 = endMinigame1, 
            dinner1 = dinner1, 
            cutscene1 = cutscene1, 
            meetSmallChild2 = meetSmallChild2, 
            endMinigame2 = endMinigame2, 
            endMinigame3 = endMinigame3, 
            dinner2 = dinner2, 
            cutscene2 = cutscene2, 
            neighbour1 = neighbour1, 
            neighbour2 = neighbour2, 
            neighbour3 = neighbour3, 
            neighbour4 = neighbour4,
            neighbour5 = neighbour5,
            dinner3 = dinner3,
            cutscene3 = cutscene3,
            drink1 = drink1,
            drink2 = drink2,
            drink3 = drink3,
            dinner4 = dinner4,
            cutscene4 = cutscene4,
            pillBottle = pillBottle,
            glass = glass,
        };
        return JsonUtility.ToJson(data);
    }

    /// <summary>
    /// Convert Json to PlayerSaveData
    /// </summary>
    public PlayerData ConvertJsonToClass(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }

    /// <summary>
    /// To save the game
    /// </summary>
    public void SavePlayerData()
    {
        System.IO.File.WriteAllText(SaveDataLocation, ConvertClassToJson());
    }

    /// <summary>
    /// To load the game save file
    /// </summary>
    public PlayerData LoadPlayerData()
    {
        if (!System.IO.File.Exists(SaveDataLocation))
        {
            return new PlayerData();
        }

        string json = System.IO.File.ReadAllText(SaveDataLocation);
        return ConvertJsonToClass(json);
    }
    */

    /// <summary>
    /// To reset the game when leaving to the start menu
    /// </summary>
    public void ResetGame()
    {
        Items.Clear(); //Clear inventory

        buildIndex = 0; 
        dayCount = 0; 
        playerHealth = 100;
        leaveBoyHouse = false; 
        introToButcher = false;
        meetSmallChild1 = false;
        endMinigame1 = false;
        dinner1 = false;
        cutscene1 = false;
        meetSmallChild2 = false;
        endMinigame2 = false;
        endMinigame3 = false;
        dinner2 = false;
        cutscene2 = false;
        neighbour1 = false;
        neighbour2 = false;
        neighbour3 = false;
        neighbour4 = false;
        neighbour5 = false;
        dinner3 = false;
        cutscene3 = false;
        drink1 = false;
        drink2 = false;
        drink3 = false;
        dinner4 = false;
        cutscene4 = false;
        pillBottle = false;
        glass = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 10000;
        pause = false;
        dead = false;
        respawn = false;
        inventory = false;
    }

    /// <summary>
    /// When the player collects a collectible, add it to the list
    /// </summary>
    public void ItemCollected(CollectibleItems item)
    {
        Items.Add(item);
    }

    /// <summary>
    /// When a collectible is used, remove it from the list 
    /// </summary>
    public void ItemUsed(CollectibleItems item)
    {
        Items.Remove(item);
        var originalImage = PlayerCanvas.instance.SelectedItem.GetChild(0).GetComponent<Image>();
        var originalDescription = PlayerCanvas.instance.SelectedItem.GetChild(1).GetComponent<TextMeshProUGUI>();

        originalImage.sprite = PlayerCanvas.instance.voidMenu;
        originalDescription.text = "";

    }

    /// <summary>
    /// To add collected items into the Inventory display
    /// </summary>
    public void ListItems()
    {
        Transform content = PlayerCanvas.instance.InventoryContent;

        foreach (Transform child in content) //To reset the inventory before it is regenerated from the list
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Items) //Generates the item images from the list
        {
            GameObject obj = Instantiate(InventoryItem, content);
            var itemIcon = obj.transform.GetChild(0).GetComponent<Image>();
            var listNum = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            itemIcon.sprite = item.icon;
            listNum.text = "" + item.id;
        }
    }

    /// <summary>
    /// When an item is selected in the inventory (There's probably a smarter way to do this sorry cher :P)
    /// </summary>
    public void SelectedItem()
    {
        var listNum = EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        int number = Int16.Parse(listNum.text);

        foreach (var item in Items)
        {
            if (item.id == number)
            {
                var image = PlayerCanvas.instance.SelectedItem.GetChild(0).GetComponent<Image>();
                var description = PlayerCanvas.instance.SelectedItem.GetChild(1).GetComponent<TextMeshProUGUI>();

                image.sprite = item.selectedImage;
                description.text = item.description;

                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (respawn) //If respawning, kill player and respawn at spawn point
        {
            Debug.Log("Respawning Player");
            RespawnPlayer();
            respawn = false;
        }

        if (dead) //If currently dead, stop time
        {
            Time.timeScale = 0;
            Player.instance.moveSpeed = 0;
            Player.instance.rotationSpeed = 0.00f;
        }
        else if (pause) //If currently paused, stop time
        {
            Time.timeScale = 0;
            Player.instance.moveSpeed = 0;
            Player.instance.rotationSpeed = 0.00f;
        }
        else if (pause != true) //If not paused or dead, time flows normally
        {
            Time.timeScale = 1;
            Player.instance.moveSpeed = 3f;
            Player.instance.rotationSpeed = 0.25f;
        }
        
        if (Player.instance.inDialogue && SceneManager.GetActiveScene().buildIndex != 0) //If player is talking, cannot walk
        {
            if (PlayerCanvas.instance.minigame1.activeSelf || PlayerCanvas.instance.minigame2.activeSelf || PlayerCanvas.instance.minigame3.activeSelf || PlayerCanvas.instance.dinner1.activeSelf || PlayerCanvas.instance.cutsceneDay1.activeSelf || PlayerCanvas.instance.dinner2.activeSelf || PlayerCanvas.instance.cutsceneDay2.activeSelf || PlayerCanvas.instance.dinner3.activeSelf || PlayerCanvas.instance.cutsceneDay3.activeSelf || PlayerCanvas.instance.dinner4.activeSelf || PlayerCanvas.instance.cutsceneDay4.activeSelf || finalCutscene) //Make an excecption for some cutscenes/events
            {
                
            }
            else
            {
                Time.timeScale = 0;
                if (EnergyBar.instance.currentEnergy == 0)
                {
                    EnergyBar.instance.currentEnergy += 1000; //Add 10 so it doesn't stop
                    EnergyBar.instance.energyBar.value = EnergyBar.instance.currentEnergy;
                }
            }
        }
        else if (Player.instance.inDialogue != true && pause != true) //If player is not talking, enable movement
        {
            Time.timeScale = 1;
        }

        //If multiple conditions are fufilled, can progess on: for days 3/4/5
        if (neighbour1 && neighbour2 && neighbour3 && neighbour4 && neighbour5)
        {
            Player.instance.deliverDone = true;
        }
        if (drink1 && drink2 && drink3)
        {
            Player.instance.bottlesDone = true;
        }
        if (pillBottle && glass)
        {
            Player.instance.mixtureDone = true;

            if (mixOnce != true)
            {
                ItemUsed(Player.instance.pillBottleNormal);
                ItemUsed(Player.instance.glass);
                ItemCollected(Player.instance.pillBottleEmpty);
                ItemCollected(Player.instance.glassMixed);
                mixOnce = true;
            }
        }
    }
}
