using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CitizenAIPatrol : MonoBehaviour
{
    public int indexPoint=0;
    private int delta = 1;
    public GameObject[] walkingPoints;
    public bool cameDestination = false;
    // Start is called before the first frame update
    void Start()
    {
            GetComponent<Animator>().Play("Idle");
        StartCoroutine("PatrolAI");
    }

    // Update is called once per frame
    void Update()
    {
        //Call Patrol Coroutine when Object came to Destination
        if(GetComponent<NavMeshAgent>().enabled)
            if(gameObject.GetComponent<NavMeshAgent>().destination!=null)
        if (gameObject.GetComponent<NavMeshAgent>().remainingDistance >=0.01f&& cameDestination&&gameObject.GetComponent<NavMeshAgent>().remainingDistance <= 0.2f)
        {
            StartCoroutine("PatrolAI");
            cameDestination = false;
            gameObject.GetComponent<Animator>().SetBool("IsWalking", false);
            gameObject.GetComponent<Animator>().Play("Idle");
            gameObject.GetComponent<AudioSource>().clip = null;
            gameObject.GetComponent<AudioSource>().loop = false;
        }
        //If player interrupt AI in third time start special dialogue
        if(GetComponent<CivilianAI>()!=null)
        if (GetComponent<CivilianAI>().gameManager.player.collisionCount >= 3)
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().CollisionDialog(gameObject);
            GetComponent<CivilianAI>().gameManager.player.collisionCount = 0;
            StopCoroutine("NullCollisionCount");
        }
    }
  IEnumerator PatrolAI() {
        //Start patrolling to point after 3 seconds since stopped
        int time = 3;
        if (name == "Hunter")
            time = 30;
            yield return new WaitForSeconds(time);
        gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<NavMeshAgent>().SetDestination(walkingPoints[indexPoint].transform.position);
       if (indexPoint == 0)
                        delta = 1;
                    else if (indexPoint == walkingPoints.Length - 1)
                        delta = -1;
                    else if (walkingPoints.Length == 1)
                        delta = 0;
        indexPoint += delta;
            cameDestination = true;
        gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
        gameObject.GetComponent<Animator>().Play("Walk");
    }
}
