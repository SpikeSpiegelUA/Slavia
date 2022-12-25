using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Audio;

public class GUIController : MonoBehaviour
{
    public GameObject journal;
    public GameObject compass;
    public bool GUIopened = false;
    public Image inventory;
    public GameObject itemInfoRay;
    public GameObject itemInfoInventory;
    public GameObject characterInfoInventory;
    public GameObject timeImageGameObject;
    public GameObject personName;
    public GameObject enemyHPPlayer;
    public GameObject personNameText;
    public GameObject enemyHPPlayerText;
    public GameObject stealtBar;
    public GameObject lootWindow;
    public GameObject stealthBar;
    public PlayerController playerController;
    public CameraMovement cameraMovement;
    public GameObject shop;
    public GameObject inventoryUI;
    public GameObject dialogueUI;
    public GameObject closeButton;
    public GameObject paladinList;
    public GameObject thiefList;
    public GameObject mageList;
    public GameObject permanentGUI;
    public GameObject questUI;
    public GameObject inventoryButton;
    public GameObject questButton;
    public GameObject skillUI;
    public GameObject skillButton;
    public GameObject statsButton;
    public GameObject statsUI;
    public GameObject blurScreen;
    public GameObject pauseMenu;
    public GameObject loadMenu;
    public GameObject saveMenu;
    public GameObject settingsMenu;
    public GameObject deathBlur;
    public GameObject deathMenu;
    public GameObject saveSlotPrefab;
    public GameObject cookWindow;
    public GameObject needIngridient;
    public Transform deadLoadSlotList;
    public Transform saveSlotsList;
    public Transform loadSlotList;
    public GameObject selectedSaveSlot;
    public AudioMixer audioMixer;
    public Slider masterVolume;
    public Slider effectsVolume;
    public Slider musicVolume;
    public GameObject alchemyWindow;
    public GameObject hunterShop;
    public GameObject thiefShop;
    public GameObject paladinShop;
    public GameObject mageShop;
    public GameObject needToBeRobber;
    public GameObject pointer;
    public GameObject enoughMana;
    public GameObject control;
    // Start is called before the first frame update
    void Start()
    {      
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
        {
            mageList.SetActive(true);
            skillUI.transform.Find("MageSkills").gameObject.SetActive(true);
        }
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
        {
            thiefList.SetActive(true);
            skillUI.transform.Find("RobberSkills").gameObject.SetActive(true);
        }
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
        {
            paladinList.SetActive(true);
            skillUI.transform.Find("PaladinSkills").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    //Script for player's GUI
    public void JournalGUI()
    {
        if (lootWindow.activeSelf == false&&!GetComponent<PauseMenu>().gameIsPaused)
        {
            if (playerController.currentHealth > 0)
            {
                //If Journal is closed than open journal
                if (GUIopened == false)
                {
                    control.SetActive(false);
                    blurScreen.SetActive(true);
                    GameObject.Find("Music").GetComponent<AudioSource>().pitch *= 0.5f;
                    Time.timeScale = 0f;
                    playerController.newLevel.SetActive(false);
                    statsButton.SetActive(true);
                    skillButton.SetActive(true);
                    questButton.SetActive(true);
                    inventoryButton.SetActive(true);
                    closeButton.SetActive(true);
                    personName.GetComponent<Image>().enabled = false;
                    stealtBar.SetActive(false);
                    enemyHPPlayer.GetComponent<Image>().enabled = false;
                    personName.GetComponent<Image>().enabled = false;
                    enemyHPPlayer.GetComponent<Image>().enabled = false;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                        GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
                    else
                        GameObject.Find("Player").GetComponent<Animator>().Play("CrouchingIdle");
                    GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
                    GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
                    GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                    GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
                    GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                    GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouchRunning", false);
                    GameObject.Find("Player").GetComponent<AudioSource>().clip = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("CloseGoldWindow");
                    GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("CloseGrabWindow");
                    GetComponent<Inventory>().inventoryIsFull.SetActive(false);
                    GetComponent<Inventory>().needMoreSpace.SetActive(false);
                    GetComponent<Inventory>().needMoreMoney.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.SetActive(false);
                    characterInfoInventory.transform.Find("HealthText").GetComponent<Text>().text = "Health:" + playerController.currentHealth + "/" + playerController.hpPlayer;
                    characterInfoInventory.transform.Find("StaminaText").GetComponent<Text>().text = "Stamina:" + playerController.currentStamina + "/" + playerController.staminaPlayer;
                    characterInfoInventory.transform.Find("ManaText").GetComponent<Text>().text = "Mana:" + playerController.currentMana + "/" + playerController.manaPlayer;
                    characterInfoInventory.transform.Find("LevelText").GetComponent<Text>().text = "Level:" + playerController.level;
                    characterInfoInventory.transform.Find("PrestigeText").GetComponent<Text>().text = "Prestige:" + playerController.prestige;
                    characterInfoInventory.transform.Find("GoldText").GetComponent<Text>().text = "Gold:" + playerController.gold;
                    characterInfoInventory.transform.Find("ArmorText").GetComponent<Text>().text = "Armor:" + playerController.armor;
                    inventory.gameObject.SetActive(true);
                    GUIopened = true;
                    playerController.enabled = false;
                    cameraMovement.enabled = false;
                    itemInfoRay.gameObject.SetActive(false);
                    characterInfoInventory.gameObject.SetActive(true);
                    GameObject.Find("Journal").gameObject.SetActive(true);
                    enemyHPPlayerText.GetComponent<Text>().text = "";
                    personNameText.GetComponent<Text>().text = "";
                    compass.SetActive(false);
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().newStatLevel.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().locked.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().needPicklock.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().pickpocketingWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().unlockFailed.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needToBeRobber.SetActive(false);
                    personName.SetActive(false);
                    enemyHPPlayer.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().enoughMana.SetActive(false);
                }
                //If journal is opened than close journal
                else if (GUIopened == true)
                {
                    control.SetActive(true);
                    blurScreen.SetActive(false);
                    GameObject.Find("Music").GetComponent<AudioSource>().pitch *= 2;
                    Time.timeScale = 1f;
                    permanentGUI.SetActive(true);
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().selectedItem = null;
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("Description").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("QuestName").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("GoldReward").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("ExperienceReward").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("PrestigeReward").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("ObjectRewardText").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("ObjectRewardImage").GetComponent<Image>().sprite = null;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("ObjectRewardImage").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("SkillPoints").GetComponent<Text>().text = "";
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().questInfo.transform.Find("CurrentGoal").GetComponent<Text>().text = "";
                    statsUI.SetActive(false);
                    statsButton.SetActive(false);
                    skillButton.SetActive(false);
                    compass.SetActive(true);
                    skillUI.SetActive(false);
                    questUI.SetActive(false);
                    questButton.SetActive(false);
                    inventoryButton.SetActive(false);
                    closeButton.SetActive(false);
                    if (playerController.isCrouched)
                        stealtBar.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem.GetComponent<Image>().color = Color.white;
                    inventory.gameObject.SetActive(false);
                    GUIopened = false;
                    if (!GameObject.Find("Player").GetComponent<Animator>().GetBool("IsStunned"))
                    {
                        playerController.enabled = true;
                        cameraMovement.enabled = true;
                    }
                    characterInfoInventory.gameObject.SetActive(false);
                    itemInfoInventory.gameObject.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem = timeImageGameObject.gameObject;
                    personName.SetActive(true);
                    enemyHPPlayer.SetActive(true);
                }
            }
        }
    }
    public void OpenQuests()
    {
        GameObject.Find("SkillManager").GetComponent<SkillManager>().selectedItem = null;
        permanentGUI.SetActive(false);
        skillUI.SetActive(false);
        questUI.SetActive(true);
        itemInfoInventory.SetActive(false);
        inventoryUI.SetActive(false);
        itemInfoInventory.SetActive(false);
        characterInfoInventory.SetActive(false);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
        statsUI.SetActive(false);
    }
    public void OpenStats()
    {
        GameObject.Find("SkillManager").GetComponent<SkillManager>().selectedItem = null;
        permanentGUI.SetActive(false);
        skillUI.SetActive(false);
        questUI.SetActive(false);
        itemInfoInventory.SetActive(false);
        inventoryUI.SetActive(false);
        itemInfoInventory.SetActive(false);
        characterInfoInventory.SetActive(false);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
        statsUI.SetActive(true);
    }
    public void OpenSkills()
    {
        skillUI.transform.Find("SkillPoints").GetComponent<Text>().text = "Skill points:" + playerController.skillPoints;
        GameObject.Find("SkillManager").GetComponent<SkillManager>().selectedItem = null;
        permanentGUI.SetActive(false);
        skillUI.SetActive(true);
        questUI.SetActive(false);
        itemInfoInventory.SetActive(false);
        inventoryUI.SetActive(false);
        itemInfoInventory.SetActive(false);
        characterInfoInventory.SetActive(false);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
        statsUI.SetActive(false);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.Find("Experience").GetComponent<Text>().text = "Experience:" + GameObject.Find("Player").GetComponent<PlayerController>().experience;
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.Find("ExperienceNextLevel").GetComponent<Text>().text = "Next level:" + GameObject.Find("Player").GetComponent<PlayerController>().experienceForNextLevel;
    }
    public void OpenInventory()
    {
        characterInfoInventory.transform.Find("HealthText").GetComponent<Text>().text = "Health:" + playerController.currentHealth + "/" + playerController.hpPlayer;
        characterInfoInventory.transform.Find("StaminaText").GetComponent<Text>().text = "Stamina:" + playerController.currentStamina + "/" + playerController.staminaPlayer;
        characterInfoInventory.transform.Find("ManaText").GetComponent<Text>().text = "Mana:" + playerController.currentMana + "/" + playerController.manaPlayer;
        characterInfoInventory.transform.Find("LevelText").GetComponent<Text>().text = "Level:" + playerController.level;
        characterInfoInventory.transform.Find("PrestigeText").GetComponent<Text>().text = "Prestige:" + playerController.prestige;
        characterInfoInventory.transform.Find("GoldText").GetComponent<Text>().text = "Gold:" + playerController.gold;
        characterInfoInventory.transform.Find("ArmorText").GetComponent<Text>().text = "Armor:" + playerController.armor;
        GameObject.Find("SkillManager").GetComponent<SkillManager>().selectedItem = null;
        skillUI.SetActive(false);
        permanentGUI.SetActive(true);
        questUI.SetActive(false);
        inventoryUI.SetActive(true);
        itemInfoInventory.SetActive(false);
        characterInfoInventory.SetActive(true);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
        statsUI.SetActive(false);
    }
    public void CloseLootWindow()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot = null;
        lootWindow.SetActive(false);
        if (!GameObject.Find("Player").GetComponent<Animator>().GetBool("IsStunned"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<CameraMovement>().enabled = true;
        }
        itemInfoInventory.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem.GetComponent<Image>().color = Color.white;
        GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem = timeImageGameObject.gameObject;
        GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(true);
        control.SetActive(true);
    }
    public void CloseCookWindow()
    {
        playerController.enabled = true;
        cameraMovement.enabled = true;
        enabled = true;
        GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = true;
        cookWindow.SetActive(false);
        personName.SetActive(true);
        enemyHPPlayer.SetActive(true);
        compass.SetActive(true);
        GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(true);
    }
    public void CloseAlchemyWindow()
    {
        playerController.enabled = true;
        cameraMovement.enabled = true;
        enabled = true;
        GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = true;
        alchemyWindow.SetActive(false);
        personName.SetActive(true);
        enemyHPPlayer.SetActive(true);
        compass.SetActive(true);
        GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(true);
    }
    public void MarketWindowControl()
    {
        if (!shop.activeSelf)
        {
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogue.personName == "Merchant")
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                    thiefShop.SetActive(true);
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                    paladinShop.SetActive(true);
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                    mageShop.SetActive(true);
                hunterShop.SetActive(false);
            }
            else
            {
                    thiefShop.SetActive(false);
                    paladinShop.SetActive(false);
                    mageShop.SetActive(false);
                hunterShop.SetActive(true);
            }
            shop.SetActive(true);
            inventoryUI.SetActive(true);
            dialogueUI.SetActive(false);
            inventoryUI.transform.Find("Use").gameObject.SetActive(false);
            inventoryUI.transform.Find("Drop").gameObject.SetActive(false);
            inventoryUI.transform.Find("Buy").gameObject.SetActive(true);
            inventoryUI.transform.Find("Back").gameObject.SetActive(true);
        }
    }
    public void CreateSaveSlot(string path)
    {
        GameObject spawn=Instantiate(saveSlotPrefab, saveSlotsList);
        spawn.GetComponentInChildren<TextMeshProUGUI>().text = "Save "+SaveLoad.numberOfSave;
        spawn.GetComponent<SaveSlot>().path = path;
        spawn.GetComponent<SaveSlot>().index = SaveLoad.numberOfSave;
        spawn.GetComponent<Button>().onClick.AddListener(()=>SelectSaveSlot());
        GameObject addedSaveSlot = Instantiate(saveSlotPrefab);
        addedSaveSlot.GetComponentInChildren<TextMeshProUGUI>().text = "Save " + SaveLoad.numberOfSave;
       addedSaveSlot.GetComponent<SaveSlot>().path = path;
        addedSaveSlot.GetComponent<SaveSlot>().index = SaveLoad.numberOfSave;
        SaveLoad.listOfSave.Add(addedSaveSlot);
        SaveLoad.numberOfSave++;
        DontDestroyOnLoad(addedSaveSlot);
    }
    public void ClearSaveGUI()
    {
        for(int i=0; i < saveSlotsList.childCount; i++)
        {
            Destroy(saveSlotsList.transform.GetChild(i).gameObject);
        }
    }
    public void ClearLoadGUI()
    {
        for (int i = 0; i < loadSlotList.childCount; i++)
        {
            Destroy(loadSlotList.transform.GetChild(i).gameObject);
        }
    }
    public void FillSaveList()
    {
     foreach(GameObject objectToSpawn in SaveLoad.listOfSave)
        {
           GameObject spawn= Instantiate(objectToSpawn, saveSlotsList);
           spawn.GetComponent<Button>().onClick.AddListener(() => SelectSaveSlot());
        }
    }
    public void FillLoadList()
    {
        for (int i = 0; i < SaveLoad.listOfSave.Count; i++)
        {
            GameObject spawn=Instantiate(SaveLoad.listOfSave[i], loadSlotList);
          spawn.GetComponent<Button>().onClick.AddListener(() => SelectSaveSlot()); 
        }
    }
    public void FillDeadLoadList()
    {
        for (int i = 0; i < SaveLoad.listOfSave.Count; i++)
        {
            GameObject spawn = Instantiate(SaveLoad.listOfSave[i], deadLoadSlotList);
            spawn.GetComponent<Button>().onClick.AddListener(() => SelectSaveSlot());
        }
    }
    public void ClearDeadLoadGUI()
    {
        for (int i = 0; i < deadLoadSlotList.childCount; i++)
        {
            Destroy(deadLoadSlotList.transform.GetChild(i).gameObject);
        }
    }
    public void SelectSaveSlot()
    {
        if (selectedSaveSlot != null)
            selectedSaveSlot.GetComponent<Image>().color = Color.white;
        selectedSaveSlot = EventSystem.current.currentSelectedGameObject;
        selectedSaveSlot.GetComponent<Image>().color = Color.green;

    }
    public void DeleteSaveSlot()
    {
        if (selectedSaveSlot != null)
        {
            foreach (GameObject saveSlot in SaveLoad.listOfSave.ToArray())
                if (saveSlot.GetComponent<SaveSlot>().path == selectedSaveSlot.GetComponent<SaveSlot>().path)
                    SaveLoad.listOfSave.Remove(saveSlot);
           for(int i=0;i< GameObject.FindObjectsOfType<SaveSlot>().Length; i++)
                if(GameObject.FindObjectsOfType<SaveSlot>()[i].path==selectedSaveSlot.GetComponent<SaveSlot>().path)
                    Destroy(GameObject.FindObjectsOfType<SaveSlot>()[i].gameObject);
                       Destroy(selectedSaveSlot);
        }
    }
    public void OverwriteSaveSlot()
    {
        if (selectedSaveSlot != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saves/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filePath = selectedSaveSlot.GetComponent<SaveSlot>().path;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                PlayerData playerData = new PlayerData(GameObject.Find("Player").GetComponent<PlayerController>());
                formatter.Serialize(fileStream, playerData);
            }
        }
    }
    public void SetSelectedSaveSlotNull()
    {
        selectedSaveSlot = null;
    }
    public void SaveSavesAndSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string filePath = Application.persistentDataPath + "/saves/" + "-1.txt";
        File.Delete(Application.persistentDataPath + "/saves/" +  "-1.txt");
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {

            SaveAndLoadData saveData = new SaveAndLoadData();
            for (int i = 0; i < GameObject.Find("Container for list").transform.childCount; i++)
            {
                saveData.path.Add(GameObject.Find("Container for list").transform.GetChild(i).GetComponent<SaveSlot>().path);
                saveData.index.Add(GameObject.Find("Container for list").transform.GetChild(i).GetComponent<SaveSlot>().index);
                saveData.numberOfSave = SaveLoad.numberOfSave;
            }
            formatter.Serialize(fileStream, saveData);
        }
    }
    public void FillSettings()
    {
        if (SaveLoad.globalSettingsData != null)
        {
            masterVolume.value = SaveLoad.globalSettingsData.masterVolume;
            effectsVolume.value = SaveLoad.globalSettingsData.effectsVolume;
            musicVolume.value = SaveLoad.globalSettingsData.musicVolume;
        }
    }
}
