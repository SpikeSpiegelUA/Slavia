using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    public float stableCrouchingSpeed;
    public float stableLightSpeed;
    public float stableHardSpeed;
    public int combatEnemies = 0;
    DialogueManager dialogueManager;
    public Camera main;
    private Image pointer;
    public Image itemInfo;
    private float mouseSensitivity = 100;
    public float speed = 5;
    public Inventory inventory;
    private Vector3 playerMovement;
    public int currentHealth = 0;
    public int currentMana = 0;
    public int currentStamina = 0;
    public int hpPlayer = 100;
    public int manaPlayer = 100;
    public int staminaPlayer = 100;
    public bool block;
    public bool dialogueIsActive = false;
    public GameObject arrow;
    public bool isCrouched = false;
    public GameObject stealthBar;
    public GameObject fireball;
    public GameObject summonedMelee;
    public GameObject summonedArcher;
    public GameObject currentSummonedMelee;
    public GameObject currentSummonedArcher;
    public bool isDetected;
    public int collisionCount = 0;
    private bool addToCollisionCount = true;
    public int stealthAttackModify = 2;
    public GameObject lootWindow;
    public GameObject goldWindow;
    public int gold = 0;
    public Text goldText;
    public int chanceForGrab = 25;
    public GameObject grabWindow;
    public GameObject pickpocketingWindow;
    public float armor;
    private Animator animator;
    public int prestige;
    public GameObject locked;
    public GameObject needPicklock;
    public int chanceForUnlock = 25;
    public GameObject unlockFailed;
    public GameObject questCompleted;
    public GameObject newStage;
    public GameObject newQuest;
    public GameObject questFailed;
    public GameObject newLevel;
    public int skillPoints;
    public int experience;
    public int fireballDamageModify = 0;
    public int recoverModify = 0;
    public int level;
    public int experienceForNextLevel = 100;
    public GameObject killExperience;
    public int potionAttackModify = 0;
    public int potionArmorModify = 0;
    private GUIController guiController;
    private AudioSource selfAudio;
    public GameObject stormPrefab;
    [HideInInspector]
    public Vector2 runAxis;
    [HideInInspector]
    public Vector2 lookAxis;
    public GameObject actionButton;
    // Start is called before the first frame update
    void Awake()
    {
        if (SaveLoad.isLoading)
        LoadPlayerData();
    }
    void Start()
    {
        selfAudio = GetComponent<AudioSource>();
        guiController = GameObject.Find("GUIManager").GetComponent<GUIController>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        if (!SaveLoad.isLoading)
        {
            currentHealth = hpPlayer;
            currentMana = manaPlayer;
            currentStamina = staminaPlayer;
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber) 
            {
                GameObject.Find("GUIManager").GetComponent<Inventory>().Take(GameObject.Find("GUIManager").GetComponent<Inventory>().daggerPrefab,20);
                GetComponent<Dialogue>().sentences[0] = "You are an ordinary robber from the guild of thieves of Wolfelmtown. Your boss has found a client who is ready to buy the Dragon Scroll for a good price. Of course, you will get a good share. You need this money to take yourself and your family to the Federation. Maybe,the Head of the Village knows something";
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                GameObject.Find("GUIManager").GetComponent<Inventory>().Take(GameObject.Find("GUIManager").GetComponent<Inventory>().waraxePrefab, 20);

                GetComponent<Dialogue>().sentences[0] = "You are a paladin who came from the Central region.The Order sent you to Lovanya to help defeat the forces of Bamur, which are gaining strength.You have heard the legend of the Dragon Scroll, which has great power. Find it to free this land from the forces of evil.Maybe,the Head of the Village knows something";
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                GameObject.Find("GUIManager").GetComponent<Inventory>().Take(GameObject.Find("GUIManager").GetComponent<Inventory>().fireballPrefab, 20);
                GetComponent<Dialogue>().sentences[0] = "You are a magician who came from the Guild of Magicians of Bosmark. The Guild has found confirmation that the legendary Dragon Scroll really exists. They want to study it and hide it so as not to give this power into unreliable hands. Find the scroll. Perhaps the head of the village knows something";
            }
            dialogueManager.SetDialoguer(gameObject);
        }
        pointer = GameObject.Find("Pointer").GetComponent<Image>();
        inventory = GameObject.Find("GUIManager").GetComponent<Inventory>();
        GetComponentInChildren<HandWeaponScript>().inventory= GameObject.Find("GUIManager").GetComponent<Inventory>();
        StartCoroutine("Regen");
        stableLightSpeed = 5;
        stableHardSpeed = 3;
        stableCrouchingSpeed = 2;
        GetComponentInChildren<CameraMovement>().gameObject.transform.localEulerAngles = new Vector3(GetComponentInChildren<CameraMovement>().gameObject.transform.localEulerAngles.x, -3.456f, 0);
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[5] = "Are you from the guild? Thank you for your help Democracy";
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[5] = "Are you from the Order? Is it true that the Order helps us?";
    }
    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0)
        {
            selfAudio.clip = null;
            selfAudio.Play();
        }
        if(SaveLoad.isLoading)
            SaveLoad.isLoading = false;
        if (experience >= experienceForNextLevel)
        {
            experienceForNextLevel =(int) (experienceForNextLevel* 1.5f);
            skillPoints++;
            level++;
            StartCoroutine("NewLevel");
        }
        //Disable scripst if player is dead
        if (currentHealth <= 0)
        {
            GetComponent<DeclineAnimationScript>().enabled = false;
            StartCoroutine("DeathMenu");
            selfAudio.clip = null;
            selfAudio.Play();
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponentInChildren<CameraMovement>().enabled = false;
            GetComponent<Animator>().SetBool("IsRunning", false);
            GetComponent<Animator>().SetBool("IsAttacking", false);
            GetComponent<Animator>().SetBool("IsHugeAttacking", false);
            GetComponent<Animator>().SetBool("IsBlocking", false);
            GetComponent<Animator>().SetBool("IsDrawingArrow", false);
            GetComponent<Animator>().SetBool("IsStunned", false);
            GetComponent<Animator>().SetBool("IsCrouching", false);
            GetComponent<Animator>().SetBool("IsCrouchRunning", false);
            GetComponent<Animator>().SetBool("IsStaying", false);
            GetComponent<Animator>().SetBool("TwoHand", false);
            GetComponent<Animator>().SetBool("IsDead", true);
            GetComponent<Animator>().Play("Death");
            GameObject.Find("Main Camera").transform.localPosition = new Vector3(-0.5f, 3.82f, 0.3f);
            GameObject.Find("Main Camera").transform.localEulerAngles = new Vector3(86, -3, 0);
        }
        Movement();
        InfoRay();
        AnimatorPlayer();
        InfoRayForTriggers();
        if (currentHealth > hpPlayer)
            currentHealth = hpPlayer;
        if (currentMana > manaPlayer)
            currentMana = manaPlayer;
        if (currentStamina > staminaPlayer)
            currentStamina = staminaPlayer;
        if (combatEnemies < 0)
            combatEnemies = 0;
    }
    //If E pressed start Ray to recognize object for dialog
    public void StartDialogue()
    {
        if (!isCrouched)
        {
            Ray ray = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f,Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                //If player started talking to guard make a link to gameobject with dialogs for special bots
                if (hit.collider.transform.root.tag == "Civilian" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
                {
                    if (hit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                    {
                        dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                        guiController.enabled = false;
                        CancelActionsAfterStartDialogue();
                        dialogueIsActive = true;
                    }
                }
                if (hit.collider.transform.root.tag == "Neutral")
                {
                        dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                        guiController.enabled = false;
                        CancelActionsAfterStartDialogue();
                        dialogueIsActive = true;
                }
                //If player started talking to guard make a link to gameobject with dialogs for guard
                if (hit.collider.transform.root.tag == "VillageGuard" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false && hit.collider.transform.root.GetComponent<SummonedAI>() == null)
                {
                    if (hit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                    {
                            if (hit.collider.transform.root.GetComponent<Dialogue>() == null)
                                dialogueManager.SetDialoguer(GameObject.Find("GuardDialogs"));
                            else if (hit.collider.transform.root.GetComponent<Dialogue>() != null)
                                dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                        guiController.enabled = false;
                        CancelActionsAfterStartDialogue();
                        dialogueIsActive = true;
                    }
                }
                //If player started talking to bandit make a link to gameobject with dialogs for guard
                if (hit.collider.transform.root.tag == "Bandit" && GameObject.Find("GameManager").GetComponent<GameManager>().isRobber&&hit.collider.transform.root.GetComponent<SummonedAI>() == null)
                {
                    if (hit.collider.GetComponentInParent<Dialogue>().personName != "Solovey")
                    {
                        if (hit.collider.gameObject.GetComponentInParent<GuardAI>() != null)
                            if (hit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                            {
                                if (hit.collider.GetComponentInParent<Dialogue>() == null)
                                    dialogueManager.SetDialoguer(GameObject.Find("BanditDialogs"));
                                else
                                    dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                                guiController.enabled = false;
                                CancelActionsAfterStartDialogue();
                                dialogueIsActive = true;
                            }
                        if (hit.collider.gameObject.GetComponentInParent<CivilianAI>() != null)
                            if (hit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                            {
                                if (hit.collider.GetComponentInParent<Dialogue>() == null)
                                    dialogueManager.SetDialoguer(GameObject.Find("BanditDialogs"));
                                else
                                    dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                                guiController.enabled = false;
                                CancelActionsAfterStartDialogue();
                                dialogueIsActive = true;
                            }
                    }
                }
                //If player started talking to bandit make a link to gameobject with dialogs for guard
                if (hit.collider.transform.root.tag == "Republican" && !GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer && hit.collider.transform.root.GetComponent<SummonedAI>() == null)
                {
                        if (hit.collider.gameObject.GetComponentInParent<GuardAI>() != null)
                            if (hit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                            {
                                if (hit.collider.GetComponentInParent<Dialogue>() == null)
                                    dialogueManager.SetDialoguer(GameObject.Find("RepublicanDialogs"));
                                else
                                    dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                                guiController.enabled = false;
                                CancelActionsAfterStartDialogue();
                                dialogueIsActive = true;
                            }
                }
                if (hit.collider.transform.root.tag == "Royalist" && !GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer && hit.collider.transform.root.GetComponent<SummonedAI>() == null)
                {
                    if (hit.collider.gameObject.GetComponentInParent<GuardAI>() != null)
                        if (hit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                        {
                            if (hit.collider.GetComponentInParent<Dialogue>() == null)
                                dialogueManager.SetDialoguer(GameObject.Find("RoyalistDialogs"));
                            else
                                dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                            guiController.enabled = false;
                            CancelActionsAfterStartDialogue();
                            dialogueIsActive = true;
                        }
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest")!=null)
                if (hit.collider.transform.root.name == "Solovey"&& !GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer&& GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage==6)
                {
                    dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                    guiController.enabled = false;
                    CancelActionsAfterStartDialogue();
                    dialogueIsActive = true;
                }
                //If player started talking to simple citizen make a link to gameobject with dialogs for simple citizen
                if (hit.collider.transform.root.tag == "SimplePeople" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false&& hit.collider.transform.root.name!="Village Merchant"&& hit.collider.transform.root.name!="Bandit Merchant")
                {
                    if (hit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                    {
                        dialogueManager.SetDialoguer(GameObject.Find("SimplePeopleDialogs"));
                        guiController.enabled = false;
                        CancelActionsAfterStartDialogue();
                        dialogueIsActive = true;
                    }
                }
                //If player started talking to merchant make a link to his dialogs
                if (hit.collider.transform.root.tag == "SimplePeople" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false && (hit.collider.transform.root.name == "Village Merchant" || hit.collider.transform.root.name == "Bandit Merchant"))
                {
                    if (hit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                    {
                        dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                        guiController.enabled = false;
                        CancelActionsAfterStartDialogue();
                        dialogueIsActive = true;
                    }
                }
                //Read book
                if (hit.collider.transform.root.tag == "Book")
                {
                        dialogueManager.SetDialoguer(hit.collider.transform.root.gameObject);
                        guiController.enabled = false;
                        CancelActionsAfterStartDialogue();
                        dialogueIsActive = true;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive)
                    {
                        if (hit.collider.GetComponent<Dialogue>() != null)
                            if (hit.collider.GetComponent<Dialogue>().personName == "Ben's diary")
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().FayeQuestStageTwo();
                        if (hit.collider.GetComponent<Dialogue>() != null)
                            if (hit.collider.GetComponent<Dialogue>().personName == "Strange book")
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().FayeQuestStageThree();
                    }
                }
            }
        }
    }
    //Take Item when pressed E
    public void TakeItem()
    {
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
            Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
            {
                if (hitItem.collider.tag == "Item"&&hitItem.collider.GetComponent<Item>().itemName!="Arrow")
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().CheckIfHavePlace(hitItem.collider.transform.root.gameObject))
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().listOfItemsOnScene.Remove(hitItem.collider.transform.root.gameObject);
                        Destroy(hitItem.collider.transform.root.gameObject);
                    }
                    inventory.Take(hitItem.collider.transform.root.gameObject, 20);
                }
                if (hitItem.collider.tag == "Item" && hitItem.collider.GetComponent<Item>().itemName== "Arrow")
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().CheckIfHavePlace(hitItem.collider.gameObject))
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().listOfItemsOnScene.Remove(hitItem.collider.gameObject);
                        Destroy(hitItem.collider.gameObject);
                    }
                    inventory.Take(hitItem.collider.gameObject, 20);
                }
            }                
    }
    public void TakeItemForTriggers()
    {
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
            Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer))
            {
                if(hitItem.collider.GetComponent<Collider>()!=null)
                    if(hitItem.collider.GetComponent<Collider>().isTrigger)
                if (hitItem.collider.tag == "Item")
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().CheckIfHavePlace(hitItem.collider.transform.root.gameObject))
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().listOfItemsOnScene.Remove(hitItem.collider.transform.root.gameObject);
                        Destroy(hitItem.collider.transform.root.gameObject);
                    }
                    inventory.Take(hitItem.collider.transform.root.gameObject, 20);
                }
            }
    }
    //Unlock locked crate
    public void UnlockCrate()
    {
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
            Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer, QueryTriggerInteraction.Ignore))
            {
                if (hitItem.collider.transform.root.tag == "Crate")
                {
                    if (hitItem.collider.GetComponentInParent<LootCrate>().isClose)
                    {
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                        {
                            int indexOfItem = -1;
                            for (int i = 0; i < inventory.images.Length; i++)
                            {
                                if (inventory.images[i].GetComponent<SlotInfo>().item != null)
                                    if (inventory.images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Lockpick")
                                    {
                                        indexOfItem = i;
                                        if (inventory.images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Permanent")
                                            break;
                                    }
                            }
                            if (indexOfItem == -1)
                            {
                                StartCoroutine("NeedLockPick");
                                CheckIfSee();
                            }
                            else
                            {
                                CheckIfSee();
                                if (inventory.images[indexOfItem].GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "OneOff")
                                {
                                    if (inventory.images[indexOfItem].GetComponent<SlotInfo>().amountOfItems > 1)
                                        inventory.images[indexOfItem].GetComponent<SlotInfo>().amountOfItems--;
                                    else
                                    {
                                        inventory.images[indexOfItem].GetComponent<SlotInfo>().item = null;
                                        inventory.images[indexOfItem].GetComponent<SlotInfo>().amountOfItems = 0;
                                        inventory.images[indexOfItem].GetComponent<Image>().sprite = null;
                                        inventory.images[indexOfItem].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                        if (inventory.filledSlots > 0)
                                            inventory.filledSlots--;
                                    }
                                }
                                int currentChanceForUnlock = Random.Range(0, 101);
                                if (currentChanceForUnlock <= chanceForUnlock)
                                {
                                    hitItem.collider.GetComponentInParent<LootCrate>().isClose = false;
                                    locked.SetActive(false);
                                    unlockFailed.SetActive(false);
                                }
                                else
                                    StartCoroutine("UnlockFailed");
                            }
                        }
                        else
                        {
                            StopCoroutine("NeedToBeRobber");
                            StartCoroutine("NeedToBeRobber");
                        }
                    }
                }
            }
    }
    //Check if enemies see player
    private void CheckIfSee()
    {
        for(int i = 0; i < GameObject.FindObjectsOfType<ConeOfView>().Length; i++)
        {
            Ray forwardRay = new Ray(GameObject.FindObjectsOfType<ConeOfView>()[i].transform.position, GameObject.FindObjectsOfType<ConeOfView>()[i].transform.forward);
            Ray playerRay = new Ray(GameObject.FindObjectsOfType<ConeOfView>()[i].transform.position+ GameObject.FindObjectsOfType<ConeOfView>()[i].transform.up*0.5f, this.transform.position - GameObject.FindObjectsOfType<ConeOfView>()[i].transform.position);
            RaycastHit checkIfSee;
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore"));
            if ((GameObject.FindObjectsOfType<ConeOfView>()[i].transform.position - transform.position).magnitude <= 10)
            {
                if(Physics.Raycast(playerRay,out checkIfSee, Mathf.Infinity, layer))
                {
                    if (Vector3.Angle(forwardRay.direction, playerRay.direction) <= 75 && (GameObject.FindObjectsOfType<ConeOfView>()[i].transform.position - transform.position).magnitude <= 10 && checkIfSee.collider.tag == "Player")
                    {
                        if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>() != null)
                        {
                            if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().currentHP > 0)
                            {
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().detection = 100;
                                    isDetected = true;
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().hasBeenAttacked = true;
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().attacker = gameObject;
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().StartCoroutine("RunRegimeCancel");                               
                                     GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                //Code for calling another people
                                CallNear();
                            }
                        }
                        else if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>() != null)
                        {                     
                                if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().currentHP > 0)
                                {
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().detection = 100;
                                            isDetected = true;
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().objectToAttack = gameObject;
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().isAlerted = true;
                                GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().StartCoroutine("CancelAlert");
                                if(GameObject.FindObjectsOfType<ConeOfView>()[i].tag=="VillageGuard")
                                            GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                if (GameObject.FindObjectsOfType<ConeOfView>()[i].tag == "Republican")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                if (GameObject.FindObjectsOfType<ConeOfView>()[i].tag == "Royalist")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer= true;
                            }
                                    //Code for calling another people
                                    CallNear();
                                }
                    }                 
                }
            }
        }
    }
    //Open window for looting
    public void OpenLootWindow()
    {
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
            Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
            {
                if (hitItem.collider.transform.root.tag == "SimplePeople" || hitItem.collider.transform.root.tag == "Civilian")
                {
                    if (hitItem.collider.GetComponentInParent<CivilianAI>().currentHP <= 0)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot = hitItem.collider.transform.root.gameObject;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold > 0)
                        {
                            gold += GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold;
                            goldWindow.GetComponentInChildren<Text>().text = "Gold:+" + GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold;
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold = 0;
                            goldText.text = "Gold:" + gold;
                            goldWindow.SetActive(true);
                            StartCoroutine("CloseGoldWindow");
                        }
                        lootWindow.SetActive(true);
                        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                        GameObject.Find("Main Camera").GetComponent<CameraMovement>().enabled = false;
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                        guiController.personName.GetComponent<Image>().enabled = false;
                        guiController.personNameText.GetComponent<Text>().text = "";
                    guiController.control.SetActive(false);
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                            GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
                        else
                            GameObject.Find("Player").GetComponent<Animator>().Play("CrouchingIdle");
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                     selfAudio.clip = null;
                        for (int i = 0; i < hitItem.collider.GetComponentInParent<Loot>().loot.Length; i++)
                        {
                            if (hitItem.collider.GetComponentInParent<Loot>().loot[i] != null)
                            {
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().item = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Item>().item;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<Image>().sprite = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Image>().sprite;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().amountOfItems = hitItem.collider.GetComponentInParent<Loot>().amountOfItems[i];
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color = new Color(GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.r, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.g, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.b, 1);
                            }
                        }
                    }
                }
                if (hitItem.collider.transform.root.tag == "VillageGuard" || hitItem.collider.transform.root.tag == "Summoned"|| hitItem.collider.transform.root.tag == "Bandit"|| hitItem.collider.transform.root.tag == "Republican"|| hitItem.collider.transform.root.tag == "Royalist" || hitItem.collider.transform.root.tag == "Undead")
                { 
                    if ((hitItem.collider.GetComponentInParent<GuardAI>() != null))
                        if (hitItem.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                        {
                            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                                if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage==2)
                            if (hitItem.collider.transform.root.name == "PatrolRoyalist2")
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansQuestStageThree();
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot = hitItem.collider.transform.root.gameObject;
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold > 0)
                            {
                                gold += GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold;
                                goldWindow.GetComponentInChildren<Text>().text = "Gold:+" + GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold;
                                GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold = 0;
                                goldText.text = "Gold:" + gold;
                                goldWindow.SetActive(true);
                                StartCoroutine("CloseGoldWindow");
                            }
                            if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                                GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
                            else
                                GameObject.Find("Player").GetComponent<Animator>().Play("CrouchingIdle");
                            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
                            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
                            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                        guiController.control.SetActive(false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
                            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouchRunning", false);
                      selfAudio.clip = null;
                            lootWindow.SetActive(true);
                            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                            GameObject.Find("Main Camera").GetComponent<CameraMovement>().enabled = false;
                            guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                            guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                            guiController.personName.GetComponent<Image>().enabled = false;
                            guiController.personNameText.GetComponent<Text>().text = "";
                            for (int i = 0; i < hitItem.collider.GetComponentInParent<Loot>().loot.Length; i++)
                            {
                                if (hitItem.collider.GetComponentInParent<Loot>().loot[i] != null)
                                {
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().item = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Item>().item;
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<Image>().sprite = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Image>().sprite;
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().amountOfItems = hitItem.collider.GetComponentInParent<Loot>().amountOfItems[i];
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color = new Color(GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.r, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.g, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.b, 1);
                                }
                            }
                        }
                        if ((hitItem.collider.GetComponentInParent<SummonedAI>() != null))
                            if (hitItem.collider.GetComponentInParent<SummonedAI>().currentHP <= 0)
                            {
                                GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot = hitItem.collider.transform.root.gameObject;
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold > 0)
                                {
                                    gold += GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold;
                                    goldWindow.GetComponentInChildren<Text>().text = "Gold:+" + GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold;
                                    GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfGold = 0;
                                    goldText.text = "Gold:" + gold;
                                    goldWindow.SetActive(true);
                                    StartCoroutine("CloseGoldWindow");
                                }
                                if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                                    GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
                                else
                                    GameObject.Find("Player").GetComponent<Animator>().Play("CrouchingIdle");
                                GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
                                GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
                        guiController.control.SetActive(false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                                GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
                                GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                                GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouchRunning", false);
                           selfAudio.clip = null;
                                lootWindow.SetActive(true);
                                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                                GameObject.Find("Main Camera").GetComponent<CameraMovement>().enabled = false;
                                guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                                guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                                guiController.personName.GetComponent<Image>().enabled = false;
                                guiController.personNameText.GetComponent<Text>().text = "";
                                for (int i = 0; i < hitItem.collider.GetComponentInParent<Loot>().loot.Length; i++)
                                {
                                    if (hitItem.collider.GetComponentInParent<Loot>().loot[i] != null)
                                    {
                                        GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().item = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Item>().item;
                                        GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<Image>().sprite = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Image>().sprite;
                                        GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().amountOfItems = hitItem.collider.GetComponentInParent<Loot>().amountOfItems[i];
                                        GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color = new Color(GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.r, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.g, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.b, 1);
                                    }
                                }
                            }
                }
                if (hitItem.collider.transform.root.tag == "Crate")
                {
                    if (!hitItem.collider.GetComponentInParent<LootCrate>().isClose)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot = hitItem.collider.transform.root.gameObject;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfGold > 0)
                        {
                            gold += GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfGold;
                            goldWindow.GetComponentInChildren<Text>().text = "Gold:+" + GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfGold;
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfGold = 0;
                            goldText.text = "Gold:" + gold;
                            goldWindow.SetActive(true);
                            StartCoroutine("CloseGoldWindow");
                        }
                        lootWindow.SetActive(true);
                        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                        GameObject.Find("Main Camera").GetComponent<CameraMovement>().enabled = false;
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                        guiController.personName.GetComponent<Image>().enabled = false;
                        guiController.personNameText.GetComponent<Text>().text = "";
                    guiController.control.SetActive(false);
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                            GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
                        else
                            GameObject.Find("Player").GetComponent<Animator>().Play("CrouchingIdle");
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouchRunning", false);
               selfAudio.clip = null;
                        for (int i = 0; i < hitItem.collider.GetComponentInParent<LootCrate>().loot.Length; i++)
                        {
                            if (hitItem.collider.GetComponentInParent<LootCrate>().loot[i] != null)
                            {
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().item = hitItem.collider.GetComponentInParent<LootCrate>().loot[i].GetComponent<Item>().item;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<Image>().sprite = hitItem.collider.GetComponentInParent<LootCrate>().loot[i].GetComponent<Image>().sprite;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().amountOfItems = hitItem.collider.GetComponentInParent<LootCrate>().amountOfItems[i];
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color = new Color(GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.r, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.g, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.b, 1);
                            }
                        }
                    }
                }
                if (hitItem.collider.GetComponentInParent<AnimalAI>()!=null)
                {
                    if (hitItem.collider.GetComponentInParent<AnimalAI>().currentHP <= 0)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot = hitItem.collider.transform.root.gameObject;
                        lootWindow.SetActive(true);
                        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                        GameObject.Find("Main Camera").GetComponent<CameraMovement>().enabled = false;
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                        guiController.personName.GetComponent<Image>().enabled = false;
                    guiController.control.SetActive(false);
                    guiController.personNameText.GetComponent<Text>().text = "";
                        if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                            GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
                        else
                            GameObject.Find("Player").GetComponent<Animator>().Play("CrouchingIdle");
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
                        GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouchRunning", false);
                    selfAudio.clip = null;
                        for (int i = 0; i < hitItem.collider.GetComponentInParent<Loot>().loot.Length; i++)
                        {
                            if (hitItem.collider.GetComponentInParent<Loot>().loot[i] != null)
                            {
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().item = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Item>().item;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<Image>().sprite = hitItem.collider.GetComponentInParent<Loot>().loot[i].GetComponent<Image>().sprite;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].GetComponent<SlotInfo>().amountOfItems = hitItem.collider.GetComponentInParent<Loot>().amountOfItems[i];
                                GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color = new Color(GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.r, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.g, GameObject.Find("GUIManager").GetComponent<Inventory>().lootSlots[i].color.b, 1);
                            }
                        }
                    }
                }
            }
    }
    //Player's movement and X rotation code 
    private void Movement()
    {

        float rotationY = lookAxis.x * mouseSensitivity*Time.deltaTime + transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);
        playerMovement = new Vector3(runAxis.x, 0, runAxis.y);
        Vector3 movementNormalized = playerMovement.normalized;
        transform.Translate(movementNormalized * speed * Time.deltaTime);
    }
    //Start ray to change the color of pointer where is near of interactive object or person
    private void InfoRay()
    {
        if (dialogueManager.GetComponent<DialogueManager>().dialogueIsOpen == false)
        {
                Ray infoRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
                LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
                RaycastHit infoHit;
                if (Physics.Raycast(infoRay, out infoHit, Mathf.Infinity, layer, QueryTriggerInteraction.Ignore))
                {
                    //Change color to yellow if we can talk with object
                    float lengthInfo = new Vector3(infoHit.transform.position.x - main.transform.position.x, infoHit.transform.position.y - main.transform.position.y, infoHit.transform.position.z - main.transform.position.z).magnitude;
                    if (infoHit.collider.transform.root.tag == "SimplePeople" && lengthInfo <= 3f)
                    {
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = true;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP + "/" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().hpCivilian;
                        guiController.personName.GetComponent<Image>().enabled = true;
                       guiController.personName.SetActive(true);
                        if (infoHit.collider.GetComponentInParent<Dialogue>() == null)
                            guiController.personNameText.GetComponent<Text>().text = "Citizen";
                        else
                            guiController.personNameText.GetComponent<Text>().text = infoHit.collider.GetComponentInParent<Dialogue>().personName;
                    actionButton.SetActive(true);
                        pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                        itemInfo.gameObject.SetActive(false);
                    }
                    //Change color to white and close info windows if we pointer is on simple object
                    else if (infoHit.collider.transform.root.tag != "SimplePeople" && infoHit.collider.transform.root.tag != "Civilian" && infoHit.collider.transform.root.tag != "VillageGuard" && infoHit.collider.tag != "Item" && infoHit.collider.tag != "Crate" && infoHit.collider.transform.root.tag != "Bandit")
                    {
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                        guiController.personName.GetComponent<Image>().enabled = false;
                        guiController.personNameText.GetComponent<Text>().text = "";
                    actionButton.SetActive(false);
                    guiController.personName.SetActive(false);
                    locked.SetActive(false);
                        pointer.color = new Color(1, 1, 1, 0.5f);
                        pointer.color = new Color(1, 1, 1, 0.5f);
                        itemInfo.gameObject.SetActive(false);
                    }
                    //Change color to yellow if we see citizen
                    if (infoHit.collider.transform.root.tag == "Civilian" && lengthInfo <= 3f)
                    {
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = true;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP + "/" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().hpCivilian;
                        guiController.personName.GetComponent<Image>().enabled = true;
                        guiController.personNameText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<Dialogue>().personName;
                        pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                    actionButton.SetActive(true);
                    guiController.personName.SetActive(true);
                    itemInfo.gameObject.SetActive(false);
                    }
                    if (infoHit.collider.transform.root.tag == "Neutral" && lengthInfo <= 3f)
                    {
                        guiController.personName.GetComponent<Image>().enabled = true;
                        guiController.personNameText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<Dialogue>().personName;
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                        pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                    actionButton.SetActive(true);
                    itemInfo.gameObject.SetActive(false);
                    }
                //Change color to yellow if we see citizen
                if (infoHit.collider.transform.root.tag == "Book" && lengthInfo <= 3f)
                    {
                        guiController.personName.GetComponent<Image>().enabled = true;
                        guiController.personNameText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<Dialogue>().personName;
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                    guiController.personName.SetActive(true);
                    pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                    actionButton.SetActive(true);
                    itemInfo.gameObject.SetActive(false);
                    }

                    //Change color to yellow if we see guard
                    if ((infoHit.collider.transform.root.tag == "VillageGuard" || infoHit.collider.transform.root.tag == "Bandit"|| infoHit.collider.transform.root.tag == "Republican"|| infoHit.collider.transform.root.tag == "Royalist" || infoHit.collider.transform.root.tag == "Undead") && lengthInfo <= 3f)
                    {
                    guiController.personName.SetActive(true);
                    actionButton.SetActive(true);
                    guiController.enemyHPPlayer.GetComponent<Image>().enabled = true;
                        if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                            guiController.enemyHPPlayerText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP + "/" + infoHit.collider.gameObject.GetComponentInParent<GuardAI>().guardHP;
                        else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                            guiController.enemyHPPlayerText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP + "/" + infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().guardHP;
                        guiController.personName.GetComponent<Image>().enabled = true;
                        if (infoHit.collider.GetComponentInParent<GuardAI>() != null && infoHit.collider.transform.root.tag == "VillageGuard")
                            guiController.personNameText.GetComponent<Text>().text = "Guard";
                        else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null && infoHit.collider.transform.root.tag == "VillageGuard")
                            guiController.personNameText.GetComponent<Text>().text = "Summoned guard";
                        if (infoHit.collider.GetComponentInParent<GuardAI>() != null && infoHit.collider.transform.root.tag == "Bandit")
                            guiController.personNameText.GetComponent<Text>().text = "Bandit";
                        else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null && infoHit.collider.transform.root.tag == "Bandit")
                            guiController.personNameText.GetComponent<Text>().text = "Summoned bandit";
                    if (infoHit.collider.GetComponentInParent<GuardAI>() != null && infoHit.collider.transform.root.tag == "Republican")
                        guiController.personNameText.GetComponent<Text>().text = "Republican";
                    else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null && infoHit.collider.transform.root.tag == "Republican")
                        guiController.personNameText.GetComponent<Text>().text = "Summoned republican";
                    if (infoHit.collider.GetComponentInParent<GuardAI>() != null && infoHit.collider.transform.root.tag == "Royalist")
                            guiController.personNameText.GetComponent<Text>().text = "Royalist";
                        else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null && infoHit.collider.transform.root.tag == "Royalist")
                            guiController.personNameText.GetComponent<Text>().text = "Summoned royalist";
                    if (infoHit.collider.GetComponentInParent<GuardAI>() != null && infoHit.collider.transform.root.tag == "Undead")
                        guiController.personNameText.GetComponent<Text>().text = "Undead";
                    else if (infoHit.collider.GetComponentInParent<SummonedAI>() != null && infoHit.collider.transform.root.tag == "Undead")
                        guiController.personNameText.GetComponent<Text>().text = "Summoned undead";
                    if (infoHit.collider.GetComponentInParent<Dialogue>() != null)
                            guiController.personNameText.GetComponent<Text>().text = infoHit.collider.GetComponentInParent<Dialogue>().personName;
                    if (infoHit.collider.transform.root.name == "Hunter")
                        guiController.personNameText.GetComponent<Text>().text = "Hunter";
                  pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                    itemInfo.gameObject.SetActive(false);
                    }
                    if (infoHit.collider.GetComponentInParent<AnimalAI>() != null && lengthInfo <= 3f)
                    {
                    guiController.personName.SetActive(true);
                    guiController.enemyHPPlayer.GetComponent<Image>().enabled = true;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = infoHit.collider.GetComponentInParent<AnimalAI>().currentHP + "/" + infoHit.collider.GetComponentInParent<AnimalAI>().hpCivilian;
                        guiController.personName.GetComponent<Image>().enabled = true;
                        if (infoHit.collider.transform.root.name[0] == 'R' && infoHit.collider.transform.root.name[1] == 'a')
                            guiController.personNameText.GetComponent<Text>().text = "Rabbit";
                        pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                        itemInfo.gameObject.SetActive(false);
                    }
                    //Change color to yellow if we see summoned
                    if (infoHit.collider.transform.root.tag == "Summoned" && lengthInfo <= 3f)
                    {
                    guiController.personName.SetActive(true);
                    guiController.enemyHPPlayer.GetComponent<Image>().enabled = true;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP + "/" + infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().guardHP;
                        guiController.personName.GetComponent<Image>().enabled = true;
                        guiController.personNameText.GetComponent<Text>().text = "Summoned";
                        pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                        itemInfo.gameObject.SetActive(false);
                    }
                    //Info about item on which player is looking
                    if (infoHit.collider.tag == "Item" && lengthInfo <= 3f)
                    {
                        pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                    actionButton.SetActive(true);
                    itemInfo.gameObject.SetActive(true);
                    if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Spell")
                    {
                        actionButton.SetActive(true);
                        if (infoHit.collider.GetComponent<Item>().itemName == "Fireball")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "Damage:" + (infoHit.collider.GetComponent<Item>().itemDamage + infoHit.collider.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().battleSpellsModifyStat / 100 + infoHit.collider.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify / 100);
                        if (infoHit.collider.GetComponent<Item>().itemName == "Storm spell")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "Damage:" + (infoHit.collider.GetComponent<Item>().itemDamage + infoHit.collider.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().battleSpellsModifyStat / 100 + infoHit.collider.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify / 100);
                        else if (infoHit.collider.GetComponent<Item>().itemName == "Health recover" || infoHit.collider.GetComponent<Item>().itemName == "Stamina recover")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "Recover:" + (infoHit.collider.GetComponent<Item>().itemDamage + infoHit.collider.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().recoverSpellsModifyStat / 100 + infoHit.collider.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().recoverModify / 100);
                    }
                    if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Weapon")
                        {
                        actionButton.SetActive(true);
                        if (infoHit.collider.gameObject.GetComponent<Item>().weaponType == "OneHand")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Damage:" + (infoHit.collider.gameObject.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100);
                            if (infoHit.collider.gameObject.GetComponent<Item>().weaponType == "TwoHand")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Damage:" + (infoHit.collider.gameObject.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100);
                            if (infoHit.collider.gameObject.GetComponent<Item>().weaponType == "Bow")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Damage:" + (infoHit.collider.gameObject.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100);
                        }
                        if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Book")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "";
                        if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Helmet" || infoHit.collider.GetComponent<Item>().itemInventoryTag == "Shield")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "Armor:" + (infoHit.collider.gameObject.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100);
                        if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Armor")
                        {
                            if (infoHit.collider.GetComponent<Item>().weaponType == "LightArmor")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Armor:" + (infoHit.collider.gameObject.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100);
                            if (infoHit.collider.GetComponent<Item>().weaponType == "HardArmor")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Armor:" + (infoHit.collider.gameObject.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100);
                        }
                        GameObject.Find("ItemCost").GetComponent<Text>().text = "Cost:" + infoHit.collider.gameObject.GetComponent<Item>().itemCost;
                        GameObject.Find("ItemName").GetComponent<Text>().text = infoHit.collider.gameObject.GetComponent<Item>().itemName;
                        if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Potion" || infoHit.collider.GetComponent<Item>().itemInventoryTag == "Food")
                        {
                            if (infoHit.collider.GetComponent<Item>().weaponType == "Health")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Recover:" + (infoHit.collider.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100).ToString() + " HP";
                            if (infoHit.collider.GetComponent<Item>().weaponType == "Mana")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Recover:" + (infoHit.collider.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100).ToString() + " MP";
                            if (infoHit.collider.GetComponent<Item>().weaponType == "Stamina")
                                GameObject.Find("ItemDamage").GetComponent<Text>().text = "Recover:" + (infoHit.collider.GetComponent<Item>().itemDamage + infoHit.collider.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100).ToString() + " SP";
                        }
                    if (infoHit.collider.GetComponent<Item>().itemName == "Berserk potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Damage:+10%";
                    if (infoHit.collider.GetComponent<Item>().itemName == "Paladin potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Armor:+10%";
                    if (infoHit.collider.GetComponent<Item>().itemName == "Robber potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Pickpocketing:+10";
                    if (infoHit.collider.GetComponent<Item>().itemName == "Breaker potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Breaking:+10";
                    if (infoHit.collider.GetComponent<Item>().itemName == "Warrior potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Max health:+50";
                    if (infoHit.collider.GetComponent<Item>().itemName == "Archimage potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Max mana:+50";
                    if (infoHit.collider.GetComponent<Item>().itemName == "Runner potion")
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "Max stamina:+50";
                    guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Ammo")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "";
                        if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Ingridient")
                            GameObject.Find("ItemDamage").GetComponent<Text>().text = "";
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                        guiController.personName.GetComponent<Image>().enabled = false;
                        guiController.personNameText.GetComponent<Text>().text = "";
                    if (infoHit.collider.GetComponent<Item>().itemInventoryTag == "Quest")
                    {
                        GameObject.Find("ItemDamage").GetComponent<Text>().text = "";
                        GameObject.Find("ItemCost").GetComponent<Text>().text = "";
                    }
                }
                    if (infoHit.collider.transform.root.tag == "Crate" && lengthInfo <= 3f)
                    {
                    actionButton.SetActive(true);
                    pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                        if (infoHit.collider.GetComponentInParent<LootCrate>().isClose)
                            locked.SetActive(true);
                        guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                        guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                    guiController.personName.SetActive(true);
                        guiController.personName.GetComponent<Image>().enabled = true;
                        guiController.personNameText.GetComponent<Text>().text = "Crate";
                        itemInfo.gameObject.SetActive(false);
                    }
                    else
                        locked.SetActive(false);
                if (infoHit.collider.tag == "Alchemy" && lengthInfo <= 3f)
                {
                    actionButton.SetActive(true);
                    guiController.personName.GetComponent<Image>().enabled = true;
                    guiController.personNameText.GetComponent<Text>().text = "Cauldron";
                    guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                    guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                    guiController.personName.SetActive(true);
                    pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                    itemInfo.gameObject.SetActive(false);
                }
            }
                else
                {
                actionButton.SetActive(false);
                pointer.color = new Color(1, 1, 1, 0.5f);
                    itemInfo.gameObject.SetActive(false);
                    locked.SetActive(false);
                    itemInfo.gameObject.SetActive(false);
                    guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                    guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                    guiController.personName.GetComponent<Image>().enabled = false;
                guiController.personName.SetActive(false);
                    guiController.personNameText.GetComponent<Text>().text = "";
                }
        }
    }
    private void InfoRayForTriggers()
    {
        Ray infoRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
        RaycastHit infoHit;
        if (Physics.Raycast(infoRay, out infoHit, Mathf.Infinity, layer))
        {
            float lengthInfo = new Vector3(infoHit.transform.position.x - main.transform.position.x, infoHit.transform.position.y - main.transform.position.y, infoHit.transform.position.z - main.transform.position.z).magnitude;
            if (infoHit.collider.tag == "Fire" && lengthInfo <= 3f)
            {
                actionButton.SetActive(true);
                guiController.personName.GetComponent<Image>().enabled = true;
                guiController.personName.SetActive(true);
                guiController.personNameText.GetComponent<Text>().text = "Fire";
                guiController.enemyHPPlayer.GetComponent<Image>().enabled = false;
                guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                pointer.color = new Color(1, 0.92f, 0.016f, 0.5f);
                itemInfo.gameObject.SetActive(false);
            }
        }
    }
    public void SimpleAttack()
    {
        //Animation for player's simple attack
        if (!animator.GetBool("IsRunning") && !dialogueIsActive && GetComponentInChildren<HandWeaponScript>().melee)
        {
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsHugeAttacking", false);
            animator.SetBool("IsBlocking", false);
            animator.SetBool("IsDrawingArrow", false);
            if (!animator.GetBool("TwoHand"))
                animator.Play("Attack");
            if (animator.GetBool("TwoHand"))
                animator.Play("TwoHand Attack");
            block = false;
        }
        //Animation for Bow simple attack if player has arrow
        if (!GetComponentInChildren<HandWeaponScript>().melee && inventory.hasArrows)
        {
            animator.SetBool("IsDrawingArrow", true);
            animator.Play("BowAttack");
            GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = arrow.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Find("PlayerWeapon").GetComponent<MeshRenderer>().material = arrow.GetComponent<MeshRenderer>().sharedMaterial;
            GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(-190, 90, 90);
            GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
    public void HugeAttack()
    {
        //If player has more than 25 stamina and  Wheel Button pressed play animation for huge attack
        if (!animator.GetBool("IsRunning") && currentStamina >= 25 && !dialogueIsActive && GetComponentInChildren<HandWeaponScript>().melee && GetComponentInChildren<HandWeaponScript>().spell == false)
        {
            if (animator.GetBool("IsHugeAttacking") == false)
                currentStamina -= 25;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsHugeAttacking", true);
            animator.SetBool("IsBlocking", false);
            animator.SetBool("IsDrawingArrow", false);
            if (!animator.GetBool("TwoHand"))
                animator.Play("HugeAttack");
            if (animator.GetBool("TwoHand"))
                animator.Play("TwoHand HugeAttack");
            block = false;
        }
    }
    public void Block()
    {
        //if RMB pressed and stamina more than 10 play block animation
        if (!block)
        {
            if (!animator.GetBool("IsRunning") && currentStamina >= 10 && !dialogueIsActive && !isCrouched)
            {
                if (inventory.shieldImage.GetComponent<SlotInfo>().item != null)
                {
                    if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Militia shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчения" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчення")
                        GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-146, 120, -120);
                    if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Cheap shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешёвый щит" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешевий щит")
                        GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-35, 213, 112);
                }
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsHugeAttacking", false);
                animator.SetBool("IsBlocking", true);
                animator.SetBool("IsDrawingArrow", false);
                if (GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh.name == "Arrow_Regular Instance")
                    GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
                animator.Play("Block");
                block = true;
            }
        }
      else  if (block)
        {
                //Change shield rotation
                if (inventory.shieldImage.GetComponent<SlotInfo>().item != null)
                {
                    if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Militia shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчения" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчення")
                        GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-20, -180, 160);
                    if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Cheap shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешёвый щит" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешевий щит")
                        GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-48, -294, 120);
                }
                block = false;
                animator.SetBool("IsBlocking", false);
        }
    }
    public void Stealth()
    {
        //Set that player is crouching
        if (isCrouched == false && GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
        {
            GameObject.Find("Music").transform.position -= GameObject.Find("Music").transform.up * 0.5f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("RayStart").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("RayStart")[i].transform.position -= GameObject.FindGameObjectsWithTag("RayStart")[i].transform.up * 0.5f;
            }
            stealthBar.SetActive(true);
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, 0.94f, GetComponent<BoxCollider>().size.z);
            GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, 0.48f, GetComponent<BoxCollider>().center.z);
            isCrouched = true;
            animator.SetBool("IsCrouching", true);
            animator.Play("CrouchingIdle");
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsHugeAttacking", false);
            animator.SetBool("IsBlocking", false);
            animator.SetBool("IsDrawingArrow", false);
            speed = stableCrouchingSpeed;
            GameObject.Find("Main Camera").transform.localPosition = new Vector3(-0.01f, 0.876f, 0.663f);
        }
        //Set that player isn't crouching
        else if (isCrouched == true)
        {
            GameObject.Find("Music").transform.position += GameObject.Find("Music").transform.up * 0.5f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("RayStart").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("RayStart")[i].transform.position += GameObject.FindGameObjectsWithTag("RayStart")[i].transform.up * 0.5f;
            }
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, 1.76f, GetComponent<BoxCollider>().size.z);
            GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, 0.95f, GetComponent<BoxCollider>().center.z);
            stealthBar.SetActive(false);
            isCrouched = false;
            animator.SetBool("IsCrouching", false);
            animator.SetBool("IsCrouchRunning", false);
            animator.Play("Idle");
            if (inventory.armorImage.GetComponent<SlotInfo>().item == null)
                speed = stableLightSpeed;
            if (inventory.armorImage.GetComponent<SlotInfo>().item != null)
                if (inventory.armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                    speed = stableLightSpeed;
            if (inventory.armorImage.GetComponent<SlotInfo>().item != null)
                if (inventory.armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                    speed = stableHardSpeed;
            GameObject.Find("Main Camera").transform.localPosition = new Vector3(GameObject.Find("Main Camera").transform.localPosition.x, 1.56f, GameObject.Find("Main Camera").transform.localPosition.z);
        }
    }
    //Player's animator logic
    private void AnimatorPlayer()
    {
        //If player released RMB cancen block animation
        if (currentStamina < 10)
        {
            //Change shield rotation
            if (inventory.shieldImage.GetComponent<SlotInfo>().item != null)
            {
                if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Militia shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчения" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчення")
                    GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-20, -180, 160);
                if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Cheap shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешёвый щит" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешевий щит")
                    GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-48, -294, 120);
            }
            block = false;
            animator.SetBool("IsBlocking", false);
        }
        //Animation for player's moving
        if ((runAxis.x!=0||runAxis.y!=0) && !dialogueIsActive && !isCrouched)
        {
            animator.SetBool("IsRunning", true);
            if (!animator.GetBool("TwoHand"))
                animator.Play("Run");
            if (animator.GetBool("TwoHand"))
                animator.Play("TwoHand Run");
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsHugeAttacking", false);
            animator.SetBool("IsBlocking", false);
            animator.SetBool("IsDrawingArrow", false);
            block = false;
            if (GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh.name == "Arrow_Regular Instance")
                GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
            if (selfAudio.clip == null)
            {
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                selfAudio.loop = true;
                selfAudio.Play();
            }
            else if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().crouchingSound)
            {
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound;
                selfAudio.loop = true;
                selfAudio.Play();
            }
        }
        //Idle animation when user doing nothing
        if (runAxis.x==0&&runAxis.y==0&& !animator.GetBool("IsAttacking") && !animator.GetBool("IsHugeAttacking") && !block && !animator.GetBool("IsDrawingArrow") && !isCrouched)
        {
            if (!animator.GetBool("TwoHand"))
                animator.Play("Idle");
            if (animator.GetBool("TwoHand"))
                animator.Play("TwoHand Idle");
            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().archerySound)
                selfAudio.clip = null;
            selfAudio.loop = false;
        }
        //Set animation bool is running to false if player is doing NOTHING
        if (runAxis.x==0&&runAxis.y==0)
        {
            animator.SetBool("IsRunning", false);
        }
        //Crouching Idle
        if (runAxis.x==0&&runAxis.y==0 && !animator.GetBool("IsAttacking") && !animator.GetBool("IsHugeAttacking") && !block && !animator.GetBool("IsDrawingArrow") && isCrouched)
        {
            animator.Play("CrouchingIdle");
            animator.SetBool("IsCrouchRunning", false);
            if (selfAudio.clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().archerySound)
                selfAudio.clip = null;
            selfAudio.loop = false;
        }
        //Crouching walking
        if ((runAxis.x!=0||runAxis.y!=0) && !dialogueIsActive && isCrouched)
        {
            animator.SetBool("IsCrouchRunning", true);
            animator.Play("CrouchingRun");
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsHugeAttacking", false);
            animator.SetBool("IsBlocking", false);
            animator.SetBool("IsDrawingArrow", false);
            block = false;
            if (GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh.name == "Arrow_Regular Instance")
                GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
            if (selfAudio.clip == null)
            {
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().crouchingSound;
                selfAudio.loop = true;
                selfAudio.Play();
            }
            else if (selfAudio.clip == GameObject.Find("AudioManager").GetComponent<AudioManager>().runningSound)
            {
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().crouchingSound;
                selfAudio.loop = true;
                selfAudio.Play();
            }
        }
    }
    //Damage function for animation event (draw arrow readeble)
    public void BowSimpleDamage()
    {
        Ray aimRay = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
        RaycastHit aimHit;
        if (Physics.Raycast(aimRay, out aimHit, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
        {
            //-1 Arrow after shot
            for (int i = 0, min = 100, index = 0, amountOfStacks = 0; i < 60; i++)
            {
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.name == "Arrow" || GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.name == "Стріла" || GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.name == "Стрела")
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems < min)
                        {
                            min = GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems;
                            index = i;

                        }
                        amountOfStacks++;
                    }
                if (i == 59 && GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<SlotInfo>().amountOfItems > 1)
                {
                    GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<SlotInfo>().amountOfItems--;
                }
                else if (i == 59 && GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<SlotInfo>().amountOfItems == 1)
                {
                    if (amountOfStacks == 1)
                        inventory.hasArrows = false;
                    GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<Image>().sprite = null;
                    GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<SlotInfo>().amountOfItems = 0;
                    GameObject.Find("GUIManager").GetComponent<Inventory>().images[index].GetComponent<SlotInfo>().item = null;
                }
            }
            //Spawn Arrow
            GameObject spawnArrow = Instantiate(arrow, GameObject.Find("BowWeapon").transform.position, Quaternion.Euler(GameObject.Find("Main Camera").transform.localRotation.x, GameObject.Find("Player").transform.localRotation.y, 0));
            spawnArrow.GetComponent<Rigidbody>().velocity = (aimHit.point - spawnArrow.transform.position).normalized * 30;
            spawnArrow.GetComponent<Item>().item = arrow;
            spawnArrow.GetComponent<Arrow>().shooter = gameObject;
            spawnArrow.GetComponent<Arrow>().arrowDamage = GetComponentInChildren<HandWeaponScript>().weaponDamage;
            spawnArrow.layer = 11;
            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().archerySound;
            selfAudio.Play();
            GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
            animator.SetBool("IsDrawingArrow", false);
        }
    }
    //Damage function for animation event (attack1readeble)
    public void Damage()
    {
        if (!GetComponentInChildren<HandWeaponScript>().spell) {
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
            Ray infoRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit infoHit;
            if (Physics.Raycast(infoRay, out infoHit, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
            {
                //Code for civilians
                if ((infoHit.collider.transform.root.tag == "Civilian" || infoHit.collider.transform.root.tag == "SimplePeople"||infoHit.collider.transform.root.name=="Solovey") && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
                {
                    if (infoHit.collider.transform.root.GetComponent<CivilianAI>().currentHP > 0)
                    {
                        //Play sound for fists if player doesn't have equiped weapon
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                        {
                            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            }
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                            }
                            selfAudio.Play();
                        }
                        //Play sound for weapon if player equiped weapon
                        else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHit;
                            selfAudio.Play();
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                            infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage;
                        else if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                            infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * stealthAttackModify;
                        if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0&&infoHit.transform.root.name!="Solovey")
                        {
                            infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().detection = 100;
                            isDetected = true;
                            infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                            infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().attacker = gameObject;
                            infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0)
                        {
                            killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                            StartCoroutine("KillExperienceShow");
                            experience += infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                        }
                            if (infoHit.collider.transform.root.name!="Solovey")
                        GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                        //Code for calling another people
                        CallNear();
                    }
                }
                //Code for guard
                if ((infoHit.collider.transform.root.tag == "VillageGuard"|| infoHit.collider.transform.root.tag == "Bandit" || infoHit.collider.transform.root.tag == "Undead" || infoHit.collider.transform.root.tag == "Royalist" || infoHit.collider.transform.root.tag == "Republican") && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
                {
                    if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                        if (infoHit.collider.transform.root.GetComponent<GuardAI>().currentHP > 0)
                        {
                            //Play sound for fists if player doesn't have equiped weapon
                            if (!infoHit.collider.gameObject.GetComponentInParent<GuardAI>().block && GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                            {
                                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                }
                                else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                                {
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                                }
                                selfAudio.Play();
                            }
                            //Play sound for weapon if player equiped weapon
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null && !infoHit.collider.gameObject.GetComponentInParent<GuardAI>().block)
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHit;
                                selfAudio.Play();
                            }
                            //If guard is blocking play block sound
                            else if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().block)
                            {
                                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                                {
                                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                    else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                }
                                else
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                                selfAudio.Play();
                            }
                            //If guard isn't blocking play simple script
                            if (infoHit.collider.transform.root.GetComponentInParent<GuardAI>().block == false)
                            {
                                if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                {
                                    if (GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage > 0)
                                        infoHit.collider.GetComponentInParent<GuardAI>().currentHP -= (int)(GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage);
                                    if (!infoHit.collider.gameObject.GetComponentInParent<GuardAI>().isArcher && !infoHit.collider.gameObject.GetComponentInParent<GuardAI>().isMage)
                                    {
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsHugeAttacking", false);
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                                    }
                                    infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsAttacking", false);
                                }
                                else if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                {
                                    if (GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage > 0)
                                        infoHit.collider.GetComponentInParent<GuardAI>().currentHP -= (int)(GameObject.Find("PlayerWeapon").GetComponentInParent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage) * stealthAttackModify;
                                    if (!infoHit.collider.gameObject.GetComponentInParent<GuardAI>().isArcher && !infoHit.collider.gameObject.GetComponentInParent<GuardAI>().isMage)
                                    {
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsHugeAttacking", false);
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                                    }
                                    infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsAttacking", false);
                                }
                                if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                                {
                                    infoHit.collider.gameObject.GetComponentInParent<GuardAI>().detection = 100;
                                    isDetected = true;
                                    infoHit.collider.gameObject.GetComponentInParent<GuardAI>().objectToAttack = gameObject;
                                    infoHit.collider.gameObject.GetComponentInParent<GuardAI>().isAlerted = true;
                                    infoHit.collider.gameObject.GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                }
                                if (infoHit.collider.transform.root.name != "Solovey")
                                {
                                    if (infoHit.collider.transform.root.tag == "VillageGuard")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                    if(infoHit.collider.transform.root.name!= "PatrolRoyalist2"&& infoHit.collider.transform.root.name != "PatrolRoyalist1"&& infoHit.collider.transform.root.name != "StrangeRoyalist")
                                    if (infoHit.collider.transform.root.tag == "Royalist")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                    if (infoHit.collider.transform.root.tag == "Republican")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                    if(infoHit.collider.transform.root.name == "StrangeRoyalist")
                                    {
                                        if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest")!=null)
                                            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage<2)
                                                GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                        if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                            GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                    }
                                }
                            }
                            if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                            {
                                killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<GuardAI>().experience;
                                StartCoroutine("KillExperienceShow");
                                experience += infoHit.collider.gameObject.GetComponentInParent<GuardAI>().experience;
                            }
                            //Code for calling another people
                            if (infoHit.collider.transform.root.name != "PatrolRoyalist2" && infoHit.collider.transform.root.name != "PatrolRoyalist1" && infoHit.collider.transform.root.name != "StrangeRoyalist")
                                CallNear();
                            if (infoHit.collider.transform.root.name == "StrangeRoyalist")
                            {
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                        CallNear();
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                    CallNear();
                            }
                        }
                    if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                        if (infoHit.collider.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                        {
                            //Play sound for fists if player doesn't have equiped weapon
                            if (!infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block && GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                            {
                                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                                {
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                                }
                                else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                                {
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                                }
                                selfAudio.Play();
                            }
                            //Play sound for weapon if player equiped weapon
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null && !infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block)
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHit;
                                selfAudio.Play();
                            }
                            //If guard is blocking play block sound
                            else if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().block)
                            {
                                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                                {
                                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                    else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                                }
                                else
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                                selfAudio.Play();
                            }
                            //If guard isn't blocking play simple script
                            if (infoHit.collider.transform.root.GetComponentInParent<SummonedAI>().block == false)
                            {
                                if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                {
                                    infoHit.collider.GetComponentInParent<SummonedAI>().currentHP -= GetComponentInChildren<HandWeaponScript>().weaponDamage;
                                    if (!infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().isArcher)
                                    {
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsHugeAttacking", false);
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                                    }
                                    infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsAttacking", false);
                                }
                                else if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                {
                                    infoHit.collider.GetComponentInParent<SummonedAI>().currentHP -= GetComponentInChildren<HandWeaponScript>().weaponDamage * stealthAttackModify;
                                    if (!infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().isArcher)
                                    {
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsHugeAttacking", false);
                                        infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                                    }
                                    infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsAttacking", false);
                                }
                                if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                                {
                                    isDetected = true;
                                    infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().objectToAttack = gameObject;
                                }
                                if (infoHit.collider.transform.root.name != "Solovey")
                                {
                                    if (infoHit.collider.transform.root.tag == "VillageGuard")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                    if (infoHit.collider.transform.root.tag == "Republican")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                    if (infoHit.collider.transform.root.tag == "Royalist")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                }
                            }
                            //Code for calling another people
                            CallNear();
                        }
                }
                //Code for summoned
                if (infoHit.collider.transform.root.tag == "Summoned" && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
                {
                    if (infoHit.collider.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                    {
                        //Play sound for fists if player doesn't have equiped weapon
                        if (!infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block && GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                        {
                            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                            }
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                            }
                            selfAudio.Play();
                        }
                        //Play sound for weapon if player equiped weapon
                        else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null && !infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block)
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHit;
                            selfAudio.Play();
                        }
                        //If guard is blocking play block sound
                        else if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block)
                        {
                            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                            {
                                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                            }
                            else
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().blockSound;
                            selfAudio.Play();
                        }
                        //If guard isn't blocking play simple script
                        if (infoHit.collider.transform.root.GetComponentInParent<SummonedAI>().block == false)
                        {
                            if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                            {
                                infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage;
                                if (!infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().isArcher)
                                {
                                    infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsHugeAttacking", false);
                                    infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                                }
                                infoHit.collider.gameObject.GetComponentInParent<Animator>().SetBool("IsAttacking", false);
                            }
                        }
                    }
                }
                if (infoHit.collider.GetComponentInParent<AnimalAI>()!=null && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
                {
                    if (infoHit.collider.transform.root.GetComponent<AnimalAI>().currentHP > 0)
                    {
                        //Play sound for fists if player doesn't have equiped weapon
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                        {
                            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().attackSound;
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            }
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            {
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHit;
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                            }
                            selfAudio.Play();
                        }
                        //Play sound for weapon if player equiped weapon
                        else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHit;
                            selfAudio.Play();
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                            infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage;
                        else if (infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                            infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * stealthAttackModify;
                        if (infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP <= 0)
                        {
                            killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().experience;
                            StartCoroutine("KillExperienceShow");
                            experience += infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().experience;
                        }
                    }
                }
                if ((infoHit.collider.GetComponentInParent<SummonedAI>() == null && infoHit.collider.GetComponentInParent<GuardAI>() == null && infoHit.collider.GetComponentInParent<CivilianAI>() == null) && (infoHit.point - transform.position).magnitude <= 2.5&&GetComponentInChildren<HandWeaponScript>().weapon!=null)
                {
                    selfAudio.loop = false;
                    selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().missSound;
                    selfAudio.Play();
                }
            }
        }
    }

    //Spawn spell on animation attack01Readeble event
    public void SpawnSpell()
    {
        if (GetComponentInChildren<HandWeaponScript>().weapon != null)
            if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemInventoryTag == "Spell")
            {
                //Fireball code
                if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemName == "Fireball")
                {
                    if (currentMana >= 15)
                    {
                        currentMana -= 15;
                        GameObject spell = Instantiate(fireball, GameObject.Find("Main Camera").transform.position - GameObject.Find("Main Camera").transform.up * 0.2f, Quaternion.Euler(0, fireball.transform.rotation.y, 0));
                        spell.GetComponent<Fireball>().StartCoroutine("DestroyAfterTime");
                        spell.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().fireballSound;
                        spell.GetComponent<AudioSource>().Play();
                        spell.GetComponent<Fireball>().spellDamage = 30 + 30 * fireballDamageModify / 100 + 30 * GameObject.Find("SkillManager").GetComponent<SkillManager>().battleSpellsModifyStat / 100;
                        spell.GetComponent<Fireball>().shooter = gameObject;
                    }
                    else
                    {
                        StopCoroutine("EnoughMana");
                        StartCoroutine("EnoughMana");
                    }
                }
                if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemName == "Storm spell")
                {
                    if (currentMana >= 50)
                    {
                        currentMana -= 50;
                        GameObject stormSpell = Instantiate(stormPrefab, GameObject.Find("Main Camera").transform.position - GameObject.Find("Main Camera").transform.up * 0.2f, Quaternion.Euler(0, stormPrefab.transform.rotation.y, 0));
                        stormSpell.GetComponent<Fireball>().StartCoroutine("DestroyAfterTime");
                        stormSpell.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().fireballSound;
                        stormSpell.GetComponent<AudioSource>().Play();
                        stormSpell.GetComponent<Fireball>().spellDamage = 100 + 100 * fireballDamageModify / 100 + 100 * GameObject.Find("SkillManager").GetComponent<SkillManager>().battleSpellsModifyStat / 100;
                        stormSpell.GetComponent<Fireball>().shooter = gameObject;
                    }
                    else
                    {
                        StopCoroutine("EnoughMana");
                        StartCoroutine("EnoughMana");
                    }
                }
                //Health recover code
                else if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemName == "Health recover")
                {
                    if (currentMana >= 25)
                    {
                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value++;
                        currentMana -= 25;
                        currentHealth += 50 + 50 * recoverModify / 100 + 50 * GameObject.Find("SkillManager").GetComponent<SkillManager>().recoverSpellsModifyStat / 100;
                        if (currentHealth > hpPlayer)
                            currentHealth = hpPlayer;
                        GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().recoverSound;
                        GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        StopCoroutine("EnoughMana");
                        StartCoroutine("EnoughMana");
                    }
                }
                //Stamina recover code
                else if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemName == "Stamina recover")
                {
                    if (currentMana >= 25)
                    {
                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value++;
                        currentMana -= 25;
                        currentStamina += 50 + 50 * recoverModify / 100 + 50 * GameObject.Find("SkillManager").GetComponent<SkillManager>().recoverSpellsModifyStat / 100;
                        if (currentStamina > staminaPlayer)
                            currentStamina = staminaPlayer;
                        GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().recoverSound;
                        GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        StopCoroutine("EnoughMana");
                        StartCoroutine("EnoughMana");
                    }
                }
                //Summon melee spell code
                else if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemName == "Summon melee")
                {
                    if (currentMana >= 50)
                    {
                        Ray aimRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
                        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
                        RaycastHit aimHit;
                        if (Physics.Raycast(aimRay, out aimHit, Mathf.Infinity, layer))
                        {
                            NavMeshPath path = new NavMeshPath();
                            if (NavMesh.CalculatePath(transform.position, aimHit.point, NavMesh.AllAreas, path) && (aimHit.point - transform.position).magnitude <= 5)
                            {
                                if (aimHit.collider.tag != "VillageGuard" && aimHit.collider.tag != "Civilian" && aimHit.collider.tag != "SimplePeople" && aimHit.collider.tag != "Summmoned" && aimHit.collider.tag != "Player" && aimHit.collider.tag != "Bandit" && aimHit.collider.tag != "Republican" && aimHit.collider.tag != "Royalist" && aimHit.collider.tag != "Undead")
                                {
                                    if (currentSummonedMelee != null)
                                    {
                                        for (int i = 0; i < GameObject.FindObjectsOfType<FractionTrigger>().Length; i++)
                                            foreach (GameObject gameObjectList in GameObject.FindObjectsOfType<FractionTrigger>()[i].objectsInRadius)
                                                if (gameObjectList == currentSummonedMelee)
                                                {
                                                    GameObject.FindObjectsOfType<FractionTrigger>()[i].objectsInRadius.Remove(currentSummonedMelee);
                                                    break;
                                                }
                                        Destroy(currentSummonedMelee);
                                    }
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value++;
                                    currentMana -= 50;
                                    GameObject meleeSummoned = Instantiate(summonedMelee, aimHit.point, summonedMelee.transform.rotation);
                                    meleeSummoned.GetComponent<SummonedAI>().summoner = gameObject;
                                    currentSummonedMelee = meleeSummoned;
                                    meleeSummoned.GetComponent<SummonedAI>().guardHP += meleeSummoned.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + meleeSummoned.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
                                    meleeSummoned.GetComponent<SummonedAI>().damage += meleeSummoned.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + meleeSummoned.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
                                    GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().summonSound;
                                    GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().Play();
                                }
                            }
                        }
                    }
                    else
                    {
                        StopCoroutine("EnoughMana");
                        StartCoroutine("EnoughMana");
                    }
                }
                else if (GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().itemName == "Summon archer")
                {
                    if (currentMana >= 50)
                    {
                        Ray aimRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
                        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
                        RaycastHit aimHit;
                        if (Physics.Raycast(aimRay, out aimHit, Mathf.Infinity, layer))
                        {
                            NavMeshPath path = new NavMeshPath();
                            if (NavMesh.CalculatePath(transform.position, aimHit.point, NavMesh.AllAreas, path) && (aimHit.point - transform.position).magnitude <= 5)
                            {
                                if (aimHit.collider.tag != "VillageGuard" && aimHit.collider.tag != "Civilian" && aimHit.collider.tag != "SimplePeople" && aimHit.collider.tag != "Summmoned" && aimHit.collider.tag != "Player" && aimHit.collider.tag != "Bandit" && aimHit.collider.tag != "Undead" && aimHit.collider.tag != "Republican" && aimHit.collider.tag != "Royalist")
                                {
                                    if (currentSummonedArcher != null)
                                    {
                                        for (int i = 0; i < GameObject.FindObjectsOfType<FractionTrigger>().Length; i++)
                                            foreach (GameObject gameObjectList in GameObject.FindObjectsOfType<FractionTrigger>()[i].objectsInRadius)
                                                if (gameObjectList == currentSummonedArcher)
                                                {
                                                    GameObject.FindObjectsOfType<FractionTrigger>()[i].objectsInRadius.Remove(currentSummonedArcher);
                                                    break;
                                                }
                                        Destroy(currentSummonedArcher);
                                    }
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value++;
                                    currentMana -= 50;
                                    GameObject archerSummoned = Instantiate(summonedArcher, aimHit.point, summonedMelee.transform.rotation);
                                    currentSummonedArcher = archerSummoned;
                                    archerSummoned.GetComponent<SummonedAI>().summoner = gameObject;
                                    archerSummoned.GetComponent<SummonedAI>().guardHP += archerSummoned.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + archerSummoned.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
                                    archerSummoned.GetComponent<SummonedAI>().damage += archerSummoned.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + archerSummoned.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
                                    GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().summonSound;
                                    GameObject.Find("ExtraPlayerSounds").GetComponent<AudioSource>().Play();
                                }
                            }
                        }
                    }
                    else
                    {
                        StopCoroutine("EnoughMana");
                        StartCoroutine("EnoughMana");
                    }
                }
            }
    }
    //Set IsAttacking bool in animator to false to prevent bugs
    public void SetIsAttacking()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsHugeAttacking", false);
        animator.Play("Idle");
    }
    //Script for huge damage for animation event in attack2Readeble animation
    public void HugeDamage()
    {
        Ray infoRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
        RaycastHit infoHit;
        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
        if (Physics.Raycast(infoRay, out infoHit, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
        {
            //Huge damage on civilian
            if ((infoHit.collider.transform.root.tag == "Civilian" || infoHit.collider.transform.root.tag == "SimplePeople"||infoHit.collider.transform.root.name=="Solovey") && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
            {
                if (infoHit.collider.transform.root.GetComponent<CivilianAI>().currentHP > 0)
                {
                    //If player has equiped weapon play huge damage sound for weapon
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                        }
                        else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                        }
                        selfAudio.Play();
                    }
                    //If player hasn't equiped weapon play huge damage sound for fists
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                    {
                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHugeHit;
                        selfAudio.Play();
                    }
                    if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                    {
                        infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * 2;
                        if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                            infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                    }
                    else if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                    {
                        infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * 2 * stealthAttackModify;
                        if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                            infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                    }
                    infoHit.collider.gameObject.GetComponentInParent<AudioSource>().clip = null;
                    infoHit.collider.gameObject.GetComponentInParent<AudioSource>().Play();
                    if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0&& infoHit.transform.root.name != "Solovey")
                    {
                        infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().detection = 100;
                        isDetected = true;
                        infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                        infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().attacker = gameObject;
                        infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                    }
                    if (infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0)
                    {
                        killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                        StartCoroutine("KillExperienceShow");
                        experience += infoHit.collider.gameObject.GetComponentInParent<CivilianAI>().experience;
                    }
                    if (infoHit.collider.transform.root.name != "Solovey")
                        GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                    //Call near people
                    if (infoHit.collider.transform.root.name != "PatrolRoyalist2" && infoHit.collider.transform.root.name != "PatrolRoyalist1" && infoHit.collider.transform.root.name != "StrangeRoyalist")
                        CallNear();
                }
            }
            //Huge damage for guard
            if ((infoHit.collider.transform.root.tag == "VillageGuard"|| infoHit.collider.transform.root.tag == "Bandit" || infoHit.collider.transform.root.tag == "Undead" || infoHit.collider.transform.root.tag == "Republican" || infoHit.collider.transform.root.tag == "Royalist") && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
            {
                if (infoHit.collider.GetComponentInParent<GuardAI>() != null)
                    if (infoHit.collider.transform.root.GetComponent<GuardAI>().currentHP > 0)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                        {
                            //If player has equiped weapon play huge damage sound for weapon
                            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            }
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                            }
                            selfAudio.Play();
                        }
                        //If player hasn't equiped weapon play huge damage sound for fists
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHugeHit;
                            selfAudio.Play();
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                        {
                            if (GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage > 0)
                                infoHit.collider.GetComponentInParent<GuardAI>().currentHP -= (int)(GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage) * 2;
                            if (infoHit.collider.GetComponentInParent<GuardAI>().currentHP > 0)
                                infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                            infoHit.collider.gameObject.GetComponentInParent<GuardAI>().block = false;
                            if(!infoHit.collider.GetComponentInParent<GuardAI>().isArcher&& !infoHit.collider.GetComponentInParent<GuardAI>().isMage)
                            infoHit.collider.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                            infoHit.collider.GetComponentInParent<Animator>().speed = 1;
                            infoHit.collider.GetComponentInParent<GuardAI>().StopCoroutine("CancelBlock");
                            infoHit.collider.GetComponentInParent<GuardAI>().StopCoroutine("TimeForBlock");
                        }
                        else if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                        {
                            if (GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage > 0)
                                infoHit.collider.GetComponentInParent<GuardAI>().currentHP -= (int)(GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage - infoHit.collider.GetComponentInParent<GuardAI>().armor / 100f * GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage) * 2 * stealthAttackModify;
                            if (infoHit.collider.GetComponentInParent<GuardAI>().currentHP > 0)
                                infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                            infoHit.collider.gameObject.GetComponentInParent<GuardAI>().block = false;
                            infoHit.collider.GetComponentInParent<Animator>().speed = 1;
                            infoHit.collider.GetComponentInParent<GuardAI>().StopCoroutine("CancelBlock");
                            infoHit.collider.GetComponentInParent<GuardAI>().StopCoroutine("TimeForBlock");
                            if (!infoHit.collider.GetComponentInParent<GuardAI>().isArcher && !infoHit.collider.GetComponentInParent<GuardAI>().isMage)
                                infoHit.collider.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                        {
                            infoHit.collider.gameObject.GetComponentInParent<GuardAI>().detection = 100;
                            isDetected = true;
                            infoHit.collider.gameObject.GetComponentInParent<GuardAI>().objectToAttack = gameObject;
                            infoHit.collider.gameObject.GetComponentInParent<GuardAI>().isAlerted = true;
                            infoHit.collider.gameObject.GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                        {
                            killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<GuardAI>().experience;
                            StartCoroutine("KillExperienceShow");
                            experience += infoHit.collider.gameObject.GetComponentInParent<GuardAI>().experience;
                        }
                        if (infoHit.collider.name != "Solovey")
                        {
                            if (infoHit.collider.transform.root.tag == "VillageGuard")
                                GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                            if (infoHit.collider.transform.root.name != "PatrolRoyalist2" && infoHit.collider.transform.root.name != "PatrolRoyalist1"&& infoHit.collider.transform.root.name != "StrangeRoyalist")
                                if (infoHit.collider.transform.root.tag == "Royalist")
                                GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                if (infoHit.collider.transform.root.tag == "Republican")
                                GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                            if (infoHit.collider.transform.root.name == "StrangeRoyalist")
                            {
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                    GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                            }
                        }
                        //Call near people
                        if (infoHit.collider.transform.root.name != "PatrolRoyalist2" && infoHit.collider.transform.root.name != "PatrolRoyalist1" && infoHit.collider.transform.root.name != "StrangeRoyalist")
                            CallNear();
                        if (infoHit.collider.transform.root.name == "StrangeRoyalist")
                        {
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                    CallNear();
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                CallNear();
                        }
                    }
                if (infoHit.collider.GetComponentInParent<SummonedAI>() != null)
                    if (infoHit.collider.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                        {
                            //If player has equiped weapon play huge damage sound for weapon
                            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                            }
                            else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                            }
                            selfAudio.Play();
                        }
                        //If player hasn't equiped weapon play huge damage sound for fists
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHugeHit;
                            selfAudio.Play();
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                        {
                            infoHit.collider.GetComponentInParent<SummonedAI>().currentHP -= GetComponentInChildren<HandWeaponScript>().weaponDamage * 2;
                            if (infoHit.collider.GetComponentInParent<SummonedAI>().currentHP > 0)
                                infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                            infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block = false;
                            infoHit.collider.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                            infoHit.collider.GetComponentInParent<Animator>().speed = 1;
                            infoHit.collider.GetComponentInParent<SummonedAI>().StopCoroutine("CancelBlock");
                            infoHit.collider.GetComponentInParent<SummonedAI>().StopCoroutine("TimeForBlock");
                        }
                        else if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                        {
                                infoHit.collider.GetComponentInParent<SummonedAI>().currentHP -= GetComponentInChildren<HandWeaponScript>().weaponDamage * 2 * stealthAttackModify;
                            if (infoHit.collider.GetComponentInParent<SummonedAI>().currentHP > 0)
                                infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                            infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block = false;
                            infoHit.collider.GetComponentInParent<Animator>().speed = 1;
                            infoHit.collider.GetComponentInParent<SummonedAI>().StopCoroutine("CancelBlock");
                            infoHit.collider.GetComponentInParent<SummonedAI>().StopCoroutine("TimeForBlock");
                            if (!infoHit.collider.GetComponentInParent<SummonedAI>().isArcher)
                                infoHit.collider.GetComponentInParent<Animator>().SetBool("IsBlocking", false);
                        }
                        if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                        {
                            isDetected = true;
                            infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().objectToAttack = gameObject;
                            infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().isAlerted = true;
                        }
                        if (infoHit.collider.name != "Solovey")
                        {
                            if (infoHit.collider.transform.root.tag == "VillageGuard")
                                GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                            if (infoHit.collider.transform.root.tag == "Royalist")
                                GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                            if (infoHit.collider.transform.root.tag == "Republican")
                                GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                        }
                        //Call near people
                        CallNear();
                    }
            }
            //Huge damage for for Summoned
            if (infoHit.collider.transform.root.tag == "Summoned" && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
            {
                if (infoHit.collider.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                {
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                    {
                        //If player has equiped weapon play huge damage sound for weapon
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                        }
                        else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                        }
                        selfAudio.Play();
                    }
                    //If player hasn't equiped weapon play huge damage sound for fists
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                    {
                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHugeHit;
                        selfAudio.Play();
                    }
                    if (infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                    {
                        infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * 2;
                        if (infoHit.collider.GetComponentInParent<SummonedAI>().currentHP > 0)
                            infoHit.collider.gameObject.GetComponentInParent<DeclineAnimationScript>().StunAnimation();
                        infoHit.collider.gameObject.GetComponentInParent<SummonedAI>().block = false;
                    }
                }
            }
            if ((infoHit.collider.GetComponentInParent<SummonedAI>() == null && infoHit.collider.GetComponentInParent<GuardAI>() == null && infoHit.collider.GetComponentInParent<CivilianAI>() == null) && (infoHit.point - transform.position).magnitude <= 2.5)
            {
                selfAudio.loop = false;
                selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().missSound;
                selfAudio.Play();
            }
            if (infoHit.collider.GetComponentInParent<AnimalAI>()!=null && (infoHit.collider.gameObject.transform.position - transform.position).magnitude <= 2.5)
            {
                if (infoHit.collider.transform.root.GetComponent<AnimalAI>().currentHP > 0)
                {
                    //If player has equiped weapon play huge damage sound for weapon
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().hugeAttackSound;
                        }
                        else if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
                            selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().twoHandHugeHit;
                        }
                        selfAudio.Play();
                    }
                    //If player hasn't equiped weapon play huge damage sound for fists
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon == null)
                    {
                        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
                        selfAudio.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().withoutWeaponHugeHit;
                        selfAudio.Play();
                    }
                    if (infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                        infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * 2;
                    else if (infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                        infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= GameObject.Find("PlayerWeapon").GetComponent<HandWeaponScript>().weaponDamage * 2 * stealthAttackModify;
                    if (infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP <= 0)
                    {
                        killExperience.GetComponentInChildren<Text>().text = "Experience:" + infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().experience;
                        StartCoroutine("KillExperienceShow");
                        experience += infoHit.collider.gameObject.GetComponentInParent<AnimalAI>().experience;
                    }
                }
            }
        }

    }
    //Show red damage screen if player got damage
    public IEnumerator DamageScreenAppear()
    {
        GameObject.Find("DamagePanel").GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("DamagePanel").GetComponent<Image>().enabled = false;
    }
    //Regenerate mana and stamina.Regenerate health if combatEnemies=0
    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(1f);
        if (combatEnemies == 0 && currentHealth < hpPlayer)
            currentHealth++;
        if (currentMana < manaPlayer)
            currentMana++;
        if (currentStamina < staminaPlayer)
            currentStamina++;
        StartCoroutine("Regen");
    }
    //Stop all actions,set animation bools to false and play idle after dialogue has been started
    private void CancelActionsAfterStartDialogue()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsBlocking", false);
        animator.SetBool("IsHugeAttacking", false);
        animator.Play("Idle");
        selfAudio.clip = null;
        selfAudio.Play();
    }
    //Stop player and throw him out if player entered closed zone
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Can'tPass")
        {
            transform.localPosition -= (collision.GetContact(0).point - transform.position) * 0.5f;
        }
    }
    //+1 to collision counter to show dialog
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.root.tag == "SimplePeople")
        {
            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
            {
                if (collisionCount == 0)
                    StartCoroutine("NullCollisionCount");
                if (addToCollisionCount && !isCrouched && !isDetected && !guiController.GUIopened)
                {
                    addToCollisionCount = false;
                    collisionCount++;
                    StartCoroutine("SetAddToCollisionCount");
                }
            }
        }
    }
    //Set addToCollisionCount to true
    private IEnumerator SetAddToCollisionCount()
    {
        yield return new WaitForSeconds(1f);
        addToCollisionCount = true;
    }
    //Collision count =0 after 30 seconds
    IEnumerator NullCollisionCount()
    {
        yield return new WaitForSeconds(30);
        collisionCount = 0;
    }
    //Function to call nearest Person after damage of hugedamage
    public void CallNear()
    {
        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
        Ray callRay = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
        RaycastHit callHit;
            if (Physics.Raycast(callRay, out callHit, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
        {
            if (callHit.collider.transform.root.tag == "SimplePeople" || callHit.collider.transform.root.tag == "Civilian" || callHit.collider.transform.root.tag == "VillageGuard"||callHit.collider.transform.root.tag=="Bandit")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
                {
                    if (callHit.collider.transform.root.tag == "Civilian" || callHit.collider.transform.root.tag == "SimplePeople")
                        if (callHit.collider.GetComponentInParent<CivilianAI>().currentHP <= 0)
                        {
                            Ray ray = new Ray(GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position + GameObject.FindObjectsOfType<CivilianAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position);
                            Ray personRay = new Ray(GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position + GameObject.FindObjectsOfType<CivilianAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<CivilianAI>()[i].transform.forward);
                            RaycastHit rayHit;
                            if (Physics.Raycast(ray, out rayHit))
                            {
                                if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                {
                                    if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = this.gameObject;
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().CallNear();
                                        if (GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount == false)
                                        {
                                            GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                                            GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount = true;
                                        }
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                            {
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = this.gameObject;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().CallNear();
                                if (GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount == false)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount = true;
                                }
                            }
                        }
                    if (callHit.collider.transform.root.tag == "VillageGuard")
                        if (callHit.collider.GetComponentInParent<GuardAI>() != null)
                        {
                            if (callHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                            {
                                Ray ray = new Ray(GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position + GameObject.FindObjectsOfType<CivilianAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position);
                                Ray personRay = new Ray(GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position + GameObject.FindObjectsOfType<CivilianAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<CivilianAI>()[i].transform.forward);
                                RaycastHit rayHit;
                                if (Physics.Raycast(ray, out rayHit))
                                {
                                    if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                    {
                                        if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                        {
                                            GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                            GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                            GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = this.gameObject;
                                            GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                            GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                                            GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().CallNear();
                                            if (GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount == false)
                                            {
                                                GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                {
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = this.gameObject;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().CallNear();
                                    if (GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount == false)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                                        GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount = true;
                                    }
                                }
                            }
                        }
                }
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                    {
                        if (callHit.collider.transform.root.tag == "VillageGuard")
                            if (callHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                if (callHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                                {
                                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                    RaycastHit rayHit;
                                    if (Physics.Raycast(ray, out rayHit))
                                    {
                                        if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                        {
                                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                            {
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                            }
                        if (callHit.collider.transform.root.tag == "Civilian" || callHit.collider.transform.root.tag == "SimplePeople")
                            if (callHit.collider.GetComponentInParent<CivilianAI>().currentHP <= 0)
                            {
                                Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                RaycastHit rayHit;
                                if (Physics.Raycast(ray, out rayHit))
                                {
                                    if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                    {
                                        if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                        {
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                            GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                            }
                    }
                }
            }
            if (callHit.collider.transform.root.tag == "Bandit")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit")
                    {
                        if (callHit.collider.transform.root.tag == "Bandit")
                        {
                            if (callHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                if (callHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                                {
                                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                    RaycastHit rayHit;
                                    if (Physics.Raycast(ray, out rayHit))
                                    {
                                        if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                        {
                                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                            {
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                            }
                            if (callHit.collider.GetComponentInParent<CivilianAI>() != null)
                            {
                                if (callHit.collider.GetComponentInParent<CivilianAI>().currentHP <= 0|| callHit.collider.GetComponentInParent<SummonedAI>().currentHP <= 0)
                                {
                                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                    RaycastHit rayHit;
                                    if (Physics.Raycast(ray, out rayHit))
                                    {
                                        if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                        {
                                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                            {
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (callHit.collider.transform.root.tag == "Republican")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Republican")
                    {
                        if (callHit.collider.transform.root.tag == "Republican")
                        {
                            if (callHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                if (callHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                                {
                                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                    RaycastHit rayHit;
                                    if (Physics.Raycast(ray, out rayHit))
                                    {
                                        if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                        {
                                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                            {
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (callHit.collider.transform.root.tag == "Royalist")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Royalist")
                    {
                        if (callHit.collider.transform.root.tag == "Royalist")
                        {
                            if (callHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                if (callHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                                {
                                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                    RaycastHit rayHit;
                                    if (Physics.Raycast(ray, out rayHit))
                                    {
                                        if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                        {
                                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                            {
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (callHit.collider.transform.root.tag == "Undead")
            {
                for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
                {
                    if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Undead")
                    {
                        if (callHit.collider.transform.root.tag == "Undead")
                        {
                            if (callHit.collider.GetComponentInParent<GuardAI>() != null)
                            {
                                if (callHit.collider.GetComponentInParent<GuardAI>().currentHP <= 0)
                                {
                                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, callHit.collider.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                                    RaycastHit rayHit;
                                    if (Physics.Raycast(ray, out rayHit))
                                    {
                                        if (rayHit.collider.transform.root == callHit.collider.transform.root && Vector3.Angle(personRay.direction, ray.direction) <= 75)
                                        {
                                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                            {
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - callHit.transform.root.transform.position).magnitude <= 10)
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = this.gameObject;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    //Pocket grab function
    public void PocketGrab()
    {
        if (isCrouched && GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
        {
            int currentGrabChance = chanceForGrab;
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
            Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
            {
                if (hitItem.collider.transform.root.tag == "VillageGuard"|| hitItem.collider.transform.root.tag == "Bandit" || hitItem.collider.transform.root.tag == "Undead" || hitItem.collider.transform.root.tag == "Republican" || hitItem.collider.transform.root.tag == "Royalist")
                {
                    if(hitItem.collider.GetComponentInParent<SummonedAI>()==null)
                    if (hitItem.collider.transform.root.GetComponent<GuardAI>().currentHP > 0 && (hitItem.collider.transform.position - transform.position).magnitude <= 2 && hitItem.collider.transform.root.GetComponent<GuardAI>().objectToAttack == null)
                    {
                        Ray grabbedRay = new Ray(hitItem.collider.transform.root.transform.position + hitItem.collider.transform.root.transform.up, hitItem.collider.transform.root.transform.forward);
                        Ray toPlayerRay = new Ray(hitItem.collider.transform.root.transform.position + hitItem.collider.transform.root.transform.up, (gameObject.transform.position - hitItem.collider.transform.root.transform.position));
                        if (Vector3.Angle(grabbedRay.direction, toPlayerRay.direction) <= 90)
                            currentGrabChance -= 51;
                        int randomNumber = Random.Range(0, 101);
                        if (randomNumber < currentGrabChance)
                        {
                            if (hitItem.collider.transform.root.GetComponent<Loot>().loot.Length > 0)
                            {
                                bool haveGold = false;
                                bool haveObjects = false;
                                int randomItem;
                                for (int i = 0; i < hitItem.collider.transform.root.GetComponent<Loot>().loot.Length; i++)
                                {
                                    if (hitItem.collider.transform.root.GetComponent<Loot>().loot[i] != null)
                                    {
                                        haveObjects = true;
                                        break;
                                    }
                                }
                                if (haveObjects)
                                {
                                    while (true)
                                    {
                                        randomItem = Random.Range(0, hitItem.collider.transform.root.GetComponent<Loot>().loot.Length);
                                        if (hitItem.collider.transform.root.GetComponent<Loot>().loot[randomItem] != null)
                                        {
                                            grabWindow.SetActive(true);
                                            grabWindow.GetComponentInChildren<Text>().text = hitItem.collider.transform.root.GetComponent<Loot>().loot[randomItem].GetComponent<Item>().itemName;
                                            StopCoroutine("CloseGrabWindow");
                                            StartCoroutine("CloseGrabWindow");
                                            GameObject.Find("GUIManager").GetComponent<Inventory>().GrabItem(hitItem.collider.transform.root.gameObject, randomItem);
                                            break;
                                        }
                                    }
                                }
                                else if (haveGold)
                                {
                                    gold += hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold;
                                    goldWindow.GetComponentInChildren<Text>().text = "Gold:+" + hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold;
                                    hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold = 0;
                                    goldText.text = "Gold:" + gold;
                                    goldWindow.SetActive(true);
                                    StartCoroutine("CloseGoldWindow");
                                    grabWindow.SetActive(false);
                                    grabWindow.GetComponentInChildren<Text>().text = "";
                                    StopCoroutine("CloseGrabWindow");

                                }
                                else if (!haveGold && !haveObjects)
                                {
                                    grabWindow.SetActive(true);
                                    grabWindow.GetComponentInChildren<Text>().text = "Nothing";
                                    StartCoroutine("CloseGrabWindow");
                                    StopCoroutine("CloseGoldWindow");
                                    goldText.text = "";
                                    goldWindow.SetActive(false);
                                }
                            }
                            else
                            {
                                grabWindow.SetActive(true);
                                grabWindow.GetComponentInChildren<Text>().text = "Nothing";
                                StartCoroutine("CloseGrabWindow");
                                StopCoroutine("CloseGoldWindow");
                                goldText.text = "";
                                goldWindow.SetActive(false);
                            }
                        }
                        else
                        {
                            pickpocketingWindow.SetActive(true);
                            StartCoroutine("ClosePickpocketingFailedWindow");
                            hitItem.collider.GetComponentInParent<GuardAI>().objectToAttack = gameObject;
                            hitItem.collider.GetComponentInParent<GuardAI>().detection = 100;
                            hitItem.collider.GetComponentInParent<GuardAI>().isAlerted = true;
                                if(hitItem.collider.transform.root.tag=="VillageGuard")
                            GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                if (hitItem.collider.transform.root.tag == "Republican")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                if (hitItem.collider.transform.root.name != "PatrolRoyalist2" && hitItem.collider.transform.root.name != "PatrolRoyalist1")
                                    if (hitItem.collider.transform.root.tag == "Royalist")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                isDetected = true;
                            CallNear();
                        }
                    }
                }
                if (hitItem.collider.transform.root.tag == "Civilian" || hitItem.collider.transform.root.tag == "SimplePeople")
                    if (hitItem.collider.transform.root.GetComponent<CivilianAI>().currentHP > 0 && (hitItem.collider.transform.position - transform.position).magnitude <= 2 && hitItem.collider.transform.root.GetComponent<CivilianAI>().attacker == null)
                    {
                        Ray grabbedRay = new Ray(hitItem.collider.transform.root.transform.position + hitItem.collider.transform.root.transform.up, hitItem.collider.transform.root.transform.forward);
                        Ray toPlayerRay = new Ray(hitItem.collider.transform.root.transform.position + hitItem.collider.transform.root.transform.up, (gameObject.transform.position - hitItem.collider.transform.root.transform.position));
                        if (Vector3.Angle(grabbedRay.direction, toPlayerRay.direction) <= 90)
                            currentGrabChance -= 51;
                        int randomNumber = Random.Range(0, 101);
                        if (randomNumber < currentGrabChance)
                        {
                            if (hitItem.collider.transform.root.GetComponent<Loot>().loot.Length > 0)
                            {
                                bool haveGold = false;
                                bool haveObjects = false;
                                int randomItem;
                                if (hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold > 0)
                                    haveGold = true;
                                for (int i = 0; i < hitItem.collider.transform.root.GetComponent<Loot>().loot.Length; i++)
                                {
                                    if (hitItem.collider.transform.root.GetComponent<Loot>().loot[i] != null)
                                    {
                                        haveObjects = true;
                                        break;
                                    }
                                }
                                if (haveObjects)
                                {
                                    while (true)
                                    {
                                        randomItem = Random.Range(0, hitItem.collider.transform.root.GetComponent<Loot>().loot.Length);
                                        if (hitItem.collider.transform.root.GetComponent<Loot>().loot[randomItem] != null)
                                        {
                                            grabWindow.SetActive(true);
                                            grabWindow.GetComponentInChildren<Text>().text = hitItem.collider.transform.root.GetComponent<Loot>().loot[randomItem].GetComponent<Item>().itemName;
                                            StopCoroutine("CloseGrabWindow");
                                            StartCoroutine("CloseGrabWindow");
                                            GameObject.Find("GUIManager").GetComponent<Inventory>().GrabItem(hitItem.collider.transform.root.gameObject, randomItem);
                                            break;
                                        }
                                    }
                                }
                                else if (haveGold)
                                {
                                    gold += hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold;
                                    goldWindow.GetComponentInChildren<Text>().text = "Gold:+" + hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold;
                                    hitItem.collider.transform.root.GetComponent<Loot>().amountOfGold = 0;
                                    goldText.text = "Gold:" + gold;
                                    goldWindow.SetActive(true);
                                    StartCoroutine("CloseGoldWindow");
                                    grabWindow.SetActive(false);
                                    grabWindow.GetComponentInChildren<Text>().text = "";
                                    StopCoroutine("CloseGrabWindow");

                                }
                                else if (!haveGold && !haveObjects)
                                {
                                    grabWindow.SetActive(true);
                                    grabWindow.GetComponentInChildren<Text>().text = "Nothing";
                                    StartCoroutine("CloseGrabWindow");
                                    StopCoroutine("CloseGoldWindow");
                                    goldText.text = "";
                                    goldWindow.SetActive(false);
                                }
                            }
                            else
                            {
                                grabWindow.SetActive(true);
                                grabWindow.GetComponentInChildren<Text>().text = "Nothing";
                                StartCoroutine("CloseGrabWindow");
                                StopCoroutine("CloseGoldWindow");
                                goldText.text = "";
                                goldWindow.SetActive(false);
                            }
                        }
                        else
                        {
                            pickpocketingWindow.SetActive(true);
                            StartCoroutine("ClosePickpocketingFailedWindow");
                            hitItem.collider.GetComponentInParent<CivilianAI>().attacker = gameObject;
                            hitItem.collider.GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                            hitItem.collider.GetComponentInParent<CivilianAI>().detection = 100;
                            GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                            isDetected = true;
                            CallNear();
                        }
                    }
            }
        }
    }
    public void OpenCookWindow()
    {
        LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
        Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
        RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer))
            {
                if (hitItem.collider.tag == "Fire")
                {
                    enabled = false;
                    GetComponentInChildren<CameraMovement>().enabled = false;
                    GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = false;
                    guiController.enabled = false;
                    guiController.cookWindow.SetActive(true);
                    guiController.personName.SetActive(false);
                    guiController.enemyHPPlayer.SetActive(false);
                    guiController.playerController.newLevel.SetActive(false);
                    guiController.stealtBar.SetActive(false);
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                  animator.Play("Idle");
                    else
                      animator.Play("CrouchingIdle");
                   animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", false);
                 animator.SetBool("IsHugeAttacking", false);
                   animator.SetBool("IsBlocking", false);
                  animator.SetBool("IsDrawingArrow", false);
                 animator.SetBool("IsCrouchRunning", false);
                selfAudio.clip = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("CloseGoldWindow");
                    GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("CloseGrabWindow");
                    GameObject.Find("GUIManager").GetComponent<Inventory>().inventoryIsFull.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().needMoreSpace.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().needMoreMoney.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.SetActive(false);
                    guiController.playerController.enabled = false;
                    guiController.cameraMovement.enabled = false;
                    guiController.itemInfoRay.gameObject.SetActive(false);
                    guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                    guiController.personNameText.GetComponent<Text>().text = "";
                    guiController.compass.SetActive(false);
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().newStatLevel.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().locked.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().needPicklock.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().pickpocketingWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().unlockFailed.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(false);
                }
            }           
    }
    public void OpenAlchemyWindow()
    {
            LayerMask layer = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("SpecialOffset"));
            Ray rayItem = main.ScreenPointToRay(new Vector3(main.scaledPixelWidth / 2, main.scaledPixelHeight / 2, 0));
            RaycastHit hitItem;
            if (Physics.Raycast(rayItem, out hitItem, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
            {
                if (hitItem.collider.tag == "Alchemy")
                {
                    enabled = false;
                    GetComponentInChildren<CameraMovement>().enabled = false;
                    GameObject.Find("GUIManager").GetComponent<PauseMenu>().enabled = false;
                    guiController.enabled = false;
                    guiController.alchemyWindow.SetActive(true);
                    guiController.personName.SetActive(false);
                    guiController.enemyHPPlayer.SetActive(false);
                    guiController.playerController.newLevel.SetActive(false);
                    guiController.stealtBar.SetActive(false);
                    if (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched)
                  animator.Play("Idle");
                    else
                    animator.Play("CrouchingIdle");
                   animator.SetBool("IsRunning", false);
                   animator.SetBool("IsAttacking", false);
                    animator.SetBool("IsHugeAttacking", false);
                    animator.SetBool("IsBlocking", false);
                  animator.SetBool("IsDrawingArrow", false);
                    animator.SetBool("IsCrouchRunning", false);
               selfAudio.clip = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("CloseGoldWindow");
                    GameObject.Find("Player").GetComponent<PlayerController>().StopCoroutine("CloseGrabWindow");
                    GameObject.Find("GUIManager").GetComponent<Inventory>().inventoryIsFull.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().needMoreSpace.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().needMoreMoney.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().newQuest.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().questFailed.SetActive(false);
                    guiController.playerController.enabled = false;
                    guiController.cameraMovement.enabled = false;
                    guiController.itemInfoRay.gameObject.SetActive(false);
                    guiController.enemyHPPlayerText.GetComponent<Text>().text = "";
                    guiController.personNameText.GetComponent<Text>().text = "";
                    guiController.compass.SetActive(false);
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().newStatLevel.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().locked.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().needPicklock.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().grabWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().goldWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().pickpocketingWindow.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().unlockFailed.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().permanentGUI.SetActive(false);
                }
            }
    }
    private IEnumerator ClosePickpocketingFailedWindow()
    {
        yield return new WaitForSeconds(3);
        pickpocketingWindow.SetActive(false);
    }
    private IEnumerator CloseGrabWindow()
    {
        yield return new WaitForSeconds(3);
        grabWindow.SetActive(false);
        grabWindow.GetComponentInChildren<Text>().text = "";
    }
    //Close gold+ widnow after 3 seconds
private IEnumerator CloseGoldWindow()
    {
        yield return new WaitForSeconds(3);
        goldWindow.GetComponentInChildren<Text>().text = "";
        goldWindow.SetActive(false);
    }
    //Open and close crateIsLocked window
    private IEnumerator NeedLockPick()
    {
        needPicklock.SetActive(true);
        yield return new WaitForSeconds(3);
        needPicklock.SetActive(false);
    }
    //Open and close unlock failed
    private IEnumerator UnlockFailed()
    {
        unlockFailed.SetActive(true);
            yield return new WaitForSeconds(3);
        unlockFailed.SetActive(false);
    }
    //Open and close new quest window
    public IEnumerator NewQuest()
    {
       newQuest.SetActive(true);
        yield return new WaitForSeconds(3);
        newQuest.SetActive(false);
    }
    public IEnumerator QuestFailed()
    {
        questFailed.SetActive(true);
        yield return new WaitForSeconds(3);
       questFailed.SetActive(false);
    }
    //Open and close NewStage window
    public IEnumerator NewStage()
    {
        newStage.SetActive(true);
        yield return new WaitForSeconds(3);
        newStage.SetActive(false);
    }
    //Open and close quest completed window
    public IEnumerator QuestCompleted()
    {
        questCompleted.SetActive(true);
        yield return new WaitForSeconds(3);
        questCompleted.SetActive(false);
    }
    public IEnumerator NewQuestThenNewStage(string questName)
    {
        newQuest.SetActive(true);
        yield return new WaitForSeconds(3);
        newQuest.SetActive(false);
        if(questName=="FayeQuest")
        newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfGuardQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfVillageQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "LibrarianSurveyQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfHuntersQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfRepublicansQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfRepublicansSecondQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfRoyalistsQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "HeadOfRoyalistsSecondQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "PaladinSpecialQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage];
        if (questName == "LibrarianSpecialQuest")
            newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage];
        if (!dialogueIsActive && !guiController.GUIopened)
        {
            newStage.SetActive(true);
            yield return new WaitForSeconds(3);
            newStage.SetActive(false);
        }
    }
    private IEnumerator NewLevel()
    {
        newLevel.SetActive(true);
        yield return new WaitForSeconds(3);
        newLevel.SetActive(false);
    }
    private IEnumerator DeathMenu()
    {
        yield return new WaitForSeconds(2);
        guiController.deathBlur.SetActive(true);
        guiController.deathMenu.SetActive(true);
        GameObject.Find("Music").GetComponent<AudioSource>().pitch *= 0.5f;
        Time.timeScale = 0f;
    }
    private IEnumerator NeedToBeRobber()
    {
        GameObject.Find("GUIManager").GetComponent<GUIController>().needToBeRobber.SetActive(true);
        yield return new WaitForSeconds(3);
        GameObject.Find("GUIManager").GetComponent<GUIController>().needToBeRobber.SetActive(false);
    }
    private IEnumerator KillExperienceShow()
    {
        killExperience.SetActive(true);
        yield return new WaitForSeconds(3);
        killExperience.SetActive(false);
    }
    private IEnumerator EnoughMana()
    {
        GameObject.Find("GUIManager").GetComponent<GUIController>().enoughMana.SetActive(true);
        yield return new WaitForSeconds(3);
        GameObject.Find("GUIManager").GetComponent<GUIController>().enoughMana.SetActive(false);
    }
    //Load player data and camera rotation
    private void LoadPlayerData()
    {
        PlayerData playerData;
        playerData = SaveLoad.globalPlayerData;
        combatEnemies = playerData.combatEnemies;
        currentHealth = playerData.currentHealth;
        currentMana = playerData.currentMana;
        currentStamina = playerData.currentStamina;
        hpPlayer = playerData.hpPlayer;
        manaPlayer = playerData.manaPlayer;
        staminaPlayer = playerData.staminaPlayer;
        isCrouched = playerData.isCrouched;
        isDetected = playerData.isDetected;
        stealthAttackModify = playerData.stealthAttackModify;
        gold = playerData.gold;
        chanceForGrab = playerData.chanceForGrab;
        armor = playerData.armor;
        prestige = playerData.prestige;
        chanceForUnlock = playerData.chanceForUnlock;
        skillPoints = playerData.skillPoints;
        experience = playerData.experience;
        fireballDamageModify = playerData.fireballDamageModify;
        recoverModify = playerData.recoverModify;
        level = playerData.level;
        experienceForNextLevel = playerData.experienceForNextLevel;
        transform.position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);
        GetComponentInChildren<CameraMovement>().gameObject.transform.eulerAngles = new Vector3(playerData.cameraRotation[0], playerData.cameraRotation[1], playerData.cameraRotation[2]);
        transform.eulerAngles = new Vector3(playerData.playerRotation[0], playerData.playerRotation[1], playerData.playerRotation[2]);
        if (playerData.currentSummonedArcherPosition != null)
        {
            GameObject spawn = Instantiate(summonedArcher, new Vector3(playerData.currentSummonedArcherPosition[0], playerData.currentSummonedArcherPosition[1], playerData.currentSummonedArcherPosition[2]), summonedArcher.transform.rotation);
            spawn.transform.eulerAngles = new Vector3(playerData.currentSummonedArcherRotation[0], playerData.currentSummonedArcherRotation[1], playerData.currentSummonedArcherRotation[2]);
            spawn.GetComponent<SummonedAI>().loadedHp = playerData.currentSummonedArcherCurrentHP;
            spawn.GetComponent<SummonedAI>().loaded = true;
            spawn.GetComponent<SummonedAI>().summoner = gameObject;
            currentSummonedArcher = spawn;
            spawn.GetComponent<SummonedAI>().guardHP += spawn.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + spawn.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
            spawn.GetComponent<SummonedAI>().damage += spawn.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + spawn.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
        }
        if (playerData.currentSummonedMeleePosition != null)
        {
            GameObject spawn = Instantiate(summonedMelee, new Vector3(playerData.currentSummonedMeleePosition[0], playerData.currentSummonedMeleePosition[1], playerData.currentSummonedMeleePosition[2]), summonedArcher.transform.rotation);
            spawn.transform.eulerAngles = new Vector3(playerData.currentSummonedMeleeRotation[0], playerData.currentSummonedMeleeRotation[1], playerData.currentSummonedMeleeRotation[2]);
            spawn.GetComponent<SummonedAI>().loadedHp = playerData.currentSummonedMeleeCurrentHP;
            spawn.GetComponent<SummonedAI>().loaded = true;
            spawn.GetComponent<SummonedAI>().summoner = gameObject;
            currentSummonedMelee = spawn;
            spawn.GetComponent<SummonedAI>().guardHP += spawn.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + spawn.GetComponent<SummonedAI>().guardHP * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
            spawn.GetComponent<SummonedAI>().damage += spawn.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonModify / 100 + spawn.GetComponent<SummonedAI>().damage * GameObject.Find("SkillManager").GetComponent<SkillManager>().summonSpellModifyStat / 100;
        }
    }
}
