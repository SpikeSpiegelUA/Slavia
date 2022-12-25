using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : MonoBehaviour
{
    public bool isClose;
    public GameObject[] loot;
    public int[] amountOfItems;
    public int amountOfGold;
    public string ID;
    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }
    private void Start()
    {
        if (SaveLoad.isLoading)
            LoadCrate();
    }
    private void LoadCrate()
    {
        LootCrateData lootCrate = SaveLoad.globalLootCrateData;
        for (int i = 0; i < lootCrate.ID.Length; i++)
        {
            if (ID == lootCrate.ID[i])
            {
                for (int b = 0; b < GetComponent<LootCrate>().loot.Length; b++)
                {
                    GetComponent<LootCrate>().loot[b] = null;
                    GetComponent<LootCrate>().amountOfItems[b] = 0;
                }
                for (int b = 0; b < GetComponent<LootCrate>().loot.Length; b++)
                {
                    GetComponent<LootCrate>().loot[b] = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(lootCrate.itemName[i, b]);
                    GetComponent<LootCrate>().amountOfItems[b] = lootCrate.amountOfItems[i, b];
                }
                GetComponent<LootCrate>().amountOfGold = lootCrate.amountOfGold[i];
                amountOfGold = lootCrate.amountOfGold[i];
                isClose = lootCrate.isClose[i];
            }
        }
    }
}
