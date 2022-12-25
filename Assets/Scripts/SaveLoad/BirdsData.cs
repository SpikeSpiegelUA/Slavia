using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdsData
{
    public string[] ID;
    public float[,] position;
    public float[,] rotation;
    public BirdsData()
    {
        ID = new string[GameObject.FindObjectsOfType<BirdAI>().Length];
        position = new float[GameObject.FindObjectsOfType<BirdAI>().Length, 3];
        for (int i = 0; i < GameObject.FindObjectsOfType<BirdAI>().Length; i++)
        {
            ID[i] = GameObject.FindObjectsOfType<BirdAI>()[i].ID;
            position[i, 0] = GameObject.FindObjectsOfType<BirdAI>()[i].transform.position.x;
            position[i, 1] = GameObject.FindObjectsOfType<BirdAI>()[i].transform.position.y;
            position[i, 2] = GameObject.FindObjectsOfType<BirdAI>()[i].transform.position.z;
        }
    }
}
