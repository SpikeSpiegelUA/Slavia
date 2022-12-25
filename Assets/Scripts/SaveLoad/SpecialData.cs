using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpecialData 
{
    public int soloveyHP;
    public SpecialData()
    {
        if (GameObject.Find("Solovey") != null)
            if(GameObject.Find("Solovey").GetComponent<SummonedAI>()!=null)
            soloveyHP = GameObject.Find("Solovey").GetComponent<SummonedAI>().currentHP;
    }
}
