using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour
{
    public GameObject inventoryIsFull;
    public Image[] images;
    public Image[] lootSlots;
    public Image weaponImage;
    public Image helmetImage;
    public Image armorImage;
    public Image shieldImage;
    public GameObject itemInfoInventory;
    public GameObject selectedItem;
    private PlayerController player;
    public Image timeImage;
    public int filledSlots = 0;
    private bool moveWasUsed = false;
    public bool hasArrows = false;
    //Some variables to set item in icons
    public GameObject daggerPrefab;
    public GameObject waraxePrefab;
    public GameObject bowPrefab;
    public GameObject arrowPrefab;
    public GameObject fireballPrefab;
    public GameObject healthResurection;
    public GameObject staminaResurection;
    public GameObject meleeSummon;
    public GameObject archerSummon;
    public GameObject cavalryHelmet;
    public GameObject militiaHelmet;
    public GameObject ironHelmet;
    public GameObject soldierHelmet;
    public GameObject militiaArmor;
    public GameObject ironArmor;
    public GameObject cavalryArmor;
    public GameObject soldierArmor;
    public GameObject militiaShield;
    public GameObject cheapShield;
    public GameObject professionalBow;
    public GameObject bigSoldierSword;
    public GameObject simpleHealthPotion;
    public GameObject smallHealthPotion;
    public GameObject hugeHealthPotion;
    public GameObject smallStaminaPotion;
    private int globalFreeIndex = 0;
    private int prestigeDiscount = 0;
    [SerializeField] public GameObject simpleStaminaPotion;
    [SerializeField] public GameObject hugeStaminaPotion;
    [SerializeField] public GameObject smallManaPotion;
    [SerializeField] public GameObject simpleManaPotion;
    [SerializeField] public GameObject hugeManaPotion;
    [SerializeField] public GameObject ironSword;
    [SerializeField] public GameObject bigIronSword;
    [SerializeField] public GameObject militiaSword;
    [SerializeField] public GameObject bigMilitiaSword;
    [SerializeField] public GameObject soldierSword;
    [SerializeField] public GameObject bigWaraxe;
    [SerializeField] public GameObject needMoreMoney;
    [SerializeField] public GameObject militiaBow;
    [SerializeField] public GameObject headNote;
    [SerializeField] public GameObject meat;
    [SerializeField] public GameObject professionalLockpick;
    [SerializeField] public GameObject oneLockpick;
    [SerializeField] public GameObject roastedMeat;
    [SerializeField] public GameObject wineBottle;
    [SerializeField] public GameObject beerBottle;
    [SerializeField] public GameObject bread;
    [SerializeField] public GameObject meatPie;
    [SerializeField] public GameObject egg;
    [SerializeField] public GameObject omelette;
    [SerializeField] public GameObject slaviaMushroom;
    [SerializeField] public GameObject hellMushroom;
    [SerializeField] public GameObject damagePotion;
    [SerializeField] public GameObject armorPotion;
    [SerializeField] public GameObject pickpocketingPotion;
    [SerializeField] public GameObject breakingPotion;
    [SerializeField] public GameObject warriorPotion;
    [SerializeField] public GameObject archimagePotion;
    [SerializeField] public GameObject runnerPotion;
    [SerializeField] public GameObject specialMushroom;
    [SerializeField] public GameObject royalistsOrders;
    [SerializeField] public GameObject republicansOrders;
    [SerializeField] public GameObject artelitSword;
    [SerializeField] public GameObject stormSpell;
    [SerializeField] public GameObject oldBook;
    [SerializeField] public GameObject dragonScroll;
    public Text specialAbilityItemText;
    public GameObject needMoreSpace;
    public Text amountOfGold;
    public int attackPotionTime = 60;
    public bool potionAttackActivated = false;
    public int potionArmorTime = 60;
    public bool potionArmorActivated = false;
    public int pickpocketingPotionTime = 60;
    public bool pickpocketingPotionActivated = false;
    public int breakingPotionTime = 60;
    public bool breakingPotionActivated=false;
    public int warriorPotionTime = 60;
    public bool warriorPotionActivated = false;
    public int archimagePotionTime = 60;
    public bool archimagePotionActivated = false;
    public int runnerPotionTime = 60;
    public bool runnerPotionActivated = false;
    private GUIController guiController;
    private void Start()
    {
        guiController = GetComponent<GUIController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameObject.Find("GUIManager").GetComponent<Inventory>().selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
        if (SaveLoad.isLoading)
        {
            LoadInventory();
            if (potionAttackActivated==true)
            {
                StartCoroutine("PotionAttackControl");
                player.GetComponent<PlayerController>().potionAttackModify = 10;
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            }
           if (potionArmorActivated == true)
            {
                StartCoroutine("PotionArmorControl");
                player.GetComponent<PlayerController>().potionArmorModify = 10;
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
            if (pickpocketingPotionActivated == true)
                StartCoroutine("PotionPickpocketingControl");
            if (breakingPotionActivated == true)
                StartCoroutine("PotionBreakingControl");
            if (warriorPotionActivated== true)
                StartCoroutine("PotionWarriorControl");
            if (archimagePotionActivated == true)
                StartCoroutine("PotionArchimageControl");
            if (runnerPotionActivated == true)
                StartCoroutine("PotionRunnerControl");
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isRobber)
                dragonScroll.GetComponent<Dialogue>().sentences[0] = "[The text is written in a language unknown to you.You can't make out anything. Hope the customer knows what he's buying]";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isMage)
                dragonScroll.GetComponent<Dialogue>().sentences[0] = "[The text is written in a language unknown to you.You can't make out anything. Hope the Academy knows something about it]";
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPaladin)
                dragonScroll.GetComponent<Dialogue>().sentences[0] = "[The text is written in a language unknown to you.You can't make out anything. Hope the Order knows something about this]";
        }
    }
    private void Update()
    {
        amountOfGold.text = "Gold:" + player.gold;
        if (globalFreeIndex == GameObject.FindGameObjectsWithTag("FreeShopIcon").Length - 1)
            globalFreeIndex = 0;
        if (guiController.shop.activeSelf)
        {
            if (selectedItem.gameObject.tag == "ShopIcon")
                guiController.inventoryUI.transform.Find("Buy").GetComponentInChildren<Text>().text = "Buy";
            else
                guiController.inventoryUI.transform.Find("Buy").GetComponentInChildren<Text>().text = "Sold";
        }
        //Equip when RMB is pressed
        if (Input.GetKeyDown(KeyCode.Mouse1))
            EquipUnequip();
    }
    //Take Item function
    public void Take(GameObject itemObject,int index)
    {
        bool gotInStack = false;
        if (itemObject != null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                //Firstly check if there are unfull stacks in inventory
                if (itemObject.GetComponent<Item>().amountInStack > 1)
                    for (int m = 0; m < images.Length; m++)
                        if (itemObject.GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < itemObject.GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == itemObject.GetComponent<Image>().sprite)
                        {
                            images[m].GetComponent<SlotInfo>().amountOfItems++;
                            if (GameObject.Find("GUIManager").GetComponent<GUIController>().lootWindow.activeSelf)
                            {
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>() != null)
                                    GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[index]--;
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>() != null)
                                    GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[index]--;
                            }
                            if (selectedItem.tag == "FreeShopIcon" || selectedItem.tag == "ShopIcon")
                                GameObject.Find("Player").GetComponent<PlayerController>().gold -= (int)(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost - selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost * prestigeDiscount / 100);
                            gotInStack = true;
                            if (itemObject.GetComponent<Item>().itemName == "Arrow" || itemObject.GetComponent<Item>().itemName == "Стрела" || itemObject.GetComponent<Item>().itemName == "Стріла")
                            {
                                hasArrows = true;
                            }
                            break;
                        }
                //End searching if unfull stack found
                if (gotInStack)
                    break;
                //Fill in empty slot in inventory
                if (images[i].sprite == null)
                {
                    if (selectedItem!=null)
                    if (selectedItem.tag == "FreeShopIcon" || selectedItem.tag == "ShopIcon")
                        GameObject.Find("Player").GetComponent<PlayerController>().gold -= (int)(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost - selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost * prestigeDiscount / 100);
                    if (GameObject.Find("GUIManager").GetComponent<GUIController>().lootWindow.activeSelf && GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>() != null)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[index]--;
                        if(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot.Length>=2)
                        if(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[1]!=null)
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage < 4)
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[1].GetComponent<Item>().itemName == "Royalists orders")
                                    GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansQuestStageFour();
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 1)
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[1].GetComponent<Item>().itemName == "Republican's orders")
                                    GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsQuestStageOne();
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage < 1)
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[1].GetComponent<Item>().itemName == "Old book")
                                    GameObject.Find("QuestManager").GetComponent<QuestManager>().LibrarianSpecialQuestStageOne();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[1].GetComponent<Item>().itemName == "Dragon scroll")
                            GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestStageOne();
                    }
                    else if (GameObject.Find("GUIManager").GetComponent<GUIController>().lootWindow.activeSelf && GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>() != null)
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[index]--;
                    images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                    images[i].sprite = itemObject.GetComponent<Image>().sprite;
                    images[i].GetComponent<SlotInfo>().amountOfItems = 1;
                    images[i].GetComponent<SlotInfo>().item = itemObject.GetComponent<Item>().item;
                    if (filledSlots < 60)
                        filledSlots++;
                    if (itemObject.GetComponent<Item>().itemName == "Arrow" || itemObject.GetComponent<Item>().itemName == "Стрела" || itemObject.GetComponent<Item>().itemName == "Стріла")
                    {
                        hasArrows = true;
                    }
                    if (itemObject.GetComponent<Item>().itemName == "Artelis mushroom")
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfHuntersStageOne();
                    break;
                }
                //Show Inventory Full if inventory is full
                if (i == 59 && images[i].GetComponent<SlotInfo>().amountOfItems == itemObject.GetComponent<Item>().amountInStack && images[i].sprite != null)
                {
                    inventoryIsFull.transform.localPosition = new Vector3(19.3f, -294, 0);
                    inventoryIsFull.SetActive(true);
                    StartCoroutine("CloseMessage");
                }
            }
        }
    }
    //Take all loot from loot window
    public void TakeAll()
    {
        if(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>()!=null)
        for (int i = 0; i < GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot.Length; i++)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i] != null)
            {
                    bool inventoryFull = false;
                while (true)
                {
                        bool mainBreak = false;
                    Take(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i], i);
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i].GetComponent<Item>().amountInStack == 1)
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[i] == 0)
                                break;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i].GetComponent<Item>().amountInStack == 1)
                            if (filledSlots == 60)
                            {
                                inventoryFull = true;
                                break;                               
                            }
                                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i].GetComponent<Item>().amountInStack > 1)
                            for (int m = 0; m < images.Length; m++)
                                if (m == 59 && filledSlots == 60)
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i].GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i].GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[i].GetComponent<Image>().sprite)
                                    {

                                    }
                                    else
                                    {
                                        inventoryFull = true;
                                        mainBreak = true;
                                    }
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[i] == 0)
                            mainBreak = true;
                        if (mainBreak)
                            break;
                }
                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[i] == 0)
                {
                    if (i == 0)
                    {
                        GameObject.Find("1").GetComponent<Image>().color = Color.white;
                        GameObject.Find("1").GetComponent<Image>().sprite = null;
                        GameObject.Find("1").GetComponent<SlotInfo>().item = null;
                        GameObject.Find("1").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        selectedItem = GameObject.Find("TimeImageForInventory");
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[0] = null;
                    }
                    else if (i == 1)
                    {
                        GameObject.Find("2").GetComponent<Image>().color = Color.white;
                        GameObject.Find("2").GetComponent<Image>().sprite = null;
                        GameObject.Find("2").GetComponent<SlotInfo>().item = null;
                        GameObject.Find("2").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        selectedItem = GameObject.Find("TimeImageForInventory");
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[1] = null;
                    }
                    else if (i == 2)
                    {
                        GameObject.Find("3").GetComponent<Image>().color = Color.white;
                        GameObject.Find("3").GetComponent<Image>().sprite = null;
                        GameObject.Find("3").GetComponent<SlotInfo>().item = null;
                        GameObject.Find("3").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        selectedItem = GameObject.Find("TimeImageForInventory");
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[2] = null;
                    }
                    else if (i == 3)
                    {
                        GameObject.Find("4").GetComponent<Image>().color = Color.white;
                        GameObject.Find("4").GetComponent<Image>().sprite = null;
                        GameObject.Find("4").GetComponent<SlotInfo>().item = null;
                        GameObject.Find("4").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        selectedItem = GameObject.Find("TimeImageForInventory");
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[3] = null;
                    }
                    else if (i == 4)
                    {
                        GameObject.Find("5").GetComponent<Image>().color = Color.white;
                        GameObject.Find("5").GetComponent<Image>().sprite = null;
                        GameObject.Find("5").GetComponent<SlotInfo>().item = null;
                        GameObject.Find("5").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        selectedItem = GameObject.Find("TimeImageForInventory");
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[4] = null;
                    }
                    else if (i == 5)
                    {
                        GameObject.Find("6").GetComponent<Image>().color = Color.white;
                        GameObject.Find("6").GetComponent<Image>().sprite = null;
                        GameObject.Find("6").GetComponent<SlotInfo>().item = null;
                        GameObject.Find("6").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        selectedItem = GameObject.Find("TimeImageForInventory");
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[5] = null;
                    }
                }
                //Show Inventory Full if inventory is full
                if (inventoryFull)
                {
                    inventoryIsFull.SetActive(true);
                    StartCoroutine("CloseMessage");
                }
                if (i == 0)
                {
                    GameObject.Find("1").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[0];
                    GameObject.Find("1").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[0].ToString();
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[0] <= 1)
                        GameObject.Find("1").GetComponentInChildren<Text>().text = "";
                }
                else if (i == 1)
                {
                    GameObject.Find("2").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[1];
                    GameObject.Find("2").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[1].ToString();
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[1] <= 1)
                        GameObject.Find("2").GetComponentInChildren<Text>().text = "";
                }
                else if (i == 2)
                {
                    GameObject.Find("3").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[2];
                    GameObject.Find("3").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[2].ToString();
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[2] <= 1)
                        GameObject.Find("3").GetComponentInChildren<Text>().text = "";
                }
                else if (i == 3)
                {
                    GameObject.Find("4").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[3];
                    GameObject.Find("4").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[3].ToString();
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[3] <= 1)
                        GameObject.Find("4").GetComponentInChildren<Text>().text = "";
                }
                else if (i == 4)
                {
                    GameObject.Find("5").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[4];
                    GameObject.Find("5").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[4].ToString();
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[4] <= 1)
                        GameObject.Find("5").GetComponentInChildren<Text>().text = "";
                }
                else if (i == 5)
                {
                    GameObject.Find("6").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[5];
                    GameObject.Find("6").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[5].ToString();
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[5] <= 1)
                        GameObject.Find("6").GetComponentInChildren<Text>().text = "";
                }
            }
        }
        if(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>()!=null)   
            for (int i = 0; i < GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot.Length; i++)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i] != null)
                {
                    bool inventoryFull = false;
                    while (true)
                    {
                        bool mainBreak = false;
                        Take(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i], i);
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i].GetComponent<Item>().amountInStack == 1)
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[i] == 0)
                                break;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i].GetComponent<Item>().amountInStack == 1)
                            if (filledSlots == 60)
                            {
                                inventoryFull = true;
                                break;
                            }
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i].GetComponent<Item>().amountInStack > 1)
                            for (int m = 0; m < images.Length; m++)
                                if (m == 59 && filledSlots == 60)
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i].GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i].GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[i].GetComponent<Image>().sprite)
                                    {

                                    }
                                    else
                                    {
                                        inventoryFull = true;
                                        mainBreak = true;
                                    }
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[i] == 0)
                            mainBreak = true;
                        if (mainBreak)
                            break;
                    }
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[i] == 0)
                    {
                        if (i == 0)
                        {
                            GameObject.Find("1").GetComponent<Image>().color = Color.white;
                            GameObject.Find("1").GetComponent<Image>().sprite = null;
                            GameObject.Find("1").GetComponent<SlotInfo>().item = null;
                            GameObject.Find("1").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            selectedItem = GameObject.Find("TimeImageForInventory");
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[0] = null;
                        }
                        else if (i == 1)
                        {
                            GameObject.Find("2").GetComponent<Image>().color = Color.white;
                            GameObject.Find("2").GetComponent<Image>().sprite = null;
                            GameObject.Find("2").GetComponent<SlotInfo>().item = null;
                            GameObject.Find("2").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            selectedItem = GameObject.Find("TimeImageForInventory");
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[1] = null;
                        }
                        else if (i == 2)
                        {
                            GameObject.Find("3").GetComponent<Image>().color = Color.white;
                            GameObject.Find("3").GetComponent<Image>().sprite = null;
                            GameObject.Find("3").GetComponent<SlotInfo>().item = null;
                            GameObject.Find("3").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            selectedItem = GameObject.Find("TimeImageForInventory");
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[2] = null;
                        }
                        else if (i == 3)
                        {
                            GameObject.Find("4").GetComponent<Image>().color = Color.white;
                            GameObject.Find("4").GetComponent<Image>().sprite = null;
                            GameObject.Find("4").GetComponent<SlotInfo>().item = null;
                            GameObject.Find("4").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            selectedItem = GameObject.Find("TimeImageForInventory");
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[3] = null;
                        }
                        else if (i == 4)
                        {
                            GameObject.Find("5").GetComponent<Image>().color = Color.white;
                            GameObject.Find("5").GetComponent<Image>().sprite = null;
                            GameObject.Find("5").GetComponent<SlotInfo>().item = null;
                            GameObject.Find("5").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            selectedItem = GameObject.Find("TimeImageForInventory");
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[4] = null;
                        }
                        else if (i == 5)
                        {
                            GameObject.Find("6").GetComponent<Image>().color = Color.white;
                            GameObject.Find("6").GetComponent<Image>().sprite = null;
                            GameObject.Find("6").GetComponent<SlotInfo>().item = null;
                            GameObject.Find("6").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            selectedItem = GameObject.Find("TimeImageForInventory");
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[5] = null;
                        }
                    }
                    //Show Inventory Full if inventory is full
                    if (inventoryFull)
                    {
                        inventoryIsFull.SetActive(true);
                        StartCoroutine("CloseMessage");
                    }
                    if (i == 0)
                    {
                        GameObject.Find("1").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[0];
                        GameObject.Find("1").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[0].ToString();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[0] <= 1)
                            GameObject.Find("1").GetComponentInChildren<Text>().text = "";
                    }
                    else if (i == 1)
                    {
                        GameObject.Find("2").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[1];
                        GameObject.Find("2").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[1].ToString();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[1] <= 1)
                            GameObject.Find("2").GetComponentInChildren<Text>().text = "";
                    }
                    else if (i == 2)
                    {
                        GameObject.Find("3").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[2];
                        GameObject.Find("3").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[2].ToString();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[2] <= 1)
                            GameObject.Find("3").GetComponentInChildren<Text>().text = "";
                    }
                    else if (i == 3)
                    {
                        GameObject.Find("4").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[3];
                        GameObject.Find("4").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[3].ToString();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[3] <= 1)
                            GameObject.Find("4").GetComponentInChildren<Text>().text = "";
                    }
                    else if (i == 4)
                    {
                        GameObject.Find("5").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[4];
                        GameObject.Find("5").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[4].ToString();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[4] <= 1)
                            GameObject.Find("5").GetComponentInChildren<Text>().text = "";
                    }
                    else if (i == 5)
                    {
                        GameObject.Find("6").GetComponent<SlotInfo>().amountOfItems = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[5];
                        GameObject.Find("6").GetComponentInChildren<Text>().text = GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[5].ToString();
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[5] <= 1)
                            GameObject.Find("6").GetComponentInChildren<Text>().text = "";
                    }
                }
            }
        GameObject.Find("GUIManager").GetComponent<GUIController>().CloseLootWindow();
    }
        //Take Item from loot window
        public void TakeLoot()
      {
        if (selectedItem != GameObject.Find("TimeImageForInventory"))
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>() != null)
            {
                int indexForArrays = 0;
                if (selectedItem.name == "1")
                    indexForArrays = 0;
                if (selectedItem.name == "2")
                    indexForArrays = 1;
                if (selectedItem.name == "3")
                    indexForArrays = 2;
                if (selectedItem.name == "4")
                    indexForArrays = 3;
                if (selectedItem.name == "5")
                    indexForArrays = 4;
                if (selectedItem.name == "6")
                    indexForArrays = 5;
                bool gotInStack = false;
                for (int i = 0; i < images.Length; i++)
                {
                    //Firstly check if there are unfull stacks in inventory
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack > 1)
                        for (int m = 0; m < images.Length; m++)
                            if ((selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == selectedItem.GetComponent<SlotInfo>().item.GetComponent<Image>().sprite))
                            {
                                images[m].GetComponent<SlotInfo>().amountOfItems++;
                                selectedItem.GetComponent<SlotInfo>().amountOfItems--;
                                GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[indexForArrays]--;
                                gotInStack = true;
                                break;
                            }
                    //End searching if unfull stack found
                    if (gotInStack)
                        break;
                    //Fill in empty slot in inventory
                    if (images[i].sprite == null)
                    {
                        images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                        images[i].sprite = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Image>().sprite;
                        images[i].GetComponent<SlotInfo>().amountOfItems++;
                        images[i].GetComponent<SlotInfo>().item = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().item;
                        if (selectedItem != null)
                        selectedItem.GetComponent<SlotInfo>().amountOfItems--;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>() != null)
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[indexForArrays]--;
                        if (selectedItem != null)
                            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 1)
                                        GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsQuestStageOne();
                        if (selectedItem != null)
                            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Dragon scroll")
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestStageOne();
                        if (selectedItem != null)
                            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Old book")
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage < 1)
                                        GameObject.Find("QuestManager").GetComponent<QuestManager>().LibrarianSpecialQuestStageOne();
                        if (selectedItem != null)
                            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage < 4)
                                    GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansQuestStageFour();
                        if (filledSlots < 60)
                            filledSlots++;
                        break;
                    }
                    //Show Inventory Full if inventory is full
                    if (i == 59 && images[i].GetComponent<SlotInfo>().amountOfItems == selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack && images[i].sprite != null)
                    {
                        inventoryIsFull.SetActive(true);
                        StartCoroutine("CloseMessage");
                    }
                }
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Arrow" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Стрела" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Стріла")
                {
                    hasArrows = true;
                }
                //Clear loot icon and delete loot
                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[indexForArrays] == 0)
                {
                    itemInfoInventory.SetActive(false);
                    selectedItem.GetComponent<Image>().color = Color.white;
                    selectedItem.GetComponent<Image>().sprite = null;
                    selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
                    selectedItem.GetComponent<SlotInfo>().item = null;
                    selectedItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    selectedItem = GameObject.Find("TimeImageForInventory");
                    GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().loot[indexForArrays] = null;
                }
            }
            else if(GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>() != null)
            {
                int indexForArrays = 0;
                if (selectedItem.name == "1")
                    indexForArrays = 0;
                if (selectedItem.name == "2")
                    indexForArrays = 1;
                if (selectedItem.name == "3")
                    indexForArrays = 2;
                if (selectedItem.name == "4")
                    indexForArrays = 3;
                if (selectedItem.name == "5")
                    indexForArrays = 4;
                if (selectedItem.name == "6")
                    indexForArrays = 5;
                bool gotInStack = false;
                for (int i = 0; i < images.Length; i++)
                {
                    //Firstly check if there are unfull stacks in inventory
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack > 1)
                        for (int m = 0; m < images.Length; m++)
                            if ((selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == selectedItem.GetComponent<SlotInfo>().item.GetComponent<Image>().sprite))
                            {
                                images[m].GetComponent<SlotInfo>().amountOfItems++;
                                selectedItem.GetComponent<SlotInfo>().amountOfItems--;
                                GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[indexForArrays]--;
                                gotInStack = true;
                                break;
                            }
                    //End searching if unfull stack found
                    if (gotInStack)
                        break;
                    //Fill in empty slot in inventory
                    if (images[i].sprite == null)
                    {
                        images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                        images[i].sprite = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Image>().sprite;
                        images[i].GetComponent<SlotInfo>().amountOfItems++;
                        images[i].GetComponent<SlotInfo>().item = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().item;
                        selectedItem.GetComponent<SlotInfo>().amountOfItems--;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>() != null)
                            GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[indexForArrays]--;
                        if (filledSlots < 60)
                            filledSlots++;
                        break;
                    }
                    //Show Inventory Full if inventory is full
                    if (i == 59 && images[i].GetComponent<SlotInfo>().amountOfItems == selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().amountInStack && images[i].sprite != null)
                    {
                        inventoryIsFull.SetActive(true);
                        StartCoroutine("CloseMessage");
                    }
                }
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Arrow" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Стрела" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Стріла")
                {
                    hasArrows = true;
                }
                //Clear loot icon and delete loot
                if (GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().amountOfItems[indexForArrays] == 0)
                {
                    itemInfoInventory.SetActive(false);
                    selectedItem.GetComponent<Image>().color = Color.white;
                    selectedItem.GetComponent<Image>().sprite = null;
                    selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
                    selectedItem.GetComponent<SlotInfo>().item = null;
                    selectedItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    selectedItem = GameObject.Find("TimeImageForInventory");
                    GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<LootCrate>().loot[indexForArrays] = null;
                }
            }
        } 
     }
    //Grab Item from person
    public void GrabItem(GameObject personToGrab,int grabItemIndex)
    {
            bool gotInStack = false;
        if (personToGrab.GetComponent<Loot>() != null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                //Firstly check if there are unfull stacks in inventory
                if (personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().amountInStack > 1)
                    for (int m = 0; m < images.Length; m++)
                        if ((personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Image>().sprite))
                        {
                            images[m].GetComponent<SlotInfo>().amountOfItems++;
                            personToGrab.GetComponent<Loot>().amountOfItems[grabItemIndex]--;
                            gotInStack = true;
                            break;
                        }
                //End searching if unfull stack found
                if (gotInStack)
                    break;
                //Fill in empty slot in inventory
                if (images[i].sprite == null)
                {
                    images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                    images[i].sprite = personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Image>().sprite;
                    if (personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Republican's orders")
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 1)
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRoyalistsQuestStageOne();
                    if (personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Dragon scroll")
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().MainQuestStageOne();
                    if (personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Old book")
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("LibrarianSpecialQuest").GetComponent<QuestSlot>().questStage < 1)
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().LibrarianSpecialQuestStageOne();
                    if (personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Royalists orders")
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest") != null)
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRepublicansQuest").GetComponent<QuestSlot>().questStage < 4)
                                GameObject.Find("QuestManager").GetComponent<QuestManager>().HeadOfRepublicansQuestStageFour();
                    images[i].GetComponent<SlotInfo>().amountOfItems++;
                    images[i].GetComponent<SlotInfo>().item = personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().item;
                    personToGrab.GetComponent<Loot>().amountOfItems[grabItemIndex]--;
                    if (filledSlots < 60)
                        filledSlots++;
                    break;
                }
                //Show Inventory Full if inventory is full
                if (i == 59 && images[i].GetComponent<SlotInfo>().amountOfItems == personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().amountInStack && images[i].sprite != null)
                {
                    inventoryIsFull.SetActive(true);
                    StartCoroutine("CloseMessage");
                }
            }
            if (personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Arrow" || personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Стрела" || personToGrab.GetComponent<Loot>().loot[grabItemIndex].GetComponent<Item>().itemName == "Стріла")
            {
                hasArrows = true;
            }
            //Clear loot icon and delete loot
            if (personToGrab.GetComponent<Loot>().amountOfItems[grabItemIndex] == 0)
            {
                itemInfoInventory.SetActive(false);
                lootSlots[grabItemIndex].GetComponent<Image>().color = Color.white;
                lootSlots[grabItemIndex].GetComponent<Image>().sprite = null;
                lootSlots[grabItemIndex].GetComponent<SlotInfo>().amountOfItems = 0;
                lootSlots[grabItemIndex].GetComponent<SlotInfo>().item = null;
                lootSlots[grabItemIndex].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                personToGrab.GetComponent<Loot>().loot[grabItemIndex] = null;
            }
        }
        else if (personToGrab.GetComponent<LootCrate>() != null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                //Firstly check if there are unfull stacks in inventory
                if (personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().amountInStack > 1)
                    for (int m = 0; m < images.Length; m++)
                        if ((personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().amountInStack > 1 && images[m].GetComponent<SlotInfo>().amountOfItems < personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().amountInStack && images[m].GetComponent<Image>().sprite == personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Image>().sprite))
                        {
                            images[m].GetComponent<SlotInfo>().amountOfItems++;
                            personToGrab.GetComponent<LootCrate>().amountOfItems[grabItemIndex]--;
                            gotInStack = true;
                            break;
                        }
                //End searching if unfull stack found
                if (gotInStack)
                    break;
                //Fill in empty slot in inventory
                if (images[i].sprite == null)
                {
                    images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                    images[i].sprite = personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Image>().sprite;
                    images[i].GetComponent<SlotInfo>().amountOfItems++;
                    images[i].GetComponent<SlotInfo>().item = personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().item;
                    personToGrab.GetComponent<LootCrate>().amountOfItems[grabItemIndex]--;
                    if (filledSlots < 60)
                        filledSlots++;
                    break;
                }
                //Show Inventory Full if inventory is full
                if (i == 59 && images[i].GetComponent<SlotInfo>().amountOfItems == personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().amountInStack && images[i].sprite != null)
                {
                    inventoryIsFull.SetActive(true);
                    StartCoroutine("CloseMessage");
                }
            }
            if (personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().itemName == "Arrow" || personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().itemName == "Стрела" || personToGrab.GetComponent<LootCrate>().loot[grabItemIndex].GetComponent<Item>().itemName == "Стріла")
            {
                hasArrows = true;
            }
            //Clear loot icon and delete loot
            if (personToGrab.GetComponent<LootCrate>().amountOfItems[grabItemIndex] == 0)
            {
                itemInfoInventory.SetActive(false);
                lootSlots[grabItemIndex].GetComponent<Image>().color = Color.white;
                lootSlots[grabItemIndex].GetComponent<Image>().sprite = null;
                lootSlots[grabItemIndex].GetComponent<SlotInfo>().amountOfItems = 0;
                lootSlots[grabItemIndex].GetComponent<SlotInfo>().item = null;
                lootSlots[grabItemIndex].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                personToGrab.GetComponent<LootCrate>().loot[grabItemIndex] = null;
            }
        }
    }
    //Drop selected Item;
    public void Drop()
    {
        if (selectedItem != null)
        {
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag!="Armor"&& selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag!="Lockpick"&&selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag != "Quest"&& selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName!= "Royalists orders"&& selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Republican's orders" && selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Old book"&&selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Dragon scroll")
            {
                //Check if position is free
                Ray forwardRay = new Ray(player.transform.position + player.transform.up, player.transform.forward);
                Ray leftRay = new Ray(player.transform.position + player.transform.up, -player.transform.right);
                Ray rightRay = new Ray(player.transform.position + player.transform.up, player.transform.right);
                Ray backRay = new Ray(player.transform.position + player.transform.up, -player.transform.forward);
                RaycastHit forwardRayHit;
                RaycastHit leftRayHit;
                RaycastHit rightRayHit;
                RaycastHit backRayHit;
                if (Physics.Raycast(forwardRay, out forwardRayHit))
                {
                    if (Physics.Raycast(leftRay, out leftRayHit))
                    {
                        if (Physics.Raycast(rightRay, out rightRayHit))
                        {
                            if (Physics.Raycast(backRay, out backRayHit))
                            {
                                Vector3 spawnPosition = player.transform.localPosition + player.transform.up * 2;
                                if ((forwardRayHit.point - player.transform.position).magnitude > 3f)
                                    spawnPosition = player.transform.localPosition + player.transform.forward + player.transform.up * 1.5f;
                                else if ((leftRayHit.point - player.transform.position).magnitude > 3f)
                                    spawnPosition = player.transform.localPosition - player.transform.right + player.transform.up * 1.5f;
                                else if ((rightRayHit.point - player.transform.position).magnitude > 3f)
                                    spawnPosition = player.transform.localPosition + player.transform.right + player.transform.up * 1.5f;
                                else if ((backRayHit.point - player.transform.position).magnitude > 3f)
                                    spawnPosition = player.transform.localPosition - player.transform.forward + player.transform.up * 1.5f;
                                //Drop one item if  more than 1 in stack
                                if (selectedItem.GetComponent<SlotInfo>().amountOfItems > 1)
                                {
                                    if (selectedItem.gameObject.transform.parent.name == "HelmetIcon")
                                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage*GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify/100);
                                    if (selectedItem.gameObject.transform.parent.name == "ShieldIcon")
                                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify/100);
                                    GameObject spawn = Instantiate(selectedItem.GetComponent<SlotInfo>().item, spawnPosition, player.transform.rotation);
                                    selectedItem.GetComponent<SlotInfo>().amountOfItems--;
                                    spawn.name = selectedItem.GetComponent<SlotInfo>().item.name;
                                    if (spawn.GetComponent<Item>() != null)
                                        if (spawn.GetComponent<Item>().itemName == "Hell mushroom" || spawn.GetComponent<Item>().itemName == "Slavia mushroom")
                                            spawn.AddComponent<Rigidbody>();
                                }
                                //Drop item and clear slot if 1 item in stack
                                else if (selectedItem.GetComponent<SlotInfo>().amountOfItems == 1)
                                {
                                    if (selectedItem.gameObject.transform.parent.name == "HelmetIcon")
                                    {
                                        GameObject.Find("PlayerHelmet").GetComponent<MeshFilter>().mesh = null;
                                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify/100);
                                    }
                                    if (selectedItem.gameObject.transform.parent.name == "ShieldIcon")
                                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100);
                                    GameObject spawn = Instantiate(selectedItem.GetComponent<SlotInfo>().item, spawnPosition, player.transform.rotation);
                                    spawn.name = selectedItem.GetComponent<SlotInfo>().item.name;
                                    if (spawn.GetComponent<Item>() != null)
                                        if (spawn.GetComponent<Item>().itemName == "Hell mushroom" || spawn.GetComponent<Item>().itemName == "Slavia mushroom")
                                            spawn.AddComponent<Rigidbody>();
                                    selectedItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                    selectedItem.GetComponent<Image>().sprite = null;
                                    selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
                                    itemInfoInventory.gameObject.SetActive(false);
                                    selectedItem.GetComponent<SlotInfo>().item = null;
                                    if (filledSlots > 0)
                                        filledSlots--;
                                    //Check arrows in inventory
                                    if (spawn.name == "Arrow" || spawn.name == "Стрела" || spawn.name == "Стріла")
                                    {
                                        for (int i = 0; i < images.Length; i++)
                                        {
                                            if (images[i].GetComponent<SlotInfo>().item != null)
                                            {
                                                if (images[i].GetComponent<SlotInfo>().item.name == "Arrow" || images[i].GetComponent<SlotInfo>().item.name == "Стрела" || images[i].GetComponent<SlotInfo>().item.name == "Стріла")
                                                {
                                                    hasArrows = true;
                                                    break;
                                                }
                                                else
                                                    hasArrows = false;
                                            }
                                            else
                                                hasArrows = false;
                                        }
                                    }

                                    //Set selectedItem to time item to prevent bugs
                                    selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
                                }
                            }
                        }
                    }
                }
            }
        }
 
    }
    //Found which slot was chose and change color
    public void SetSelectedItemName()
    {
        InventoryMove();
        if (EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>().sprite != null)
        {
            selectedItem.GetComponent<Image>().color = new Color(255, 255, 255, selectedItem.GetComponent<Image>().color.a);
            selectedItem = EventSystem.current.currentSelectedGameObject.gameObject;
            selectedItem.GetComponent<Image>().color = Color.red;
            ShowItemInfo();
            if (moveWasUsed)
            {
                selectedItem.GetComponent<Image>().color = Color.white;
                selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
                moveWasUsed = false;
                itemInfoInventory.SetActive(false);
            }
        }
    }
    //Show Item information in inventory
    public void ShowItemInfo()
    {
        if (selectedItem.GetComponent<Image>().sprite != null)
        {
            itemInfoInventory.gameObject.SetActive(true);
            GameObject.Find("InventoryItemInfoHead").GetComponent<Text>().text = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName;
            GameObject.Find("InventoryInfoItemImage").GetComponent<Image>().sprite = selectedItem.GetComponent<Image>().sprite;
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Weapon")
            {
                if(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType=="OneHand")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Damage:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100+ selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100).ToString();
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Damage:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100).ToString();
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Bow")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Damage:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100).ToString();
            }
            if(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag=="Helmet"|| selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag=="Shield")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Armor:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage+ selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage*GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify/100).ToString();
            if(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Armor")
            {
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Armor:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100+ selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100).ToString();
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Armor:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100+ selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100).ToString();
            }
            if (GameObject.Find("GUIManager").GetComponent<GUIController>().shop.activeSelf)
            {
                if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 25 && GameObject.Find("Player").GetComponent<PlayerController>().prestige < 50)
                    prestigeDiscount = 25;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 50 && GameObject.Find("Player").GetComponent<PlayerController>().prestige < 75)
                    prestigeDiscount = 50;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 75)
                    prestigeDiscount = 75;
                else if(GameObject.Find("Player").GetComponent<PlayerController>().prestige<=-25&& GameObject.Find("Player").GetComponent<PlayerController>().prestige>-50)
                    prestigeDiscount =-25;
                else if(GameObject.Find("Player").GetComponent<PlayerController>().prestige<=-50&& GameObject.Find("Player").GetComponent<PlayerController>().prestige>-75)
                    prestigeDiscount =-50;
                else if(GameObject.Find("Player").GetComponent<PlayerController>().prestige<=-75)
                    prestigeDiscount = -75;
                if (selectedItem.tag != "ShopIcon" && selectedItem.tag != "FreeShopIcon")
                    GameObject.Find("InventoryItemCostInfo").GetComponent<Text>().text = "Cost:" + ((int)(selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemCost / 4 + selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemCost / 4 * prestigeDiscount / 100)).ToString();
                else
                    GameObject.Find("InventoryItemCostInfo").GetComponent<Text>().text = "Cost:" + ((int)(selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemCost - selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemCost * prestigeDiscount / 100)).ToString();
                prestigeDiscount = 0;
            }
            else
                GameObject.Find("InventoryItemCostInfo").GetComponent<Text>().text = "Cost:" + (selectedItem.GetComponent<SlotInfo>().item.gameObject.GetComponent<Item>().itemCost).ToString();
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                GameObject.Find("SpecialAbility").GetComponent<Text>().text = "Hard armor";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                GameObject.Find("SpecialAbility").GetComponent<Text>().text = "Light armor";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "OneHand")
                GameObject.Find("SpecialAbility").GetComponent<Text>().text = "One-handed";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand"|| selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Bow")
                GameObject.Find("SpecialAbility").GetComponent<Text>().text = "Two-handed";
            if(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType != "OneHand"&& selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType != "Bow"&& selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType != "TwoHand")
                GameObject.Find("SpecialAbility").GetComponent<Text>().text = "";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Potion" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Food")
            {
                if(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType=="Health")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Recover:" + (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100)+" HP";
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Health")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Recover:" + (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100) + " HP";
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Mana")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Recover:" + (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100) + " MP";
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Stamina")
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Recover:" + (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100) + " SP";
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Ingridient")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Arrow")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Damage:"+selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage;
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "OneOff")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Uses:1";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Permanent")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Uses:infinite";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Ingridient")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Spell") 
            {
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Fireball")
                {
                    specialAbilityItemText.text = "Mana:15";
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text ="Damage:"+(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().battleSpellsModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify / 100);
                }
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Storm spell")
                {
                    specialAbilityItemText.text = "Mana:50";
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Damage:" + (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().battleSpellsModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify / 100);
                }
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Summon archer")
                    specialAbilityItemText.text = "Mana:50";
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Summon melee")
                    specialAbilityItemText.text = "Mana:50";
                else if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Health recover" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Stamina recover")
                {
                    specialAbilityItemText.text = "Mana:25";
                    GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Recover:" + (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().recoverSpellsModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().recoverModify / 100);
                }
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Quest")
            {
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "";
                GameObject.Find("InventoryItemCostInfo").GetComponent<Text>().text = "";
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Berserk potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Damage:+10%";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Paladin potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Armor:+10%";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Robber potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Pickpocketing:+10";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Breaker potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Breaking:+10";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Warrior potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Max health:+50";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Archimage potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Max mana:+50";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Runner potion")
                GameObject.Find("InventoryItemDamageInfo").GetComponent<Text>().text = "Max stamina:+50";
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Special")
                GameObject.Find("SpecialAbility").GetComponent<Text>().text = "Time:60 seconds";
        }
    }
    //Equip/Unequip button function
    public void EquipUnequip()
    {
        //Equip if item is weapon or armor part
        if ((selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Weapon" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Armor" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Helmet" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Spell"|| selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Shield") && selectedItem.name != "CharacterSlot")
        {
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Weapon" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Spell")
            {
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Bow" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                {
                    if (shieldImage.sprite != null && weaponImage.sprite != null)
                    {
                        if (filledSlots < 60)
                        {
                            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                                player.GetComponent<Animator>().SetBool("TwoHand", true);
                            else
                                player.GetComponent<Animator>().SetBool("TwoHand", false);
                            Take(shieldImage.GetComponent<SlotInfo>().item, 20);
                            shieldImage.GetComponent<Image>().sprite = null;
                            shieldImage.GetComponent<Image>().color = Color.white;
                            shieldImage.GetComponent<Image>().color = new Color(shieldImage.GetComponent<Image>().color.r, shieldImage.GetComponent<Image>().color.g, shieldImage.GetComponent<Image>().color.b, 0);
                            shieldImage.GetComponent<SlotInfo>().amountOfItems = 0;
                            shieldImage.GetComponent<SlotInfo>().item = null;
                            EquipUseUIChange(weaponImage);
                        }
                        else
                        {
                            needMoreSpace.SetActive(true);
                            StartCoroutine("NeedMoreSpaceDisable");
                        }
                    }
                    else if (shieldImage.sprite == null && weaponImage.sprite == null)
                    {
                        if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                            player.GetComponent<Animator>().SetBool("TwoHand", true);
                        else
                            player.GetComponent<Animator>().SetBool("TwoHand", false);
                        EquipUseUIChange(weaponImage);
                    }
                    else if (shieldImage.sprite != null && weaponImage.sprite == null)
                    {
                        if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                            player.GetComponent<Animator>().SetBool("TwoHand", true);
                        else
                            player.GetComponent<Animator>().SetBool("TwoHand", false);
                        Take(shieldImage.GetComponent<SlotInfo>().item, 20);
                        shieldImage.GetComponent<Image>().sprite = null;
                        shieldImage.GetComponent<Image>().color = Color.white;
                        shieldImage.GetComponent<Image>().color = new Color(shieldImage.GetComponent<Image>().color.r, shieldImage.GetComponent<Image>().color.g, shieldImage.GetComponent<Image>().color.b, 0);
                        shieldImage.GetComponent<SlotInfo>().amountOfItems = 0;
                        shieldImage.GetComponent<SlotInfo>().item = null;
                        EquipUseUIChange(weaponImage);
                    }
                    else if (shieldImage.sprite == null && weaponImage.sprite != null)
                    {
                        if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                            player.GetComponent<Animator>().SetBool("TwoHand", true);
                        else
                            player.GetComponent<Animator>().SetBool("TwoHand", false);
                        EquipUseUIChange(weaponImage);
                    }
                }
                else
                {
                    EquipUseUIChange(weaponImage);
                    player.GetComponent<Animator>().SetBool("TwoHand", false);
                }
            }
            else if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Helmet")
            {
                if (helmetImage.sprite != null)
                    player.GetComponent<PlayerController>().armor -= (helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100);
                player.GetComponent<PlayerController>().armor += selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100; ;
                GameObject.Find("PlayerHelmet").GetComponent<MeshFilter>().mesh = selectedItem.GetComponent<SlotInfo>().item.GetComponent<MeshFilter>().sharedMesh;
                EquipUseUIChange(helmetImage);
                GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("ArmorText").GetComponent<Text>().text = "Armor:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.armor;
            }
            else if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Armor")
            {
                if (armorImage.sprite != null)
                {
                    if(armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType=="HardArmor")
                    player.GetComponent<PlayerController>().armor -= (armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100+ armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100);
                    if (armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        player.GetComponent<PlayerController>().armor -= (armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat/ 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100);
                }
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                    player.GetComponent<PlayerController>().armor += selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100+ selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                    player.GetComponent<PlayerController>().armor += selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                    player.GetComponent<PlayerController>().speed = player.GetComponent<PlayerController>().stableLightSpeed;
                if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                    player.GetComponent<PlayerController>().speed = player.GetComponent<PlayerController>().stableHardSpeed;
                EquipUseUIChange(armorImage);
                GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("ArmorText").GetComponent<Text>().text = "Armor:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.armor;
            }
            else if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Shield")
            {
                if (shieldImage.sprite != null)
                    player.GetComponent<PlayerController>().armor -= (shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100);
                player.GetComponent<PlayerController>().armor += selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                EquipUseUIChange(shieldImage);
                if(weaponImage.sprite!=null)
                if (weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType=="Bow"|| weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "TwoHand")
                {
                    Take(weaponImage.GetComponent<SlotInfo>().item,20);
                    weaponImage.GetComponent<Image>().sprite = null;
                    weaponImage.GetComponent<Image>().color = Color.white;
                    weaponImage.GetComponent<Image>().color = new Color(weaponImage.GetComponent<Image>().color.r, weaponImage.GetComponent<Image>().color.g, weaponImage.GetComponent<Image>().color.b, 0);
                    weaponImage.GetComponent<SlotInfo>().amountOfItems = 0;
                    weaponImage.GetComponent<SlotInfo>().item = null;
                }
                GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("ArmorText").GetComponent<Text>().text = "Armor:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.armor;
            }
        }
        if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Book")
        {
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Head note")
            {
                GameObject.Find("GUIManager").GetComponent<GUIController>().journal.SetActive(false);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttons[0].gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.text = "Head note";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttonTexts[0].text = "Bye";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.text = "I don't know who, but some traitor took the Dragon Scroll from us and went with him bandits. Now the big force in hands of the leader of bandits ...";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueBackground.gameObject.SetActive(true);
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Royalists orders")
            {
                GameObject.Find("GUIManager").GetComponent<GUIController>().journal.SetActive(false);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttons[0].gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.text = "Royalists orders";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttonTexts[0].text = "Bye";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.text = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Dialogue>().sentences[0];
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueBackground.gameObject.SetActive(true);
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Republican's orders")
            {
                GameObject.Find("GUIManager").GetComponent<GUIController>().journal.SetActive(false);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttons[0].gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.text = "Republican's orders";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttonTexts[0].text = "Bye";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.text = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Dialogue>().sentences[0];
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueBackground.gameObject.SetActive(true);
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Dragon scroll")
            {
                GameObject.Find("GUIManager").GetComponent<GUIController>().journal.SetActive(false);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttons[0].gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.text = "Dragon scroll";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttonTexts[0].text = "Bye";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.text = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Dialogue>().sentences[0];
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueBackground.gameObject.SetActive(true);
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Old book")
            {
                GameObject.Find("GUIManager").GetComponent<GUIController>().journal.SetActive(false);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttons[0].gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.text = "Old book";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().head.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().buttonTexts[0].text = "Bye";
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.text = selectedItem.GetComponent<SlotInfo>().item.GetComponent<Dialogue>().sentences[0];
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().main.gameObject.SetActive(true);
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueBackground.gameObject.SetActive(true);
            }
        }
        if(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Potion"|| selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag == "Food")
        {
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Health")
                player.GetComponent<PlayerController>().currentHealth += (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage+ selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage*GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat/100);
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Mana")
                player.GetComponent<PlayerController>().currentMana += (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100);
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Stamina")
                player.GetComponent<PlayerController>().currentStamina += (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().alchemyModifyStat / 100);
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Berserk potion")
            {
                player.GetComponent<PlayerController>().potionAttackModify = 10;
                attackPotionTime = 60;
                potionAttackActivated = true;
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
                {
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                        GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                        GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                    if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                        GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                }
                StopCoroutine("PotionArmorControl");
                StartCoroutine("PotionArmorControl");
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Paladin potion")
            {
                player.GetComponent<PlayerController>().potionArmorModify = 10;
                potionArmorTime = 60;
                potionArmorActivated = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                StopCoroutine("PotionAttackControl");
                StartCoroutine("PotionAttackControl");
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Robber potion")
            {
                if(pickpocketingPotionActivated==false)
                GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab += 10;
                pickpocketingPotionActivated = true;
                pickpocketingPotionTime = 60;
                StopCoroutine("PotionPickpocketingControl");
                StartCoroutine("PotionPickpocketingControl");
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Breaker potion")
            {
                if (breakingPotionActivated == false)
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock += 10;
                breakingPotionActivated = true;
                breakingPotionTime = 60;
                StopCoroutine("PotionBreakingControl");
                StartCoroutine("PotionBreakingControl");
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Warrior potion")
            {
                if (warriorPotionActivated == false)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer += 50;
                    GameObject.Find("Player").GetComponent<PlayerController>().currentHealth += 50;
                }
                warriorPotionActivated = true;
                warriorPotionTime = 60;
                GameObject.Find("GameManager").GetComponent<GameManager>().healthBar.SetMaxValue();
                StopCoroutine("PotionWarriorControl");
                StartCoroutine("PotionWarriorControl");
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Archimage potion")
            {
                if (archimagePotionActivated == false)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer += 50;
                    GameObject.Find("Player").GetComponent<PlayerController>().currentMana += 50;
                }
                archimagePotionActivated = true;
                archimagePotionTime = 60;
                GameObject.Find("GameManager").GetComponent<GameManager>().manaBar.SetMaxValue();
                StopCoroutine("PotionArchimageControl");
                StartCoroutine("PotionArchimageControl");
            }
            if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Runner potion")
            {
                if (runnerPotionActivated == false)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer += 50;
                    GameObject.Find("Player").GetComponent<PlayerController>().currentStamina += 50;
                }
                runnerPotionActivated = true;
                runnerPotionTime = 60;
                GameObject.Find("GameManager").GetComponent<GameManager>().staminaBar.SetMaxValue();
                StopCoroutine("PotionRunnerControl");
                StartCoroutine("PotionRunnerControl");
            }
            if (player.GetComponent<PlayerController>().currentStamina > player.GetComponent<PlayerController>().staminaPlayer)
                player.GetComponent<PlayerController>().currentStamina = player.GetComponent<PlayerController>().staminaPlayer;
            if (player.GetComponent<PlayerController>().currentMana > player.GetComponent<PlayerController>().manaPlayer)
                player.GetComponent<PlayerController>().currentMana = player.GetComponent<PlayerController>().manaPlayer;
            if (player.GetComponent<PlayerController>().currentHealth > player.GetComponent<PlayerController>().hpPlayer)
                player.GetComponent<PlayerController>().currentHealth = player.GetComponent<PlayerController>().hpPlayer;

            if (selectedItem.GetComponent<SlotInfo>().amountOfItems > 1)
                selectedItem.GetComponent<SlotInfo>().amountOfItems--;
            else if (selectedItem.GetComponent<SlotInfo>().amountOfItems == 1)
            {
                selectedItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                selectedItem.GetComponent<Image>().sprite = null;
                selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
                itemInfoInventory.gameObject.SetActive(false);
                selectedItem.GetComponent<SlotInfo>().item = null;
                if (filledSlots > 0)
                    filledSlots--;
                //Set selectedItem to time item to prevent bugs
                selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
            }
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value++;
            GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("ArmorText").GetComponent<Text>().text = "Armor:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.armor;
            GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("StaminaText").GetComponent<Text>().text = "Stamina:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.currentStamina + "/" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.staminaPlayer;
            GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("ManaText").GetComponent<Text>().text = "Mana:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.currentMana + "/" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.manaPlayer;
            GameObject.Find("GUIManager").GetComponent<GUIController>().characterInfoInventory.transform.Find("HealthText").GetComponent<Text>().text = "Health:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.currentHealth + "/" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.hpPlayer;
        }
        //Unequip armor or weapon
        if (selectedItem.gameObject.name == "CharacterSlot")
        {
            if (filledSlots < 60)
            {
                if (selectedItem.gameObject.transform.parent.name == "HelmetIcon")
                {
                    Debug.LogWarning("OK");
                    GameObject.Find("PlayerHelmet").GetComponent<MeshFilter>().mesh = null;
                    player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100);
                }
                if (selectedItem.gameObject.transform.parent.name == "ArmorIcon")
                {
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100);
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100);
                    player.GetComponent<PlayerController>().speed = player.GetComponent<PlayerController>().stableLightSpeed;
                }
                if (selectedItem.gameObject.transform.parent.name == "ShieldIcon")
                    player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100);
                selectedItem.GetComponent<Image>().color = Color.white;
                Take(selectedItem.gameObject.GetComponent<SlotInfo>().item, 20);
                selectedItem.GetComponent<Image>().color = new Color(selectedItem.GetComponent<Image>().color.r, selectedItem.GetComponent<Image>().color.g, selectedItem.GetComponent<Image>().color.b, 0);
                selectedItem.GetComponent<Image>().sprite = null;
                selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
                selectedItem.GetComponent<SlotInfo>().item = null;
                selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
                itemInfoInventory.gameObject.SetActive(false);
            }
            else
            {
                needMoreSpace.SetActive(true);
                StartCoroutine("NeedMoreSpaceDisable");
            }
        }
    }
    //Changes in GUI for weapon and armor when equip
    private void EquipUseUIChange(Image selectedImage)
    {
        //If character slot is empty
        if (selectedImage.GetComponent<Image>().sprite == null)
        {
            selectedItem.GetComponent<Image>().color = Color.white;
            selectedImage.GetComponent<SlotInfo>().amountOfItems = 1;
            selectedImage.GetComponent<SlotInfo>().item = selectedItem.GetComponent<SlotInfo>().item;
            selectedImage.GetComponent<Image>().color = new Color(selectedImage.GetComponent<Image>().color.r, selectedImage.GetComponent<Image>().color.g, selectedImage.GetComponent<Image>().color.b, 1);
            selectedImage.sprite = selectedItem.GetComponent<Image>().sprite;
            selectedItem.GetComponent<Image>().sprite = null;
            selectedItem.GetComponent<Image>().color = new Color(selectedItem.GetComponent<Image>().color.r, selectedItem.GetComponent<Image>().color.g, selectedItem.GetComponent<Image>().color.b, 0);
            selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
            itemInfoInventory.gameObject.SetActive(false);
            selectedItem.GetComponent<SlotInfo>().item = null;
            filledSlots--;
            selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
        }
        //If character slot isn't empty
        else if (selectedImage.GetComponent<Image>().sprite != null)
        {
            selectedItem.GetComponent<Image>().color = Color.white;
            timeImage.GetComponent<SlotInfo>().amountOfItems = 1;
            timeImage.GetComponent<SlotInfo>().item = selectedImage.GetComponent<SlotInfo>().item;
            timeImage.GetComponent<Image>().color = new Color(timeImage.GetComponent<Image>().color.r, timeImage.GetComponent<Image>().color.g, timeImage.GetComponent<Image>().color.b, 1);
            timeImage.sprite = selectedImage.GetComponent<Image>().sprite;
            selectedImage.GetComponent<SlotInfo>().amountOfItems = 1;
            selectedImage.GetComponent<SlotInfo>().item = selectedItem.GetComponent<SlotInfo>().item;
            selectedImage.sprite = selectedItem.GetComponent<Image>().sprite;
            selectedItem.GetComponent<SlotInfo>().amountOfItems = 1;
            itemInfoInventory.gameObject.SetActive(false);
            selectedItem.GetComponent<SlotInfo>().item = timeImage.GetComponent<SlotInfo>().item;
            selectedItem.GetComponent<Image>().sprite = timeImage.GetComponent<Image>().sprite;
            selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
        }
    }
    //Close Messages like Inventory is full after 2 seconds
    IEnumerator CloseMessage()
    {
        yield return new WaitForSeconds(2);
        inventoryIsFull.SetActive(false);
        inventoryIsFull.transform.localPosition = new Vector3(-3, -137.3f, 0);
        StopCoroutine("CloseMessage");
    }
    //Moving items in inventory
    private void InventoryMove()
    {
        if (!lootSlots[1].IsActive())
            if (EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>().sprite == null && selectedItem.GetComponent<Image>().sprite != null && EventSystem.current.currentSelectedGameObject.gameObject.name != "CharacterSlot" && EventSystem.current.currentSelectedGameObject.gameObject.name != "FastSlotImage"&&EventSystem.current.currentSelectedGameObject.tag!="ShopIcon"&&selectedItem.tag!="ShopIcon" && EventSystem.current.currentSelectedGameObject.tag != "FreeShopIcon" && selectedItem.tag != "FreeShopIcon")
            {
                EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>().sprite = selectedItem.GetComponent<Image>().sprite;
                EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<SlotInfo>().item = selectedItem.GetComponent<SlotInfo>().item;
                EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<SlotInfo>().amountOfItems = selectedItem.GetComponent<SlotInfo>().amountOfItems;
                EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                if (selectedItem.gameObject.transform.parent.name == "HelmetIcon")
                {
                    player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100);
                    GameObject.Find("PlayerHelmet").GetComponent<MeshFilter>().mesh = null;
                }
                if (selectedItem.gameObject.transform.parent.name == "ArmorIcon")
                {
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100);
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100);
                }
                if (selectedItem.gameObject.transform.parent.name == "ShieldIcon")
                    player.GetComponent<PlayerController>().armor -= (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100);
                selectedItem.GetComponent<Image>().color = new Color(selectedItem.GetComponent<Image>().color.r, selectedItem.GetComponent<Image>().color.g, selectedItem.GetComponent<Image>().color.b, 0);
            selectedItem.GetComponent<SlotInfo>().item = null;
            selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
            selectedItem.GetComponent<Image>().sprite = null;
            selectedItem.GetComponentInChildren<Text>().text = "";
            moveWasUsed = true;
        }
    }
    public IEnumerator NeedMoreSpaceDisable()
    {
        yield return new WaitForSeconds(3);
        needMoreSpace.SetActive(false);
    }
    //Buy item in market
    public void Buy()
    {
        if (selectedItem != GameObject.Find("TimeImageForInventory").gameObject && selectedItem != null&& GetComponent<GUIController>().inventoryUI.transform.Find("Buy").GetComponentInChildren<Text>().text == "Buy")
        {
            if (selectedItem.tag == "ShopIcon"||selectedItem.tag=="FreeShopIcon")
            {
                if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 25 && GameObject.Find("Player").GetComponent<PlayerController>().prestige < 50)
                    prestigeDiscount = 25;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 50 && GameObject.Find("Player").GetComponent<PlayerController>().prestige < 75)
                    prestigeDiscount = 50;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 75)
                    prestigeDiscount = 75;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige <= -25 && GameObject.Find("Player").GetComponent<PlayerController>().prestige > -50)
                    prestigeDiscount = -25;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige <= -50 && GameObject.Find("Player").GetComponent<PlayerController>().prestige > -75)
                    prestigeDiscount = -50;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige <= -75)
                    prestigeDiscount = -75;
                if (GameObject.Find("Player").GetComponent<PlayerController>().gold >= (int)(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost- selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost*prestigeDiscount/100))
                {
                    if (selectedItem.tag == "ShopIcon")
                        selectedItem.GetComponent<SlotInfo>().amountOfItems++;
                    Take(selectedItem.GetComponent<SlotInfo>().item, 20);
                }
                else
                    StartCoroutine("NeedMoreMoneyClose");
                prestigeDiscount = 0;
            }
        }
    }
    //Sold item in market
    public void Sold()
    {
        if (selectedItem != GameObject.Find("TimeImageForInventory").gameObject && selectedItem != null&&GetComponent<GUIController>().inventoryUI.transform.Find("Buy").GetComponentInChildren<Text>().text=="Sold")
        {
            if (selectedItem.tag != "ShopIcon"&& selectedItem.tag != "FreeShopIcon" && selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemInventoryTag != "Quest"&& selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Royalists orders" && selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Republican's orders" && selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Old book" && selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName != "Dragon scroll")
            {
                TakeShop(selectedItem.GetComponent<SlotInfo>().item, 20);
                if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 25 && GameObject.Find("Player").GetComponent<PlayerController>().prestige < 50)
                    prestigeDiscount = 25;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 50 && GameObject.Find("Player").GetComponent<PlayerController>().prestige < 75)
                    prestigeDiscount = 50;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige >= 75)
                    prestigeDiscount = 75;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige <= -25 && GameObject.Find("Player").GetComponent<PlayerController>().prestige > -50)
                    prestigeDiscount = -25;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige <= -50 && GameObject.Find("Player").GetComponent<PlayerController>().prestige > -75)
                    prestigeDiscount = -50;
                else if (GameObject.Find("Player").GetComponent<PlayerController>().prestige <= -75)
                    prestigeDiscount = -75;
                GameObject.Find("Player").GetComponent<PlayerController>().gold += (int)(selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost/4+ selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().itemCost / 4*prestigeDiscount/100);
                prestigeDiscount = 0;
                if (selectedItem.GetComponent<SlotInfo>().amountOfItems > 1)
                    selectedItem.GetComponent<SlotInfo>().amountOfItems--;
                //Drop item and clear slot if 1 item in stack
                else if (selectedItem.GetComponent<SlotInfo>().amountOfItems == 1)
                {
                    //Check arrows in inventory
                    if (selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Arrow" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Стрела" || selectedItem.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Стріла")
                    {
                        for (int i = 0; i < images.Length; i++)
                        {
                            if (images[i].GetComponent<SlotInfo>().item != null)
                            {
                                if ((images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Arrow" || images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Стрела" || images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "Стріла") && images[i].gameObject != selectedItem)
                                {
                                    hasArrows = true;
                                    break;
                                }
                                else
                                    hasArrows = false;
                            }
                            else
                                hasArrows = false;
                        }
                    }
                    selectedItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    selectedItem.GetComponent<Image>().sprite = null;
                    selectedItem.GetComponent<SlotInfo>().amountOfItems = 0;
                    itemInfoInventory.gameObject.SetActive(false);
                    selectedItem.GetComponent<SlotInfo>().item = null;
                    if (filledSlots > 0)
                        filledSlots--;
                    //Set selectedItem to time item to prevent bugs
                    selectedItem = GameObject.Find("TimeImageForInventory").gameObject;
                }
            }
        }
    }
    private IEnumerator NeedMoreMoneyClose()
    {
        needMoreMoney.SetActive(true);
        yield return new WaitForSeconds(3);
        needMoreMoney.SetActive(false);
    }
    private IEnumerator NeedIngridient()
    {
        yield return new WaitForSeconds(3);
        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(false);
    }
    private void TakeShop(GameObject itemObject, int index)
    {
        bool isSimpleObject = false;
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("ShopIcon").Length; i++)
        {
            if (itemObject == GameObject.FindGameObjectsWithTag("ShopIcon")[i].GetComponent<SlotInfo>().item)
            {
                isSimpleObject = true;
                break;
            }
        }
        bool gotInStack = false;
        if (isSimpleObject==false) {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("FreeShopIcon").Length; i++)
            {
                //Firstly check if there are unfull stacks in inventory
                if (itemObject.GetComponent<Item>().amountInStack > 1)
                    for (int m = 0; m < GameObject.FindGameObjectsWithTag("FreeShopIcon").Length; m++)
                        if (itemObject.GetComponent<Item>().amountInStack > 1 && GameObject.FindGameObjectsWithTag("FreeShopIcon")[m].GetComponent<SlotInfo>().amountOfItems < itemObject.GetComponent<Item>().amountInStack && GameObject.FindGameObjectsWithTag("FreeShopIcon")[m].GetComponent<Image>().sprite == itemObject.GetComponent<Image>().sprite)
                        {
                            GameObject.FindGameObjectsWithTag("FreeShopIcon")[m].GetComponent<SlotInfo>().amountOfItems++;
                            if (GameObject.Find("GUIManager").GetComponent<GUIController>().lootWindow.activeSelf)
                                GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[index]--;
                            gotInStack = true;
                            break;
                        }
                //End searching if unfull stack found
                if (gotInStack)
                    break;
                //Fill in empty slot in inventory
                if (GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<Image>().sprite == null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<GUIController>().lootWindow.activeSelf)
                        GameObject.Find("GameManager").GetComponent<GameManager>().objectForLoot.GetComponent<Loot>().amountOfItems[index]--;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<Image>().color = Color.white;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<Image>().sprite = itemObject.GetComponent<Image>().sprite;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<Image>().GetComponent<SlotInfo>().amountOfItems = 1;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<Image>().GetComponent<SlotInfo>().item = itemObject.GetComponent<Item>().item;
                    break;
                }
                //Show Inventory Full if inventory is full
                if (i == GameObject.FindGameObjectsWithTag("FreeShopIcon").Length - 1 && GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<SlotInfo>().amountOfItems == itemObject.GetComponent<Item>().amountInStack && GameObject.FindGameObjectsWithTag("FreeShopIcon")[i].GetComponent<Image>().sprite != null)
                {
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[globalFreeIndex].GetComponent<Image>().color = Color.white;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[globalFreeIndex].GetComponent<Image>().sprite = itemObject.GetComponent<Image>().sprite;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[globalFreeIndex].GetComponent<Image>().GetComponent<SlotInfo>().amountOfItems = 1;
                    GameObject.FindGameObjectsWithTag("FreeShopIcon")[globalFreeIndex].GetComponent<Image>().GetComponent<SlotInfo>().item = itemObject.GetComponent<Item>().item;
                    globalFreeIndex++;
                }
            }
        }
    }
    public void CraftItem(string itemName)
    {
        List<int> indridientOneIndexes = new List<int>();
        List<int> indridientTwoIndexes = new List<int>();
        int numberOneIngridientNow = 0;
        int numberTwoIngridientNow = 0;
        int ingridientOneIndexTime = -1;
        int ingridientTwoIndexTime = -1;
        int lowestNumber1 = 1000;
        int lowestNumber2 = 1000;
        string ingridientOne = "";
        string ingridientTwo = "";
        int ingridientOneNumber = 0;
        int ingridientTwoNumber = 0;
        bool hasTwoIngridients = false;
        if (itemName == "Roasted meat")
        {
            ingridientOne = "Meat";
            ingridientOneNumber = 1;
        }
        if (itemName == "Small health potion")
        {
            ingridientOne = "Hell mushroom";
            ingridientOneNumber = 1;
        }
        if (itemName == "Simple health potion")
        {
            ingridientOne = "Hell mushroom";
            ingridientOneNumber = 2;
        }
        if (itemName == "Huge health potion")
        {
            ingridientOne = "Hell mushroom";
            ingridientOneNumber = 3;
        }
        if (itemName == "Small stamina potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientOneNumber = 1;
        }
        if (itemName == "Simple stamina potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientOneNumber = 2;
        }
        if (itemName == "Huge stamina potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientOneNumber = 3;
        }
        if (itemName == "Small mana potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientTwo = "Hell mushroom";
            ingridientOneNumber = 1;
            ingridientTwoNumber = 1;
            hasTwoIngridients = true;
        }
        if (itemName == "Simple mana potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientTwo = "Hell mushroom";
            ingridientOneNumber = 2;
            ingridientTwoNumber = 2;
            hasTwoIngridients = true;
        }
        if (itemName == "Huge mana potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientTwo = "Hell mushroom";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Omelette")
        {
            ingridientOne = "Egg";
            ingridientOneNumber = 1;
        }
        if(itemName=="Meat pie")
        {
            ingridientOne = "Meat";
            ingridientTwo = "Bread";
            ingridientOneNumber = 2;
            ingridientTwoNumber = 2;
            hasTwoIngridients = true;
        }
        if (itemName == "Berserk potion")
        {
            ingridientOne = "Hell mushroom";
            ingridientTwo = "Bread";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Paladin potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientTwo = "Egg";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Robber potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientTwo = "Hell mushroom";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Breaker potion")
        {
            ingridientOne = "Meat";
            ingridientTwo = "Bread";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Warrior potion")
        {
            ingridientOne = "Hell mushroom";
            ingridientTwo = "Omelette";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Archimage potion")
        {
            ingridientOne = "Slavia mushroom";
            ingridientTwo = "Omelette";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (itemName == "Runner potion")
        {
            ingridientOne = "Meat";
            ingridientTwo = "Slavia mushroom";
            ingridientOneNumber = 3;
            ingridientTwoNumber = 3;
            hasTwoIngridients = true;
        }
        if (CheckIfHavePlace(ReturnItemByName(itemName)))
        {
            if (!hasTwoIngridients)
            {
                while (true)
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        bool isInList = false;
                        if (images[i].GetComponent<SlotInfo>().item != null)
                            if (images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == ingridientOne && images[i].GetComponent<SlotInfo>().amountOfItems < lowestNumber1) {
                                foreach (int index in indridientOneIndexes)
                                {
                                    if (index == i)
                                        isInList = true;
                                }
                                if (isInList == false)
                                        ingridientOneIndexTime = i;
                                if (images[i].GetComponent<SlotInfo>().amountOfItems >= ingridientOneNumber)
                                    break;
                            }
                    }
                    if (ingridientOneIndexTime != -1)
                    {
                        indridientOneIndexes.Add(ingridientOneIndexTime);
                        numberOneIngridientNow += images[ingridientOneIndexTime].GetComponent<SlotInfo>().amountOfItems;
                        ingridientOneIndexTime = -1;
                    }
                    else
                        break;
                    if (numberOneIngridientNow >= ingridientOneNumber)
                        break;
                }
                if (numberOneIngridientNow>=ingridientOneNumber)
                {
                    foreach (int index in indridientOneIndexes)
                    {
                        if (images[index].GetComponent<SlotInfo>().amountOfItems<ingridientOneNumber)
                        images[index].GetComponent<SlotInfo>().amountOfItems-= images[index].GetComponent<SlotInfo>().amountOfItems;
                        if (images[index].GetComponent<SlotInfo>().amountOfItems >= ingridientOneNumber)
                            images[index].GetComponent<SlotInfo>().amountOfItems -= ingridientOneNumber;
                        if (images[index].GetComponent<SlotInfo>().amountOfItems == 0)
                        {
                            images[index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            images[index].GetComponent<Image>().sprite = null;
                            images[index].GetComponent<SlotInfo>().amountOfItems = 0;
                            images[index].GetComponent<SlotInfo>().item = null;
                            filledSlots--;
                        }
                    }
                    if (itemName == "Roasted meat")
                        Take(roastedMeat, 20);
                    if (itemName == "Omelette")
                        Take(omelette, 20);
                    if (itemName == "Small health potion") 
                        Take(smallHealthPotion, 20);
                    if (itemName == "Simple health potion")
                        Take(simpleHealthPotion, 20);
                    if (itemName == "Huge health potion")
                        Take(hugeHealthPotion, 20);
                    if (itemName == "Small stamina potion")
                        Take(smallStaminaPotion, 20);
                    if (itemName == "Simple stamina potion")
                        Take(simpleStaminaPotion, 20);
                    if (itemName == "Huge stamina potion")
                        Take(hugeStaminaPotion, 20);
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value++;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_craft);
                }
                else
                {
                    if (itemName == "Roasted meat")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:"+(ingridientOneNumber-numberOneIngridientNow)+" Meat";
                    if (itemName == "Omelette")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) +" Egg";
                    if (itemName == "Small health potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) +" Hell mushroom";
                    if (itemName == "Simple health potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Hell mushroom";
                    if (itemName == "Huge health potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Hell mushroom";
                    if (itemName == "Small stamina potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom";
                    if (itemName == "Simple stamina potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom";
                    if (itemName == "Huge stamina potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom";
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(true);
                    StopCoroutine("NeedIngridient");
                    StartCoroutine("NeedIngridient");
                }
            }
            else
            {
                bool hasFirst = false;
                bool hasSecond = false;
                while (true)
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        bool isInList = false;
                        if (images[i].GetComponent<SlotInfo>().item != null)
                            if (images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == ingridientOne && images[i].GetComponent<SlotInfo>().amountOfItems < lowestNumber1)
                            {
                                foreach (int index in indridientOneIndexes)
                                {
                                    if (index == i)
                                        isInList = true;
                                }
                                if (isInList == false)
                                    ingridientOneIndexTime = i;
                                if (images[i].GetComponent<SlotInfo>().amountOfItems >= ingridientOneNumber)
                                    break;
                            }
                    }
                    if (ingridientOneIndexTime != -1)
                    {
                        indridientOneIndexes.Add(ingridientOneIndexTime);
                        numberOneIngridientNow += images[ingridientOneIndexTime].GetComponent<SlotInfo>().amountOfItems;
                        ingridientOneIndexTime = -1;
                    }
                    else
                        break;
                    if (numberOneIngridientNow >= ingridientOneNumber)
                        break;
                }
                if (numberOneIngridientNow >= ingridientOneNumber)
                    hasFirst = true;
                while (true)
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        bool isInList = false;
                        if (images[i].GetComponent<SlotInfo>().item != null)
                            if (images[i].GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == ingridientTwo && images[i].GetComponent<SlotInfo>().amountOfItems < lowestNumber2)
                            {
                                foreach (int index in indridientTwoIndexes)
                                {
                                    if (index == i)
                                        isInList = true;
                                }
                                if (isInList == false)
                                    ingridientTwoIndexTime = i;
                                if (images[i].GetComponent<SlotInfo>().amountOfItems >= ingridientTwoNumber)
                                    break;
                            }
                    }
                    if (ingridientTwoIndexTime != -1)
                    {
                        indridientTwoIndexes.Add(ingridientTwoIndexTime);
                        numberTwoIngridientNow += images[ingridientTwoIndexTime].GetComponent<SlotInfo>().amountOfItems;
                        ingridientTwoIndexTime = -1;
                    }
                    else
                        break;
                    if (numberTwoIngridientNow >= ingridientTwoNumber)
                        break;
                }
                if (numberTwoIngridientNow >= ingridientTwoNumber)
                    hasSecond = true;
                if (!hasFirst && !hasSecond)
                {
                    if (itemName == "Meat pie")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Meat,"+(ingridientTwoNumber-numberTwoIngridientNow)+" Bread";
                    if (itemName == "Small mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom," + (ingridientTwoNumber - numberTwoIngridientNow) + " Hell mushroom";
                    if (itemName == "Simple mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom," + (ingridientTwoNumber - numberTwoIngridientNow) + " Hell mushroom";
                    if (itemName == "Huge mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom," + (ingridientTwoNumber - numberTwoIngridientNow) + " Hell mushroom";
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " "+ ingridientOne+ "," + (ingridientTwoNumber - numberTwoIngridientNow) + " " + ingridientTwo;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(true);
                    StopCoroutine("NeedIngridient");
                    StartCoroutine("NeedIngridient");
                }
               else if (!hasFirst)
                {
                    if (itemName == "Meat pie")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Meat";
                    if (itemName == "Small mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom";
                    if (itemName == "Simple mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom";
                    if (itemName == "Huge mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " Slavia mushroom";
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientOneNumber - numberOneIngridientNow) + " " + ingridientOne;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(true);
                    StopCoroutine("NeedIngridient");
                    StartCoroutine("NeedIngridient");
                }
              else  if (!hasSecond)
                {
                    if (itemName == "Meat pie")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientTwoNumber - numberTwoIngridientNow) + " Bread";
                    if (itemName == "Small mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientTwoNumber - numberTwoIngridientNow) + " Hell mushroom";
                    if (itemName == "Simple mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientTwoNumber - numberTwoIngridientNow) + " Hell mushroom";
                    if (itemName == "Huge mana potion")
                        GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientTwoNumber - numberTwoIngridientNow) + " Hell mushroom";
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.GetComponentInChildren<Text>().text = "Need ingridient:" + (ingridientTwoNumber - numberTwoIngridientNow) + " " + ingridientTwo;
                    GameObject.Find("GUIManager").GetComponent<GUIController>().needIngridient.SetActive(true);
                    StopCoroutine("NeedIngridient");
                    StartCoroutine("NeedIngridient");
                }
               else if (hasFirst && hasSecond)
                {
                    foreach (int index in indridientOneIndexes)
                    {
                        if (images[index].GetComponent<SlotInfo>().amountOfItems < ingridientOneNumber)
                            images[index].GetComponent<SlotInfo>().amountOfItems -= images[index].GetComponent<SlotInfo>().amountOfItems;
                        if (images[index].GetComponent<SlotInfo>().amountOfItems >= ingridientOneNumber)
                            images[index].GetComponent<SlotInfo>().amountOfItems -= ingridientOneNumber;
                        if (images[index].GetComponent<SlotInfo>().amountOfItems == 0)
                        {
                            images[index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            images[index].GetComponent<Image>().sprite = null;
                            images[index].GetComponent<SlotInfo>().amountOfItems = 0;
                            images[index].GetComponent<SlotInfo>().item = null;
                            filledSlots--;
                        }
                    }
                    foreach (int index in indridientTwoIndexes)
                    {
                        if (images[index].GetComponent<SlotInfo>().amountOfItems < ingridientTwoNumber)
                            images[index].GetComponent<SlotInfo>().amountOfItems -= images[index].GetComponent<SlotInfo>().amountOfItems;
                        if (images[index].GetComponent<SlotInfo>().amountOfItems >= ingridientTwoNumber)
                            images[index].GetComponent<SlotInfo>().amountOfItems -= ingridientTwoNumber;
                        if (images[index].GetComponent<SlotInfo>().amountOfItems == 0)
                        {
                            images[index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            images[index].GetComponent<Image>().sprite = null;
                            images[index].GetComponent<SlotInfo>().amountOfItems = 0;
                            images[index].GetComponent<SlotInfo>().item = null;
                            filledSlots--;
                        }
                    }
                    if (itemName == "Meat pie")
                        Take(meatPie, 20);
                    if (itemName == "Small mana potion")
                        Take(smallManaPotion, 20);
                    if (itemName == "Simple mana potion")
                        Take(simpleManaPotion, 20);
                    if (itemName == "Huge mana potion")
                        Take(hugeManaPotion, 20);
                    if (itemName == "Berserk potion")
                        Take(damagePotion, 20);
                    if (itemName == "Paladin potion")
                        Take(armorPotion, 20);
                    if (itemName == "Robber potion")
                        Take(pickpocketingPotion, 20);
                    if (itemName == "Breaker potion")
                        Take(breakingPotion, 20);
                    if (itemName == "Runner potion")
                        Take(runnerPotion, 20);
                    if (itemName == "Warrior potion")
                        Take(warriorPotion, 20);
                    if (itemName == "Archimage potion")
                        Take(archimagePotion, 20);
                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value++;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_craft);
                }
            }
        }
        else
        {
            needMoreSpace.SetActive(true);
            StartCoroutine("NeedMoreSpaceDisable");
        }
    }
    public bool CheckIfHavePlace(GameObject objectToTake)
    {
        if (objectToTake.GetComponent<Item>().amountInStack == 1)
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots < 60)
                return true;
        if (objectToTake.GetComponent<Item>().amountInStack > 1)
        {
            for (int m = 0; m < GameObject.Find("GUIManager").GetComponent<Inventory>().images.Length; m++)
                if (objectToTake.GetComponent<Item>().amountInStack > 1 && GameObject.Find("GUIManager").GetComponent<Inventory>().images[m].GetComponent<SlotInfo>().amountOfItems < objectToTake.GetComponent<Item>().amountInStack && GameObject.Find("GUIManager").GetComponent<Inventory>().images[m].GetComponent<Image>().sprite == objectToTake.GetComponent<Image>().sprite)
                    return true;
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().filledSlots < 60)
                return true;
        }
        return false;
    }
    public GameObject ReturnItemByName(string name)
    {
        if (name == "Waraxe" || name == "Топор" || name == "Сокира")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().waraxePrefab;
        if (name == "Bow" || name == "Лук")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().bowPrefab;
        if (name == "Dagger" || name == "Кинджал" || name == "Кинжал")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().daggerPrefab;
        if (name == "Arrow" || name == "Стрела" || name == "Стріла")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().arrowPrefab;
        if (name == "Fireball" || name == "Огненный шар" || name == "Вогняна куля")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().fireballPrefab;
        if (name == "Health recover" || name == "Восстановление HP" || name == "Відновлення HP")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().healthResurection;
        if (name == "Восстановление SP" || name == "Відновлення SP" || name == "Stamina recover")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().staminaResurection;
        if (name == "Summon melee" || name == "Призыв ББ" || name == "Виклик ББ")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().meleeSummon;
        if (name == "Summon archer" || name == "Призыв ДБ" || name == "Виклик ДБ")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().archerSummon;
        if (name == "Cavalry helmet" || name == "Кавалерійський шолом" || name == "Кавалерийский шлем")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().cavalryHelmet;
        if (name == "Militia helmet" || name == "Гайдамацький шолом" || name == "Шлем ополчения")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().militiaHelmet;
        if (name == "Iron helmet" || name == "Залізний шолом" || name == "Железный шлем")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().ironHelmet;
        if (name == "Soldier helmet" || name == "Солдатський шолом" || name == "Солдатский шлем")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().soldierHelmet;
        if (name == "Militia armor" || name == "Доспех ополчения" || name == "Гайдамацький обладунок")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().militiaArmor;
        if (name == "Iron armor" || name == "Железний доспех" || name == "Залізний обладунок")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().ironArmor;
        if (name == "Cavalry armor" || name == "Кавалерийский доспех" || name == "Кавалерійський обладунок")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().cavalryArmor;
        if (name == "Soldier armor" || name == "Солдатський обладунок" || name == "Солдатский доспех ")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().soldierArmor;
        if (name == "Militia shield" || name == "Щит ополчения" || name == "Гайдамацький щит")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().militiaShield;
        if (name == "Cheap shield" || name == "Дешёвый щит" || name == "Дешевий щит")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().cheapShield;
        if (name == "Professional bow" || name == "Профессиональный лук" || name == "Професійний лук")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().professionalBow;
        if (name == "Big soldier sword" || name == "Большой меч солдата" || name == "Великий меч солдата")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().bigSoldierSword;
        if (name == "Small health potion" || name == "Маленькое зелье лечения" || name == "Маленьке лікувальне зілля")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().smallHealthPotion;
        if (name == "Simple health potion" || name == "Обычное зелье лечения" || name == "Звичайне лікувальне зілля")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().simpleHealthPotion;
        if (name == "Huge health potion" || name == "Большое зелье лечения" || name == "Велике лікувальне зілля")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().hugeHealthPotion;
        if (name == "Small stamina potion" || name == "Маленькое зелье выносливости" || name == "Маленьке зілля витривалості")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().smallStaminaPotion;
        if (name == "Simple stamina potion" || name == "Обычное зелье выносливости" || name == "Звичайне зілля витривалості")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().simpleStaminaPotion;
        if (name == "Huge stamina potion" || name == "Большое зелье выносливости" || name == "Велике зілля витривалості")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().hugeStaminaPotion;
        if (name == "Small mana potion" || name == "Маленьке зілля мани" || name == "Маленькое зелье маны")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().smallManaPotion;
        if (name == "Simple mana potion" || name == "Звичайне зілля мани" || name == "Обычное зелье маны")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().simpleManaPotion;
        if (name == "Huge mana potion" || name == "Велике зілля мани" || name == "Большое зелье маны")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().hugeManaPotion;
        if (name == "Iron sword" || name == "Залізний меч" || name == "Железный меч")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().ironSword;
        if (name == "Big iron sword" || name == "Великий залізний меч" || name == "Большой железный меч")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().bigIronSword;
        if (name == "Militia sword" || name == "Гайдамацький меч" || name == "Меч ополчения")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().militiaSword;
        if (name == "Big militia sword" || name == "Великий гайдамацький меч" || name == "Большой меч ополчения")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().bigMilitiaSword;
        if (name == "Soldier sword" || name == "Меч солдата" || name == "Меч солдата")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().soldierSword;
        if (name == "Big waraxe" || name == "Велика сокира" || name == "Большая секира")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().bigWaraxe;
        if (name == "Militia bow" ||name == "Лук ополчения" || name == "Гайдамацький лук")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().militiaBow;
        if (name == "Head note" || name == "Записка главы деревни" || name == "Записка голови села")
            return  GameObject.Find("GUIManager").GetComponent<Inventory>().headNote;
        if (name == "Meat" || name == "Мясо" || name == "М'ясо")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().meat;
        if (name == "Professional lockpick" || name == "Профессиональная отмычка" || name == "Професійна відмичка")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().professionalLockpick;
        if (name == "One-off lockpick" || name == "Одноразова відмичка" || name == "Одноразовая отмычка")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().oneLockpick;
        if (name == "Roasted meat" || name == "Жарене м'ясо" || name == "Жареное мясо")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().roastedMeat;
        if (name == "Wine" || name == "Вино")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().wineBottle;
        if (name == "Beer" || name == "Пиво")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().beerBottle;
        if (name == "Meat pie" || name == "Мясной пирог" || name == "М'ясний пиріг")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().meatPie;
        if (name == "Bread" || name == "Хлеб" || name == "Хліб")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().bread;
        if (name == "Egg" || name == "Яйцо" || name == "Яйце")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().egg;
        if (name == "Omelette" || name == "Омлет")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().omelette;
        if (name == "Slavia mushroom" || name == "Славійський гриб" || name == "Славийский гриб")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().slaviaMushroom;
        if (name == "Hell mushroom" || name == "Пекельний гриб" || name == "Адский гриб")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().hellMushroom;
        if (name == "Berserk potion" || name == "Зілля берсерка" || name == "Зелье берсерка")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().damagePotion;
        if (name == "Paladin potion" || name == "Зілля паладина" || name == "Зелье паладина")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().armorPotion;
        if (name == "Robber potion" || name == "Зілля крадія" || name == "Зелье вор")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotion;
        if (name == "Breaker potion" || name == "Зілля зломщика" || name == "Зелье взломщика")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotion;
        if (name == "Warrior potion" || name == "Зілля воїна" || name == "Зелье воина")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotion;
        if (name == "Archimage potion" || name == "Зілля архимага" || name == "Зелье архимага")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotion;
        if (name == "Runner potion" || name == "Зілля бігуна" || name == "Зелье бегуна")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotion;
        if (name == "Artelis mushroom" || name == "Гриб Артеліса" || name == "Гриб Артелиса")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().specialMushroom;
        if (name == "Royalists orders" || name == "Накази роялістів" || name == "Приказы роялистов")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().royalistsOrders;
        if (name == "Republican's orders" || name == "Накази республіканців" || name == "Приказы республиканцов")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().republicansOrders;
        if (name == "Storm spell" || name == "Штормове закляття" || name == "Штурмовое заклинание")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().stormSpell;
        if (name == "Artelit sword" || name == "Меч Артеліта" || name == "Меч Артелита")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().artelitSword;
        if (name == "Old book" || name == "Стара книга" || name == "Старая книжка")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().oldBook;
        if (name == "Dragon scroll" || name == "Драконний сувій" || name == "Драконний свиток")
            return GameObject.Find("GUIManager").GetComponent<Inventory>().dragonScroll;
        return null;
    }
    private IEnumerator PotionAttackControl()
    {
        yield return new WaitForSeconds(1);
        attackPotionTime--;
        if (potionAttackActivated&&attackPotionTime!=0)
            StartCoroutine("PotionAttackControl");
        if (attackPotionTime <= 0)
        {
            potionAttackActivated = false;
            attackPotionTime = 60;
            player.GetComponent<PlayerController>().potionAttackModify = 0;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon != null)
            {
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            }
        }
    }
    private IEnumerator PotionPickpocketingControl()
    {
        yield return new WaitForSeconds(1);
        pickpocketingPotionTime--;
        if (pickpocketingPotionActivated && pickpocketingPotionTime != 0)
            StartCoroutine("PotionPickpocketingControl");
        if (pickpocketingPotionTime <= 0)
        {
            pickpocketingPotionActivated = false;
            pickpocketingPotionTime = 60;
            player.GetComponent<PlayerController>().chanceForGrab -= 10;
        }
    }
    private IEnumerator PotionBreakingControl()
    {
        yield return new WaitForSeconds(1);
        breakingPotionTime--;
        if (breakingPotionActivated && breakingPotionTime != 0)
            StartCoroutine("PotionBreakingControl");
        if (breakingPotionTime <= 0)
        {
            breakingPotionActivated = false;
            breakingPotionTime = 60;
            player.GetComponent<PlayerController>().chanceForUnlock -= 10;
        }
    }
    private IEnumerator PotionWarriorControl()
    {
        yield return new WaitForSeconds(1);
        warriorPotionTime--;
        if (warriorPotionActivated && warriorPotionTime != 0)
            StartCoroutine("PotionWarriorControl");
        if (warriorPotionTime <= 0)
        {
            warriorPotionActivated = false;
            warriorPotionTime = 60;
            player.GetComponent<PlayerController>().hpPlayer -= 50;
            GameObject.Find("GameManager").GetComponent<GameManager>().healthBar.SetMaxValue();
        }
    }
    private IEnumerator PotionArchimageControl()
    {
        yield return new WaitForSeconds(1);
        archimagePotionTime--;
        if (archimagePotionActivated && archimagePotionTime != 0)
            StartCoroutine("PotionArchimageControl");
        if (archimagePotionTime <= 0)
        {
            archimagePotionActivated = false;
            archimagePotionTime = 60;
            player.GetComponent<PlayerController>().manaPlayer -= 50;
            GameObject.Find("GameManager").GetComponent<GameManager>().manaBar.SetMaxValue();
        }
    }
    private IEnumerator PotionRunnerControl()
    {
        yield return new WaitForSeconds(1);
        runnerPotionTime--;
        if (runnerPotionActivated && runnerPotionTime != 0)
            StartCoroutine("PotionRunnerControl");
        if (runnerPotionTime <= 0)
        {
            runnerPotionActivated = false;
            runnerPotionTime = 60;
            player.GetComponent<PlayerController>().staminaPlayer -= 50;
            GameObject.Find("GameManager").GetComponent<GameManager>().staminaBar.SetMaxValue();
        }
    }
    private IEnumerator PotionArmorControl()
    {
        yield return new WaitForSeconds(1);
        potionArmorTime--;
        if (potionArmorActivated && potionArmorTime != 0)
            StartCoroutine("PotionArmorControl");
        if (potionArmorTime <= 0)
        {
            potionArmorActivated = false;
            potionArmorTime = 60;
            player.GetComponent<PlayerController>().potionArmorModify = 0;
            GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
            {
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
        }
    }
    private void LoadInventory()
    {
        InventoryData inventoryData = SaveLoad.globalInventoryData;
        hasArrows = inventoryData.hasArrows;
        for(int i = 0; i < images.Length; i++)
        {
            if (ReturnItemByName(inventoryData.itemName[i]) != null)
            {
                images[i].GetComponent<SlotInfo>().item = ReturnItemByName(inventoryData.itemName[i]);
                images[i].GetComponent<SlotInfo>().amountOfItems = inventoryData.amountOfItems[i];
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                images[i].sprite = ReturnItemByName(inventoryData.itemName[i]).GetComponent<Image>().sprite;
            }
        }
        if (inventoryData.shieldName != null)
        {
            shieldImage.sprite = ReturnItemByName(inventoryData.shieldName).GetComponent<Image>().sprite;
            shieldImage.color = new Color(ReturnItemByName(inventoryData.shieldName).GetComponent<Image>().color.r, ReturnItemByName(inventoryData.shieldName).GetComponent<Image>().color.g, ReturnItemByName(inventoryData.shieldName).GetComponent<Image>().color.b, 255);
            shieldImage.GetComponent<SlotInfo>().item = ReturnItemByName(inventoryData.shieldName);
            GameObject.Find("BowWeapon").GetComponent<MeshFilter>().mesh = shieldImage.GetComponent<SlotInfo>().item.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Find("BowWeapon").GetComponent<MeshFilter>().gameObject.GetComponent<MeshRenderer>().material = shieldImage.GetComponent<SlotInfo>().item.GetComponent<MeshRenderer>().sharedMaterial;
            if (shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Militia shield" || shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчения" || shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчення")
            {
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localScale = new Vector3(40, 40, 40);
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-20, -180, 160);
            }
            if (shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Cheap shield" ||shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешёвый щит" ||shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешевий щит")
            {
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-48, -294, 120);
            }
        }
        if (inventoryData.weaponName != null)
        {
            weaponImage.sprite = ReturnItemByName(inventoryData.weaponName).GetComponent<Image>().sprite;
            weaponImage.color = new Color(ReturnItemByName(inventoryData.weaponName).GetComponent<Image>().color.r, ReturnItemByName(inventoryData.weaponName).GetComponent<Image>().color.g, ReturnItemByName(inventoryData.weaponName).GetComponent<Image>().color.b, 255);
            weaponImage.GetComponent<SlotInfo>().item = ReturnItemByName(inventoryData.weaponName);
            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().ChangeWeaponInHand();
        }
        if (inventoryData.armorName != null)
        {
            armorImage.sprite = ReturnItemByName(inventoryData.armorName).GetComponent<Image>().sprite;
            armorImage.color = new Color(ReturnItemByName(inventoryData.armorName).GetComponent<Image>().color.r, ReturnItemByName(inventoryData.armorName).GetComponent<Image>().color.g, ReturnItemByName(inventoryData.armorName).GetComponent<Image>().color.b, 255);
            armorImage.GetComponent<SlotInfo>().item = ReturnItemByName(inventoryData.armorName);
        }
        if (inventoryData.helmetName != null)
        {
            helmetImage.sprite = ReturnItemByName(inventoryData.helmetName).GetComponent<Image>().sprite;
            helmetImage.color = new Color(ReturnItemByName(inventoryData.helmetName).GetComponent<Image>().color.r, ReturnItemByName(inventoryData.helmetName).GetComponent<Image>().color.g, ReturnItemByName(inventoryData.helmetName).GetComponent<Image>().color.b, 255);
            helmetImage.GetComponent<SlotInfo>().item = ReturnItemByName(inventoryData.helmetName);
            GameObject.Find("PlayerHelmet").GetComponent<MeshFilter>().mesh = ReturnItemByName(inventoryData.helmetName).GetComponent<MeshFilter>().sharedMesh;
        }
        attackPotionTime = inventoryData.potionAttackTime;
        potionAttackActivated = inventoryData.potionAttackActivated;
        potionArmorTime = inventoryData.potionArmorTime;
        potionArmorActivated = inventoryData.potionArmorActivated;
        pickpocketingPotionTime = inventoryData.pickpocketingPotionTime;
        pickpocketingPotionActivated = inventoryData.pickpocketingPotionActivated;
        breakingPotionActivated = inventoryData.breakingPotionActivated;
        breakingPotionTime = inventoryData.breakingPotionTime;
        warriorPotionTime = inventoryData.warriorPotionTime;
        warriorPotionActivated = inventoryData.warriorPotionActivated;
        archimagePotionTime = inventoryData.archimagePotionTime;
        archimagePotionActivated = inventoryData.archimagePotionActivated;
        runnerPotionActivated = inventoryData.runnerPotionActivated;
        runnerPotionTime = inventoryData.runnerPotionTime;
    }
}
