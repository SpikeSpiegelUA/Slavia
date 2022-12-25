using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneLoadingBar : MonoBehaviour
{
    public GameObject canvasLoadingScreen;
    public IEnumerator LoadMenu()
    {
        AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu");
        if (canvasLoadingScreen.transform.parent.gameObject.name=="Canvas")
        {
            canvasLoadingScreen.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().enabled = false;
            if (canvasLoadingScreen.transform.parent.Find("PauseMenu") != null)
                canvasLoadingScreen.transform.parent.Find("PauseMenu").gameObject.SetActive(false);
        }
        if (canvasLoadingScreen.transform.parent.gameObject.name=="Canvas2")
        {
            canvasLoadingScreen.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().enabled = false;
            if (canvasLoadingScreen.transform.parent.Find("DeathMenu") != null)
                canvasLoadingScreen.transform.parent.Find("DeathMenu").gameObject.SetActive(false);
        }
        while (!loadScene.isDone)
        {
            float progress = loadScene.progress / 0.9f;
            canvasLoadingScreen.transform.Find("LoadingSlider").GetComponent<UnityEngine.UI.Slider>().value = progress;
            yield return null;
        }
    }
    public IEnumerator LoadMainScene(int numberOfSave)
    {       
        if (canvasLoadingScreen.transform.parent.gameObject.name == "Canvas")
        {
            canvasLoadingScreen.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().enabled = false;
            if (canvasLoadingScreen.transform.parent.Find("LoadMenu") != null)
                canvasLoadingScreen.transform.parent.Find("LoadMenu").gameObject.SetActive(false);
        }
        if (canvasLoadingScreen.transform.parent.gameObject.name == "Canvas2")
        {
            canvasLoadingScreen.SetActive(true);
            GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = false;
            GameObject.Find("GUIManager").GetComponent<GUIController>().enabled = false;
            if (canvasLoadingScreen.transform.parent.Find("LoadMenu") != null)
                canvasLoadingScreen.transform.parent.Find("LoadMenu").gameObject.SetActive(false);
        }
        AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
        while (!loadScene.isDone)
        {
            float progress = loadScene.progress / 0.9f;
            canvasLoadingScreen.transform.Find("LoadingSlider").GetComponent<UnityEngine.UI.Slider>().value = progress;
            yield return null;
            if (loadScene.progress>0.8f)
                SaveLoad.Load(numberOfSave);
        }

    }
}
