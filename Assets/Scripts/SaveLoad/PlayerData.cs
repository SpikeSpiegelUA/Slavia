using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int combatEnemies;
    public int currentHealth;
    public int currentMana;
    public int currentStamina;
    public int hpPlayer;
    public int manaPlayer;
    public int staminaPlayer;
    public bool isCrouched;
    public bool isDetected;
    public int stealthAttackModify;
    public int gold;
    public int chanceForGrab;
    public float armor;
    public int prestige;
    public int chanceForUnlock;
    public int skillPoints;
    public int experience;
    public int fireballDamageModify;
    public int recoverModify;
    public int level;
    public int experienceForNextLevel;
    public float[] position;
    public float[] cameraRotation;
    public float[] playerRotation;
    public float[] currentSummonedMeleePosition;
    public float[] currentSummonedMeleeRotation;
    public float[] currentSummonedArcherPosition;
    public float[] currentSummonedArcherRotation;
    public int currentSummonedMeleeCurrentHP;
    public int currentSummonedArcherCurrentHP;
    public PlayerData(PlayerController player)
    {
        combatEnemies = player.combatEnemies;
        currentHealth = player.currentHealth;
        currentMana = player.currentMana;
        currentStamina = player.currentStamina;
        hpPlayer = player.hpPlayer;
        manaPlayer = player.manaPlayer;
        staminaPlayer = player.staminaPlayer;
        isCrouched = player.isCrouched;
        isDetected = player.isDetected;
        stealthAttackModify = player.stealthAttackModify;
        gold= player.gold;
        chanceForGrab = player.chanceForGrab;
        armor = player.armor;
        prestige = player.prestige;
        chanceForUnlock = player.chanceForUnlock;
        skillPoints = player.skillPoints;
        experience = player.experience;
        fireballDamageModify = player.fireballDamageModify;
        recoverModify = player.recoverModify;
        level = player.level;
        experienceForNextLevel = player.experienceForNextLevel;
        position = new float[3];
        position[0] = player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
        position[2] = player.gameObject.transform.position.z;
        playerRotation = new float[3];
        playerRotation[0] = player.transform.eulerAngles.x;
        playerRotation[1] = player.transform.eulerAngles.y;
        playerRotation[2] = player.transform.eulerAngles.z;
        cameraRotation = new float[3];
        cameraRotation[0] = player.gameObject.GetComponentInChildren<CameraMovement>().gameObject.transform.rotation.x;
        cameraRotation[1] = player.gameObject.GetComponentInChildren<CameraMovement>().gameObject.transform.rotation.y;
        cameraRotation[2] = player.gameObject.GetComponentInChildren<CameraMovement>().gameObject.transform.rotation.z;
        if (player.currentSummonedArcher != null)
        {
            currentSummonedArcherPosition = new float[3];
            currentSummonedArcherRotation = new float[3];
            currentSummonedArcherPosition[0] = player.currentSummonedArcher.transform.position.x;
            currentSummonedArcherPosition[1] = player.currentSummonedArcher.transform.position.y;
            currentSummonedArcherPosition[2] = player.currentSummonedArcher.transform.position.z;
            currentSummonedArcherRotation[0] = player.currentSummonedArcher.transform.eulerAngles.x;
            currentSummonedArcherRotation[0] = player.currentSummonedArcher.transform.eulerAngles.x;
            currentSummonedArcherRotation[0] = player.currentSummonedArcher.transform.eulerAngles.x;
            currentSummonedArcherCurrentHP = player.currentSummonedArcher.GetComponent<SummonedAI>().currentHP;
        }
        if (player.currentSummonedMelee != null)
        {
            currentSummonedMeleePosition = new float[3];
            currentSummonedMeleeRotation = new float[3];
            currentSummonedMeleePosition[0] = player.currentSummonedMelee.transform.position.x;
            currentSummonedMeleePosition[1] = player.currentSummonedMelee.transform.position.y;
            currentSummonedMeleePosition[2] = player.currentSummonedMelee.transform.position.z;
            currentSummonedMeleeRotation[0] = player.currentSummonedMelee.transform.eulerAngles.x;
            currentSummonedMeleeRotation[1] = player.currentSummonedMelee.transform.eulerAngles.y;
            currentSummonedMeleeRotation[2] = player.currentSummonedMelee.transform.eulerAngles.z;
            currentSummonedMeleeCurrentHP = player.currentSummonedMelee.GetComponent<SummonedAI>().currentHP;
        }
    }
}
