using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData
{
    public string[] itemName;
    public float[,] itemPosition;
    public float[,] itemRotation;
    public string[] rootName;
    public bool[] canDamage;
    public string[] parentObject;
    public bool[] hasRigidbody;
    public ItemData()
    {
        itemName = new string[GameObject.FindObjectsOfType<Item>().Length];
        itemPosition = new float[GameObject.FindObjectsOfType<Item>().Length, 3];
        itemRotation = new float[GameObject.FindObjectsOfType<Item>().Length, 3];
        rootName = new string[GameObject.FindObjectsOfType<Item>().Length];
        canDamage = new bool[GameObject.FindObjectsOfType<Item>().Length];
        parentObject = new string[GameObject.FindObjectsOfType<Item>().Length];
        hasRigidbody = new bool[GameObject.FindObjectsOfType<Item>().Length];
        for (int i = 0; i < GameObject.FindObjectsOfType<Item>().Length; i++)
        {
            itemName[i] = GameObject.FindObjectsOfType<Item>()[i].itemName;
            itemPosition[i, 0] = GameObject.FindObjectsOfType<Item>()[i].transform.position.x;
            itemPosition[i, 1] = GameObject.FindObjectsOfType<Item>()[i].transform.position.y;
            itemPosition[i, 2] = GameObject.FindObjectsOfType<Item>()[i].transform.position.z;
            itemRotation[i, 0] = GameObject.FindObjectsOfType<Item>()[i].transform.eulerAngles.x;
            itemRotation[i, 1] = GameObject.FindObjectsOfType<Item>()[i].transform.eulerAngles.y;
            itemRotation[i, 2] = GameObject.FindObjectsOfType<Item>()[i].transform.eulerAngles.z;
            if (GameObject.FindObjectsOfType<Item>()[i].itemName == "Arrow")
            {
                canDamage[i] = GameObject.FindObjectsOfType<Item>()[i].GetComponent<Arrow>().canDamage;
                if (GameObject.FindObjectsOfType<Item>()[i].GetComponent<Arrow>().canDamage)
                {
                    if (GameObject.FindObjectsOfType<Item>()[i].GetComponentInParent<GuardAI>() != null)
                    {
                        parentObject[i] = GameObject.FindObjectsOfType<Item>()[i].transform.parent.name;
                        rootName[i] = GameObject.FindObjectsOfType<Item>()[i].transform.root.name;
                    }
                }
            }
            if (GameObject.FindObjectsOfType<Item>()[i].itemName == "Hell mushroom" || GameObject.FindObjectsOfType<Item>()[i].itemName == "Slavia mushroom")
                if (GameObject.FindObjectsOfType<Item>()[i].GetComponent<Rigidbody>() != null)
                    hasRigidbody[i] = true;
                else
                    hasRigidbody[i] = false;
            }
    }
}
