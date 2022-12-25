using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
[System.Serializable]
public static class SaveLoad
{
    public static List<GameObject> listOfSave = new List<GameObject>();
    public static int numberOfSave = 0;
    public static bool isLoading;
    public static PlayerData globalPlayerData;
    public static SettingsData globalSettingsData;
    public static GuardData globalGuardData;
    public static CivilianData globalCivilianData;
    public static ItemData globalItemData;
    public static LootCrateData globalLootCrateData;
    public static InventoryData globalInventoryData;
    public static SkillData globalSkillData;
    public static QuestsData globalQuestsData;
    public static DialogueManagerData globalDialogueManagerData;
    public static SpecialData globalSpecialData;
    public static GameManagerData globalGameManagerData;
    public static AnimalData globalAnimalData;
    public static BirdsData globalBirdsData;
    public static CloudData globalCloudData;
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string filePath = Application.persistentDataPath + "/saves/" + numberOfSave + ".txt";
        File.Delete(Application.persistentDataPath + "/saves/" + numberOfSave + ".txt");
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
                GameObject.Find("GUIManager").GetComponent<GUIController>().CreateSaveSlot(filePath);
            AllDatasType allType = new AllDatasType();
            PlayerData playerData = new PlayerData(GameObject.Find("Player").GetComponent<PlayerController>());
            CivilianData civilianData = new CivilianData();
            GuardData guardData = new GuardData();
            ItemData itemData = new ItemData();
            LootCrateData lootCrateData = new LootCrateData();
            InventoryData inventoryData = new InventoryData();
            SkillData skillData = new SkillData();
            QuestsData questsData = new QuestsData();
            DialogueManagerData dialogueManagerData = new DialogueManagerData();
            SpecialData specialData = new SpecialData();
            GameManagerData gameManagerData = new GameManagerData();
            AnimalData animalData = new AnimalData();
            BirdsData birdsData = new BirdsData();
            CloudData cloudData = new CloudData();
            allType.animalData = animalData;
            allType.itemData = itemData;
            allType.inventoryData = inventoryData;
            allType.skillData = skillData;
            allType.playerData = playerData;
            allType.guardData = guardData;
            allType.civilianData = civilianData;
            allType.lootCrateData = lootCrateData;
            allType.questsData = questsData;
            allType.dialogueManagerData = dialogueManagerData;
            allType.specialData = specialData;
            allType.gameManagerData = gameManagerData;
            allType.birdsData = birdsData;
            allType.cloudData = cloudData;
            formatter.Serialize(fileStream, allType);
        }
    }
    public static void Load(int numberOfSave)
    {
        isLoading = true;
        AllDatasType allType;
    BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/saves/" + numberOfSave + ".txt";
        if (File.Exists(filePath))
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
            allType = (AllDatasType)formatter.Deserialize(fileStream);
                globalInventoryData = allType.inventoryData;
                globalPlayerData = allType.playerData;
                globalGuardData = allType.guardData;
                globalCivilianData = allType.civilianData;
                globalItemData = allType.itemData;
                globalLootCrateData = allType.lootCrateData;
                globalSkillData = allType.skillData;
                globalQuestsData = allType.questsData;
                globalDialogueManagerData = allType.dialogueManagerData;
                globalSpecialData = allType.specialData;
                globalGameManagerData = allType.gameManagerData;
                globalAnimalData = allType.animalData;
                globalBirdsData = allType.birdsData;
                globalCloudData = allType.cloudData;
            }
        }    
    }
    public static void LoadSettings()
    {
        SettingsData settingsDataThis = default(SettingsData);
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/saves/" + "-2.txt";
        if (File.Exists(filePath))
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                settingsDataThis= (SettingsData)formatter.Deserialize(fileStream);
                globalSettingsData = settingsDataThis;
                GameObject.Find("Canvas").GetComponent<MainMenu>().LoadSettings(settingsDataThis);
            }
        }
    }
    public static void LoadSaves()
    {
        SaveAndLoadData saveData = default(SaveAndLoadData);
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/saves/"+"-1.txt";
        if (File.Exists(filePath))
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
               saveData = (SaveAndLoadData)formatter.Deserialize(fileStream);
                GameObject.Find("Canvas").GetComponent<MainMenu>().LoadSaves(saveData);
            }
        }
    }
}
