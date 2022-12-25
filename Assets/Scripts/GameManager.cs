using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    public float stableSpellSpeed;
    public bool villageAttackedByPlayer;
    public HealthBar healthBar;
    public ManaBar manaBar;
    public StaminaBar staminaBar;
    public Canvas canvas;
    public PlayerController player;
    public Slider stealthBar;
    public GameObject objectForLoot;
    public GameObject arrow;
    public GameObject aiSummonedMelee;
    public GameObject aiSummonedArcher;
    public GameObject rabbitPrefab;
    public bool withQuestMarkers = true;
    public float stableWalkingSpeed;
    public float stableHardRunningSpeed;
    public float stableLightRunningSpeed;
    public bool republicanAttackedByPlayer;
    public bool royalistAttackedByPlayer;
    public bool isPaladin;
    public bool isMage;
    public bool isRobber;
    public int numberOfRabbits = 0;
    public List<GameObject> listOfItemsOnScene = new List<GameObject>();
    public int timeToCreateCloud = 3;
    public GameObject cloudPrefab;
    private Dialogue guardDialogs;
    private Dialogue simplePeopleDialogs;
    public int killedDungeon;
    public bool headOfBanditKilled = false;
    public bool rightHandBanditKilled = false;
    public GameObject stranger;
    public Image blockButton;
    public GameObject stealthButton;
    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 120;
        if (MenuVariables.isRobber)
            isRobber = true;
        if (MenuVariables.isPaladin)
            isPaladin = true;
        if (MenuVariables.isMage)
            isMage = true;
        if (!MenuVariables.withQuestMarkers)
            withQuestMarkers = false;
        stableSpellSpeed = 0.4f;
        stableWalkingSpeed = 1.5f;
        stableLightRunningSpeed = 4;
        stableHardRunningSpeed = 2;
        if (SaveLoad.isLoading)
        {
            LoadManager();
            LoadAnimals();
            LoadClouds();
        }
        if (isRobber)
        {
            stranger.SetActive(true);
            stealthButton.SetActive(true);
        }
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            GameObject.Find("GUIManager").GetComponent<Inventory>().dragonScroll.GetComponent<Dialogue>().sentences[0] = "[The text is written in a language unknown to you.You can't make out anything. Hope the customer knows what he's buying]";
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            GameObject.Find("GUIManager").GetComponent<Inventory>().dragonScroll.GetComponent<Dialogue>().sentences[0] = "[The text is written in a language unknown to you.You can't make out anything. Hope the Academy knows something about it]";
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            GameObject.Find("GUIManager").GetComponent<Inventory>().dragonScroll.GetComponent<Dialogue>().sentences[0] = "[The text is written in a language unknown to you.You can't make out anything. Hope the Order knows something about this]";
    }
    void Start()
    {
        if (!SaveLoad.isLoading)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_first_adventure);
            if(isRobber)
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_robber);
            if (isPaladin)
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_paladin);
            if (isMage)
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_mage);
        }
        //Set max values for GUI bars
        simplePeopleDialogs = GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>();
        guardDialogs = GameObject.Find("GuardDialogs").GetComponent<Dialogue>();
        healthBar.SetMaxValue();
        manaBar.SetMaxValue();
        staminaBar.SetMaxValue();
        ChangeDialogsOnStart();
        if (SaveLoad.isLoading)
            LoadItems();
        StartCoroutine("SpawnAnimals");
        StartCoroutine("CreateClouds");
        if (headOfBanditKilled)
        {
            player.prestige = 100;
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[18] = "The head of bandits is destroyed. And the handful of deserters which remained, cost nothing without it. Now it is possible to have a rest a little";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[18] = "The bandits are destroyed? Now we can relax";
            if(GameObject.Find("Village Merchant")!=null)
            GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[17] = "Thank you for destroying the bandits. Now I can leave the valley";
            if (GameObject.Find("Hunter") != null)
                GameObject.Find("Hunter").GetComponent<Dialogue>().sentences[4] = "We took revenge on Brad";
            if (GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[7] = "The bandits were destroyed. We helped ordinary people. That's good";
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                    GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[7] = "Deserters must be destroyed";
            }
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questCompleted)
        {
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[11] = "Thanks for helping with that bandit squad in the valley";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[11] = "It's good that you defended our tower. It offers a beautiful view";
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[9] = "Thank you for destroying those bandits. Now the roads are safer";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questCompleted)
        {
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[12] = "Thank you for helping with the food. Maybe we will survive this year ...";
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[12] = "Thank you for the food. We will be able to fight the bandits much longer";
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[12] = "Thanks to you, these people will be able to survive the winter";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
            {
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[10] = "Now Solovey will say those bandits,that we have alcohol.Thanks...";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[10] = "Are you working with Solovey?We see you";
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled || GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
            {
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[10] = "Thanks.Solovey can't steal our beer anymore";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[10] = "Thanks for your help with betrayer";
            }
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[8] = "Sad,that people have to betray their homeland to rescue someone.In Arkelia treatment from plague is free...";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questCompleted)
        {
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[11] = "Good work in the crypt. It is interesting that the Academy learns";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[14] = "Something interesting was found in the crypt. Maybe extraterrestrial life?";
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[14] = "Something secret in the crypt? Well, it doesn't concern me";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
        {
            GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Thank you for your help. The Republic will not forget it";
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[14] = "The royalists are destroyed ... Believe me, you are fighting for the right cause";
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[16] = "The royalists are destroyed ... The main one will be furious";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[16] = "Someone destroyed the royalists. It seems we need to side with the Republicans";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
        {
            GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[15] = "Republicans are destroyed... Well, it's your choice";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[17] = "Someone destroyed the Republicans. It seems time to move to the side of the royalists";
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[17] = "Republicans are destroyed ... The chief will be happy";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.questCompleted)
        {
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[10] = "I heard that you destroyed Bamur's forces in the mountains. Great job";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[13] = "The priest says that you are our savior. Is this true?";
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[13] = "I felt it when you cleaned the sanctuary of Artelit. Thank you for your help";
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questCompleted)
        {
                GameObject.Find("Hunter").GetComponent<Dialogue>().sentences[3] = "Thank you for your help. The mushroom will help you survive the winter";
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[13] = "Slavian mushroom is a very valuable thing. And you could just take it to yourself ...";
        }
        if (killedDungeon == 10)
        {
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[15] = "Did you clear the crypt? Don't you want to join the guard?";
            if (GameObject.Find("Village Merchant") != null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[16] = "Thank you for the crypt. Now this land is calmer...";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[15] = "Crypt cleared?I hope that the night terrors will not appear again";
            if (GameObject.Find("Candle_Wall") != null)
            {
                GameObject.Find("Candle_Wall").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (1)") != null)
            {
                GameObject.Find("Candle_Wall (1)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (1)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (2)") != null)
            {
                GameObject.Find("Candle_Wall (2)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (2)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (3)") != null)
            {
                GameObject.Find("Candle_Wall (3)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (3)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (4)") != null)
            {
                GameObject.Find("Candle_Wall (4)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (4)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (5)") != null)
            {
                GameObject.Find("Candle_Wall (5)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (5)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (6)") != null)
            {
                GameObject.Find("Candle_Wall (6)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (6)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (7)") != null)
            {
                GameObject.Find("Candle_Wall (7)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (7)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (8)") != null)
            {
                GameObject.Find("Candle_Wall (8)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (8)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (9)") != null)
            {
                GameObject.Find("Candle_Wall (9)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (9)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Desk") != null)
            {
                GameObject.Find("Desk").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Desk").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Desk").transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("Desk").transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted)
            GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[5] = "Thank you for your help with the orders of the royalists. We are happy to see you";
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
            GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
    }

    // Update is called once per frame
    void Update()
    {
        if (player.block)
            blockButton.color = new Color(0, 1, 0, 0.6862745f);
        else
            blockButton.color = new Color(1, 1, 1, 0.6862745f);
        if (stealthButton.activeSelf)
        {
            if (player.isCrouched)
                stealthButton.GetComponent<Image>().color = new Color(0, 1, 0, 0.6862745f);
            else
                stealthButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.6862745f);
        }
        if (player.currentHealth <= 0 && !GameObject.Find("Player").GetComponent<Animator>().GetBool("IsDead"))
        {
            GameObject.Find("Player").GetComponent<DeclineAnimationScript>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("DeathMenu");
            GameObject.Find("Player").GetComponent<AudioSource>().clip = null;
            GameObject.Find("Player").GetComponent<AudioSource>().Play();
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Player").GetComponentInChildren<CameraMovement>().enabled = false;
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsHugeAttacking", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsBlocking", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDrawingArrow", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsStunned", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouching", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsCrouchRunning", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsStaying", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("TwoHand", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsDead", true);
            GameObject.Find("Player").GetComponent<Animator>().Play("Death");
            GameObject.Find("Main Camera").transform.localPosition = new Vector3(-0.5f, 3.82f, 0.3f);
            GameObject.Find("Main Camera").transform.localEulerAngles = new Vector3(86, -3, 0);
        }
        if (player.prestige <= -100)
            villageAttackedByPlayer = true;
        if (villageAttackedByPlayer)
            player.prestige = -100;
        if (isMage)
        {
            guardDialogs.sentences[5] = "Oh,mage,we are glad to see you.Maybe you can help with bandits";
            simplePeopleDialogs.sentences[5] = "Can you create wine?Our brewer was captured by bandits.";
        }
        else if (isRobber)
        {
            guardDialogs.sentences[5] = "I hope that you aren't with those bastards in bandit camp,because I will kill you,if you are";
            simplePeopleDialogs.sentences[5] = "If you will see brewer in camp,say him to come back";
        }
        else if (isPaladin)
        {
            guardDialogs.sentences[5] = "Warrior of Light?Maybe,this country has chance,if you alive";
            simplePeopleDialogs.sentences[5] = "Say Artelit,that we are waiting for new brewer.We can give him the priest as sacrifice";
        }
        if (player.prestige >= 25)
        {
            guardDialogs.sentences[6] = "Thanks for your help";
            simplePeopleDialogs.sentences[6] = "I thought that you are typical wanderer,but it was a mistake";
        }
        else if (player.prestige < 25 && player.prestige > -25)
        {
            guardDialogs.sentences[6] = "";
            simplePeopleDialogs.sentences[6] = "";
        }
        if (player.prestige >= 50)
        {
            guardDialogs.sentences[7] = "Our forces are weak,but we are ready to help you";
            simplePeopleDialogs.sentences[7] = "We have to drink vodka together";
        }
        else if (player.prestige < 50 && player.prestige > -50)
        {
            guardDialogs.sentences[7] = "";
            simplePeopleDialogs.sentences[7] = "";
        }
        if (player.prestige >= 75)
        {
            guardDialogs.sentences[8] = "Our village will never forget you";
            simplePeopleDialogs.sentences[8] = "You can take my wife,if you want";
        }
        if (player.prestige < 75 && player.prestige>-75)
        {
            guardDialogs.sentences[8] = "";
            simplePeopleDialogs.sentences[8] = "";
        }
        if (player.prestige >= 100)
        {
            guardDialogs.sentences[9] = "Sad,that you have to leave us.We wanted yo choose you as our chief";
            simplePeopleDialogs.sentences[9] = "I sold my reserves of wine to give money for a monument to you.But I am happy to do this";
        }
        if (player.prestige < 100&&player.prestige>-100)
        {
            guardDialogs.sentences[9] = "";
            simplePeopleDialogs.sentences[9] = "";
        }
        if (player.prestige <=-25)
        {
            guardDialogs.sentences[6] = "I am watching you";
            simplePeopleDialogs.sentences[6] = "Don't waste my time";
        }
        if (player.prestige <= -50)
        {
            guardDialogs.sentences[7] = "My blade is ready for your blood";
            simplePeopleDialogs.sentences[7] = "Get off!";
        }
        if (player.prestige <= -75)
        {
            guardDialogs.sentences[8] = "Get out of this village";
            simplePeopleDialogs.sentences[8] = "I will call guard";
        }
        if (killedDungeon == 9)
        {
            killedDungeon++;
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_clear_the_crypt);
            if (GameObject.Find("Candle_Wall") != null)
            {
                GameObject.Find("Candle_Wall").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (1)") != null)
            {
                GameObject.Find("Candle_Wall (1)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (1)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (2)") != null)
            {
                GameObject.Find("Candle_Wall (2)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (2)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (3)") != null)
            {
                GameObject.Find("Candle_Wall (3)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (3)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (4)") != null)
            {
                GameObject.Find("Candle_Wall (4)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (4)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (5)") != null)
            {
                GameObject.Find("Candle_Wall (5)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (5)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (6)") != null)
            {
                GameObject.Find("Candle_Wall (6)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (6)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (7)") != null)
            {
                GameObject.Find("Candle_Wall (7)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (7)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (8)") != null)
            {
                GameObject.Find("Candle_Wall (8)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (8)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Candle_Wall (9)") != null)
            {
                GameObject.Find("Candle_Wall (9)").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Candle_Wall (9)").transform.GetChild(1).gameObject.SetActive(false);
            }
            if (GameObject.Find("Desk") != null)
            {
                GameObject.Find("Desk").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Desk").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Desk").transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("Desk").transform.GetChild(2).gameObject.SetActive(false);
            }
            GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[15] = "Did you clear the crypt? Don't you want to join the guard?";
            GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[16] = "Thank you for the crypt. Now this land is calmer...";
            GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[15] = "Crypt cleared?I hope that the night terrors will not appear again";
        }
        //Update values of GUI bars
        healthBar.SetHealth();
        manaBar.SetMana();
        staminaBar.SetStamina();
        CloseGUIPartsAfterDeath();
        SaveLoad.globalInventoryData = null;
        SaveLoad.globalPlayerData = null;
        SaveLoad.globalGuardData = null;
        SaveLoad.globalCivilianData = null;
        SaveLoad.globalItemData = null;
        SaveLoad.globalLootCrateData = null;
        SaveLoad.globalSkillData = null;
        SaveLoad.globalQuestsData = null;
        SaveLoad.globalDialogueManagerData = null;
        SaveLoad.globalSpecialData = null;
        SaveLoad.globalGameManagerData = null;
        SaveLoad.globalAnimalData = null;
        SaveLoad.globalBirdsData = null;
        SaveLoad.globalCloudData = null;
    }
    //Disable GUI after player's death
    private void CloseGUIPartsAfterDeath()
    {
        if (player.currentHealth <= 0)
            canvas.enabled = false;
    }
    //Set value for stealth bar.Choose the biggest value
    public void SetValueForStealth()
    {
        for (int i = 0, maxvalue = 0; i < GameObject.FindObjectsOfType<ConeOfView>().Length; i++)
        {
            if (GameObject.FindObjectsOfType<ConeOfView>()[i].gameObject.tag == "Civilian" || GameObject.FindObjectsOfType<ConeOfView>()[i].gameObject.tag == "SimplePeople")
                if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().detection >= maxvalue)
            {
                    maxvalue = GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().detection;
                    stealthBar.value = GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<CivilianAI>().detection;
            }
                if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>()!=null)
                if (GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().detection >= maxvalue)
                {
                    maxvalue = GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().detection;
                    stealthBar.value = GameObject.FindObjectsOfType<ConeOfView>()[i].GetComponent<GuardAI>().detection;
                }
        }
    }
    //Change dialog texts depending on player's class
    private void ChangeDialogsOnStart()
    {
        if (isMage)
            GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[0] = "Greetings,mage,my friend in Academy sold me some spells,is interested?";
    }
    public IEnumerator TakeRewardFromSister()
    {
        yield return new WaitForSeconds(600);
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().rewardFromSister = true;
    }
    private IEnumerator SpawnAnimals()
    {
        yield return new WaitForSeconds(60);
        if (numberOfRabbits < 5)
        {
            int placeIndex = 0;
            Vector3 spawnPos = Vector3.up;
            while (true)
            {
                placeIndex = Random.Range(0, 4);
                if (placeIndex == 0)
                    spawnPos = new Vector3(-44.2f, 21, 63.02f);
                if (placeIndex == 1)
                    spawnPos = new Vector3(-48.69f, 21, -23.67f);
                if (placeIndex == 2)
                    spawnPos = new Vector3(-1.73f, 21, 39.5f);
                if (placeIndex == 3)
                    spawnPos = new Vector3(6.29f, 21, 78.93f);
                NavMeshPath path = new NavMeshPath();
                if ((spawnPos - player.gameObject.transform.position).magnitude >= 20)
                {
                    Instantiate(rabbitPrefab, spawnPos, rabbitPrefab.transform.rotation);
                    numberOfRabbits++;
                    break;
                }
            }
        }
        StartCoroutine("SpawnAnimals");
    }
    private IEnumerator CreateClouds()
    {
        yield return new WaitForSeconds(timeToCreateCloud);
        int numberOfClouds;
        Vector3 cloudRotation;
        Vector3 cloudPosition;
        numberOfClouds = Random.Range(1, 5);
        timeToCreateCloud = Random.Range(10, 61);
        while (numberOfClouds > 0)
        {
            cloudPosition = new Vector3(Random.Range(345,400), Random.Range(150, 200), Random.Range(-333, 270));
            cloudRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject spawn=Instantiate(cloudPrefab, cloudPosition,new Quaternion(0,0,0,0),GameObject.Find("Sky").transform);
            spawn.transform.eulerAngles = cloudRotation;
            numberOfClouds--;
        }
        StartCoroutine("CreateClouds");
    }
    private void LoadItems()
    {
        for (int i = 0; i < GameObject.FindObjectsOfType<Item>().Length; i++)
            if (GameObject.FindObjectsOfType<Item>()[i].name != "TimeObjectForInventory" && GameObject.FindObjectsOfType<Item>()[i].name != "TimeObjectForMoving")
                Destroy(GameObject.FindObjectsOfType<Item>()[i].gameObject);
                ItemData itemData = SaveLoad.globalItemData;
        listOfItemsOnScene.Clear();
        for(int i = 0; i < itemData.itemName.Length; i++)
        {
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(itemData.itemName[i]) != null)
            {
                GameObject spawn = Instantiate(GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(itemData.itemName[i]), new Vector3(itemData.itemPosition[i, 0], itemData.itemPosition[i, 1], itemData.itemPosition[i, 2]), transform.rotation);
                spawn.transform.eulerAngles = new Vector3(itemData.itemRotation[i, 0], itemData.itemRotation[i, 1], itemData.itemRotation[i, 2]);
                if (spawn.GetComponent<Item>().itemName == "Arrow")
                {
                    spawn.GetComponent<Arrow>().canDamage = itemData.canDamage[i];
                    if (spawn.GetComponent<Arrow>().canDamage)
                    {
                        foreach (Collider gameObject in FindObjectsOfType<Collider>())
                        {
                            if (gameObject.name == itemData.parentObject[i])
                                if (gameObject.transform.root.name == itemData.rootName[i])
                                    spawn.transform.SetParent(gameObject.transform, true);

                        }
                        spawn.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                        spawn.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
                if (spawn.GetComponent<Item>().itemName == "Hell mushroom" || spawn.GetComponent<Item>().itemName == "Slavia mushroom")
                    if (itemData.hasRigidbody[i])
                        spawn.AddComponent<Rigidbody>();
            }
        }
    }
    private void LoadManager()
    {
        GameManagerData gameManagerData = SaveLoad.globalGameManagerData;
        isRobber = gameManagerData.isRobber;
        isPaladin = gameManagerData.isPaladin;
        isMage = gameManagerData.isMage;
        villageAttackedByPlayer = gameManagerData.villageAttackedByPlayer;
        withQuestMarkers = gameManagerData.withQuestMarkers;
        timeToCreateCloud = gameManagerData.timeCloudsSpawn;
        killedDungeon = gameManagerData.killedDungeon;
        headOfBanditKilled = gameManagerData.headOfBanditKilled;
        rightHandBanditKilled = gameManagerData.rightHandBanditKilled;
    }
    private void LoadAnimals()
    {
        AnimalData animalData = SaveLoad.globalAnimalData;
        for(int i = 0; i < animalData.ID.Length; i++)
        {
            if (numberOfRabbits < 5)
            {
                if (animalData.ID[i] != "")
                {
                    GameObject spawn = Instantiate(rabbitPrefab);
                    spawn.GetComponent<AnimalAI>().ID = animalData.ID[i];
                    numberOfRabbits++;
                }
            }
        }
    }
    private void LoadClouds()
    {
        CloudData cloudData = SaveLoad.globalCloudData;
        for (int i = 0; i < cloudData.ID.Length; i++)
        {
                    GameObject spawn = Instantiate(cloudPrefab);
                    spawn.GetComponent<CloudAI>().ID = cloudData.ID[i];
        }
    }
}
