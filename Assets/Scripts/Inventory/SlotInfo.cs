using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInfo : MonoBehaviour
{
public int amountOfItems;
    public GameObject item;
    
    void Update()
    {
        ChangeItemNumber();
    }
    //Update number of items in inventory
    private void ChangeItemNumber()
    {
        if (tag != "ShopIcon")
        {
            if (amountOfItems > 1)
                gameObject.GetComponentInChildren<Text>().text = amountOfItems.ToString();
            else if (amountOfItems == 1)
                gameObject.GetComponentInChildren<Text>().text = "";
        }
    }
}
