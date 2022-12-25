using UnityEngine;
using System;
[Serializable]
public class Quest
{
    public GameObject objectReward;
    public int goldReward;
    public int experienceReward;
    public int prestigeReward;
    public bool isActive;
    public string description;
    public string questName;
    public string[] goal;
    public int skillPoints;
    public QuestMarker currentQuestMarker;
    public bool questCompleted = false;
}
