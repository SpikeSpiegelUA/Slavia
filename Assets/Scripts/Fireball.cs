using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour
{
    private Vector3 movement;
    public GameObject shooter;
    public int numberOfEnters = 0;
    public int spellDamage;
    void Update()
    {
        //Move spell
        transform.Translate(movement, Space.World);
    }
    void Start()
    {
        //Set movement direction
        if (shooter == GameObject.Find("Player"))
            movement = GameObject.Find("Main Camera").transform.forward * GameObject.Find("GameManager").GetComponent<GameManager>().stableSpellSpeed;
        else
            if(shooter.GetComponent<GuardAI>().objectToAttack!=null)
            movement = (shooter.GetComponent<GuardAI>().objectToAttack.transform.position-shooter.transform.position).normalized * GameObject.Find("GameManager").GetComponent<GameManager>().stableSpellSpeed;
        //Set spell rotation
        if (shooter == GameObject.Find("Player"))
            transform.eulerAngles = new Vector3(0, GameObject.Find("Player").transform.eulerAngles.y - 90, -GameObject.Find("Main Camera").transform.localEulerAngles.x);
        else
            transform.eulerAngles = new Vector3(0, shooter.transform.eulerAngles.y - 90, Quaternion.LookRotation(movement).z);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer!=17&&collision.gameObject.layer!=shooter.layer)
        {
            if (numberOfEnters == 0 && collision.gameObject != shooter)
            {
                numberOfEnters++;
                Destroy(gameObject);
                //Damage for civillians
                if ((collision.gameObject.transform.root.tag == "Civilian" || collision.transform.root.tag == "SimplePeople"||collision.transform.root.name=="Solovey"))
                {
                    if (collision.gameObject.transform.root.GetComponent<CivilianAI>().currentHP > 0)
                    {
                        if (shooter == GameObject.Find("Player"))
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value++;
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<CivilianAI>().currentHP -= spellDamage;
                            else if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<CivilianAI>().currentHP -= spellDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                                shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                            }
                        }
                        else
                        {
                            if(collision.transform.root.tag!=shooter.tag)
                            collision.GetComponent<Collider>().gameObject.GetComponentInParent<CivilianAI>().currentHP -= spellDamage;
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0 && shooter.GetComponent<SummonedAI>() != null)
                            {
                                if (shooter.GetComponent<SummonedAI>().summoner.GetComponent<PlayerController>() != null)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                                    shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                    GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<CivilianAI>().experience;
                                }
                            }
                            if(collision.gameObject.GetComponentInParent<CivilianAI>().currentHP <= 0)
                            if (shooter.GetComponent<GuardAI>() != null)
                                shooter.GetComponent<GuardAI>().objectToAttack = null;
                        }
                        if (shooter.name == "Player"&&collision.transform.root.name!="Solovey")
                            GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                        if (shooter.name == "Player")
                            if (collision.gameObject.GetComponentInParent<CivilianAI>().currentHP > 0&&collision.transform.root.name!="Solovey")
                            {
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<CivilianAI>().detection = 100;
                                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<CivilianAI>().hasBeenAttacked = true;
                                if (collision.gameObject.GetComponentInParent<CivilianAI>().attacker == null)
                                    collision.GetComponent<Collider>().gameObject.GetComponentInParent<CivilianAI>().attacker = shooter;
                                collision.gameObject.GetComponentInParent<CivilianAI>().StartCoroutine("RunRegimeCancel");
                            }
                        CallNear(collision);
                    }
                }
                if (collision.GetComponentInParent<AnimalAI>()!=null)
                {
                    if (collision.gameObject.transform.root.GetComponent<AnimalAI>().currentHP > 0)
                    {
                        if (shooter == GameObject.Find("Player"))
                        {
                            GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value++;
                            if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<AnimalAI>().currentHP -= spellDamage;
                            else if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<AnimalAI>().currentHP -= spellDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                            if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP <= 0)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                                shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                            }
                        }
                        else
                        {
                            if (collision.transform.root.tag != shooter.tag)
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<AnimalAI>().currentHP -= spellDamage;
                            if (collision.gameObject.GetComponentInParent<AnimalAI>().currentHP <= 0 && shooter.GetComponent<SummonedAI>() != null)
                            {
                                if (shooter.GetComponent<SummonedAI>().summoner.GetComponent<PlayerController>() != null)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                                    shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                    GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<AnimalAI>().experience;
                                }
                            }
                        }
                    }
                }
                //Damage for guard
                if ((collision.gameObject.transform.root.tag == "VillageGuard" && shooter.transform.root.tag != "VillageGuard")||(collision.gameObject.transform.root.tag == "Bandit" && shooter.transform.root.tag != "Bandit") || (collision.gameObject.transform.root.tag == "Royalist" && shooter.transform.root.tag != "Royalist") || (collision.gameObject.transform.root.tag == "Republican" && shooter.transform.root.tag != "Republican") || (collision.gameObject.transform.root.tag == "Undead" && shooter.transform.root.tag != "Undead"))
                {
                    if (collision.GetComponentInParent<GuardAI>() != null)
                    {
                        if (collision.gameObject.transform.root.GetComponent<GuardAI>().currentHP > 0)
                        {
                            if (shooter == GameObject.Find("Player"))
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value++;
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                {
                                    if (spellDamage - collision.GetComponentInParent<GuardAI>().armor / 100f * spellDamage > 0)
                                        collision.GetComponentInParent<GuardAI>().currentHP -= (int)(spellDamage - collision.GetComponentInParent<GuardAI>().armor / 100f * spellDamage);
                                }
                                else if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                {
                                    if (spellDamage - collision.GetComponentInParent<GuardAI>().armor / 100f * spellDamage > 0)
                                        collision.GetComponentInParent<GuardAI>().currentHP -= (int)(spellDamage - collision.GetComponentInParent<GuardAI>().armor / 100f * spellDamage) * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                                }
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                                {
                                    GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                    shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                    GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                }
                            }
                            else
                            {
                                if (spellDamage - collision.GetComponentInParent<GuardAI>().armor / 100f * spellDamage > 0)
                                    if (collision.transform.root.tag != shooter.tag)
                                        collision.GetComponentInParent<GuardAI>().currentHP -= (int)(spellDamage - collision.GetComponentInParent<GuardAI>().armor / 100f * spellDamage);
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0 && shooter.GetComponent<SummonedAI>() != null)
                                {
                                    if (shooter.GetComponent<SummonedAI>().summoner.GetComponent<PlayerController>() != null)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().killExperience.GetComponentInChildren<Text>().text = "Experience:" + collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                        shooter.GetComponent<PlayerController>().StartCoroutine("KillExperienceShow");
                                        GameObject.Find("Player").GetComponent<PlayerController>().experience += collision.gameObject.GetComponentInParent<GuardAI>().experience;
                                    }
                                }
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP <= 0)
                                    if (shooter.GetComponent<GuardAI>() != null)
                                        shooter.GetComponent<GuardAI>().objectToAttack = null;
                            }

                            if (shooter.name == "Player")
                                if (collision.gameObject.GetComponentInParent<GuardAI>().currentHP > 0)
                                {
                                    collision.GetComponent<Collider>().gameObject.GetComponentInParent<GuardAI>().detection = 100;
                                    GameObject.Find("Player").GetComponentInParent<PlayerController>().isDetected = true;
                                    if (collision.gameObject.GetComponentInParent<GuardAI>().objectToAttack == null)
                                        collision.gameObject.GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                    collision.gameObject.GetComponentInParent<GuardAI>().isAlerted = true;
                                    collision.gameObject.GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                }
                            if (collision.transform.root.name != "Solovey")
                            {
                                if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "VillageGuard")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "Republican")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                if (collision.transform.root.name != "PatrolRoyalist2" && collision.transform.root.name != "PatrolRoyalist1" && collision.transform.root.name != "StrangeRoyalist")
                                    if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "Royalist")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                if (collision.transform.root.name == "StrangeRoyalist")
                                {
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                            GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                        GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer = true;
                                }
                            }
                            if (collision.transform.root.name != "PatrolRoyalist2" && collision.transform.root.name != "PatrolRoyalist1" && collision.transform.root.name != "StrangeRoyalist")
                                CallNear(collision);
                            if (collision.transform.root.name == "StrangeRoyalist")
                            {
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") != null)
                                    if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest").GetComponent<QuestSlot>().questStage < 2)
                                        CallNear(collision);
                                if (GameObject.Find("QuestManager").GetComponent<QuestManager>().containerForList.transform.Find("HeadOfRoyalistsQuest") == null)
                                    CallNear(collision);
                            }
                        }
                    }
                    else if (collision.GetComponentInParent<SummonedAI>() != null)
                    {
                        if (collision.gameObject.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                        {
                            if (shooter == GameObject.Find("Player"))
                            {
                                GameObject.Find("SkillManager").GetComponent<SkillManager>().statsWindow.Find("BattleSpellsSlider").GetComponent<Slider>().value++;
                                if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                    collision.GetComponentInParent<SummonedAI>().currentHP -= spellDamage;
                                else if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                    collision.GetComponentInParent<SummonedAI>().currentHP -= spellDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                            }
                            else
                            {
                                if (collision.transform.root.tag != shooter.tag)
                                    collision.GetComponentInParent<SummonedAI>().currentHP -= spellDamage;
                            }
                            if (collision.GetComponentInParent<SummonedAI>().currentHP <= 0)
                                if (shooter.GetComponent<GuardAI>() != null)
                                    shooter.GetComponent<GuardAI>().objectToAttack = null;
                            if (shooter.name == "Player")
                                if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0)
                                {
                                    GameObject.Find("Player").GetComponentInParent<PlayerController>().isDetected = true;
                                    if (collision.gameObject.GetComponentInParent<SummonedAI>().objectToAttack == null)
                                        collision.gameObject.GetComponentInParent<SummonedAI>().objectToAttack = shooter;
                                    collision.gameObject.GetComponentInParent<SummonedAI>().isAlerted = true;
                                }
                            if (collision.transform.root.name != "Solovey")
                            {
                                if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "VillageGuard")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().villageAttackedByPlayer = true;
                                if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "Republican")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().republicanAttackedByPlayer = true;
                                if (shooter == GameObject.Find("Player") && collision.transform.root.tag == "Royalist")
                                    GameObject.Find("GameManager").GetComponent<GameManager>().royalistAttackedByPlayer= true;
                            }
                            CallNear(collision);
                        }
                    }
                }
                //Damage for summoned
                if (collision.gameObject.transform.root.tag == "Summoned" && shooter.transform.root.tag != "Summoned")
                {
                    if (collision.gameObject.transform.root.GetComponent<SummonedAI>().currentHP > 0)
                    {
                        if (shooter == GameObject.Find("Player"))
                        {
                            if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && (!GameObject.Find("Player").GetComponent<PlayerController>().isCrouched || GameObject.Find("Player").GetComponent<PlayerController>().isDetected))
                                collision.gameObject.GetComponentInParent<SummonedAI>().currentHP -= spellDamage;
                            else if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP > 0 && GameObject.Find("Player").GetComponent<PlayerController>().isCrouched && !GameObject.Find("Player").GetComponent<PlayerController>().isDetected)
                                collision.GetComponent<Collider>().gameObject.GetComponentInParent<SummonedAI>().currentHP -= spellDamage * GameObject.Find("Player").GetComponent<PlayerController>().stealthAttackModify;
                        }
                        else
                        {
                            if (collision.transform.root.tag != shooter.tag)
                                collision.gameObject.GetComponentInParent<SummonedAI>().currentHP -= spellDamage;
                        }
                        if (collision.gameObject.GetComponentInParent<SummonedAI>().currentHP <= 0)
                            if (shooter.GetComponent<GuardAI>() != null)
                                shooter.GetComponent<GuardAI>().objectToAttack = null;
                    }
                }
                if (collision.gameObject.tag == "Player")
                {
                    if (collision.gameObject.GetComponent<PlayerController>().currentHealth > 0)
                    {
                        if (spellDamage - collision.GetComponent<PlayerController>().armor / 100f * spellDamage > 0)
                            collision.GetComponent<PlayerController>().currentHealth -= (int)(spellDamage - collision.GetComponent<PlayerController>().armor / 100f * spellDamage);
                        collision.gameObject.GetComponent<PlayerController>().StartCoroutine("DamageScreenAppear");
                        if (collision.GetComponent<PlayerController>().currentHealth <= 0)
                            if (shooter.GetComponent<GuardAI>() != null)
                                shooter.GetComponent<GuardAI>().objectToAttack = null;
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
        }
    }
    private void CallNear(Collider collision)
    {
        //Call nearest person
        if (shooter == GameObject.Find("Player") && (collision.transform.root.tag== "SimplePeople" || collision.transform.root.tag== "Civilian" || collision.transform.root.tag == "VillageGuard"||collision.transform.root.tag=="Bandit" || collision.transform.root.tag == "Royalist" || collision.transform.root.tag == "Republican" || collision.transform.root.tag == "Undead"))
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<GuardAI>().Length; i++)
            {
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "VillageGuard")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.GetComponent<Collider>().tag == "VillageGuard")
                            if (collision.transform.root.GetComponent<GuardAI>().currentHP <= 0)
                            {
                                if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                    if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10&& GameObject.FindObjectsOfType<GuardAI>()[i].tag=="VillageGuard")
                                    {
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().detection = 100;
                                        GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
                                        if (GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack==null)
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().objectToAttack = shooter;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().isAlerted = true;
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().StartCoroutine("CancelAlert");
                                        GameObject.FindObjectsOfType<GuardAI>()[i].GetComponentInParent<GuardAI>().CallNear();
                                    }

                            }
                            else
                            {
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10&& GameObject.FindObjectsOfType<GuardAI>()[i].tag=="VillageGuard")
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
                    if (collision.GetComponent<Collider>().tag == "SimplePeople" || collision.GetComponent<Collider>().tag == "Civilian")
                    {
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
                        if (collision.transform.root.tag == "Bandit" && collision.transform.root.name != "Solovey")
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
                    if (collision.transform.root.name == "Solovey")
                        if (collision.GetComponentInParent<SummonedAI>().currentHP <= 0 || collision.GetComponentInParent<CivilianAI>().currentHP <= 0)
                        {
                            if (rayHit.collider.gameObject.transform.root == collision.transform.root && Vector3.Angle(ray.direction, personRay.direction) <= 75)
                                if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit" && collision.transform.root.tag == "Bandit")
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
                            if ((GameObject.FindObjectsOfType<GuardAI>()[i].transform.position - collision.transform.root.transform.position).magnitude <= 10 && GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Bandit" && collision.transform.root.tag == "Bandit")
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
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Republican")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.transform.root.tag == "Republican")
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
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Royalist")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.transform.root.tag == "Royalist")
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
                if (GameObject.FindObjectsOfType<GuardAI>()[i].tag == "Undead")
                {
                    Ray ray = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, collision.transform.root.transform.position - GameObject.FindObjectsOfType<GuardAI>()[i].transform.position);
                    Ray personRay = new Ray(GameObject.FindObjectsOfType<GuardAI>()[i].transform.position + GameObject.FindObjectsOfType<GuardAI>()[i].transform.up * 1.2f, GameObject.FindObjectsOfType<GuardAI>()[i].transform.forward);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                        if (collision.transform.root.tag == "Undead")
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
                    if (collision.GetComponent<Collider>().tag == "SimplePeople" || collision.GetComponent<Collider>().tag == "Civilian")
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
                if (collision.GetComponent<Collider>().tag == "VillageGuard")
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
        else if (shooter!=null)
            if (shooter.GetComponent<PlayerController>() == null)
            {
                if (collision.tag == "VillageGuard"||collision.tag=="SimplePeople"||collision.tag=="Civilian")
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
                else if (collision.tag == "Bandit")
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
                else if (collision.tag == "Republican")
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
                else if (collision.tag == "Royalist")
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
                else if (collision.tag == "Undead")
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
    //Destroy spell after 5 seconds
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
