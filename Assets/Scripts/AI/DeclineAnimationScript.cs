using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeclineAnimationScript : MonoBehaviour
{
    public bool canDissapear;
    private Transform player;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        //Destroy object if it is dead and player is farrer than 30 points
        if ((player.position - transform.position).magnitude > 30 && canDissapear)
            Destroy(this.gameObject);
    }
    //Code for stun aniimations disable scripts
    public void StunAnimation()
    {
        gameObject.GetComponent<Animator>().Play("Stun");
        gameObject.GetComponent<Animator>().SetBool("IsStunned", true);
        if (gameObject.tag == "VillageGuard"||gameObject.tag=="Summoned"||gameObject.tag=="Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
            gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
        gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
        if (gameObject.tag == "VillageGuard"||gameObject.tag=="Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
        {
            if(GetComponent<GuardAI>()!=null)
            gameObject.GetComponent<GuardAI>().enabled = false;
            if(GetComponent<SummonedAI>()!=null)
                gameObject.GetComponent<SummonedAI>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (gameObject.tag == "Summoned")
        {
            gameObject.GetComponent<SummonedAI>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (gameObject.tag == "SimplePeople" || gameObject.tag == "Civilian")
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<CivilianAI>().enabled = false;
        }
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponentInChildren<CameraMovement>().enabled = false;
        }
    }
    //Enable scripts after stun ended
    public void DeclineStunAfterAnimationEnd()
    {
        if (gameObject.tag == "VillageGuard" || gameObject.tag == "Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
        {
            if (gameObject.name != "Solovey")
            {
                if (GetComponent<GuardAI>() != null)
                    gameObject.GetComponent<GuardAI>().enabled = true;
                if (GetComponent<SummonedAI>() != null)
                    gameObject.GetComponent<SummonedAI>().enabled = true;
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            }
        }
        if (gameObject.tag == "SimplePeople" || gameObject.tag == "Civilian"||gameObject.name=="Solovey")
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            gameObject.GetComponent<CivilianAI>().enabled = true;
        }
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<PlayerController>().enabled = true;
            gameObject.GetComponentInChildren<CameraMovement>().enabled = true;
        }
        if (gameObject.tag == "Summoned")
            gameObject.GetComponent<SummonedAI>().enabled = true;
        gameObject.GetComponent<Animator>().SetBool("IsStunned", false);
    }
}
