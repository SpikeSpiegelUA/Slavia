using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalData
{
    public string[] ID;
    public int[] currentHP;
    public bool[] foundWay;
    public float[,] position;
    public float[,] rotation;
    public float[,] destination;
    //Loot variables
    public string[,] itemName;
    public int[,] amountOfItems;
    public AnimalData()
    {
        itemName = new string[Resources.FindObjectsOfTypeAll<AnimalAI>().Length, 6];
        amountOfItems = new int[Resources.FindObjectsOfTypeAll<AnimalAI>().Length, 6];
        ID = new string[Resources.FindObjectsOfTypeAll<AnimalAI>().Length];
        currentHP = new int[Resources.FindObjectsOfTypeAll<AnimalAI>().Length];
        foundWay = new bool[Resources.FindObjectsOfTypeAll<AnimalAI>().Length];
        position = new float[Resources.FindObjectsOfTypeAll<AnimalAI>().Length, 3];
        rotation = new float[Resources.FindObjectsOfTypeAll<AnimalAI>().Length, 3];
        destination = new float[Resources.FindObjectsOfTypeAll<AnimalAI>().Length, 3];
        for (int i = 0; i < Resources.FindObjectsOfTypeAll(typeof(AnimalAI)).Length; i++)
        {
            ID[i] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].ID;
            destination[i, 0] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.x;
            destination[i, 1] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.y;
            destination[i, 2] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination.z;
            if (Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<Loot>() != null)
            {
                for (int b = 0; b < Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<Loot>().loot.Length; b++)
                {
                    if (Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<Loot>().loot[b] != null)
                    {
                        itemName[i, b] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<Loot>().loot[b].GetComponent<Item>().itemName;
                        amountOfItems[i, b] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].GetComponent<Loot>().amountOfItems[b];
                    }
                }
            }
            currentHP[i] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].currentHP;
            foundWay[i] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].foundWay;
            position[i, 0] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].transform.position.x;
            position[i, 1] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].transform.position.y;
            position[i, 2] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].transform.position.z;
            rotation[i, 0] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].transform.eulerAngles.x;
            rotation[i, 1] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].transform.eulerAngles.y;
            rotation[i, 2] = Resources.FindObjectsOfTypeAll<AnimalAI>()[i].transform.eulerAngles.z;
        }
    }
}
