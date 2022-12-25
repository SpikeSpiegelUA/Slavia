using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BirdAI : MonoBehaviour
{
    public string ID;
    private bool hasPlace = false;
    private Vector3 destination;
    void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
        if (SaveLoad.isLoading)
            LoadData();
    }
    void Update()
    {
        if (!hasPlace)
        {
            float positionX = Random.Range(-160, 240);
            float positionZ = Random.Range(-192, 233);
            hasPlace = true;
            destination = new Vector3(positionX, transform.position.y, positionZ);
        }
        if ((transform.position - destination).magnitude <= 5)
            hasPlace = false;
        transform.position = Vector3.MoveTowards(transform.position, destination, 5*Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(destination-transform.position);
    }
    private void LoadData()
    {
        BirdsData birdsData = SaveLoad.globalBirdsData;
        for(int i = 0; i < birdsData.ID.Length; i++)
            if (birdsData.ID[i] == ID)
                transform.position = new Vector3(birdsData.position[i,0], birdsData.position[i,1], birdsData.position[i,2]);
    }
}
