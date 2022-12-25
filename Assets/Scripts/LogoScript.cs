using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("LoadMenu");
    }
    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
