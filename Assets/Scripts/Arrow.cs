using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Arrow : MonoBehaviour
{
    public GameObject shooter;
    public bool canDamage = false;
    public int arrowDamage;
    private Vector3 velocityBefore;
    void Update()
    {
        if (canDamage == false && shooter != null)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        }
        if (shooter== null || canDamage ==true)
        {
            GetComponent<MeshCollider>().enabled = true;
        }
        else
        {
            GetComponent<MeshCollider>().enabled = false;
        }
    }
    void FixedUpdate()
    {
        velocityBefore = GetComponent<Rigidbody>().velocity;
    }
    //When arrow collides with enemy or guard or civillian hit damage and disable damage function
    void OnCollisionEnter(Collision collision)
    {
        if (shooter != null)
        {
            if (collision.transform.root.GetComponent<GuardAI>() != null)
                if (collision.collider.name == "Shield")
                {
                    canDamage = true;
                    transform.localPosition += ((collision.GetContact(0).point - transform.position) * 0.2f);
                    transform.SetParent(collision.gameObject.transform, true);
                    GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    GetComponent<Rigidbody>().isKinematic = true;
                    collision.collider.gameObject.GetComponentInParent<GuardAI>().detection = 100;
                    GameObject.Find("Player").GetComponentInParent<PlayerController>().isDetected = true;
                    if(collision.gameObject.GetComponentInParent<GuardAI>().objectToAttack==null)
                    collision.gameObject.GetComponentInParent<GuardAI>().objectToAttack = shooter;
                    collision.gameObject.GetComponentInParent<GuardAI>().isAlerted = true;
                    collision.gameObject.GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                    if (shooter == GameObject.Find("Player") && (collision.transform.root.tag == "VillageGuard" || collision.transform.root.tag == "Civilian" || collision.transform.root.tag == "SimplePeople"))
                        GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                    if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "Republican")
                        GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                    if (collision.collider.transform.root.name != "PatrolRoyalist2" && collision.collider.transform.root.name != "PatrolRoyalist1" && collision.collider.transform.root.name != "StrangeRoyalist")
                        if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "Royalist")
                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                    if (collision.collider.transform.root.name == "StrangeRoyalist")
                    {
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                CallNear(collision);
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                            CallNear(collision);
                    }
                    if (collision.transform.root.name != "PatrolRoyalist2" && collision.transform.root.name != "PatrolRoyalist1" && collision.transform.root.name != "StrangeRoyalist")
                        CallNear(collision);
                    if (collision.collider.transform.root.name == "StrangeRoyalist")
                    {
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                            if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                CallNear(collision);
                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                            CallNear(collision);
                    }
                }
            if (collision.transform.root.GetComponent<PlayerController>() != null)
                if (GameObject.Find("GUIManager").GetComponent<Inventory>().shieldImage.GetComponent<SlotInfo>().item != null)
                    if (collision.transform.root.GetComponent<PlayerController>().block)
                        if (checkShieldPlayer(collision))
                            canDamage = true;
            if(canDamage==false)
            GetComponent<Rigidbody>().velocity = velocityBefore;
            if (canDamage == false && collision.collider.gameObject != shooter.gameObject && shooter.gameObject.tag != collision.collider.transform.root.tag)
            {
                //Damage for Summoned
                if (collision.gameObject.transform.root.tag == "Summoned")
                {
                    if (collision.gameObject.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                    { 
                        if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                            collision.gameObject.GetComponentInParent<SummonedAI>().currentHP -= arrowDamage;
                    }
                }
                //Damage for civillians
                if ((collision.gameObject.transform.root.tag == "Civilian" || collision.transform.root.tag == "SimplePeople"||collision.transform.root.name=="Solovey"))
                {
                    if (collision.gameObject.transform.root.GetComponent<CivilianAI>().currentHP > 0)
                    {
                        if (shooter == GameObject.Find("Player"))
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value++;
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                collision.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= arrowDamage;
                            else if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                collision.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= arrowDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0)
                            {
                                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                                    shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                    GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                            }
                        }
                        else if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0)
                        {
                            if (collision.transform.root.tag != shooter.tag)
                                collision.collider.gameObject.GetComponentInParent<CivilianAI>().currentHP -= arrowDamage;
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0 && shooter.GetComponent<SummonedAI>() != null)
                            {
                                if (shooter.GetComponent<SummonedAI>().summoner.GetComponent<PlayerController>() != null)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                                    StartCoroutine("KillExperienceShow");
                                    GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                                }
                            }
                            if(collision.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0)
                                if (shooter.GetComponent<GuardAI>() != null)
                                    shooter.GetComponent<GuardAI>().objectToAttack = null;
                        }
                        if (shooter == GameObject.Find("Player")&&collision.collider.transform.root.name!="Solovey")
                            GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                        if (shooter == GameObject.Find("Player"))
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0&&collision.collider.transform.root.name!="Solovey")
                            {
                                collision.collider.gameObject.GetComponentInParent<CivilianAI>().detection = 100;
                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                if(collision.collider.GetComponentInParent<CivilianAI>().attacker==null)
                                collision.collider.gameObject.GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                    collision.collider.gameObject.GetComponentInParent<CivilianAI>().attacker = shooter;
                                collision.gameObject.GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                            }
                        CallNear(collision);
                    }
                }
                //Damage for guard
                if ((collision.gameObject.transform.root.tag == "VillageGuard" && shooter.transform.root.tag != "VillageGuard")|| (collision.gameObject.transform.root.tag == "Bandit" && shooter.transform.root.tag != "Bandit") || (collision.gameObject.transform.root.tag == "Royalist" && shooter.transform.root.tag != "Royalist") || (collision.gameObject.transform.root.tag == "Republican" && shooter.transform.root.tag != "Republican") || (collision.gameObject.transform.root.tag == "Undead" && shooter.transform.root.tag != "Undead"))
                {
                    if(collision.collider.GetComponentInParent<GuardAI>()!=null)
                    if (collision.gameObject.transform.root.GetComponent<GuardAI>().currentHP > 0)
                    {
                            if (shooter == GameObject.Find("Player"))
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value++;
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                {
                                    if (arrowDamage - collision.collider.GetComponentInParent<GuardAI>().armor / 100f * arrowDamage > 0)
                                        collision.collider.GetComponentInParent<GuardAI>().currentHP -= (int)(arrowDamage - collision.collider.GetComponentInParent<GuardAI>().armor / 100f * arrowDamage);
                                    if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                        shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                        GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                    }
                                }
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                {
                                    if (collision.transform.root.tag != shooter.tag)
                                        if (arrowDamage - collision.collider.GetComponentInParent<GuardAI>().armor / 100f * arrowDamage > 0)
                                        collision.collider.GetComponentInParent<GuardAI>().currentHP -= (int)(arrowDamage - collision.collider.GetComponentInParent<GuardAI>().armor / 100f * arrowDamage) * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                                    if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                        shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                        GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                    }
                                }
                            }
                            else if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                            {
                                collision.collider.gameObject.GetComponentInParent<GuardAI>().currentHP -= (int)(arrowDamage - collision.collider.GetComponentInParent<GuardAI>().armor / 100f * arrowDamage);
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0 && shooter.GetComponent<SummonedAI>() != null)
                                {
                                    if (shooter.GetComponent<SummonedAI>().summoner.GetComponent<PlayerController>() != null)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                        StartCoroutine("KillExperienceShow");
                                        GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                    }
                                }
                                if(collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                                    if (shooter.GetComponent<GuardAI>() != null)
                                        shooter.GetComponent<GuardAI>().objectToAttack = null;
                            }
                            if (shooter == GameObject.Find("Player"))
                            if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                            {
                                collision.collider.gameObject.GetComponentInParent<GuardAI>().detection = 100;
                                GameObject.Find("Player").GetComponentInParent<PlayerController>().isDetected = true;
                                    if (collision.gameObject.GetComponentInParent<GuardAI>().objectToAttack == null)
                                        collision.gameObject.GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                collision.gameObject.GetComponentInParent<GuardAI>().isAlerted = true;
                                collision.gameObject.GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                            }
                            if (collision.collider.transform.root.name != "Solovey")
                            {
                                if (shooter == GameObject.Find("Player") && collision.gameObject.transform.root.tag == "VillageGuard")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                if (shooter == GameObject.Find("Player") && collision.gameObject.transform.root.tag == "Republican")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                if (collision.collider.transform.root.name != "PatrolRoyalist2" && collision.collider.transform.root.name != "PatrolRoyalist1"&& collision.collider.transform.root.name != "StrangeRoyalist")
                                    if (shooter == GameObject.Find("Player") && collision.gameObject.transform.root.tag == "Royalist")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                if (collision.collider.transform.root.name == "StrangeRoyalist")
                                {
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                            GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                }
                            }
                            if (collision.collider.transform.root.name != "PatrolRoyalist2" && collision.collider.transform.root.name != "PatrolRoyalist1" && collision.collider.transform.root.name != "StrangeRoyalist")
                                CallNear(collision);
                            if (collision.collider.transform.root.name == "StrangeRoyalist")
                            {
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                        CallNear(collision);
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                    CallNear(collision);
                            }
                        }
                    else if (collision.collider.GetComponentInParent<SummonedAI>() != null)
                        {
                            if (collision.gameObject.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                            {
                                if (shooter == GameObject.Find("Player"))
                                {
                                    GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value++;
                                    if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                    {
                                        collision.collider.GetComponentInParent<GuardAI>().currentHP -= arrowDamage;
                                    }
                                    if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                    {
                                            collision.collider.GetComponentInParent<SummonedAI>().currentHP -= arrowDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                                    }
                                }
                                else if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                                    if(collision.transform.root.tag!=shooter.tag)
                                    collision.collider.gameObject.GetComponentInParent<SummonedAI>().currentHP -= arrowDamage;
                                if(collision.gameObject.GetComponentInParent<SummonedAI>().currentHP <= 0)
                                    if (shooter.GetComponent<GuardAI>() != null)
                                        shooter.GetComponent<GuardAI>().objectToAttack = null;
                                if (shooter == GameObject.Find("Player"))
                                    if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                                    {
                                        GameObject.Find("Player").GetComponentInParent<PlayerController>().isDetected = true;
                                        if (collision.gameObject.GetComponentInParent<SummonedAI>().objectToAttack==null)
                                        collision.gameObject.GetComponentInParent<SummonedAI>().objectToAttack = shooter;
                                        collision.gameObject.GetComponentInParent<SummonedAI>().isAlerted = true;
                                    }
                                if (collision.collider.transform.root.name != "Solovey")
                                {
                                    if (shooter == GameObject.Find("Player") && collision.gameObject.transform.root.tag == "VillageGuard")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                    if (shooter == GameObject.Find("Player") && collision.gameObject.transform.root.tag == "Republican")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                    if (shooter == GameObject.Find("Player") && collision.gameObject.transform.root.tag == "Royalist")
                                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                }
                                CallNear(collision);
                            }
                        }
                }
                if (collision.collider.GetComponentInParent<AnimalAI>()!=null)
                {
                    if (collision.gameObject.transform.root.GetComponent<AnimalAI>().currentHP > 0)
                    {
                        if (shooter == GameObject.Find("Player"))
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("ArcherySlider").GetComponent<Slider>().value++;
                            if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                collision.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= arrowDamage;
                            else if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                collision.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= arrowDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                            if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP <= 0)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                                shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                            }
                        }
                        else if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0)
                        {
                            if (collision.transform.root.tag != shooter.tag)
                                collision.collider.gameObject.GetComponentInParent<AnimalAI>().currentHP -= arrowDamage;
                            if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP <= 0 && shooter.GetComponent<SummonedAI>() != null)
                            {
                                if (shooter.GetComponent<SummonedAI>().summoner.GetComponent<PlayerController>() != null)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                                    StartCoroutine("KillExperienceShow");
                                    GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                                }
                            }
                        }
                    }
                }
                //Damage for Player
                if (collision.gameObject.tag == "Player")
                    {
                        if (collision.gameObject.GetComponent<PlayerController>().currentHealth > 0)
                        {
                        if (arrowDamage - collision.collider.GetComponent<PlayerController>().armor / 100f * arrowDamage > 0)
                            collision.collider.GetComponent<PlayerController>().currentHealth -= (int)(arrowDamage - collision.collider.GetComponent<PlayerController>().armor / 100f * arrowDamage);
                        if(collision.collider.GetComponent<PlayerController>().currentHealth<=0)
                            if (shooter.GetComponent<GuardAI>() != null)
                                shooter.GetComponent<GuardAI>().objectToAttack = null;
                        collision.gameObject.GetComponent<PlayerController>().StartCoroutine("DamageScreenAppear");
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
                    }
            }
            if (collision.collider.transform.root.tag != "Solid"&&canDamage==false)
            {
                transform.localPosition += ((collision.GetContact(0).point - transform.position) * 0.2f);
                if (!collision.collider.isTrigger)
                    transform.SetParent(collision.collider.transform, true);
                else
                    transform.SetParent(collision.collider.transform.GetChild(0).transform, true);
                GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                GetComponent<Rigidbody>().isKinematic = true;
            }
            if (collision.collider.transform.root != shooter.gameObject && collision.collider.transform.root.tag != shooter.gameObject.tag)
                canDamage = true;
            if (collision.collider.transform.root.name == "Player")
                Destroy(gameObject);
            if (collision.collider.transform.root.tag == "Summoned"&&shooter.tag!="Summoned")
                Destroy(gameObject);
            if (collision.collider.transform.root.tag == "Undead" && shooter.tag != "Undead")
                Destroy(gameObject);
        }
    }
    public void CallNear(Collision collision)
    {
        //Call nearest person
        if (shooter == GameObject.Find("Player") && (collision.transform.root.tag == "SimplePeople" || collision.transform.root.tag == "Civilian" || collision.transform.root.tag == "VillageGuard"||collision.transform.root.tag=="Bandit" || collision.transform.root.tag == "Royalist" || collision.transform.root.tag == "Republican" || collision.transform.root.tag == "Undead"))
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                    {
                        if (collision.collider.transform.root.tag == "VillageGuard")
                            if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                            {
                                if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if(GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack==null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }

                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                            }
                        if (collision.collider.transform.root.tag == "SimplePeople" || collision.collider.transform.root.tag == "Civilian")
                            if (collision.transform.root.GetComponent<CivilianAI>().currentHP <= 0)
                            {
                                if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }

                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                            }
                    }
                }
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))                       
                    if (collision.collider.transform.root.tag == "Bandit"&&collision.collider.transform.root.name!="Solovey")
                                if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                                {
                                    if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                        if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit")
                                        {
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                            GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                        }
                                }
                                else
                                {
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                                }
                    if (collision.collider.transform.root.name == "Solovey")
                        if (collision.collider.GetComponentInParent<SummonedAI>().currentHP <= 0 || collision.collider.GetComponentInParent<CivilianAI>().currentHP <= 0)
                        {
                            if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit"&&collision.collider.transform.root.tag=="Bandit")
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                        }
                        else
                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit" && collision.collider.transform.root.tag == "Bandit")
                            {
                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                            if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                            }
                }
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Royalist")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.collider.transform.root.tag == "Royalist")
                            if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                            {
                                if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Royalist")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Royalist")
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                            }
                }
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Republican")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.collider.transform.root.tag == "Republican")
                            if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                            {
                                if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Republican")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Republican")
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                            }
                }
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Undead")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.collider.transform.root.tag == "Undead")
                            if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                            {
                                if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Undead")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                            GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }
                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Undead")
                                {
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack == null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                    GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                }
                            }
                }
            }
            for (int i = 0; i < GameObject.FindObjectsOfType<CivilianAI>().Length; i++)
            {
                Ray ray = new Ray(GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position + GameObject.FindObjectsOfType<CivilianAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position);
                Ray personRay = new Ray(GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position + GameObject.FindObjectsOfType<CivilianAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<CivilianAI>()[i].transform.forward);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    if (collision.collider.transform.root.tag == "SimplePeople" || collision.collider.transform.root.tag == "Civilian")
                        if (collision.transform.root.GetComponent<CivilianAI>().currentHP <= 0)
                        {
                            if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10)
                                {
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = shooter;
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
                        else
                        {
                            if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10)
                            {
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = shooter;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().CallNear();
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                                if (GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount == false)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().combatEnemies++;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().plusToCount = true;
                                }
                            }
                        }
                    if (collision.collider.transform.root.tag == "VillageGuard")
                        if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                        {
                            if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10)
                                {
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                    GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                    GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = shooter;
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
                        else
                        {
                            if ((GameObject.FindObjectsOfType<CivilianAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10)
                            {
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().detection = 100;
                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                GameObject.FindObjectsOfType<CivilianAI>()[i].GetComponentInParent<CivilianAI>().attacker = shooter;
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
        }
        else if (shooter != null)
            if (shooter.GetComponent<PlayerController>() == null)
            {
                if (collision.collider.transform.root.tag == "VillageGuard" || collision.collider.transform.root.tag == "SimplePeople" || collision.collider.transform.root.tag == "Civilian")
                {
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                    {
                        if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "VillageGuard")
                        {
                            if (GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack == null)
                                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                        }
                    }
                    for (int b = 0; b < GameObject.FindObjectsOfType<CivilianAI>().Length; b++)
                    {
                        if ((GameObject.FindObjectsOfType<CivilianAI>()[b].transform.position - transform.position).magnitude <= 10)
                        {
                            GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().attacker = shooter;
                            GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                            GameObject.FindObjectsOfType<CivilianAI>()[b].GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                        }
                    }
                }
                else if (collision.collider.transform.root.tag == "Bandit")
                {
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                    {
                        if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Bandit")
                        {
                            if (GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack == null)
                                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                        }
                    }
                }
                else if (collision.collider.transform.root.tag == "Royalist")
                {
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                    {
                        if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Royalist")
                        {
                            if (GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack == null)
                                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                        }
                    }
                }
                else if (collision.collider.transform.root.tag == "Republican")
                {
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                    {
                        if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Republican")
                        {
                            if (GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack == null)
                                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                        }
                    }
                }
                else if (collision.collider.transform.root.tag == "Undead")
                {
                    for (int b = 0; b < GameObject.FindObjectsOfType<GuardAI>().Length; b++)
                    {
                        if ((GameObject.FindObjectsOfType<GuardAI>()[b].transform.position - transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[b].tag == "Undead")
                        {
                            if (GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack == null)
                                GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().isAlerted = true;
                            GameObject.FindObjectsOfType<GuardAI>()[b].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                        }
                    }
                }
            }
            }
        public bool checkShieldPlayer(Collision collision)
    {
        Ray forwardRay = new Ray(collision.collider.transform.position, collision.collider.transform.forward);
        Ray shooterRay = new Ray(collision.collider.transform.position, shooter.transform.position-collision.collider.transform.position);
        if (Vector3.Angle(forwardRay.direction, shooterRay.direction) < 60)
            return true;
        else
            return false;
    }
}


