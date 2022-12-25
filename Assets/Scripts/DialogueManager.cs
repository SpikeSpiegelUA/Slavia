using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    //Scripts and UI elements
    public GameObject permanentGUI;
    public PlayerController playerController;
    public CameraMovement cameraMovement;
    public Text[] buttonTexts;
    public Image dialogueBackground;
    public Text head;
    public Text main;
    public Button[] buttons;
    public Dialogue dialogue;
    //Head of village quest
    public bool headOfVillageTakeQuest = false;
    public bool headOfVillageQuestPaladinChoosed = false;
    public bool headOfVillageQuestSimpleChoosed = false;
    public bool headOfVillageQuestRobberChoosed = false;
    public bool headOfVillageQuestMageChoosed = false;
    //Head of republicans
    public bool headOfRepublicansTakeQuest = false;
    public bool headOfRepublicansQuestPaladinChoosed = false;
    public bool headOfRepublicansQuestSimpleChoosed = false;
    public bool headOfRepublicansQuestRobberChoosed = false;
    public bool headOfRepublicansQuestMageChoosed = false;
    //Head of republicans second
    public bool headOfRepublicansTakeSecondQuest = false;
    public bool headOfRepublicansSecondQuestPaladinChoosed = false;
    public bool headOfRepublicansSecondQuestSimpleChoosed = false;
    public bool headOfRepublicansSecondQuestRobberChoosed = false;
    public bool headOfRepublicansSecondQuestMageChoosed = false;
    //Head of royalists
    public bool headOfRoyalistsTakeQuest = false;
    public bool headOfRoyalistsQuestPaladinChoosed = false;
    public bool headOfRoyalistsQuestSimpleChoosed = false;
    public bool headOfRoyalistsQuestRobberChoosed = false;
    public bool headOfRoyalistsQuestMageChoosed = false;
    //Head of royalists quest
    public bool headOfRoyalistsTakeSecondQuest = false;
    public bool headOfRoyalistsSecondQuestPaladinChoosed = false;
    public bool headOfRoyalistsSecondQuestSimpleChoosed = false;
    public bool headOfRoyalistsSecondQuestRobberChoosed = false;
    public bool headOfRoyalistsSecondQuestMageChoosed = false;
    //Head of hunters quest
    public bool headOfHuntersTakeQuest = false;
    public bool headOfHuntersQuestPaladinChoosed = false;
    public bool headOfHuntersQuestSimpleChoosed = false;
    public bool headOfHuntersQuestRobberChoosed = false;
    public bool headOfHuntersQuestMageChoosed = false;
    //Librarian special quest
    public bool librarianSpecialTakeQuest = false;
    //Paladin special quest
    public bool priestSpecialQuestTake = false;
    //Librarian survey variables
    public bool librarianSurveyTakeQuest = false;
    public bool librarianSurveyQuestPaladinChoosed = false;
    public bool librarianSurveyQuestSimpleChoosed = false;
    public bool librarianSurveyQuestRobberChoosed = false;
    public bool librarianSurveyQuestMageChoosed = false;
    public int surveyCorrectAnswers;
    //Guard head variables
    public bool headOfGuardTakeQuest = false;
    public bool headOfGuardQuestPaladinChoosed = false;
    public bool headOfGuardQuestSimpleChoosed = false;
    public bool headOfGuardQuestRobberChoosed = false;
    public bool headOfGuardQuestMageChoosed = false;
    //Some bools for faye dialogue
    public bool fayeHelpSaid1 = false;
    public bool fayeHelpSaid2 = false;
    public bool fayeTakeQuest = false;
    public bool fayeDialoguer = false;
    public bool fayeQuestPaladinChoosed = false;
    public bool fayeQuestSimpleChoosed = false;
    public bool fayeQuestRobberChoosed = false;
    public bool fayeQuestMageChoosed = false;
    public bool CivilWarTreeActivated = false;
    public bool fayeYesNoAnswers = false;
    public string clickedButtonName;
    private GameObject clickedButtonObject;
    public GameObject inventoryFullMessage;
    public bool dialogueIsOpen;
    public GUIController guiController;
    public bool rewardFromSister = false;
    public bool knowAboutScroll = false;
    public bool villageGuardStageOne = false;
    public bool extraWarriorsInArmy = false;
    public bool armyStageTwo = false;
    public bool soloveySisterHelped = false;
    public GameObject finishGame;
    public bool knowAboutOffer = false;
    public bool headOfGuardKilled = false;
  private void Awake()
    {
        if (SaveLoad.isLoading)
            LoadDialogueManager();
    }
    private void Update()
    {
        if (dialogue != null)
        {
            if (dialogue.gameObject.GetComponent<CivilianAI>() != null)
            {
                if (dialogue.gameObject.GetComponent<CivilianAI>().hasBeenAttacked == true)
                {
                    fayeYesNoAnswers = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
                    GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
                    GameObject.Find("Player").GetComponent<PlayerController>().newQuest.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
                    dialogueIsOpen = false;
                    permanentGUI.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().shop.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().dialogueUI.SetActive(true);
                    dialogueBackground.gameObject.SetActive(false);
                    head.gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(false);
                    buttons[5].gameObject.SetActive(false);
                    buttons[6].gameObject.SetActive(false);
                    buttons[7].gameObject.SetActive(false);
                    buttons[1].GetComponent<Image>().color = Color.white;
                    guiController.enabled = true;
                    main.gameObject.SetActive(false);
                    buttons[3].GetComponent<Image>().color = Color.white;
                    GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Use").gameObject.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Drop").gameObject.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Buy").gameObject.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Back").gameObject.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().itemInfoInventory.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem.GetComponent<Image>().color = Color.white;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem = GameObject.Find("GUIManager").GetComponent<GUIController>().timeImageGameObject.gameObject;
                    if (!GameObject.Find("Player").GetComponent<Animator>().GetBool("IsStunned"))
                    {
                        playerController.enabled = true;
                        cameraMovement.enabled = true;
                    }
                    dialogue = null;
                    fayeDialoguer = false;
                }
            }
            else if (dialogue.gameObject.GetComponent<GuardAI>() != null)
            {
                if (dialogue.gameObject.GetComponent<GuardAI>().isAlerted)
                {
                    fayeYesNoAnswers = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
                    GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
                    GameObject.Find("Player").GetComponent<PlayerController>().newQuest.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
                    dialogueIsOpen = false;
                    permanentGUI.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().shop.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().dialogueUI.SetActive(true);
                    dialogueBackground.gameObject.SetActive(false);
                    head.gameObject.SetActive(false);
                    buttons[3].GetComponent<Image>().color = Color.white;
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                    buttons[4].gameObject.SetActive(false);
                    buttons[5].gameObject.SetActive(false);
                    buttons[6].gameObject.SetActive(false);
                    buttons[7].gameObject.SetActive(false);
                    buttons[1].GetComponent<Image>().color = Color.white;
                    guiController.enabled = true;
                    main.gameObject.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Use").gameObject.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Drop").gameObject.SetActive(true);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Buy").gameObject.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Back").gameObject.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<GUIController>().itemInfoInventory.SetActive(false);
                    GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem.GetComponent<Image>().color = Color.white;
                    GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem = GameObject.Find("GUIManager").GetComponent<GUIController>().timeImageGameObject.gameObject;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.SetActive(true);
                    if (!GameObject.Find("Player").GetComponent<Animator>().GetBool("IsStunned"))
                    {
                        playerController.enabled = true;
                        cameraMovement.enabled = true;
                    }
                    dialogue = null;
                    fayeDialoguer = false;
                }
            }
        }
    }
    //Set dialoguer for other code in class
    public void SetDialoguer(GameObject dialoguer)
    {
        dialogue = dialoguer.GetComponent<Dialogue>();
        HandleDialogue();
    }
    //Start dialog for collision dialog
    public void CollisionDialog(GameObject dialoguer)
    {
        dialogueIsOpen = true;
        GameObject.Find("EnemyHPPlayer").GetComponent<Image>().enabled = false;
        GameObject.Find("EnemyHPText").GetComponent<Text>().text = "";
        GameObject.Find("PersonName").GetComponent<Image>().enabled = false;
        GameObject.Find("PersonNameText").GetComponent<Text>().text = "";
        inventoryFullMessage.SetActive(false);
        dialogue = dialoguer.GetComponent<Dialogue>();
        buttons[0].gameObject.SetActive(true);
        head.text = "Citizen";
            buttonTexts[0].text = "Bye";
        main.text = "Get Lost!";
        main.gameObject.SetActive(true);
        head.gameObject.SetActive(true);
        cameraMovement.enabled = false;
        playerController.enabled = false;
        dialogueBackground.gameObject.SetActive(true);
    }
    //Start of dialog for each person
    public void HandleDialogue()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -312, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -312, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -312, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
        permanentGUI.SetActive(false);
        dialogueIsOpen = true;
        GameObject.Find("EnemyHPPlayer").GetComponent<Image>().enabled = false;
        GameObject.Find("EnemyHPText").GetComponent<Text>().text = "";
        GameObject.Find("PersonName").GetComponent<Image>().enabled = false;
        GameObject.Find("PersonNameText").GetComponent<Text>().text = "";
        inventoryFullMessage.SetActive(false);
        playerController.newQuest.SetActive(false);
        playerController.newStage.SetActive(false);
        playerController.questCompleted.SetActive(false);
        playerController.goldWindow.SetActive(false);
        playerController.questFailed.SetActive(false);
        playerController.newLevel.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().enoughMana.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<Inventory>().needMoreSpace.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(false);
        playerController.grabWindow.SetActive(false);
        playerController.pickpocketingWindow.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<Inventory>().needMoreMoney.SetActive(false);
        playerController.needPicklock.SetActive(false);
        playerController.unlockFailed.SetActive(false);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().newStatLevel.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().killExperience.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().needToBeRobber.SetActive(false);
        //Info for simple people dialogue
        if ((dialogue.personTag == "SimplePeople")&& GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            buttons[0].gameObject.SetActive(true);
            head.text = dialogue.personName;
            buttonTexts[0].text = "Bye";
            int randomDialogue;
            while (true)
            {
                randomDialogue = Random.Range(0, GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences.Length);
                if (GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[randomDialogue] != "")
                {
                    main.text = GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[randomDialogue];
                    break;
                }
            }
        }
        //Info for village guard dialogue
        if ((dialogue.personTag == "VillageGuard") && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            buttons[0].gameObject.SetActive(true);
            head.text = dialogue.personName;
            buttonTexts[0].text = "Bye";
            int randomDialogue;
            while (true)
            {
                randomDialogue = Random.Range(0, GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences.Length);
                if (GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[randomDialogue] != "")
                {
                    main.text = GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[randomDialogue];
                    break;
                }
            }

        }
        if ((dialogue.personTag == "Republican") && GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer == false)
        {
            buttons[0].gameObject.SetActive(true);
            head.text = dialogue.personName;
            buttonTexts[0].text = "Bye";
            int randomDialogue;
            while (true)
            {
                randomDialogue = Random.Range(0, GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences.Length);
                if (GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[randomDialogue] != "")
                {
                    main.text = GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[randomDialogue];
                    break;
                }
            }
        }
        if ((dialogue.personTag == "Royalist") && GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer == false)
        {
            buttons[0].gameObject.SetActive(true);
            head.text = dialogue.personName;
            buttonTexts[0].text = "Bye";
            int randomDialogue;
            while (true)
            {
                randomDialogue = Random.Range(0, GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences.Length);
                if (GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[randomDialogue] != "")
                {
                    main.text = GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[randomDialogue];
                    break;
                }
            }

        }
        if ((dialogue.personName == "Hunter") && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            buttons[0].gameObject.SetActive(true);
            head.text = dialogue.personName;
            buttonTexts[0].text = "Bye";
            int randomDialogue;
            while (true)
            {
                randomDialogue = Random.Range(0, dialogue.sentences.Length);
                if (dialogue.sentences[randomDialogue] != "")
                {
                    main.text = dialogue.sentences[randomDialogue];
                    break;
                }
            }

        }
        main.gameObject.SetActive(true);
        cameraMovement.enabled = false;
        playerController.enabled = false;
        dialogueBackground.gameObject.SetActive(true);
        head.gameObject.SetActive(true);
        head.text = dialogue.personName;
        //Faye's dialog start
        if (dialogue.personName == "Faye"&&GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer==false)
        {
            fayeDialoguer = true;
            if (fayeHelpSaid1 == false && fayeHelpSaid2 == false && fayeTakeQuest == false)
            {
                buttonTexts[1].text = "Do you need a help?";
                buttonTexts[0].text = "Bye";
                main.text = dialogue.sentences[0];
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
            }
            //Start Faye's Dialog after taking quest
            if (fayeTakeQuest)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 1)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you know something about \"Ben's place\"?";
                }
                buttons[0].gameObject.SetActive(true);
                buttonTexts[0].text = "Bye";               
                main.text = dialogue.sentences[3];
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 8)
                    main.text = "Hi,do you want to ask something?";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "Why village is so empty?";
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "Can say me something about yourself?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "What can I find in this village?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What can I find in this valley?";
            }
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 8)
            {
                buttons[1].gameObject.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                    buttonTexts[1].text = "He is arrested";
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled)
                    buttonTexts[1].text = "He is dead";
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                    buttonTexts[1].text = "I lost him";
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Thank you for your help with the bandits. We are always happy for you";
        }
        if (dialogue.personName == "Player")
        {
            main.text = dialogue.sentences[0];
            buttonTexts[0].text = "Let's do it...";
            buttons[0].gameObject.SetActive(true);
            head.text = "Prologue";
        }
        if (dialogue.personName == "Artelit stone")
        {
            main.text = dialogue.sentences[0];
            buttons[1].gameObject.SetActive(true);
            buttonTexts[1].text = "Who are you?";
            head.text = "Artelit";
        }
        if (dialogue.personName == "Stranger")
        {
            if(!knowAboutOffer)
            main.text = "Hello.I know that you are from the guild. I am a mercenary. I was hired to eliminate the head of the bandits, but so far I am not very successful.Listen, there is a proposal. Kill the head of the village guard.I will tell the head of the bandits that you want to meet him .After that you will kill him.Return here when you are finished.He will be waiting for you here";
            if (knowAboutOffer && !headOfGuardKilled)
                main.text = "I will be waiting for you here";
            if (headOfGuardKilled && !GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Do it";
            if (!knowAboutOffer && headOfGuardKilled)
                main.text = "There is no time to explain. Wait for the bandits on the head here. Prepare for battle";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Thanks for the help. We will meet again";
            buttons[0].gameObject.SetActive(true);
            knowAboutOffer = true;
            buttonTexts[0].text = "Bye";
            head.text = "Stranger";
        }
        if (dialogue.personName == "Librarian" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                main.text = dialogue.sentences[0];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = dialogue.sentences[2];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                main.text = dialogue.sentences[1];
            buttons[0].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "Where is library?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "Which books do you have?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "You don't live here,do you?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you think about civil war?";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage&&!librarianSpecialTakeQuest)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].GetComponent<Image>().color = Color.blue;
            }
            if (librarianSurveyTakeQuest&&!GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted)
            buttonTexts[2].text = "I'm ready";
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage == 1)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "I have something interesting...";
                }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Thank you for your help with the bandits.Now I can leave the valley safely";
        }
        if (dialogue.personName == "Priest" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                main.text = dialogue.sentences[2];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = dialogue.sentences[1];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                main.text = dialogue.sentences[0];
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "What about Order?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "Сan you tell me about Bamur?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "Can you tell me about Artelit";
            buttons[0].gameObject.SetActive(true);
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin && !priestSpecialQuestTake)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponent<QuestSlot>().questStage == 2)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "I have spoken with Artelit...";
                }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "The bandits went against the will of Artelit. He destroyed them";
        }
        if (dialogue.personName == "Head of Republicans" && GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer == false)
        {
            bool simpleDialogs = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 0 || GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                    simpleDialogs = false;
            if (simpleDialogs)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                    main.text = dialogue.sentences[2];
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                    main.text = dialogue.sentences[1];
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                    main.text = dialogue.sentences[0];
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                if (!headOfRepublicansTakeQuest&&!headOfRoyalistsTakeQuest)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you need a help?";
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 4)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "I have orders";
                    }
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 2)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We did it!";
                    }
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted && !headOfRepublicansTakeSecondQuest)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "I want to join";
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted && !GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy&& GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "We need help with bandits";
                }
                if (extraWarriorsInArmy)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                            main.text = "We were happy to help you";
            }
            else
            {
                buttonTexts[0].text = "Bye";
                buttons[0].gameObject.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 0)
                    {
                        if ((dialogue.transform.position - dialogue.GetComponent<GuardAI>().startPosition).magnitude < 1)
                        {
                            main.text = "Are you ready?";
                            buttonTexts[0].text = "Give me a minute";
                            buttons[1].gameObject.SetActive(true);
                            buttonTexts[1].text = "For the Republic!";
                        }
                        else
                        {
                            main.text = "We will talk at meeting point";
                        }
                    }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                        main.text = "For the republic!";
            }
        }
        if (dialogue.personName == "Strange royalist")
        {
            bool canSpeak = false;
            if (dialogue.tag == "Royalist" && GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer == false)
                canSpeak = true;
            if (dialogue.tag == "Republican" && GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer == false)
                canSpeak = true;
            if (canSpeak)
            {
                main.text = dialogue.sentences[1];
                buttons[0].gameObject.SetActive(true);
                buttonTexts[0].text = "Bye";
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 1)
                        main.text = dialogue.sentences[2];
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 0 && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive)
                    {
                        main.text = dialogue.sentences[0];
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker = GameObject.Find("PatrolRoyalistPlace").GetComponent<QuestMarker>();
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.currentQuestMarker;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage++;
                        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage];
                        GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().royalistPatrol1.SetActive(true);
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().royalistPatrol2.SetActive(true);
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 3)
                        main.text = dialogue.sentences[3];
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                        main.text = "For the republic!";
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponent<QuestSlot>().questStage == 2)
                        main.text = "We did it!";
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 0)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "I am a mage";
                    }

                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                    main.text = "We did it!";
            }
        }
        if (dialogue.personName == "Head of Royalists" && GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer == false)
        {
            bool simpleDialogs = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 0 || GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                    simpleDialogs = false;
            if (simpleDialogs)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                    main.text = dialogue.sentences[2];
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                    main.text = dialogue.sentences[1];
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                    main.text = dialogue.sentences[0];
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                if (!headOfRepublicansTakeQuest && !headOfRoyalistsTakeQuest)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you need a help?";
                }
                if (!headOfRoyalistsTakeSecondQuest && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "I want to join";
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage < 3)
                    {
                        buttons[2].gameObject.SetActive(true);
                        buttonTexts[2].text = "I arrived on orders";
                        buttons[2].GetComponent<Image>().color = Color.red;
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage == 3)
                    {
                        buttons[2].GetComponent<Image>().color = Color.white;
                        buttons[2].gameObject.SetActive(true);
                        buttonTexts[2].text = "I arrived on orders";
                    }
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 1)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "I have information about traitor";
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 3)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "He is dead";
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                        for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                            if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                                if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                                {
                                    buttons[1].gameObject.SetActive(true);
                                    buttonTexts[1].text = "I have information about traitor";
                                }
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 2)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We did it!";
                    }
                }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted && !GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy && GameObject.Find("DialogueManager").GetComponent<DialogueManager>().knowAboutScroll)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "We need help with bandits";
                }
                if(extraWarriorsInArmy)
                    if(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
                if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                    main.text = "We were happy to help you";
            }
            else
            {
                buttonTexts[0].text = "Bye";
                buttons[0].gameObject.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 0)
                    {
                        if ((dialogue.transform.position - dialogue.GetComponent<GuardAI>().startPosition).magnitude < 1)
                        {
                            main.text = "Are you ready?";
                            buttonTexts[0].text = "Give me a minute";
                            buttons[1].gameObject.SetActive(true);
                            buttonTexts[1].text = "For the King!";
                        }
                        else
                        {
                            main.text = "We will talk at meeting point";
                        }
                    }
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") != null)
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponent<QuestSlot>().questStage == 1)
                        main.text = "For the King!";
            }
            }
        if (dialogue.personName == "Head of Village" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            bool hasMeat = false;
            List<int> indexes = new List<int>();
            int numberOfMeat = 0;
            Inventory inventory = GameObject.Find("GUIManager").GetComponent<Inventory>();
            for(int i = 0; i < inventory.images.Length; i++)
            {
                if(inventory.images[i].GetComponent<SlotInfo>().item!=null)
                if (inventory.images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Meat")
                {
                    indexes.Add(i);
                    numberOfMeat += inventory.images[i].GetComponent<SlotInfo>().amountOfItems;
                }
                if (numberOfMeat >= 10)
                {
                    hasMeat = true;
                    break;
                }
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                main.text = dialogue.sentences[1];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = dialogue.sentences[0];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                main.text = dialogue.sentences[2];
            buttons[0].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
            buttonTexts[0].text = "Bye";
            buttonTexts[3].text = "What do you think about war?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "How are things in the village going?";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "Can you tell me about bandits?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "Where I can find job?";
            if (hasMeat && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive)
                buttonTexts[6].text = "I have meat for you";
            buttons[0].gameObject.SetActive(true);
            if (!knowAboutScroll)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Do you know something about Dragon scroll?";
            }
            if (!villageGuardStageOne && knowAboutScroll)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Gather soldiers";
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Thank you for your help with the bandits. You are always welcome in our village";
        }
        if (dialogue.personName == "Head of hunters" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            main.text = dialogue.sentences[0];
            buttons[0].gameObject.SetActive(true);
            if (!headOfHuntersTakeQuest)
            {
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].gameObject.SetActive(true);
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponent<QuestSlot>().questStage == 1)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "I have mushroom";
                }
            buttons[3].gameObject.SetActive(true);
            buttonTexts[0].text = "Bye";
            buttonTexts[3].text = "Show your goods";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "Who are you?";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "What can I find in forest?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you think about civil war?";
            buttons[0].gameObject.SetActive(true);
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Thank you for your help with the bandits.We took revenge on Brad";
        }
        if (dialogue.personName == "Head of Guard" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                main.text = dialogue.sentences[1];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = dialogue.sentences[0];
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                main.text = dialogue.sentences[2];
            buttonTexts[0].text = "Bye";
            if (!headOfGuardTakeQuest&&!villageGuardStageOne)
            {
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].gameObject.SetActive(true);
            }
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage == 0)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "I want to talk about royalists...";
                }            
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest") != null)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage==1) 
                {
                    buttonTexts[1].text = "Well,we did it";
                    buttons[1].gameObject.SetActive(true);
                }
            }
            buttons[0].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "Who is leading in war now?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "Tell me about course of war";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "What is this tower?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "Who do you support?";
            buttons[0].gameObject.SetActive(true);
            if (villageGuardStageOne && (GameObject.Find("HeadOfGuard").transform.position - GameObject.Find("HeadOfGuard").GetComponent<GuardAI>().startPosition).magnitude < 2 && !armyStageTwo)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "For the village!";
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
                main.text = "Good work. Now we can relax";
        }
        if (dialogue.personName == "Bob" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            buttons[0].gameObject.SetActive(true);
            buttonTexts[1].text = "I am looking for betrayer";
            buttonTexts[0].text = "Bye";
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 3)
            {
                main.text = dialogue.sentences[0];
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
            }
            else if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 4)
            {
                main.text = dialogue.sentences[3];
                buttons[0].gameObject.SetActive(true);
            }
            else if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage > 6)
            { 
                if(GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                    main.text = dialogue.sentences[6];
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled)
                    main.text = dialogue.sentences[4];
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                    main.text = dialogue.sentences[5];
            }
        }
        if (dialogue.personName == "Solovey" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage != 8)
            {
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested && !GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                {
                    buttonTexts[1].text = "You are under arrest,Solovey!";
                    buttonTexts[0].text = "Bye";
                    main.text = dialogue.sentences[0];
                    buttons[0].gameObject.SetActive(true);
                    buttons[1].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(true);
                    buttons[2].gameObject.SetActive(true);
                    buttonTexts[3].text = "You are already dead";
                    buttonTexts[2].text = "I won't arrest you,go";
                }
                else if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                {
                    buttonTexts[0].text = "Bye";
                    main.text = "Let's end with this";
                    buttons[0].gameObject.SetActive(true);
                }
                else if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                {
                    buttonTexts[0].text = "Bye";
                    main.text = "I will never forget this!";
                    buttons[0].gameObject.SetActive(true);
                }
            }
            else
            {
                buttonTexts[0].text = "Bye";
                buttons[0].gameObject.SetActive(true);
                main.text = dialogue.sentences[4];
            }
        }
        if (dialogue.tag == "Book")
        {
            buttonTexts[0].text = "Bye";
            main.text = dialogue.sentences[0];
            buttons[0].gameObject.SetActive(true);
            if(dialogue.sentences.Length>1)
            buttons[4].gameObject.SetActive(true);
        }
        if (dialogue.personName== "Epilogue")
        {
            if(GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin|| GameObject.Find("GameManager").GetComponent<GameManager>().isMage|| GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                if(GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
            dialogue.GetComponent<Dialogue>().sentences[0] = "After the disappearance of the dragon scroll, the head of the bandits became angry and decided that the villagers had done it.The village was destroyed";
            if(!GameObject.Find("GameManager").GetComponent<GameManager>().headOfBanditKilled)
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
                dialogue.GetComponent<Dialogue>().sentences[1] = "Your help strongly helped royalists in war. In a month the capital of Republicans was captured, and all leadership of the Republic was executed. In the country the new epoch of \"strong hand\" began.";
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                dialogue.GetComponent<Dialogue>().sentences[1] = "Your help greatly helped the Republicans in the war. A month later the capital of the Royalists was captured, and the regent was arrested. The President-Marshal compromised with the democratic royal forces and adopted the Constitution of the Monarchical Republic";
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted && !GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
                dialogue.GetComponent<Dialogue>().sentences[1] = "No one was able to take control of the valley. After 3 months, the forces of both sides were completely exhausted. At the same time, the Rosenblaum Empire invaded the country.The country is occupied...";
            dialogue.GetComponent<Dialogue>().sentences[2] = "Your journey is just beginning";
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                dialogue.GetComponent<Dialogue>().sentences[3] = "Solovey was executed the day after you left";
            if(GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled)
                dialogue.GetComponent<Dialogue>().sentences[3] = "Solovey was killed by you. Nobody remembers it any more ...";
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                dialogue.GetComponent<Dialogue>().sentences[3] = "Solovey was killed during an attack on the village";
            if(!GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway&&!GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled&&!GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                dialogue.GetComponent<Dialogue>().sentences[3] = "The traitor was never found";
            if (soloveySisterHelped)
                dialogue.GetComponent<Dialogue>().sentences[4] = "Solovey's sister recovered and moved to the Federation";
            if (!soloveySisterHelped)
                dialogue.GetComponent<Dialogue>().sentences[4] = "Solovey's sister died of an illness";
            main.text = dialogue.sentences[0];
            if (dialogue.sentences.Length > 1)
                buttons[4].gameObject.SetActive(true);
        }
        if (dialogue.personName == "Prison guard")
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 7)
            {
                buttonTexts[0].text = "Bye";
                main.text = dialogue.sentences[0];
                buttons[0].gameObject.SetActive(true);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
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
                GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                GameObject.Find("Cage 1").transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("Faye").GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
            }
            else
            {
                buttonTexts[0].text = "Bye";
                main.text = "Last prisoner was Antonio Zaporebrikoviy.He died 5 years ago";
                buttons[0].gameObject.SetActive(true);
            }
        }
        if (dialogue.personName == "Merchant" && GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer == false)
        {
                buttonTexts[1].text = "What is happening in the world?";
                buttonTexts[0].text = "Bye";
                buttonTexts[3].text = "Show your goods";
                main.text = dialogue.sentences[0];
            if (rewardFromSister)
            {
                playerController.skillPoints++;
                main.text = "Solovey's sister asked to give you this.This is book,which can help to improve your skills.Sister feels much better.";
                rewardFromSister = false;
            }
                buttons[3].gameObject.SetActive(true);
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
        }
    }
    public void LibrarianAboutHer()
    {
        if (dialogue.personName == "Librarian" && buttonTexts[5].text == "You don't live here,do you?")
            main.text = dialogue.sentences[5];
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "Eternal Federation")
        {
            main.text = "When liberalization in Endless Empire started?";
            buttonTexts[2].text = "402";
            buttonTexts[3].text = "452";
            buttonTexts[5].text = "453";
            buttonTexts[6].text = "356";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "453")
        {
            main.text = "Consequences of liberalization?";
            buttonTexts[2].text = "Continental Crisis";
            buttonTexts[3].text = "Creation of the Eternal Federation";
            buttonTexts[5].text = "Rising standard of living";
            buttonTexts[6].text = "Strengthening national liberation movements";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "Rising standard of living")
        {
            main.text = "First king of Lovania";
            buttonTexts[2].text = "Ludwig The Great";
            buttonTexts[3].text = "Bill Rosenblaum";
            buttonTexts[5].text = "Wilhelm The Second";
            buttonTexts[6].text = "Jacob von DeLune";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "Wilhelm The Second")
        {
            main.text = "When the First Constitution was adopted";
            buttonTexts[2].text = "887";
            buttonTexts[3].text = "1072";
            buttonTexts[5].text = "768";
            buttonTexts[6].text = "1073";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "768")
        {
            surveyCorrectAnswers++;
            main.text = "When the Second Constitution was adopted";
            buttonTexts[2].text = "1211";
            buttonTexts[3].text = "1072";
            buttonTexts[5].text = "1073";
            buttonTexts[6].text = "1189";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "1073")
        {
            surveyCorrectAnswers++;
            main.text = "Causes of the December Revolution";
            buttonTexts[2].text = "Low standard of living. Striving for democracy";
            buttonTexts[3].text = "Dissatisfaction of the nobility";
            buttonTexts[5].text = "Influence of the Federation";
            buttonTexts[6].text = "Offensive on the Order";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "Influence of the Federation")
        {
            main.text = "Who declared war on Lovania after the December Revolution";
            buttonTexts[2].text = "The Rosenblaum Empire";
            buttonTexts[3].text = "The Federation";
            buttonTexts[5].text = "South Triumvirate";
            buttonTexts[6].text = "Republic of Artholid";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "South Triumvirate")
        {
            main.text = "Prime Minister of the Provisional Government";
            buttonTexts[2].text = "Augusto Pinochet";
            buttonTexts[3].text = "Vladimir Chikenko";
            buttonTexts[5].text = "Harry Chacril";
            buttonTexts[6].text = "Myhailo Hrushevskiy";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "Harry Chacril")
        {
            main.text = "Consequences of the December Revolution";
            buttonTexts[2].text = "The beginning of the revival of Lavania";
            buttonTexts[3].text = "Loss of sovereignty";
            buttonTexts[5].text = "Proclamation of the Republic";
            buttonTexts[6].text = "Loss of territories";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[5].text == "Proclamation of the Republic")
            LibrarianSurveyFinishQuest();
    }
    public void LibrarianAboutWar()
    {
        if (dialogue.personName == "Librarian"&&buttonTexts[6].text== "What do you think about civil war?")
            main.text = dialogue.sentences[6];
        else if(dialogue.personName=="Librarian"&&buttonTexts[6].text=="Star Republic")
        {
            main.text = "When liberalization in Endless Empire started?";
            buttonTexts[2].text = "402";
            buttonTexts[3].text = "452";
            buttonTexts[5].text = "453";
            buttonTexts[6].text = "356";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "356")
        {
            main.text = "Consequences of liberalization?";
            buttonTexts[2].text = "Continental Crisis";
            buttonTexts[3].text = "Creation of the Eternal Federation";
            buttonTexts[5].text = "Rising standard of living";
            buttonTexts[6].text = "Strengthening national liberation movements";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "Strengthening national liberation movements")
        {
            surveyCorrectAnswers++;
            main.text = "First king of Lovania";
            buttonTexts[2].text = "Ludwig The Great";
            buttonTexts[3].text = "Bill Rosenblaum";
            buttonTexts[5].text = "Wilhelm The Second";
            buttonTexts[6].text = "Jacob von DeLune";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "Jacob von DeLune")
        {
            surveyCorrectAnswers++;
            main.text = "When the First Constitution was adopted";
            buttonTexts[2].text = "887";
            buttonTexts[3].text = "1072";
            buttonTexts[5].text = "768";
            buttonTexts[6].text = "1073";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "1073")
        {
            main.text = "When the Second Constitution was adopted";
            buttonTexts[2].text = "1211";
            buttonTexts[3].text = "1072";
            buttonTexts[5].text = "1073";
            buttonTexts[6].text = "1189";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "1189")
        {
            main.text = "Causes of the December Revolution";
            buttonTexts[2].text = "Low standard of living. Striving for democracy";
            buttonTexts[3].text = "Dissatisfaction of the nobility";
            buttonTexts[5].text = "Influence of the Federation";
            buttonTexts[6].text = "Offensive on the Order";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "Offensive on the Order")
        {
            main.text = "Who declared war on Lovania after the December Revolution";
            buttonTexts[2].text = "The Rosenblaum Empire";
            buttonTexts[3].text = "The Federation";
            buttonTexts[5].text = "South Triumvirate";
            buttonTexts[6].text = "Republic of Artholid";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "Republic of Artholid")
        {
            main.text = "Prime Minister of the Provisional Government";
            buttonTexts[2].text = "Augusto Pinochet";
            buttonTexts[3].text = "Vladimir Chikenko";
            buttonTexts[5].text = "Harry Chacril";
            buttonTexts[6].text = "Myhailo Hrushevskiy";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "Myhailo Hrushevskiy")
        {
            main.text = "Consequences of the December Revolution";
            buttonTexts[2].text = "The beginning of the revival of Lavania";
            buttonTexts[3].text = "Loss of sovereignty";
            buttonTexts[5].text = "Proclamation of the Republic";
            buttonTexts[6].text = "Loss of territories";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[6].text == "Loss of territories")
            LibrarianSurveyFinishQuest();
    }
    public void LibrarianBackFromSurvey()
    {
        if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Sorry,but I can't pass it now")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = dialogue.sentences[7];
            buttonTexts[0].text = "Bye";
            buttons[1].gameObject.SetActive(false);
            buttonTexts[2].text = "Which books do you have?";
            buttonTexts[3].text = "Where is library?";
            buttons[3].gameObject.SetActive(true);
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
            if (!librarianSpecialTakeQuest && GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].GetComponent<Image>().color=Color.blue;
            }
            if (librarianSurveyTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted)
                buttonTexts[2].text = "I'm ready";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Which books do you have?")
        {
            if (!librarianSurveyTakeQuest)
            {
                buttons[1].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[4];
                buttonTexts[0].text = "Bye";
                buttonTexts[1].text = "I will answer on the survey";
                buttonTexts[2].text = "Sorry,but I can't pass it now";
                buttons[3].gameObject.SetActive(true);
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                {
                    buttonTexts[3].text = "I will pass it,but not for free";
                    buttons[3].GetComponent<Image>().color = Color.green;
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                {
                    buttonTexts[3].text = "I will do my best";
                    buttons[3].GetComponent<Image>().color = Color.red;
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                {
                    buttons[3].GetComponent<Image>().color = Color.blue;
                    buttonTexts[3].text = "I will pass it,but I need knowledge";
                }
                buttons[5].gameObject.SetActive(false);
                buttons[6].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(true);
            }
            else
            {
                main.text = "Did you pass the test without repetition?Here is a book on the history of Lavania and the history of Artholis. There is also a text about the gods and our valley.";
            }
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "I'm ready")
        {
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            main.text = "What was the name of the first state on Artholida?";
            buttonTexts[2].text = "Star Empire";
            buttonTexts[3].text = "Eternal Empire";
            buttonTexts[5].text = "Eternal Federation";
            buttonTexts[6].text = "Star Republic";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Star Empire")
        {
            main.text = "When liberalization in Endless Empire started?";
            buttonTexts[2].text = "402";
            buttonTexts[3].text = "452";
            buttonTexts[5].text = "453";
            buttonTexts[6].text = "356";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "402")
        {
            surveyCorrectAnswers++;
            main.text = "Consequences of liberalization?";
            buttonTexts[2].text = "Continental Crisis";
            buttonTexts[3].text = "Creation of the Eternal Federation";
            buttonTexts[5].text = "Rising standard of living";
            buttonTexts[6].text = "Strengthening national liberation movements";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Continental Crisis")
        {
            main.text = "First king of Lovania";
            buttonTexts[2].text = "Ludwig The Great";
            buttonTexts[3].text = "Bill Rosenblaum";
            buttonTexts[5].text = "Wilhelm The Second";
            buttonTexts[6].text = "Jacob von DeLune";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Ludwig The Great")
        {
            main.text = "When the First Constitution was adopted";
            buttonTexts[2].text = "887";
            buttonTexts[3].text = "1072";
            buttonTexts[5].text = "768";
            buttonTexts[6].text = "1073";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "887")
        {
            main.text = "When the Second Constitution was adopted";
            surveyCorrectAnswers++;
            buttonTexts[2].text = "1211";
            buttonTexts[3].text = "1072";
            buttonTexts[5].text = "1073";
            buttonTexts[6].text = "1189";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "1211")
        {
            main.text = "Causes of the December Revolution";
            buttonTexts[2].text = "Low standard of living. Striving for democracy";
            buttonTexts[3].text = "Dissatisfaction of the nobility";
            buttonTexts[5].text = "Influence of the Federation";
            buttonTexts[6].text = "Offensive on the Order";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Low standard of living. Striving for democracy")
        {
            surveyCorrectAnswers++;
            main.text = "Who declared war on Lovania after the December Revolution";
            buttonTexts[2].text = "The Rosenblaum Empire";
            buttonTexts[3].text = "The Federation";
            buttonTexts[5].text = "South Triumvirate";
            buttonTexts[6].text = "Republic of Artholid";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "The Rosenblaum Empire")
        {
            surveyCorrectAnswers++;
            main.text = "Prime Minister of the Provisional Government";
            buttonTexts[2].text = "Augusto Pinochet";
            buttonTexts[3].text = "Vladimir Chikenko";
            buttonTexts[5].text = "Harry Chacril";
            buttonTexts[6].text = "Myhailo Hrushevskiy";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "Augusto Pinochet")
        {
            main.text = "Consequences of the December Revolution";
            buttonTexts[2].text = "The beginning of the revival of Lavania";
            buttonTexts[3].text = "Loss of sovereignty";
            buttonTexts[5].text = "Proclamation of the Republic";
            buttonTexts[6].text = "Loss of territories";
        }
        else if (dialogue.personName == "Librarian" && buttonTexts[2].text == "The beginning of the revival of Lavania")
        {
            surveyCorrectAnswers++;
            LibrarianSurveyFinishQuest();
        }
    }
    public void PriestAboutBamur()
    {
        if (dialogue.personName == "Priest")
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = dialogue.sentences[7];
            else
                main.text = dialogue.sentences[5];
        }
    }
    public void PriestAboutArtelit()
    {
        if (dialogue.personName == "Priest")
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = dialogue.sentences[6];
            else
                main.text = dialogue.sentences[8];
        }
    }
    public void PriestAboutWar()
    {
        if (dialogue.personName == "Priest")
                main.text = dialogue.sentences[3];
    }
    public void PriestAboutOrder()
    {
        if (dialogue.personName == "Priest"&&buttonTexts[2].text=="What about Order?")
            main.text = dialogue.sentences[4];
        else if(dialogue.personName == "Priest" && buttonTexts[2].text == "I will think about it")
        {
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "What about Order?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "Сan you tell me about Bamur?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "Can you tell me about Artelit";
            buttons[0].gameObject.SetActive(true);
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin && !priestSpecialQuestTake)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].GetComponent<Image>().color = Color.red;
            }
            main.text = "Come back if you change your mind.It's your duty";
        }
    }
    public void HeadOfHuntersAboutCivilWar()
    {
        if(buttonTexts[6].text== "What do you think about civil war?" && dialogue.personName== "Head of hunters")
        {
            main.text = dialogue.sentences[3];
        }
    } 
    public void HeadOfRepublicansWarGoing()
    {
        if (buttonTexts[5].text == "How's the war going?" && dialogue.personName == "Head of Republicans")
        {
            main.text = dialogue.sentences[4];
        }
    }
    public void HeadOfRepublicansAboutCountry()
    {
        if (buttonTexts[6].text == "What do you see the country after the war?" && dialogue.personName == "Head of Republicans")
        {
            main.text = dialogue.sentences[5];
        }
    }
    public void HeadOfRepublicansThinkProposal()
    {
        if (buttonTexts[2].text == "I will think about your proposal" && dialogue.personName == "Head of Republicans")
        {
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            buttonTexts[1].text = "Do you need a help?";
            buttonTexts[3].text = "What do you think about war?";
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = "Unfortunately. Come back if you change your mind";
        }
       else if (buttonTexts[2].text == "Maybe,later" && dialogue.personName == "Head of Republicans")
        {
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            buttonTexts[1].text = "I want to join";
            buttonTexts[3].text = "What do you think about war?";
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = "If you change your mind, come here. We don't have a lot of time";
        }
    }
    public void HeadOfRoyalistsBackFromQuest()
    {
        if (buttonTexts[2].text == "I will think about your proposal" && dialogue.personName == "Head of Royalists")
        {
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            if (!headOfRoyalistsTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
                buttonTexts[1].text = "Do you need a help?";
            else
               if (!headOfRoyalistsTakeSecondQuest)
                buttonTexts[1].text = "I want to join";
            buttonTexts[3].text = "What do you think about war?";
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = "Unfortunately. Come back if you change your mind";
        }
    }
    public void StrangeRoyalistMage()
    {
        if (buttonTexts[1].text == "I am a mage" && dialogue.personName == "Strange royalist")
        {
            main.text = "Magician? So are you with us? Go to our camp and join us when we storm the royalists. I will be waiting for you here";
            buttons[1].gameObject.SetActive(false);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsQuestStageOne();
        }
    }
    public void HeadOfRoyalistsWarGoing()
    {
        if (buttonTexts[5].text == "How's the war going?" && dialogue.personName == "Head of Royalists")
        {
            main.text = dialogue.sentences[4];
        }
    }
    public void HeadOfRoyalistsAboutCountry()
    {
        if (buttonTexts[6].text == "What do you see the country after the war?" && dialogue.personName == "Head of Royalists")
        {
            main.text = dialogue.sentences[5];
        }
    }
    public void HeadOfRoyalistsOnOrders()
    {
        if (buttonTexts[2].text == "I arrived on orders" && dialogue.personName == "Head of Royalists")
        {
            main.text = "Yes, of course. At night, our troops must destroy the Republican reconnaissance groups in the valley. After that, our agents in the main Republican camp will carry out a sabotage. While they are engaged in sabotage. Our troops will approach their camp and start storming.Pass it on to others to track down and destroy reconnaissance teams";
            if (buttons[2].GetComponent<Image>().color == Color.red)
                main.text = "Paladin? I thought it was just a rumor. But it's a great disguise, because no one will interfere with the paladin. At night, our troops must destroy the Republican reconnaissance groups in the valley. After that, our agents in the main Republican camp will carry out a sabotage. While they are engaged in sabotage. Our troops will approach their camp and start storming.Pass it on to others to track down and destroy reconnaissance teams";
                buttons[2].GetComponent<Image>().color = Color.white;
            buttons[2].gameObject.SetActive(false);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansQuestStageFour();
        }
    }
    public void HeadOfHuntersAboutForest()
    {
        if (buttonTexts[5].text == "What can I find in forest?" && dialogue.personName == "Head of hunters")
        {
            main.text = dialogue.sentences[2];
        }
    }
    public void HeadOfHuntersAboutHimself()
    {
        if (buttonTexts[2].text == "Who are you?" && dialogue.personName == "Head of hunters")
        {
            main.text = dialogue.sentences[1];
        }
        else if(buttonTexts[2].text == "Sorry,but I can't help you" && dialogue.personName == "Head of hunters")
        {
            main.text = "Sad.Tell me if you change your mind";
            buttonTexts[2].text = "Who are you?";
            buttonTexts[5].text = "What can I find in the forest";
            buttonTexts[6].text = "What do you think about civil war?";
            buttonTexts[3].text = "Show your goods";
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
        }
    }
    public void HeadOfVillageAboutBandits()
    {
        if (dialogue.personName == "Head of Village")
            main.text = dialogue.sentences[5];
    }
    public void HeadOfVillageAboutJob()
    {
        if (dialogue.personName == "Head of Village" && buttonTexts[6].text == "Where I can find job?")
        {
            if (!headOfVillageTakeQuest)
            {
                buttons[5].gameObject.SetActive(false);
                buttons[6].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "I will help you";
                buttonTexts[2].text = "Sorry,but I can't help you now";
                buttons[2].gameObject.SetActive(true);
                buttons[3].gameObject.SetActive(true);
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                {
                    buttons[3].GetComponent<Image>().color = Color.blue;
                    buttonTexts[3].text = "I will help you,but I need knowledge";
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                {
                    buttons[3].GetComponent<Image>().color = Color.green;
                    buttonTexts[3].text = "I will help you,if you pay well";
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                {
                    buttons[3].GetComponent<Image>().color = Color.red;
                    buttonTexts[3].text = "I will do my best";
                }
                main.text = dialogue.sentences[6];
            }
            else
                main.text = "Ask hunters and just villagers. I'm sure someone will find a job for you. Contact the head of the guard, the priest, the head of the hunters and the librarian. Also ask ordinary people.";
        }
        else if (dialogue.personName == "Head of Village" && buttonTexts[6].text == "I have meat for you")
            HeadOfVillageFinishQuest();
    }
    public void HeadOfVillageAboutVillage()
    {
        if (dialogue.personName == "Head of Village" && buttonTexts[2].text == "How are things in the village going?")
            main.text = dialogue.sentences[3];
        else if (dialogue.personName == "Head of Village" && buttonTexts[2].text == "Sorry,but I can't help you now")
        {
            main.text = "Sad.Anything else?";
            buttons[1].gameObject.SetActive(false);
            buttonTexts[3].text = "What do you think about war?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "How are things in the village going?";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "Can you tell me about bandits?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "Where I can find job?";
            buttons[3].GetComponent<Image>().color = Color.white;
        }
    }
    public void HeadOfGuardAboutWar()
    {
        if (dialogue.personName == "Head of Guard"&&buttonTexts[3].text== "Who is leading in war now?")
            main.text = dialogue.sentences[4];
    }
    public void HeadOfGuardAboutCourse()
    {
        if (dialogue.personName == "Head of Guard"&&buttonTexts[2].text== "Tell me about course of war")
            main.text = dialogue.sentences[6];
    }
    public void HeadOfGuardAboutTower()
    {
        if (dialogue.personName == "Head of Guard")
            main.text = dialogue.sentences[5];
    }
    public void HeadOfGuardAboutSupport()
    {
        if (dialogue.personName == "Head of Guard")
            main.text = dialogue.sentences[3];
    }
    public void HeadOfGuardBackFromNeedHelp()
    {
        if (dialogue.personName == "Head of Guard" && buttonTexts[2].text == "Sorry,but I can't help you now")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = dialogue.sentences[8];
            buttonTexts[0].text = "Bye";
            buttonTexts[1].text = "Do you need a help?";
            buttonTexts[2].text = "Tell me about course of war";
            buttonTexts[3].text = "Who is leading in war now?";
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
        }
    }
    public void HeadOfGuardLetsDoThis()
    {
        if (dialogue.personName == "Head of Guard" && buttonTexts[1].text == "Let's do this")
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfGuardStageOne();
    }
    public void Continue()
    {
        if (dialogue.tag == "Book")
        {
            for (int i = 0; i < dialogue.sentences.Length; i++)
            {
                if (main.text == dialogue.sentences[i])
                {
                    main.text = dialogue.sentences[i + 1];
                    if (i + 2 == dialogue.sentences.Length)
                        buttons[4].gameObject.SetActive(false);
                    break;
                }
            }
            buttons[7].gameObject.SetActive(true);
        }
        if (dialogue.personName == "Epilogue")
        {
            for (int i = 0; i < dialogue.sentences.Length; i++)
            {
                if (main.text == dialogue.sentences[i])
                {
                    main.text = dialogue.sentences[i + 1];
                    if (i + 2 == dialogue.sentences.Length)
                    {
                        finishGame.SetActive(true);
                        buttons[4].gameObject.SetActive(false);
                    }
                    break;
                }
            }
        }
    }
    public void FinishGame()
    {
        GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_complete);
        if(GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_masterrobber);
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_archimage);
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_the_great_paladin);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void Previous()
    {
        if (dialogue.tag == "Book")
        {
            for (int i = 0; i < dialogue.sentences.Length; i++)
            {
                if (main.text == dialogue.sentences[i])
                {
                    main.text = dialogue.sentences[i - 1];
                    if (i - 2 == -1)
                        buttons[7].gameObject.SetActive(false);
                    break;
                }
            }
            buttons[4].gameObject.SetActive(true);
        }
        if (dialogue.personName == "Epilogue")
        {
            for (int i = 0; i < dialogue.sentences.Length; i++)
            {
                if (main.text == dialogue.sentences[i])
                {
                    main.text = dialogue.sentences[i - 1];
                    if (i - 2 == -1)
                        buttons[7].gameObject.SetActive(false);
                    break;
                }
            }
            buttons[4].gameObject.SetActive(true);
        }
    }
    //Close market and back to dialogue
    public void CloseMarket()
    {
        GameObject.Find("GUIManager").GetComponent<GUIController>().dialogueUI.SetActive(true);
        GameObject.Find("GUIManager").GetComponent<GUIController>().shop.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().itemInfoInventory.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Buy").gameObject.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Back").gameObject.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem.GetComponent<Image>().color = Color.white;
        GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem = GameObject.Find("GUIManager").GetComponent<GUIController>().timeImageGameObject.gameObject;
        GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Use").gameObject.SetActive(true);
        GameObject.Find("GUIManager").GetComponent<GUIController>().inventoryUI.transform.Find("Drop").gameObject.SetActive(true);
        GameObject.Find("GUIManager").GetComponent<GUIController>().thiefShop.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().paladinShop.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().mageShop.SetActive(false);
        GameObject.Find("GUIManager").GetComponent<GUIController>().hunterShop.SetActive(false);
    }
    //Merchant.What is happening in the world script
    public void MerchantWhatIsHappening()
    {
        if (dialogue.personName == "Merchant")
        {
            int randomDialogue = 0;
            while (true)
            {
                 randomDialogue = Random.Range(1, dialogue.sentences.Length);
                if (dialogue.sentences[randomDialogue] != "")
                    break;
            }
            main.text = dialogue.sentences[randomDialogue];
        }
    }
    //Faye finish quest
    public void FayeFinishQuest()
    {
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
            if (buttonTexts[1].text == "He is arrested" || buttonTexts[1].text == "He is dead"||buttonTexts[1].text== "I lost him")
            if (dialogue.name == "Faye" && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 8 && GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive)
        {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
            {
                buttons[1].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttonTexts[2].text = "Yes";
                buttonTexts[3].text = "No";
                buttons[2].gameObject.SetActive(true);
                    buttons[6].gameObject.SetActive(false);
                    fayeYesNoAnswers = true;
                buttons[3].gameObject.SetActive(true);
                main.text = "What?Sorry,but I can't give you reward for this job.It is a huge lose for us.However,we are collection money for treatment for Solovey's sister.Will you give 50 coins?";
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled)
            {
                buttons[1].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttonTexts[2].text = "Yes";
                buttonTexts[3].text = "No";
                fayeYesNoAnswers = true;
                buttons[2].gameObject.SetActive(true);
                    buttons[6].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(true);
                main.text = "It is sad,but he deserved this.However,we are collection money for treatment for Solovey's sister.Will you give 50 coins?";
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
            {
                buttons[1].gameObject.SetActive(false);
                buttons[4].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttonTexts[2].text = "Yes";
                buttonTexts[3].text = "No";
                fayeYesNoAnswers = true;
                buttons[2].gameObject.SetActive(true);
                    buttons[6].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(true);
                main.text = "Thanks for your service.We are collection money for treatment for Solovey's sister.Will you give 50 coins?";
            }
        }
    }
    public void HeadOfGuardFinishQuest()
    {
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest") != null)
            if (buttonTexts[1].text == "Well,we did it")
                if (dialogue.personName == "Head of Guard" && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponent<QuestSlot>().questStage == 1 && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive)
                {
                        bool havePlace = false;
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward == null)
                            havePlace = true;
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward != null)
                        {
                            havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward);
                        }
                        if (havePlace)
                        {
                        GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_kill_bandits);
                        main.text = "Of course, here is your reward. Do you want to know something else?";
                        buttons[1].gameObject.SetActive(false);
                            playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Kill bandits";
                            playerController.StartCoroutine("QuestCompleted");
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").GetComponentInChildren<Image>().color == Color.red)
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                        Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfGuardQuest").gameObject);
                            playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.skillPoints;
                            playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questCompleted = true;
                        playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.experienceReward;
                            playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive = false;
                            GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                        GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[11] = "Thanks for helping with that bandit squad in the valley";
                        GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[11] = "It's good that you defended our tower. It offers a beautiful view";
                        if (GameObject.Find("Village Merchant") != null)
                            GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[9] = "Thank you for destroying those bandits. Now the roads are safer";
                    }
                        else
                            main.text = "You don't have place in inventory for reward";                           
                }
    }
    private void HeadOfVillageFinishQuest()
    {
        if (dialogue.personName == "Head of Village" && buttonTexts[6].text == "I have meat for you" && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive)
        {
            buttons[0].gameObject.SetActive(true);
            buttonTexts[0].text = "Bye";
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "How are things in the village going?";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "Can you tell me about bandits?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "Where I can find job?";
            main.text = "Thanks for your help!This will help us in winter";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_supply);
                bool hasMeat = false;
                List<int> indexes = new List<int>();
                int numberOfMeat = 0;
                Inventory inventory = GameObject.Find("GUIManager").GetComponent<Inventory>();
                for (int i = 0; i < inventory.images.Length; i++)
                {
                    if (inventory.images[i].GetComponent<SlotInfo>().item != null)
                        if (inventory.images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Meat")
                        {
                            indexes.Add(i);
                            numberOfMeat += inventory.images[i].GetComponent<SlotInfo>().amountOfItems;
                        }
                    if (numberOfMeat >= 10)
                    {
                        hasMeat = true;
                        break;
                    }
                }
                if (hasMeat)
                {
                    int deltaMeat = 10;
                    foreach (int index in indexes)
                    {
                        while (inventory.images[index].GetComponent<SlotInfo>().amountOfItems > 0)
                            if (deltaMeat > 0)
                            {
                                inventory.images[index].GetComponent<SlotInfo>().amountOfItems--;
                                deltaMeat--;
                                if (inventory.images[index].GetComponent<SlotInfo>().amountOfItems == 0)
                                {
                                    inventory.images[index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                    inventory.images[index].GetComponent<Image>().sprite = null;
                                    inventory.images[index].GetComponent<SlotInfo>().amountOfItems = 0;
                                    inventory.images[index].GetComponent<SlotInfo>().item = null;
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                                    inventory.images[index].GetComponentInChildren<Text>().text = "";
                                }
                            }
                            else
                                break;
                    }
                }
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Supply";
                playerController.StartCoroutine("QuestCompleted");
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").GetComponentInChildren<Image>().color == Color.red)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfVillageQuest").gameObject);
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[12] = "Thank you for helping with the food. Maybe we will survive this year ...";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[12] = "Thank you for the food. We will be able to fight the bandits much longer";
                if(GameObject.Find("Village Merchant")!=null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[12] = "Thanks to you, these people will be able to survive the winter";
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
    }
    private void HeadOfHuntersFinishQuest()
    {
        if (dialogue.personName == "Head of hunters" && buttonTexts[1].text == "I have mushroom" && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive)
        {
            buttons[1].gameObject.SetActive(false);
            main.text = "Thank you! With this we will be able to survive the winter. Here is your reward";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_artelit__mushroom);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                {
                    if(GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item!=null)
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName== "Artelis mushroom")
                        {
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                            break;
                        }
                }
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Find mushroom";
                playerController.StartCoroutine("QuestCompleted");
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").GetComponentInChildren<Image>().color == Color.red)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfHuntersQuest").gameObject);
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("Hunter").GetComponent<Dialogue>().sentences[3] = "Thank you for your help. The mushroom will help you survive the winter";
                if (GameObject.Find("Village Merchant") != null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[13] = "Artelit mushroom is a very valuable thing. And you could just take it to yourself ...";
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
    }
    private void LibrarianSurveyFinishQuest()
    {
        buttons[0].gameObject.SetActive(true);
        buttonTexts[0].text = "Bye";
        buttons[3].gameObject.SetActive(true);
        buttonTexts[3].text = "Where is library?";
        buttons[2].gameObject.SetActive(true);
        buttonTexts[2].text = "Which books do you have?";
        buttons[5].gameObject.SetActive(true);
        buttonTexts[5].text = "You don't live here,do you?";
        buttons[6].gameObject.SetActive(true);
        buttonTexts[6].text = "What do you think about civil war?";
        if (!librarianSpecialTakeQuest && GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
        {
            buttons[1].gameObject.SetActive(true);
            buttonTexts[1].text = "Do you need a help?";
            buttons[1].GetComponent<Image>().color = Color.blue;
        }
        if (surveyCorrectAnswers < 5)
            main.text = "You have " + surveyCorrectAnswers + " correct answers.Sorry, but that's not enough to reward";
        if (surveyCorrectAnswers >= 5 && surveyCorrectAnswers < 10)
        {
            main.text = "You have " + surveyCorrectAnswers + " correct answers.Sorry, but that's not enough to reward";
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward = 10;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 100;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward = 250;
        }
        if (surveyCorrectAnswers == 10)
        {
            main.text = "You have " + surveyCorrectAnswers + " correct answers.Here is your reward";
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward = 15;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 200;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward = 500;
        }
        if (librarianSurveyQuestMageChoosed)
        {
            main.text = "You have " + surveyCorrectAnswers + " correct answers. This is the best result! Here is your reward";          
            if (surveyCorrectAnswers >= 5)
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.skillPoints = 1;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 0;
        }
        if (librarianSurveyQuestRobberChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward *= 2;
        }
        if (librarianSurveyQuestPaladinChoosed)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward *= 2;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward = 0;
        }
        bool havePlace = false;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward == null)
                        havePlace = true;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward != null)
                    {
                        havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward);
                    }
                    if (havePlace)
                    {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_survey);
            playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Survey";
                        playerController.StartCoroutine("QuestCompleted");
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").GetComponentInChildren<Image>().color == Color.red)
                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
            Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSurveyQuest").gameObject);
                        playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.skillPoints;
                        playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted = true;
                        playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward;
                        playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive = false;
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    }
                    else
                        main.text = "You don't have place in inventory for reward";
    }
    public void FayeHelpSisterYes()
    {
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
            if (buttonTexts[2].text == "Yes")
            {
                if (dialogue.name == "Faye" && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 8 && GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive && fayeYesNoAnswers)
                {
                    bool haveMoney = false;
                    if ((GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled || GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)&&!fayeQuestPaladinChoosed)                
                        haveMoney = true;
                    else if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway||fayeQuestPaladinChoosed)
                        if (playerController.gold >= 50)
                            haveMoney = true;
                    if (haveMoney)
                    {
                        bool havePlace = false;
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward == null)
                            havePlace = true;
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward != null)
                        {
                            havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward);
                        }
                        if (havePlace)
                        {
                            soloveySisterHelped = true;
                            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_help_sister);
                            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_find_traitor);
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                            {
                                playerController.gold -= 50;
                                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[10] = "Now Solovey will say those bandits,that we have alcohol.Thanks...";
                                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[10] = "Are you working with Solovey?We see you";
                            }
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled || GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                            {
                                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[10] = "Thanks.Solovey can't steal our beer anymore";
                                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[10] = "Thanks for your help with betrayer";
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward -= 50;
                            }
                            playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Find traitor";
                            playerController.StartCoroutine("QuestCompleted");
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponentInChildren<Image>().color == Color.red)
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                            Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").gameObject);
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                            {
                                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_solovey_arrested);
                                main.text = "Do you want to ask something?";
                                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints;
                                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward + 10 + 10;
                                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
                                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
                                buttons[3].gameObject.SetActive(true);
                                buttons[2].gameObject.SetActive(true);
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted = true;
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = false;
                                buttons[4].gameObject.SetActive(true);
                                buttons[5].gameObject.SetActive(true);
                                buttons[6].gameObject.SetActive(true);
                                buttonTexts[2].text = "Why village is so empty?";
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward != null)
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().Take(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward, 20);
                                buttonTexts[3].text = "Can say me something about yourself?";
                                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                            }
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled)
                            {
                                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_kill_solovey);
                                main.text = "Do you want to ask something?";
                                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints;
                                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward + 10;
                                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
                                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
                                buttons[3].gameObject.SetActive(true);
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted = true;
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = false;
                                buttons[2].gameObject.SetActive(true);
                                buttons[4].gameObject.SetActive(true);
                                buttons[5].gameObject.SetActive(true);
                                buttons[6].gameObject.SetActive(true);
                                buttonTexts[2].text = "Why village is so empty?";
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward != null)
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().Take(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward, 20);
                                buttonTexts[3].text = "Can say me something about yourself?";
                                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                            }
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                            {
                                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_solovey_runned_away);
                                main.text = "Do you want to ask something?";
                                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
                                buttons[3].gameObject.SetActive(true);
                                buttons[2].gameObject.SetActive(true);
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted = true;
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = false;
                                buttons[4].gameObject.SetActive(true);
                                buttons[5].gameObject.SetActive(true);
                                buttons[6].gameObject.SetActive(true);
                                buttonTexts[2].text = "Why village is so empty?";
                                buttonTexts[3].text = "Can say me something about yourself?";
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward != null)
                                    GameObject.Find("GUIManager").GetComponent<Inventory>().Take(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward, 20);
                                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                            }
                            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("TakeRewardFromSister");
                            if (GameObject.Find("Village Merchant") != null)
                                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[8] = "Sad,that people have to betray their homeland to rescue someone.In Arkelia treatment from plague is free...";
                        }
                        else
                            main.text = "You don't have place in inventory for reward";
                    }
                    else
                        main.text = "You don't have enough money";
                }
            }
    }
    public void FayeHelpSisterNo()
    {
        if (dialogue.name == "Faye" && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 8 && GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive && fayeYesNoAnswers)
        {
            if (buttonTexts[3].text == "No")
            {
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward);
                }
                if (havePlace)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                    {
                        GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[10] = "Now Solovey will say those bandits,that we have alcohol.Thanks...";
                        GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[10] = "Are you working with Solovey?We see you";
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled|| GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                    {
                        GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[10] = "Thanks.Solovey can't steal our beer anymore";
                        GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[10] = "Thanks for your help with betrayer";
                    }
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Find traitor";
                    playerController.StartCoroutine("QuestCompleted");
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponentInChildren<Image>().color == Color.red)
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").gameObject);
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_find_traitor);
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested)
                    {
                        GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_solovey_arrested);
                        main.text = "Do you want to ask something?";
                        playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints;
                        playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward + 10;
                        playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
                        playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
                        buttons[3].gameObject.SetActive(true);
                        buttons[2].gameObject.SetActive(true);
                        buttons[6].gameObject.SetActive(true);
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = false;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted = true;
                        buttons[4].gameObject.SetActive(true);
                        buttons[5].gameObject.SetActive(true);
                        buttonTexts[2].text = "Why village is so empty?";
                        buttonTexts[3].text = "Can say me something about yourself?";
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyKilled)
                    {
                        GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_kill_solovey);
                        main.text = "Do you want to ask something?";
                        playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints;
                        playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
                        playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
                        playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
                        buttons[3].gameObject.SetActive(true);
                        buttons[2].gameObject.SetActive(true);
                        buttons[4].gameObject.SetActive(true);
                        buttons[6].gameObject.SetActive(true);
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = false;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted = true;
                        buttons[5].gameObject.SetActive(true);
                        buttonTexts[2].text = "Why village is so empty?";
                        buttonTexts[3].text = "Can say me something about yourself?";
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    }
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway)
                    {
                        GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_solovey_runned_away);
                        main.text = "Do you want to ask something?";
                        playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
                        buttons[3].gameObject.SetActive(true);
                        buttons[2].gameObject.SetActive(true);
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = false;
                        buttons[4].gameObject.SetActive(true);
                        buttons[5].gameObject.SetActive(true);
                        buttons[6].gameObject.SetActive(true);
                        buttonTexts[2].text = "Why village is so empty?";
                        buttonTexts[3].text = "Can say me something about yourself?";
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questCompleted = true;
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    }
                    if (GameObject.Find("Village Merchant") != null)
                        GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[8] = "Sad,that people have to betray their homeland to rescue someone.In Arkelia treatment from plague is free...";
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
        }
    }
    //Open market window
    public void ShowYourGoods()
    {
        if(dialogue.personName=="Merchant"&&buttonTexts[3].text=="Show your goods")
        {
            GameObject.Find("GUIManager").GetComponent<GUIController>().MarketWindowControl();
;        }
    }
    public void BobNextText()
    {
        if (dialogue.personName == "Bob"&&GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive&& GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage==3)
        {
            if (main.text == dialogue.sentences[0])
            {
                main.text = dialogue.sentences[1];
                buttonTexts[1].text = "Where I can find him?";
            }
            else if(main.text == dialogue.sentences[1])
            {
                main.text = dialogue.sentences[2];
                buttons[1].gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyBandit.GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
                GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                dialogue.gameObject.GetComponent<CivilianAI>().startRotation = new Vector3(0, 0, 0);
                dialogue.gameObject.GetComponent<CivilianAI>().startPosition =new Vector3(63.73f,21.718f,26.188f);
                dialogue.gameObject.GetComponent<NavMeshAgent>().SetDestination(dialogue.gameObject.GetComponent<CivilianAI>().startPosition);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.SetActive(true);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyBandit.SetActive(true);
                dialogue.gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
                dialogue.gameObject.GetComponent<Animator>().Play("Walk");
            }
        }
    }
    //When can you tell me about you answer pressed
    public void FayeAboutHerself()
    {
        if (dialogue.personName == "Faye")
            if (buttonTexts[3].text == "Can say me something about yourself?" && clickedButtonName == "DialogAnswer6")
            main.text = dialogue.sentences[12];
    }
    //If interesting in town answer pressed
    public void FayeInterestingInTown()
    {
        if (dialogue.personName == "Faye")
            if (buttonTexts[5].text == "What can I find in this village?" && clickedButtonName == "DialogAnswer3")
        {
            main.text = dialogue.sentences[13];
        }
    }
    public void ArtelitDialogueControl()
    {
        if (dialogue.personName == "Artelit stone"&&buttonTexts[1].text=="Who are you?")
        {
            main.text = dialogue.sentences[1];
            buttonTexts[1].text = "No,I don't";
        }
        else if (dialogue.personName == "Artelit stone" && buttonTexts[1].text == "No,I don't")
        {
            main.text = dialogue.sentences[2];
            buttonTexts[1].text = "What we may do?";
        }
        else if (dialogue.personName == "Artelit stone" && buttonTexts[1].text == "What we may do?")
        {
            main.text = dialogue.sentences[3];
            buttonTexts[1].text = "Yes,my Lord";
        }
        else if (dialogue.personName == "Artelit stone" && buttonTexts[1].text == "Yes,my Lord")
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().SpecialQuestStageTwo();
            CloseDialogue();
        }

    }
    //If interesting in valley pressed
    public void FayeInterestingInValley()
    {
        if (dialogue.personName == "Faye")
            if (buttonTexts[6].text == "What can I find in this valley?" && clickedButtonName == "DialogAnswer4")
        {
            main.text = dialogue.sentences[14];
        }
    }
    public void ArrestSolovey()
    {
        if(dialogue.personName=="Solovey")
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 6)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyArrested = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
            GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<SummonedAI>().enabled = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<SummonedAI>().summoner = GameObject.Find("Player");
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.tag = "Civilian";
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<NavMeshAgent>().speed = 4;
            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            main.text = dialogue.sentences[3];
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("Village prison guard").GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
            }
    }
    public void KillSolovey()
    {
        if (dialogue.personName == "Solovey")
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 6)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[7] = "Kill Solovey";
            GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
            GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<NavMeshAgent>().speed = 4;
            main.text = dialogue.sentences[1];
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("Faye").GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
            }
    }
    public void RunSolovey()
    {
        if (dialogue.personName == "Solovey")
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive && GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 6)
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().soloveyRunnedAway = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage = 8;
            GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[8];
            GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<NavMeshAgent>().speed = 4;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().SetBool("IsRunning", true);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<Animator>().Play("Run");
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<CivilianAI>().startPosition= new Vector3(73.6f, 21.7f, -101);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<CivilianAI>().startRotation= new Vector3(0, -59, 0);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("QuestManager").GetComponent<QuestManager>().solovey.GetComponent<CivilianAI>().startPosition);
            main.text = dialogue.sentences[2];
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("Faye").GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
            }
    }
    //Close dialog tab and reset bools
    public void CloseDialogue()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
        GameObject.Find("Player").GetComponent<PlayerController>().questCompleted.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
        GameObject.Find("Player").GetComponent<PlayerController>().newQuest.GetComponent<RectTransform>().localPosition = new Vector3(GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.x, -119, GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponent<RectTransform>().localPosition.z);
        permanentGUI.SetActive(true);
        dialogueIsOpen = false;
        CivilWarTreeActivated = false;
        main.gameObject.SetActive(false);
        if (guiController.journal.activeSelf)
            GameObject.Find("GUIManager").GetComponent<GUIController>().compass.SetActive(true);
        if (dialogueBackground.IsActive()&&guiController.journal.activeSelf)
        {
            cameraMovement.enabled = true;
            playerController.enabled = true;
        }
        GameObject.Find("GUIManager").GetComponent<GUIController>().journal.SetActive(true);
        dialogueBackground.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttons[4].gameObject.SetActive(false);
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
        buttons[1].GetComponent<Image>().color = Color.white;
        buttons[3].GetComponent<Image>().color = Color.white;
        buttons[7].gameObject.SetActive(false);
        guiController.enabled = true;
        fayeYesNoAnswers = false;
        fayeDialoguer = false;
        GameObject.Find("Player").GetComponent<PlayerController>().dialogueIsActive = false;
        //Reset bools for restarting dialogs
        if (fayeTakeQuest == false)
        {
            fayeHelpSaid2 = false;
            fayeHelpSaid1 = false;
        }
    }
    //Faye's quest  dialog code
    public void FayeHelpSetTextMain()
    {
        if (fayeDialoguer)
        {
            //Faye's dialog when after choose quest reward
            if (fayeHelpSaid2 == false && fayeHelpSaid1)
            {
                if(fayeQuestSimpleChoosed)
                GameObject.Find("MainText").GetComponent<Text>().text = dialogue.sentences[2];
                if(fayeQuestPaladinChoosed)
                    GameObject.Find("MainText").GetComponent<Text>().text = dialogue.sentences[5];
                fayeTakeQuest = true;
                fayeHelpSaid2 = true;
                GameObject.Find("DialogAnswer2").SetActive(false);
                buttonTexts[0].text = "Bye";
                buttons[2].gameObject.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest") != null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 1&& GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you know something about \"Ben's place\"?";
                }
                buttonTexts[2].text = "Why village is so empty?";
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "Can say me something about yourself?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "What can I find in this village?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What can I find in this valley?";
            }
            //First step:Bye and How I can help
            if (fayeHelpSaid1 == false)
            {
                GameObject.Find("DialogAnswer2Text").GetComponentInChildren<Text>().text = "I will help you.";
                buttons[3].gameObject.SetActive(true);
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                {
                    buttons[3].GetComponent<Image>().color = Color.red;
                    buttonTexts[3].text = "I vowed to protect people.I will do my best.";
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                {
                    buttons[3].GetComponent<Image>().color = Color.blue;
                    buttonTexts[3].text = "I can help you,if you have knowledge";
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                {
                    buttons[3].GetComponent<Image>().color = Color.green;
                    buttonTexts[3].text = "I will help you,but not for free";
                }
                GameObject.Find("DialogAnswer1Text").GetComponentInChildren<Text>().text = "Sorry,maybe later";
                GameObject.Find("MainText").GetComponent<Text>().text = dialogue.sentences[1];
                fayeHelpSaid1 = true;
            }
        }

    }
    //Faye empty village tree
    public void FayeEmptyVillageDialog()
    {
        if (fayeDialoguer)
        {
            if (CivilWarTreeActivated) {
                //Royalist Answer
                if (buttonTexts[3].text == "Royalists?" && clickedButtonName == "DialogAnswer6")
                    main.text = dialogue.sentences[8];
                //Republicns answer
                if (buttonTexts[2].text == "Republicans?" && clickedButtonName == "DialogAnswer5")
                    main.text = dialogue.sentences[6];
                //Civil War answer
                if (buttonTexts[5].text == "Сivil War?" && clickedButtonName == "DialogAnswer3")
                {
                    buttonTexts[5].text = "What do you think about this war?";
                    main.text = dialogue.sentences[7];
                }
                //What do you think answer
                else if (buttonTexts[5].text == "What do you think about this war?" && clickedButtonName == "DialogAnswer3")
                {
                    buttonTexts[5].text = "Сivil War?";
                    main.text = dialogue.sentences[9];
                }
                //How war going answer
                if (buttonTexts[6].text == "How is war going?" && clickedButtonName == "DialogAnswer4")
                    main.text = dialogue.sentences[10];
                //Village empty dialogue tree start
                if (buttonTexts[2].text == "Why village is so empty?")
                {
                    main.text = dialogue.sentences[4];
                    buttonTexts[2].text = "Republicans?";
                    buttonTexts[3].text = "Royalists?";
                    buttonTexts[5].text = "Сivil War?";
                    buttonTexts[6].text = "How is war going?";
                    buttonTexts[1].text = "Let's back to another questions";
                    buttons[2].gameObject.SetActive(true);
                    buttons[1].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(true);
                    buttons[5].gameObject.SetActive(true);
                    buttons[6].gameObject.SetActive(true);
                }
            }
            //Let's back to another questions answer
            if (buttonTexts[1].text == "Let's back to another questions" && clickedButtonName == "DialogAnswer2")
            {
                main.text = dialogue.sentences[11];
                buttons[1].gameObject.SetActive(false);
                buttonTexts[2].text = "Why village is so empty?";
                buttonTexts[3].text = "Can say me something about yourself?";
                buttonTexts[6].text = "What can I find in this valley?";
                buttonTexts[5].text = "What can I find in this village?";
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 1&& GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you know something about \"Ben's place\"?";
                }
                CivilWarTreeActivated = false;
            }

        }

    }
    //Faye "Ben's place" answer
    public void FayeBensPlaceAnswer()
    {
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest")!=null&& GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive) {
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage == 1 && buttonTexts[1].text == "Do you know something about \"Ben's place\"?")
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage++;
                main.text = dialogue.sentences[15];
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().strangeBook.GetComponent<QuestMarker>();
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker;
                GameObject.Find("Player").GetComponent<PlayerController>().newStage.GetComponentInChildren<Text>().text = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("FayeQuest").GetComponent<QuestSlot>().questStage];
                GameObject.Find("Player").GetComponent<PlayerController>().StartCoroutine("NewStage");
                buttons[1].gameObject.SetActive(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().strangeBook.SetActive(true);
            }
        }
    }
    //Faye quest choose paladin answer
    public void FayeQuestChoosePaladin()
    {
        if(GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
        if (fayeDialoguer && fayeTakeQuest == false&& buttonTexts[3].text == "I vowed to protect people.I will do my best.")
        {
                buttons[3].GetComponent<Image>().color = Color.white;
                playerController.newQuest.GetComponentInChildren < Text >().text= "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("FayeQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.SetActive(true);
            fayeQuestPaladinChoosed = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward *= 2;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "FayeQuest";
                fayeTakeQuest = true;
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.GetComponent<QuestMarker>();
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward;
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
                    for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[i]);
            }
            }
    }
    //Faye quest simple choose
    public void FayeQuestChooseSimple()
    {
        if (fayeDialoguer && fayeTakeQuest == false&&buttonTexts[1].text=="I will help you.")
        {
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("FayeQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.SetActive(true);
            fayeQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "FayeQuest";
            buttons[3].GetComponent<Image>().color = Color.white;
            fayeTakeQuest = true;
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.GetComponent<QuestMarker>();
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
    //Faye quest robber choose
    public void FayeQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (fayeDialoguer && fayeTakeQuest == false && buttonTexts[3].text == "I will help you,but not for free")
        {
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("FayeQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.SetActive(true);
            fayeQuestRobberChoosed = true;
                buttons[3].GetComponent<Image>().color = Color.white;
                fayeTakeQuest = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward *= 2;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "FayeQuest";
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.GetComponent<QuestMarker>();
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[i]);
            }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
    //Faye quest mage choose
    public void FayeQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (fayeDialoguer && fayeTakeQuest == false && buttonTexts[3].text == "I can help you,if you have knowledge")
        {
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("FayeQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.SetActive(true);
            fayeQuestMageChoosed = true;
                buttons[3].GetComponent<Image>().color = Color.white;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints++;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "FayeQuest";
                fayeTakeQuest = true;
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.isActive = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().bensDiary.GetComponent<QuestMarker>();
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.skillPoints;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward != null)
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().fayeQuest.goal[i]);
            }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
    //Head of Guard quest chooses
    public void HeadOfGuardQuestChoosePaladin()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Head of Guard" && headOfGuardTakeQuest == false && buttonTexts[3].text == "I will do my best")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[9];
                buttonTexts[1].text = "Let's do this";
                buttons[2].gameObject.SetActive(false);
                buttons[0].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttons[6].gameObject.SetActive(false);
                headOfGuardTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfGuardQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfGuardQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfGuardQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
    }
    public void HeadOfGuardQuestChooseSimple()
    {
        if (dialogue.personName == "Head of Guard" && headOfGuardTakeQuest == false && buttonTexts[1].text == "I will help you")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = dialogue.sentences[9];
            buttonTexts[1].text = "Let's do this";
            buttons[2].gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
            headOfGuardTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfGuardQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfGuardQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfGuardQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (dialogue.personName == "Head of Guard" && buttonTexts[1].text == "Do you need a help?")
        {
            main.text = dialogue.sentences[7];
            buttonTexts[0].text = "Bye";
            buttonTexts[1].text = "I will help you";
            buttonTexts[2].text = "Sorry,but I can't help you now";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            {
                buttonTexts[3].text = "I will help you,but not for free";
                buttons[3].GetComponent<Image>().color = Color.green;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                buttonTexts[3].text = "I will do my best";
                buttons[3].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttonTexts[3].text = "I will help,but I need knowledge";
                buttons[3].GetComponent<Image>().color = Color.blue;
            }
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
        }
        else if (dialogue.personName == "Head of Guard" && buttonTexts[1].text == "I want to talk about royalists...")
        {
            if (!headOfGuardTakeQuest)
                buttonTexts[1].text = "Do you need a help?";
            else
                buttons[1].gameObject.SetActive(false);
            main.text = "6 people? This can not be. I was aware of the details of the operation. 5 people is the maximum. Pass it to the new commander";
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsQuestStageOne();
        }
        else if (dialogue.personName == "Head of Guard" && buttonTexts[1].text == "For the village!")
        {
            if (extraWarriorsInArmy)
            {
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestRoyalistsStageTwo();
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestRepublicanStageTwo();
                GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestVillageGuardMoveTwo();
                buttons[1].gameObject.SetActive(false);
                main.text = "For the village!";
                armyStageTwo = true;
            }
        }
        }
    public void HeadOfGuardQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (dialogue.personName == "Head of Guard" && headOfGuardTakeQuest == false && buttonTexts[3].text == "I will help you,but not for free")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[9];
                buttonTexts[1].text = "Let's do this";
                buttons[2].gameObject.SetActive(false);
                buttons[0].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttons[6].gameObject.SetActive(false);
                headOfGuardTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfGuardQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfGuardQuestRobberChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfGuardQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
    }
    public void HeadOfGuardQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (dialogue.personName == "Head of Guard" && headOfGuardTakeQuest == false && buttonTexts[3].text == "I will help,but I need knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[9];
                buttonTexts[1].text = "Let's do this";
                buttons[2].gameObject.SetActive(false);
                buttons[0].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttons[6].gameObject.SetActive(false);
                headOfGuardTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfGuardQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfGuardQuestMageChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.skillPoints++;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfGuardQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.skillPoints;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward != null)
                    spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfGuardQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
    }
    //Head of Guard quest chooses
    public void LibrarianSurveyQuestChoosePaladin()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Librarian" && librarianSurveyTakeQuest == false && buttonTexts[3].text == "I will do my best")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[8];
                if (!librarianSpecialTakeQuest)
                    buttonTexts[1].text = "Do you need a help?";
                else
                    buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(true);
                buttons[0].gameObject.SetActive(true);
                buttons[3].gameObject.SetActive(true);
                buttons[5].gameObject.SetActive(true);
                buttons[6].gameObject.SetActive(true);
                buttonTexts[3].text = "Where is library?";
                buttonTexts[2].text = "I'm ready";
                buttonTexts[0].text = "Bye";
                buttonTexts[5].text = "You don't live here,do you?";
                buttonTexts[6].text = "What do you think about civil war?";
                if (!librarianSpecialTakeQuest && GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you need a help?";
                    buttons[1].GetComponent<Image>().color = Color.blue;
                }
                if (librarianSurveyTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted)
                    buttonTexts[2].text = "I'm ready";
                librarianSurveyTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("LibrarianSurveyQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                librarianSurveyQuestPaladinChoosed = true;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "LibrarianSurveyQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Where is library?")
                main.text = dialogue.sentences[3];
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Eternal Empire")
            {
                main.text = "When liberalization in Endless Empire started?";
                buttonTexts[2].text = "402";
                buttonTexts[3].text = "452";
                buttonTexts[5].text = "453";
                buttonTexts[6].text = "356";
                surveyCorrectAnswers++;
            }
        else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "452")
            {
                surveyCorrectAnswers++;
                main.text = "Consequences of liberalization?";
                buttonTexts[2].text = "Continental Crisis";
                buttonTexts[3].text = "Creation of the Eternal Federation";
                buttonTexts[5].text = "Rising standard of living";
                buttonTexts[6].text = "Strengthening national liberation movements";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Creation of the Eternal Federation")
            {
                main.text = "First king of Lovania";
                buttonTexts[2].text = "Ludwig The Great";
                buttonTexts[3].text = "Bill Rosenblaum";
                buttonTexts[5].text = "Wilhelm The Second";
                buttonTexts[6].text = "Jacob von DeLune";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Bill Rosenblaum")
            {
                main.text = "When the First Constitution was adopted";
                buttonTexts[2].text = "887";
                buttonTexts[3].text = "1072";
                buttonTexts[5].text = "768";
                buttonTexts[6].text = "1073";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "1072" && main.text == "When the First Constitution was adopted")
            {
                main.text = "When the Second Constitution was adopted";
                buttonTexts[2].text = "1211";
                buttonTexts[3].text = "1072";
                buttonTexts[5].text = "1073";
                buttonTexts[6].text = "1189";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "1072" && main.text == "When the Second Constitution was adopted")
            {
                main.text = "Causes of the December Revolution";
                buttonTexts[2].text = "Low standard of living. Striving for democracy";
                buttonTexts[3].text = "Dissatisfaction of the nobility";
                buttonTexts[5].text = "Influence of the Federation";
                buttonTexts[6].text = "Offensive on the Order";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Dissatisfaction of the nobility")
            {
                main.text = "Who declared war on Lovania after the December Revolution";
                buttonTexts[2].text = "The Rosenblaum Empire";
                buttonTexts[3].text = "The Federation";
                buttonTexts[5].text = "South Triumvirate";
                buttonTexts[6].text = "Republic of Artholid";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "The Federation")
            {
                surveyCorrectAnswers++;
                main.text = "Prime Minister of the Provisional Government";
                buttonTexts[2].text = "Augusto Pinochet";
                buttonTexts[3].text = "Vladimir Chikenko";
                buttonTexts[5].text = "Harry Chacril";
                buttonTexts[6].text = "Myhailo Hrushevskiy";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Vladimir Chikenko")
            {
                surveyCorrectAnswers++;
                main.text = "Consequences of the December Revolution";
                buttonTexts[2].text = "The beginning of the revival of Lavania";
                buttonTexts[3].text = "Loss of sovereignty";
                buttonTexts[5].text = "Proclamation of the Republic";
                buttonTexts[6].text = "Loss of territories";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Loss of sovereignty")
                LibrarianSurveyFinishQuest();
    }
    public void LibrarianSurveyQuestChooseSimple()
    {
        if (dialogue.personName == "Librarian" && librarianSurveyTakeQuest == false && buttonTexts[1].text == "I will answer on the survey")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = dialogue.sentences[8];
            if (!librarianSpecialTakeQuest)
                buttonTexts[1].text = "Do you need a help?";
            else
                buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(true);
            buttons[0].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
            if (!librarianSpecialTakeQuest && GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "Do you need a help?";
                buttons[1].GetComponent<Image>().color = Color.blue;
            }
            if (librarianSurveyTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted)
                buttonTexts[2].text = "I'm ready";
            buttonTexts[3].text = "Where is library?";
            buttonTexts[2].text = "I'm ready";
            buttonTexts[0].text = "Bye";
            buttonTexts[5].text = "You don't live here,do you?";
            buttonTexts[6].text = "What do you think about civil war?";
            librarianSurveyTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("LibrarianSurveyQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            librarianSurveyQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "LibrarianSurveyQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest&&GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (dialogue.personName == "Librarian" && buttonTexts[1].text == "Do you need a help?")
        {
            main.text = dialogue.sentences[9];
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "Sorry,but I can't pass it now";
            buttonTexts[1].text = "I will help you";
            buttons[3].gameObject.SetActive(false);
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
            for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
            {
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Old book")
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "I have something interesting...";
                    }
            }
        }
        else if (dialogue.personName == "Librarian" && !librarianSpecialTakeQuest && buttonTexts[1].text == "I will help you")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            main.text = "Thanks!I will be waiting for you here";
            buttons[1].gameObject.SetActive(false);
            buttons[1].GetComponent<Image>().color = Color.white;
            buttons[2].gameObject.SetActive(true);
            buttons[0].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(true);
            if (librarianSurveyTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questCompleted)
                buttonTexts[2].text = "I'm ready";
            if (!librarianSurveyTakeQuest)
                buttonTexts[2].text = "Which books do you have?";
            buttonTexts[3].text = "Where is library?";
            buttonTexts[0].text = "Bye";
            buttonTexts[5].text = "You don't live here,do you?";
            buttonTexts[6].text = "What do you think about civil war?";
            librarianSpecialTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("LibrarianSpecialQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "LibrarianSpecialQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (dialogue.personName == "Librarian"&& buttonTexts[1].text == "I have something interesting...")
                {
                    bool havePlace = false;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.objectReward == null)
                        havePlace = true;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.objectReward != null)
                    {
                        havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.objectReward);
                    }
                    if (havePlace)
                    {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_crypt);
                main.text = "I have never seen such a language before. And these drawings ... It should be taken to the Academy. I will leave as soon as I can. Thank you for your help. Here is your reward";
                        buttons[1].gameObject.SetActive(false);
                        playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Crypt";
                        playerController.StartCoroutine("QuestCompleted");
                if(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest")!=null)
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponentInChildren<Image>().color == Color.red)
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").gameObject);
                        playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.skillPoints;
                        playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.prestigeReward;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.questCompleted = true;
                        playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.experienceReward;
                        playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.goldReward;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSpecialQuest.isActive = false;
                librarianSpecialTakeQuest = true;
                if(GameObject.Find("Village Merchant")!=null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[11] = "Good work in the crypt. It is interesting that the Academy learns";
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[14] = "Something interesting was found in the crypt. Maybe extraterrestrial life?";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[14] = "Something secret in the crypt? Well, it doesn't concern me";
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Old book")
                        {
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                            break;
                        }
                }
            }
                    else
                        main.text = "You don't have place in inventory for reward";
                }
    }
    public void LibrarianSurveyQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (dialogue.personName == "Librarian" && librarianSurveyTakeQuest == false && buttonTexts[3].text == "I will pass it,but not for free")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[8];
                if (!librarianSpecialTakeQuest)
                    buttonTexts[1].text = "Do you need a help?";
                else
                    buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(true);
                buttons[0].gameObject.SetActive(true);
                buttons[3].gameObject.SetActive(true);
                buttons[5].gameObject.SetActive(true);
                buttons[6].gameObject.SetActive(true);
                if (!librarianSpecialTakeQuest && GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you need a help?";
                    buttons[1].GetComponent<Image>().color = Color.blue;
                }
                if (librarianSurveyTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted)
                    buttonTexts[2].text = "I'm ready";
                buttonTexts[3].text = "Where is library?";
                buttonTexts[2].text = "I'm ready";
                buttonTexts[0].text = "Bye";
                buttonTexts[5].text = "You don't live here,do you?";
                buttonTexts[6].text = "What do you think about civil war?";
                librarianSurveyTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("LibrarianSurveyQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                librarianSurveyQuestRobberChoosed = true;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "LibrarianSurveyQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Where is library?")
                main.text = dialogue.sentences[3];
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Eternal Empire")
            {
                surveyCorrectAnswers++;
                main.text = "When liberalization in Endless Empire started?";
                buttonTexts[2].text = "402";
                buttonTexts[3].text = "452";
                buttonTexts[5].text = "453";
                buttonTexts[6].text = "356";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "452")
            {
                surveyCorrectAnswers++;
                main.text = "Consequences of liberalization?";
                buttonTexts[2].text = "Continental Crisis";
                buttonTexts[3].text = "Creation of the Eternal Federation";
                buttonTexts[5].text = "Rising standard of living";
                buttonTexts[6].text = "Strengthening national liberation movements";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Creation of the Eternal Federation")
            {
                main.text = "First king of Lovania";
                buttonTexts[2].text = "Ludwig The Great";
                buttonTexts[3].text = "Bill Rosenblaum";
                buttonTexts[5].text = "Wilhelm The Second";
                buttonTexts[6].text = "Jacob von DeLune";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Bill Rosenblaum")
            {
                main.text = "When the First Constitution was adopted";
                buttonTexts[2].text = "887";
                buttonTexts[3].text = "1072";
                buttonTexts[5].text = "768";
                buttonTexts[6].text = "1073";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "1072" && main.text == "When the First Constitution was adopted")
            {
                main.text = "When the Second Constitution was adopted";
                buttonTexts[2].text = "1211";
                buttonTexts[3].text = "1072";
                buttonTexts[5].text = "1073";
                buttonTexts[6].text = "1189";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "1072" && main.text == "When the Second Constitution was adopted")
            {
                main.text = "Causes of the December Revolution";
                buttonTexts[2].text = "Low standard of living. Striving for democracy";
                buttonTexts[3].text = "Dissatisfaction of the nobility";
                buttonTexts[5].text = "Influence of the Federation";
                buttonTexts[6].text = "Offensive on the Order";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Dissatisfaction of the nobility")
            {
                main.text = "Who declared war on Lovania after the December Revolution";
                buttonTexts[2].text = "The Rosenblaum Empire";
                buttonTexts[3].text = "The Federation";
                buttonTexts[5].text = "South Triumvirate";
                buttonTexts[6].text = "Republic of Artholid";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "The Federation")
            {
                surveyCorrectAnswers++;
                main.text = "Prime Minister of the Provisional Government";
                buttonTexts[2].text = "Augusto Pinochet";
                buttonTexts[3].text = "Vladimir Chikenko";
                buttonTexts[5].text = "Harry Chacril";
                buttonTexts[6].text = "Myhailo Hrushevskiy";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Vladimir Chikenko")
            {
                surveyCorrectAnswers++;
                main.text = "Consequences of the December Revolution";
                buttonTexts[2].text = "The beginning of the revival of Lavania";
                buttonTexts[3].text = "Loss of sovereignty";
                buttonTexts[5].text = "Proclamation of the Republic";
                buttonTexts[6].text = "Loss of territories";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Loss of sovereignty")
                LibrarianSurveyFinishQuest();
    }
    public void LibrarianSurveyQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (dialogue.personName == "Librarian" && librarianSurveyTakeQuest == false && buttonTexts[3].text == "I will pass it,but I need knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = dialogue.sentences[8];
                if (!librarianSpecialTakeQuest)
                    buttonTexts[1].text = "Do you need a help?";
                else
                    buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(true);
                buttons[0].gameObject.SetActive(true);
                buttons[3].gameObject.SetActive(true);
                buttons[5].gameObject.SetActive(true);
                buttons[6].gameObject.SetActive(true);
                if (!librarianSpecialTakeQuest && GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "Do you need a help?";
                    buttons[1].GetComponent<Image>().color = Color.blue;
                }
                if (librarianSurveyTakeQuest && !GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questCompleted)
                    buttonTexts[2].text = "I'm ready";
                buttonTexts[3].text = "Where is library?";
                buttonTexts[2].text = "I'm ready";
                buttonTexts[0].text = "Bye";
                buttonTexts[5].text = "You don't live here,do you?";
                buttonTexts[6].text = "What do you think about civil war?";
                librarianSurveyTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("LibrarianSurveyQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                librarianSurveyQuestMageChoosed = true;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "LibrarianSurveyQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.skillPoints;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward != null)
                    spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().librarianSurveyQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Where is library?")
                main.text = dialogue.sentences[3];
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Eternal Empire")
            {
                main.text = "When liberalization in Endless Empire started?";
                surveyCorrectAnswers++;
                buttonTexts[2].text = "402";
                buttonTexts[3].text = "452";
                buttonTexts[5].text = "453";
                buttonTexts[6].text = "356";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "452")
            {
                surveyCorrectAnswers++;
                main.text = "Consequences of liberalization?";
                buttonTexts[2].text = "Continental Crisis";
                buttonTexts[3].text = "Creation of the Eternal Federation";
                buttonTexts[5].text = "Rising standard of living";
                buttonTexts[6].text = "Strengthening national liberation movements";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Creation of the Eternal Federation")
            {
                main.text = "First king of Lovania";
                buttonTexts[2].text = "Ludwig The Great";
                buttonTexts[3].text = "Bill Rosenblaum";
                buttonTexts[5].text = "Wilhelm The Second";
                buttonTexts[6].text = "Jacob von DeLune";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Bill Rosenblaum")
            {
                main.text = "When the First Constitution was adopted";
                buttonTexts[2].text = "887";
                buttonTexts[3].text = "1072";
                buttonTexts[5].text = "768";
                buttonTexts[6].text = "1073";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "1072"&&main.text== "When the First Constitution was adopted")
            {
                main.text = "When the Second Constitution was adopted";
                buttonTexts[2].text = "1211";
                buttonTexts[3].text = "1072";
                buttonTexts[5].text = "1073";
                buttonTexts[6].text = "1189";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "1072"&&main.text== "When the Second Constitution was adopted")
            {
                main.text = "Causes of the December Revolution";
                buttonTexts[2].text = "Low standard of living. Striving for democracy";
                buttonTexts[3].text = "Dissatisfaction of the nobility";
                buttonTexts[5].text = "Influence of the Federation";
                buttonTexts[6].text = "Offensive on the Order";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Dissatisfaction of the nobility")
            {
                main.text = "Who declared war on Lovania after the December Revolution";
                buttonTexts[2].text = "The Rosenblaum Empire";
                buttonTexts[3].text = "The Federation";
                buttonTexts[5].text = "South Triumvirate";
                buttonTexts[6].text = "Republic of Artholid";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "The Federation")
            {
                surveyCorrectAnswers++;
                main.text = "Prime Minister of the Provisional Government";
                buttonTexts[2].text = "Augusto Pinochet";
                buttonTexts[3].text = "Vladimir Chikenko";
                buttonTexts[5].text = "Harry Chacril";
                buttonTexts[6].text = "Myhailo Hrushevskiy";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Vladimir Chikenko")
            {
                surveyCorrectAnswers++;
                main.text = "Consequences of the December Revolution";
                buttonTexts[2].text = "The beginning of the revival of Lavania";
                buttonTexts[3].text = "Loss of sovereignty";
                buttonTexts[5].text = "Proclamation of the Republic";
                buttonTexts[6].text = "Loss of territories";
            }
            else if (dialogue.personName == "Librarian" && buttonTexts[3].text == "Loss of sovereignty")
                LibrarianSurveyFinishQuest();
    }
    public void HeadOfVillageQuestChoosePaladin()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Head of Village" && headOfVillageTakeQuest == false && buttonTexts[3].text == "I will do my best")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttonTexts[3].text = "What do you think about war?";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "How are things in the village going?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "Can you tell me about bandits?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "Where I can find job?";
                main.text = "Thanks!I will be waiting for you here";
                headOfVillageTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfVillageQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfVillageQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfVillageQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
       else if (dialogue.personName == "Head of Village" && buttonTexts[3].text == "What do you think about war?")
            main.text = dialogue.sentences[4];
    }
    public void HeadOfVillageQuestChooseSimple()
    {
        if (dialogue.personName == "Head of Village" && headOfVillageTakeQuest == false && buttonTexts[1].text == "I will help you")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[1].gameObject.SetActive(false);
            buttonTexts[3].text = "What do you think about war?";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "How are things in the village going?";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "Can you tell me about bandits?";
            buttons[6].gameObject.SetActive(true);
            main.text = "Thanks!I will be waiting for you here";
            headOfVillageTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfVillageQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfVillageQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfVillageQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (dialogue.personName == "Head of Village" && buttonTexts[1].text == "Do you know something about Dragon scroll?")
        {
            if (playerController.prestige >= 50)
            {
                main.text = "The scroll was taken away by the head of bandits. It is my brother. My family kept the scroll safe for a long time here where nobody will think to look for it.He stole it when I went to those deserters.They terrorize our village within a month.It is necessary to finish it.Tell me when you're ready. The guard will go to their camp and wait for your order to storm. Visit the Republican and Royalist camps and help them. Maybe they\'ll agree to help us.";
                knowAboutScroll = true;
                buttonTexts[1].text = "Gather soldiers";
            }
            else
                main.text = "I'm sorry, but I don't trust you.Help our village, then we'll talk [You need to have 50 prestige]";
        }
        else if (dialogue.personName == "Head of Village" && buttonTexts[1].text == "Gather soldiers")
        {
            main.text = "Good.Talk to the head of the guard at the meeting place when you are ready";
            villageGuardStageOne = true;
            buttons[1].gameObject.SetActive(false);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestVillageGuardMove();
        }
    }
    public void HeadOfVillageQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (dialogue.personName == "Head of Village" && headOfVillageTakeQuest == false && buttonTexts[3].text == "I will help you,if you pay well")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttonTexts[3].text = "What do you think about war?";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "How are things in the village going?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "Can you tell me about bandits?";
                buttons[6].gameObject.SetActive(true);
                main.text = "Thanks!I will be waiting for you here";
                headOfVillageTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfVillageQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfVillageQuestRobberChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfVillageQuest";

                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Head of Village" && buttonTexts[3].text == "What do you think about war?")
                main.text = dialogue.sentences[4];
    }
    public void HeadOfVillageQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (dialogue.personName == "Head of Village" && headOfVillageTakeQuest == false && buttonTexts[3].text == "I will help you,but I need knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttonTexts[3].text = "What do you think about war?";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "How are things in the village going?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "Can you tell me about bandits?";
                buttons[6].gameObject.SetActive(true);
                main.text = "Thanks!I will be waiting for you here";
                headOfVillageTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfVillageQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfVillageQuestMageChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.skillPoints++;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfVillageQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.skillPoints;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward != null)
                    spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest)
                {
                    spawn.GetComponentInChildren<Image>().color = Color.red;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.currentQuestMarker != null && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
                    {
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfVillageQuest.currentQuestMarker;
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                    }
                    else
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
                }
            }
            else if (dialogue.personName == "Head of Village" && buttonTexts[3].text == "What do you think about war?")
                main.text = dialogue.sentences[4];
    }
    public void HeadOfHuntersQuestChoosePaladin()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Head of hunters" && headOfHuntersTakeQuest == false && buttonTexts[3].text == "I will do my best")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttonTexts[0].text = "Bye";
                buttonTexts[3].text = "Show your goods";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "Who are you?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "What can I find in forest?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you think about civil war?";
                main.text = "Look for a mushroom in the west. According to legend, it will be \"under the green darkness\"";
                headOfHuntersTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfHuntersQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfHuntersQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfHuntersQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().specialMushroom.SetActive(true);
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Head of hunters"  && buttonTexts[3].text == "Show your goods")
                GameObject.Find("GUIManager").GetComponent<GUIController>().MarketWindowControl();
    }
    public void HeadOfHuntersQuestChooseSimple()
    {
        if (dialogue.personName == "Head of hunters" && headOfHuntersTakeQuest == false && buttonTexts[1].text == "I will help you")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(true);
            buttonTexts[0].text = "Bye";
            buttonTexts[3].text = "Show your goods";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "Who are you?";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "What can I find in forest?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you think about civil war?";
            main.text = "Look for a mushroom in the west. According to legend, it will be \"under the green darkness\"";
            headOfHuntersTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfHuntersQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfHuntersQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfHuntersQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().specialMushroom.SetActive(true);
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (dialogue.personName == "Head of hunters" && buttonTexts[1].text == "Do you need a help?")
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            {
                buttonTexts[3].text = "I will help you,if you pay well";
                buttons[3].GetComponent<Image>().color = Color.green;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                buttonTexts[3].text = "I will do my best";
                buttons[3].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttonTexts[3].text = "I will help you,but I need knowledge";
                buttons[3].GetComponent<Image>().color = Color.blue;
            }
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
            buttonTexts[1].text = "I will help you";
            buttonTexts[2].text = "Sorry,but I can't help you";
            main.text = "According to legend, every 10 years a special mushroom appears in the forest, which is said to have a connection with Artelis. A gentleman from the Сapital is ready to help us with food if we give it to him. Bring me a mushroom and I will give you a generous reward";
        }
        else if (dialogue.personName == "Head of hunters" && buttonTexts[1].text == "I have mushroom")
            HeadOfHuntersFinishQuest();
    }
    public void HeadOfHuntersQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (dialogue.personName == "Head of hunters" && headOfHuntersTakeQuest == false && buttonTexts[3].text == "I will help you,if you pay well")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttonTexts[0].text = "Bye";
                buttonTexts[3].text = "Show your goods";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "Who are you?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "What can I find in forest?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you think about civil war?";
                main.text = "Look for a mushroom in the west. According to legend, it will be \"under the green darkness\"";
                headOfHuntersTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfHuntersQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfHuntersQuestRobberChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfHuntersQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().specialMushroom.SetActive(true);
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
                {
                    spawn.GetComponentInChildren<Image>().color = Color.red;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker != null)
                    {
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().specialMushroom.GetComponent<QuestMarker>();
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.GetComponentInChildren<Compass>().questMarker = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.currentQuestMarker;
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(true);
                    }
                    else
                        GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = true;
                }
            }
            else if (dialogue.personName == "Head of hunters"  && buttonTexts[3].text == "Show your goods")
                GameObject.Find("GUIManager").GetComponent<GUIController>().MarketWindowControl();
    }
    public void HeadOfHuntersQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (dialogue.personName == "Head of hunters" && headOfHuntersTakeQuest == false && buttonTexts[3].text == "I will help you,but I need knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttonTexts[0].text = "Bye";
                buttonTexts[3].text = "Show your goods";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "Who are you?";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "What can I find in forest?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you think about civil war?";
                main.text = "Look for a mushroom in the west. According to legend, it will be \"under the green darkness\"";
                headOfHuntersTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfHuntersQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfHuntersQuestMageChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.skillPoints++;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfHuntersQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.skillPoints;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().specialMushroom.SetActive(true);
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward != null)
                    spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfHuntersQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Head of hunters" && buttonTexts[3].text == "Show your goods")
                GameObject.Find("GUIManager").GetComponent<GUIController>().MarketWindowControl();
    }
    public void HeadOfRepublicansQuestChoosePaladin()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeQuest == false && buttonTexts[3].text == "I will do my best")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                main.text = "Great!I will be waiting for you here";
                headOfRepublicansTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRepublicansQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRepublicansQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal.Length; i++)
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[i]);
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
      else  if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeSecondQuest == false && buttonTexts[3].text == "I will help republicans")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttonTexts[3].text = "What do you think about war?";
            buttonTexts[0].text = "Let's go";
            buttons[5].gameObject.SetActive(false);
            buttonTexts[5].text = "How's the war going?";
            buttons[6].gameObject.SetActive(false);
            buttonTexts[6].text = "What do you see the country after the war?";
            buttons[0].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            main.text = "Great. See you at their camp";
                headOfRepublicansTakeSecondQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansSecondQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRepublicansSecondQuestPaladinChoosed = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward *= 2;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRepublicansSecondQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal.Length; i++)
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[i]);
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansSecondQuestStageZero();
        }
        else if (buttonTexts[3].text == "What do you think about war?" && dialogue.personName == "Head of Republicans")
        {
            main.text = dialogue.sentences[3];
        }
            else if (dialogue.personName == "Head of Republicans" && buttonTexts[3].text == "I have orders")
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "I want to join";
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                buttonTexts[3].text = "What do you think about war?";
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = "There are traitors among us. We will pass this information to the main camp. By the way, we need another person to storm the camp of the royalists. Tell me if you want to join. Here is your reward";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_get_orders);
                    for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                            if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
                            {
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                                break;
                            }
                    }
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Get orders";
                    playerController.StartCoroutine("QuestCompleted");
                    headOfRepublicansTakeQuest = true;
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                    {
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponentInChildren<Image>().color == Color.red)
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                        Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").gameObject);
                    }
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward *= 2;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[5] = "Thank you for your help with the orders of the royalists. We are happy to see you";
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
            else if (dialogue.personName == "Head of Republicans" && buttonTexts[3].text == "They are already dead")
            {
                buttons[1].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);
                main.text = "Good job.Thanks for your help.Tell me,if you want something";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_assault);
                    headOfRepublicansTakeSecondQuest = true;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward *= 2;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Assault";
                    playerController.StartCoroutine("QuestCompleted");
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Thank you for your help. The Republic will not forget it";
                    if(GameObject.Find("Village Merchant")!=null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[14] = "The royalists are destroyed ... Believe me, you are fighting for the right cause";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[16] = "The royalists are destroyed ... The main one will be furious";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[16] = "Someone destroyed the royalists. It seems we need to side with the Republicans";
                    if (knowAboutScroll) 
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We need help with bandits";
                    }
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }
    public void HeadOfRepublicansQuestChooseSimple()
    {
        if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeQuest == false && buttonTexts[1].text == "I will help you")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "How's the war going?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you see the country after the war?";
            buttons[0].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            main.text = "Great!I will be waiting for you here";
            headOfRepublicansTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfRepublicansQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRepublicansQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (buttonTexts[1].text == "I want to join" && dialogue.personName == "Head of Republicans")
        {
            main.text = "We gather near their camp if you want to join";
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(true);
            buttonTexts[1].text = "Let's do it";
            buttonTexts[2].text = "Maybe,later";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            {
                buttonTexts[3].text = "I will join,but I need money";
                buttons[3].GetComponent<Image>().color = Color.green;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                buttonTexts[3].text = "I will help republicans";
                buttons[3].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttonTexts[3].text = "I will help you,but I need some knowledge";
                buttons[3].GetComponent<Image>().color = Color.blue;
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest") == null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRoyalists == 5)
                {
                    buttonTexts[1].text = "They are already dead";
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                    {
                        buttonTexts[3].text = "They are already dead";
                        buttons[3].GetComponent<Image>().color = Color.green;
                    }
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                    {
                        buttonTexts[3].text = "They are already dead";
                        buttons[3].GetComponent<Image>().color = Color.red;
                    }
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                    {
                        buttonTexts[3].text = "They are already dead";
                        buttons[3].GetComponent<Image>().color = Color.blue;
                    }
                }
        }
        else if (buttonTexts[1].text == "Do you need a help?" && dialogue.personName == "Head of Republicans")
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                main.text = "Help? Yes, we need an ordinary person. We don't know what Republicans plan to do and it confuses us a little. We need their orders from the high command. We have the spy there, talk to him, he will tell you how to get them. Or can you try to steal them ...";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = "Help? Yes, we need an ordinary person. We don't know what Republicans plan to do and it confuses us a little. We need their orders from the high command. We have the spy there, talk to him, he will tell you how to get them. Or, you are a paladin? There are rumors that the Order supports royalists. Tell that you came on orders. They will believe you";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                main.text = "Help? Yes, we need an ordinary person. We don't know what Republicans plan to do and it confuses us a little. We need their orders from the high command. We have the spy there, talk to him, he will tell you how to get them.";
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(true);
            buttonTexts[1].text = "I will help you";
            buttonTexts[2].text = "I will think about your proposal";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            {
                buttonTexts[3].text = "I will help you,if you pay well";
                buttons[3].GetComponent<Image>().color = Color.green;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                buttonTexts[3].text = "I will do my best";
                buttons[3].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttonTexts[3].text = "I will help you,but I need knowledge";
                buttons[3].GetComponent<Image>().color = Color.blue;
            }
            for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
            {
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
                    {
                        buttonTexts[1].text = "I have orders";
                        buttonTexts[3].text = "I have orders";
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                            buttons[3].GetComponent<Image>().color = Color.green;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                            buttons[3].GetComponent<Image>().color = Color.red;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                            buttons[3].GetComponent<Image>().color = Color.blue;
                        break;
                    }
            }
        }
        else if (dialogue.personName == "Head of Republicans" && buttonTexts[1].text == "I have orders")
        {
            buttons[1].gameObject.SetActive(true);
            buttonTexts[1].text = "I want to join";
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "How's the war going?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you see the country after the war?";
            buttons[0].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            main.text = "There are traitors among us. We will pass this information to the main camp. By the way, we need another person to storm the camp of the royalists. Tell me if you want to join. Here is your reward";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_get_orders);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
                        {
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                            break;
                        }
                }
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Get orders";
                playerController.StartCoroutine("QuestCompleted");
                headOfRepublicansTakeQuest = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                {
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponentInChildren<Image>().color == Color.red)
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").gameObject);
                }
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[5] = "Thank you for your help with the orders of the royalists. We are happy to see you";
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
        else if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeSecondQuest == false && buttonTexts[1].text == "Let's do it")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
            buttonTexts[3].text = "What do you think about war?";
            buttonTexts[0].text = "Let's go";
            buttons[5].gameObject.SetActive(false);
            buttonTexts[5].text = "How's the war going?";
            buttons[6].gameObject.SetActive(false);
            buttonTexts[6].text = "What do you see the country after the war?";
            buttons[0].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            main.text = "Great. See you at their camp";
            headOfRepublicansTakeSecondQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansSecondQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfRepublicansSecondQuestPaladinChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRepublicansSecondQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal.Length; i++)
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[i]);
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansSecondQuestStageZero();
        }
        else if (dialogue.personName == "Head of Republicans" && buttonTexts[1].text == "For the Republic!")
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansSecondQuestStageOne();
            CloseDialogue();
        }
        else if (dialogue.personName == "Head of Republicans" && buttonTexts[1].text == "We did it!")
        {
            buttons[1].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            main.text = "Good job.Thanks for your help.Tell me,if you want something";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_assault);
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Assault";
                playerController.StartCoroutine("QuestCompleted");
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").GetComponentInChildren<Image>().color == Color.red)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansSecondQuest").gameObject);
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Thank you for your help. The Republic will not forget it";
                if(GameObject.Find("Village Merchant")!=null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[14] = "The royalists are destroyed ... Believe me, you are fighting for the right cause";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[16] = "The royalists are destroyed ... The main one will be furious";
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[16] = "Someone destroyed the royalists. It seems we need to side with the Republicans";
                if (knowAboutScroll)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "We need help with bandits";
                }
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
        else if (dialogue.personName == "Head of Republicans" && buttonTexts[1].text == "They are already dead")
        {
            buttons[1].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            main.text = "Good job.Thanks for your help.Tell me,if you want something";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_assault);
                headOfRepublicansTakeSecondQuest = true;
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Assault";
                playerController.StartCoroutine("QuestCompleted");
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Thank you for your help. The Republic will not forget it";
                if(GameObject.Find("Village Merchant")!=null)
                GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[14] = "The royalists are destroyed ... Believe me, you are fighting for the right cause";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[16] = "The royalists are destroyed ... The main one will be furious";
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[16] = "Someone destroyed the royalists. It seems we need to side with the Republicans";
                if (knowAboutScroll)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "We need help with bandits";
                }
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
        else if (dialogue.personName == "Head of Republicans" && buttonTexts[1].text == "We need help with bandits")
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestRepublicanStageOne();
            buttons[1].gameObject.SetActive(false);
            main.text = "Of course. Service for service";
        }
        }
    public void HeadOfRepublicansQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeQuest == false && buttonTexts[3].text == "I will help you,if you pay well")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                main.text = "Great!I will be waiting for you here";
                headOfRepublicansTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRepublicansQuestRobberChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRepublicansQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (buttonTexts[3].text == "What do you think about war?" && dialogue.personName == "Head of Republicans")
            {
                main.text = dialogue.sentences[3];
            }
            else if (dialogue.personName == "Head of Republicans" && buttonTexts[3].text == "I have orders")
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "I want to join";
                buttonTexts[3].text = "What do you think about war?";
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = "There are traitors among us. We will pass this information to the main camp. By the way, we need another person to storm the camp of the royalists. Tell me if you want to join. Here is your reward";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_get_orders);
                    headOfRepublicansTakeQuest = true;
                    for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                            if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
                            {
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                                break;
                            }
                    }
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Get orders";
                    playerController.StartCoroutine("QuestCompleted");
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                    {
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponentInChildren<Image>().color == Color.red)
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                        Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").gameObject);
                    }
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward = 0;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward *= 2;
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[5] = "Thank you for your help with the orders of the royalists. We are happy to see you";
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
            else if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeSecondQuest == false && buttonTexts[3].text == "I will join,but I need money")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Let's go";
                buttons[5].gameObject.SetActive(false);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(false);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                main.text = "Great. See you at their camp";
                headOfRepublicansTakeSecondQuest = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansSecondQuestStageZero();
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansSecondQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRepublicansSecondQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward = 0;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRepublicansSecondQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal.Length; i++)
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[i]);
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Head of Republicans" && buttonTexts[3].text == "They are already dead")
            {
                buttons[1].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);
                main.text = "Good job.Thanks for your help.Tell me,if you want something";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_assault);
                    headOfRepublicansTakeSecondQuest = true;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward = 0;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward *= 2;
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Assault";
                    playerController.StartCoroutine("QuestCompleted");
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Thank you for your help. The Republic will not forget it";
                    if(GameObject.Find("Village Merchant")!=null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[14] = "The royalists are destroyed ... Believe me, you are fighting for the right cause";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[16] = "The royalists are destroyed ... The main one will be furious";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[16] = "Someone destroyed the royalists. It seems we need to side with the Republicans";
                    if (knowAboutScroll)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We need help with bandits";
                    }
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }
    public void HeadOfRepublicansQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeQuest == false && buttonTexts[3].text == "I will help you,but I need knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                main.text = "Great!I will be waiting for you here";
                headOfRepublicansTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRepublicansQuestMageChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints++;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRepublicansQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward != null)
                    spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (buttonTexts[3].text == "What do you think about war?" && dialogue.personName == "Head of Republicans")
            {
                main.text = dialogue.sentences[3];
            }
            else if (dialogue.personName == "Head of Republicans" && buttonTexts[3].text == "I have orders")
            {
                buttons[1].gameObject.SetActive(true);
                buttonTexts[1].text = "I want to join";
                buttonTexts[3].text = "What do you think about war?";
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                buttons[3].GetComponent<Image>().color = Color.white;
                main.text = "There are traitors among us. We will pass this information to the main camp. By the way, we need another person to storm the camp of the royalists. Tell me if you want to join. Here is your reward";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_get_orders);
                    headOfRepublicansTakeQuest = true;
                    for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                            if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
                            {
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                                GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                                break;
                            }
                    }
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Get orders";
                    playerController.StartCoroutine("QuestCompleted");
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                    {
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponentInChildren<Image>().color == Color.red)
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                        Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").gameObject);
                    }
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints++;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward = 0;
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[5] = "Thank you for your help with the orders of the royalists. We are happy to see you";
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
            else if (dialogue.personName == "Head of Republicans" && headOfRepublicansTakeSecondQuest == false && buttonTexts[3].text == "I will help you,but I need some knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Let's go";
                buttons[5].gameObject.SetActive(false);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(false);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                main.text = "Great. See you at their camp";
                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansSecondQuestStageZero();
                headOfRepublicansTakeSecondQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRepublicansSecondQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRepublicansSecondQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints++;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRepublicansSecondQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal.Length; i++)
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goal[i]);
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Head of Republicans" && buttonTexts[3].text == "They are already dead")
            {
                buttons[1].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);
                main.text = "Good job.Thanks for your help.Tell me,if you want something";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_assault);
                    headOfRepublicansTakeSecondQuest = true;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints++;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward = 0;
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Assault";
                    playerController.StartCoroutine("QuestCompleted");
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRepublicansSecondQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RepublicanDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Thank you for your help. The Republic will not forget it";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[16] = "The royalists are destroyed ... The main one will be furious";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[16] = "Someone destroyed the royalists. It seems we need to side with the Republicans";
                    if (knowAboutScroll)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We need help with bandits";
                    }
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }
    public void HeadOfRoyalistsQuestChoosePaladin()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeQuest == false && buttonTexts[3].text == "I will do my best")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                        {
                            buttons[1].gameObject.SetActive(true);
                            buttonTexts[1].text = "I have information about traitor";
                        }
                main.text = "Fine.You need to talk to the head of the village guard.He commanded this detachment before me";
                headOfRoyalistsTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRoyalistsQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRoyalistsQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
       else if (buttonTexts[3].text == "What do you think about war?" && dialogue.personName == "Head of Royalists")
            main.text = dialogue.sentences[3];
            else if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeSecondQuest == false && buttonTexts[3].text == "I will do my best" && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                main.text = "Good.Let's talk at the meeting point";
                headOfRoyalistsTakeSecondQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsSecondQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRoyalistsSecondQuestPaladinChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward *= 2;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward = 0;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRoyalistsSecondQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsSecondQuestStageZero();
            }
            else if (dialogue.personName == "Head of Royalists" && buttonTexts[3].text == "They are already dead")
            {
                buttons[1].gameObject.SetActive(false);
                main.text = "Good job.Thanks for your help.Tell me,if you want something";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_storm);
                    headOfRoyalistsTakeSecondQuest = true;
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Storm";
                    playerController.StartCoroutine("QuestCompleted");
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
                    if (GameObject.Find("Village Merchant") != null)
                        GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[15] = "Republicans are destroyed... Well, it's your choice";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[17] = "Someone destroyed the Republicans. It seems time to move to the side of the royalists";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[17] = "Republicans are destroyed ... The chief will be happy";
                    if (knowAboutScroll)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We need help with bandits";
                    }
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }
    public void HeadOfRoyalistsQuestChooseSimple()
    {
        if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeQuest == false && buttonTexts[1].text == "I will help you")
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "How's the war going?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you see the country after the war?";
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "I have information about traitor";
                    }
            main.text = "Fine.You need to talk to the head of the village guard.He commanded this detachment before me";
            headOfRoyalistsTakeQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfRoyalistsQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRoyalistsQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "Do you need a help?")
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                main.text = "I suspect that the guy who stands near a stone on the right.The group consists of 5 people.But he told that we are the main group therefore reinforcements were sent to us.Find out, whether really he is the traitor.";
            if(GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            main.text = "I suspect that the guy who stands near a stone on the right.The group consists of 5 people.But he told that we are the main group therefore reinforcements were sent to us.Find out, whether really he is the traitor.By the way, Republicans have a lot of respect for magicians. Try to talk to him";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                main.text = "I suspect that the guy who stands near a stone on the right.The group consists of 5 people.But he told that we are the main group therefore reinforcements were sent to us.Find out, whether really he is the traitor.Maybe he has evidence in his pocket";
            buttons[6].gameObject.SetActive(false);
            buttons[5].gameObject.SetActive(false);
            buttonTexts[1].text = "I will help you";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "I will think about your proposal";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                buttonTexts[3].text = "I will do my best";
                buttons[3].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttonTexts[3].text = "I will help you,but I need knowledge";
                buttons[3].GetComponent<Image>().color = Color.blue;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            {
                buttonTexts[3].text = "I will help you,if you pay well";
                buttons[3].GetComponent<Image>().color = Color.green;
            }
        }
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "I have information about traitor")
        {
            main.text = "I knew that... Eliminate him";
            buttons[1].gameObject.SetActive(false);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsQuestStageTwo();
        }
       else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "He is dead" && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive)
        {
            buttonTexts[1].text = "I want to join";
            main.text = "Good job. Here's your reward. If you want to show Republicans their place, let me know";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_traitor);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                        {
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<Image>().sprite = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().amountOfItems = 0;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item = null;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots--;
                            GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponentInChildren<Text>().text = "";
                            break;
                        }
                }
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Traitor";
                playerController.StartCoroutine("QuestCompleted");
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponentInChildren<Image>().color == Color.red)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").gameObject);
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[5] = "Thank you for your help with the traitor.I did not expect that there are rats of the Federation among us...";
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "I want to join")
        {
            main.text = "Okay. Then let's go?";
            buttons[6].gameObject.SetActive(false);
            buttons[5].gameObject.SetActive(false);
            buttonTexts[1].text = "I will join";
            buttons[2].gameObject.SetActive(true);
            buttonTexts[2].text = "I will think about your proposal";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            {
                buttonTexts[3].text = "I will do my best";
                buttons[3].GetComponent<Image>().color = Color.red;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            {
                buttonTexts[3].text = "I will help you,but I need knowledge";
                buttons[3].GetComponent<Image>().color = Color.blue;
            }
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            {
                buttonTexts[3].text = "I will help you,if you pay well";
                buttons[3].GetComponent<Image>().color = Color.green;
            }
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest") == null)
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().killedRepublicans== 5)
                {
                    buttonTexts[1].text = "They are already dead";
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                    {
                        buttonTexts[3].text = "They are already dead";
                        buttons[3].GetComponent<Image>().color = Color.green;
                    }
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                    {
                        buttonTexts[3].text = "They are already dead";
                        buttons[3].GetComponent<Image>().color = Color.red;
                    }
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                    {
                        buttonTexts[3].text = "They are already dead";
                        buttons[3].GetComponent<Image>().color = Color.blue;
                    }
                }
        }
        else if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeSecondQuest == false && buttonTexts[1].text == "I will join"&&GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
        {
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[3].gameObject.SetActive(true);
            buttonTexts[3].text = "What do you think about war?";
            buttonTexts[0].text = "Bye";
            buttons[5].gameObject.SetActive(true);
            buttonTexts[5].text = "How's the war going?";
            buttons[6].gameObject.SetActive(true);
            buttonTexts[6].text = "What do you see the country after the war?";
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            main.text = "Good.Let's talk at the meeting point";
            headOfRoyalistsTakeSecondQuest = true;
            playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
            IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsSecondQuest");
            playerController.StartCoroutine(newQuestThenNewStage);
            headOfRoyalistsSecondQuestSimpleChoosed = true;
            GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
            spawn.name = "HeadOfRoyalistsSecondQuest";
            spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
            GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = true;
            spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
            spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
            spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.description;
            spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
            spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
            spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
            spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward;
            for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal.Length; i++)
            {
                spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[i]);
            }
            if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsSecondQuestStageZero();
        }
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "For the King!")
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsSecondQuestStageOne();
            CloseDialogue();
        }
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "We did it!")
        {
            buttons[1].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            main.text = "Good job.Thanks for your help.Tell me,if you want something";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_storm);
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Storm";
                playerController.StartCoroutine("QuestCompleted");
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").GetComponentInChildren<Image>().color == Color.red)
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsSecondQuest").gameObject);
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
                if (GameObject.Find("Village Merchant") != null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[15] = "Republicans are destroyed... Well, it's your choice";
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[17] = "Someone destroyed the Republicans. It seems time to move to the side of the royalists";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[17] = "Republicans are destroyed ... The chief will be happy";
                if (knowAboutScroll)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "We need help with bandits";
                }
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "They are already dead")
        {
            buttons[1].gameObject.SetActive(false);
            main.text = "Good job.Thanks for your help.Tell me,if you want something";
            bool havePlace = false;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward == null)
                havePlace = true;
            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward != null)
            {
                havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward);
            }
            if (havePlace)
            {
                GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_storm);
                headOfRoyalistsTakeSecondQuest = true;
                playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Storm";
                playerController.StartCoroutine("QuestCompleted");
                playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints;
                playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted = true;
                playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = false;
                GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
                if (GameObject.Find("Village Merchant") != null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[15] = "Republicans are destroyed... Well, it's your choice";
                GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[17] = "Someone destroyed the Republicans. It seems time to move to the side of the royalists";
                GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[17] = "Republicans are destroyed ... The chief will be happy";
                if (knowAboutScroll)
                {
                    buttons[1].gameObject.SetActive(true);
                    buttonTexts[1].text = "We need help with bandits";
                }
            }
            else
                main.text = "You don't have place in inventory for reward";
        }
        else if (dialogue.personName == "Head of Royalists" && buttonTexts[1].text == "We need help with bandits")
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().extraWarriorsInArmy = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestRoyalistsStageOne();
            buttons[1].gameObject.SetActive(false);
            main.text = "Of course. Service for service";
        }
    }
    public void HeadOfRoyalistsQuestChooseRobber()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
            if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeQuest == false && buttonTexts[3].text == "I will help you,if you pay well")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item != null)
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                        {
                            buttons[1].gameObject.SetActive(true);
                            buttonTexts[1].text = "I have information about traitor";
                        }
                main.text = "Fine.You need to talk to the head of the village guard.He commanded this detachment before me";
                headOfRoyalistsTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRoyalistsQuestRobberChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRoyalistsQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (buttonTexts[3].text == "What do you think about war?" && dialogue.personName == "Head of Royalists")
                main.text = dialogue.sentences[3];
            else if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeSecondQuest == false && buttonTexts[3].text == "I will help you,if you pay well" && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                main.text = "Good.Let's talk at the meeting point";
                headOfRoyalistsTakeSecondQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsSecondQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRoyalistsSecondQuestRobberChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward *= 2;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRoyalistsSecondQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsSecondQuestStageZero();
            }
            else if (dialogue.personName == "Head of Royalists" && buttonTexts[3].text == "They are already dead")
            {
                buttons[1].gameObject.SetActive(false);
                main.text = "Good job.Thanks for your help.Tell me,if you want something";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_storm);
                    headOfRoyalistsTakeSecondQuest = true;
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Storm";
                    playerController.StartCoroutine("QuestCompleted");
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
                    if (GameObject.Find("Village Merchant") != null)
                        GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[15] = "Republicans are destroyed... Well, it's your choice";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[17] = "Someone destroyed the Republicans. It seems time to move to the side of the royalists";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[17] = "Republicans are destroyed ... The chief will be happy";
                    if (knowAboutScroll)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We need help with bandits";
                    }
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }
    public void HeadOfRoyalistsQuestChooseMage()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
            if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeQuest == false && buttonTexts[3].text == "I will help you,but I need knowledge")
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                for (int i = 0; i < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; i++)
                    if(GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item!=null)
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "I have information about traitor";
                    }
                main.text = "Fine.You need to talk to the head of the village guard.He commanded this detachment before me";
                headOfRoyalistsTakeQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRoyalistsQuestMageChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward = 0;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.skillPoints++;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRoyalistsQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().skillPoints = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.skillPoints;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward != null)
                    spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (buttonTexts[3].text == "What do you think about war?" && dialogue.personName == "Head of Royalists")
                main.text = dialogue.sentences[3];
            else if (dialogue.personName == "Head of Royalists" && headOfRoyalistsTakeSecondQuest == false && buttonTexts[3].text == "I will help you,but I need knowledge" && GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsQuest.questCompleted)
            {
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "How's the war going?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "What do you see the country after the war?";
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                main.text = "Good.Let's talk at the meeting point";
                headOfRoyalistsTakeSecondQuest = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("HeadOfRoyalistsSecondQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                headOfRoyalistsSecondQuestMageChoosed = true;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints++;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward = 0;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "HeadOfRoyalistsSecondQuest";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsSecondQuestStageZero();
            }
            else if (dialogue.personName == "Head of Royalists" && buttonTexts[3].text == "They are already dead")
            {
                buttons[1].gameObject.SetActive(false);
                main.text = "Good job.Thanks for your help.Tell me,if you want something";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_storm);
                    headOfRoyalistsTakeSecondQuest = true;
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Storm";
                    playerController.StartCoroutine("QuestCompleted");
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().headOfRoyalistsSecondQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    GameObject.Find("RoyalistDialogs").GetComponent<Dialogue>().sentences[6] = "We did it! Now we will show the Republicans their place!";
                    if (GameObject.Find("Village Merchant") != null)
                        GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[15] = "Republicans are destroyed... Well, it's your choice";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[17] = "Someone destroyed the Republicans. It seems time to move to the side of the royalists";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[17] = "Republicans are destroyed ... The chief will be happy";
                    if (knowAboutScroll)
                    {
                        buttons[1].gameObject.SetActive(true);
                        buttonTexts[1].text = "We need help with bandits";
                    }
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }
    public void PriestSpecialQuest()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
            if (dialogue.personName == "Priest" && !priestSpecialQuestTake && buttonTexts[1].text == "I will help you")
            {
                buttons[3].gameObject.SetActive(true);
                buttonTexts[3].text = "What do you think about war?";
                buttons[2].gameObject.SetActive(true);
                buttonTexts[2].text = "What about Order?";
                buttonTexts[0].text = "Bye";
                buttons[5].gameObject.SetActive(true);
                buttonTexts[5].text = "Сan you tell me about Bamur?";
                buttons[6].gameObject.SetActive(true);
                buttonTexts[6].text = "Can you tell me about Artelit";
                buttons[0].gameObject.SetActive(true);
                buttonTexts[0].text = "Bye";
                buttons[3].GetComponent<Image>().color = Color.white;
                buttons[1].gameObject.SetActive(false);
                buttons[1].GetComponent<Image>().color = Color.white;
                buttons[3].gameObject.SetActive(true);
                priestSpecialQuestTake = true;
                playerController.newQuest.GetComponentInChildren<Text>().text = "New quest:" + GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.questName;
                IEnumerator newQuestThenNewStage = GameObject.Find("Player").GetComponent<PlayerController>().NewQuestThenNewStage("PaladinSpecialQuest");
                playerController.StartCoroutine(newQuestThenNewStage);
                priestSpecialQuestTake = true;
                GameObject spawn = Instantiate(GameObject.Find("QuestManager").GetComponent<QuestManager>().sideQuestPrefab, GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform);
                spawn.name = "PaladinSpecialQuest";
                main.text = "Thank you. May Artelit protect you";
                spawn.GetComponent<Button>().onClick.AddListener(GameObject.Find("QuestManager").GetComponent<QuestManager>().SetQuestInfo);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.isActive = true;
                spawn.GetComponent<QuestSlot>().questName = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.questName;
                spawn.GetComponentInChildren<Text>().text = spawn.GetComponent<QuestSlot>().questName;
                spawn.GetComponent<QuestSlot>().description = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.description;
                spawn.GetComponent<QuestSlot>().goldReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goldReward;
                spawn.GetComponent<QuestSlot>().experienceReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.experienceReward;
                spawn.GetComponent<QuestSlot>().prestigeReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.prestigeReward;
                spawn.GetComponent<QuestSlot>().objectReward = GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.objectReward;
                for (int i = 0; i < GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goal.Length; i++)
                {
                    spawn.GetComponent<QuestSlot>().goal.Add(GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goal[i]);
                }
                if (!GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest && GameObject.Find("GameManager").GetComponent<GameManager>().withQuestMarkers)
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
            else if (dialogue.personName == "Priest" && buttonTexts[1].text == "Do you need a help?")
            {
                buttons[6].gameObject.SetActive(false);
                buttons[5].gameObject.SetActive(false);
                buttonTexts[1].text = "I will help you";
                buttons[3].gameObject.SetActive(false);
                buttonTexts[2].text = "I will think about it";
                main.text = "A few days ago, the mountain temple was captured by the soldiers of Bamur.I hear their disgusting influence right here.Please kill them until they gain strength";
            }
            else if (dialogue.personName == "Priest" && buttonTexts[1].text == "I have spoken with Artelit...")
            {
                buttons[1].gameObject.SetActive(false);
                main.text = "You know, I believe you. I felt it a few weeks ago. Something has changed. Go to the Capital region. You have to talk to Her Holiness";
                bool havePlace = false;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.objectReward == null)
                    havePlace = true;
                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.objectReward != null)
                {
                    havePlace = GameObject.Find("QuestManager").GetComponent<QuestManager>().CheckIfHavePlace(GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.objectReward);
                }
                if (havePlace)
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_shrine);
                    playerController.questCompleted.GetComponentInChildren<Text>().text = "Quest completed:Shrine";
                    playerController.StartCoroutine("QuestCompleted");
                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").GetComponentInChildren<Image>().color == Color.red)
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().hasActivaQuest = false;
                    Destroy(GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("PaladinSpecialQuest").gameObject);
                    playerController.skillPoints += GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.skillPoints;
                    playerController.prestige += GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.prestigeReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.questCompleted = true;
                    playerController.experience += GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.experienceReward;
                    playerController.gold += GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.goldReward;
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().paladinSpecialQuest.isActive = false;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().compass.transform.Find("Compass").Find("QuestMarker").gameObject.SetActive(false);
                    if(GameObject.Find("Village Merchant")!=null)
                    GameObject.Find("Village Merchant").GetComponent<Dialogue>().sentences[10] = "I heard that you destroyed Bamur's forces in the mountains. Great job";
                    GameObject.Find("SimplePeopleDialogs").GetComponent<Dialogue>().sentences[13] = "The priest says that you are our savior. Is this true?";
                    GameObject.Find("GuardDialogs").GetComponent<Dialogue>().sentences[13] = "I felt it when you cleaned the sanctuary of Artelit. Thank you for your help";
                }
                else
                    main.text = "You don't have place in inventory for reward";
            }
    }

    //Recognize clicked button
    public void SetClickedButtonName()
    {
        clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        clickedButtonObject= EventSystem.current.currentSelectedGameObject.gameObject;
        if (clickedButtonName == "DialogAnswer5" && buttonTexts[2].text == "Why village is so empty?")
         CivilWarTreeActivated = true;
    }
    private void LoadDialogueManager()
    {
        headOfVillageQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfVillageQuestMageChoosed;
        headOfVillageQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfVillageQuestPaladinChoosed;
        headOfVillageQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfVillageQuestRobberChoosed;
        headOfVillageQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfVillageQuestSimpleChoosed;
        headOfVillageTakeQuest = SaveLoad.globalDialogueManagerData.headOfVillageTakeQuest;
        fayeHelpSaid1 = SaveLoad.globalDialogueManagerData.fayeHelpSaid1;
        fayeHelpSaid2 = SaveLoad.globalDialogueManagerData.fayeHelpSaid2;
        fayeTakeQuest = SaveLoad.globalDialogueManagerData.fayeTakeQuest;
        fayeQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.fayeQuestPaladinChoosed;
        fayeQuestMageChoosed = SaveLoad.globalDialogueManagerData.fayeQuestMageChoosed;
        fayeQuestRobberChoosed = SaveLoad.globalDialogueManagerData.fayeQuestRobberChoosed;
        fayeQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.fayeQuestSimpleChoosed;
        fayeYesNoAnswers = SaveLoad.globalDialogueManagerData.fayeYesNoAnswers;
        rewardFromSister = SaveLoad.globalDialogueManagerData.rewardFromSister;
        librarianSurveyQuestMageChoosed = SaveLoad.globalDialogueManagerData.librarianSurveyQuestMageChoosed;
        librarianSurveyQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.librarianSurveyQuestPaladinChoosed;
        librarianSurveyQuestRobberChoosed = SaveLoad.globalDialogueManagerData.librarianSurveyQuestRobberChoosed;
        librarianSurveyQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.librarianSurveyQuestSimpleChoosed;
        librarianSurveyTakeQuest = SaveLoad.globalDialogueManagerData.librarianSurveyTakeQuest;
        librarianSpecialTakeQuest = SaveLoad.globalDialogueManagerData.librarianSpecialTakeQuest;
        headOfGuardQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfGuardQuestMageChoosed;
        headOfGuardQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfGuardQuestPaladinChoosed;
        headOfGuardQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfGuardQuestRobberChoosed;
        headOfGuardQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfGuardQuestSimpleChoosed;
        headOfGuardTakeQuest = SaveLoad.globalDialogueManagerData.headOfGuardTakeQuest;
        headOfHuntersTakeQuest = SaveLoad.globalDialogueManagerData.headOfHuntersTakeQuest;
        headOfHuntersQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfHuntersQuestSimpleChoosed;
        headOfHuntersQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfHuntersQuestRobberChoosed;
        headOfHuntersQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfHuntersQuestPaladinChoosed;
        headOfHuntersQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfHuntersQuestMageChoosed;
        headOfRepublicansTakeQuest = SaveLoad.globalDialogueManagerData.headOfRepublicansTakeQuest;
        headOfRepublicansQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansQuestSimpleChoosed;
        headOfRepublicansQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansQuestRobberChoosed;
        headOfRepublicansQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansQuestPaladinChoosed;
        headOfRepublicansQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansQuestMageChoosed;
        headOfRepublicansSecondQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansSecondQuestMageChoosed;
        headOfRepublicansSecondQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansSecondQuestPaladinChoosed;
        headOfRepublicansSecondQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansSecondQuestRobberChoosed;
        headOfRepublicansSecondQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfRepublicansSecondQuestSimpleChoosed;
        headOfRepublicansTakeSecondQuest = SaveLoad.globalDialogueManagerData.headOfRepublicansTakeSecondQuest;
        headOfRoyalistsTakeQuest = SaveLoad.globalDialogueManagerData.headOfRoyalistsTakeQuest;
        headOfRoyalistsQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsQuestSimpleChoosed;
        headOfRoyalistsQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsQuestRobberChoosed;
        headOfRoyalistsQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsQuestPaladinChoosed;
        headOfRoyalistsQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsQuestMageChoosed;
        headOfRoyalistsTakeSecondQuest = SaveLoad.globalDialogueManagerData.headOfRoyalistsTakeSecondQuest;
        headOfRoyalistsSecondQuestSimpleChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsSecondQuestSimpleChoosed;
        headOfRoyalistsSecondQuestRobberChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsSecondQuestRobberChoosed;
        headOfRoyalistsSecondQuestPaladinChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsSecondQuestPaladinChoosed;
        headOfRoyalistsSecondQuestMageChoosed = SaveLoad.globalDialogueManagerData.headOfRoyalistsSecondQuestMageChoosed;
        priestSpecialQuestTake = SaveLoad.globalDialogueManagerData.priestSpecialQuestTake;
        armyStageTwo = SaveLoad.globalDialogueManagerData.armyStageTwo;
        knowAboutScroll = SaveLoad.globalDialogueManagerData.knowAboutScroll;
        villageGuardStageOne = SaveLoad.globalDialogueManagerData.villageGuardStageOne;
        extraWarriorsInArmy = SaveLoad.globalDialogueManagerData.extraWarriorsInArmy;
        soloveySisterHelped = SaveLoad.globalDialogueManagerData.soloveySisterHelped;
        headOfGuardKilled = SaveLoad.globalDialogueManagerData.headOfGuardKilled;
        knowAboutOffer = SaveLoad.globalDialogueManagerData.knowAboutOffer;
    }
}
