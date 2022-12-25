using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CloudData 
{
    public float[,] position;
    public float[,] rotation;
    public string[] ID;
    public CloudData()
    {
        position = new float[GameObject.FindObjectsOfType<CloudAI>().Length, 3];
        rotation = new float[GameObject.FindObjectsOfType<CloudAI>().Length, 3];
        ID = new string[GameObject.FindObjectsOfType<CloudAI>().Length];
        for (int i = 0; i < GameObject.FindObjectsOfType<CloudAI>().Length; i++)
        {
            ID[i] = GameObject.FindObjectsOfType<CloudAI>()[i].ID;
            position[i, 0] = GameObject.FindObjectsOfType<CloudAI>()[i].transform.position.x;
            position[i, 1] = GameObject.FindObjectsOfType<CloudAI>()[i].transform.position.y;
            position[i, 2] = GameObject.FindObjectsOfType<CloudAI>()[i].transform.position.z;
            rotation[i, 0] = GameObject.FindObjectsOfType<CloudAI>()[i].transform.eulerAngles.x;
            rotation[i, 1] = GameObject.FindObjectsOfType<CloudAI>()[i].transform.eulerAngles.y;
            rotation[i, 2] = GameObject.FindObjectsOfType<CloudAI>()[i].transform.eulerAngles.z;
        }
    }
}
