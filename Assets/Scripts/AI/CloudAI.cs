using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAI : MonoBehaviour
{
    public string ID;
   private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }
    private void Start()
    {
        if (SaveLoad.isLoading)
            LoadCloudData();
    }
    void Update()
    {
        if (transform.position.x <= -666)
            Destroy(gameObject);
        transform.position=Vector3.MoveTowards(transform.position, new Vector3(-666, transform.position.y, transform.position.z), 3 * Time.deltaTime);
    }
    private void LoadCloudData()
    {
        CloudData cloudData = SaveLoad.globalCloudData;
        for (int i = 0; i < cloudData.ID.Length; i++)
        {
            if (ID == cloudData.ID[i])
            {
                transform.position = new Vector3(cloudData.position[i, 0], cloudData.position[i, 1], cloudData.position[i, 2]);
                transform.eulerAngles = new Vector3(cloudData.rotation[i, 0], cloudData.rotation[i, 1], cloudData.rotation[i, 2]);
            }
        }
    }
}
