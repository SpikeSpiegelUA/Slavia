using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class QuestManager : MonoBehaviour
{
    public GameObject sideQuestPrefab;
    [SerializeField]
    public Quest fayeQuest;
    public Quest mainQuest;
    public Quest headOfGuardQuest;
    public Quest librarianSurveyQuest;
    public Quest headOfVillageQuest;
    public Quest headOfHuntersQuest;
    public Quest headOfRepublicansQuest;
    public Quest headOfRepublicansSecondQuest;
    public Quest headOfRoyalistsQuest;
    public Quest headOfRoyalistsSecondQuest;
    public Quest paladinSpecialQuest;
    public Quest librarianSpecialQuest;
    public GameObject questInfo;
    public GameObject mainQuestObject;
    public GameObject containerForList;
    //Faye quest variables
    public GameObject bensDiary;
    public GameObject strangeBook;
    public GameObject bob;
    public GameObject solovey;
    public GameObject soloveyBandit;
    public bool soloveyKilled = false;
    public bool soloveyArrested = false;
    public bool soloveyRunnedAway = false;
    public bool soloveyBanditKilled = false;
    public int killedBanditsHeadOfGuardQuest;
    public GameObject selectedQuest;
    public bool hasActivaQuest = false;
    public GameObject specialMushroom;
    public int killedRoyalistPatrol = 0;
    public GameObject headOfRoyalists;
    public GameObject headOfRepublicans;
    public GameObject strangeRoyalist;
    private GameManager gameManager;
    public GameObject royalistPatrol1;
    public GameObject royalistPatrol2;
    public int killedRoyalists = 0;
    public int killedRepublicans = 0;
    public int killedWarriors = 0;
    private GameObject priest;
    private GameObject librarian;
    public GameObject artelitStone;
    private GameObject villagePrisonGuard;
    private GameObject headOfGuard;
    private GameObject villageHead;
    private GameObject headOfHunters;
    public GameObject finishDialog;
    public GameObject watchTowerBandit1;
    public GameObject watchTowerBandit2;
    public GameObject watchTowerBandit3;
    // Start is called before the first frame update
    private void Awake()
    {
        if (SaveLoad.isLoading)
            LoadQuest();
    }
    private void Start()
    {
        headOfHunters = GameObject.Find("HeadOfHunters");
        villageHead = GameObject.Find("VillageHead");
        headOfGuard = GameObject.Find("HeadOfGuard");
        villagePrisonGuard = GameObject.Find("Village prison guard");
        priest = GameObject.Find("Priest");
        librarian = GameObject.Find("Librarian");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.isRobber)
        {
            mainQuest.description = "Find the Dragon Scroll and give it to your guild to start a new life";
            headOfGuardQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().cavalryHelmet;
            fayeQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().cavalryArmor;
            headOfVillageQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ironSword;
        }
        if (gameManager.isMage)
        {
            mainQuest.description = "The dragon scroll hides dangerous knowledge. Find it and pass it to the academy to study it and prevent it from falling into the wrong hands";
            headOfGuardQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().staminaResurection;
            fayeQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().healthResurection;
            headOfVillageQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ironSword;
            headOfRoyalistsSecondQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().meleeSummon;
            headOfRoyalistsQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().archerSummon;
            headOfRepublicansQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().archerSummon;
            headOfRepublicansSecondQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().meleeSummon;
        }
        if (gameManager.isPaladin)
        {
            mainQuest.description = "Because of civil war,church lost power and Bamur becomes stronger by each day.Find artelian scroll and return to abbey to restore power of church and defeat Bamur.";
            headOfGuardQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ironHelmet;
            fayeQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ironArmor;
            headOfVillageQuest.objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().bigIronSword;
        }
        //Set main quest
        mainQuestObject.GetComponent<QuestSlot>().questName = mainQuest.questName;
        mainQuestObject.GetComponentInChildren<Text>().text = mainQuestObject.GetComponent<QuestSlot>().questName;
        mainQuestObject.GetComponent<QuestSlot>().description = mainQuest.description;
        mainQuestObject.GetComponent<QuestSlot>().goldReward = mainQuest.goldReward;
        mainQuestObject.GetComponent<QuestSlot>().experienceReward = mainQuest.experienceReward;
        mainQuestObject.GetComponent<QuestSlot>().prestigeReward = mainQuest.prestigeReward;      
        for (int i = 0; i < mainQuest.goal.Length; i++)
        {
            mainQuestObject.GetComponent<QuestSlot>().goal.Add(mainQuest.goal[i]);
        }
        solovey.GetComponent<NavMeshAgent>().speed = 3;
        if (SaveLoad.isLoading)
            LoadQuestsReward();
    }
    void Update()
    {
        //Faye quest
        if(fayeQuest.isActive)
        if (containerForList.transform.Find("FayeQuest") != null)
        {
                if (bob!=null)
                    if (bob.GetComponent<CivilianAI>().currentHP <= 0&&bob.activeSelf)
                    {
                        fayeQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + fayeQuest.questName;
                        soloveyRunnedAway = true;
                        solovey.SetActive(false);
                        solovey.transform.position = new Vector3(73.6f, 22.3f, -101);
                        solovey.SetActive(true);
                        solovey.GetComponent<CivilianAI>().enabled = false;
                        solovey.transform.eulerAngles = new Vector3(0, -59, 0);
                        if (soloveyBandit != null && soloveyBandit.activeSelf)
                            if (soloveyBandit.GetComponent<GuardAI>().currentHP > 0)
                            {
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startRotation = new Vector3(0, 126, 0);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition = new Vector3(71, 22, -100);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().ImmediatelyCancelAlert();
                                GameObject.Find("SoloveyBandit").GetComponent<Animator>().SetBool("IsRunning", true);
                                GameObject.Find("SoloveyBandit").GetComponent<Animator>().Play("Run");
                                GameObject.Find("SoloveyBandit").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition);
                            }
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("FayeQuest").gameObject);
                    }
                if(villagePrisonGuard!=null)
                    if(villagePrisonGuard.GetComponent<GuardAI>().currentHP <= 0&& villagePrisonGuard.activeSelf)
                    {
                        fayeQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + fayeQuest.questName;
                        soloveyRunnedAway = true;
                        solovey.SetActive(false);
                        solovey.transform.position = new Vector3(73.6f, 22.3f, -101);
                        solovey.SetActive(true);
                        solovey.GetComponent<CivilianAI>().enabled = false;
                        solovey.transform.eulerAngles = new Vector3(0, -59, 0);
                        if (soloveyBandit != null && soloveyBandit.activeSelf)
                            if (soloveyBandit.GetComponent<GuardAI>().currentHP > 0)
                            {
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startRotation = new Vector3(0, 126, 0);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition = new Vector3(71, 22, -100);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().ImmediatelyCancelAlert();
                                GameObject.Find("SoloveyBandit").GetComponent<Animator>().SetBool("IsRunning", true);
                                GameObject.Find("SoloveyBandit").GetComponent<Animator>().Play("Run");
                                GameObject.Find("SoloveyBandit").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition);
                            }
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("FayeQuest").gameObject);
                    }
                    if (gameManager.villageAttackedByPlayer)
                    {
                        fayeQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + fayeQuest.questName;
                    soloveyRunnedAway = true;
                    solovey.SetActive(false);
                    solovey.transform.position = new Vector3(73.6f, 22.3f, -101);
                    solovey.SetActive(true);
                    solovey.GetComponent<CivilianAI>().enabled = false;
                    solovey.transform.eulerAngles = new Vector3(0, -59, 0);
                    if (soloveyBandit != null&&soloveyBandit.activeSelf)
                        if (soloveyBandit.GetComponent<GuardAI>().currentHP > 0)
                        {
                            GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startRotation = new Vector3(0, 126, 0);
                            GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition = new Vector3(71, 22, -100);
                            GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().ImmediatelyCancelAlert();
                            GameObject.Find("SoloveyBandit").GetComponent<Animator>().SetBool("IsRunning", true);
                            GameObject.Find("SoloveyBandit").GetComponent<Animator>().Play("Run");
                            GameObject.Find("SoloveyBandit").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition);
                        }
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                    Destroy(containerForList.transform.Find("FayeQuest").gameObject);
                }
                if (solovey != null)
                    if(containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage < 8)
                    if ((solovey.GetComponent<CivilianAI>().currentHP <= 0||solovey.GetComponent<SummonedAI>().currentHP<=0) && containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 4 && containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage < 8&&fayeQuest.isActive)
                    {
                        soloveyKilled = true;
                        soloveyArrested = false;
                        soloveyRunnedAway = false;
                        containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage = 8;
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = fayeQuest.goal[8];
                            if(soloveyBandit!=null)
                            if (soloveyBandit.GetComponent<GuardAI>().currentHP > 0)
                            {
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startRotation = new Vector3(0, 126, 0);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition = new Vector3(71, 22, -100);
                            }
                                GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                    }
                if ((GameObject.Find("SoloveyMeetingPoint").transform.position - GameObject.Find("Player").transform.position).magnitude <= 10 && containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage==4&&fayeQuest.isActive)
                {
                    containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = fayeQuest.goal[containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                    GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                }
                if((GameObject.Find("SoloveyMeetingPoint").transform.position - GameObject.Find("Player").transform.position).magnitude >=30&& containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage>=5&&containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage<8&&fayeQuest.isActive&&!soloveyArrested&&!soloveyRunnedAway&&solovey!=null)
                {
                    if (solovey.GetComponent<CivilianAI>().currentHP > 0) 
                    {
                        soloveyRunnedAway = true;
                        solovey.SetActive(false);
                        solovey.transform.position = new Vector3(73.6f, 22.3f, -101);
                        solovey.SetActive(true);
                        solovey.GetComponent<CivilianAI>().enabled = false;
                        solovey.transform.eulerAngles = new Vector3(0, -59, 0);
                        solovey.GetComponent<CivilianAI>().startPosition = new Vector3(73.6f, 22.3f, -101);
                        solovey.GetComponent<CivilianAI>().startRotation = new Vector3(0, -59, 0);
                    if (soloveyBandit != null)
                            if (soloveyBandit.GetComponent<GuardAI>().currentHP > 0)
                            {
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startRotation = new Vector3(0, 126, 0);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition = new Vector3(71, 22, -100);
                                GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().ImmediatelyCancelAlert();
                                GameObject.Find("SoloveyBandit").GetComponent<Animator>().SetBool("IsRunning", true);
                                GameObject.Find("SoloveyBandit").GetComponent<Animator>().Play("Run");
                                GameObject.Find("SoloveyBandit").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("SoloveyBandit").GetComponent<GuardAI>().startPosition);
                            }
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = fayeQuest.goal[8];
                        containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage = 8;
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                    }
                }
                if(soloveyBandit!=null)
                    if (soloveyBandit.GetComponent<GuardAI>().currentHP <= 0&& containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >=5&& containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage<8&&!soloveyBanditKilled&&fayeQuest.isActive)
                    {
                        containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = fayeQuest.goal[containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                        soloveyBanditKilled = true;
                        fayeQuest.currentQuestMarker = solovey.GetComponent<QuestMarker>();
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                    }
        }
        if (headOfGuardQuest.isActive)
            if (containerForList.transform.Find("HeadOfGuardQuest") != null)
            {
                if (gameManager.villageAttackedByPlayer)
                {
                    headOfGuardQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfGuardQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfGuardQuest").gameObject);
                }
                if(headOfGuard!=null)
                    if(headOfGuard.GetComponent<GuardAI>().currentHP<=0&& headOfGuard.activeSelf)
                    {
                        headOfGuardQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfGuardQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfGuardQuest").gameObject);
                    }
                if(containerForList.transform.Find("HeadOfGuardQuest")!=null)
                if(killedBanditsHeadOfGuardQuest==3&& containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage == 0)
                {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage++;
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = headOfGuardQuest.goal[1];
                    GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                        headOfGuardQuest.currentQuestMarker = GameObject.Find("HeadOfGuard").GetComponent<QuestMarker>();
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.currentQuestMarker;
                    }
            }
        if (headOfVillageQuest.isActive)
            if (containerForList.transform.Find("HeadOfVillageQuest") != null)
            {
                if (gameManager.villageAttackedByPlayer)
                {
                    headOfVillageQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfVillageQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfVillageQuest").gameObject);
                }
                if (villageHead != null)
                    if (villageHead.GetComponent<CivilianAI>().currentHP <= 0 && villageHead.activeSelf)
                    {
                        headOfVillageQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfVillageQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfVillageQuest").gameObject);
                    }
            }
        if (librarianSurveyQuest.isActive)
            if (containerForList.transform.Find("LibrarianSurveyQuest") != null)
            {
                if (gameManager.villageAttackedByPlayer)
                {
                    librarianSurveyQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + librarianSurveyQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("LibrarianSurveyQuest").gameObject);
                }
                if (librarian != null)
                    if (librarian.GetComponent<CivilianAI>().currentHP <= 0 && librarian.activeSelf)
                    {
                        librarianSurveyQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + librarianSurveyQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("LibrarianSurveyQuest").gameObject);
                    }
            }
        if (headOfHuntersQuest.isActive)
            if (containerForList.transform.Find("HeadOfHuntersQuest") != null)
            {
                if (gameManager.villageAttackedByPlayer)
                {
                    headOfHuntersQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfHuntersQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfHuntersQuest").gameObject);
                }
                if (headOfHunters != null)
                    if (headOfHunters.GetComponent<GuardAI>().currentHP <= 0 && headOfHunters.activeSelf)
                    {
                        headOfHuntersQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfHuntersQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfHuntersQuest").gameObject);
                    }
            }
        if (headOfRoyalistsQuest.isActive)
            if (containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
            {
                if (gameManager.royalistAttackedByPlayer)
                {
                    headOfRoyalistsQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRoyalistsQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfRoyalistsQuest").gameObject);
                }
                if (headOfRoyalists!=null)
                    if (headOfRoyalists.GetComponent<GuardAI>().currentHP <= 0 && headOfRoyalists.activeSelf)
                    {
                        headOfRoyalistsQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRoyalistsQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfRoyalistsQuest").gameObject);
                    }
                if (strangeRoyalist.GetComponent<GuardAI>().currentHP <= 0 && containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage <= 2)
                    HeadOfRoyalistsQuestStageThree();
            }
        if (headOfRepublicansQuest.isActive)
            if (containerForList.transform.Find("HeadOfRepublicansQuest") != null)
            {
                if (gameManager.republicanAttackedByPlayer)
                {
                    headOfRepublicansQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRepublicansQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfRepublicansQuest").gameObject);
                }
                if (headOfRepublicans!=null)
                    if (headOfRepublicans.GetComponent<GuardAI>().currentHP <= 0 && headOfRepublicans.activeSelf)
                    {
                        headOfRepublicansQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRepublicansQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfRepublicansQuest").gameObject);
                    }
                if (killedRoyalistPatrol == 2)
                {
                    HeadOfRepublicansQuestStageTwo();
                    killedRoyalistPatrol++;
                }
            }
        if (headOfRepublicansSecondQuest.isActive)
            if (containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
            {
                if (gameManager.republicanAttackedByPlayer)
                {
                    headOfRepublicansSecondQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRepublicansSecondQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfRepublicansSecondQuest").gameObject);
                }
                if (headOfRepublicans!=null)
                    if (headOfRepublicans.GetComponent<GuardAI>().currentHP <= 0 && headOfRepublicans.activeSelf)
                    {
                        headOfRepublicansSecondQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRepublicansSecondQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfRepublicansSecondQuest").gameObject);
                    }
                if (killedRoyalists == 5)
                {
                    killedRoyalists++;
                    HeadOfRepublicansSecondQuestStageTwo();
                }
            }
        if (headOfRoyalistsSecondQuest.isActive)
            if (containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
            {
                if (gameManager.royalistAttackedByPlayer)
                {
                    headOfRoyalistsSecondQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRoyalistsSecondQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("HeadOfRoyalistsSecondQuest").gameObject);
                }
                if (headOfRoyalists != null)
                    if (headOfRoyalists.GetComponent<GuardAI>().currentHP <= 0 && headOfRoyalists.activeSelf)
                    {
                        headOfRoyalistsSecondQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + headOfRoyalistsSecondQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("HeadOfRoyalistsSecondQuest").gameObject);
                    }
                if (killedRepublicans == 5)
                {
                    killedRepublicans++;
                }
            }
        if (paladinSpecialQuest.isActive)
            if (containerForList.transform.Find("PaladinSpecialQuest") != null)
            {
                if (gameManager.villageAttackedByPlayer)
                {
                    paladinSpecialQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + paladinSpecialQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("PaladinSpecialQuest").gameObject);
                }
                if (priest!= null)
                    if (priest.GetComponent<CivilianAI>().currentHP <= 0 && priest.activeSelf)
                    {
                        paladinSpecialQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + paladinSpecialQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("PaladinSpecialQuest").gameObject);
                    }
                if (killedWarriors == 5)
                {
                    killedWarriors++;
                    SpecialQuestStageOne();
                }
            }
        if (librarianSpecialQuest.isActive)
            if (containerForList.transform.Find("LibrarianSpecialQuest") != null)
            {
                if (gameManager.villageAttackedByPlayer)
                {
                    paladinSpecialQuest.isActive = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + librarianSpecialQuest.questName;
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    }
                    Destroy(containerForList.transform.Find("LibrarianSpecialQuest").gameObject);
                }
                if (librarian != null)
                    if (librarian.GetComponent<CivilianAI>().currentHP <= 0 && librarian.activeSelf)
                    {
                        librarianSpecialQuest.isActive = false;
                        GameObject.Find("Player").GetComponent<PlayerController>().questFailed.GetComponentInChildren<Text>().text = "Quest failed:" + librarianSpecialQuest.questName;
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive && !GameObject.Find("GUIManager").GetComponent<GUIController>().GUIopened)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("QuestFailed");
                            GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                            GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                        }
                        Destroy(containerForList.transform.Find("LibrarianSpecialQuest").gameObject);
                    }
            }
    }
    //Found which slot was chose and change color
    public void SetQuestInfo()
    {
        questInfo.transform.Find("Description").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().description;
        questInfo.transform.Find("QuestName").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName;
        questInfo.transform.Find("GoldReward").GetComponent<Text>().text ="Gold:"+EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().goldReward;
        questInfo.transform.Find("ExperienceReward").GetComponent<Text>().text ="Experience:"+EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().experienceReward;
        questInfo.transform.Find("PrestigeReward").GetComponent<Text>().text = "Prestige:" + EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().prestigeReward;
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().objectReward != null)
        {
            questInfo.transform.Find("ObjectRewardText").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().objectReward.GetComponent<Item>().itemName;
            questInfo.transform.Find("ObjectRewardImage").GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().objectReward.GetComponent<Image>().sprite;
            questInfo.transform.Find("ObjectRewardImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            questInfo.transform.Find("ObjectRewardText").GetComponent<Text>().text = "";
            questInfo.transform.Find("ObjectRewardImage").GetComponent<Image>().sprite = null;
            questInfo.transform.Find("ObjectRewardImage").GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Find traitor" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if(containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color==Color.red)
                        containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Kill bandits" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Supply" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Survey" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Artelit  mushroom" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Get orders" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Assault" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Traitor" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Storm" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Shrine" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Crypt" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questName == "Find Dragon scroll" && gameManager.withQuestMarkers)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color != Color.red)
            {
                if (hasActivaQuest)
                    for (int i = 0; i < containerForList.transform.childCount; i++)
                        if (containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color == Color.red)
                            containerForList.transform.GetChild(i).GetComponentInChildren<Image>().color = Color.white;
                if (selectedQuest != null)
                    selectedQuest.GetComponentInChildren<Image>().color = Color.white;
                selectedQuest = EventSystem.current.currentSelectedGameObject;
                selectedQuest.GetComponentInChildren<Image>().color = Color.red;
                if (mainQuest.currentQuestMarker == null)
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                else
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                hasActivaQuest = true;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>().color = Color.white;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                hasActivaQuest = false;
            }
        }
        questInfo.transform.Find("SkillPoints").GetComponent<Text>().text = "Skill points:" + EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().skillPoints;
        questInfo.transform.Find("CurrentGoal").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().goal[EventSystem.current.currentSelectedGameObject.GetComponent<QuestSlot>().questStage];
    }
    public void HeadOfGuardStageOne()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().CloseDialogue();
        watchTowerBandit1.gameObject.SetActive(true);
        watchTowerBandit2.gameObject.SetActive(true);
        watchTowerBandit3.gameObject.SetActive(true);
    }
    public void FayeQuestFirstStage()
    {
        if (fayeQuest.isActive)
            bensDiary.SetActive(true);
    }
    public void MainQuestStageOne()
    {
        finishDialog.SetActive(true);
        mainQuest.currentQuestMarker = finishDialog.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuest.currentQuestMarker;
        mainQuestObject.GetComponent<QuestSlot>().questStage = 1;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().mainQuest.goal[mainQuestObject.GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void FayeQuestStageTwo()
    {
        if (fayeQuest.isActive)
        {
            if (containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 0)
            {
                fayeQuest.currentQuestMarker = GameObject.Find("Faye").GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
                GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = fayeQuest.goal[containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
            }
        }
    }
    public void FayeQuestStageThree()
    {
        if (fayeQuest.isActive)
        {
            if (containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 2)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
                fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().bob.GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
                GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = fayeQuest.goal[containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                bob.SetActive(true);
            }
        }
    }
    public void HeadOfHuntersStageOne()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("NewQuestThenNewStage");
        if (GameObject.Find("HeadOfHunters") != null)
            headOfHuntersQuest.currentQuestMarker = GameObject.Find("HeadOfHunters").GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker;
        containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = headOfHuntersQuest.goal[containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    private void HeadOfRepublicansQuestStageTwo()
    {
        headOfRepublicansQuest.currentQuestMarker = GameObject.Find("PatrolRoyalist2").GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRepublicansQuestStageThree()
    {
        headOfRepublicansQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRepublicansQuestStageFour()
    {
        headOfRepublicansQuest.currentQuestMarker = headOfRepublicans.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage = 4;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void SpecialQuestStageOne()
    {
        artelitStone.SetActive(true);
        paladinSpecialQuest.currentQuestMarker = GameObject.Find("ArtelitStone").GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage = 1;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void SpecialQuestStageTwo()
    {
        artelitStone.SetActive(false);
        Instantiate(GameObject.Find("GUIManager").GetComponent<Inventory>().artelitSword, new Vector3(-94.6f, 23.6f, -38.8f), new Quaternion(11.3f, 87.87f, -89.8f,0));
        paladinSpecialQuest.currentQuestMarker =priest.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage = 2;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRepublicansSecondQuestStageZero()
    {
        if (GameObject.Find("Republican3") != null)
            if (GameObject.Find("Republican3").GetComponent<GuardAI>().currentHP > 0)
            {
                Destroy(GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>());
            GameObject.Find("Republican3").GetComponent<GuardAI>().enabled = true;
            GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(158.65f,22.32f,-9.33f);
            GameObject.Find("Republican3").GetComponent<GuardAI>().startRotation = new Vector3(0, 189.58f, 0);
            GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican3").GetComponent<NavMeshAgent>().speed = 4;
    GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican4") != null)
            if (GameObject.Find("Republican4").GetComponent<GuardAI>().currentHP > 0)
            {
                Destroy(GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>());
            GameObject.Find("Republican4").GetComponent<GuardAI>().enabled = true;
            GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(154.47f, 22.09f, -9.1f);
            GameObject.Find("Republican4").GetComponent<GuardAI>().startRotation = new Vector3(0, 543, 0);
            GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican4").GetComponent<NavMeshAgent>().speed = 4;
            GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican1") != null)
            if (GameObject.Find("Republican1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition = new Vector3(158.3f, 22.25f, -13f);
            GameObject.Find("Republican1").GetComponent<GuardAI>().startRotation = new Vector3(0, 175, 0);
            GameObject.Find("Republican1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican1").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican2") != null)
            if (GameObject.Find("Republican2").GetComponent<GuardAI>().currentHP > 0)
            { 
                GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition = new Vector3(155.3f, 22.16f, -13f);
            GameObject.Find("Republican2").GetComponent<GuardAI>().startRotation = new Vector3(0, 174 , 0);
            GameObject.Find("Republican2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican2").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRepublican") != null)
            if (GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition = new Vector3(156.4f, 22.16f, -15.55f);
            GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startRotation = new Vector3(0, 166.6f, 0);
            GameObject.Find("HeadOfRepublican").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition);
            GameObject.Find("HeadOfRepublican").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("HeadOfRepublican").GetComponent<Animator>().Play("Run");
            }
    }
    public void MainQuestVillageGuardMove()
    {
        if (GameObject.Find("TwoHand Guard") != null)
            if (GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().currentHP > 0)
            {
                Destroy(GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>());
                GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().enabled = true;
                GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition = new Vector3(28.16f,21.7f, -64.11f);
                GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startRotation = new Vector3(0, 165.746f, 0);
                GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().speed = 4;
                GameObject.Find("TwoHand Guard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("TwoHand Guard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("ArcherGuard") != null)
            if (GameObject.Find("ArcherGuard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("ArcherGuard").GetComponent<GuardAI>().startPosition = new Vector3(32.23f, 21.61f, -60.73f);
                GameObject.Find("ArcherGuard").GetComponent<GuardAI>().startRotation = new Vector3(0, 165.024f, 0);
                GameObject.Find("ArcherGuard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("ArcherGuard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("ArcherGuard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("ArcherGuard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Mage") != null)
            if (GameObject.Find("Mage").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Mage").GetComponent<GuardAI>().startPosition = new Vector3(29.18f, 21.7f, -61.72f);
                GameObject.Find("Mage").GetComponent<GuardAI>().startRotation = new Vector3(0, 157.977f, 0);
                GameObject.Find("Mage").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Mage").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Mage").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Mage").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Guard") != null)
            if (GameObject.Find("Guard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Guard").GetComponent<GuardAI>().startPosition = new Vector3(30.82f, 22.16f, -63.29f);
                GameObject.Find("Guard").GetComponent<GuardAI>().startRotation = new Vector3(0, 164.8f, 0);
                GameObject.Find("Guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Guard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Guard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Guard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Village prison guard") != null)
            if (GameObject.Find("Village prison guard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Village prison guard").GetComponent<GuardAI>().startPosition = new Vector3(34.15f, 22.16f, -62.52f);
                GameObject.Find("Village prison guard").GetComponent<GuardAI>().startRotation = new Vector3(0, -191.149f, 0);
                GameObject.Find("Village prison guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Village prison guard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Village prison guard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Village prison guard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfGuard") != null)
            if (GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startPosition = new Vector3(32.86f, 22.16f, -67.28f);
                GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startRotation = new Vector3(0, -172.514f, 0);
                GameObject.Find("HeadOfGuard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfGuard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfGuard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("ArcherGuard2") != null)
            if (GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().startPosition = new Vector3(35.37f, 22.16f, -59.98f);
                GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().startRotation = new Vector3(0, -162.545f, 0);
                GameObject.Find("ArcherGuard2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("ArcherGuard2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("ArcherGuard2").GetComponent<Animator>().Play("Run");
            }
    }
    public void MainQuestVillageGuardMoveTwo()
    {
        if (GameObject.Find("TwoHand Guard") != null)
            if (GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().currentHP > 0)
            {
                Destroy(GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>());
                GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().enabled = true;
                GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition = new Vector3(52.2f, 21.7f, -79.3f);
                GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startRotation = new Vector3(0, 165.746f, 0);
                GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().speed = 4;
                GameObject.Find("TwoHand Guard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("TwoHand Guard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("ArcherGuard") != null)
            if (GameObject.Find("ArcherGuard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("ArcherGuard").GetComponent<GuardAI>().startPosition = new Vector3(50, 21.61f, -87.82f);
                GameObject.Find("ArcherGuard").GetComponent<GuardAI>().startRotation = new Vector3(0, 359.386f, 0);
                GameObject.Find("ArcherGuard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("ArcherGuard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("ArcherGuard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("ArcherGuard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Mage") != null)
            if (GameObject.Find("Mage").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Mage").GetComponent<GuardAI>().startPosition = new Vector3(57.9f, 21.7f, -82.3f);
                GameObject.Find("Mage").GetComponent<GuardAI>().startRotation = new Vector3(0, 177.865f, 0);
                GameObject.Find("Mage").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Mage").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Mage").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Mage").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Guard") != null)
            if (GameObject.Find("Guard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Guard").GetComponent<GuardAI>().startPosition = new Vector3(73.98f, 22.16f, -83.61f);
                GameObject.Find("Guard").GetComponent<GuardAI>().startRotation = new Vector3(0, 164.8f, 0);
                GameObject.Find("Guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Guard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Guard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Guard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Village prison guard") != null)
            if (GameObject.Find("Village prison guard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Village prison guard").GetComponent<GuardAI>().startPosition = new Vector3(71.39f, 22.16f, -84f);
                GameObject.Find("Village prison guard").GetComponent<GuardAI>().startRotation = new Vector3(0, -191.149f, 0);
                GameObject.Find("Village prison guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Village prison guard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Village prison guard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Village prison guard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfGuard") != null)
            if (GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startPosition = new Vector3(79.52f, 22.16f, -84.04f);
                GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startRotation = new Vector3(0, -225.197f, 0);
                GameObject.Find("HeadOfGuard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfGuard").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfGuard").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("ArcherGuard2") != null)
            if (GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().startPosition = new Vector3(76.04f, 22.16f, -95.71f);
                GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().startRotation = new Vector3(0, -347.159f, 0);
                GameObject.Find("ArcherGuard2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("ArcherGuard2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("ArcherGuard2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("ArcherGuard2").GetComponent<Animator>().Play("Run");
            }
    }
    public void MainQuestRepublicanStageOne()
    {
        if (GameObject.Find("Republican3") != null)
            if (GameObject.Find("Republican3").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(29.54f, 21.50507f, -58.65f);
                GameObject.Find("Republican3").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.976f, 0);
                GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican4") != null)
            if (GameObject.Find("Republican4").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(24.36f, 21.42112f, -57.78f);
                GameObject.Find("Republican4").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.049f, 0);
                GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican1") != null)
            if (GameObject.Find("Republican1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition = new Vector3(28.96f, 22.16f, -66.07f);
                GameObject.Find("Republican1").GetComponent<GuardAI>().startRotation = new Vector3(0, 169.945f, 0);
                GameObject.Find("Republican1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican1").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican2") != null)
            if (GameObject.Find("Republican2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition = new Vector3(36.69f, 22.16f, -64.79f);
                GameObject.Find("Republican2").GetComponent<GuardAI>().startRotation = new Vector3(0, 161.904f, 0);
                GameObject.Find("Republican2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRepublican") != null)
            if (GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition = new Vector3(32.95f, 21.95f, -65.4f);
                GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startRotation = new Vector3(0, 169.658f, 0);
                GameObject.Find("HeadOfRepublican").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfRepublican").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfRepublican").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("StrangeRoyalist") != null)
            if (GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startPosition = new Vector3(37.94f, 22.16f, -61.46f);
                GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startRotation = new Vector3(0, 147.943f, 0);
                GameObject.Find("StrangeRoyalist").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startPosition);
                GameObject.Find("StrangeRoyalist").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("StrangeRoyalist").GetComponent<Animator>().Play("Run");
            }
    }
    public void MainQuestRepublicanStageTwo()
    {
        if (GameObject.Find("Republican3") != null)
            if (GameObject.Find("Republican3").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(71.35f, 22.32f, -81.22f);
                GameObject.Find("Republican3").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.976f, 0);
                GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican4") != null)
            if (GameObject.Find("Republican4").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(68.01f, 22.32f, -80.53f);
                GameObject.Find("Republican4").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.049f, 0);
                GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican1") != null)
            if (GameObject.Find("Republican1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition = new Vector3(74.77f, 22.16f, -90.67f);
                GameObject.Find("Republican1").GetComponent<GuardAI>().startRotation = new Vector3(0, 353.677f, 0);
                GameObject.Find("Republican1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican1").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican2") != null)
            if (GameObject.Find("Republican2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition = new Vector3(77.35f, 22.16f, -89.96f);
                GameObject.Find("Republican2").GetComponent<GuardAI>().startRotation = new Vector3(0, 346.23f, 0);
                GameObject.Find("Republican2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRepublican") != null)
            if (GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition = new Vector3(79.06f, 21.95f, -85.89f);
                GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startRotation = new Vector3(0, 236.206f, 0);
                GameObject.Find("HeadOfRepublican").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfRepublican").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfRepublican").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("StrangeRoyalist") != null)
            if (GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startPosition = new Vector3(77.99f, 22.16f, -96.76f);
                GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startRotation = new Vector3(0, 147.943f, 0);
                GameObject.Find("StrangeRoyalist").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startPosition);
                GameObject.Find("StrangeRoyalist").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("StrangeRoyalist").GetComponent<Animator>().Play("Run");
            }
    }
    public void MainQuestRoyalistsStageOne()
    {
        if (GameObject.Find("Royalist3") != null)
            if (GameObject.Find("Royalist3").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(29.54f, 21.50507f, -58.65f);
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.976f, 0);
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist4") != null)
            if (GameObject.Find("Royalist4").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(24.36f, 21.42112f, -57.78f);
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.049f, 0);
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist1") != null)
            if (GameObject.Find("Royalist1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition = new Vector3(28.96f, 22.16f, -66.07f);
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startRotation = new Vector3(0, 169.945f, 0);
                GameObject.Find("Royalist1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist1").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist2") != null)
            if (GameObject.Find("Royalist2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition = new Vector3(36.69f, 22.16f, -64.79f);
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startRotation = new Vector3(0, 161.904f, 0);
                GameObject.Find("Royalist2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRoyalists") != null)
            if (GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition = new Vector3(32.95f, 21.95f, -65.4f);
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startRotation = new Vector3(0, 169.658f, 0);
                GameObject.Find("HeadOfRoyalists").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().Play("Run");
            }
    }
    public void MainQuestRoyalistsStageTwo()
    {
        if (GameObject.Find("Royalist3") != null)
            if (GameObject.Find("Royalist3").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(71.35f, 22.32f, -81.22f);
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.976f, 0);
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist4") != null)
            if (GameObject.Find("Royalist4").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(68.01f, 22.09f, -80.53f);
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startRotation = new Vector3(0, 159.049f, 0);
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist1") != null)
            if (GameObject.Find("Royalist1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition = new Vector3(74.77f, 22.16f, -90.67f);
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startRotation = new Vector3(0, 353.677f, 0);
                GameObject.Find("Royalist1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist1").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist2") != null)
            if (GameObject.Find("Royalist2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition = new Vector3(77.35f, 22.16f, -89.96f);
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startRotation = new Vector3(0, 346.23f, 0);
                GameObject.Find("Royalist2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRoyalists") != null)
            if (GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition = new Vector3(79.06f, 21.95f, -85.89f);
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startRotation = new Vector3(0, 236.206f, 0);
                GameObject.Find("HeadOfRoyalists").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().Play("Run");
            }
    }
    public void HeadOfRepublicansSecondQuestStageOne()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
        if (GameObject.Find("Republican3") != null)
            if (GameObject.Find("Republican3").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(152.4f, 22.32f, -48.69f);
                GameObject.Find("Republican3").GetComponent<GuardAI>().startRotation = new Vector3(0, 358, 0);
                GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican4") != null)
            if (GameObject.Find("Republican4").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(149, 22.09f, -31f);
            GameObject.Find("Republican4").GetComponent<GuardAI>().startRotation = new Vector3(0, 363, 0);
            GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican1") != null)
            if (GameObject.Find("Republican1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition = new Vector3(151.279f, 22.16f, -38.95f);
            GameObject.Find("Republican1").GetComponent<GuardAI>().startRotation = new Vector3(0, 124f, 0);
            GameObject.Find("Republican1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican1").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican1").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Republican2") != null)
            if (GameObject.Find("Republican2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition = new Vector3(153.49f, 22.16f, -40.65f);
            GameObject.Find("Republican2").GetComponent<GuardAI>().startRotation = new Vector3(0, 291, 0);
            GameObject.Find("Republican2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican2").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican2").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRepublican") != null)
            if (GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition = new Vector3(150, 21.95f, -43.1f);
            GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startRotation = new Vector3(0, 38, 0);
            GameObject.Find("HeadOfRepublican").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition);
            GameObject.Find("HeadOfRepublican").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("HeadOfRepublican").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("StrangeRoyalist") != null)
            if (GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("StrangeRoyalist").tag = "Republican";
                GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startPosition = new Vector3(155, 21.95f, -38.3f);
                GameObject.Find("StrangeRoyalist").GetComponent<GuardAI>().startRotation = new Vector3(0, 395, 0);
                GameObject.Find("StrangeRoyalist").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRepublican").GetComponent<GuardAI>().startPosition);
                GameObject.Find("StrangeRoyalist").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("StrangeRoyalist").GetComponent<Animator>().Play("Run");
            }
        headOfRepublicansSecondQuest.currentQuestMarker = GameObject.Find("RoyalistCamp").GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRoyalistsSecondQuestStageOne()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
        if (GameObject.Find("Royalist3") != null)
            if (GameObject.Find("Royalist3").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(56.18795f, 22.32f, 160.3629f);
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startRotation = new Vector3(0, 85.72501f, 0);
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist4") != null)
            if (GameObject.Find("Royalist4").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(41.2506f, 22.09f, 160.5618f);
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startRotation = new Vector3(0, 288.921f, 0);
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist1") != null)
            if (GameObject.Find("Royalist1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition = new Vector3(54.25641f, 22.16f, 153.4668f);
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startRotation = new Vector3(0, 229.563f, 0);
                GameObject.Find("Royalist1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist1").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist2") != null)
            if (GameObject.Find("Royalist2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition = new Vector3(51.44122f, 22.16f, 152.79f);
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startRotation = new Vector3(0, 97.925f, 0);
                GameObject.Find("Royalist2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRoyalists") != null)
            if (GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition = new Vector3(51.47403f, 21.95f, 161.92f);
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startRotation = new Vector3(0, 166.643f, 0);
                GameObject.Find("HeadOfRoyalists").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().Play("Run");
            }
        headOfRoyalistsSecondQuest.currentQuestMarker = GameObject.Find("RepublicansCamp").GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRoyalistsSecondQuestStageZero()
    {
        if (GameObject.Find("Royalist3") != null)
            if (GameObject.Find("Royalist3").GetComponent<GuardAI>().currentHP > 0)
            {
                Destroy(GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>());
                GameObject.Find("Royalist3").GetComponent<GuardAI>().enabled = true;
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(104.6f, 22.32f, 145.2f);
                GameObject.Find("Royalist3").GetComponent<GuardAI>().startRotation = new Vector3(0, -55.2f, 0);
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().speed = 4;
                GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist4") != null)
            if (GameObject.Find("Royalist4").GetComponent<GuardAI>().currentHP > 0)
            {
                Destroy(GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>());
                GameObject.Find("Royalist4").GetComponent<GuardAI>().enabled = true;
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(98, 22.09f, 141.6f);
                GameObject.Find("Royalist4").GetComponent<GuardAI>().startRotation = new Vector3(0, 315, 0);
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().speed = 4;
                GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist1") != null)
            if (GameObject.Find("Royalist1").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition = new Vector3(97.97f, 22.25f, 145.75f);
                GameObject.Find("Royalist1").GetComponent<GuardAI>().startRotation = new Vector3(0, -37.8f, 0);
                GameObject.Find("Royalist1").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist1").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist1").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist1").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("Royalist2") != null)
            if (GameObject.Find("Royalist2").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition = new Vector3(101, 22.16f,147);
                GameObject.Find("Royalist2").GetComponent<GuardAI>().startRotation = new Vector3(0, 329, 0);
                GameObject.Find("Royalist2").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist2").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist2").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist2").GetComponent<Animator>().Play("Run");
            }
        if (GameObject.Find("HeadOfRoyalists") != null)
            if (GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().currentHP > 0)
            {
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition = new Vector3(98.58f, 22.16f, 148.84f);
                GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startRotation = new Vector3(0, -53, 0);
                GameObject.Find("HeadOfRoyalists").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfRoyalists").GetComponent<GuardAI>().startPosition);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("HeadOfRoyalists").GetComponent<Animator>().Play("Run");
            }
    }
    public void LibrarianSpecialQuestStageOne()
    {
        librarianSpecialQuest.currentQuestMarker = GameObject.Find("TPPointDungeon").GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRepublicansSecondQuestStageTwo()
    {
        headOfRepublicansSecondQuest.currentQuestMarker = headOfRepublicans.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRoyalistsSecondQuestStageTwo()
    {
        headOfRoyalistsSecondQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRoyalistsQuestStageOne()
    {
        headOfRoyalistsQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage++;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRoyalistsQuestStageTwo()
    {
        headOfRoyalistsQuest.currentQuestMarker = strangeRoyalist.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage = 2;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public void HeadOfRoyalistsQuestStageThree()
    {
        headOfRoyalistsQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker;
        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage = 3;
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage];
        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
    }
    public bool CheckIfHavePlace(GameObject objectToTake)
    {
        if (objectToTake.GetComponent<Item>().amountInStack == 1)
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots < 60)
            {
                GameObject.Find("GUIManager").GetComponent<Inventory>().Take(objectToTake, 20);
                return true;
            }
        if (objectToTake.GetComponent<Item>().amountInStack > 1)
        {
            for (int m = 0; m < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; m++)
            {
                if (objectToTake.GetComponent<Item>().amountInStack > 1 && GameObject.Find("GUIManager").GetComponent<Inventory>().images[m].GetComponent<SlotInfo>().amountOfItems < objectToTake.GetComponent<Item>().amountInStack && GameObject.Find("GUIManager").GetComponent<Inventory>().images[m].GetComponent<Image>().sprite == objectToTake.GetComponent<Image>().sprite)
                {
                    GameObject.Find("GUIManager").GetComponent<Inventory>().Take(objectToTake, 20);
                    return true;
                }
            }
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots < 60)
            {
                GameObject.Find("GUIManager").GetComponent<Inventory>().Take(objectToTake, 20);
                return true;
            }
        }
        return false;
    }    
    private void LoadQuest()
    {
        QuestsData questsData = SaveLoad.globalQuestsData;
        soloveyArrested = questsData.soloveyArrested;
        soloveyBanditKilled = questsData.soloveyBanditKilled;
        soloveyRunnedAway = questsData.soloveyRunnedAway;
        soloveyKilled = questsData.soloveyKilled;
        killedBanditsHeadOfGuardQuest = questsData.killedBanditsHeadOfGuardQuest;
        fayeQuest.questCompleted = questsData.questCompleted[1];
        headOfGuardQuest.questCompleted = questsData.questCompleted[2];
        librarianSurveyQuest.questCompleted = questsData.questCompleted[3];
        headOfVillageQuest.questCompleted = questsData.questCompleted[4];
        headOfHuntersQuest.questCompleted= questsData.questCompleted[5];
        headOfRepublicansQuest.questCompleted= questsData.questCompleted[6];
        headOfRepublicansSecondQuest.questCompleted = questsData.questCompleted[7];
        killedRoyalistPatrol = questsData.killedRoyalistsPatrol;
        killedRoyalists = questsData.killedRoyalists;
        killedRepublicans = questsData.killedRepublicans;
        killedWarriors = questsData.killedWarriors;
        if (questsData.description[1] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "FayeQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = questsData.isActive[1];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[1];
            spawn.GetComponent<QuestSlot>().description = questsData.description[1];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[1];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[1];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[1];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[1]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[1];
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().fayeQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward= GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().fayeQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().fayeQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            }
            for (int i = 0; i < 15; i++)
                if(questsData.goal[1, i]!=null)
                spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[1,i]);
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive || GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage > 0)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 2)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().strangeBook.SetActive(true);
                if (containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 3)
                    bob.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 4)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(true);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyBandit.SetActive(true);
                }
                if(soloveyArrested&& GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 8)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.tag = "Neutral";
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<NavMeshAgent>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<CivilianAI>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<SummonedAI>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Rigidbody>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<AudioSource>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponentInChildren<FractionTrigger>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<ConeOfView>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<DeclineAnimationScript>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Loot>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<BoxCollider>());
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsRunning", false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsStunned", false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsRunning", false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.transform.eulerAngles = new Vector3(0, -243, 0);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.transform.position = new Vector3(44.054f, 22.269f, 65.673f);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(true);
                    GameObject.Find("Cage 1").transform.GetChild(1).gameObject.SetActive(true);
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 0)
                    fayeQuest.currentQuestMarker= bensDiary.GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 1)
                    fayeQuest.currentQuestMarker = GameObject.Find("Faye").GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 2)
                    fayeQuest.currentQuestMarker = strangeBook.GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 3)
                    fayeQuest.currentQuestMarker = bob.GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 4)
                    fayeQuest.currentQuestMarker = solovey.GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 5)
                    fayeQuest.currentQuestMarker = soloveyBandit.GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 6)
                    fayeQuest.currentQuestMarker = solovey.GetComponent<QuestMarker>();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 7)
                    fayeQuest.currentQuestMarker = GameObject.Find("Village prison guard").GetComponent<QuestMarker>();
                if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 8)
                    fayeQuest.currentQuestMarker = GameObject.Find("Faye").GetComponent<QuestMarker>();
                if (questsData.questActivated[1])
                {
                    spawn.GetComponentInChildren<Image>().color = Color.red;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker != null)
                    {
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                    }
                    else
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
                }
            }         
        }
        fayeQuest.questCompleted = questsData.questCompleted[1];
        if (fayeQuest.questCompleted)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.SetActive(true);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().strangeBook.SetActive(true);
            if (bob != null)
                bob.SetActive(true);
            if (solovey != null)
                GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(true);
            if (soloveyBandit != null)
                GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyBandit.SetActive(true);
            if (soloveyArrested)
            {
                if (solovey != null)
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.tag = "Neutral";
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<NavMeshAgent>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<CivilianAI>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<SummonedAI>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Rigidbody>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<AudioSource>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponentInChildren<FractionTrigger>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<ConeOfView>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<DeclineAnimationScript>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Loot>());
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<BoxCollider>());
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsRunning", false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsStunned", false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsRunning", false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.transform.eulerAngles = new Vector3(0, -243, 0);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.transform.position = new Vector3(44.054f, 22.269f, 65.673f);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(true);
                }
                GameObject.Find("Cage 1").transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        if (questsData.description[2] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfGuardQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive = questsData.isActive[2];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[2];
            spawn.GetComponent<QuestSlot>().description = questsData.description[2];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[2];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[2];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[2];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[2]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[2];
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
            }
            for (int i = 0; i < 15; i++)
                if (questsData.goal[2, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[2, i]);
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive || GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage > 0)
                    if(watchTowerBandit1!=null)
                    watchTowerBandit1.SetActive(true);
            if (watchTowerBandit2 != null)
                watchTowerBandit2.SetActive(true);
            if (watchTowerBandit3 != null)
                watchTowerBandit3.SetActive(true);
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest") != null)
                if (spawn.GetComponent<QuestSlot>().questStage == 1)
                    headOfGuardQuest.currentQuestMarker = GameObject.Find("HeadOfGuard").GetComponent<QuestMarker>();
            if (questsData.questActivated[2])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfGuardQuest.questCompleted = questsData.questCompleted[2];
        if (headOfGuardQuest.questCompleted)
            if (watchTowerBandit1 != null)
                watchTowerBandit1.SetActive(true);
        if (watchTowerBandit2 != null)
            watchTowerBandit2.SetActive(true);
        if (watchTowerBandit3 != null)
            watchTowerBandit3.SetActive(true);
        if (questsData.description[3] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "LibrarianSurveyQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive = questsData.isActive[3];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[3];
            spawn.GetComponent<QuestSlot>().description = questsData.description[3];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[3];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[3];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[3];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[3]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[3];
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().librarianSurveyQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().librarianSurveyQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().librarianSurveyQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
            }
            for (int i = 0; i < 15; i++)
                if (questsData.goal[3, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[3, i]);
            if (questsData.questActivated[3])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        librarianSurveyQuest.questCompleted = questsData.questCompleted[3];
        if (questsData.description[4] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfVillageQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive = questsData.isActive[4];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[4];
            spawn.GetComponent<QuestSlot>().description = questsData.description[4];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[4];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[4];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[4];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[4]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[4];
            for (int i = 0; i < 15; i++)
                if (questsData.goal[4, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[4, i]);
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfVillageQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfVillageQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfVillageQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
            }
            if (questsData.questActivated[4])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfVillageQuest.questCompleted = questsData.questCompleted[4];
        if (questsData.description[5] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfHuntersQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive = questsData.isActive[5];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[5];
            spawn.GetComponent<QuestSlot>().description = questsData.description[5];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[5];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[5];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[5];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[5]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[5];
            for (int i = 0; i < 15; i++)
                if (questsData.goal[5, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[5, i]);
            if (specialMushroom != null)
                specialMushroom.SetActive(true);
            if (spawn.GetComponent<QuestSlot>().questStage == 1)
                if (GameObject.Find("HeadOfHunters") != null)
                    headOfHuntersQuest.currentQuestMarker = GameObject.Find("HeadOfHunters").GetComponent<QuestMarker>();
                else
                    headOfHuntersQuest.currentQuestMarker = null;
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfHuntersQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfHuntersQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfHuntersQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
            }
            if (questsData.questActivated[5])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfHuntersQuest.questCompleted = questsData.questCompleted[5];
        if (questsData.description[6] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRepublicansQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = questsData.isActive[6];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[6];
            spawn.GetComponent<QuestSlot>().description = questsData.description[6];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[6];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[6];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[6];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[6]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[6];
            if (spawn.GetComponent<QuestSlot>().questStage >= 1)
            {
                royalistPatrol1.SetActive(true);
                royalistPatrol2.SetActive(true);
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 1)
                headOfRepublicansQuest.currentQuestMarker = GameObject.Find("PatrolRoyalistPlace").GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 2)
                headOfRepublicansQuest.currentQuestMarker = GameObject.Find("PatrolRoyalist2").GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 3)
                headOfRepublicansQuest.currentQuestMarker = GameObject.Find("HeadOfRoyalists").GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 4)
                headOfRepublicansQuest.currentQuestMarker = GameObject.Find("HeadOfRepublican").GetComponent<QuestMarker>();
            for (int i = 0; i < 15; i++)
                if (questsData.goal[6, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[6, i]);
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
            }
            if (questsData.questActivated[6])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfRepublicansQuest.questCompleted = questsData.questCompleted[6];
        if (questsData.description[7] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRepublicansSecondQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = questsData.isActive[7];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[7];
            spawn.GetComponent<QuestSlot>().description = questsData.description[7];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[7];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[7];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[7];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[7]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[7];
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                headOfRepublicansSecondQuest.currentQuestMarker = GameObject.Find("RoyalistCamp").GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 2)
                headOfRepublicansSecondQuest.currentQuestMarker = headOfRepublicans.GetComponent<QuestMarker>();
            for (int i = 0; i < 15; i++)
                if (questsData.goal[7, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[7, i]);
            if (GameObject.Find("Republican3") != null)
                Destroy(GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>());
            if (GameObject.Find("Republican4") != null)
                Destroy(GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>());
            if (spawn.GetComponent<QuestSlot>().questStage > 0)
                GameObject.Find("StrangeRoyalist").tag = "Republican";
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansSecondQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansSecondQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansSecondQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
            }
            if (questsData.questActivated[7])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfRepublicansSecondQuest.questCompleted = questsData.questCompleted[7];
        if (questsData.description[8] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRoyalistsQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive = questsData.isActive[8];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[8];
            spawn.GetComponent<QuestSlot>().description = questsData.description[8];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[8];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[8];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[8];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[8]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[8];
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 1)
                headOfRoyalistsQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 2)
                headOfRoyalistsQuest.currentQuestMarker = strangeRoyalist.GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 3)
                headOfRoyalistsQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
            for (int i = 0; i < 15; i++)
                if (questsData.goal[8, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[8, i]);
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
            }
            if (questsData.questActivated[8])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfRoyalistsQuest.questCompleted = questsData.questCompleted[8];
        if (questsData.description[9] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRoyalistsSecondQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = questsData.isActive[9];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[9];
            spawn.GetComponent<QuestSlot>().description = questsData.description[9];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[9];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[9];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[9];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[9]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[9];
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                headOfRoyalistsSecondQuest.currentQuestMarker = GameObject.Find("RepublicansCamp").GetComponent<QuestMarker>();
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 2)
                headOfRoyalistsSecondQuest.currentQuestMarker = headOfRoyalists.GetComponent<QuestMarker>();
            for (int i = 0; i < 15; i++)
                if (questsData.goal[9, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[9, i]);
            if (GameObject.Find("Royalist3") != null)
                Destroy(GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>());
            if (GameObject.Find("Royalist4") != null)
                Destroy(GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>());
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsSecondQuestMageChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsSecondQuestPaladinChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward = 0;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
            }
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsSecondQuestRobberChoosed)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward *= 2;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
            }
            if (questsData.questActivated[9])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        headOfRoyalistsSecondQuest.questCompleted = questsData.questCompleted[9];
        if (questsData.description[10] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "PaladinSpecialQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.isActive = questsData.isActive[10];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[10];
            spawn.GetComponent<QuestSlot>().description = questsData.description[10];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[10];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[10];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[10];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[10]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[10];
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage == 1)
            {
                paladinSpecialQuest.currentQuestMarker = artelitStone.GetComponent<QuestMarker>();
                artelitStone.SetActive(true);
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage == 2)
                paladinSpecialQuest.currentQuestMarker = GameObject.Find("Priest").GetComponent<QuestMarker>();
            for (int i = 0; i < 15; i++)
                if (questsData.goal[10, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[10, i]);
            if (questsData.questActivated[10])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        paladinSpecialQuest.questCompleted = questsData.questCompleted[10];
        if (questsData.description[11] != null)
        {
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "LibrarianSpecialQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.isActive = questsData.isActive[11];
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questName;
            spawn.GetComponentInChildren<Text>().text = questsData.questName[11];
            spawn.GetComponent<QuestSlot>().description = questsData.description[11];
            spawn.GetComponent<QuestSlot>().goldReward = questsData.goldReward[11];
            spawn.GetComponent<QuestSlot>().experienceReward = questsData.experienceReward[11];
            spawn.GetComponent<QuestSlot>().prestigeReward = questsData.prestigeReward[11];
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(questsData.objectRewardName[11]);
            spawn.GetComponent<QuestSlot>().questStage = questsData.questStage[11];
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage == 1)
                librarianSpecialQuest.currentQuestMarker = librarian.GetComponent<QuestMarker>();
            for (int i = 0; i < 15; i++)
                if (questsData.goal[11, i] != null)
                    spawn.GetComponent<QuestSlot>().goal.Add(questsData.goal[11, i]);
            if (questsData.questActivated[11])
            {
                spawn.GetComponentInChildren<Image>().color = Color.red;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker != null)
                {
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.currentQuestMarker;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                }
                else
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
            }
        }
        librarianSpecialQuest.questCompleted = questsData.questCompleted[11];
    }
    private void LoadQuestsReward()
    {
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfHuntersQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfHuntersQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfHuntersQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfVillageQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfVillageQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfVillageQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().librarianSurveyQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().librarianSurveyQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().librarianSurveyQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().fayeQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().fayeQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().fayeQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansSecondQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansSecondQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRepublicansSecondQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward *= 2;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsSecondQuestMageChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints++;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsSecondQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward = 0;
        }
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfRoyalistsSecondQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward *= 2;
        }
    }
}
