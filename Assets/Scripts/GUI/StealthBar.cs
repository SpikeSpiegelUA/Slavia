using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StealthBar : MonoBehaviour
{
    private void Start()
    {
        SetMaxValue();
        StartCoroutine("HideBar");
    }
    //Set max value of stealth slider
    public void SetMaxValue()
    {
        this.GetComponent<Slider>().maxValue = 100;
        this.GetComponent<Slider>().value = 0;
    }
    //Hide bar when value was set
    private IEnumerator HideBar()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
