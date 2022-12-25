using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject skillInfo;
    public GameObject selectedItem;
    public int armorModify = 0;
    public int damageModify = 0;
    public int stealthModify = 0;
    public int summonModify = 0;
    public Text needSkillPoint;
    public Transform statsWindow;
    public int oneHandModifyStat;
    public int twoHandModifyStat;
    public int lightArmorModifyStat;
    public int hardArmorModifyStat;
    public int alchemyModifyStat;
    public int battleSpellsModifyStat;
    public int recoverSpellsModifyStat;
    public int summonSpellModifyStat;
    public int archeryModifyStat;
    public GameObject newStatLevel;
    public GameObject simpleHealthPotionWindow;
    public GameObject hugeHealthPotionWindow;
    public GameObject simpleStaminaPotionWindow;
    public GameObject hugeStaminaPotionWindow;
    public GameObject simpleManaPotionWindow;
    public GameObject hugeManaPotionWindow;
    public GameObject berserkPotionWindow;
    public GameObject paladinPotionWindow;
    public GameObject robberPotionWindow;
    public GameObject breakerPotionWindow;
    public GameObject warriorPotionWindow;
    public GameObject archimagePotionWindow;
    public GameObject runnerPotionWindow;
    private void Awake()
    {
        if (SaveLoad.isLoading)
            LoadSkill();
    }
    //Set info of skill
    public void SetSelectedItemSkillInfo()
    {
       selectedItem = EventSystem.current.currentSelectedGameObject.gameObject;
        skillInfo.transform.Find("SkillName").GetComponent<Text>().text = selectedItem.GetComponentInChildren<Text>().text;
        skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
        if (selectedItem.name == "SpellDamage1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage of damage spells on 10%";
        if (selectedItem.name == "SpellDamage2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage of damage spells on 20%";
        if (selectedItem.name == "SpellDamage3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage of damage spells on 30%";
        if (selectedItem.name == "Summon1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage and health of summoned creatures on 10%";
        if (selectedItem.name == "Summon2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage and health of summoned creatures on 20%";
        if (selectedItem.name == "Summon3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage and health of summoned creatures on 30%";
        if (selectedItem.name == "Recover1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases power of recover spells on 10%";
        if (selectedItem.name == "Recover2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases power of recover spells on 20%";
        if (selectedItem.name == "Recover3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases power of recover spells on 30%";
        if (selectedItem.name == "Mana1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max mana 125";
        if (selectedItem.name == "Mana2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max mana 150";
        if (selectedItem.name == "Mana3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max mana to 200";
        if (selectedItem.name == "Health1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max health to 125";
        if (selectedItem.name == "Health2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max health to 150";
        if (selectedItem.name == "Health3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max health to 200";
        if (selectedItem.name == "Armor1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases armor on 10%";
        if (selectedItem.name == "Armor2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases armor on 20%";
        if (selectedItem.name == "Armor3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases armor on 30%";
        if (selectedItem.name == "Damage1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage on 10%";
        if (selectedItem.name == "Damage2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage on 20%";
        if (selectedItem.name == "Damage3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases damage on 30%";
        if (selectedItem.name == "Stamina1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max stamina to 125";
        if (selectedItem.name == "Stamina2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max stamina to 150";
        if (selectedItem.name == "Stamina3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases max stamina to 200";
        if (selectedItem.name == "Pickpocketing1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases pickpocketing chance to 50%";
        if (selectedItem.name == "Pickpocketing2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases pickpocketing chance to 75%";
        if (selectedItem.name == "Pickpocketing3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases pickpocketing chance to 100%";
        if (selectedItem.name == "Breaking1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases breaking chance to 50%";
        if (selectedItem.name == "Breaking2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases breaking chance to 75%";
        if (selectedItem.name == "Breaking3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Increases breaking chance to 100%";
        if (selectedItem.name == "Stealth1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Make you more secretive on 10%";
        if (selectedItem.name == "Stealth2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Make you more secretive on 20%";
        if (selectedItem.name == "Stealth3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Make you more secretive on 30%";
        if (selectedItem.name == "StealthAttack1")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Multiply your stealth attack by 3";
        if (selectedItem.name == "StealthAttack2")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Multiply your stealth attack by 4";
        if (selectedItem.name == "StealthAttack3")
            skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "Multiply your stealth attack by 5";
        if (selectedItem.name == "SpellDamage1" || selectedItem.name == "Summon1" || selectedItem.name == "Recover1" || selectedItem.name == "Mana1" || selectedItem.name == "Stealth1" || selectedItem.name == "Health1" || selectedItem.name == "Stamina1" || selectedItem.name == "Armor1" || selectedItem.name == "Damage1" || selectedItem.name == "Pickpocketing1" || selectedItem.name == "Breaking1" || selectedItem.name == "StealthAttack1")
            skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "Price:1";
        if (selectedItem.name == "SpellDamage2" || selectedItem.name == "Summon2" || selectedItem.name == "Recover2" || selectedItem.name == "Mana2" || selectedItem.name == "Stealth2" || selectedItem.name == "Health2" || selectedItem.name == "Stamina2" || selectedItem.name == "Armor2" || selectedItem.name == "Damage2" || selectedItem.name == "Pickpocketing2" || selectedItem.name == "Breaking2" || selectedItem.name == "StealthAttack2")
            skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "Price:2";
        if (selectedItem.name == "SpellDamage3" || selectedItem.name == "Summon3" || selectedItem.name == "Recover3" || selectedItem.name == "Mana3" || selectedItem.name == "Stealth3" || selectedItem.name == "Health3" || selectedItem.name == "Stamina3" || selectedItem.name == "Armor3" || selectedItem.name == "Damage3" || selectedItem.name == "Pickpocketing3" || selectedItem.name == "Breaking3" || selectedItem.name == "StealthAttack3")
            skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "Price:3";
    }
    //Buy skill if have skillpoint
    public void BuySkill()
    {
        if (selectedItem != null)
        {
            int needSkillPoints = 0;
            if (selectedItem.name == "SpellDamage1" || selectedItem.name == "Summon1" || selectedItem.name == "Recover1" || selectedItem.name == "Mana1" || selectedItem.name == "Stealth1" || selectedItem.name == "Health1" || selectedItem.name == "Stamina1" || selectedItem.name == "Armor1" || selectedItem.name == "Damage1" || selectedItem.name == "Pickpocketing1" || selectedItem.name == "Breaking1" || selectedItem.name == "StealthAttack1")
                needSkillPoints = 1;
            if (selectedItem.name == "SpellDamage2" || selectedItem.name == "Summon2" || selectedItem.name == "Recover2" || selectedItem.name == "Mana2" || selectedItem.name == "Stealth2" || selectedItem.name == "Health2" || selectedItem.name == "Stamina2" || selectedItem.name == "Armor2" || selectedItem.name == "Damage2" || selectedItem.name == "Pickpocketing2" || selectedItem.name == "Breaking2" || selectedItem.name == "StealthAttack2")
                needSkillPoints = 2;
            if (selectedItem.name == "SpellDamage3" || selectedItem.name == "Summon3" || selectedItem.name == "Recover3" || selectedItem.name == "Mana3" || selectedItem.name == "Stealth3" || selectedItem.name == "Health3" || selectedItem.name == "Stamina3" || selectedItem.name == "Armor3" || selectedItem.name == "Damage3" || selectedItem.name == "Pickpocketing3" || selectedItem.name == "Breaking3" || selectedItem.name == "StealthAttack3")
                needSkillPoints = 3;
            if (GameObject.Find("Player").GetComponent<PlayerController>().skillPoints >= needSkillPoints && needSkillPoints != 0)
            {
                if (selectedItem.name == "SpellDamage1")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify = 10;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage2").GetComponent<Image>().color = Color.white;
                    selectedItem = null;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "SpellDamage2")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify = 20;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage3").GetComponent<Image>().color = Color.white;
                    selectedItem = null;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "SpellDamage3")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().fireballDamageModify = 30;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage3").GetComponent<Image>().color = Color.green;
                    selectedItem = null;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Summon1")
                {
                    summonModify = 10;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon2").GetComponent<Button>().interactable = true;
                    selectedItem = null;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Summon2")
                {
                    summonModify = 20;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon3").GetComponent<Button>().interactable = true;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Summon3")
                {
                    summonModify = 30;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Recover1")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().recoverModify = 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover2").GetComponent<Button>().interactable = true;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Recover2")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().recoverModify = 20;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover3").GetComponent<Button>().interactable = true;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Recover3")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().recoverModify = 30;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover3").GetComponent<Button>().interactable = false;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Mana1")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer = 125;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().manaBar.SetMaxValue();
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana2").GetComponent<Button>().interactable = true;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Mana2")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer = 150;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().manaBar.SetMaxValue();
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana2").GetComponent<Button>().interactable = false;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }

                else if (selectedItem.name == "Mana3")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer = 200;
                               if (GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().manaPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().manaBar.SetMaxValue();
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana3").GetComponent<Button>().interactable = false;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Health1")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer = 125;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().healthBar.SetMaxValue();
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Health2")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer = 150;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().healthBar.SetMaxValue();
                    selectedItem = null;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Health3")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer = 200;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().hpPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().healthBar.SetMaxValue();
                    selectedItem = null;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Armor1")
                {
                    armorModify = 10;
                    GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 +GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify/100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.sprite != null)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                            GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                            GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    }
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor2").GetComponent<Image>().color = Color.white;
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Armor2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    armorModify = 20;
                    GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.sprite != null)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                            GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                            GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    }
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor3").GetComponent<Button>().interactable = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Armor3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    armorModify = 30;
                    GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.sprite != null)
                    {
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                            GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                            GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    }
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor3").GetComponent<Button>().interactable = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Damage1")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    damageModify = 10;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                    }
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Damage2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    damageModify = 20;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                    }
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage + GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage * damageModify / 100;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Damage3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    damageModify = 30;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
                    {
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                        if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                            GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
                    }
                    GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage + GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage * damageModify / 100;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Stamina1")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer = 125;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().staminaBar.SetMaxValue();
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina2").GetComponent<Button>().interactable = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Stamina2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer = 150;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().staminaBar.SetMaxValue();
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina3").GetComponent<Button>().interactable = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Stamina3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer = 200;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().staminaPlayer += 50;
                    GameObject.Find("GameManager").GetComponent<GameManager>().staminaBar.SetMaxValue();
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina3").GetComponent<Button>().interactable = false;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Pickpocketing1")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab = 50;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab += 10;
                    selectedItem = null;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing2").GetComponent<Button>().interactable = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Pickpocketing2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab = 75;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab += 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Pickpocketing3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab = 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().chanceForGrab += 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Breaking1")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock = 50;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock += 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Breaking2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock = 75;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock += 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Breaking3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock = 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotionActivated)
                        GameObject.Find("Player").GetComponent<PlayerController>().chanceForUnlock += 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Stealth1")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    stealthModify = 10;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Stealth2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    stealthModify = 20;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "Stealth3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    stealthModify = 30;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "StealthAttack1")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify = 3;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints--;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack1").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack2").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack1").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack2").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "StealthAttack2")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify = 4;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 2;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack2").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack3").GetComponent<Image>().color = Color.white;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack2").GetComponent<Button>().interactable = false;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack3").GetComponent<Button>().interactable = true;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                else if (selectedItem.name == "StealthAttack3")
                {
                    GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_skill);
                    GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify = 5;
                    selectedItem = null;
                    GameObject.Find("Player").GetComponent<PlayerController>().skillPoints -= 3;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack3").GetComponent<Image>().color = Color.green;
                    skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack3").GetComponent<Button>().interactable = false;
                    skillInfo.transform.Find("SkillName").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillDescription").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("NeedSkillpoint").GetComponent<Text>().text = "";
                    skillInfo.transform.Find("SkillPrice").GetComponent<Text>().text = "";
                }
                GameObject.Find("GUIManager").GetComponent<GUIController>().skillUI.transform.Find("SkillPoints").GetComponent<Text>().text = "Skill points:" + GameObject.Find("GUIManager").GetComponent<GUIController>().playerController.skillPoints;
            }
            else if (GameObject.Find("Player").GetComponent<PlayerController>().skillPoints < needSkillPoints && selectedItem != null && needSkillPoints != 0)
            {
                StopCoroutine("NeedSkillPointActivation");
                StartCoroutine("NeedSkillPointActivation");
            }
        }
    }
    //Activate needSkillPoint if try to buy without skillpoint
    private IEnumerator NeedSkillPointActivation()
    {
        needSkillPoint.text = "Need skillpoint";
        yield return new WaitForSeconds(3);
        needSkillPoint.text = "";
    }
    public void OneHandWeaponChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value < 101)
            oneHandModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value<202)
            oneHandModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value >= 202)
            oneHandModifyStat = 30;
        if(GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value==50|| GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value==101|| GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:one-hand weapon";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
        {
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage+ GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat/ 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
        }
    }
    public void TwoHandWeaponChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value<101)
          twoHandModifyStat= 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value<202)
         twoHandModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value >= 202)
         twoHandModifyStat = 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:two-hand weapon";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
        {
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
        }
    }
    public void ArcheryChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value<101)
         archeryModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value<202)
         archeryModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value >= 202)
           archeryModifyStat = 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:archery";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item != null)
        {
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "TwoHand")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "OneHand")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weapon.GetComponent<Item>().weaponType == "Bow")
                GameObject.Find("Player").GetComponentInChildren<HandWeaponScript>().weaponDamage = GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
        }
    }
    public void HardArmorChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value<101)
           hardArmorModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value<202)
          hardArmorModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value >= 202)
           hardArmorModifyStat = 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:hard armor";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.sprite != null)
        {
            if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value == 50)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value++;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
            if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value == 101)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value++;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
            if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value == 202)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
        }
    }
    public void LightArmorChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value<101)
           lightArmorModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value<202)
          lightArmorModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value >= 202)
            lightArmorModifyStat= 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:light armor";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
        if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.sprite != null)
        {
            if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value == 50)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
            if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value == 101)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value++;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
            if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value == 202)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().armor = 0;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item != null)
                {
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "HardArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * hardArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType == "LightArmor")
                        GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * lightArmorModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                }
                    if (GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.sprite != null)
                    GameObject.Find("Player").GetComponent<PlayerController>().armor += GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + GameObject.Find("GUIManager").GetComponent<Inventory>().helmetImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * armorModify / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().armorImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionArmorModify / 100;
            }
        }
    }
    public void AlchemyChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value >= 50 && GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value < 101)
        {
            alchemyModifyStat = 10;
            simpleHealthPotionWindow.SetActive(true);
            simpleManaPotionWindow.SetActive(true);
            simpleStaminaPotionWindow.SetActive(true);
        }
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value >= 101 && GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value < 202)
        {
            alchemyModifyStat = 20;
            hugeHealthPotionWindow.SetActive(true);
            hugeManaPotionWindow.SetActive(true);
            hugeStaminaPotionWindow.SetActive(true);
        }
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value >= 202)
        {
            warriorPotionWindow.SetActive(true);
            archimagePotionWindow.SetActive(true);
            robberPotionWindow.SetActive(true);
            runnerPotionWindow.SetActive(true);
            breakerPotionWindow.SetActive(true);
            berserkPotionWindow.SetActive(true);
            paladinPotionWindow.SetActive(true);
            alchemyModifyStat = 30;
        }
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:Alchemy";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
    }
    public void BattleSpellsChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value<101)
            battleSpellsModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value<202)
            battleSpellsModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value >= 202)
           battleSpellsModifyStat = 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:battle spells";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
    }
    public void RecoverChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value<101)
         recoverSpellsModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value >= 101&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value<202)
         recoverSpellsModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value >= 202)
          recoverSpellsModifyStat = 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:recover spells";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
    }
    public void SummonChange()
    {
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value >= 50&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value<100)
           summonSpellModifyStat = 10;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value >= 100&& GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value<200)
           summonSpellModifyStat = 20;
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value >= 200)
          summonSpellModifyStat = 30;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value == 50 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value == 101 || GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value == 202)
        {
            GooglePlayAchievements.UnlockRegular(GPGSIds.achievement_stat);
            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value++;
            newStatLevel.GetComponentInChildren<Text>().text = "New level:summon spells";
            StopCoroutine("ShowNewStatLevel");
            StartCoroutine("ShowNewStatLevel");
        }
    }
    private void LoadSkill()
    {
        SkillData skillData = SaveLoad.globalSkillData;
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("OneHandSlider").GetComponent<Slider>().value = skillData.statValues[0];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("TwoHandSlider").GetComponent<Slider>().value = skillData.statValues[1];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value = skillData.statValues[2];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("HardArmorSlider").GetComponent<Slider>().value = skillData.statValues[3];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("LightArmorSlider").GetComponent<Slider>().value = skillData.statValues[4];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value = skillData.statValues[5];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value = skillData.statValues[6];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("RecoverSpellsSlider").GetComponent<Slider>().value = skillData.statValues[7];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("SummonSpellsSlider").GetComponent<Slider>().value = skillData.statValues[8];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage1").GetComponent<Image>().color = new Color(skillData.color[0, 0], skillData.color[0, 1], skillData.color[0, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage1").GetComponent<Button>().interactable=skillData.enabled[0];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage2").GetComponent<Image>().color = new Color(skillData.color[1, 0], skillData.color[1, 1], skillData.color[1, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage2").GetComponent<Button>().interactable = skillData.enabled[1];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage3").GetComponent<Image>().color = new Color(skillData.color[2, 0], skillData.color[2, 1], skillData.color[2, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("SpellDamage3").GetComponent<Button>().interactable = skillData.enabled[2];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon1").GetComponent<Image>().color = new Color(skillData.color[3, 0], skillData.color[3, 1], skillData.color[3, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon1").GetComponent<Button>().interactable = skillData.enabled[3];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon2").GetComponent<Image>().color = new Color(skillData.color[4, 0], skillData.color[4, 1], skillData.color[4, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon2").GetComponent<Button>().interactable = skillData.enabled[4];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon3").GetComponent<Image>().color = new Color(skillData.color[5, 0], skillData.color[5, 1], skillData.color[5, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Summon3").GetComponent<Button>().interactable = skillData.enabled[5];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover1").GetComponent<Image>().color = new Color(skillData.color[6, 0], skillData.color[6, 1], skillData.color[6, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover1").GetComponent<Button>().interactable = skillData.enabled[6];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover2").GetComponent<Image>().color = new Color(skillData.color[7, 0], skillData.color[7, 1], skillData.color[7, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover2").GetComponent<Button>().interactable = skillData.enabled[7];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover3").GetComponent<Image>().color = new Color(skillData.color[8, 0], skillData.color[8, 1], skillData.color[8, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Recover3").GetComponent<Button>().interactable = skillData.enabled[8];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana1").GetComponent<Image>().color = new Color(skillData.color[9, 0], skillData.color[9, 1], skillData.color[9, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana1").GetComponent<Button>().interactable = skillData.enabled[9];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana2").GetComponent<Image>().color = new Color(skillData.color[10, 0], skillData.color[10, 1], skillData.color[10, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana2").GetComponent<Button>().interactable = skillData.enabled[10];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana3").GetComponent<Image>().color = new Color(skillData.color[11, 0], skillData.color[11, 1], skillData.color[11, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("MageSkills").transform.Find("Mana3").GetComponent<Button>().interactable = skillData.enabled[11];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health1").GetComponent<Image>().color = new Color(skillData.color[12, 0], skillData.color[12, 1], skillData.color[12, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health1").GetComponent<Button>().interactable = skillData.enabled[12];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health2").GetComponent<Image>().color = new Color(skillData.color[13, 0], skillData.color[13, 1], skillData.color[13, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health2").GetComponent<Button>().interactable = skillData.enabled[13];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health3").GetComponent<Image>().color = new Color(skillData.color[14, 0], skillData.color[14, 1], skillData.color[14, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Health3").GetComponent<Button>().interactable = skillData.enabled[14];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor1").GetComponent<Image>().color = new Color(skillData.color[15, 0], skillData.color[15, 1], skillData.color[15, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor1").GetComponent<Button>().interactable = skillData.enabled[15];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor2").GetComponent<Image>().color = new Color(skillData.color[16, 0], skillData.color[16, 1], skillData.color[16, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor2").GetComponent<Button>().interactable = skillData.enabled[16];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor3").GetComponent<Image>().color = new Color(skillData.color[17, 0], skillData.color[17, 1], skillData.color[17, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Armor3").GetComponent<Button>().interactable = skillData.enabled[17];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage1").GetComponent<Image>().color = new Color(skillData.color[18, 0], skillData.color[18, 1], skillData.color[18, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage1").GetComponent<Button>().interactable = skillData.enabled[18];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage2").GetComponent<Image>().color = new Color(skillData.color[19, 0], skillData.color[19, 1], skillData.color[19, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage2").GetComponent<Button>().interactable = skillData.enabled[19];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage3").GetComponent<Image>().color = new Color(skillData.color[20, 0], skillData.color[20, 1], skillData.color[20, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Damage3").GetComponent<Button>().interactable = skillData.enabled[20];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina1").GetComponent<Image>().color = new Color(skillData.color[21, 0], skillData.color[21, 1], skillData.color[21, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina1").GetComponent<Button>().interactable = skillData.enabled[21];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina2").GetComponent<Image>().color = new Color(skillData.color[22, 0], skillData.color[22, 1], skillData.color[22, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina2").GetComponent<Button>().interactable = skillData.enabled[22];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina3").GetComponent<Image>().color = new Color(skillData.color[23, 0], skillData.color[23, 1], skillData.color[23, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("PaladinSkills").transform.Find("Stamina3").GetComponent<Button>().interactable = skillData.enabled[23];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing1").GetComponent<Image>().color = new Color(skillData.color[24, 0], skillData.color[24, 1], skillData.color[24, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing1").GetComponent<Button>().interactable = skillData.enabled[24];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing2").GetComponent<Image>().color = new Color(skillData.color[25, 0], skillData.color[25, 1], skillData.color[25, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing2").GetComponent<Button>().interactable = skillData.enabled[25];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing3").GetComponent<Image>().color = new Color(skillData.color[26, 0], skillData.color[26, 1], skillData.color[26, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Pickpocketing3").GetComponent<Button>().interactable = skillData.enabled[26];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking1").GetComponent<Image>().color = new Color(skillData.color[27, 0], skillData.color[27, 1], skillData.color[27, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking1").GetComponent<Button>().interactable = skillData.enabled[27];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking2").GetComponent<Image>().color = new Color(skillData.color[28, 0], skillData.color[28, 1], skillData.color[28, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking2").GetComponent<Button>().interactable = skillData.enabled[28];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking3").GetComponent<Image>().color = new Color(skillData.color[29, 0], skillData.color[29, 1], skillData.color[29, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Breaking3").GetComponent<Button>().interactable = skillData.enabled[29];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth1").GetComponent<Image>().color = new Color(skillData.color[30, 0], skillData.color[30, 1], skillData.color[30, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth1").GetComponent<Button>().interactable = skillData.enabled[30];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth2").GetComponent<Image>().color = new Color(skillData.color[31, 0], skillData.color[31, 1], skillData.color[31, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth2").GetComponent<Button>().interactable = skillData.enabled[31];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth3").GetComponent<Image>().color = new Color(skillData.color[32, 0], skillData.color[32, 1], skillData.color[32, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("Stealth3").GetComponent<Button>().interactable = skillData.enabled[32];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack1").GetComponent<Image>().color = new Color(skillData.color[33, 0], skillData.color[33, 1], skillData.color[33, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack1").GetComponent<Button>().interactable = skillData.enabled[33];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack2").GetComponent<Image>().color = new Color(skillData.color[34, 0], skillData.color[34, 1], skillData.color[34, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack2").GetComponent<Button>().interactable = skillData.enabled[34];
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack3").GetComponent<Image>().color = new Color(skillData.color[35, 0], skillData.color[35, 1], skillData.color[35, 2]);
        GameObject.Find("SkillManager").GetComponent<SkillManager>().skillInfo.transform.parent.transform.Find("RobberSkills").transform.Find("StealthAttack3").GetComponent<Button>().interactable = skillData.enabled[35];
        armorModify = skillData.armorModify;
        damageModify = skillData.damageModify;
        summonModify = skillData.summonModify;
        stealthModify = skillData.stealthModify;
        oneHandModifyStat = skillData.oneHandModifyStat;
        twoHandModifyStat = skillData.twoHandModifyStat;
        lightArmorModifyStat = skillData.lightArmorModifyStat;
        hardArmorModifyStat = skillData.hardArmorModifyStat;
        alchemyModifyStat = skillData.alchemyModifyStat;
        summonSpellModifyStat = skillData.summonSpellModifyStat;
        battleSpellsModifyStat = skillData.battleSpellsModifyStat;
        recoverSpellsModifyStat = skillData.recoverSpellsModifyStat;
        archeryModifyStat = skillData.archeryModifyStat;
        if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value >= 50 && GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value < 101)
        {
            simpleHealthPotionWindow.SetActive(true);
            simpleManaPotionWindow.SetActive(true);
            simpleStaminaPotionWindow.SetActive(true);
        }
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value >= 101 && GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value < 202)
        {
            hugeHealthPotionWindow.SetActive(true);
            hugeManaPotionWindow.SetActive(true);
            hugeStaminaPotionWindow.SetActive(true);
        }
        else if (GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("AlchemySlider").GetComponent<Slider>().value >= 202)
        {
            warriorPotionWindow.SetActive(true);
            archimagePotionWindow.SetActive(true);
            robberPotionWindow.SetActive(true);
            runnerPotionWindow.SetActive(true);
            breakerPotionWindow.SetActive(true);
            berserkPotionWindow.SetActive(true);
            paladinPotionWindow.SetActive(true);
        }
    }
    private IEnumerator ShowNewStatLevel()
    {
        newStatLevel.SetActive(true);
        yield return new WaitForSeconds(3);
        newStatLevel.SetActive(false);
    }
}
