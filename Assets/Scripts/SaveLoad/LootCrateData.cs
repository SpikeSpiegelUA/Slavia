using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LootCrateData
{
    public string[,] itemName;
    public int[,] amountOfItems;
    public int[] amountOfGold;
    public bool[] isClose;
    public string[] ID;
    public LootCrateData()
    {
        ID = new string[GameObject.FindObjectsOfType<LootCrate>().Length];
        itemName = new string[GameObject.FindObjectsOfType<LootCrate>().Length, 6];
        amountOfItems = new int[GameObject.FindObjectsOfType<LootCrate>().Length, 6];
        amountOfGold = new int[GameObject.FindObjectsOfType<LootCrate>().Length];
        isClose = new bool[GameObject.FindObjectsOfType<LootCrate>().Length];
        for (int i = 0; i < GameObject.FindObjectsOfType<LootCrate>().Length; i++)
        {
                ID[i] = GameObject.FindObjectsOfType<LootCrate>()[i].ID;
                amountOfGold[i] = GameObject.FindObjectsOfType<LootCrate>()[i].amountOfGold;
                isClose[i] = GameObject.FindObjectsOfType<LootCrate>()[i].isClose;
                for (int b = 0; b < GameObject.FindObjectsOfType<LootCrate>()[i].loot.Length; b++)
                {
                if (GameObject.FindObjectsOfType<LootCrate>()[i].loot[b] != null)
                {
                    itemName[i, b] = GameObject.FindObjectsOfType<LootCrate>()[i].loot[b].GetComponent<Item>().itemName;
                    amountOfItems[i, b] = GameObject.FindObjectsOfType<LootCrate>()[i].amountOfItems[b];
                }
                }
        }
    }
}
