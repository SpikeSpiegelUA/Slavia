using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoadingBar : MonoBehaviour
{
    public GameObject loadingScreen;
    public IEnumerator LoadMainScene(int numberOfSave)
    {
        loadingScreen.SetActive(true);
        if (loadingScreen.transform.parent.Find("ClassChoose") != null)
            if(loadingScreen.transform.parent.Find("ClassChoose").gameObject.activeSelf)
        {
            loadingScreen.transform.parent.Find("ClassChoose").gameObject.SetActive(false);
            AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
            while (!loadScene.isDone)
            {
                float progress = loadScene.progress / 0.9f;
                loadingScreen.transform.Find("LoadingSlider").GetComponent<UnityEngine.UI.Slider>().value = progress;
                yield return null;
            }
        }
        if (loadingScreen.transform.parent.Find("LoadMenu") != null)
            if(loadingScreen.transform.parent.Find("LoadMenu").gameObject.activeSelf)
        {
            loadingScreen.transform.parent.Find("LoadMenu").gameObject.SetActive(false);
            AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
            while (!loadScene.isDone)
            {
                float progress = loadScene.progress / 0.9f;
                loadingScreen.transform.Find("LoadingSlider").GetComponent<UnityEngine.UI.Slider>().value = progress;
                yield return null;
                    if (loadScene.progress > 0.8f)
                        SaveLoad.Load(numberOfSave);
                }
            }
    }
}
