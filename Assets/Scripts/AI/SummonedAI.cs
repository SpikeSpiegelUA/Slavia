using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SummonedAI : MonoBehaviour
{
    public string ID;
    private bool isAttacking = false;
    public GameObject objectToAttack;
    public bool isAlerted;
    public int damage = 10;
    public int guardHP;
    public int currentHP;
    public bool isAlive = true;
    public bool block;
    public bool isArcher;
    private bool seeEnemy;
    public GameObject summoner;
    GameManager gameManager;
    public bool plusToCount = false;
    public int loadedHp;
    public bool loaded;
    public int soloveyHP;
    private AudioSource selfAudio;
    private Animator selfAnimator;
    private NavMeshAgent selfAgent;
    void Awake()
    {
        if (SaveLoad.isLoading)
        {
            LoadSpecialData();
            loaded = true;
        }
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }
    // Start is called before the first frame update
    void Start()
    {
        selfAgent = GetComponent<NavMeshAgent>();
        selfAnimator = GetComponent<Animator>();
        selfAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(!isArcher)
        selfAnimator.SetBool("IsSummoned", true);
        if(!SaveLoad.isLoading)
        currentHP = guardHP;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(name!="Solovey")
        StartCoroutine("KillAfterTime");
        if (SaveLoad.isLoading)
        {
            if (summoner.GetComponent<GuardAI>() != null)
                summoner.GetComponent<GuardAI>().mageSummoned = gameObject;
            if (name != "Solovey")
            {
                if (summoner.GetComponent<PlayerController>() != null && isArcher)
                    summoner.GetComponent<PlayerController>().currentSummonedArcher = gameObject;
                if (summoner.GetComponent<PlayerController>() != null && !isArcher)
                    summoner.GetComponent<PlayerController>().currentSummonedMelee = gameObject;
            }
        }
        if (loaded&&name!="Solovey")
        {
            loaded = false;
            currentHP = loadedHp;
            if (currentHP <= 0)
            {
                selfAnimator.SetBool("IsDead", true);
                selfAnimator.PlayInFixedTime("Death", 0, 1);
            }
        }
        if (name == "Solovey"&&SaveLoad.isLoading)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                tag = "Civilian";
            selfAgent.speed = 4;
            currentHP = soloveyHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            selfAudio.clip = null;
            selfAudio.Play();
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested && currentHP > 0)
        {
            if (name == "Solovey")
            {
                GetComponent<CivilianAI>().enabled = false;
                GetComponent<SummonedAI>().enabled = true;
                GetComponent<SummonedAI>().summoner = GameObject.Find("Player");
            }
        }
        if (objectToAttack != null)
            if (objectToAttack.tag == tag)
                objectToAttack = null;
        if (summoner != gameManager.player.gameObject)
            if (summoner.GetComponent<GuardAI>().currentHP <= 0)
                currentHP = 0;
        if(summoner!=gameManager.player.gameObject)
        if (objectToAttack == gameManager.player.gameObject && !plusToCount)
        {
            plusToCount = true;
            gameManager.player.combatEnemies++;
        }
        //Disable scripts and play death animation if hp<0
        if (currentHP <= 0&&!selfAnimator.GetBool("IsDead"))
        {
            ChangeLayer(gameObject);
            gameObject.layer = 21;
            if (name == "Solovey")
                GetComponent<CivilianAI>().StopAllCoroutines();
            selfAudio.clip = null;
            selfAudio.Play();
            if (plusToCount)
                gameManager.player.combatEnemies--;
                GetComponent<BoxCollider>().size = new Vector3(1.88f, 0.6f, 2.11f);
                GetComponent<BoxCollider>().center = new Vector3(-0.22f, 0.33f, -1.33f);
            selfAgent.isStopped = true;
            isAlive = false;
            if (name == "Solovey")
                GetComponent<CivilianAI>().currentHP = 0;
            currentHP = 0;
            selfAnimator.Play("Death");
            selfAnimator.SetBool("IsDead", true);
            selfAnimator.SetBool("IsRunning", false);
            this.gameObject.GetComponent<SummonedAI>().enabled = false;
            StopAllCoroutines();
            StartCoroutine("DestroyDelay");
        }
        if (currentHP > 0)
        {
            if (summoner != gameManager.player.gameObject)
                if (objectToAttack == gameManager.player.gameObject && !plusToCount)
                {
                    plusToCount = true;
                    gameManager.player.combatEnemies++;
                }
            movementControl();
            Movement();
            DamageControl();
            if (objectToAttack != null && isArcher)
                MovementForArcher();
            if (objectToAttack != summoner)
            {
                //Attack if length to objectToAttack less than 3 for guard
                if (objectToAttack != null&&objectToAttack.gameObject!=summoner.gameObject)
                    if ((objectToAttack.transform.position - transform.position).magnitude <= 2.2f && !isArcher)
                       selfAnimator.SetBool("IsAttacking", true);
                //Attack if length to objectToAttack less than 8 for archer
                if (objectToAttack != null&&objectToAttack!=summoner)
                    if ((objectToAttack.transform.position - transform.position).magnitude <= 8 && isArcher && seeEnemy)
                    {
                        if (!isAttacking)
                            isAttacking = true;
                        if(objectToAttack.gameObject != summoner.gameObject)
                        selfAnimator.SetBool("IsAttacking", true);
                        if (FindArrowMeshRenderer(gameObject) != null)
                            if (!FindArrowMeshRenderer(gameObject).enabled)
                        FindArrowMeshRenderer(gameObject).enabled = true;
                    }
            }
            if (objectToAttack != null)
            {
                if (objectToAttack.GetComponent<GuardAI>() != null)
                    if (objectToAttack.GetComponent<GuardAI>().currentHP <= 0)
                    {
                        objectToAttack = summoner;
                        isAlerted = false;
                        isAttacking = false;
                        if (FindArrowMeshRenderer(gameObject) != null)
                            if (FindArrowMeshRenderer(gameObject).enabled)
                            FindArrowMeshRenderer(gameObject).enabled = false;
                    }
                if (objectToAttack.GetComponent<AnimalAI>() != null)
                    if (objectToAttack.GetComponent<AnimalAI>().currentHP <= 0)
                    {
                        objectToAttack = summoner;
                        isAlerted = false;
                        isAttacking = false;
                        if (FindArrowMeshRenderer(gameObject) != null)
                            if (FindArrowMeshRenderer(gameObject).enabled)
                                FindArrowMeshRenderer(gameObject).enabled = false;
                    }
                if (objectToAttack.GetComponent<PlayerController>() != null)
                    if (objectToAttack.GetComponent<PlayerController>().currentHealth <= 0)
                    {
                        objectToAttack = summoner;
                        isAlerted = false;
                        isAttacking = false;
                        if(FindArrowMeshRenderer(gameObject)!=null)
                        if (FindArrowMeshRenderer(gameObject).enabled)
                            FindArrowMeshRenderer(gameObject).enabled = false;
                    }
                if (objectToAttack.GetComponent<CivilianAI>() != null)
                    if (objectToAttack.GetComponent<CivilianAI>().currentHP <= 0)
                    {
                        objectToAttack = summoner;
                        isAlerted = false;
                        isAttacking = false;
                        if (FindArrowMeshRenderer(gameObject) != null)
                            if (FindArrowMeshRenderer(gameObject).enabled)
                            FindArrowMeshRenderer(gameObject).enabled = false;
                    }
                if (objectToAttack.GetComponent<SummonedAI>() != null)
                    if (objectToAttack.GetComponent<SummonedAI>().currentHP <= 0)
                    {
                        objectToAttack = summoner;
                        isAlerted = false;
                        isAttacking = false;
                        if (FindArrowMeshRenderer(gameObject) != null)
                            if (FindArrowMeshRenderer(gameObject).enabled)
                            FindArrowMeshRenderer(gameObject).enabled = false;
                    }
            }
            if(summoner.GetComponent<GuardAI>()!=null)
            summoner.GetComponent<GuardAI>().mageSummoned = gameObject;
            if (name != "Solovey")
            {
                if (summoner.GetComponent<PlayerController>() != null && isArcher)
                    summoner.GetComponent<PlayerController>().currentSummonedArcher = gameObject;
                if (summoner.GetComponent<PlayerController>() != null && !isArcher)
                    summoner.GetComponent<PlayerController>().currentSummonedMelee = gameObject;
            }
        }
    }
    //Choose what type of action in fight will AI do
    public void DamageControl()
    {
        if (isAlerted&&objectToAttack!=summoner&&objectToAttack!=null)
        {
            int attackChoose = -1;
            if (!block)
                attackChoose = Random.Range(0, 11);
            Ray infoRay = new Ray(selfAgent.transform.position + selfAgent.transform.up * 0.5f, selfAgent.transform.forward * 10);
            RaycastHit infoHit;
            if (Physics.Raycast(infoRay, out infoHit))
            {
                if (!isArcher && isAlerted)
                {
                    if (infoHit.transform.root.gameObject.CompareTag("VillageGuard")|| infoHit.transform.root.gameObject.CompareTag("Bandit") || infoHit.transform.root.gameObject.CompareTag("Republican") || infoHit.transform.root.gameObject.CompareTag("Royalist") || infoHit.transform.root.gameObject.CompareTag("Undead"))
                        if (objectToAttack.CompareTag("VillageGuard")|| objectToAttack.CompareTag("Bandit") || objectToAttack.CompareTag("Royalist") || objectToAttack.CompareTag("Republican") || objectToAttack.CompareTag("Undead"))
                        {
                            if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<GuardAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsAttacking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("Attack");
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<GuardAI>().currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsHugeAttacking", true);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("HugeAttack");
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<GuardAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        block = true;
                                        selfAnimator.SetBool("IsBlocking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.Play("SummonedBlock");
                                        StartCoroutine("CancelBlock");
                                    }
                                }
                            }
                            else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                            {
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsAttacking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("Attack");
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsHugeAttacking", true);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("HugeAttack");
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        block = true;
                                        selfAnimator.SetBool("IsBlocking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.Play("SummonedBlock");
                                        StartCoroutine("CancelBlock");
                                    }
                                }
                            }
                        }
                    if (infoHit.transform.root.gameObject.CompareTag("Player")|| infoHit.transform.root.gameObject.CompareTag("Summoned"))
                        if (objectToAttack.CompareTag("Player")|| infoHit.transform.root.gameObject.CompareTag("Summoned"))
                        {
                            if (infoHit.collider.GetComponentInParent<PlayerController>() != null)
                            {
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2f && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsAttacking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("Attack");
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsHugeAttacking", true);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("HugeAttack");
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        block = true;
                                        selfAnimator.SetBool("IsBlocking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.Play("SummonedBlock");
                                        StartCoroutine("CancelBlock");
                                    }
                                }
                            }
                            else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                            {
                                //If attackChoose >3 and <11 play simple attack
                                if (attackChoose > 3 && attackChoose < 11)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsAttacking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("Attack");
                                    }
                                }
                                //if attackChoose 1 or 0 play huge attack
                                else if (attackChoose <= 1 && attackChoose >= 0)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        isAttacking = true;
                                        selfAnimator.SetBool("IsHugeAttacking", true);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.SetBool("IsBlocking", false);
                                        selfAnimator.Play("HugeAttack");
                                    }
                                }
                                //If attackChoose 3 pr 2 play block
                                else if (attackChoose <= 3 && attackChoose >= 2)
                                {
                                    if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                    {
                                        block = true;
                                        selfAnimator.SetBool("IsBlocking", true);
                                        selfAnimator.SetBool("IsHugeAttacking", false);
                                        selfAnimator.SetBool("IsAttacking", false);
                                        selfAnimator.Play("SummonedBlock");
                                        StartCoroutine("CancelBlock");
                                    }
                                }
                            }
                        }
                    if (infoHit.transform.root.gameObject.CompareTag("Civilian")|| infoHit.transform.root.gameObject.CompareTag("SimplePeople")||infoHit.transform.root.name=="Solovey")
                        if (objectToAttack.CompareTag("Civilian")|| objectToAttack.CompareTag("SimplePeople")||objectToAttack.name=="Solovey")
                        {
                            //If attackChoose >3 and <11 play simple attack
                            if (attackChoose > 3 && attackChoose < 11)
                            {
                                if (infoHit.transform.root.gameObject.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    isAttacking = true;
                                    selfAnimator.SetBool("IsAttacking", true);
                                    selfAnimator.SetBool("IsHugeAttacking", false);
                                    selfAnimator.SetBool("IsBlocking", false);
                                    selfAnimator.Play("Attack");
                                }
                            }
                            //if attackChoose 1 or 0 play huge attack
                            else if (attackChoose <= 1 && attackChoose >= 0)
                            {
                                if (infoHit.transform.root.gameObject.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    isAttacking = true;
                                    selfAnimator.SetBool("IsHugeAttacking", true);
                                    selfAnimator.SetBool("IsAttacking", false);
                                    selfAnimator.SetBool("IsBlocking", false);
                                    selfAnimator.Play("HugeAttack");
                                }
                            }
                            //If attackChoose 3 pr 2 play block
                            else if (attackChoose <= 3 && attackChoose >= 2)
                            {
                                if (infoHit.transform.root.gameObject.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    block = true;
                                    selfAnimator.SetBool("IsBlocking", true);
                                    selfAnimator.SetBool("IsHugeAttacking", false);
                                    selfAnimator.SetBool("IsAttacking", false);
                                    selfAnimator.Play("SummonedBlock");
                                    StartCoroutine("CancelBlock");
                                }
                            }
                        }
                    if (infoHit.collider.GetComponentInParent<AnimalAI>()!=null)
                        if (objectToAttack.GetComponentInParent<AnimalAI>()!=null)
                        {
                            //If attackChoose >3 and <11 play simple attack
                            if (attackChoose > 3 && attackChoose < 11)
                            {
                                if (infoHit.transform.root.gameObject.GetComponent<AnimalAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    isAttacking = true;
                                    selfAnimator.SetBool("IsAttacking", true);
                                    selfAnimator.SetBool("IsHugeAttacking", false);
                                    selfAnimator.SetBool("IsBlocking", false);
                                    selfAnimator.Play("Attack");
                                }
                            }
                            //if attackChoose 1 or 0 play huge attack
                            else if (attackChoose <= 1 && attackChoose >= 0)
                            {
                                if (infoHit.transform.root.gameObject.GetComponent<AnimalAI>().currentHP > 0 && (infoHit.transform.root.gameObject.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    isAttacking = true;
                                    selfAnimator.SetBool("IsHugeAttacking", true);
                                    selfAnimator.SetBool("IsAttacking", false);
                                    selfAnimator.SetBool("IsBlocking", false);
                                    selfAnimator.Play("HugeAttack");
                                }
                            }
                            //If attackChoose 3 pr 2 play block
                            else if (attackChoose <= 3 && attackChoose >= 2)
                            {
                                if (infoHit.transform.root.gameObject.GetComponent<AnimalAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2 && isAttacking == false)
                                {
                                    block = true;
                                    selfAnimator.SetBool("IsBlocking", true);
                                    selfAnimator.SetBool("IsHugeAttacking", false);
                                    selfAnimator.SetBool("IsAttacking", false);
                                    selfAnimator.Play("SummonedBlock");
                                    StartCoroutine("CancelBlock");
                                }
                            }
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
            if (infoHit.transform.root.gameObject.CompareTag("VillageGuard")|| infoHit.transform.root.gameObject.CompareTag("Bandit") || infoHit.transform.root.gameObject.CompareTag("Republican") || infoHit.transform.root.gameObject.CompareTag("Royalist") || infoHit.transform.root.gameObject.CompareTag("Undead"))
                if (objectToAttack.CompareTag("VillageGuard")|| objectToAttack.CompareTag("Bandit") || objectToAttack.CompareTag("Royalist") || objectToAttack.CompareTag("Undead") || objectToAttack.CompareTag("Republican"))
                {
                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.collider.GetComponentInParent<GuardAI>()!= null) 
                    {
                        if (infoHit.transform.root.GetComponent<GuardAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound)
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                                selfAudio.Play();
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsAttacking", false);
                            if (infoHit.collider.GetComponentInParent<GuardAI>().isArcher == false&& !infoHit.collider.GetComponentInParent<GuardAI>().isMage)
                            {
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsBlocking", false);
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsRunning", false);
                            infoHit.transform.root.GetComponent<AudioSource>().clip = null;
                            infoHit.transform.root.GetComponent<AudioSource>().Play();
                            if (damage - objectToAttack.GetComponent<GuardAI>().armor / 100f * damage > 0)
                                objectToAttack.GetComponent<GuardAI>().currentHP -= (int)(damage - objectToAttack.GetComponent<GuardAI>().armor / 100f * damage) * 2;
                            objectToAttack.GetComponent<GuardAI>().block = false;
                            if (infoHit.transform.root.gameObject.GetComponent<GuardAI>().currentHP > 0)
                                if (infoHit.transform.root.GetComponent<GuardAI>().currentHP > 0)
                                    infoHit.transform.root.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                            if (infoHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0 && summoner.GetComponent<PlayerController>() != null)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<GuardAI>().experience;
                                StartCoroutine("KillExperienceShow");
                                gameManager.player.experience += infoHit.collider.GetComponentInParent<GuardAI>().experience;
                            }
                            infoHit.collider.GetComponentInParent<Animator>().speed = 1.0f;
                        }
                    }
                    else if(infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                    {
                        if (infoHit.transform.root.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound)
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                                selfAudio.Play();
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsAttacking", false);
                            if (infoHit.collider.GetComponentInParent<SummonedAI>().isArcher == false)
                            {
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsBlocking", false);
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsRunning", false);
                            infoHit.transform.root.GetComponent<AudioSource>().clip = null;
                            infoHit.transform.root.GetComponent<AudioSource>().Play();
                            objectToAttack.GetComponent<SummonedAI>().currentHP -= damage;
                            objectToAttack.GetComponent<SummonedAI>().block = false;
                            if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0)
                                    infoHit.transform.root.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                        }
                    }
                }
            if (infoHit.transform.root.gameObject.CompareTag("Player")|| objectToAttack.CompareTag("Summoned"))
                if (objectToAttack.CompareTag("Player")|| objectToAttack.CompareTag("Summoned"))
                {
                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.collider.GetComponentInParent<PlayerController>() != null)
                    {
                        if (infoHit.transform.root.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound)
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                                selfAudio.Play();
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsAttacking", false);
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsBlocking", false);
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsRunning", false);
                            infoHit.transform.root.GetComponent<AudioSource>().clip = null;
                            infoHit.transform.root.GetComponent<AudioSource>().Play();
                            if (damage - objectToAttack.GetComponent<PlayerController>().armor / 100f * damage > 0)
                                objectToAttack.GetComponent<PlayerController>().currentHealth -= (int)(damage - objectToAttack.GetComponent<PlayerController>().armor / 100f * damage) * 2;
                            objectToAttack.GetComponent<PlayerController>().block = false;
                            if (infoHit.transform.root.gameObject.GetComponent<PlayerController>().currentHealth > 0)
                                    infoHit.transform.root.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                            objectToAttack.GetComponent<PlayerController>().StartCoroutine("DamageScreenAppear");
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
                    else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                    {
                        if (infoHit.transform.root.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound)
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                                selfAudio.Play();
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsAttacking", false);
                            if (infoHit.collider.GetComponentInParent<SummonedAI>().isArcher == false)
                            {
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                                infoHit.transform.root.GetComponent<Animator>().SetBool("IsBlocking", false);
                            }
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsRunning", false);
                            infoHit.transform.root.GetComponent<AudioSource>().clip = null;
                            infoHit.transform.root.GetComponent<AudioSource>().Play();
                            objectToAttack.GetComponent<SummonedAI>().currentHP -= damage;
                            objectToAttack.GetComponent<SummonedAI>().block = false;
                            if (infoHit.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0)
                                infoHit.transform.root.gameObject.GetComponent<DeclineAnimationScript>().StunAnimation();
                        }
                    }
                }
            if (infoHit.transform.root.gameObject.CompareTag("SimplePeople")||infoHit.transform.root.gameObject.CompareTag("Civilian")||infoHit.transform.root.name=="Solovey")
                if (objectToAttack.CompareTag("SimplePeople")|| objectToAttack.CompareTag("Civilian")||infoHit.transform.root.name=="Solovey")
                {

                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.transform.root.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            selfAudio.Play();
                        }
                        infoHit.transform.root.GetComponent<Animator>().SetBool("IsAttacking", false);
                        if (!isArcher)
                        {
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                            infoHit.transform.root.GetComponent<Animator>().SetBool("IsBlocking", false);
                        }
                        infoHit.transform.root.GetComponent<Animator>().SetBool("IsRunning", false);
                        infoHit.transform.root.GetComponent<AudioSource>().clip = null;
                        infoHit.transform.root.GetComponent<AudioSource>().Play();
                            objectToAttack.GetComponent<PlayerController>().currentHealth -= damage*2;
                        if (infoHit.transform.root.gameObject.GetComponent<CivilianAI>().currentHP > 0)
                            infoHit.collider.transform.root.GetComponent<DeclineAnimationScript>().StunAnimation();
                        if (infoHit.collider.GetComponentInParent<CivilianAI>().currentHP <=0  && summoner.GetComponent<PlayerController>() != null) 
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                            StartCoroutine("KillExperienceShow");
                            gameManager.player.experience += infoHit.collider.GetComponentInParent<CivilianAI>().experience;
                        }
                    }
                }
            if (infoHit.collider.GetComponentInParent<AnimalAI>()!=null)
                if (objectToAttack.GetComponentInParent<AnimalAI>()!=null)
                {
                    //Play stun animation,disable scripts,make damage and play hit sound
                    if (infoHit.transform.root.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                    {
                        if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            selfAudio.Play();
                        }
                        objectToAttack.GetComponent<PlayerController>().currentHealth -= damage * 2;
                        if (infoHit.collider.GetComponentInParent<CivilianAI>().currentHP <= 0 && summoner.GetComponent<PlayerController>() != null)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                            StartCoroutine("KillExperienceShow");
                            gameManager.player.experience += infoHit.collider.GetComponentInParent<CivilianAI>().experience;
                        }
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
        {
            if (infoHit.collider.transform.root.gameObject.CompareTag("VillageGuard") || infoHit.collider.transform.root.gameObject.CompareTag("Bandit") || infoHit.collider.transform.root.gameObject.CompareTag("Republican") || infoHit.collider.transform.root.gameObject.CompareTag("Royalist") || infoHit.collider.transform.root.gameObject.CompareTag("Undead"))
                if (objectToAttack.CompareTag("VillageGuard")|| objectToAttack.CompareTag("Bandit") || objectToAttack.CompareTag("Undead") || objectToAttack.CompareTag("Royalist") || objectToAttack.CompareTag("Republican"))
                {
                    //If guard isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                    {
                        if (infoHit.collider.transform.root.gameObject.GetComponent<GuardAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f && !infoHit.collider.transform.root.GetComponent<GuardAI>().block)
                        {
                            if (damage - objectToAttack.GetComponent<GuardAI>().armor / 100f * damage > 0)
                                objectToAttack.GetComponent<GuardAI>().currentHP -= (int)(damage - objectToAttack.GetComponent<GuardAI>().armor / 100f * damage);
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                            selfAudio.Play();
                            if (infoHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0&&summoner.GetComponent<PlayerController>()!=null)
                            {
                                gameManager.player.experience += infoHit.collider.GetComponentInParent<GuardAI>().experience;
                                GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<GuardAI>().experience;
                                StartCoroutine("KillExperienceShow");
                            }
                        }
                        //If guard is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                        if (infoHit.collider.transform.root.GetComponent<GuardAI>().block && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                            selfAudio.Play();
                        }
                    }
                    else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                    {
                        if (infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f && !infoHit.collider.transform.root.GetComponent<SummonedAI>().block)
                        {
                            objectToAttack.GetComponent<SummonedAI>().currentHP -= damage;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                            selfAudio.Play();
                        }
                        //If guard is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                        if (infoHit.collider.transform.root.GetComponent<SummonedAI>().block && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                            selfAudio.Play();
                        }
                    }
                }
            if (infoHit.collider.transform.root.gameObject.CompareTag("Civilian")|| infoHit.collider.transform.root.gameObject.CompareTag("SimplePeople")||infoHit.collider.transform.root.name=="Solovey")
                if (objectToAttack.CompareTag("Civilian")|| objectToAttack.CompareTag("SimplePeople")||infoHit.collider.transform.root.name=="Solovey")
                {
                    //If civilian isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.transform.root.gameObject.GetComponent<CivilianAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                    {
                        objectToAttack.GetComponent<CivilianAI>().currentHP -= damage;
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                        selfAudio.Play();
                        if (infoHit.collider.GetComponentInParent<CivilianAI>().currentHP <= 0&&summoner.GetComponent<PlayerController>()!=null)
                        {
                            gameManager.player.experience += infoHit.collider.GetComponentInParent<CivilianAI>().experience;
                            GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                            StartCoroutine("KillExperienceShow");
                        }
                    }
                }
            if (infoHit.collider.GetComponentInParent<AnimalAI>()!=null)
                if (objectToAttack.GetComponentInParent<AnimalAI>()!=null)
                {
                    //If civilian isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.transform.root.gameObject.GetComponent<AnimalAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                    {
                        objectToAttack.GetComponent<AnimalAI>().currentHP -= damage;
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                        selfAudio.Play();
                        if (infoHit.collider.GetComponentInParent<AnimalAI>().currentHP <= 0 && summoner.GetComponent<PlayerController>() != null)
                        {
                            gameManager.player.experience += infoHit.collider.GetComponentInParent<AnimalAI>().experience;
                            GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().experience;
                            StartCoroutine("KillExperienceShow");
                        }
                    }
                }
            if (infoHit.collider.transform.root.gameObject.CompareTag("Player")|| infoHit.collider.transform.root.gameObject.CompareTag("Summoned"))
                if (objectToAttack.CompareTag("Player")|| objectToAttack.CompareTag("Summoned"))
                {
                    //If guard isn't blocking or is blocking but player's stamina is less than 10 do damage and play sound of hit
                    if (infoHit.collider.GetComponentInParent<PlayerController>() != null)
                    {
                        if (infoHit.collider.transform.root.gameObject.GetComponent<PlayerController>().currentHealth > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f && !infoHit.collider.transform.root.GetComponent<PlayerController>().block)
                        {
                            if (damage - objectToAttack.GetComponent<PlayerController>().armor / 100f * damage > 0)
                                objectToAttack.GetComponent<PlayerController>().currentHealth -= (int)(damage - objectToAttack.GetComponent<PlayerController>().armor / 100f * damage);
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                            selfAudio.Play();
                            objectToAttack.GetComponent<PlayerController>().StartCoroutine("DamageScreenAppear");
                        }
                        //If player is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                        if (infoHit.collider.GetComponentInParent<PlayerController>().block && infoHit.collider.GetComponentInParent<PlayerController>().currentStamina >= 10)
                        {
                            objectToAttack.GetComponent<PlayerController>().currentStamina -= 10;
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
                    else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                    {
                        if (infoHit.collider.transform.root.gameObject.GetComponent<SummonedAI>().currentHP > 0 && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f && !infoHit.collider.transform.root.GetComponent<SummonedAI>().block)
                        {
                            objectToAttack.GetComponent<SummonedAI>().currentHP -= damage;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                            selfAudio.Play();
                        }
                        //If guard is blocking and have more than 10 stamina play block sound and take 10 points of stamina from player
                        if (infoHit.collider.transform.root.GetComponent<SummonedAI>().block && (infoHit.collider.transform.root.transform.position - transform.position).magnitude <= 2.2f)
                        {
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                            selfAudio.Play();
                        }
                    }
                }
        }
    }
    //Activated on each attack animation.Prevent to attack before attack animation end
    public void SetIsAttacking()
    {
        isAttacking = false;
    }
    //Plus 1 to player's combatEnemies.If player has attacked village and length between player and guard less than 10 guard will chase and attack player
    public void movementControl()
    {
        if (summoner == gameManager.player.gameObject)
        {
            if (gameManager.player.combatEnemies == 0)
            {
                isAlerted = false;
                objectToAttack = gameManager.player.gameObject;
                if (name != "Solovey")
                {
                    selfAnimator.SetBool("IsAttacking", false);
                    if (!isArcher)
                    {
                        selfAnimator.SetBool("IsHugeAttacking", false);
                        selfAnimator.SetBool("IsBlocking", false);
                    }
                }
            }
            else if (gameManager.player.combatEnemies > 0)
            {
                isAlerted = true;
                bool haveObject = false;
                if (objectToAttack != gameManager.player.gameObject && objectToAttack != null)
                    haveObject = true;
                if (objectToAttack != null)
                    if (objectToAttack.tag == "SimplePeople" || objectToAttack.tag == "Civilian")
                        haveObject = false;
                if (objectToAttack != null)
                {
                    if (objectToAttack.tag == "VillageGuard"|| objectToAttack.tag == "Bandit" || objectToAttack.tag == "Republican" || objectToAttack.tag == "Royalist" || objectToAttack.tag == "Undead")
                    {
                        if(objectToAttack.GetComponent<GuardAI>()!=null)
                        if (objectToAttack.GetComponent<GuardAI>().currentHP <= 0)
                            haveObject = false;
                        if (objectToAttack.GetComponent<SummonedAI>() != null)
                            if (objectToAttack.GetComponent<SummonedAI>().currentHP <= 0)
                                haveObject = false;
                    }
                    if (objectToAttack.tag == "Civilian" || objectToAttack.tag == "SimplePeople")
                        if (objectToAttack.GetComponent<CivilianAI>().currentHP <= 0)
                            haveObject = false;
                    if (objectToAttack.GetComponent<AnimalAI>()!=null)
                        if (objectToAttack.GetComponent<AnimalAI>().currentHP <= 0)
                            haveObject = false;
                    if (gameManager.player.combatEnemies == 0)
                    {
                        isAlerted = false;
                        objectToAttack = gameManager.player.gameObject;
                        selfAnimator.SetBool("IsAttacking", false);
                        if (!isArcher)
                        {
                            selfAnimator.SetBool("IsHugeAttacking", false);
                            selfAnimator.SetBool("IsBlocking", false);
                        }
                        haveObject = true;
                    }
                }
                if (haveObject == false)
                {
                    if (gameManager.GetComponent<GameManager>().villageAttackedByPlayer == true)
                    {
                        for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                        {
                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0&&GameObject.FindObjectsOfType<GuardAI>()[i].tag=="VillageGuard")
                            {
                                objectToAttack = GameObject.FindObjectsOfType<GuardAI>()[i].gameObject;
                                haveObject = true;
                            }
                        }
                        for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
                        {
                            if (!haveObject)
                            {
                                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponent<CivilianAI>().currentHP > 0)
                                {
                                    objectToAttack = GameObject.FindObjectsOfType<CivilianAI>()[i].gameObject;
                                    haveObject = true;
                                }
                            }
                        }
                        if (!haveObject)
                        {
                            objectToAttack = gameManager.player.gameObject;
                            isAlerted = false;
                            selfAnimator.SetBool("IsAttacking", false);
                            if (!isArcher)
                            {
                                selfAnimator.SetBool("IsHugeAttacking", false);
                                selfAnimator.SetBool("IsBlocking", false);
                            }
                        }
                    }
                    if (gameManager.GetComponent<GameManager>().republicanAttackedByPlayer == true)
                    {
                        for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                        {
                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Republican")
                            {
                                objectToAttack = GameObject.FindObjectsOfType<GuardAI>()[i].gameObject;
                                haveObject = true;
                            }
                        }
                        if (!haveObject)
                        {
                            objectToAttack = gameManager.player.gameObject;
                            isAlerted = false;
                            selfAnimator.SetBool("IsAttacking", false);
                            if (!isArcher)
                            {
                                selfAnimator.SetBool("IsHugeAttacking", false);
                                selfAnimator.SetBool("IsBlocking", false);
                            }
                        }
                    }
                    if (gameManager.GetComponent<GameManager>().royalistAttackedByPlayer == true)
                    {
                        for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                        {
                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Royalist")
                            {
                                objectToAttack = GameObject.FindObjectsOfType<GuardAI>()[i].gameObject;
                                haveObject = true;
                            }
                        }
                        if (!haveObject)
                        {
                            objectToAttack = gameManager.player.gameObject;
                            isAlerted = false;
                            selfAnimator.SetBool("IsAttacking", false);
                            if (!isArcher)
                            {
                                selfAnimator.SetBool("IsHugeAttacking", false);
                                selfAnimator.SetBool("IsBlocking", false);
                            }
                        }
                    }
                    for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                        {
                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].GetComponent<GuardAI>().currentHP > 0 && (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit"|| GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Undead"))
                            {
                                objectToAttack = GameObject.FindObjectsOfType<GuardAI>()[i].gameObject;
                                haveObject = true;
                            }
                        }
                        if (!haveObject)
                        {
                            objectToAttack = gameManager.player.gameObject;
                            isAlerted = false;
                            selfAnimator.SetBool("IsAttacking", false);
                            if (!isArcher)
                            {
                                selfAnimator.SetBool("IsHugeAttacking", false);
                                selfAnimator.SetBool("IsBlocking", false);
                            }
                        }
                }
            }
        }
        else
        {
            GuardAI guardAI = summoner.GetComponent<GuardAI>();
            if (guardAI.objectToAttack==null)
            {
                isAlerted = false;
                objectToAttack = summoner;
                selfAnimator.SetBool("IsAttacking", false);
                if (!isArcher)
                {
                    selfAnimator.SetBool("IsHugeAttacking", false);
                    selfAnimator.SetBool("IsBlocking", false);
                }
            }
            else if (guardAI.objectToAttack!=null)
            {
                isAlerted = true;
                bool haveObject = false;
                if (objectToAttack != summoner && objectToAttack != null)
                    haveObject = true;
                if (objectToAttack != null)
                    if (objectToAttack.tag == "SimplePeople" || objectToAttack.tag == "Civilian")
                        haveObject = false;
                if (objectToAttack != null)
                {
                    if (objectToAttack.GetComponent<GuardAI>()!=null)
                        if (objectToAttack.GetComponent<GuardAI>().currentHP <= 0)
                            haveObject = false;
                    if (objectToAttack.GetComponent<SummonedAI>() != null)
                        if (objectToAttack.GetComponent<SummonedAI>().currentHP <= 0)
                            haveObject = false;
                    if (objectToAttack.GetComponent<PlayerController>() != null)
                        if (objectToAttack.GetComponent<PlayerController>().currentHealth <= 0)
                            haveObject = false;
                    if (objectToAttack.tag == "Civilian" || objectToAttack.tag == "SimplePeople")
                        if (objectToAttack.GetComponent<CivilianAI>().currentHP <= 0)
                            haveObject = false;
                    if (objectToAttack.GetComponent<AnimalAI>()!=null)
                        if (objectToAttack.GetComponent<AnimalAI>().currentHP <= 0)
                            haveObject = false;
                    if (guardAI.objectToAttack==null)
                    {
                        isAlerted = false;
                        objectToAttack = summoner;
                        selfAnimator.SetBool("IsAttacking", false);
                        if (!isArcher)
                        {
                            selfAnimator.SetBool("IsHugeAttacking", false);
                            selfAnimator.SetBool("IsBlocking", false);
                        }
                        haveObject = true;
                    }
                }
                if (haveObject == false)
                    if(guardAI.objectToAttack!=null)
                    objectToAttack = guardAI.objectToAttack;
            }
        }
    }
    //Movement script
    public void Movement()
    {
        if (objectToAttack != null)
        {
            selfAgent.SetDestination(GameObject.Find(objectToAttack.name).transform.position);
            //If length between player and guard more than 2.5 guard will run
            if (!isArcher && currentHP > 0)
            {
                if (summoner == gameManager.player.gameObject)
                {
                    Vector3 rayStart = transform.position + transform.up;
                    if (gameManager.player.isCrouched)
                        rayStart = transform.position + transform.up * 0.4f;
                    Ray ray = new Ray(rayStart, objectToAttack.transform.position - transform.position);
                    LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard") & ~(1 << LayerMask.NameToLayer("SummonedArrow")));
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer))
                    {
                        if (objectToAttack != gameManager.player.gameObject)
                            if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 2.2f && gameManager.player.GetComponent<PlayerController>().combatEnemies > 0) || ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 4 && gameManager.player.GetComponent<PlayerController>().combatEnemies == 0))
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                block = false;
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(GameObject.Find(objectToAttack.name).transform.position);
                            }
                        if (objectToAttack == gameManager.player.gameObject)
                            if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 3 && gameManager.player.combatEnemies > 0) || ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 3 && gameManager.player.combatEnemies == 0))
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                block = false;
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(objectToAttack.transform.position);
                            }
                        //If length between objectToAttack and summoned less tthan 2.2 summoned attack objectToAttack;
                        if (objectToAttack != gameManager.player.gameObject)
                            if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 2.2f && gameManager.player.combatEnemies > 0 && rayHit.collider.gameObject == objectToAttack)
                            {
                                if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = null;
                                    selfAudio.loop = false;
                                }
                                selfAgent.isStopped = true;
                                RotateTowards(objectToAttack.transform);
                            }
                            //Run if object is farrer than 2.2
                            else if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 2.2f || rayHit.collider.gameObject != objectToAttack)&& gameManager.player.combatEnemies > 0 && objectToAttack != gameManager.player.gameObject)
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                block = false;
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(objectToAttack.transform.position);
                            }
                        //Stop if player
                        if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 3 && (gameManager.player.combatEnemies <= 0 || objectToAttack == gameManager.player.gameObject))
                        {
                            selfAnimator.SetBool("IsRunning", false);
                            selfAnimator.Play("Idle");
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
                else
                {
                    Vector3 rayStart = transform.position + transform.up;
                    if (gameManager.player.isCrouched)
                        rayStart = transform.position + transform.up * 0.4f;
                    Ray ray = new Ray(rayStart, objectToAttack.transform.position - transform.position);
                    LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard") & ~(1 << LayerMask.NameToLayer("SummonedArrow")));
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer))
                    {
                        GuardAI guardAI = summoner.GetComponent<GuardAI>();
                        if (objectToAttack != summoner)
                            if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 2.2f && guardAI.objectToAttack != null) || ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 4 && guardAI.objectToAttack == null))
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                block = false;
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(objectToAttack.transform.position);
                            }
                        if (objectToAttack == summoner)
                            if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 6 && guardAI.objectToAttack != null) || ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 5 && guardAI.objectToAttack == null))
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                block = false;
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(objectToAttack.transform.position);
                            }
                        //If length between objectToAttack and summoned less tthan 2.2 summoned attack objectToAttack;
                        if (objectToAttack != summoner)
                            if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 2.2f && guardAI.objectToAttack != null&&rayHit.collider.gameObject==objectToAttack)
                            {
                                if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = null;
                                    selfAudio.loop = false;
                                }
                                selfAnimator.SetBool("IsRunning", false);
                                selfAgent.isStopped = true;
                                RotateTowards(objectToAttack.transform);
                            }
                            //Run if object is farrer than 2.2
                            else if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 2.2f|| rayHit.collider.gameObject != objectToAttack) && guardAI.objectToAttack != null && objectToAttack != summoner)
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                block = false;
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(objectToAttack.transform.position);
                            }
                        //Stop if player
                        if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 4 && (guardAI.objectToAttack == null || objectToAttack == summoner))
                        {
                            selfAnimator.SetBool("IsRunning", false);
                            selfAnimator.Play("Idle");
                            if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                            {
                                selfAudio.clip = null;
                                selfAudio.loop = false;
                            }
                            selfAnimator.SetBool("IsRunning", false);
                            selfAgent.isStopped = true;
                            RotateTowards(objectToAttack.transform);
                        }
                    }
                }
            }
        }
        }
    private void MovementForArcher()
    {
        if (objectToAttack == gameManager.player.gameObject&&summoner==gameManager.player.gameObject)
        {
                    if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 5 && (gameManager.player.combatEnemies <= 0 || objectToAttack == gameManager.player.gameObject))
            {
                selfAnimator.SetBool("IsRunning", false);
                selfAnimator.Play("Idle");
                if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                {
                    selfAudio.clip = null;
                    selfAudio.loop = false;
                }
                selfAgent.isStopped = true;
                RotateTowards(objectToAttack.transform);
            }
            if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 5 && gameManager.player.combatEnemies > 0) || ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 5 && gameManager.player.combatEnemies == 0))
            {
                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                {
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                    selfAudio.loop = true;
                    selfAudio.Play();
                }
                selfAnimator.Play("Run");
                selfAnimator.SetBool("IsRunning", true);
                isAttacking = false;
                if (currentHP > 0)
                    selfAgent.isStopped = false;
                selfAgent.SetDestination(objectToAttack.transform.position);
            }
        }
        else if(objectToAttack==summoner&&summoner!=gameManager.player.gameObject)
        {
            GuardAI guardAI = summoner.GetComponent<GuardAI>();
            if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 5 && ( guardAI.objectToAttack==null || objectToAttack == summoner))
            {
                selfAnimator.SetBool("IsRunning", false);
                selfAnimator.Play("Idle");
                if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                {
                    selfAudio.clip = null;
                    selfAudio.loop = false;
                }
                selfAgent.isStopped = true;
                RotateTowards(objectToAttack.transform);
            }
            if (((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 5 && guardAI.objectToAttack!=null) || ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 5 && guardAI.objectToAttack==null))
            {
                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                {
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                    selfAudio.loop = true;
                    selfAudio.Play();
                }
                selfAnimator.Play("Run");
                selfAnimator.SetBool("IsRunning", true);
                isAttacking = false;
                if (currentHP > 0)
                    selfAgent.isStopped = false;
                selfAgent.SetDestination(objectToAttack.transform.position);
            }
        }
        if (isAlerted)
        {
            if (isArcher)
            {               
                if (objectToAttack != summoner) {
                    Vector3 rayDirection = (objectToAttack.transform.position + objectToAttack.transform.up * 1.5f) - transform.Find("RayStart").transform.position;
                    if (gameManager.player.isCrouched)
                        rayDirection = objectToAttack.transform.position - transform.Find("RayStart").transform.position;
                    Ray aimRay = new Ray(transform.Find("RayStart").transform.position, rayDirection);
                    LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore")) & ~(1 << LayerMask.NameToLayer("Arrow")) & ~(1 << LayerMask.NameToLayer("ArrowPlayer")) & ~(1 << LayerMask.NameToLayer("ArrowGuard") & ~(1 << LayerMask.NameToLayer("SummonedArrow")));
                    RaycastHit aimHit;
                        if (Physics.Raycast(aimRay, out aimHit, Mathf.Infinity, layer))
                        {                            
                            //If length between player and archer more than 8 archer will run
                            if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude > 8)
                            {
                                if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                                    selfAudio.loop = true;
                                    selfAudio.Play();
                                }
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                selfAnimator.SetBool("IsAttacking", false);
                                isAttacking = false;
                                FindArrowMeshRenderer(gameObject).enabled = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(objectToAttack.transform.position);
                            }
                            //If length between player and archer less tthan 8 archer attack player
                            else if ((GameObject.Find(objectToAttack.name).transform.position - transform.position).magnitude <= 8 && aimHit.collider.transform.root.gameObject == objectToAttack)
                            {
                                if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
                                {
                                    selfAudio.clip = null;
                                    selfAudio.loop = false;
                                }
                                selfAnimator.SetBool("IsRunning", false);
                                selfAgent.isStopped = true;
                                RotateTowards(objectToAttack.transform);
                            FindArrowMeshRenderer(gameObject).enabled = true;
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
                                selfAnimator.Play("Run");
                                selfAnimator.SetBool("IsRunning", true);
                                selfAnimator.SetBool("IsAttacking", false);
                                isAttacking = false;
                                if (currentHP > 0)
                                    selfAgent.isStopped = false;
                                selfAgent.SetDestination(FindPosition());
                            FindArrowMeshRenderer(gameObject).enabled = false;
                                seeEnemy = false;

                            }
                        }
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
        //Spawn Arrow
        Vector3 velocity = transform.forward * 30;
        GameObject spawnArrow = Instantiate(gameManager.arrow, transform.position+Vector3.up, Quaternion.Euler(0.917f, -179, -177));
        IgnoreCollision(gameObject, spawnArrow);
        spawnArrow.GetComponent<Rigidbody>().velocity = velocity;
        spawnArrow.GetComponent<Item>().item = gameManager.arrow;
        spawnArrow.GetComponent<Arrow>().arrowDamage = damage;
        spawnArrow.GetComponent<Arrow>().shooter = gameObject;
        spawnArrow.layer = 10;
        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().archerySound;
        selfAudio.Play();
        FindArrowMeshRenderer(gameObject).enabled = true;
        selfAnimator.SetBool("IsAttacking", false);
        isAttacking = false;
    }
    //If guard is attacking objectToAttack and is near him rotate toward it
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360);
    }
    //Cancel guard's block after 2 seconds since guard started blocking
    private IEnumerator CancelBlock()
    {
        yield return new WaitForSeconds(2f);
        selfAnimator.SetBool("IsBlocking", false);
        block = false;
    }
    private IEnumerator KillAfterTime()
    {
        yield return new WaitForSeconds(120);
        currentHP = 0;
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
    private void IgnoreCollision(GameObject objectIgnore, GameObject bullet)
    {
        for (int i = 0; i < objectIgnore.transform.childCount; i++)
        {
            if (objectIgnore.transform.GetChild(i).gameObject.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(objectIgnore.transform.GetChild(i).gameObject.GetComponent<Collider>(), bullet.GetComponent<BoxCollider>(), true);
                Physics.IgnoreCollision(objectIgnore.transform.GetChild(i).gameObject.GetComponent<Collider>(), bullet.GetComponent<MeshCollider>(), true);
            }
            if (objectIgnore.transform.GetChild(i).transform.childCount > 0)
                IgnoreCollision(objectIgnore.transform.GetChild(i).gameObject, bullet);
        }
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullet.GetComponent<BoxCollider>());
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullet.GetComponent<MeshCollider>());
    }
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
            if(rendererToReturn!=null)
            if (rendererToReturn.name == "Arrow")
                return rendererToReturn;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                rendererToReturn = FindArrowMeshRenderer(objectToSearch.transform.GetChild(i).gameObject);
        }
        return rendererToReturn;
    }
    //Find bow in children
    private Transform FindBow(GameObject objectToSearch)
    {
        Transform bowToReturn = null;
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            if (objectToSearch.transform.GetChild(i).gameObject.name == "Bow")
                return objectToSearch.transform.GetChild(i);
            if (bowToReturn.name == "Bow")
                return bowToReturn;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                bowToReturn = FindBow(objectToSearch.transform.GetChild(i).gameObject);
        }
        return bowToReturn;
    }
    private void LoadSpecialData()
    {
        SpecialData specialData = SaveLoad.globalSpecialData;
        soloveyHP = specialData.soloveyHP;
    }
}
