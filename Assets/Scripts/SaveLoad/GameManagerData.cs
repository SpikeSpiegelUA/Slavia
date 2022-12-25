using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameManagerData
{
    public bool villageAttackedByPlayer;
    public bool banditsAttackedByPlayer;
    public bool isPaladin;
    public bool isMage;
    public bool isRobber;
    public bool withQuestMarkers;
    public int timeCloudsSpawn;
    public int killedDungeon;
    public bool headOfBanditKilled = false;
    public bool rightHandBanditKilled = false;
    public GameManagerData()
    {
        villageAttackedByPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer;
        isPaladin = GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin;
        isMage = GameObject.Find("GameManager").GetComponent<GameManager>().isMage;
        isRobber = GameObject.Find("GameManager").GetComponent<GameManager>().isRobber;
        withQuestMarkers = GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers;
        timeCloudsSpawn = GameObject.Find("GameManager").GetComponent<GameManager>().timeToCreateCloud;
        killedDungeon = GameObject.Find("GameManager").GetComponent<GameManager>().killedDungeon;
        headOfBanditKilled = GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled;
        rightHandBanditKilled = GameObject.Find("GameManager").GetComponent<GameManager>().rightHandBanditKilled;
    }
}
