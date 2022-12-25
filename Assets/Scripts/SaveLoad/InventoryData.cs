using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryData
{
    public string[] itemName;
    public int[] amountOfItems;
    public string shieldName;
    public string helmetName;
    public string armorName;
    public string weaponName;
    public bool potionAttackActivated;
    public int potionAttackTime;
    public bool potionArmorActivated;
    public int potionArmorTime;
    public int pickpocketingPotionTime;
    public bool pickpocketingPotionActivated;
    public int breakingPotionTime = 60;
    public bool breakingPotionActivated = false;
    public int warriorPotionTime;
    public bool warriorPotionActivated;
    public int archimagePotionTime;
    public bool archimagePotionActivated;
    public int runnerPotionTime = 60;
    public bool runnerPotionActivated = false;
    public bool hasArrows = false;
    public InventoryData()
    {
        itemName = new string[60];
        amountOfItems = new int[60];
        hasArrows = GameObject.Find("GUIManager").GetComponent<Inventory>().hasArrows;
        for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
        {
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
            {
                itemName[i] = GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].gameObject.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName;
                amountOfItems[i] = GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems;
            }
        }
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
            weaponName = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName;
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item != null)
            shieldName = GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName;
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item != null)
            helmetName = GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName;
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
            armorName = GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName;
        potionAttackActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().potionAttackActivated;
        potionAttackTime = GameObject.Find("GUIManager").GetComponent<Inventory>().attackPotionTime;
        potionArmorActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().potionArmorActivated;
        potionArmorTime = GameObject.Find("GUIManager").GetComponent<Inventory>().potionArmorTime;
        pickpocketingPotionActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotionActivated;
        pickpocketingPotionTime = GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotionTime;
        breakingPotionTime = GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotionTime;
        breakingPotionActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotionActivated;
        warriorPotionTime = GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotionTime;
        warriorPotionActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotionActivated;
        archimagePotionActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotionActivated;
        archimagePotionTime = GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotionTime;
        runnerPotionActivated = GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotionActivated;
        runnerPotionTime = GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotionTime;
    }
}
