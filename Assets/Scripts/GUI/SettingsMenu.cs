using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer masterMixer;
    public void SetMusicVolume(float volume)
    {
     masterMixer.SetFloat("Music volume", volume);
        SaveSettings();
    }
    public void SetEffectsVolume(float volume)
    {
        if(SceneManager.GetActiveScene().name=="MainScene")
        Resources.FindObjectsOfTypeAll<PauseMenu>()[0].effectVolumeBeforeMute = volume;
        if (SceneManager.GetActiveScene().name == "Menu")
            masterMixer.SetFloat("Effects volume", volume);
        SaveSettings();
    }
        public void SetMasterVolume(float volume)
    {
      masterMixer.SetFloat("Master volume", volume);
        SaveSettings();
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SaveSettings();
    }
    private void SaveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string filePath = Application.persistentDataPath + "/saves/" + "-2.txt";
        File.Delete(Application.persistentDataPath + "/saves/" + "-2.txt");
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            bool effectsVolume = false;
            SettingsData settingsData = new SettingsData();
            settingsData.qualityLevel = QualitySettings.GetQualityLevel();
            bool masterVolume = masterMixer.GetFloat("Master volume", out settingsData.masterVolume);
            bool musicVolume = masterMixer.GetFloat("Music volume", out settingsData.musicVolume);
            if(SceneManager.GetActiveScene().name=="MainScene")
            settingsData.effectsVolume = Resources.FindObjectsOfTypeAll<PauseMenu>()[0].effectVolumeBeforeMute;
            if (SceneManager.GetActiveScene().name == "Menu")
            effectsVolume = masterMixer.GetFloat("Effects volume", out settingsData.effectsVolume);
            SaveLoad.globalSettingsData = settingsData;
            formatter.Serialize(fileStream, settingsData);
        }
    }
}
