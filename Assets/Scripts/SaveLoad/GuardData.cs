using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuardData
{
    public string[] ID;
    public int[] currentHP;
    public float[,] startPosition;
    public float[,] startRotation;
    public bool[] plusToCount;
    public bool[] startCancelCoroutine;
    public int[] detection;
    public float[,] position;
    public float[,] rotation;
    public bool[] cameDestination;
    public float[,] destination;
    public int[] indexPoint;
    public string[] objectToAttackID;
    public float[,] mageSummonedPosition;
    public float[,] mageSummonedRotation;
    public string[] mageSummonedType;
    //Loot script variables
    public string[,] itemName;
    public int[,] amountOfItems;
    public int[] amountOfGold;
    //Summoned variables
    public int[] summonedCurrentHP;
    public bool[] summonedPlusToCount;
    public bool[] isAlerted;
    public GuardData()
    {
        isAlerted = new bool[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        summonedCurrentHP = new int[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        summonedPlusToCount = new bool[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        itemName = new string[Resources.FindObjectsOfTypeAll<GuardAI>().Length, 6];
        amountOfItems = new int[Resources.FindObjectsOfTypeAll<GuardAI>().Length, 6];
        amountOfGold = new int[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        ID = new string[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        currentHP = new int[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        startPosition = new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length,3];
        startRotation = new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length, 3];
        plusToCount = new bool[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        startCancelCoroutine = new bool[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        detection = new int[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        mageSummonedPosition=new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length,3];
        mageSummonedType = new string[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        objectToAttackID = new string[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        position = new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length, 3];
        rotation = new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length, 3];
        cameDestination = new bool[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        indexPoint = new int[Resources.FindObjectsOfTypeAll<GuardAI>().Length];
        mageSummonedRotation = new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length,3];
        destination = new float[Resources.FindObjectsOfTypeAll<GuardAI>().Length, 3];
        for (int i=0;i< Resources.FindObjectsOfTypeAll<GuardAI>().Length; i++)
        {
            if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<Loot>() != null)
            {
                amountOfGold[i]= Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<Loot>().amountOfGold;
                for (int b = 0; b < Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<Loot>().loot.Length; b++)
                {
                    if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<Loot>().loot[b] != null)
                    {
                        itemName[i, b] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<Loot>().loot[b].GetComponent<Item>().itemName;
                        amountOfItems[i, b] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<Loot>().amountOfItems[b];
                    }
                }
            }
            destination[i, 0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.x;
            destination[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.y;
            destination[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.z;
            ID[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].ID;
            isAlerted[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].isAlerted;
            currentHP[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].currentHP;
            startPosition[i, 0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startPosition.x;
            startPosition[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startPosition.y;
            startPosition[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startPosition.z;
            startRotation[i, 0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startRotation.x;
            startRotation[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startRotation.y;
            startRotation[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startRotation.z;
            position[i, 0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].transform.position.x;
            position[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].transform.position.y;
            position[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].transform.position.z;
            rotation[i, 0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].transform.eulerAngles.x;
            rotation[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].transform.eulerAngles.y;
            rotation[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].transform.eulerAngles.z;
            plusToCount[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].plusToCount;
            startCancelCoroutine[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].startCancelCoroutine;
            detection[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].detection;
            if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack != null) 
            {       
                if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<CivilianAI>() != null)
                    objectToAttackID[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<CivilianAI>().ID;
                if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<SummonedAI>() != null)
                    objectToAttackID[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<SummonedAI>().ID;
                if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<GuardAI>() != null)
                    objectToAttackID[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<GuardAI>().ID;
                if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].objectToAttack.GetComponent<PlayerController>() != null)
                    objectToAttackID[i] = "Player";
            }
            if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned != null) 
            {
                summonedPlusToCount[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.GetComponent<SummonedAI>().plusToCount;
                summonedCurrentHP[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.GetComponent<SummonedAI>().currentHP;
                mageSummonedPosition[i,0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.transform.position.x;
                mageSummonedPosition[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.transform.position.y;
                mageSummonedPosition[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.transform.position.z;
                mageSummonedRotation[i,0] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.transform.eulerAngles.x;
                mageSummonedRotation[i, 1] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.transform.eulerAngles.y;
                mageSummonedRotation[i, 2] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.transform.eulerAngles.z;
                if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.GetComponent<SummonedAI>().isArcher) 
                    mageSummonedType[i] = "Archer";
                if (!Resources.FindObjectsOfTypeAll<GuardAI>()[i].mageSummoned.GetComponent<SummonedAI>().isArcher)
                    mageSummonedType[i] = "Melee";
            }
            if (Resources.FindObjectsOfTypeAll<GuardAI>()[i].gameObject.GetComponent<CitizenAIPatrol>() != null)
            {
                cameDestination[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].gameObject.GetComponent<CitizenAIPatrol>().cameDestination;
                indexPoint[i] = Resources.FindObjectsOfTypeAll<GuardAI>()[i].gameObject.GetComponent<CitizenAIPatrol>().indexPoint;
            }
        }
    }
}
