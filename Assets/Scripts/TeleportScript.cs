using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{ 
    private void OnCollisionEnter(Collision collider)
    {
           if(name== "TPPointCrypt" && collider.collider.GetComponentInParent<PlayerController>() != null)
        {
            collider.transform.position = new Vector3(290,27.5f,-3);
            if (collider.collider.GetComponent<PlayerController>().currentSummonedArcher != null)
                if (collider.collider.GetComponent<PlayerController>().currentSummonedArcher.GetComponent<SummonedAI>().currentHP > 0)
                {
                    collider.collider.GetComponent<PlayerController>().currentSummonedArcher.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    collider.collider.GetComponent<PlayerController>().currentSummonedArcher.transform.position = new Vector3(288, 27.5f, -5);
                    collider.collider.GetComponent<PlayerController>().currentSummonedArcher.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                }
            if (collider.collider.GetComponent<PlayerController>().currentSummonedMelee != null)
                if (collider.collider.GetComponent<PlayerController>().currentSummonedMelee.GetComponent<SummonedAI>().currentHP > 0)
                {
                    collider.collider.GetComponent<PlayerController>().currentSummonedMelee.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    collider.collider.GetComponent<PlayerController>().currentSummonedMelee.transform.position = new Vector3(288, 27.5f, -1);
                    collider.collider.GetComponent<PlayerController>().currentSummonedMelee.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage == 0)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker = GameObject.Find("UndeadMage (1)").GetComponent<QuestMarker>();
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage == 1)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker = GameObject.Find("TPPointDungeon").GetComponent<QuestMarker>();
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
                }
            }

        }
           else if(name == "TPPointDungeon" && collider.collider.GetComponentInParent<PlayerController>() != null)
        {
            collider.transform.position = new Vector3(-8, 22, 105);
            collider.transform.eulerAngles = new Vector3(0, 90, 0);
            if (collider.collider.GetComponent<PlayerController>().currentSummonedArcher != null)
                if (collider.collider.GetComponent<PlayerController>().currentSummonedArcher.GetComponent<SummonedAI>().currentHP > 0)
                {
                    collider.collider.GetComponent<PlayerController>().currentSummonedArcher.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    collider.collider.GetComponent<PlayerController>().currentSummonedArcher.transform.position = new Vector3(-8, 22, 102);
                    collider.collider.GetComponent<PlayerController>().currentSummonedArcher.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                }
            if (collider.collider.GetComponent<PlayerController>().currentSummonedMelee != null)
                if (collider.collider.GetComponent<PlayerController>().currentSummonedMelee.GetComponent<SummonedAI>().currentHP > 0)
                {
                    collider.collider.GetComponent<PlayerController>().currentSummonedMelee.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    collider.collider.GetComponent<PlayerController>().currentSummonedMelee.transform.position = new Vector3(-8, 22, 110);
                    collider.collider.GetComponent<PlayerController>().currentSummonedMelee.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage == 0)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker = GameObject.Find("LP").GetComponent<QuestMarker>();
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage == 1)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker = GameObject.Find("Librarian").GetComponent<QuestMarker>();
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
                }
            }
        }
    }
}
