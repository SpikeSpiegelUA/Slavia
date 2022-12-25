using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public PlayerController player;
    //Set max value of stamina slider
    private void Start()
    {
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void SetMaxValue()
    {
        staminaBar.maxValue = player.staminaPlayer;
        staminaBar.value = player.staminaPlayer;
    }
    //Set current value of stamina slider
    public void SetStamina()
    {
        staminaBar.value = player.currentStamina;
    }
}
