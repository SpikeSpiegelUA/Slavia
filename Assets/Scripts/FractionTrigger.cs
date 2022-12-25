using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FractionTrigger : MonoBehaviour
{
    public List<GameObject> objectsInRadius;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(15, gameObject.layer, true);
        Physics.IgnoreLayerCollision(0, gameObject.layer, true);

    }
 private void Start()
    {
        objectsInRadius = new List<GameObject>();
    }
    private void Update()
    {
        //Fraction manager.Check if enemies are in radius for each fraction
        foreach (GameObject item in objectsInRadius.ToArray())
        {
            if (item != null)
            {
                PlayerController playerController = null;
                CivilianAI civilianAI = null;
                GuardAI guardAI = null;
                SummonedAI summonedAI = null;
                if (item.GetComponent<GuardAI>() != null)
                {
                    guardAI = item.GetComponent<GuardAI>();
                    if (guardAI.currentHP <= 0)
                        objectsInRadius.Remove(item);
                }
                if (item.GetComponent<CivilianAI>() != null)
                {
                    civilianAI = item.GetComponent<CivilianAI>();
                    if (civilianAI.currentHP <= 0)
                        objectsInRadius.Remove(item);
                }
                if (item.GetComponent<SummonedAI>() != null)
                {
                    summonedAI = item.GetComponent<SummonedAI>();
                    if (summonedAI.currentHP <= 0)
                        objectsInRadius.Remove(item);
                }
                if (item.GetComponent<PlayerController>() != null)
                {
                    playerController = item.GetComponent<PlayerController>();
                    if (playerController.currentHealth <= 0)
                        objectsInRadius.Remove(item);
                }
                //Village fraction code
                if (transform.root.tag == "VillageGuard" || transform.root.tag == "SimplePeople" || transform.root.tag == "Civilian")
                {
                    //Guard
                    if (transform.root.GetComponent<GuardAI>() != null)
                    {
                        GuardAI ownGuard = transform.root.GetComponent<GuardAI>();
                        if (item.tag == "Summoned" || item.tag == "Player")
                        {
                            if (ownGuard.gameManager.villageAttackedByPlayer && (item.transform.position - gameObject.transform.position).magnitude <= 10 && (ownGuard.detection >= 100 || ownGuard.gameManager.player.isDetected))
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (playerController != null)
                                    if (playerController.currentHealth > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (playerController != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        ownGuard.detection = 100;
                                    }
                                }
                            }
                            if ((ownGuard.gameManager.player.transform.position - gameObject.transform.position).magnitude <= 10 && ownGuard.gameManager.player.isCrouched == false && ownGuard.gameManager.villageAttackedByPlayer)
                            {
                                if (ownGuard.currentHP > 0)
                                {
                                    ownGuard.detection = 100;
                                    ownGuard.gameManager.player.isDetected = true;
                                }
                            }
                            //Plus to combat enemies
                            if (ownGuard.plusToCount == false && ownGuard.isAlerted && (ownGuard.objectToAttack == ownGuard.gameManager.player.summonedMelee || ownGuard.objectToAttack == ownGuard.gameManager.player.gameObject || ownGuard.objectToAttack == ownGuard.gameManager.player.summonedArcher))
                            {
                                ownGuard.gameManager.player.combatEnemies++;
                                ownGuard.plusToCount = true;
                            }
                        }
                        if (item.tag == "Bandit" || item.tag == "Undead")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10)
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (guardAI != null)
                                    if (guardAI.currentHP > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (guardAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                    }
                                }
                            }
                        }
                    }
                    //Civilian
                    if (transform.root.GetComponent<CivilianAI>() != null)
                    {
                        CivilianAI ownCivilian = transform.root.GetComponent<CivilianAI>();
                        if (item.tag == "Summoned" || item.tag == "Player")
                        {
                            //If player fraction is in radius of 10 and village was attacked
                            if (ownCivilian.gameManager.villageAttackedByPlayer && (item.transform.position - transform.position).magnitude <= 10 && (ownCivilian.detection >= 100 || ownCivilian.gameManager.player.isDetected))
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (playerController != null)
                                    if (playerController.currentHealth > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownCivilian.attacker != null)
                                    {
                                        PlayerController attackerPlayerController = null;
                                        CivilianAI attackerCivilian = null;
                                        GuardAI attackerGuard = null;
                                        SummonedAI attackerSummoned = null;
                                        bool objectIsAlive = false;
                                        if (ownCivilian.attacker.GetComponent<GuardAI>() != null)
                                        {
                                            attackerGuard = ownCivilian.attacker.GetComponent<GuardAI>();
                                            if (attackerGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        }
                                        if (ownCivilian.attacker.GetComponent<CivilianAI>() != null)
                                        {
                                            attackerCivilian = ownCivilian.attacker.GetComponent<CivilianAI>();
                                            if (attackerCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        }
                                        if (ownCivilian.attacker.GetComponent<SummonedAI>() != null)
                                        {
                                            attackerSummoned = ownCivilian.attacker.GetComponent<SummonedAI>();
                                            if (attackerSummoned.currentHP > 0)
                                                objectIsAlive = true;
                                        }
                                        if (ownCivilian.attacker.GetComponent<PlayerController>() != null)
                                        {
                                            attackerPlayerController = ownCivilian.attacker.GetComponent<PlayerController>();
                                            if (attackerPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        }
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownCivilian.attacker.transform.position - transform.position).magnitude && (ownCivilian.attacker.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownCivilian.hasBeenAttacked = true;
                                                    ownCivilian.attacker = item;
                                                }
                                            if (playerController != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownCivilian.attacker.transform.position - transform.position).magnitude && (ownCivilian.attacker.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownCivilian.hasBeenAttacked = true;
                                                    ownCivilian.attacker = item;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        ownCivilian.hasBeenAttacked = true;
                                        ownCivilian.attacker = item.gameObject;
                                    }
                                    if (!ownCivilian.hasBeenAttacked)
                                        ownCivilian.CallNear();
                                    ownCivilian.hasBeenAttacked = true;
                                    Animator animator = transform.root.GetComponent<Animator>();
                                    if (!animator.GetBool("IsStunned"))
                                    {
                                        animator.Play("Run");
                                        animator.SetBool("IsRunning", true);
                                    }
                                    ownCivilian.StartCoroutine("RunRegimeCancel");
                                    ownCivilian.FindGuard();
                                    if (ownCivilian.plusToCount == false && ownCivilian.attacker == ownCivilian.gameManager.player.gameObject)
                                    {
                                        ownCivilian.gameManager.player.combatEnemies++;
                                        ownCivilian.plusToCount = true;
                                    }
                                }
                            }
                            //Set detection 100 if player isn't crouching and is in radius
                            if (ownCivilian.gameManager.player.isCrouched == false && ownCivilian.gameManager.villageAttackedByPlayer && item.tag == "Player")
                            {
                                if (ownCivilian.currentHP > 0)
                                {
                                    ownCivilian.detection = 100;
                                    ownCivilian.gameManager.player.isDetected = true;
                                    //Plus to combat enemies
                                    if (ownCivilian.plusToCount == false && (ownCivilian.attacker == ownCivilian.gameManager.player.gameObject || ownCivilian.attacker == ownCivilian.gameManager.player.summonedMelee || ownCivilian.attacker == ownCivilian.gameManager.player.summonedArcher))
                                    {
                                        ownCivilian.gameManager.player.combatEnemies++;
                                        ownCivilian.plusToCount = true;
                                    }
                                }
                            }
                        }
                        if (item.tag == "Bandit" || item.tag == "Undead")
                        {
                            //If player fraction is in radius of 10 and village was attacked
                            if ((item.transform.position - transform.position).magnitude <= 10)
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (guardAI != null)
                                    if (guardAI.currentHP > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownCivilian.attacker != null)
                                    {
                                        PlayerController attackerPlayerController = null;
                                        CivilianAI attackerCivilian = null;
                                        GuardAI attackerGuard = null;
                                        SummonedAI attackerSummoned = null;
                                        bool objectIsAlive = false;
                                        if (ownCivilian.attacker.GetComponent<GuardAI>() != null)
                                        {
                                            attackerGuard = ownCivilian.attacker.GetComponent<GuardAI>();
                                            if (attackerGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        }
                                        if (ownCivilian.attacker.GetComponent<CivilianAI>() != null)
                                        {
                                            attackerCivilian = ownCivilian.attacker.GetComponent<CivilianAI>();
                                            if (attackerCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        }
                                        if (ownCivilian.attacker.GetComponent<SummonedAI>() != null)
                                        {
                                            attackerSummoned = ownCivilian.attacker.GetComponent<SummonedAI>();
                                            if (attackerSummoned.currentHP > 0)
                                                objectIsAlive = true;
                                        }
                                        if (ownCivilian.attacker.GetComponent<PlayerController>() != null)
                                        {
                                            attackerPlayerController = ownCivilian.attacker.GetComponent<PlayerController>();
                                            if (attackerPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        }
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownCivilian.attacker.transform.position - transform.position).magnitude && (ownCivilian.attacker.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownCivilian.hasBeenAttacked = true;
                                                    ownCivilian.attacker = item;
                                                }
                                            if (guardAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownCivilian.attacker.transform.position - transform.position).magnitude && (ownCivilian.attacker.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownCivilian.hasBeenAttacked = true;
                                                    ownCivilian.attacker = item;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        ownCivilian.hasBeenAttacked = true;
                                        ownCivilian.attacker = item.gameObject;
                                    }
                                    if (!ownCivilian.hasBeenAttacked)
                                        ownCivilian.CallNear();
                                    ownCivilian.hasBeenAttacked = true;
                                    Animator animator = transform.root.GetComponent<Animator>();
                                    animator.Play("Run");
                                    animator.SetBool("IsRunning", true);
                                    ownCivilian.StartCoroutine("RunRegimeCancel");
                                    ownCivilian.FindGuard();
                                }
                            }
                        }
                    }
                }
                //Bandit fraction code
                if (transform.root.tag == "Bandit" && transform.root.name != "Solovey")
                {
                    //Guard
                    if (transform.root.GetComponent<GuardAI>() != null)
                    {
                        GuardAI ownGuard = transform.root.GetComponent<GuardAI>();
                        if (item.tag == "Summoned" || item.tag == "Player")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10 && (ownGuard.detection >= 100 || ownGuard.gameManager.player.isDetected))
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (playerController != null)
                                    if (playerController.currentHealth > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (playerController != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                        ownGuard.detection = 100;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                    }
                                }
                            }
                            if ((ownGuard.gameManager.player.transform.position - gameObject.transform.position).magnitude <= 10 && ownGuard.gameManager.player.isCrouched == false)
                            {
                                if (ownGuard.currentHP > 0)
                                {
                                    ownGuard.detection = 100;
                                    ownGuard.gameManager.player.isDetected = true;
                                }
                            }
                            //Plus to combat enemies
                            if (ownGuard.plusToCount == false && ownGuard.isAlerted && (ownGuard.objectToAttack == ownGuard.gameManager.player.summonedMelee || ownGuard.objectToAttack == ownGuard.gameManager.player.gameObject || ownGuard.objectToAttack == ownGuard.gameManager.player.summonedArcher))
                            {
                                ownGuard.gameManager.player.combatEnemies++;
                                ownGuard.plusToCount = true;
                            }
                        }
                        if (item.tag == "VillageGuard" || item.tag == "SimplePeople" || item.tag == "Civilian" || item.tag == "Undead" || item.tag == "Republican" || item.tag == "Royalist")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10)
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (civilianAI != null)
                                    if (civilianAI.currentHP > 0)
                                        isAlive = true;
                                if (guardAI != null)
                                    if (guardAI.currentHP > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (guardAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (civilianAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                    }
                                }
                            }
                        }
                    }
                }
                //Republican fraction code
                if (transform.root.tag == "Republican")
                {
                    //Guard
                    if (transform.root.GetComponent<GuardAI>() != null)
                    {
                        GuardAI ownGuard = transform.root.GetComponent<GuardAI>();
                        if (item.tag == "Summoned" || item.tag == "Player")
                        {
                            if (ownGuard.gameManager.republicanAttackedByPlayer && (item.transform.position - gameObject.transform.position).magnitude <= 10 && (ownGuard.detection >= 100 || ownGuard.gameManager.player.isDetected))
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (playerController != null)
                                    if (playerController.currentHealth > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (playerController != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                        ownGuard.detection = 100;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                    }
                                }
                            }
                            if ((ownGuard.gameManager.player.transform.position - gameObject.transform.position).magnitude <= 10 && ownGuard.gameManager.player.isCrouched == false && ownGuard.gameManager.republicanAttackedByPlayer)
                            {
                                if (ownGuard.currentHP > 0)
                                {
                                    ownGuard.detection = 100;
                                    ownGuard.gameManager.player.isDetected = true;
                                }
                            }
                            //Plus to combat enemies
                            if (ownGuard.plusToCount == false && ownGuard.isAlerted && (ownGuard.objectToAttack == ownGuard.gameManager.player.summonedMelee || ownGuard.objectToAttack == ownGuard.gameManager.player.gameObject || ownGuard.objectToAttack == ownGuard.gameManager.player.summonedArcher))
                            {
                                ownGuard.gameManager.player.combatEnemies++;
                                ownGuard.plusToCount = true;
                            }
                        }
                        if (item.tag == "Undead" || item.tag == "Bandit" || item.tag == "Royalist")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10)
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (civilianAI != null)
                                    if (civilianAI.currentHP > 0)
                                        isAlive = true;
                                if (guardAI != null)
                                    if (guardAI.currentHP > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (guardAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (civilianAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                    }
                                }
                            }
                        }
                    }
                }
                //Royalist fraction code
                if (transform.root.tag == "Royalist")
                {
                    //Guard
                    if (transform.root.GetComponent<GuardAI>() != null)
                    {
                        GuardAI ownGuard = transform.root.GetComponent<GuardAI>();
                        if (item.tag == "Summoned" || item.tag == "Player")
                        {
                            if (ownGuard.gameManager.royalistAttackedByPlayer && (item.transform.position - gameObject.transform.position).magnitude <= 10 && (ownGuard.detection >= 100 || ownGuard.gameManager.player.isDetected))
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (playerController != null)
                                    if (playerController.currentHealth > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (playerController != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                        ownGuard.detection = 100;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                    }
                                }
                            }
                            if ((ownGuard.gameManager.player.transform.position - gameObject.transform.position).magnitude <= 10 && ownGuard.gameManager.player.isCrouched == false && ownGuard.gameManager.royalistAttackedByPlayer)
                            {
                                if (ownGuard.currentHP > 0)
                                {
                                    ownGuard.detection = 100;
                                    ownGuard.gameManager.player.isDetected = true;
                                }
                            }
                            //Plus to combat enemies
                            if (ownGuard.plusToCount == false && ownGuard.isAlerted && (ownGuard.objectToAttack == ownGuard.gameManager.player.summonedMelee || ownGuard.objectToAttack == ownGuard.gameManager.player.gameObject || ownGuard.objectToAttack == ownGuard.gameManager.player.summonedArcher))
                            {
                                ownGuard.gameManager.player.combatEnemies++;
                                ownGuard.plusToCount = true;
                            }
                        }
                        if (item.tag == "Undead" || item.tag == "Bandit" || item.tag == "Republican")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10)
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (civilianAI != null)
                                    if (civilianAI.currentHP > 0)
                                        isAlive = true;
                                if (guardAI != null)
                                    if (guardAI.currentHP > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (guardAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (civilianAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                    }
                                }
                            }
                        }
                    }
                }
                //Royalist fraction code
                if (transform.root.tag == "Undead")
                {
                    //Guard
                    if (transform.root.GetComponent<GuardAI>() != null)
                    {
                        GuardAI ownGuard = transform.root.GetComponent<GuardAI>();
                        if (item.tag == "Summoned" || item.tag == "Player")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10 && (ownGuard.detection >= 100 || ownGuard.gameManager.player.isDetected))
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (playerController != null)
                                    if (playerController.currentHealth > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (playerController != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3.5f)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                        ownGuard.detection = 100;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                    }
                                }
                            }
                            if ((ownGuard.gameManager.player.transform.position - gameObject.transform.position).magnitude <= 10 && ownGuard.gameManager.player.isCrouched == false)
                            {
                                if (ownGuard.currentHP > 0)
                                {
                                    ownGuard.detection = 100;
                                    ownGuard.gameManager.player.isDetected = true;
                                }
                            }
                            //Plus to combat enemies
                            if (ownGuard.plusToCount == false && ownGuard.isAlerted && (ownGuard.objectToAttack == ownGuard.gameManager.player.summonedMelee || ownGuard.objectToAttack == ownGuard.gameManager.player.gameObject || ownGuard.objectToAttack == ownGuard.gameManager.player.summonedArcher))
                            {
                                ownGuard.gameManager.player.combatEnemies++;
                                ownGuard.plusToCount = true;
                            }
                        }
                        if (item.tag == "Bandit" || item.tag == "Republican" || item.tag == "Royalist" || item.tag == "VillageGuard" || item.tag == "Civilian" || item.tag == "SimplePeople")
                        {
                            if ((item.transform.position - gameObject.transform.position).magnitude <= 10)
                            {
                                bool isAlive = false;
                                if (summonedAI != null)
                                    if (summonedAI.currentHP > 0)
                                        isAlive = true;
                                if (civilianAI != null)
                                    if (civilianAI.currentHP > 0)
                                        isAlive = true;
                                if (guardAI != null)
                                    if (guardAI.currentHP > 0)
                                        isAlive = true;
                                if (isAlive)
                                {
                                    if (ownGuard.objectToAttack != null)
                                    {
                                        GuardAI objectToAttackGuard = null;
                                        CivilianAI objectToAttackCivilian = null;
                                        PlayerController objectToAttackPlayerController = null;
                                        SummonedAI objectToAttackSummonedAI = null;
                                        if (ownGuard.objectToAttack.GetComponent<GuardAI>() != null)
                                            objectToAttackGuard = ownGuard.objectToAttack.GetComponent<GuardAI>();
                                        if (ownGuard.objectToAttack.GetComponent<CivilianAI>() != null)
                                            objectToAttackCivilian = ownGuard.objectToAttack.GetComponent<CivilianAI>();
                                        if (ownGuard.objectToAttack.GetComponent<PlayerController>() != null)
                                            objectToAttackPlayerController = ownGuard.objectToAttack.GetComponent<PlayerController>();
                                        if (ownGuard.objectToAttack.GetComponent<SummonedAI>() != null)
                                            objectToAttackSummonedAI = ownGuard.objectToAttack.GetComponent<SummonedAI>();
                                        bool objectIsAlive = false;
                                        if (objectToAttackGuard != null)
                                            if (objectToAttackGuard.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackCivilian != null)
                                            if (objectToAttackCivilian.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackSummonedAI != null)
                                            if (objectToAttackSummonedAI.currentHP > 0)
                                                objectIsAlive = true;
                                        if (objectToAttackPlayerController != null)
                                            if (objectToAttackPlayerController.currentHealth > 0)
                                                objectIsAlive = true;
                                        if (objectIsAlive)
                                        {
                                            if (summonedAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (guardAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                            if (civilianAI != null)
                                                if ((item.transform.position - transform.position).magnitude < (ownGuard.objectToAttack.transform.position - transform.position).magnitude && (ownGuard.objectToAttack.transform.position - transform.position).magnitude >= 3)
                                                {
                                                    ownGuard.isAlerted = true;
                                                    ownGuard.objectToAttack = item;
                                                    GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                                }
                                        }
                                        else
                                        {
                                            ownGuard.isAlerted = true;
                                            ownGuard.objectToAttack = item;
                                            GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        ownGuard.objectToAttack = item;
                                        GetComponentInParent<NavMeshAgent>().SetDestination(ownGuard.objectToAttack.transform.position);
                                        if (ownGuard.isAlerted == false)
                                            ownGuard.CallNear();
                                        ownGuard.isAlerted = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                objectsInRadius.Remove(item);
        }
    }
        private void OnTriggerEnter(Collider collision)
        {
            //Trigger for everyone except of village citizen and guard
            if (transform.root.tag != "SimplePeople" && transform.root.tag != "Civilian" && transform.root.tag != "VillageGuard")
            {
                if ((collision.transform.root.tag == "SimplePeople" || collision.transform.root.tag == "Civilian" || collision.transform.root.tag == "VillageGuard" || collision.transform.root.tag == "Summoned" || collision.transform.root.tag == "Player" || collision.transform.root.tag == "Bandit" || collision.transform.root.tag == "Undead" || collision.transform.root.tag == "Royalist" || collision.transform.root.tag == "Republican") && collision.transform.root.tag != transform.root.tag)
                {
                    bool isAlive = false;
                    if (collision.GetComponentInParent<GuardAI>() != null)
                        if (collision.GetComponentInParent<GuardAI>().currentHP > 0)
                            isAlive = true;
                    if (collision.GetComponentInParent<CivilianAI>() != null)
                        if (collision.GetComponentInParent<CivilianAI>().currentHP > 0)
                            isAlive = true;
                    if (collision.GetComponentInParent<SummonedAI>() != null)
                        if (collision.GetComponentInParent<SummonedAI>().currentHP > 0)
                            isAlive = true;
                    if (collision.GetComponentInParent<PlayerController>() != null)
                        if (collision.GetComponentInParent<PlayerController>().currentHealth > 0)
                            isAlive = true;
                    if (isAlive)
                    {
                        bool haveInList = false;
                        foreach (GameObject item in objectsInRadius)
                            if (item.gameObject == collision.transform.root.gameObject)
                                haveInList = true;
                        if (!haveInList)
                            objectsInRadius.Add(collision.transform.root.gameObject);
                    }
                }
            }
            //Trigger for village citizen and guard
            else if (transform.root.tag == "SimplePeople" || transform.root.tag == "Civilian" || transform.root.tag == "VillageGuard")
            {
                if ((collision.transform.root.tag == "SimplePeople" || collision.transform.root.tag == "Civilian" || collision.transform.root.tag == "VillageGuard" || collision.transform.root.tag == "Summoned" || collision.transform.root.tag == "Player" || collision.transform.root.tag == "Bandit" || collision.transform.root.tag == "Royalist" || collision.transform.root.tag == "Republican" || collision.transform.root.tag == "Undead") && collision.transform.root.tag != transform.root.tag && collision.transform.root.tag != "SimplePeople" && collision.transform.root.tag != "VillageGuard" && collision.transform.root.tag != "Civilian")
                {
                    bool isAlive = false;
                    if (collision.GetComponentInParent<GuardAI>() != null)
                        if (collision.GetComponentInParent<GuardAI>().currentHP > 0)
                            isAlive = true;
                    if (collision.GetComponentInParent<CivilianAI>() != null)
                        if (collision.GetComponentInParent<CivilianAI>().currentHP > 0)
                            isAlive = true;
                    if (collision.GetComponentInParent<SummonedAI>() != null)
                        if (collision.GetComponentInParent<SummonedAI>().currentHP > 0)
                            isAlive = true;
                    if (collision.GetComponentInParent<PlayerController>() != null)
                        if (collision.GetComponentInParent<PlayerController>().currentHealth > 0)
                            isAlive = true;
                    if (isAlive)
                    {
                        bool haveInList = false;
                        foreach (GameObject item in objectsInRadius)
                            if (item.gameObject == collision.transform.root.gameObject)
                                haveInList = true;
                        if (!haveInList)
                            objectsInRadius.Add(collision.transform.root.gameObject);
                    }
                }
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            //Delete object from list when it exit trigger
            bool haveInList = false;
            foreach (GameObject item in objectsInRadius)
                if (item.gameObject == collision.transform.root.gameObject)
                    haveInList = true;
            if (haveInList)
                objectsInRadius.Remove(collision.transform.root.gameObject);
        }
}
