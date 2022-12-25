using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class QuestsData
{
    public string[] objectRewardName;
    public int[] goldReward;
    public int[] experienceReward;
    public int[] prestigeReward;
    public string[] description;
    public string[] questName;
    public string[,] goal;
    public int[] questStage;
    public int[] skillPoints;
    public bool[] isActive;
    public bool[] questCompleted;
    public bool soloveyKilled;
    public bool soloveyArrested;
    public bool soloveyRunnedAway;
    public bool soloveyBanditKilled;
    public bool[] questActivated;
    public int killedBanditsHeadOfGuardQuest;
    public int killedRoyalistsPatrol;
    public int killedRoyalists = 0;
    public int killedRepublicans;
    public int killedWarriors = 0;
    public QuestsData()
    {
        isActive = new bool[12];
        questActivated = new bool[12];
        objectRewardName = new string[12];
        goldReward = new int[12];
        experienceReward = new int[12];
        prestigeReward = new int[12];
        description = new string[12];
        questName = new string[12];
        goal = new string[12,15];
        questStage = new int[12];
        skillPoints = new int[12];
        questCompleted = new bool[12];
        soloveyKilled = GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled;
        soloveyArrested = GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested;
        soloveyRunnedAway = GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway;
        soloveyBanditKilled = GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyBanditKilled;
        killedBanditsHeadOfGuardQuest = GameObject.Find("QuestManager").GetComponent<QuestManager>().killedBanditsHeadOfGuardQuest;
        killedRoyalistsPatrol = GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRoyalistPatrol;
        killedRoyalists = GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRoyalists;
        killedRepublicans = GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRepublicans;
        killedWarriors = GameObject.Find("QuestManager").GetComponent<QuestManager>().killedWarriors;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
        {
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().objectReward!=null)
            objectRewardName[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().prestigeReward;
            description[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().description;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[1] = true;
            questName[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questName;
            for(int i=0;i< GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[1, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().goal[i];
            questStage[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().skillPoints;
        }
        questCompleted[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted;
        isActive[1] = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().prestigeReward;
            description[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().description;
            questName[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[2, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().goal[i];
            questStage[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[2] = true;
        }
        questCompleted[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questCompleted;
        isActive[2] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().prestigeReward;
            description[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().description;
            questName[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[3, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().goal[i];
            questStage[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[3] = true;
        }
        questCompleted[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted;
        isActive[3] = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().prestigeReward;
            description[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().description;
            questName[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[4, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().goal[i];
            questStage[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[4] = true;
        }
        questCompleted[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questCompleted;
        isActive[4] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().prestigeReward;
            description[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().description;
            questName[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[5, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().goal[i];
            questStage[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[5] = true;
        }
        questCompleted[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questCompleted;
        isActive[5] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().prestigeReward;
            description[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().description;
            questName[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[6, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().goal[i];
            questStage[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[6] = true;
        }
        questCompleted[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted;
        isActive[6] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().prestigeReward;
            description[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().description;
            questName[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[7, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().goal[i];
            questStage[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[7] = true;
        }
        questCompleted[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted;
        isActive[7] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().prestigeReward;
            description[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().description;
            questName[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[8, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().goal[i];
            questStage[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[8] = true;
        }
        questCompleted[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted;
        isActive[8] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().prestigeReward;
            description[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().description;
            questName[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[9, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().goal[i];
            questStage[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[9] = true;
        }
        questCompleted[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted;
        isActive[9] = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().prestigeReward;
            description[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().description;
            questName[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[10, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().goal[i];
            questStage[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[10] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[10] = true;
        }
        questCompleted[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.questCompleted;
        isActive[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().objectReward != null)
                objectRewardName[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().goldReward;
            experienceReward[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().experienceReward;
            prestigeReward[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().prestigeReward;
            description[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().description;
            questName[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questName;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().goal.Count; i++)
                goal[11, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().goal[i];
            questStage[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage;
            skillPoints[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponentInChildren<Image>().color == Color.red)
                questActivated[11] = true;
        }
        questCompleted[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questCompleted;
        isActive[11] = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.isActive;
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject != null)
        {
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().objectReward!=null)
            objectRewardName[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            goldReward[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().goldReward;
            experienceReward[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().experienceReward;
            prestigeReward[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().prestigeReward;
            description[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().description;
            questName[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().questName;
            skillPoints[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().skillPoints;
            questStage[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().questStage;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().goal.Count; i++)
                goal[0, i] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponent<QuestSlot>().goal[i];
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuestObject.GetComponentInChildren<Image>().color == Color.red)
                questActivated[5] = true;
        }
        questCompleted[0] = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuest.questCompleted;
        isActive[0] = true;
    }
}
