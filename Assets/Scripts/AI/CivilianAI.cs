using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CivilianAI : MonoBehaviour
{
    public string ID;
    public int hpCivilian = 10;
    public GameObject attacker;
    public bool hasBeenAttacked = false;
    public bool guardFound = false;
    public Vector3 startPosition;
    public int currentHP;
    public GameManager gameManager;
    public int detection;
    public bool plusToCount = false;
    public GameObject guardToRun;
    public bool foundWay = false;
    private ConeOfView coneOfView;
    private NavMeshAgent navMeshAgent;
    private FractionTrigger fractionTrigger;
    public Vector3 startRotation;
    public bool ignoreEnemies = false;
    public int experience;
    private Animator animator;
    private AudioSource selfAudio;
    private CitizenAIPatrol selfPatrol;
    void Awake()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
        if (SaveLoad.isLoading)
        {
            LoadLoot();
            LoadHP();
        }
    }
    void Start()
    {
        selfPatrol = GetComponent<CitizenAIPatrol>();
        selfAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        fractionTrigger = GetComponentInChildren<FractionTrigger>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        coneOfView = GetComponent<ConeOfView>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(!SaveLoad.isLoading)
        currentHP = hpCivilian;
        if (selfPatrol != null)
            navMeshAgent.speed = gameManager.stableWalkingSpeed;
        else
            navMeshAgent.speed = 3;
        if (SaveLoad.isLoading)
            LoadCivilianData();
        navMeshAgent.enabled = true;
        if(SaveLoad.isLoading&&guardToRun!=null)
            navMeshAgent.SetDestination(guardToRun.transform.position);
        //Some  code for BOB 
        if (name == "Bob"&&SaveLoad.isLoading)
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest")!=null)
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 4 && navMeshAgent.destination.x != startPosition.x && (startPosition - transform.position).magnitude > 1 && !animator.GetBool("IsStunned") && currentHP > 0 && !hasBeenAttacked)
            {
                animator.SetBool("IsWalking", true);
                animator.Play("Walk");
                navMeshAgent.SetDestination(startPosition);
            }
        if (name == "Solovey" && SaveLoad.isLoading)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null) {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage >= 7 && navMeshAgent.destination.x != startPosition.x && (startPosition - transform.position).magnitude > 1 && !animator.GetBool("IsStunned") && currentHP > 0 && !hasBeenAttacked && GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                {
                    animator.SetBool("IsRunning", true);
                    animator.Play("Run");
                    navMeshAgent.SetDestination(startPosition);
                    navMeshAgent.speed = 4;
                }
            }
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested && currentHP > 0)
        {
            if (GetComponent<SummonedAI>() != null)
            {
                GetComponent<SummonedAI>().summoner = GameObject.Find("Player");
                GetComponent<SummonedAI>().enabled = true;
            }
            GetComponent<CivilianAI>().enabled = false;
        }
        if (currentHP > 0)
        {
            if (name == "VillageWorker1")
            {
                if (startPosition.x != 90)
                {
                    animator.SetBool("IsPickaxing", true);
                    animator.Play("Pickaxe");
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().workingSound;
                    selfAudio.loop = true;
                    selfAudio.Play();
                }
                if (startPosition.x == 63.35f)
                    StartCoroutine("PositionOneUse");
                if (startPosition == new Vector3(78, 21.5f, 44))
                    StartCoroutine("PositionTwoUse");
                if (startPosition == new Vector3(90, 21.7f, 54.5f))
                    StartCoroutine("PositionThreeUse");
                navMeshAgent.speed = 3.0f;
                if ((transform.position - startPosition).magnitude > 1)
                {
                    animator.SetBool("IsPickaxing", false);
                    animator.SetBool("IsRunning", true);
                    animator.Play("Run");
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                    selfAudio.loop = true;
                    selfAudio.Play();
                    navMeshAgent.SetDestination(startPosition);
                }
            }
            if (name == "VillageWorker2")
            {
                if (startPosition.x != 89.7f)
                {
                    animator.SetBool("IsHammering", true);
                    animator.Play("Hammer");
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().workingSound;
                    selfAudio.loop = true;
                    selfAudio.Play();
                }
                if (startPosition.x == 76.68f)
                    StartCoroutine("HammerPositionOne");
                if (startPosition == new Vector3(71.1f, 21.6f, 58))
                    StartCoroutine("HammerPositionTwo");
                if (startPosition == new Vector3(89.7f, 21.7f, 56))
                    StartCoroutine("HammerPositionThree");
                navMeshAgent.speed = 3.0f;
                if ((transform.position - startPosition).magnitude > 1)
                {
                    animator.SetBool("IsHammering", false);
                    animator.SetBool("IsRunning", true);
                    animator.Play("Run");
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                    selfAudio.loop = true;
                    selfAudio.Play();
                    navMeshAgent.SetDestination(startPosition);
                }
            }
        }
    }
    void Update()
    {
        if(attacker!=null)
        if (attacker.tag == tag)
            attacker = null;
        //If AI is killed dissable script and play death animation
        if (currentHP <= 0 && !animator.GetBool("IsDead"))
        {
            ChangeLayer(gameObject);
            gameObject.layer = 21;
            if (fractionTrigger != null)
                fractionTrigger.enabled = false;
            if (plusToCount)
                gameManager.player.combatEnemies--;
            currentHP = 0;
            if (name == "Solovey")
                GetComponent<SummonedAI>().currentHP = 0;
            animator.Play("Death");
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsStunned", false);
            animator.SetBool("IsDead", true);
            navMeshAgent.isStopped = true;
            enabled = false;
            if (selfPatrol != null)
            {
                this.selfPatrol.enabled = false;
                animator.SetBool("IsWalking", false);
                selfPatrol.StopCoroutine("PatrolAI");
            }
            this.selfAudio.clip = null;
            this.selfAudio.Play();
            StopAllCoroutines();
            navMeshAgent.enabled = false;
            if (name == "Solovey")
                GetComponent<SummonedAI>().StopAllCoroutines();
            StartCoroutine("DestroyDelay");
            enabled = false;
        }
        if (currentHP > 0)
        {
            if (guardToRun != null)
                if (guardToRun.GetComponent<GuardAI>().isAlerted || guardToRun.GetComponent<GuardAI>().currentHP <= 0)
                    guardToRun = null;
            if (detection < 0)
                detection = 0;
            if (detection > 100)
                detection = 100;
            if (detection == 100)
            {
                gameManager.player.GetComponent<PlayerController>().isDetected = true;
                gameManager.stealthBar.GetComponent<Slider>().value = 100;
            }

            //If AI returned to his position before running turn to standard rotation
            if (name != "VillageWorker1"&&name!= "VillageWorker2")
                if (navMeshAgent.destination.x == startPosition.x && (startPosition - transform.position).magnitude <= 1 && currentHP > 0 && !animator.GetBool("IsStunned"))
            {
                selfAudio.clip = null;
                selfAudio.loop = false;
                animator.Play("Idle");
                animator.SetBool("IsRunning", false);
                if (name == "Bob")
                    animator.SetBool("IsWalking", false);
                transform.eulerAngles = startRotation;
            }
            if(name=="VillageWorker1"||name=="VillageWorker2")
            if (navMeshAgent.destination.x == startPosition.x && (startPosition - transform.position).magnitude <= 1 && currentHP > 0 && !animator.GetBool("IsStunned")&&!animator.GetBool("IsPickaxing") && !animator.GetBool("IsHammering"))
            {
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().workingSound;
                selfAudio.loop = true;
                selfAudio.Play();
                animator.Play("Idle");
                if (name == "Bob")
                    animator.SetBool("IsWalking", false);
                transform.eulerAngles = startRotation;
                    if (name == "VillageWorker1")
                    {
                        if (startPosition == new Vector3(78, 21.5f, 44))
                        StartCoroutine("PositionTwoUse");
                    if (startPosition == new Vector3(90, 21.7f, 54.5f) && animator.GetBool("IsRunning"))
                        StartCoroutine("PositionThreeUse");
                    if(startPosition.x==63.35f)
                        StartCoroutine("PositionOneUse");
                        if (startPosition == new Vector3(78, 21.5f, 44))
                        {
                            animator.SetBool("IsPickaxing", true);
                            animator.Play("Pickaxe");
                        }
                        if (startPosition == new Vector3(63.35f, 21.69795f, 52.77f))
                        {
                            animator.SetBool("IsPickaxing", true);
                            animator.Play("Pickaxe");
                        }
                    }
                    if (name == "VillageWorker2")
                    {
                        if (startPosition.x == 71.1f)
                            StartCoroutine("HammerPositionTwo");
                        if (startPosition.z == 56&&animator.GetBool("IsRunning")) 
                        StartCoroutine("HammerPositionThree");
                        if (startPosition.z == 53.64f)
                            StartCoroutine("HammerPositionOne");
                        if (startPosition.z == 53.64f)
                        {
                            animator.SetBool("IsHammering", true);
                            animator.Play("Hammer");
                        }
                        if (startPosition.z == 58)
                        {
                            animator.SetBool("IsHammering", true);
                            animator.Play("Hammer");
                        }
                    }
                    animator.SetBool("IsRunning", false);
                }
            FindGuard();
            RunAwayFromPlayer();
            if (gameManager.villageAttackedByPlayer && gameManager.player.isCrouched)
                coneOfView.ConeOfViewMake();
        }
    }
    //When was attacked by player find guard
    public void FindGuard()
    {
        if (name == "Solovey")
            guardFound = true;
        if (currentHP <= 0)
            hasBeenAttacked = false;
        if (hasBeenAttacked == true && guardFound == false)
        {
            navMeshAgent.speed = gameManager.stableLightRunningSpeed;
            if (selfPatrol != null && !animator.GetBool("IsStunned"))
            {
                selfPatrol.StopCoroutine("PatrolAI");
                selfPatrol.enabled = false;
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", true);
                animator.Play("Run");
            }
            //Searching for guard that isn't alerted.If no more,start running
            float length = 10000;
            bool foundGuard = false;
            if (guardToRun == null)
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().isAlerted == false && GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0 && (GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < length && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                    {
                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0)
                        {
                            guardToRun = GameObject.FindObjectsOfType<GuardAI>()[i].gameObject;
                            length = (GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude;
                            navMeshAgent.SetDestination(guardToRun.transform.position);
                            foundGuard = true;
                        }
                    }
                    else if (i == GameObject.FindObjectsOfType<GuardAI>().Length - 1 && GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().isAlerted == true && guardToRun == null && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                    {
                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0)
                        {
                            foundGuard = true;
                            if (!animator.GetBool("IsStunned"))
                            {
                                animator.Play("Run");
                                animator.SetBool("IsRunning", true);
                            }
                            guardFound = true;
                            guardToRun = null;
                        }
                    }
                }
            if (!foundGuard && guardToRun == null)
                guardFound = true;
            length = 10000;
            if (!animator.GetBool("IsStunned"))
            {
                animator.Play("Run");
                animator.SetBool("IsRunning", true);
            }
            if (guardToRun != null)
                if ((guardToRun.transform.position - this.transform.position).magnitude <= 3)
                {
                    guardToRun.GetComponent<GuardAI>().isAlerted = true;
                    guardToRun.GetComponent<GuardAI>().detection = 100;
                    guardToRun.GetComponent<GuardAI>().objectToAttack = attacker;
                    guardFound = true;
                    guardToRun = null;
                    CallNear();
                }
        }
        if (hasBeenAttacked && !guardFound && selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
        {
            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
            selfAudio.loop = true;
            selfAudio.Play();
        }
    }
    //When found guard run away from player to random gameobject avoiding player and obstacles
    private void RunAwayFromPlayer()
    {
        if (hasBeenAttacked && guardFound)
        {
            navMeshAgent.speed = gameManager.stableLightRunningSpeed;
            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
            {
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                selfAudio.loop = true;
                selfAudio.Play();
            }
            if (!foundWay)
            {
                while (true)
                {
                    float randomX = 0;
                    float randomZ = 0;
                    if (name != "Solovey")
                    {
                       randomX = Random.Range(20, 101);
                       randomZ = Random.Range(0, 91);
                    }
                    if(name == "Solovey")
                    {
                        randomX = Random.Range(50, 100);
                        randomZ = Random.Range(-50, -100);
                    }
                    Vector3 destination = new Vector3(randomX, transform.position.y, randomZ);
                    NavMeshPath path = new NavMeshPath();
                    if (navMeshAgent.CalculatePath(destination, path))
                    {
                        bool hasEnemy = false;
                        foreach (GameObject person in fractionTrigger.objectsInRadius.ToArray())
                        {
                            if ((destination - person.transform.position).magnitude < (destination - transform.position).magnitude&&!ignoreEnemies)
                                hasEnemy = true;
                        }
                        if (!hasEnemy)
                        {
                            foundWay = true;
                            navMeshAgent.SetDestination(destination);
                            break;
                        }
                    }
                }
            }
            if (navMeshAgent.destination != null)
                foreach (GameObject person in fractionTrigger.objectsInRadius.ToArray())
                {
                    if ((navMeshAgent.destination - person.transform.position).magnitude < (navMeshAgent.destination - transform.position).magnitude&&!ignoreEnemies)
                        foundWay = false;
                }
            if (navMeshAgent.remainingDistance <= 1.5f)
                foundWay = false;
        }
    }
    //Call near people.Activate after some actions in another functions
    public void CallNear()
    {
        for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
        {
            if(name!="Solovey")
            if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "VillageGuard")
            {
                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = attacker;
                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
            }
            if(name=="Solovey")
                if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Bandit")
                {
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().detection = 100;
                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = attacker;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                    GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                }
        }
        for (int b = 0; b < GameObject.FindObjectsOfType<CivilianAI>().Length; b++)
        {
            if(name!="Solovey")
            if ((GameObject.FindObjectsOfType<CivilianAI>()[b].transform.position - transform.position).magnitude <= 10)
            {
                GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().detection = 100;
                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().attacker = this.attacker;
                GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                if (GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().plusToCount == false && attacker == gameManager.player.gameObject)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                    GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().plusToCount = true;
                }
            }
        }
    }
    //Cancel "alert" mode if player is farrer then 10 and 30 seconds have passed else start coroutine again
    IEnumerator RunRegimeCancel()
    {

        yield return new WaitForSeconds(30);
        bool isClear = true;
        for (int i = 0; i < GameObject.FindObjectsOfType<SummonedAI>().Length; i++)
        {
            if ((GameObject.FindObjectsOfType<SummonedAI>()[i].transform.position - transform.position).magnitude < 10)
            {
                isClear = false;
            }
        }
        for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
        {
            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude < 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag != "VillageGuard")
            {
                isClear = false;
            }
        }
        if ((gameManager.player.transform.position - transform.position).magnitude < 10)
            isClear = false;
        if (isClear)
        {
            if (selfPatrol != null)
            {
                selfPatrol.enabled = true;
                selfPatrol.StartCoroutine("PatrolAI");
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                selfAudio.loop = true;
                selfAudio.Play();
                animator.Play("Walk");
                navMeshAgent.speed = gameManager.stableWalkingSpeed;
            }
            if (plusToCount)
            {
                gameManager.player.combatEnemies--;
                plusToCount = false;
            }
            foundWay = false;
            guardToRun = null;
            detection = 0;
            gameManager.player.isDetected = false;
            gameManager.GetComponent<GameManager>().SetValueForStealth();
            attacker = null;
            hasBeenAttacked = false;
            guardFound = false;
            navMeshAgent.SetDestination(startPosition);
            StopCoroutine("RunRegimeCancel");
        }
        else
            StartCoroutine("RunRegimeCancel");
    }
    //Destroy gameObject after 10 minutes
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(600);
        GetComponent<DeclineAnimationScript>().canDissapear = true; ;
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
    private void LoadCivilianData()
    {
        CivilianData civilianData = SaveLoad.globalCivilianData;
        bool found = false;
        for (int i = 0; i < civilianData.ID.Length; i++)
        {
                if (ID == civilianData.ID[i])
                {                   
                    found = true;
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                        if (GameObject.FindObjectsOfType<GuardAI>()[b].ID == civilianData.attackerID[i])
                            attacker = GameObject.FindObjectsOfType<GuardAI>()[b].gameObject;
                    for (int b = 0; b < GameObject.FindObjectsOfType<SummonedAI>().Length; b++)
                        if (GameObject.FindObjectsOfType<SummonedAI>()[b].ID == civilianData.attackerID[i])
                            attacker = GameObject.FindObjectsOfType<SummonedAI>()[b].gameObject;
                    if (civilianData.attackerID[i] == "Player")
                        attacker = GameObject.Find("Player");
                    hasBeenAttacked = civilianData.hasBeenAttacked[i];
                    guardFound = civilianData.guardFound[i];
                    startPosition = new Vector3(civilianData.startPosition[i, 0], civilianData.startPosition[i, 1], civilianData.startPosition[i, 2]);
                    startRotation = new Vector3(civilianData.startRotation[i, 0], civilianData.startRotation[i, 1], civilianData.startRotation[i, 2]);
                    detection = civilianData.detection[i];
                    plusToCount = civilianData.plusToCount[i];
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                        if (GameObject.FindObjectsOfType<GuardAI>()[b].ID == civilianData.guardToRunID[i])
                            guardToRun = GameObject.FindObjectsOfType<GuardAI>()[b].gameObject;
                    foundWay = civilianData.foundWay[i];
                    transform.position = new Vector3(civilianData.position[i, 0], civilianData.position[i, 1], civilianData.position[i, 2]);
                    transform.eulerAngles = new Vector3(civilianData.rotation[i, 0], civilianData.rotation[i, 1], civilianData.rotation[i, 2]);
                    if (selfPatrol != null)
                    {
                        selfPatrol.indexPoint = civilianData.indexPoint[i];
                        selfPatrol.cameDestination = civilianData.cameDestination[i];
                    }
                if (currentHP > 0)
                {
                    if(selfPatrol==null)
                    if ((new Vector2(startPosition.x, startPosition.z) - new Vector2(civilianData.destination[i, 0], civilianData.destination[i, 2])).magnitude > 0.2f)
                    {
                        navMeshAgent.enabled = true;
                        navMeshAgent.SetDestination(new Vector3(civilianData.destination[i, 0], civilianData.destination[i, 1], civilianData.destination[i, 2]));
                            animator.SetBool("IsRunning", true);
                            animator.Play("Run");
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                        selfAudio.loop = true;
                        selfAudio.Play();
                    }
                }
                }
        }
        if (!found)
            Destroy(gameObject);
    }
    private void LoadHP()
    {
        CivilianData civilianData = SaveLoad.globalCivilianData;
        for (int i = 0; i < civilianData.ID.Length; i++)
        {
            if (ID == civilianData.ID[i])
            {
                transform.position = new Vector3(civilianData.position[i, 0], civilianData.position[i, 1], civilianData.position[i, 2]);
                transform.eulerAngles = new Vector3(civilianData.rotation[i, 0], civilianData.rotation[i, 1], civilianData.rotation[i, 2]);
                currentHP = civilianData.currentHP[i];
                if (currentHP <= 0)
                {
                    GetComponent<Animator>().SetBool("IsDead", true);
                    GetComponent<Animator>().PlayInFixedTime("Death", 0, 1);
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
        CivilianData civilianData = SaveLoad.globalCivilianData;
        for (int i = 0; i < civilianData.ID.Length; i++)
        {
            if (ID == civilianData.ID[i])
            {
                    for (int b = 0; b < GetComponent<Loot>().loot.Length; b++)
                    {
                        GetComponent<Loot>().loot[b] = null;
                        GetComponent<Loot>().amountOfItems[b] = 0;
                    }
                    for (int b = 0; b < GetComponent<Loot>().loot.Length; b++)
                    {
                        GetComponent<Loot>().loot[b] = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(civilianData.itemName[i, b]);
                        GetComponent<Loot>().amountOfItems[b] = civilianData.amountOfItems[i, b];
                    }
                    GetComponent<Loot>().amountOfGold = civilianData.amountOfGold[i];
            }
        }
            }
    //Coroutines for village workers
    private IEnumerator PositionOneUse()
    {
        yield return new WaitForSeconds(30);
        startPosition = new Vector3(78, 21.5f, 44);
        startRotation = new Vector3(0, -288, 0);
        navMeshAgent.SetDestination(startPosition);
        animator.SetBool("IsPickaxing", false);
        animator.SetBool("IsRunning", true);
        animator.Play("Run");
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
        selfAudio.loop = true;
        selfAudio.Play();
        navMeshAgent.speed = 3.0f;
    }
    private IEnumerator PositionTwoUse()
    {
        yield return new WaitForSeconds(30);
        startPosition = new Vector3(90,21.7f,54.5f);
        startRotation = new Vector3(0, -363, 0);
        navMeshAgent.SetDestination(startPosition);
        animator.SetBool("IsPickaxing", false);
        animator.SetBool("IsRunning", true);
        animator.Play("Run");
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
        selfAudio.loop = true;
        selfAudio.Play();
        navMeshAgent.speed = 3.0f;
    }
    private IEnumerator PositionThreeUse()
    {
        yield return new WaitForSeconds(30);
        startPosition = new Vector3(63.35f, 21.69795f, 52.77f);
        startRotation = new Vector3(0, -363.735f, 0);
        navMeshAgent.SetDestination(startPosition);
        animator.SetBool("IsPickaxing", false);
        animator.SetBool("IsRunning", true);
        animator.Play("Run");
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
        selfAudio.loop = true;
        selfAudio.Play();
        navMeshAgent.speed = 3.0f;
    }
    //Village worker 2 coroutines
    private IEnumerator HammerPositionOne()
    {
        yield return new WaitForSeconds(30);
        startPosition = new Vector3(71.1f,21.6f,58);
        startRotation = new Vector3(0, -363, 0);
        navMeshAgent.SetDestination(startPosition);
        animator.SetBool("IsHammering", false);
        animator.SetBool("IsRunning", true);
        animator.Play("Run");
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
        selfAudio.loop = true;
        selfAudio.Play();
        navMeshAgent.speed = 3.0f;
    }
    private IEnumerator HammerPositionTwo()
    {
        yield return new WaitForSeconds(30);
        startPosition = new Vector3(89.7f,21.7f,56);
        startRotation = new Vector3(0, -187, 0);
        navMeshAgent.SetDestination(startPosition);
        animator.SetBool("IsHammering", false);
        animator.SetBool("IsRunning", true);
        animator.Play("Run");
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
        selfAudio.loop = true;
        selfAudio.Play();
        navMeshAgent.speed = 3.0f;
    }
    private IEnumerator HammerPositionThree()
    {
        yield return new WaitForSeconds(30);
        startPosition = new Vector3(76.6f,21.7f,53.64f);
        startRotation = new Vector3(0,-187,0);
        navMeshAgent.SetDestination(startPosition);
        animator.SetBool("IsHammering", false);
        animator.SetBool("IsRunning", true);
        animator.Play("Run");
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
        selfAudio.loop = true;
        selfAudio.Play();
        navMeshAgent.speed = 3.0f;
    }
}
