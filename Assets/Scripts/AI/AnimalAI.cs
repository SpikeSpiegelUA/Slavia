using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AnimalAI : MonoBehaviour
{
    public string ID;
    public int hpCivilian = 10;
    public int currentHP;
    public GameManager gameManager;
    public bool foundWay = false;
    private NavMeshAgent navMeshAgent;
    private FractionTrigger fractionTrigger;
    public int experience;
    void Awake()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }
    void Start()
    {
        if (SaveLoad.isLoading)
        {
            LoadAnimalData();
            LoadLoot();
            LoadHP();
        }
        fractionTrigger = GetComponentInChildren<FractionTrigger>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!SaveLoad.isLoading)
            currentHP = hpCivilian;
        GetComponent<NavMeshAgent>().enabled = true;
        if(currentHP>0)
        GetComponent<Animator>().SetBool("IsRunning", true);
    }
    void Update()
    {
        //If AI is killed dissable script and play death animation
        if (currentHP <= 0 && !gameObject.GetComponent<Animator>().GetBool("IsDead"))
        {
            ChangeLayer(gameObject);
            gameObject.layer = 21;
            if (GetComponentInChildren<FractionTrigger>() != null)
                GetComponentInChildren<FractionTrigger>().enabled = false;
            currentHP = 0;
            gameObject.GetComponent<Animator>().Play("Dead");
            gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
            gameObject.GetComponent<Animator>().SetBool("IsDead", true);
            navMeshAgent.isStopped = true;
            enabled = false;
            this.GetComponent<AudioSource>().clip = null;
            this.GetComponent<AudioSource>().Play();
            StopAllCoroutines();
            navMeshAgent.enabled = false;
            StartCoroutine("DestroyDelay");
        }
        if (currentHP > 0)
            RunAwayFromPlayer();
    }
    //When was attacked by player find guard
    //When found guard run away from player to random gameobject avoiding player and obstacles
    private void RunAwayFromPlayer()
    {
            if (gameObject.GetComponent<AudioSource>().clip != GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound)
            {
                gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                gameObject.GetComponent<AudioSource>().loop = true;
                gameObject.GetComponent<AudioSource>().Play();
            }
            if (!foundWay)
            {
                while (true)
                {
                    float randomX = 0;
                    float randomZ = 0;
                    randomX = Random.Range(-50, 1);
                    randomZ = Random.Range(-40,40);
                    Vector3 destination = new Vector3(randomX, transform.position.y, randomZ);
                    NavMeshPath path = new NavMeshPath();
                    if (GetComponent<NavMeshAgent>().CalculatePath(destination, path))
                    {
                            foundWay = true;
                            navMeshAgent.SetDestination(destination);
                            break;
                    }
                }
            }
            if (navMeshAgent.destination != null)
                foreach (GameObject person in fractionTrigger.objectsInRadius.ToArray())
                {
                    if ((navMeshAgent.destination - person.transform.position).magnitude < (navMeshAgent.destination - transform.position).magnitude)
                        foundWay = false;
                }
            if (navMeshAgent.remainingDistance <= 1.5f)
                foundWay = false;
    }
    //Destroy gameObject after 10 minutes
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(600);
        Destroy(gameObject);
    }
    private void ChangeLayer(GameObject objectToSearch)
    {
        for (int i = 0; i < objectToSearch.transform.childCount; i++)
        {
            objectToSearch.transform.GetChild(i).gameObject.layer = 21;
            if (objectToSearch.transform.GetChild(i).transform.childCount > 0)
                ChangeLayer(objectToSearch.transform.GetChild(i).gameObject);
        }
    }
    private void LoadAnimalData()
    {
        AnimalData animalData = SaveLoad.globalAnimalData;
        for (int i = 0; i < animalData.ID.Length; i++)
        {
            if (ID == animalData.ID[i])
            {       
                foundWay = animalData.foundWay[i];
                transform.position = new Vector3(animalData.position[i, 0], animalData.position[i, 1], animalData.position[i, 2]);
                transform.eulerAngles = new Vector3(animalData.rotation[i, 0], animalData.rotation[i, 1], animalData.rotation[i, 2]);
                if (currentHP > 0)
                {
                            GetComponent<NavMeshAgent>().enabled = true;
                            GetComponent<NavMeshAgent>().SetDestination(new Vector3(animalData.destination[i, 0], animalData.destination[i, 1], animalData.destination[i, 2]));
                            GetComponent<Animator>().SetBool("IsRunning", true);
                            GetComponent<Animator>().Play("Run");
                    GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().walkingSound;
                            GetComponent<AudioSource>().loop = true;
                            GetComponent<AudioSource>().Play();
                }
            }
        }
    }
    private void LoadHP()
    {
        AnimalData animalData = SaveLoad.globalAnimalData;
        for (int i = 0; i < animalData.ID.Length; i++)
        {
            if (ID == animalData.ID[i])
            {
                transform.position = new Vector3(animalData.position[i, 0], animalData.position[i, 1], animalData.position[i, 2]);
                transform.eulerAngles = new Vector3(animalData.rotation[i, 0], animalData.rotation[i, 1], animalData.rotation[i, 2]);
                currentHP = animalData.currentHP[i];
                if (currentHP <= 0)
                {
                    GetComponent<Animator>().SetBool("IsDead", true);
                    GetComponent<Animator>().PlayInFixedTime("Death", 0, 1);
                }
            }
        }
    }
    private void LoadLoot()
    {
        AnimalData animalData = SaveLoad.globalAnimalData;
        for (int i = 0; i < animalData.ID.Length; i++)
        {
            if (ID == animalData.ID[i])
            {
                for (int b = 0; b < GetComponent<Loot>().loot.Length; b++)
                {
                    GetComponent<Loot>().loot[b] = null;
                    GetComponent<Loot>().amountOfItems[b] = 0;
                }
                for (int b = 0; b < GetComponent<Loot>().loot.Length; b++)
                {
                    GetComponent<Loot>().loot[b] = GameObject.Find("GUIManager").GetComponent<Inventory>().ReturnItemByName(animalData.itemName[i, b]);
                    GetComponent<Loot>().amountOfItems[b] = animalData.amountOfItems[i, b];
                }
            }
        }
    }
}
