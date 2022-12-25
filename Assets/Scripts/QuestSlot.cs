using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSlot : MonoBehaviour
{
    public GameObject objectReward;
    public int goldReward;
    public int experienceReward;
    public int prestigeReward;
    public string description;
    public string questName;
    public List<string> goal;
    public int questStage = 0;
    public int skillPoints;
}
