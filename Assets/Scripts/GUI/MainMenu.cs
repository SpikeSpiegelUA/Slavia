using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    private GameObject selectedSaveSlot;
    public Transform loadSlotList;
    public GameObject saveSlotPrefab;
    public AudioMixer audioMixer;
    public TMP_Dropdown qualitySettingsDropdown;
    public Slider masterVolume;
    public Slider effectsVolume;
    public Slider musicVolume;
    public Toggle withQuestMarkersToggle;
    void Start()
    {
        SaveLoad.LoadSaves();
        SaveLoad.LoadSettings();
        GoogleAds.ShowBanner();
    }
    public void PlayGame()
    {
        GoogleAds.ShowInterpages();
        IEnumerator coroutine = GameObject.Find("Canvas").GetComponent<LoadingBar>().LoadMainScene(-19);
        StartCoroutine(coroutine);
    }
    public void FillSettings()
    {
        if (SaveLoad.globalSettingsData != null)
        {
            effectsVolume.value = SaveLoad.globalSettingsData.effectsVolume;
            musicVolume.value = SaveLoad.globalSettingsData.musicVolume;
            qualitySettingsDropdown.value = SaveLoad.globalSettingsData.qualityLevel;
            masterVolume.value = SaveLoad.globalSettingsData.masterVolume;
        }
    }
    public void SetIsRobber()
    {
        MenuVariables.isRobber = true;
        MenuVariables.isPaladin = false;
        MenuVariables.isMage =false;
    }
    public void SetIsPaladin()
    {
        MenuVariables.isPaladin = true;
        MenuVariables.isMage = false;
        MenuVariables.isRobber =false;
    }
    public void SetIsMage()
    {
        MenuVariables.isMage = true;
        MenuVariables.isRobber = false;
        MenuVariables.isPaladin = false;
    }
    public void FillLoadList()
    {
        for (int i = 0; i < SaveLoad.listOfSave.Count; i++)
        {
            GameObject spawn = Instantiate(SaveLoad.listOfSave[i], loadSlotList);
            spawn.GetComponent<Button>().onClick.AddListener(() => SelectSaveSlot());
        }
    }
    public void SelectSaveSlot()
    {
        if (selectedSaveSlot != null)
            selectedSaveSlot.GetComponent<Image>().color = Color.white;
        selectedSaveSlot = EventSystem.current.currentSelectedGameObject;
        selectedSaveSlot.GetComponent<Image>().color = Color.green;
    }
    public void ClearLoadGUI()
    {
        for (int i = 0; i < loadSlotList.childCount; i++)
        {
            Destroy(loadSlotList.transform.GetChild(i).gameObject);
        }
    }
    public void SetSelectedSaveSlotNull()
    {
        selectedSaveSlot = null;
    }
    public void Load()
    {
        GoogleAds.ShowInterpages();
        IEnumerator coroutine = null;
        if (SceneManager.GetActiveScene().name == "Menu")
            if (selectedSaveSlot != null)
            {
                        if (GameObject.Find("Canvas") != null)
                            coroutine = GameObject.Find("Canvas").GetComponent<LoadingBar>().LoadMainScene(selectedSaveSlot.GetComponent<SaveSlot>().index);
                        StartCoroutine(coroutine);
            }
    }
    public  void LoadSaves(SaveAndLoadData saveData)
    {
        SaveLoad.listOfSave.Clear();
        SaveLoad.numberOfSave = saveData.numberOfSave;
          for(int i = 0; i < saveData.path.Count; i++)
        {
            bool exist = false;
            for (int b = 0; b < loadSlotList.transform.childCount; b++)
                if (loadSlotList.transform.GetChild(b).GetComponent<SaveSlot>().path == saveData.path[i])
                    exist = true;
                    if (!exist) {
                        GameObject addedSaveSlot = Instantiate(saveSlotPrefab);
                        addedSaveSlot.GetComponentInChildren<TextMeshProUGUI>().text = "Save " + saveData.index[i];
                        addedSaveSlot.GetComponent<SaveSlot>().path = saveData.path[i];
                        addedSaveSlot.GetComponent<SaveSlot>().index = saveData.index[i];
                        SaveLoad.listOfSave.Add(addedSaveSlot);
                        DontDestroyOnLoad(addedSaveSlot);
                    }
        }
    }
    public void LoadSettings(SettingsData settingsData)
    {
        if (settingsData != null)
        {
            QualitySettings.SetQualityLevel(settingsData.qualityLevel);
            audioMixer.SetFloat("Master volume", settingsData.masterVolume);
            audioMixer.SetFloat("Music volume", settingsData.musicVolume);
            audioMixer.SetFloat("Effects volume", settingsData.effectsVolume);
        }
    }
    public void WithQuestMarkersChange(bool isOn)
    {
        if (!isOn)
            MenuVariables.withQuestMarkers = false;
        if (isOn)
            MenuVariables.withQuestMarkers = true;
    }
    public void NewGameButtonActions()
    {
        MenuVariables.withQuestMarkers = withQuestMarkersToggle.isOn;
    }
}

