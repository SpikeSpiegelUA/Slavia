using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GuardAI : MonoBehaviour
{
    public string ID;
    public bool isAttacking = false;
    public GameObject objectToAttack;
    public bool isAlerted;
    public int damage = 10;
    public int guardHP;
    public int currentHP = 1;
    public Vector3 startPosition;
    public Vector3 startRotation;
    public bool plusToCount = false;
    public bool block;
    public bool isArcher;
    public bool seeEnemy;
    public bool startCancelCoroutine = false;
    public GameManager gameManager;
    public int detection;
    public bool twoHand;
    public double armor;
    public bool isMage;
    public GameObject mageSummoned;
    public string spellName;
    public ConeOfView coneOfView;
    Animator animator;
    private Vector3 loadPosition = new Vector3(-100000, 0, 0);
    private Vector3 loadRotation = new Vector3(-100000, 0, 0);
    private bool loadedPosition = false;
    private bool loadedRotation = false;
    public int experience;
    private NavMeshAgent selfAgent;
    private MeshRenderer weapon;
    private MeshRenderer arrow;
    private CitizenAIPatrol selfPatrol;
    private AudioSource selfAudio;
    private PlayerController enemyPlayerController;
    private CivilianAI enemyCivilianAI;
    private SummonedAI enemySummonedAI;
    private GuardAI enemyGuardAI;
    void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
        GetComponent<NavMeshAgent>().enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (SaveLoad.isLoading)
        {
            LoadLoot();
            LoadObjectToAttack();
            LoadHP();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
        {
            if (GameObject.Find("Royalist3") != null)
                Destroy(GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>());
            if (GameObject.Find("Royalist4") != null)
                Destroy(GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>());
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
        {
            if (GameObject.Find("Republican3") != null)
                Destroy(GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>());
            if (GameObject.Find("Republican4") != null)
                Destroy(GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>());
        }
        if(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().villageGuardStageOne)
            if (GameObject.Find("TwoHand Guard") != null)
                Destroy(GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>());
        if (name == "StrangeRoyalist")
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage > 0)
                    tag = "Republican";
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                tag = "Republican";
        }
        selfAudio=GetComponent<AudioSource>();
        selfPatrol = GetComponent<CitizenAIPatrol>();
        arrow = FindArrowMeshRenderer(gameObject);
        weapon = FindWeapon(gameObject);
        selfAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        coneOfView = GetComponent<ConeOfView>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!SaveLoad.isLoading)
        {
            startPosition = this.transform.position;
            startRotation = this.transform.localEulerAngles;
            currentHP = guardHP;
        }
        if (selfPatrol == null)
        {
            if (armor<50)
                selfAgent.speed = gameManager.stableLightRunningSpeed;
            else
                selfAgent.speed = gameManager.stableHardRunningSpeed;
        }
        else
            selfAgent.speed = gameManager.stableWalkingSpeed;
        if (objectToAttack != null)
        {
            if (armor < 50)
                selfAgent.speed = gameManager.stableLightRunningSpeed;
            else
                selfAgent.speed = gameManager.stableHardRunningSpeed;
        }
        if (SaveLoad.isLoading)
            LoadGuardData();
        selfAgent.enabled = true;
        if (name == "SoloveyBandit" && SaveLoad.isLoading)
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 5 && selfAgent.destination.x != startPosition.x && (startPosition - transform.position).magnitude > 1 && !GetComponent<Animator>().GetBool("IsStunned") && currentHP > 0 && !isAlerted)
                {
                    GetComponent<Animator>().SetBool("IsRunning", true);
                    GetComponent<Animator>().Play("Run");
                    selfAgent.SetDestination(startPosition);
                }
            }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
            if (name == "Republican3")
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 0)
                    GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(158.65f, 22.32f, -9.33f);
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                    GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(152.4f, 22.32f, -48.69f);
                GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
            GameObject.Find("Republican3").GetComponent<NavMeshAgent>().speed = 4;
        }
        if(GameObject.Find("Republican3")!=null)
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy&&GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
        {
                if (GameObject.Find("Republican3").GetComponent<GuardAI>().currentHP > 0)
                {
                    if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                    {
                        GameObject.Find("Republican3").GetComponent<NavMeshAgent>().enabled = true;
                        GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(29.54f, 21.50507f, -58.65f);
                        GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
                        GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
                        GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
                        GameObject.Find("Republican3").GetComponent<NavMeshAgent>().speed = 4;
                        if (GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>() != null)
                            Destroy(GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>());
                    }
                    if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().armyStageTwo)
                    {
                        GameObject.Find("Republican3").GetComponent<NavMeshAgent>().enabled = true;
                        GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition = new Vector3(71.35f, 22.32f, -81.22f);
                        GameObject.Find("Republican3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican3").GetComponent<GuardAI>().startPosition);
                        GameObject.Find("Republican3").GetComponent<Animator>().SetBool("IsRunning", true);
                        GameObject.Find("Republican3").GetComponent<Animator>().Play("Run");
                        GameObject.Find("Republican3").GetComponent<NavMeshAgent>().speed = 4;
                        if (GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>() != null)
                            Destroy(GameObject.Find("Republican3").GetComponent<CitizenAIPatrol>());
                    }
                }
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
            if (name == "Republican4")
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 0)
                    GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(154.47f, 22.09f, -9.1f);
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                    GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(41.2506f, 22.09f, 160.5618f);
                GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
            GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
            GameObject.Find("Republican4").GetComponent<NavMeshAgent>().speed = 4;
        }
        if(GameObject.Find("Republican4")!=null)
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
        {
            if (GameObject.Find("Republican4").GetComponent<GuardAI>().currentHP > 0)
            {
                if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                {
                    GameObject.Find("Republican4").GetComponent<NavMeshAgent>().enabled = true;
                    GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(24.36f, 21.42112f, -57.78f);
                        GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                    GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
                    GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
                    GameObject.Find("Republican4").GetComponent<NavMeshAgent>().speed = 4;
                    if (GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>() != null)
                        Destroy(GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>());
                }
                if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().armyStageTwo)
                {
                    GameObject.Find("Republican4").GetComponent<NavMeshAgent>().enabled = true;
                    GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition = new Vector3(68.01f, 22.32f, -80.53f);
                    GameObject.Find("Republican4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Republican4").GetComponent<GuardAI>().startPosition);
                    GameObject.Find("Republican4").GetComponent<Animator>().SetBool("IsRunning", true);
                    GameObject.Find("Republican4").GetComponent<Animator>().Play("Run");
                    GameObject.Find("Republican4").GetComponent<NavMeshAgent>().speed = 4;
                    if (GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>() != null)
                        Destroy(GameObject.Find("Republican4").GetComponent<CitizenAIPatrol>());
                }
            }
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
            if (name == "Royalist3")
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 0)
                        GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(104.6f, 22.32f, 145.2f);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                        GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(56.18795f, 22.32f, 160.3629f);
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
                GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().speed = 4;
            }
        if(GameObject.Find("Royalist3")!=null)
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
        {
            if (GameObject.Find("Royalist3").GetComponent<GuardAI>().currentHP > 0)
            {
                if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                {
                    GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().enabled = true;
                    GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(29.54f, 21.50507f, -58.65f);
                    GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                    GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                    GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
                    GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().speed = 4;
                    if (GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>() != null)
                        Destroy(GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>());
                }
                if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().armyStageTwo)
                {
                    GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().enabled = true;
                    GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition = new Vector3(71.35f, 22.32f, -81.22f);
                    GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist3").GetComponent<GuardAI>().startPosition);
                    GameObject.Find("Royalist3").GetComponent<Animator>().SetBool("IsRunning", true);
                    GameObject.Find("Royalist3").GetComponent<Animator>().Play("Run");
                    GameObject.Find("Royalist3").GetComponent<NavMeshAgent>().speed = 4;
                    if (GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>() != null)
                        Destroy(GameObject.Find("Royalist3").GetComponent<CitizenAIPatrol>());
                }
            }
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
            if (name == "Royalist4")
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 0)
                        GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(98, 22.09f, 141.6f);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                        GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(41.2506f, 22.09f, 160.5618f);
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition);
                GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
                GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().speed = 4;
            }
        if(GameObject.Find("Royalist4")!=null)
        if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
        {
            if (GameObject.Find("Royalist4").GetComponent<GuardAI>().currentHP > 0)
            {
                if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                {
                    GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().enabled = true;
                    GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(24.36f, 21.42112f, -57.78f);
                        GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition);
                    GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                    GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
                    GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().speed = 4;
                    if (GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>() != null)
                        Destroy(GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>());
                }
                if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().armyStageTwo)
                {
                    GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().enabled = true;
                    GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition = new Vector3(68.01f, 22.32f, -80.53f);
                    GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Royalist4").GetComponent<GuardAI>().startPosition);
                    GameObject.Find("Royalist4").GetComponent<Animator>().SetBool("IsRunning", true);
                    GameObject.Find("Royalist4").GetComponent<Animator>().Play("Run");
                    GameObject.Find("Royalist4").GetComponent<NavMeshAgent>().speed = 4;
                    if (GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>() != null)
                        Destroy(GameObject.Find("Royalist4").GetComponent<CitizenAIPatrol>());
                }
            }
        }
        if(GameObject.Find("TwoHand Guard")!=null)
        if (name == "TwoHand Guard")
        {
                if (GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().currentHP > 0)
                {
                    if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                    {
                        GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().enabled = true;
                        GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition = new Vector3(25.82f, 21.33112f, -60.04f);
                        GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition);
                        GameObject.Find("TwoHand Guard").GetComponent<Animator>().SetBool("IsRunning", true);
                        GameObject.Find("TwoHand Guard").GetComponent<Animator>().Play("Run");
                        GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().speed = 4;
                        if (GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>() != null)
                            Destroy(GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>());
                    }
                    if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().armyStageTwo)
                    {
                        GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().enabled = true;
                        GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition = new Vector3(68.01f, 22.32f, -80.53f);
                        GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("TwoHand Guard").GetComponent<GuardAI>().startPosition);
                        GameObject.Find("TwoHand Guard").GetComponent<Animator>().SetBool("IsRunning", true);
                        GameObject.Find("TwoHand Guard").GetComponent<Animator>().Play("Run");
                        GameObject.Find("TwoHand Guard").GetComponent<NavMeshAgent>().speed = 4;
                        if (GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>() != null)
                            Destroy(GameObject.Find("TwoHand Guard").GetComponent<CitizenAIPatrol>());
                    }
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToAttack != null)
        {
            if (objectToAttack.GetComponent<PlayerController>() != null)
            {
                enemyPlayerController = objectToAttack.GetComponent<PlayerController>();
                enemySummonedAI = null;
                enemyGuardAI = null;
                enemyCivilianAI = null;
            }
            else if (objectToAttack.GetComponent<GuardAI>() != null)
            {
                enemyGuardAI = objectToAttack.GetComponent<GuardAI>();
                enemySummonedAI = null;
                enemyPlayerController = null;
                enemyCivilianAI = null;
            }
            else if (objectToAttack.GetComponent<CivilianAI>() != null)
            {
                enemyCivilianAI = objectToAttack.GetComponent<CivilianAI>();
                enemySummonedAI = null;
                enemyGuardAI = null;
                enemyPlayerController = null;
            }
            else if (objectToAttack.GetComponent<SummonedAI>() != null)
            {
                enemySummonedAI = objectToAttack.GetComponent<SummonedAI>();
                enemyPlayerController = null;
                enemyGuardAI = null;
                enemyCivilianAI = null;
            }
        }
        if (loadPosition.x != -100000&& !loadedPosition)
            if (transform.position.x != loadPosition.x)
            {
                loadedPosition = true;
                transform.position = loadPosition;
            }
        if (loadRotation.x != -100000 && !loadedRotation)
            if (transform.eulerAngles.x != loadRotation.x)
            {
                loadedRotation = true;
                transform.eulerAngles = loadRotation;
            }
      selfAgent.enabled = true;
        if (objectToAttack!=null)
        if (objectToAttack.tag == tag)
            objectToAttack = null;
        if (isMage)
            if (objectToAttack == null && mageSummoned != null && !SaveLoad.isLoading)
                mageSummoned.GetComponent<SummonedAI>().currentHP = 0;
        //Disable scripts and play death animation if hp<0
        if (currentHP <= 0&&!animator.GetBool("IsDead"))
        {
            if (name == "HeadOfGuard")
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                {
                    GameObject.Find("DialogueManager").GetComponent<DialogueManager>().headOfGuardKilled = true;
                    if (GameObject.Find("HeadOfBandit") != null)
                        if (GameObject.Find("HeadOfBandit").GetComponent<GuardAI>().currentHP > 0)
                        {
                            GameObject.Find("HeadOfBandit").GetComponent<GuardAI>().startPosition = new Vector3(11.91f, 22.345f, -53.4f);
                            GameObject.Find("HeadOfBandit").GetComponent<GuardAI>().startRotation = new Vector3(0, 212.798f, 0);
                            GameObject.Find("HeadOfBandit").GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HeadOfBandit").GetComponent<GuardAI>().startPosition);
                            GameObject.Find("HeadOfBandit").GetComponent<Animator>().SetBool("IsRunning", true);
                            GameObject.Find("HeadOfBandit").GetComponent<Animator>().Play("Run");
                        }
                }
            }
            if (name == "StrangeRoyalist")
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker == GetComponent<QuestMarker>())
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker = null;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[0] = "Get orders";
                }
            if (name == "HeadOfBandit" && !gameManager.headOfBanditKilled)
            {
                gameManager.headOfBanditKilled = true;
                if (gameManager.headOfBanditKilled)
                {
                    gameManager.player.prestige = 100;
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[18] = "The head of bandits is destroyed. And the handful of deserters which remained, cost nothing without it. Now it is possible to have a rest a little";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[18] = "The bandits are destroyed? Now we can relax";
                    if(GameObject.Find("Village Merchant")!=null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[17] = "Thank you for destroying the bandits. Now I can leave the valley";
                    if(GameObject.Find("Hunter")!=null)
                    GameObject.Find("Hunter").GetComponent<Dialogue>().sentences[4] = "We took revenge on Brad";
                    if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy)
                    {
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
                            GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[7] = "The bandits were destroyed. We helped ordinary people. That's good";
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                            GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[7] = "Deserters must be destroyed";
                    }
                }
            }
            ChangeLayer(gameObject);
            gameObject.layer = 21;
            selfAgent.radius = 0;
            if (name == "Royalist1" || name == "Royalist2" || name == "Royalist3" || name == "Royalist4" || name == "HeadOfRoyalists")
                GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRoyalists++;
            if (name == "Republican1" || name == "Republican2" || name == "Republican3" || name == "Republican4" || name == "HeadOfRepublican")
                GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRepublicans++;
            if (name == "UndeadMelee1" || name == "UndeadMelee3" || name == "UndeadMelee2" || name == "UndeadMage" || name == "UndeadArcher")
                GameObject.Find("QuestManager").GetComponent<QuestManager>().killedWarriors++;
            if (name == "PatrolRoyalist1" || name == "PatrolRoyalist2")
                GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRoyalistPatrol++;
            GetComponent<Animator>().speed = 1.0f;
            if (name == "WatchtowerBandit"|| name == "WatchtowerBandit2"|| name == "WatchtowerBandit3")
                GameObject.Find("QuestManager").GetComponent<QuestManager>().killedBanditsHeadOfGuardQuest++;
            if (name == "UndeadArcher (3)"|| name == "UndeadMelee3 (5)" || name == "UndeadMelee3 (4)" || name == "UndeadMelee3 (3)" || name == "UndeadMelee3 (2)" || name == "UndeadArcher (2)" || name == "UndeadMage (1)" || name == "UndeadArcher (1)" || name == "UndeadMelee3 (1)")
                gameManager.killedDungeon++;
            selfAudio.clip = null;
            selfAudio.Play();
            if (!isArcher)
            {
                GetComponent<BoxCollider>().size = new Vector3(1.88f, 0.6f, 2.11f);
                GetComponent<BoxCollider>().center = new Vector3(-0.22f, 0.33f, -1.33f);
            }
            if (isArcher||isMage)
            {
                if (isArcher&& FindArrowMeshRenderer(gameObject).enabled)
                    FindArrowMeshRenderer(gameObject).enabled = false;
                GetComponent<BoxCollider>().center = new Vector3(-0.68f, 0.06f, -0.2f);
                GetComponent<BoxCollider>().size = new Vector3(2.15f, 0.2f, 1.54f);
            }
            if (selfPatrol!=null)
            {
                this.selfPatrol.enabled = false;
                animator.SetBool("IsWalking", false);
                selfPatrol.StopCoroutine("PatrolAI");
            }
            if(GetComponentInChildren<FractionTrigger>()!=null)
            GetComponentInChildren<FractionTrigger>().enabled = false;
            selfAgent.isStopped = true;
            if (plusToCount)
                gameManager.player.combatEnemies--;
            currentHP = 0;
            enabled = false;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", false);
            if (!isArcher && !isMage)
            {
                animator.SetBool("IsBlocking", false);
                animator.SetBool("IsHugeAttacking", false);
            }
            animator.SetBool("IsStunned", false);
            animator.SetBool("IsDead", true);
            animator.Play("Death");
            StopAllCoroutines();
            StartCoroutine("DestroyDelay");
        }
        if (currentHP > 0)
        {
            if (!isMage)
            {
                if(weapon!=null){
                    if (isAlerted)
                        if (!weapon.enabled)
                            weapon.enabled = true;
                    if (selfPatrol!= null)
                        if (selfPatrol.enabled && weapon.enabled)
                            weapon.enabled = false;
                }
            }
            if (isMage)
                if (objectToAttack == null && mageSummoned != null && !SaveLoad.isLoading)
                    mageSummoned.GetComponent<SummonedAI>().currentHP = 0;
            if (isMage)
                if (mageSummoned != null)
                    if (mageSummoned.GetComponent<SummonedAI>().currentHP <= 0)
                        mageSummoned = null;
            if (objectToAttack != null)
            {
                    if (enemyGuardAI != null)
                    {
                        if (enemyGuardAI.currentHP <= 0)
                        {
                        objectToAttack = null;
                        isAlerted = false;
                        isAttacking = false;
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("IsBlocking", false);
                        animator.SetBool("IsHugeAttacking", false);
                        animator.SetBool("IsStunned", false);
                        selfAgent.isStopped = false;
                        selfAgent.SetDestination(startPosition);
                            animator.SetBool("IsAttacking", false);
                        animator.speed = 1.0f;
                        animator.SetBool("IsRunning", true);
                        animator.Play("Run");
                        }
                    }
                 else   if (enemyPlayerController != null)
                    {
                        if (enemyPlayerController.currentHealth <= 0)
                        {
                        objectToAttack = null;
                        selfAgent.isStopped = false;
                        isAlerted = false;
                        isAttacking = false;
                        animator.SetBool("IsAttacking", false);
                            animator.SetBool("IsBlocking", false);
                            animator.SetBool("IsHugeAttacking", false);
                        animator.SetBool("IsStunned", false);
                        selfAgent.SetDestination(startPosition);
                            animator.SetBool("IsAttacking", false);
                        animator.speed = 1.0f;
                        animator.SetBool("IsRunning", true);
                        animator.Play("Run");
                    }
                    }
                 else   if (enemyCivilianAI != null)
                    {
                        if (enemyCivilianAI.currentHP <= 0)
                        {
                        objectToAttack = null;
                        isAlerted = false;
                        selfAgent.isStopped = false;
                        isAttacking = false;
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("IsBlocking", false);
                        animator.SetBool("IsHugeAttacking", false);
                        animator.SetBool("IsStunned", false);
                        selfAgent.SetDestination(startPosition);
                            animator.SetBool("IsAttacking", false);
                        animator.speed = 1.0f;
                        animator.SetBool("IsRunning", true);
                        animator.Play("Run");
                    }
                    }
                  else  if (enemySummonedAI != null)
                    {
                        if (enemySummonedAI.currentHP <= 0)
                        {
                        objectToAttack = null;
                        isAlerted = false;
                        isAttacking = false;
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("IsBlocking", false);
                        animator.SetBool("IsHugeAttacking", false);
                        animator.SetBool("IsStunned", false);
                        selfAgent.isStopped = false;
                        selfAgent.SetDestination(startPosition);
                            animator.SetBool("IsAttacking", false);
                        animator.speed = 1.0f;
                        animator.SetBool("IsRunning", true);
                        animator.Play("Run");
                    }
                    }
            }
            if (detection < 0)
                detection = 0;
            if (detection > 100)
                detection = 100;
            if (detection == 100)
            {
               gameManager.player.isDetected = true;
                gameManager.stealthBar.value = 100;
            }
            if (tag=="VillageGuard")
            if (gameManager.villageAttackedByPlayer && gameManager.player.isCrouched)
                coneOfView.ConeOfViewMake();
            if (tag == "Republican")
                if (gameManager.republicanAttackedByPlayer && gameManager.player.isCrouched)
                    coneOfView.ConeOfViewMake();
            if (tag == "Royalist")
                if (gameManager.royalistAttackedByPlayer && gameManager.player.isCrouched)
                    coneOfView.ConeOfViewMake();
            if (tag=="Bandit"||tag=="Undead")
                if (gameManager.player.isCrouched)
                    coneOfView.ConeOfViewMake();
            Movement();
            if(!isArcher&&!isMage)
            DamageControl();
            if (isMage && !isArcher)
                MageAttackControl();
            if (objectToAttack != null && (isArcher||isMage))
                MovementForArcher();
            //Change rotation if guard came to start position
            if (selfAgent.destination.x == startPosition.x && (startPosition - transform.position).magnitude <= 0.5f && currentHP > 0)
            {
                if(!isMage)
                    if(weapon!=null)
                if (weapon.enabled)
                    weapon.enabled = false;
                this.transform.localEulerAngles = startRotation;
                animator.SetBool("IsRunning", false);
                animator.Play("Idle");
                selfAudio.clip = null;
                selfAudio.Play();
                isAttacking = false;
            }
            //Attack if length to objectToAttack less than 3 for guard
            if (objectToAttack != null)
                if ((objectToAttack.transform.position - transform.position).magnitude <= 3 && !isArcher&&!isMage)
                    animator.SetBool("IsAttacking", true);
            //Attack if length to objectToAttack less than 8 for archer
            if (objectToAttack != null)
                if ((objectToAttack.transform.position - transform.position).magnitude <= 8 && (isArcher||isMage) && seeEnemy)
                {
                    if (!isAttacking)
                      isAttacking = true;
                   animator.SetBool("IsAttacking", true);
                    if (!isMage&&!arrow.enabled)
                        arrow.enabled = true;
                }
            if (objectToAttack != null)
                selfAgent.SetDestination(objectToAttack.transform.position);
        }
    }
    //Choose what type of action in fight will AI do
    public void DamageControl()
    {
        if (isAlerted&&objectToAttack!=null)
        {
            int attackChoose = -1;
            if (animator.GetBool("IsBlocking")==false)
                attackChoose = Random.Range(0, 11);
            LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard") & ~(1 << LayerMask.NameToLayer("SummonedArrow")));
            Ray infoRay = new Ray(selfAgent.transform.position + selfAgent.transform.up * 0.5f, selfAgent.transform.forward * 10);
            RaycastHit infoHit;
            if (Physics.Raycast(infoRay, out infoHit,Mathf.Infinity,layer))
            {
                if (!isArcher && isAlerted&&!isMage) {
                    if (infoHit.transform.root.gameObject.CompareTag("Player"))
                        if (objectToAttack.CompareTag("Player"))
                        {
                            PlayerController playerController = infoHit.transform.root.gameObject.GetComponent<PlayerController>();
                            //If attackChoose >3 and <11 play simple attack
                            if (attackChoose > 3 && attackChoose < 11)
                            {
                                if (playerController.currentHealth > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    animator.SetBool("IsAttacking", true);
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    animator.Play("Attack");
                                    isAttacking = true;
                                }
                            }
                            //if attackChoose 1 or 0 play huge attack
                            else if (attackChoose <= 1 && attackChoose >= 0)
                            {
                                if (playerController.currentHealth > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    animator.SetBool("IsHugeAttacking", true);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    animator.Play("HugeAttack");
                                    isAttacking = true;
                                }
                            }
                            //If attackChoose 3 pr 2 play block
                            else if (attackChoose <= 3 && attackChoose >= 2)
                            {
                                if (playerController.currentHealth > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    animator.SetBool("IsBlocking", true);
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.Play("Block");
                                    if (twoHand)
                                        StartCoroutine("TwoHandCancelBlock");
                                }
                            }
                        }
                    if (infoHit.transform.root.gameObject.CompareTag("Civilian")|| infoHit.transform.root.gameObject.CompareTag("SimplePeople")||infoHit.collider.transform.root.name=="Solovey")
                        if (objectToAttack.CompareTag("Civilian")||objectToAttack.CompareTag("SimplePeople")||infoHit.collider.transform.root.name=="Solovey")
                        {
                            CivilianAI civilianAI = infoHit.transform.root.gameObject.GetComponentInParent<CivilianAI>();
                            //If attackChoose >3 and <11 play simple attack
                            if (attackChoose > 3 && attackChoose < 11)
                            {
                                if (civilianAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    animator.SetBool("IsAttacking", true);
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    animator.Play("Attack");
                                    isAttacking = true;
                                }
                            }
                            //if attackChoose 1 or 0 play huge attack
                            else if (attackChoose <= 1 && attackChoose >= 0)
                            {
                                if (civilianAI.currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    animator.SetBool("IsHugeAttacking", true);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    animator.Play("HugeAttack");
                                    isAttacking = true;
                                }
                            }
                            //If attackChoose 3 pr 2 play block
                            else if (attackChoose <= 3 && attackChoose >= 2)
                            {
                                if (civilianAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    animator.SetBool("IsBlocking", true);
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.Play("Block");
                                    if (twoHand)
                                        StartCoroutine("TwoHandCancelBlock");
                                }
                            }
                        }
                    //Bandit damage
                    if (infoHit.transform.root.gameObject.CompareTag("VillageGuard"))
                            if (objectToAttack.CompareTag("VillageGuard"))
                            {
                            if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                GuardAI guardAI = infoHit.transform.root.gameObject.GetComponentInParent<GuardAI>();
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (guardAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsAttacking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("Attack");
                                        isAttacking = true;
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (guardAI.currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsHugeAttacking", true);
                                        animator.SetBool("IsAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("HugeAttack");
                                        isAttacking = true;
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (guardAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsBlocking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsAttacking", false);
                                        animator.Play("Block");
                                        if (twoHand)
                                            StartCoroutine("TwoHandCancelBlock");
                                    }
                                }
                            }
                            else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                            {
                                SummonedAI summonedAI = infoHit.transform.root.gameObject.GetComponentInParent<SummonedAI>();
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (summonedAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsAttacking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("Attack");
                                        isAttacking = true;
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (summonedAI.currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsHugeAttacking", true);
                                        animator.SetBool("IsAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("HugeAttack");
                                        isAttacking = true;
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (summonedAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsBlocking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsAttacking", false);
                                        animator.Play("Block");
                                        if (twoHand)
                                            StartCoroutine("TwoHandCancelBlock");
                                    }
                                }
                            }
                            }
                    if (infoHit.transform.root.gameObject.CompareTag("Bandit")|| infoHit.transform.root.gameObject.CompareTag("Undead")|| infoHit.transform.root.gameObject.CompareTag("Royalist")|| infoHit.transform.root.gameObject.CompareTag("Republican"))
                        if (objectToAttack.CompareTag("Bandit")|| objectToAttack.CompareTag("Undead")|| objectToAttack.CompareTag("Republican")|| objectToAttack.CompareTag("Royalist"))
                        {
                            if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                GuardAI guardAI = infoHit.transform.root.gameObject.GetComponentInParent<GuardAI>();
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (guardAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsAttacking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("Attack");
                                        isAttacking = true;
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (guardAI.currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsHugeAttacking", true);
                                        animator.SetBool("IsAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("HugeAttack");
                                        isAttacking = true;
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (guardAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsBlocking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsAttacking", false);
                                        animator.Play("Block");
                                        if (twoHand)
                                            StartCoroutine("TwoHandCancelBlock");
                                    }
                                }
                            }
                            else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                            {
                                SummonedAI summonedAI = infoHit.transform.root.gameObject.GetComponentInParent<SummonedAI>();
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (summonedAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsAttacking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("Attack");
                                        isAttacking = true;
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (summonedAI.currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsHugeAttacking", true);
                                        animator.SetBool("IsAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        animator.Play("HugeAttack");
                                        isAttacking = true;
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (summonedAI.currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        animator.SetBool("IsBlocking", true);
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsAttacking", false);
                                        animator.Play("Block");
                                        if (twoHand)
                                            StartCoroutine("TwoHandCancelBlock");
                                    }
                                }
                            }
                        }
                    //If object is killed go to start position and cancel alert
                    if (objectToAttack!=null)
                    if (objectToAttack.tag == "Summoned")
                        if (enemySummonedAI.currentHP <= 0)
                        {
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                            StopCoroutine("CancelAlert");
                            objectToAttack = null;
                            isAlerted = false;
                            isAttacking = false;
                            animator.SetBool("IsAttacking", false);
                            animator.Play("Run");
                            animator.SetBool("IsRunning", true);
                            if (currentHP > 0)
                                selfAgent.isStopped = false;
                            animator.SetBool("IsAttacking", false);
                            if (selfPatrol == true)
                            {
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    selfPatrol.enabled = true;
                                selfPatrol.StartCoroutine("PatrolAI");
                                animator.SetBool("IsWalking", true);
                                animator.SetBool("IsRunning", false);
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                selfAudio.loop = true;
                                selfAudio.Play();
                                animator.Play("Walk");
                                selfAgent.speed = gameManager.stableWalkingSpeed;
                            }
                            else
                                selfAgent.SetDestination(startPosition);
                        }
                    if (objectToAttack != null)
                        if (objectToAttack.tag == "Civilian"|| objectToAttack.tag == "SimplePeople"||objectToAttack.name=="Solovey")
                            if (enemyCivilianAI.currentHP <= 0)
                            {
                                GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                StopCoroutine("CancelAlert");
                                objectToAttack = null;
                                isAlerted = false;
                                isAttacking = false;
                                animator.SetBool("IsAttacking", false);
                                animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                animator.SetBool("IsAttacking", false);
                                selfAgent.SetDestination(startPosition);
                                animator.SetBool("IsAttacking", false);
                                animator.speed = 1.0f;
                                animator.SetBool("IsRunning", true);
                                animator.Play("Run");
                                if (selfPatrol == true)
                                {
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    selfPatrol.enabled = true;
                                    selfPatrol.StartCoroutine("PatrolAI");
                                    animator.SetBool("IsWalking", true);
                                    animator.SetBool("IsRunning", false);
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                    animator.Play("Walk");
                                    selfAgent.speed = gameManager.stableWalkingSpeed;
                                }
                                else
                                    selfAgent.SetDestination(startPosition);
                            }
                    if (objectToAttack != null)
                        if (objectToAttack.tag == "Bandit"|| objectToAttack.tag == "VillageGuard" || objectToAttack.tag == "Royalist" || objectToAttack.tag == "Republican" || objectToAttack.tag == "Undead") 
                        { 
                            if(enemyGuardAI!=null)
                            if (enemyGuardAI.currentHP <= 0)
                            {
                                GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                StopCoroutine("CancelAlert");
                                objectToAttack = null;
                                isAlerted = false;
                                isAttacking = false;
                                animator.SetBool("IsAttacking", false);
                                animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                                    selfAgent.SetDestination(startPosition);
                                    GetComponent<Animator>().SetBool("IsAttacking", false);
                                    GetComponent<Animator>().speed = 1.0f;
                                    GetComponent<Animator>().SetBool("IsRunning", true);
                                    GetComponent<Animator>().Play("Run");
                                    if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                animator.SetBool("IsAttacking", false);
                                if (selfPatrol == true)
                                {
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    selfPatrol.enabled = true;
                                    selfPatrol.StartCoroutine("PatrolAI");
                                    animator.SetBool("IsWalking", true);
                                    animator.SetBool("IsRunning", false);
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                    animator.Play("Walk");
                                    selfAgent.speed = gameManager.stableWalkingSpeed;
                                }
                                else
                                    selfAgent.SetDestination(startPosition);
                            }
                            if (objectToAttack != null)
                                if (enemySummonedAI != null)
                            {
                                if (enemySummonedAI.currentHP <= 0)
                                {
                                    GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                    StopCoroutine("CancelAlert");
                                    objectToAttack = null;
                                    isAlerted = false;
                                    isAttacking = false;
                                    animator.SetBool("IsAttacking", false);
                                        selfAgent.SetDestination(startPosition);
                                        GetComponent<Animator>().SetBool("IsAttacking", false);
                                        GetComponent<Animator>().speed = 1.0f;
                                        GetComponent<Animator>().SetBool("IsRunning", true);
                                        GetComponent<Animator>().Play("Run");
                                        animator.Play("Run");
                                    animator.SetBool("IsRunning", true);
                                    if (currentHP > 0)
                                        selfAgent.isStopped = false;
                                    animator.SetBool("IsAttacking", false);
                                    if (selfPatrol == true)
                                    {
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        selfPatrol.enabled = true;
                                        selfPatrol.StartCoroutine("PatrolAI");
                                        animator.SetBool("IsWalking", true);
                                        animator.SetBool("IsRunning", false);
                                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                        selfAudio.loop = true;
                                        selfAudio.Play();
                                        animator.Play("Walk");
                                        selfAgent.speed = gameManager.stableWalkingSpeed;
                                    }
                                    else
                                        selfAgent.SetDestination(startPosition);
                                }
                            }
                        }
                      if (objectToAttack!=null)
                    if (objectToAttack.tag == "Player")
                        if (enemyPlayerController.currentHealth <= 0)
                        {
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                            StopCoroutine("CancelAlert");
                            objectToAttack = null;
                            isAlerted = false;
                            isAttacking = false;
                            animator.SetBool("IsAttacking", false);
                                selfAgent.SetDestination(startPosition);
                                GetComponent<Animator>().SetBool("IsAttacking", false);
                                GetComponent<Animator>().speed = 1.0f;
                                GetComponent<Animator>().SetBool("IsRunning", true);
                                GetComponent<Animator>().Play("Run");
                                animator.Play("Run");
                            animator.SetBool("IsRunning", true);
                            if (currentHP > 0)
                                selfAgent.isStopped = false;
                            animator.SetBool("IsAttacking", false);
                            if (selfPatrol == true)
                            {
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    selfPatrol.enabled = true;
                                selfPatrol.StartCoroutine("PatrolAI");
                                animator.SetBool("IsWalking", true);
                                animator.SetBool("IsRunning", false);
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                selfAudio.loop = true;
                                selfAudio.Play();
                                animator.Play("Walk");
                                selfAgent.speed = gameManager.stableWalkingSpeed;
                            }
                            else
                                    if (selfAgent.enabled)
                                selfAgent.SetDestination(startPosition);
                        }
                }
            }
        }
    }
    //Make huge damage.Activated by animation event on attack2Readeble animation
    public void HugeDamage()
    {
        Ray infoRay = new Ray(selfAgent.transform.position + selfAgent.transform.up * 0.5f, selfAgent.transform.forward * 6);
        RaycastHit infoHit;
        LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard") & ~(1 << LayerMask.NameToLayer("SummonedArrow")));
        if (Physics.Raycast(infoRay, out infoHit, Mathf.Infinity, layer))
        {
            //Huge Damage for player
            if (infoHit.transform.root.gameObject.CompareTag("Player"))
                if (objectToAttack.CompareTag("Player"))
                {

                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.transform.root.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 4)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound&& selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit)
                        {
                            if (!twoHand)
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            else
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                            selfAudio.Play();
                            selfAudio.loop = false;
                        }
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                        {
                            if(GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType=="HardArmor")
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value++;
                            else if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
                        }
                        else if(GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item == null)
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
                        gameManager.player.GetComponent<Animator>().SetBool("IsAttacking", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsBlocking", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsRunning", false);
                        gameManager.player.GetComponent<AudioSource>().clip = null;
                        gameManager.player.GetComponent<AudioSource>().Play();
                        if(damage- infoHit.transform.root.GetComponent<PlayerController>().armor/100f*damage>0)
                            infoHit.transform.root.GetComponent<PlayerController>().currentHealth -= (int)(damage - infoHit.transform.root.GetComponent<PlayerController>().armor / 100f * damage)*2;
                        infoHit.transform.root.GetComponent<PlayerController>().StartCoroutine("DamageScreenAppear");
                        infoHit.transform.root.GetComponent<PlayerController>().block = false;
                        if(infoHit.transform.root.GetComponent<PlayerController>().currentHealth > 0)
                        infoHit.collider.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                    }
                }
            if (infoHit.transform.root.gameObject.CompareTag("VillageGuard") || infoHit.transform.root.gameObject.CompareTag("Bandit") || infoHit.transform.root.gameObject.CompareTag("Undead") || infoHit.transform.root.gameObject.CompareTag("Republican") || infoHit.transform.root.gameObject.CompareTag("Royalist"))
                if (objectToAttack.CompareTag("VillageGuard") || objectToAttack.CompareTag("Bandit") || objectToAttack.CompareTag("Undead") || objectToAttack.CompareTag("Republican") || objectToAttack.CompareTag("Royalist"))
                {
                    if (infoHit.transform.root.GetComponent<GuardAI>() != null)
                    {
                        //Play stun animation,disable scripts,make damage and play hit sound
                        if (infoHit.transform.root.GetComponent<GuardAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 4)
                        {
                            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound && selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit)
                            {
                                if (!twoHand)
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                                else
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                                selfAudio.Play();
                                selfAudio.loop = false;
                            }
                            gameManager.player.GetComponent<Animator>().SetBool("IsAttacking", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsBlocking", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsRunning", false);
                            gameManager.player.GetComponent<AudioSource>().clip = null;
                            gameManager.player.GetComponent<AudioSource>().Play();
                            if (damage - infoHit.transform.root.GetComponent<GuardAI>().armor / 100f * damage > 0)
                                infoHit.transform.root.GetComponent<GuardAI>().currentHP -= (int)(damage - infoHit.transform.root.GetComponent<GuardAI>().armor / 100f * damage) * 2;
                            infoHit.transform.root.GetComponent<GuardAI>().block = false;
                            if (infoHit.transform.root.GetComponent<GuardAI>().currentHP > 0)
                                infoHit.collider.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                            infoHit.collider.GetComponentInParent<Animator>().speed = 1.0f;
                        }
                    }
                    else if (infoHit.collider.GetComponentInParent<SummonedAI>()!= null)
                    {
                        //Play stun animation,disable scripts,make damage and play hit sound
                        if (infoHit.transform.root.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 4)
                        {
                            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound && selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit)
                            {
                                if (!twoHand)
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                                else
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                                selfAudio.Play();
                                selfAudio.loop = false;
                            }
                            gameManager.player.GetComponent<Animator>().SetBool("IsAttacking", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsBlocking", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                            gameManager.player.GetComponent<Animator>().SetBool("IsRunning", false);
                            gameManager.player.GetComponent<AudioSource>().clip = null;
                            gameManager.player.GetComponent<AudioSource>().Play();
                            infoHit.collider.GetComponentInParent<SummonedAI>().currentHP -= damage;
                            infoHit.collider.GetComponentInParent<SummonedAI>().block = false;
                            if (infoHit.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                                infoHit.collider.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                        }
                    }
                }
            if (infoHit.transform.root.gameObject.CompareTag("Civilian") || infoHit.transform.root.gameObject.CompareTag("SimplePeople")||infoHit.collider.transform.root.name=="Solovey")
                if (objectToAttack.CompareTag("Civilian") || objectToAttack.CompareTag("SimplePeople")||objectToAttack.name=="Solovey")
                {
                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.transform.root.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 4)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound && selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit)
                        {
                            if (!twoHand)
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            else
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                            selfAudio.Play();
                            selfAudio.loop = false;
                        }
                        gameManager.player.GetComponent<Animator>().SetBool("IsAttacking", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsBlocking", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                        gameManager.player.GetComponent<Animator>().SetBool("IsRunning", false);
                        gameManager.player.GetComponent<AudioSource>().clip = null;
                        gameManager.player.GetComponent<AudioSource>().Play();
                        infoHit.transform.root.GetComponent<CivilianAI>().currentHP -= damage * 2;
                        if (infoHit.transform.root.GetComponent<CivilianAI>().currentHP > 0)
                            infoHit.collider.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                    }
                }
            //Huge Damage for summoned
            if (infoHit.transform.root.gameObject.CompareTag("Summoned"))
                if (objectToAttack.CompareTag("Summoned"))
                {
                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.transform.root.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 4)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound&& selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit)
                        {
                            if (!twoHand)
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            else
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                            selfAudio.Play();
                            selfAudio.loop = false;
                        }
                        infoHit.transform.root.GetComponent<Animator>().SetBool("IsAttacking", false);
                        if (!infoHit.transform.root.GetComponent<SummonedAI>().isArcher)
                        {
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsBlocking", false);
                        }
                        infoHit.transform.root.GetComponent<Animator>().SetBool("IsRunning", false);
                        infoHit.transform.root.GetComponent<AudioSource>().clip = null;
                        infoHit.transform.root.GetComponent<AudioSource>().Play();
                        infoHit.transform.root.GetComponent<SummonedAI>().currentHP -= damage * 2;
                        infoHit.transform.root.GetComponent<SummonedAI>().block = false;
                        if(infoHit.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                        infoHit.collider.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                    }
                }
        }
    }
    //Make simple damage.Activated by animation event on attack1Readeble animation
    public void Damage()
    {
        Ray infoRay = new Ray(selfAgent.transform.position + selfAgent.transform.up * 0.5f, selfAgent.transform.forward * 6);
        LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard") & ~(1 << LayerMask.NameToLayer("SummonedArrow")));
        RaycastHit infoHit;
        if (Physics.Raycast(infoRay, out infoHit, Mathf.Infinity, layer))
        
            if (infoHit.collider.transform.root.gameObject.CompareTag("Player"))
                if (objectToAttack.CompareTag("Player"))
                {
                    //If player isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 4 && (!enemyPlayerController.block || enemyPlayerController.currentStamina < 10))
                    {
                        if (damage - infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().armor / 100f * damage > 0)
                            infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().currentHealth -= (int) (damage - infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().armor / 100f * damage);
                        infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().StartCoroutine("DamageScreenAppear");
                        if (!twoHand)
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                        else
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                        selfAudio.loop = false;
                        selfAudio.Play();
                    }
                    //If player is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                    if (infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().block && infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().currentStamina >= 10)
                    {
                        infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().currentStamina -= 10;
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                        selfAudio.loop = false;
                        selfAudio.Play();
                    }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value++;
                        else if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
                    }
                    else if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item == null)
                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
                }
        if (infoHit.collider.transform.root.gameObject.CompareTag("Bandit")|| infoHit.collider.transform.root.gameObject.CompareTag("VillageGuard") || infoHit.collider.transform.root.gameObject.CompareTag("Republican") || infoHit.collider.transform.root.gameObject.CompareTag("Royalist") || infoHit.collider.transform.root.gameObject.CompareTag("Undead"))
            if (objectToAttack.CompareTag("Bandit")|| objectToAttack.CompareTag("VillageGuard") || objectToAttack.CompareTag("Undead") || objectToAttack.CompareTag("Royalist") || objectToAttack.CompareTag("Republican"))
            {
                if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                {
                    //If player isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 4 && !infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().block)
                    {
                        if (damage - infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().armor / 100f * damage > 0)
                            infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().currentHP -= (int)(damage - infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().armor / 100f * damage);
                        if (!twoHand)
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                        else
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                        selfAudio.loop = false;
                        selfAudio.Play();
                    }
                    //If player is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                    if (infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().block)
                    {
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                        selfAudio.loop = false;
                        selfAudio.Play();
                    }
                }
                else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                {
                    //If player isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 4 && !infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().block)
                    {
                        infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().currentHP -= damage;
                        infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().StartCoroutine("DamageScreenAppear");
                        if (!twoHand)
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                        else
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                        selfAudio.loop = false;
                        selfAudio.Play();
                    }
                    //If player is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                    if (infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().block)
                    {
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                        selfAudio.loop = false;
                        selfAudio.Play();
                    }
                }
            }
        if (infoHit.collider.transform.root.gameObject.CompareTag("Civilian") || infoHit.collider.transform.root.gameObject.CompareTag("SimplePeople")||infoHit.collider.transform.root.name=="Solovey")
            if (objectToAttack.CompareTag("SimplePeople") || objectToAttack.CompareTag("Civilian")||objectToAttack.name=="Solovey")
            {
                //If player isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                if (infoHit.collider.transform.root.gameObject.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 3)
                {
                    infoHit.collider.transform.root.gameObject.GetComponent<CivilianAI>().currentHP -= damage;
                    if (!twoHand)
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                    else
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                    selfAudio.loop = false;
                    selfAudio.Play();
                }
            }
        if (infoHit.collider.transform.root.gameObject.CompareTag("Summoned"))
            if (objectToAttack.CompareTag("Summoned"))
            {
                //If summoned isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                if (infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 4 && !infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().block)
                {
                    infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().currentHP -= damage;
                    if (!twoHand)
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                    else
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                    selfAudio.loop = false;
                    selfAudio.Play();
                }
                //If summoned is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                if (infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().block)
                {
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                    selfAudio.loop = false;
                    selfAudio.Play();
                }
            }
    }
    //Activated on each attack animation.Prevent to attack before attack animation end
    public void SetIsAttacking()
    { 
        isAttacking = false;
        if(twoHand)
        GetComponent<Animator>().SetBool("IsAttacking", false);
    }
    //Movement script
    public void Movement()
    {
        if (isAlerted && objectToAttack != null)
        {
            Vector3 rayStart = transform.position + transform.up;
            if (gameManager.player.isCrouched)
                rayStart = transform.position + transform.up * 0.4f;
            Ray ray = new Ray(rayStart, objectToAttack.transform.position - transform.position);

            LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard")) & ~(1 << LayerMask.NameToLayer("SummonedArrow"))& ~(1 << LayerMask.NameToLayer("TriggerFraction"));
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer))
            {
                if (selfPatrol != null && selfPatrol.enabled == true)
                {
                    if (armor < 50)
                        selfAgent.speed = gameManager.stableLightRunningSpeed;
                    else
                        selfAgent.speed = gameManager.stableHardRunningSpeed;
                    selfPatrol.StopCoroutine("PatrolAI");
                    selfPatrol.enabled = false;
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", true);
                    animator.Play("Run");
                }
                if (startCancelCoroutine == false)
                {
                    startCancelCoroutine = true;
                    StartCoroutine("CancelAlert");
                }
                selfAgent.SetDestination(objectToAttack.transform.position);
                //If length between player and guard more than 2.5 guard will run
                if (!isArcher && currentHP > 0 && !isMage)
                {
                    if ((objectToAttack.transform.position - transform.position).magnitude > 2.2f||rayHit.collider.transform.root.gameObject!=objectToAttack)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                            selfAudio.loop = true;
                            selfAudio.Play();
                        }
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("IsBlocking", false);
                        animator.SetBool("IsHugeAttacking", false);
                        block = false;
                        animator.Play("Run");
                        animator.SetBool("IsRunning", true);
                        animator.speed = 1;
                        isAttacking = false;
                        if (currentHP > 0)
                            selfAgent.isStopped = false;
                        selfAgent.SetDestination(objectToAttack.transform.position);
                    }
                    //If length between player and guard lesttthan 2.5 guard attack player
                    else if ((objectToAttack.transform.position - transform.position).magnitude <= 2f&&rayHit.collider.transform.root.gameObject==objectToAttack)
                    {
                        animator.SetBool("IsRunning", false);
                        if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                        {
                            selfAudio.clip = null;
                            selfAudio.loop = false;
                        }
                        selfAgent.isStopped = true;
                        RotateTowards(objectToAttack.transform);
                    }
                }
            }
        }

    }


    private void MovementForArcher()
    {
        if (isAlerted && objectToAttack != null)
        {
            if (selfPatrol != null && selfPatrol.enabled == true)
            {
                if (armor<50)
                    selfAgent.speed = gameManager.stableLightRunningSpeed;
                else
                    selfAgent.speed = gameManager.stableHardRunningSpeed;
                selfPatrol.StopCoroutine("PatrolAI");
                selfPatrol.enabled = false;
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", true);
                animator.Play("Run");
            }
            if (isArcher||isMage)
            {
                if (startCancelCoroutine == false)
                {
                    startCancelCoroutine = true;
                    StartCoroutine("CancelAlert");
                }
                Vector3 rayDirection = (objectToAttack.transform.position + objectToAttack.transform.up * 1.5f) - transform.Find("RayStart").transform.position;
                if (gameManager.player.isCrouched)
                    rayDirection = objectToAttack.transform.position - transform.Find("RayStart").transform.position;
                Ray aimRay = new Ray(transform.Find("RayStart").transform.position, rayDirection);
                LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard")) & ~(1 << LayerMask.NameToLayer("SummonedArrow"));
                RaycastHit aimHit;
                if (Physics.Raycast(aimRay, out aimHit, Mathf.Infinity, layer))
                {
                    //If length between player and archer more than 8 archer will run
                    if ((objectToAttack.transform.position - transform.position).magnitude > 8)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                            selfAudio.loop = true;
                            selfAudio.Play();
                        }
                       animator.Play("Run");
                        animator.SetBool("IsRunning", true);
                       animator.SetBool("IsAttacking", false);
                        isAttacking = false;
                        if (arrow.enabled)
                        arrow.enabled = false;
                        if (currentHP > 0)
                            selfAgent.isStopped = false;
                        selfAgent.SetDestination(objectToAttack.transform.position);
                    }
                    //If length between player and archer less tthan 8 archer attack player
                    else if ((objectToAttack.transform.position - transform.position).magnitude <= 8 && aimHit.collider.transform.root.gameObject == objectToAttack)
                    {
                        if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                        {
                            selfAudio.clip = null;
                            selfAudio.loop = false;
                        }
                        animator.SetBool("IsRunning", false);
                        selfAgent.isStopped = true;
                        RotateTowards(objectToAttack.transform);
                        if(isArcher)
                            if (!arrow.enabled)
                            arrow.enabled = true;
                        seeEnemy = true;
                    }
                    if (aimHit.collider.transform.root.gameObject != objectToAttack)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                            selfAudio.loop = true;
                           selfAudio.Play();
                        }
                       animator.Play("Run");
                       animator.SetBool("IsRunning", true);
                       animator.SetBool("IsAttacking", false);
                        isAttacking = false;
                        if (currentHP > 0)
                            selfAgent.isStopped = false;
                        selfAgent.SetDestination(FindPosition());
                        if (arrow.enabled)
                        arrow.enabled = false;
                        seeEnemy = false;

                    }
                }
                //If object is killed go to start position and cancel alert
                if(objectToAttack!=null)
                if (objectToAttack.transform.root.tag == "Summoned")
                    if (enemySummonedAI.currentHP <= 0)
                    {
                            animator.SetBool("IsAttacking", false);
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                        StopCoroutine("CancelAlert");
                            animator.SetBool("IsDrawingArrow", false);
                        isAlerted = false;
                        isAttacking = false;
                      animator.SetBool("IsAttacking", false);
                     animator.Play("Run");
                       animator.SetBool("IsRunning", true);
                        if (currentHP > 0)
                            selfAgent.isStopped = false;
                       animator.SetBool("IsAttacking", false);
                        selfAgent.SetDestination(startPosition);
                    }
                if(objectToAttack!=null)
                if (objectToAttack.transform.root.tag == "Player")
                    if (enemyPlayerController.currentHealth <= 0)
                    {
                            animator.SetBool("IsAttacking", false);
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                        StopCoroutine("CancelAlert");
                        isAlerted = false;
                        isAttacking = false;
                      animator.SetBool("IsAttacking", false);
                            animator.SetBool("IsDrawingArrow", false);
                            animator.Play("Run");
                     animator.SetBool("IsRunning", true);
                        if (currentHP > 0)
                            selfAgent.isStopped = false;
                       animator.SetBool("IsAttacking", false);
                        selfAgent.SetDestination(startPosition);
                    }
                if (objectToAttack != null)
                    if (objectToAttack.transform.root.tag == "VillageGuard" || objectToAttack.transform.root.tag == "Bandit" || objectToAttack.transform.root.tag == "Undead" || objectToAttack.transform.root.tag == "Royalist" || objectToAttack.transform.root.tag == "Republican")
                    {
                        if (objectToAttack.GetComponentInParent<GuardAI>() != null)
                            if (enemyGuardAI.currentHP <= 0)
                            {
                                animator.SetBool("IsAttacking", false);
                                GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                StopCoroutine("CancelAlert");
                                isAlerted = false;
                                isAttacking = false;
                                animator.SetBool("IsAttacking", false);
                                animator.SetBool("IsDrawingArrow", false);
                                animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                animator.SetBool("IsAttacking", false);
                                selfAgent.SetDestination(startPosition);
                            }
                        if (objectToAttack.GetComponentInParent<SummonedAI>() != null)
                        {
                            if (enemySummonedAI.currentHP <= 0)
                            {
                                animator.SetBool("IsAttacking", false);
                                GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                StopCoroutine("CancelAlert");
                                isAlerted = false;
                                isAttacking = false;
                                animator.SetBool("IsAttacking", false);
                                animator.SetBool("IsDrawingArrow", false);
                                animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                animator.SetBool("IsAttacking", false);
                                selfAgent.SetDestination(startPosition);
                            }
                        }
                    }
                if (objectToAttack != null)
                    if (objectToAttack.transform.root.tag == "SimplePeople" || objectToAttack.transform.root.tag == "Civilian")
                        if (enemyCivilianAI.currentHP <= 0)
                            {
                                animator.SetBool("IsAttacking", false);
                                GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                StopCoroutine("CancelAlert");
                                isAlerted = false;
                                isAttacking = false;
                                animator.SetBool("IsAttacking", false);
                            animator.SetBool("IsDrawingArrow", false);
                            animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                animator.SetBool("IsAttacking", false);
                                selfAgent.SetDestination(startPosition);
                            }
                if (objectToAttack != null)
                    if (objectToAttack.tag == "Summoned")
                        if (enemySummonedAI.currentHP <= 0)
                        {
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                            StopCoroutine("CancelAlert");
                            objectToAttack = null;
                            isAlerted = false;
                            isAttacking = false;
                            animator.SetBool("IsAttacking", false);
                            animator.SetBool("IsDrawingArrow", false);
                            animator.Play("Run");
                            animator.SetBool("IsRunning", true);
                            if (currentHP > 0)
                                selfAgent.isStopped = false;
                            animator.SetBool("IsAttacking", false);
                            if (selfPatrol == true)
                            {
                                animator.SetBool("IsHugeAttacking", false);
                                animator.SetBool("IsAttacking", false);
                                animator.SetBool("IsBlocking", false);
                                selfPatrol.enabled = true;
                                selfPatrol.StartCoroutine("PatrolAI");
                                animator.SetBool("IsWalking", true);
                                animator.SetBool("IsDrawingArrow", false);
                                animator.SetBool("IsRunning", false);
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                selfAudio.loop = true;
                                selfAudio.Play();
                                animator.Play("Walk");
                                selfAgent.speed = gameManager.stableWalkingSpeed;
                            }
                            else
                                selfAgent.SetDestination(startPosition);
                        }
                if (objectToAttack != null)
                    if (objectToAttack.tag == "Civilian" || objectToAttack.tag == "SimplePeople" || objectToAttack.name == "Solovey")
                        if (enemyCivilianAI.currentHP <= 0)
                        {
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                            StopCoroutine("CancelAlert");
                            objectToAttack = null;
                            isAlerted = false;
                            isAttacking = false;
                            animator.SetBool("IsAttacking", false);
                            animator.SetBool("IsDrawingArrow", false);
                            animator.Play("Run");
                            animator.SetBool("IsRunning", true);
                            if (currentHP > 0)
                                selfAgent.isStopped = false;
                            animator.SetBool("IsAttacking", false);
                            selfAgent.SetDestination(startPosition);
                            GetComponent<Animator>().SetBool("IsAttacking", false);
                            GetComponent<Animator>().speed = 1.0f;
                            GetComponent<Animator>().SetBool("IsRunning", true);
                            GetComponent<Animator>().Play("Run");
                            if (selfPatrol == true)
                            {
                                animator.SetBool("IsHugeAttacking", false);
                                animator.SetBool("IsAttacking", false);
                                animator.SetBool("IsBlocking", false);
                                selfPatrol.enabled = true;
                                selfPatrol.StartCoroutine("PatrolAI");
                                animator.SetBool("IsWalking", true);
                                animator.SetBool("IsDrawingArrow", false);
                                animator.SetBool("IsRunning", false);
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                selfAudio.loop = true;
                                selfAudio.Play();
                                animator.Play("Walk");
                                selfAgent.speed = gameManager.stableWalkingSpeed;
                            }
                            else
                                selfAgent.SetDestination(startPosition);
                        }
                if (objectToAttack != null)
                    if (objectToAttack.tag == "Bandit" || objectToAttack.tag == "VillageGuard" || objectToAttack.tag == "Republican" || objectToAttack.tag == "Royalist" || objectToAttack.tag == "Undead")
                    {
                        if (enemyGuardAI != null)
                            if (enemyGuardAI.currentHP <= 0)
                            {
                                GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                StopCoroutine("CancelAlert");
                                objectToAttack = null;
                                isAlerted = false;
                                isAttacking = false;
                                animator.SetBool("IsAttacking", false);
                                animator.SetBool("IsDrawingArrow", false);
                                animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                                selfAgent.SetDestination(startPosition);
                                GetComponent<Animator>().SetBool("IsAttacking", false);
                                GetComponent<Animator>().speed = 1.0f;
                                GetComponent<Animator>().SetBool("IsRunning", true);
                                GetComponent<Animator>().Play("Run");
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                animator.SetBool("IsAttacking", false);
                                if (selfPatrol == true)
                                {
                                    animator.SetBool("IsHugeAttacking", false);
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsBlocking", false);
                                    selfPatrol.enabled = true;
                                    selfPatrol.StartCoroutine("PatrolAI");
                                    animator.SetBool("IsWalking", true);
                                    animator.SetBool("IsRunning", false);
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                    animator.Play("Walk");
                                    selfAgent.speed = gameManager.stableWalkingSpeed;
                                }
                                else
                                    selfAgent.SetDestination(startPosition);
                            }
                        if (objectToAttack != null)
                            if (enemySummonedAI != null)
                            {
                                if (enemySummonedAI.currentHP <= 0)
                                {
                                    GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                                    StopCoroutine("CancelAlert");
                                    objectToAttack = null;
                                    isAlerted = false;
                                    isAttacking = false;
                                    animator.SetBool("IsAttacking", false);
                                    animator.SetBool("IsDrawingArrow", false);
                                    selfAgent.SetDestination(startPosition);
                                    GetComponent<Animator>().SetBool("IsAttacking", false);
                                    GetComponent<Animator>().speed = 1.0f;
                                    GetComponent<Animator>().SetBool("IsRunning", true);
                                    GetComponent<Animator>().Play("Run");
                                    animator.Play("Run");
                                    animator.SetBool("IsRunning", true);
                                    if (currentHP > 0)
                                        selfAgent.isStopped = false;
                                    animator.SetBool("IsAttacking", false);
                                    if (selfPatrol == true)
                                    {
                                        animator.SetBool("IsHugeAttacking", false);
                                        animator.SetBool("IsAttacking", false);
                                        animator.SetBool("IsBlocking", false);
                                        selfPatrol.enabled = true;
                                        selfPatrol.StartCoroutine("PatrolAI");
                                        animator.SetBool("IsWalking", true);
                                        animator.SetBool("IsRunning", false);
                                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                        selfAudio.loop = true;
                                        selfAudio.Play();
                                        animator.Play("Walk");
                                        selfAgent.speed = gameManager.stableWalkingSpeed;
                                    }
                                    else
                                        selfAgent.SetDestination(startPosition);
                                }
                            }
                    }
                if (objectToAttack != null)
                    if (objectToAttack.tag == "Player")
                        if (enemyPlayerController.currentHealth <= 0)
                        {
                            GetComponentInChildren<FractionTrigger>().objectsInRadius.Remove(objectToAttack);
                            StopCoroutine("CancelAlert");
                            objectToAttack = null;
                            isAlerted = false;
                            isAttacking = false;
                            animator.SetBool("IsAttacking", false);
                            selfAgent.SetDestination(startPosition);
                            GetComponent<Animator>().SetBool("IsAttacking", false);
                            animator.SetBool("IsDrawingArrow", false);
                            GetComponent<Animator>().speed = 1.0f;
                            GetComponent<Animator>().SetBool("IsRunning", true);
                            GetComponent<Animator>().Play("Run");
                            animator.Play("Run");
                            animator.SetBool("IsRunning", true);
                            if (currentHP > 0)
                                selfAgent.isStopped = false;
                            animator.SetBool("IsAttacking", false);
                            if (selfPatrol == true)
                            {
                                animator.SetBool("IsHugeAttacking", false);
                                animator.SetBool("IsAttacking", false);
                                animator.SetBool("IsBlocking", false);
                                selfPatrol.enabled = true;
                                selfPatrol.StartCoroutine("PatrolAI");
                                animator.SetBool("IsWalking", true);
                                animator.SetBool("IsRunning", false);
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                                selfAudio.loop = true;
                                selfAudio.Play();
                                animator.Play("Walk");
                                selfAgent.speed = gameManager.stableWalkingSpeed;
                            }
                            else
                                    if (selfAgent.enabled)
                                selfAgent.SetDestination(startPosition);
                        }
            }
        }
    }
    private Vector3 FindPosition()
    {
        Ray objectForward = new Ray(objectToAttack.transform.position, objectToAttack.transform.forward);
        Ray objectLeft = new Ray(objectToAttack.transform.position, -objectToAttack.transform.right);
        Ray objectRight = new Ray(objectToAttack.transform.position, objectToAttack.transform.right);
        Ray objectBack = new Ray(objectToAttack.transform.position, -objectToAttack.transform.forward);
        if (!Physics.Raycast(objectLeft, 7f))
            return objectToAttack.transform.position - objectToAttack.transform.right * 7;
        if (!Physics.Raycast(objectRight, 7f))
            return objectToAttack.transform.position + objectToAttack.transform.right * 7;
        if (!Physics.Raycast(objectForward, 7f))
            return objectToAttack.transform.position + objectToAttack.transform.forward * 7;
        if (!Physics.Raycast(objectBack, 7f))
            return objectToAttack.transform.position - objectToAttack.transform.forward * 7;
        return objectToAttack.transform.position;
    }
    //Damage function for animation event (draw arrow readeble)
    public void BowSimpleDamage()
    {
        Vector3 spawnPosition = FindBow(gameObject).position;
        Vector3 velocity = transform.forward * 30;
        if (objectToAttack != null)
        {
            if (GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && objectToAttack == gameManager.player.gameObject)
                velocity = ((GameObject.Find("Player").transform.position - Vector3.up) - transform.position) * 30;
            if (objectToAttack!=gameManager.player.gameObject)
            {
                velocity = (objectToAttack.transform.position - transform.position) * 30;
                spawnPosition = transform.position + Vector3.up;
            }
        }
        //Spawn Arrow
        GameObject spawnArrow;
        spawnArrow = Instantiate(gameManager.arrow, spawnPosition, Quaternion.Euler(0.917f, -179, -177));
        IgnoreCollision(gameObject, spawnArrow);
        spawnArrow.GetComponent<Rigidbody>().velocity = velocity;
        spawnArrow.GetComponent<RectTransform>().rotation = Quaternion.LookRotation(spawnArrow.GetComponent<Rigidbody>().velocity);
        spawnArrow.GetComponent<Item>().item = gameManager.arrow;
        spawnArrow.GetComponent<Arrow>().arrowDamage = damage;
        spawnArrow.GetComponent<Arrow>().shooter = gameObject;
        spawnArrow.layer = 12;
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().archerySound;
        selfAudio.Play();
        FindArrowMeshRenderer(gameObject).enabled = true;
        gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
        isAttacking = false;
    }
    //Call near people
    public void CallNear()
    {
        //Call near for village guard
        if (tag == "VillageGuard")
        {
            for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10&& GameObject.FindObjectsOfType<GuardAI>()[b].tag=="VillageGuard")
                {
                    if (objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    }
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = this.objectToAttack;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                }
            }
            for (int b = 0; b < GameObject.FindObjectsOfType<CivilianAI>().Length; b++)
            {
                if ((GameObject.FindObjectsOfType<CivilianAI>()[b].transform.position - transform.position).magnitude <= 10)
                {
                    if (objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().detection = 100;
                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    }
                    GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().attacker = objectToAttack;
                    GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                    GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                    if (GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().plusToCount == false && objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                        GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().plusToCount = true;
                    }
                }
            }
        }
        //Call near for village guard
        if (tag == "Bandit")
        {
            for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Bandit")
                {
                    if (objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    }
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = this.objectToAttack;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                }
            }
        }
        if (tag == "Undead")
        {
            for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Undead")
                {
                    if (objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    }
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = this.objectToAttack;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                }
            }
        }
        if (tag == "Republican")
        {
            for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Republican")
                {
                    if (objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    }
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = this.objectToAttack;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                }
            }
        }
        if (tag == "Undead")
        {
            for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
            {
                int distance = 10;
                if (name == "UndeadMelee3 (3)" || name == "UndeadArcher (2)" || name == "UndeadMelee3 (2)")
                    distance = 2;
                if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= distance && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Undead")
                {
                    if (objectToAttack == gameManager.player.gameObject)
                    {
                        GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    }
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = this.objectToAttack;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                }
            }
        }
    }
    //Cancel chasing,minus 1 to player's combatEnemies and back to position if length between player and guard more than 10 after 30 seconds since guard startet attacking player.Else call coroutine again
    IEnumerator CancelAlert()
    {
        yield return new WaitForSeconds(30);
        bool isClear = true;
        if (tag == "VillageGuard" || tag == "Civilian" || tag == "SimplePeople")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "VillageGuard")
                    isClear = false;
            }
            for(int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "VillageGuard")
                    isClear = false;
            }
            if ((gameManager.player.gameObject.transform.position - transform.position).magnitude < 10&&GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer)
                isClear = false;
        }
        if (tag == "Republican")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Republican")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Republican")
                    isClear = false;
            }
            if ((gameManager.player.gameObject.transform.position - transform.position).magnitude < 10 && GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer)
                isClear = false;
        }
        if (tag == "Royalist")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Royalist")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Royalist")
                    isClear = false;
            }
            if ((gameManager.player.gameObject.transform.position - transform.position).magnitude < 10 && GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer)
                isClear = false;
        }
        if (tag == "Bandit")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Bandit")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Bandit")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<CivilianAI>()[i].tag != "Bandit")
                    isClear = false;
            }
            if ((gameManager.player.gameObject.transform.position - transform.position).magnitude < 10)
                isClear = false;
        }
        if (tag == "Undead")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Undead")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Undead")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<CivilianAI>()[i].tag != "Undead")
                    isClear = false;
            }
            if ((gameManager.player.gameObject.transform.position - transform.position).magnitude < 10)
                isClear = false;
        }
        if (isClear)
        {
            if(currentHP>0)
            if (selfPatrol==true)
            {
                    animator.SetBool("IsHugeAttacking", false);
                    animator.SetBool("IsAttacking", false);
                    animator.SetBool("IsBlocking", false);
                    selfPatrol.enabled = true;
                selfPatrol.StartCoroutine("PatrolAI");
                gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
                gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                selfAudio.loop = true;
                selfAudio.Play();
                gameObject.GetComponent<Animator>().Play("Walk");
                selfAgent.speed = gameManager.stableWalkingSpeed;
            }
            else
                selfAgent.SetDestination(startPosition);
            if (isMage)
                if (mageSummoned != null)
                    mageSummoned.GetComponent<SummonedAI>().currentHP = 0;
            detection = 0;
            if (GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies == 0)
               gameManager.player.isDetected = false;
            gameManager.SetValueForStealth();
            StopCoroutine("CancelAlert");
            if (plusToCount)
               gameManager.player.combatEnemies--;
            plusToCount = false;
            objectToAttack = null;
            isAlerted = false;
            startCancelCoroutine = false;
            if (!isArcher&&!isMage)
            {
                gameManager.player.GetComponent<Animator>().SetBool("IsAttacking", false);
                gameManager.player.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                gameManager.player.GetComponent<Animator>().SetBool("IsBlocking", false);
            }
            else
            {
                gameManager.player.GetComponent<Animator>().SetBool("IsDrawingArrow", false);
            }
        }
        else
        {
            StopCoroutine("CancelAlert");
            StartCoroutine("CancelAlert");
        }
    }
    public void ImmediatelyCancelAlert()
    {
            bool isClear = true;
            if (tag == "VillageGuard" || tag == "Civilian" || tag == "SimplePeople")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
                {
                    if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "VillageGuard")
                        isClear = false;
                }
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "VillageGuard")
                        isClear = false;
                }
                if ((gameManager.player.transform.position - transform.position).magnitude < 10 && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer)
                    isClear = false;
            }
        if (tag == "Royalist")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Royalist")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Royalist")
                    isClear = false;
            }
            if ((gameManager.player.transform.position - transform.position).magnitude < 10 && GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer)
                isClear = false;
        }
        if (tag == "Republican")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Republican")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Republican")
                    isClear = false;
            }
            if ((gameManager.player.transform.position - transform.position).magnitude < 10 && GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer)
                isClear = false;
        }
        if (tag == "Bandit")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
                {
                    if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Bandit")
                        isClear = false;
                }
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Bandit")
                        isClear = false;
                }
                for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
                {
                    if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<CivilianAI>()[i].tag != "Bandit")
                        isClear = false;
                }
                if ((gameManager.player.transform.position - transform.position).magnitude < 10)
                    isClear = false;
            }
        if (tag == "Undead")
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<SummonedAI>()[i].tag != "Undead")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "Undead")
                    isClear = false;
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
            {
                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<CivilianAI>()[i].tag != "Undead")
                    isClear = false;
            }
            if ((gameManager.player.transform.position - transform.position).magnitude < 10)
                isClear = false;
        }
        if (isClear)
            {
                if (currentHP > 0)
                    if (selfPatrol == true)
                    {
                        animator.SetBool("IsHugeAttacking", false);
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("IsBlocking", false);
                        selfPatrol.enabled = true;
                        selfPatrol.StartCoroutine("PatrolAI");
                        gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
                        gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                        selfAudio.loop = true;
                        selfAudio.Play();
                        gameObject.GetComponent<Animator>().Play("Walk");
                        selfAgent.speed = gameManager.stableWalkingSpeed;
                    }
                    else
                        selfAgent.SetDestination(startPosition);
                if (isMage)
                    if (mageSummoned != null)
                        mageSummoned.GetComponent<SummonedAI>().currentHP = 0;
                detection = 0;
                if (GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies == 0)
                    gameManager.player.isDetected = false;
                gameManager.SetValueForStealth();
                StopCoroutine("CancelAlert");
                if (plusToCount)
                    gameManager.player.combatEnemies--;
                plusToCount = false;
                objectToAttack = null;
                isAlerted = false;
                startCancelCoroutine = false;
                if (!isArcher && !isMage)
                {
                    gameManager.player.GetComponent<Animator>().SetBool("IsAttacking", false);
                    gameManager.player.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                    gameManager.player.GetComponent<Animator>().SetBool("IsBlocking", false);
                }
                else
                {
                    gameManager.player.GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                }
            }
    }
    //Choose spell for mage
    public void MageAttackControl()
    {
        if (isAlerted&&seeEnemy&&objectToAttack!=null)
        {
            if ((objectToAttack.transform.position-transform.position).magnitude<=8) { 
            Ray infoRay = new Ray(selfAgent.transform.position + selfAgent.transform.up * 0.5f, selfAgent.transform.forward * 10);
            RaycastHit infoHit;
                if (Physics.Raycast(infoRay, out infoHit))
                {
                    if (!isArcher && isAlerted && isMage)
                    {
                        if (mageSummoned == null)
                        {
                            animator.SetBool("IsAttacking", true);
                            animator.Play("Attack");
                            spellName = "Summoned";
                        }
                        else
                        {
                            int chooseSpell = Random.Range(0, 11);
                            if (chooseSpell >= 2 && chooseSpell <= 10)
                            {
                                animator.SetBool("IsAttacking", true);
                                animator.Play("Attack");
                                spellName = "Fireball";
                            }
                            if (chooseSpell < 2 && currentHP <= 50)
                            {
                                animator.SetBool("IsAttacking", true);
                                animator.Play("Attack");
                                spellName = "Recover";
                            }
                        }
                    }
                }
            }
        }
    }
    //Spawn spell (animation event)
    public void SpawnSpell()
    {
        if (isAlerted)
        {
            //AI summon script
            if (spellName == "Summoned")
            {
                NavMeshPath path = new NavMeshPath();
                float randomX = Random.Range((transform.position - transform.right * 3).x, (transform.position + transform.right * 3).x);
                float randomZ = Random.Range((transform.position - transform.forward * 3).z, (transform.position + transform.forward * 3).z);
                int randomNumber;
                randomNumber = Random.Range(0, 2);
                while (true)
                {
                    if (selfAgent.CalculatePath(new Vector3(randomX, transform.position.y, randomZ), path))
                    {
                        GameObject spawn = null;
                        if (randomNumber == 0)
                            spawn = Instantiate(gameManager.player.summonedMelee, new Vector3(randomX, transform.position.y, randomZ), gameManager.player.summonedMelee.transform.rotation);
                        if (randomNumber == 1)
                            spawn = Instantiate(gameManager.player.summonedArcher, new Vector3(randomX, transform.position.y, randomZ), gameManager.player.summonedArcher.transform.rotation);
                        mageSummoned = spawn;
                        spawn.tag = gameObject.tag;
                        spawn.layer = gameObject.layer;
                        spawn.GetComponent<SummonedAI>().isAlerted = true;
                        spawn.GetComponent<SummonedAI>().summoner = gameObject;
                        transform.Find("ExtraSoundsMage").GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().summonSound;
                        transform.Find("ExtraSoundsMage").GetComponent<AudioSource>().Play();
                        break;
                    }
                }
            }                               
            //AI  script
            if (spellName == "Fireball")
            {
                GameObject spellObject = Instantiate(gameManager.player.fireball, FindSpawnPositionForFireball(gameObject).transform.position, Quaternion.Euler(0, gameManager.player.fireball.transform.rotation.y, 0));
                spellObject.GetComponent<Fireball>().StartCoroutine("DestroyAfterTime");
                spellObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().fireballSound;
                spellObject.GetComponent<AudioSource>().Play();
                spellObject.layer = 10;
                spellObject.GetComponent<Fireball>().spellDamage = damage;
                spellObject.GetComponent<Fireball>().shooter = gameObject;
            }
            //AI recover script
            if (spellName == "Recover")
            {
                currentHP += 50;
                transform.Find("ExtraSoundsMage").GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().recoverSound;
                transform.Find("ExtraSoundsMage").GetComponent<AudioSource>().Play();
            }
            spellName = "";
        }
    }    
    //If guard is attacking objectToAttack and is near him rotate toward it
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360);
    }
    //Cancel guard's block after 2 seconds since guard started blocking
    private void CancelBlock()
    {
        gameObject.GetComponent<Animator>().SetBool("IsBlocking", false);
    }
    //Cancel animation for two-hand weapon enemy
    private IEnumerator TwoHandCancelBlock()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("IsBlocking", false);
        block = false;
    }
    //Ignore shooter for arrows
    private void IgnoreCollision(GameObject objectIgnore,GameObject bullet)
    {
        for (int i = 0; i < objectIgnore.transform.childCount; i++)
        {
            if (objectIgnore.transform.GetChild(i).gameObject.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(objectIgnore.transform.GetChild(i).gameObject.GetComponent<Collider>(), bullet.GetComponent<BoxCollider>(), true);
                Physics.IgnoreCollision(objectIgnore.transform.GetChild(i).gameObject.GetComponent<Collider>(), bullet.GetComponent<MeshCollider>(), true);
            }
            if (objectIgnore.transform.GetChild(i).transform.childCount > 0)
            IgnoreCollision(objectIgnore.transform.GetChild(i).gameObject,bullet);
        }
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),bullet.GetComponent<BoxCollider>());
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullet.GetComponent<MeshCollider>());
    }
    //Set isTrigger to true to colliders after death
    private void MakeCollidersTrigger(GameObject objectToMake)
    {
        for (int i = 0; i < objectToMake.transform.childCount; i++)
        {
            if (objectToMake.transform.GetChild(i).gameObject.GetComponent<Collider>() != null)
                objectToMake.transform.GetChild(i).gameObject.GetComponent<Collider>().isTrigger = true;
            if (objectToMake.transform.GetChild(i).transform.childCount > 0)
               MakeCollidersTrigger(objectToMake.transform.GetChild(i).gameObject);
        }
    }
    //Find spawn position for fireball
    private GameObject FindSpawnPositionForFireball(GameObject objectToSearch)
    {
        GameObject objectReturn = gameObject;
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            if (objectToSearch.transform.GetChild(i).gameObject.name == "SpawnFinger")
               return objectToSearch.transform.GetChild(i).gameObject;
            if (objectReturn.name == "SpawnFinger")
                return objectReturn;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
               objectReturn=FindSpawnPositionForFireball(objectToSearch.transform.GetChild(i).gameObject);
        }
        return objectReturn;
    }
    //Destroy dead guard after 10 minutes if player is farrer than 30 points
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(600);
        GetComponent<DeclineAnimationScript>().canDissapear = true;
    }
    //Find arrow MeshFilter
    private MeshRenderer FindArrowMeshRenderer(GameObject objectToSearch)
    {
        MeshRenderer rendererToReturn = null;
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            if (objectToSearch.transform.GetChild(i).gameObject.name == "Arrow")
                return objectToSearch.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            if (rendererToReturn != null)
                if (rendererToReturn.name == "Arrow")
                    return rendererToReturn;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                rendererToReturn = FindArrowMeshRenderer(objectToSearch.transform.GetChild(i).gameObject);
        }
        return rendererToReturn;
    }
    private MeshRenderer FindWeapon(GameObject objectToSearch)
    {
        MeshRenderer rendererToReturn = null;
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            if (objectToSearch.transform.GetChild(i).gameObject.name == "Weapon")
                return objectToSearch.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            if (rendererToReturn != null)
                if (rendererToReturn.name == "Weapon")
                    return rendererToReturn;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                rendererToReturn = FindWeapon(objectToSearch.transform.GetChild(i).gameObject);
        }
        return rendererToReturn;
    }
    private void ChangeLayer(GameObject objectToSearch)
    {
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            objectToSearch.transform.GetChild(i).gameObject.layer = 21;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                ChangeLayer(objectToSearch.transform.GetChild(i).gameObject);
        }
    }
    //Find bow in children
    private Transform FindBow(GameObject objectToSearch)
    {
        Transform bowToReturn = null;
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            if (objectToSearch.transform.GetChild(i).gameObject.name == "Weapon")
                return objectToSearch.transform.GetChild(i);
            if(bowToReturn!=null)
            if (bowToReturn.name == "Weapon")
                return bowToReturn;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                bowToReturn = FindBow(objectToSearch.transform.GetChild(i).gameObject);
        }
        return bowToReturn;
    }
    //Pause melee guard block to make block idle
    private void PauseBlock()
    {
        GetComponent<Animator>().speed = 0;
        block = true;
        StartCoroutine("TimeForBlock");
    }
    //Cancel block pause after 2 seconds
    private IEnumerator TimeForBlock()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Animator>().speed = 1;
        block = false;
    }
    private void LoadGuardData()
    {
        GuardData guardData = SaveLoad.globalGuardData;
        bool found = false;
            for (int i = 0; i < guardData.ID.Length; i++)
            {
                if (ID == guardData.ID[i])
                {
                found = true;
                    Vector3 vectorPos= new Vector3(guardData.position[i, 0], guardData.position[i, 1], guardData.position[i, 2]);
                startPosition = new Vector3(guardData.startPosition[i, 0], guardData.startPosition[i, 1], guardData.startPosition[i, 2]);
                startRotation = new Vector3(guardData.startRotation[i, 0], guardData.startRotation[i, 1], guardData.startRotation[i, 2]);
                loadRotation= new Vector3(guardData.rotation[i, 0], guardData.rotation[i, 1], guardData.rotation[i, 2]);
                plusToCount = guardData.plusToCount[i];
                isAlerted = guardData.isAlerted[i];
                    startCancelCoroutine = guardData.startCancelCoroutine[i];
                    detection = guardData.detection[i];
                loadPosition = vectorPos;
                if (guardData.objectToAttackID[i] != null)
                {
                    if (selfPatrol != null)
                    {
                        selfAgent.speed = 4;
                        selfPatrol.enabled = false;
                    }
                }
                    if (guardData.mageSummonedType[i] != null)
                {
                    GameObject spawn = null;
                    if (guardData.mageSummonedType[i] == "Melee")
                    {
                        spawn = Instantiate(gameManager.player.summonedMelee, new Vector3(guardData.mageSummonedPosition[i, 0], guardData.mageSummonedPosition[i, 1], guardData.mageSummonedPosition[i, 2]), gameManager.player.summonedMelee.transform.rotation);
                        mageSummoned = spawn;
                    }
                    if (guardData.mageSummonedType[i] == "Archer")
                    {
                        spawn = Instantiate(gameManager.player.summonedArcher, new Vector3(guardData.mageSummonedPosition[i, 0], guardData.mageSummonedPosition[i, 1], guardData.mageSummonedPosition[i, 2]), gameManager.player.summonedArcher.transform.rotation);
                        mageSummoned = spawn;
                    }
                    spawn.tag = gameObject.tag;
                    spawn.transform.eulerAngles = new Vector3(guardData.mageSummonedRotation[i, 0], guardData.mageSummonedRotation[i, 1], guardData.mageSummonedRotation[i, 2]);
                    spawn.layer = gameObject.layer;
                    spawn.GetComponent<SummonedAI>().isAlerted = true;
                    spawn.GetComponent<SummonedAI>().summoner = gameObject;
                    spawn.GetComponent<SummonedAI>().loadedHp = guardData.summonedCurrentHP[i];
                    spawn.GetComponent<SummonedAI>().plusToCount = guardData.plusToCount[i];
                }
                    if (selfPatrol != null)
                    {
                        selfPatrol.cameDestination = guardData.cameDestination[i];
                        selfPatrol.indexPoint = guardData.indexPoint[i];
                    }
                if (currentHP > 0)
                {
                    if (selfPatrol == null)
                        if ((new Vector2(transform.position.x, transform.position.z) - new Vector2(guardData.destination[i, 0], guardData.destination[i, 2])).magnitude > 0.2f)
                        {
                            selfAgent.enabled = true;
                            selfAgent.SetDestination(new Vector3(guardData.destination[i, 0], guardData.destination[i, 1], guardData.destination[i, 2]));
                            GetComponent<Animator>().SetBool("IsRunning", true);
                            GetComponent<Animator>().Play("Run");
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                            selfAudio.loop = true;
                            selfAudio.Play();
                        }
                }
                if (GetComponent<Animator>().GetBool("IsRunning"))
                    if (FindWeapon(gameObject)!=null)
                    FindWeapon(gameObject).enabled = true;
                }
            }
            if (!found)
                Destroy(gameObject);        
    }
    private void LoadObjectToAttack()
    {
        GuardData guardData = SaveLoad.globalGuardData;
        for (int i = 0; i < guardData.ID.Length; i++)
        {
            if (ID == guardData.ID[i])
            {
                for (int b = 0; b < GameObject.FindObjectsOfType<CivilianAI>().Length; b++)
                    if (guardData.objectToAttackID[i] == GameObject.FindObjectsOfType<CivilianAI>()[b].ID)
                        objectToAttack = GameObject.FindObjectsOfType<CivilianAI>()[b].gameObject;
                for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                    if (guardData.objectToAttackID[i] == GameObject.FindObjectsOfType<GuardAI>()[b].ID)
                        objectToAttack = GameObject.FindObjectsOfType<GuardAI>()[b].gameObject;
                for (int b = 0; b < GameObject.FindObjectsOfType<SummonedAI>().Length; b++)
                    if (guardData.objectToAttackID[i] == GameObject.FindObjectsOfType<SummonedAI>()[b].ID)
                        objectToAttack = GameObject.FindObjectsOfType<SummonedAI>()[b].gameObject;
                if (guardData.objectToAttackID[i] == "Player")
                    objectToAttack = GameObject.Find("Player");
                isAlerted = true;
                StopCoroutine("CancelAlert");
                StartCoroutine("CancelAlert");
            }
        }
    }
    private void LoadHP()
    {
        GuardData guardData = SaveLoad.globalGuardData;
        for (int i = 0; i < guardData.ID.Length; i++)
        {
            if (ID == guardData.ID[i])
            {
                currentHP = guardData.currentHP[i];
                Vector3 vectorPos = new Vector3(guardData.position[i, 0], guardData.position[i, 1], guardData.position[i, 2]);
                transform.position = vectorPos;
                transform.eulerAngles = new Vector3(guardData.rotation[i, 0], guardData.rotation[i, 1], guardData.rotation[i, 2]);
                if (currentHP <= 0)
                {
                    GetComponent<Animator>().SetBool("IsDead", true);
                    GetComponent<Animator>().PlayInFixedTime("Death", 0, 1);
                    enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponentInChildren<FractionTrigger>().enabled = false;
                    if (GetComponent<CitizenAIPatrol>() != null)
                    {
                        GetComponent<CitizenAIPatrol>().StopAllCoroutines();
                        GetComponent<CitizenAIPatrol>().enabled = false;
                    }
                }
            }
        }
    }
    private void LoadLoot()
    {
        GuardData guardData = SaveLoad.globalGuardData;
        for (int i = 0; i < guardData.ID.Length; i++)
        {
            if (ID == guardData.ID[i])
            {
                for(int b = 0; b < GetComponent<Loot>().loot.Length; b++)
                {
                    GetComponent<Loot>().loot[b] = null;
                    GetComponent<Loot>().amountOfItems[b] = 0;
                }
                for(int b = 0; b < GetComponent<Loot>().loot.Length; b++)
                {
                    GetComponent<Loot>().loot[b] = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(guardData.itemName[i,b]);
                    GetComponent<Loot>().amountOfItems[b] = guardData.amountOfItems[i,b];
                }
                GetComponent<Loot>().amountOfGold = guardData.amountOfGold[i];
            }
        }
    }
}
