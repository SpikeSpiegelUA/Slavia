using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public float effectVolumeBeforeMute;
    public void Pause()
    {
        if (!GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened&&!GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueIsOpen&&!gameIsPaused)
        {
            GameObject.Find("GUIManager").GetComponent<GUIController>().blurScreen.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<GUIController>().pauseMenu.SetActive(true);
            GameObject.Find("Music").GetComponent<AudioSource>().pitch *= 0.5f;
            Time.timeScale = 0f;
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Player").GetComponentInChildren<CameraMovement>().enabled = false;
            gameIsPaused = true;
            Resources.FindObjectsOfTypeAll<SettingsMenu>()[0].masterMixer.GetFloat("Effects volume", out effectVolumeBeforeMute);
            Resources.FindObjectsOfTypeAll<SettingsMenu>()[0].masterMixer.SetFloat("Effects volume", -80);
            GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.newLevel.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().personName.GetComponent<Image>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().stealtBar.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().personName.GetComponent<Image>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().personName.GetComponentInChildren<Text>().text = "";
            GameObject.Find("GUIManager").GetComponent<GUIController>().enemyHPPlayer.GetComponent<Image>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().enemyHPPlayer.GetComponentInChildren<Text>().text = "";
            GameObject.Find("GUIManager").GetComponent<GUIController>().compass.SetActive(false);
            GetComponent<Inventory>().inventoryIsFull.SetActive(false);
            GetComponent<Inventory>().needMoreSpace.SetActive(false);
            GetComponent<Inventory>().needMoreMoney.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(false);
GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().questFailed.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().itemInfoRay.gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().locked.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().enoughMana.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().needPicklock.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().pickpocketingWindow.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().unlockFailed.SetActive(false);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().newStatLevel.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().killExperience.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().needToBeRobber.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().pointer.SetActive(false);
        }
      else  if (!GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened && !GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueIsOpen && gameIsPaused)
        {
            GameObject.Find("GUIManager").GetComponent<GUIController>().blurScreen.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().pauseMenu.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().settingsMenu.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().saveMenu.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().loadMenu.SetActive(false);
            GameObject.Find("GUIManager").GetComponent<GUIController>().compass.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<GUIController>().ClearLoadGUI();
            GameObject.Find("GUIManager").GetComponent<GUIController>().ClearSaveGUI();
            Time.timeScale = 1f;
            GameObject.Find("Music").GetComponent<AudioSource>().pitch *= 2;
            gameIsPaused = false;
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Player").GetComponentInChildren<CameraMovement>().enabled = true;
            Resources.FindObjectsOfTypeAll<SettingsMenu>()[0].masterMixer.SetFloat("Effects volume", effectVolumeBeforeMute);
            GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<GUIController>().pointer.SetActive(true);
        }
    }
    public void BackToMenu()
    {
        if (GameObject.Find("Canvas") != null)
            GameObject.Find("Canvas").GetComponent<MainSceneLoadingBar>().StartCoroutine("LoadMenu");
    }
    public void Save()
    {
        SaveLoad.Save();
    }
    public void Load()
    {
        GoogleAds.ShowInterpages();
        IEnumerator coroutine = null;
                    if (SceneManager.GetActiveScene().name == "MainScene")
            if (GameObject.Find("GUIManager").GetComponent<GUIController>().selectedSaveSlot != null)
            {
                if (GameObject.Find("Canvas") != null)
                    coroutine = GameObject.Find("Canvas").GetComponent<MainSceneLoadingBar>().LoadMainScene(GameObject.Find("GUIManager").GetComponent<GUIController>().selectedSaveSlot.GetComponent<SaveSlot>().index);
               else if (GameObject.Find("Canvas2") != null)
                    coroutine = GameObject.Find("Canvas2").GetComponent<MainSceneLoadingBar>().LoadMainScene(GameObject.Find("GUIManager").GetComponent<GUIController>().selectedSaveSlot.GetComponent<SaveSlot>().index);
                StartCoroutine(coroutine);
            }
    }
}
