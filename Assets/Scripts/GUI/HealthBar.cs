using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerController player;
    //Set max value of health slider
    private void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void SetMaxValue()
    {
        healthBar.maxValue = player.hpPlayer;
        healthBar.value = player.hpPlayer;
    }
    //Set current health value
    public void SetHealth()
    {
        healthBar.value = player.currentHealth;
    }
}
