using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CivilianData
{
    public string[] ID;
    public string[] attackerID;
    public bool[] hasBeenAttacked;
    public bool[] guardFound;
    public float[,] startPosition;
    public float[,] startRotation;
    public int[] currentHP;
    public int[] detection;
    public bool[] plusToCount;
    public string[] guardToRunID;
    public bool[] foundWay;
    public float[,] position;
    public float[,] rotation;
    public bool[] cameDestination;
    public int[] indexPoint;
    public float[,] destination;
    //Loot variables
    public string[,] itemName;
    public int[,] amountOfItems;
    public int[] amountOfGold;
    public CivilianData()
    {
        itemName = new string[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 6];
        amountOfItems = new int[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 6];
        amountOfGold = new int[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        cameDestination = new bool[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        indexPoint = new int[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        ID = new string[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        attackerID = new string[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        hasBeenAttacked = new bool[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        guardFound = new bool[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        startPosition = new float[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 3];
        startRotation = new float[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 3];
        currentHP = new int[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        detection = new int[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        plusToCount = new bool[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        guardToRunID = new string[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        foundWay = new bool[Resources.FindObjectsOfTypeAll<CivilianAI>().Length];
        position = new float[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 3];
        rotation = new float[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 3];
        destination = new float[Resources.FindObjectsOfTypeAll<CivilianAI>().Length, 3];
        for(int i=0;i< Resources.FindObjectsOfTypeAll(typeof(CivilianAI)).Length; i++)
        {
            ID[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].ID;
            destination[i, 0] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.x;
            destination[i, 1] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.y;
            destination[i, 2] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.z;
            if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].attacker != null)
            {
                if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].attacker.GetComponent<PlayerController>() != null)
                    attackerID[i] = "Player";
                if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].attacker.GetComponent<GuardAI>() != null)
                    attackerID[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].attacker.GetComponent<GuardAI>().ID;
                if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].attacker.GetComponent<SummonedAI>() != null)
                    attackerID[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].attacker.GetComponent<SummonedAI>().ID;
            }
            if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<Loot>() != null)
            {
                amountOfGold[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<Loot>().amountOfGold;
                for (int b = 0; b < Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<Loot>().loot.Length; b++)
                {
                    if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<Loot>().loot[b] != null)
                    {
                        itemName[i, b] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<Loot>().loot[b].GetComponent<Item>().itemName;
                        amountOfItems[i, b] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].GetComponent<Loot>().amountOfItems[b];
                    }
                }
            }
            hasBeenAttacked[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].hasBeenAttacked;
            guardFound[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].guardFound;
            startPosition[i, 0] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].startPosition.x;
            startPosition[i, 1] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].startPosition.y;
            startPosition[i, 2] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].startPosition.z;
            startRotation[i, 0] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].startRotation.x;
            startRotation[i, 1] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].startRotation.y;
            startRotation[i, 2] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].startRotation.z;
            currentHP[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].currentHP;
            detection[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].detection;
            plusToCount[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].plusToCount;
            if(Resources.FindObjectsOfTypeAll<CivilianAI>()[i].guardToRun!=null)
            guardToRunID[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].guardToRun.GetComponent<GuardAI>().ID;
            foundWay[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].foundWay;
            position[i, 0] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].transform.position.x;
            position[i, 1] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].transform.position.y;
            position[i, 2] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].transform.position.z;
            rotation[i, 0] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].transform.eulerAngles.x;
            rotation[i, 1] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].transform.eulerAngles.y;
            rotation[i, 2] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].transform.eulerAngles.z;
            if (Resources.FindObjectsOfTypeAll<CivilianAI>()[i].gameObject.GetComponent<CitizenAIPatrol>() != null)
            {
                cameDestination[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].gameObject.GetComponent<CitizenAIPatrol>().cameDestination;
                indexPoint[i] = Resources.FindObjectsOfTypeAll<CivilianAI>()[i].gameObject.GetComponent<CitizenAIPatrol>().indexPoint;
            }
        }
    }
}
