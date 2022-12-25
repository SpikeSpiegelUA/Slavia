using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    public PlayerController player;
    //Set max value of mana slider
    private void Start()
    {
        manaBar = GameObject.Find("ManaBar").GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void SetMaxValue()
    {
        manaBar.maxValue = player.manaPlayer;
        manaBar.value =player.manaPlayer;
    }
    //Set current mana value
    public void SetMana()
    {
        manaBar.value = player.currentMana;
    }
}
