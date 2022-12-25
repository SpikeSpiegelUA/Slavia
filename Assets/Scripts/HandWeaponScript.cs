using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandWeaponScript : MonoBehaviour
{
    public int weaponDamage;
        public GameObject weapon;
    public Inventory inventory;
    public Material weaponMaterial;
    public string playerName;
    public bool melee = true;
    public bool spell = false;
    private void Update()
    {
        if (weapon != null)
        {
            if (weapon.GetComponent<Item>().weaponType == "TwoHand")
                GetComponentInParent<Animator>().SetBool("TwoHand", true);
            if (weapon.GetComponent<Item>().weaponType == "OneHand")
                GetComponentInParent<Animator>().SetBool("TwoHand", false);
            if (weapon.GetComponent<Item>().weaponType == "Bow")
                GetComponentInParent<Animator>().SetBool("TwoHand", false);
        }
        else
            GetComponentInParent<Animator>().SetBool("TwoHand", false);
    }
        public void ChangeWeaponInHand() {
        //If player has equiped weapon set weapon's info to player's weapon in hand
        if (inventory.weaponImage.GetComponent<Image>().sprite != null)
        {   
                weapon = inventory.weaponImage.GetComponent<SlotInfo>().item;
            if(weapon.GetComponent<Item>().weaponType == "TwoHand")
            weaponDamage = inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100+inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().twoHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (weapon.GetComponent<Item>().weaponType == "OneHand")
                weaponDamage = inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().oneHandModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (weapon.GetComponent<Item>().weaponType == "Bow")
                weaponDamage = inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage + inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().damageModify / 100 + inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("SkillManager").GetComponent<SkillManager>().archeryModifyStat / 100 + GameObject.Find("GUIManager").GetComponent<Inventory>().weaponImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemDamage * GameObject.Find("Player").GetComponent<PlayerController>().potionAttackModify / 100;
            if (weapon.GetComponent<Item>().weaponType!="Bow")
                GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<MeshFilter>().sharedMesh;          
        }
        if (weapon != null)
            if (weapon.GetComponent<Item>().weaponType == "TwoHand")
            {
                GameObject.Find("PlayerWeapon").transform.localPosition = new Vector3(0.071f, 0.08f, 0.045f);
                GameObject.Find("PlayerWeapon").transform.localEulerAngles = new Vector3(147, 5, -16);
                GameObject.Find("PlayerWeapon").transform.localScale = new Vector3(1, 0.7f, 1);
                GameObject.Find("PlayerWeapon").GetComponent<MeshRenderer>().material = weapon.GetComponent<MeshRenderer>().sharedMaterial;
            }
        else
                GameObject.Find("PlayerWeapon").transform.localPosition = new Vector3(-0.011f,0.004f,-0.004f);
        //If player hasn't equiped weapon equip fists
        if (inventory.weaponImage.GetComponent<Image>().sprite == null)
        {
            if (GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh.name != "Arrow_Regular Instance")
            {
                weapon = null;
                weaponDamage = 1;
                GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().mesh = null;
                melee = true;
                spell = false;
            }
        }
        //Change rotation if player has equiped bow
        if (weapon != null)
            if (weapon.GetComponent<Item>().weaponType == "Bow")
            {
                if (GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh.name != "Arrow_Regular Instance")
                    GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().mesh = inventory.weaponImage.GetComponent<SlotInfo>().item.GetComponent<MeshFilter>().sharedMesh;
                melee = false;
                spell = false;
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().gameObject.GetComponent<MeshRenderer>().material = weaponMaterial;
                if (weapon.name == "Militia bow" || weapon.name == "Лук ополчения" || weapon.name == "Лук ополчення")
                {
                    GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(15, 90, 90);
                }
                else if (weapon.name == "Professional bow" || weapon.name == "Профессиональный лук" || weapon.name == "Професійний лук")
                {
                    GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localScale = new Vector3(40, 40, 40);
                    GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(15, 90, 90);
                }
            }
        //Change object if shield is equiped
         if(inventory.shieldImage.sprite !=null)
            {
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().mesh = inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().gameObject.GetComponent<MeshRenderer>().material = inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<MeshRenderer>().sharedMaterial;
            if(inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName=="Militia shield"|| inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчения"|| inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Щит ополчення")
            {
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localScale = new Vector3(40,40,40);
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-20,-180,160);
            }
            if (inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Cheap shield" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешёвый щит" || inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().itemName == "Дешевий щит")
            {
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().transform.localEulerAngles = new Vector3(-48,-294,120);
            }
        }
        //Change rotation if player hasn't equiped bow
        if(weapon!=null)
          if(weapon.GetComponent<Item>().weaponType != "Bow"&& weapon.GetComponent<Item>().weaponType != "TwoHand")
        {
                if(inventory.shieldImage.sprite!=null)
            if(inventory.shieldImage.GetComponent<SlotInfo>().item.GetComponent<Item>().weaponType != "Shield")
                GameObject.Find("BowWeapon").GetComponent<MeshFilter>().mesh = null;
                GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = weapon.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Find("PlayerWeapon").GetComponent<MeshRenderer>().material = weapon.GetComponent<MeshRenderer>().sharedMaterial;
                    GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(165, 24, 171);
                    GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                melee = true;
                spell = false;
        }
        if (weapon != null)
            if(weapon.GetComponent<Item>().itemName=="Iron sword"||weapon.GetComponent<Item>().itemName=="Железный меч"|| weapon.GetComponent<Item>().itemName=="Залізний меч")
            {
                GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(131,-72,76);
                GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(0.2f,0.2f,0.2f);
            }
        if (weapon != null)
            if (weapon.GetComponent<Item>().itemName == "Big iron sword" || weapon.GetComponent<Item>().itemName == "Большой железный меч" || weapon.GetComponent<Item>().itemName == "Великий залізний меч")
            {
                GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(116,-55,314);
                GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
                GameObject.Find("PlayerWeapon").gameObject.transform.localPosition = new Vector3(0.001f, 0.01f, 0.041f);
            }
        if (weapon != null)
            if (weapon.GetComponent<Item>().itemName == "Militia sword" || weapon.GetComponent<Item>().itemName == "Меч ополчения" || weapon.GetComponent<Item>().itemName == "Гайдамацький меч")
            {
                GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(165,24,317);
                GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(40,40,40);
            }
        if (weapon != null)
            if (weapon.GetComponent<Item>().itemName == "Big militia sword" || weapon.GetComponent<Item>().itemName == "Большой меч ополчения" || weapon.GetComponent<Item>().itemName == "Великий гайдамацький меч")
            {
                GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(147,5,317);
                GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(60,60,60);
            }
        if (weapon != null)
            if (weapon.GetComponent<Item>().itemName == "Soldier sword" || weapon.GetComponent<Item>().itemName == "Меч солдата" || weapon.GetComponent<Item>().itemName == "Меч солдата")
            {
                GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(219,-61,-15);
                GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(0.6f,0.6f,0.6f);
            }
        if (weapon != null)
            if (weapon.GetComponent<Item>().itemName == "Big waraxe" || weapon.GetComponent<Item>().itemName == "Большая секира" || weapon.GetComponent<Item>().itemName == "Велика сокира")
            {
                GameObject.Find("PlayerWeapon").gameObject.transform.localEulerAngles = new Vector3(147,6,511);
                GameObject.Find("PlayerWeapon").GetComponent<RectTransform>().localScale = new Vector3(2,2,2.5f);
                GameObject.Find("PlayerWeapon").gameObject.transform.localPosition = new Vector3(0.046f, 0.067f, 0.053f);
            }
        if (weapon != null)
            if (weapon.GetComponent<Item>().itemInventoryTag == "Spell")
            {
                GameObject.Find("PlayerWeapon").GetComponent<MeshFilter>().mesh = null;
                melee = true;
                spell = true;
            }
        
    }
}

