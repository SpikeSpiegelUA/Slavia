using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ConeOfView : MonoBehaviour
{
    public bool addToDetected = false;
    public bool minusToDetected = false;
    private bool hasHit = false;
    //Cone of View for stealth
    public void ConeOfViewMake()
    {
        if (gameObject.tag == "VillageGuard"||gameObject.tag=="Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
        {
            if(gameObject.GetComponent<GuardAI>()!=null)
            if (GetComponent<GuardAI>().gameManager.player.isDetected == false && GetComponent<GuardAI>().currentHP > 0)
            {
                Ray aimRay = new Ray(gameObject.GetComponent<NavMeshAgent>().transform.position + gameObject.GetComponent<NavMeshAgent>().transform.up * 1.2f, transform.forward);
                Ray viewRay = new Ray(gameObject.transform.position+transform.up*0.5f, GameObject.Find("Player").transform.position - transform.position);
                    LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore"));
                    RaycastHit checkHit;
                    if (Physics.Raycast(viewRay,out checkHit, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore)) 
                    {
                            if (Vector3.Angle(aimRay.direction, viewRay.direction) <= 75 && ((GameObject.Find("Player").transform.position - transform.position).magnitude <= 10))
                            {
                            if (checkHit.collider.tag=="Player")
                                hasHit = true;
                            else
                                hasHit = false;
                            if (hasHit)
                            {
                                StopCoroutine("MinusPointsToDetectedValue");
                                minusToDetected = false;
                            }
                                if (addToDetected == false && (GetComponent<GuardAI>().detection < 100))
                                {
                                addToDetected = true;
                                    StartCoroutine("AddPointsToDetectedValue");
                                }
                            }
                            if (Vector3.Angle(aimRay.direction, viewRay.direction) > 75 || (GameObject.Find("Player").transform.position - transform.position).magnitude > 10 || checkHit.collider.gameObject != GameObject.Find("Player"))
                            {
                                StopCoroutine("AddPointsToDetectedValue");
                                addToDetected = false;
                                if (minusToDetected == false && GetComponent<GuardAI>().detection > 0)
                                {
                                    minusToDetected = true;
                                    StartCoroutine("MinusPointsToDetectedValue");
                                }
                            
                        }
                   }
            }
        }
            if (gameObject.tag == "Civilian" || gameObject.tag == "SimplePeople")
            {
                if (GetComponent<CivilianAI>().gameManager.player.GetComponent<PlayerController>().isDetected == false && GetComponent<CivilianAI>().currentHP > 0)
                {
                    Ray aimRay = new Ray(gameObject.GetComponent<NavMeshAgent>().transform.position + gameObject.GetComponent<NavMeshAgent>().transform.up * 1.2f, transform.forward);
                    Ray viewRay = new Ray(gameObject.transform.position+transform.up*0.5f, (GameObject.Find("Player").transform.position - transform.position));
                LayerMask layer = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("SelfIgnore"));
                RaycastHit checkHit;
                Debug.DrawRay(viewRay.origin, viewRay.direction);
                if (Physics.Raycast(viewRay, out checkHit, Mathf.Infinity, layer,QueryTriggerInteraction.Ignore))
                {
                    if (Vector3.Angle(aimRay.direction, viewRay.direction) <= 75 && ((GameObject.Find("Player").transform.position - transform.position).magnitude <= 10))
                    {
                        if (checkHit.collider.tag=="Player")
                            hasHit = true;
                        else
                            hasHit = false;
                        if (hasHit)
                        {
                            StopCoroutine("MinusPointsToDetectedValue");
                            minusToDetected = false;
                        }
                        if (addToDetected == false && (GetComponent<CivilianAI>().detection < 100))
                        {
                            addToDetected = true;
                            StartCoroutine("AddPointsToDetectedValue");
                        }
                    }
                    if (Vector3.Angle(aimRay.direction, viewRay.direction) > 75 || (GameObject.Find("Player").transform.position - transform.position).magnitude > 10||checkHit.collider.gameObject!= GameObject.Find("Player"))
                    {
                        StopCoroutine("AddPointsToDetectedValue");
                        addToDetected = false;
                        if (minusToDetected == false && GetComponent<CivilianAI>().detection > 0)
                        {
                            minusToDetected = true;
                            StartCoroutine("MinusPointsToDetectedValue");
                        }

                    }
                }
                }
            }
    }
    //Add points to detection slider in addict to destination length
    private IEnumerator AddPointsToDetectedValue()
    {
        if (hasHit) 
        { 
            if ((GameObject.Find("Player").transform.position - transform.position).magnitude <= 10 && (GameObject.Find("Player").transform.position - transform.position).magnitude >= 7)
            {
                if (gameObject.tag == "SimplePeople" || gameObject.tag == "Civilian")
                    GetComponent<CivilianAI>().detection += (10-10*GameObject.Find("SkillManager").GetComponent<SkillManager>().stealthModify/100);
                if (gameObject.tag == "VillageGuard"||gameObject.tag=="Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
                    GetComponent<GuardAI>().detection += (10 - 10 * GameObject.Find("SkillManager").GetComponent<SkillManager>().stealthModify / 100);
            }
            if ((GameObject.Find("Player").transform.position - transform.position).magnitude < 7 && (GameObject.Find("Player").transform.position - transform.position).magnitude > 3)
            {
                if (gameObject.tag == "VillageGuard" || gameObject.tag == "Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
                    GetComponent<GuardAI>().detection += (20 - 20 * GameObject.Find("SkillManager").GetComponent<SkillManager>().stealthModify / 100);
                if (gameObject.tag == "SimplePeople" || gameObject.tag == "Civilian")
                    GetComponent<CivilianAI>().detection += (20 - 20 * GameObject.Find("SkillManager").GetComponent<SkillManager>().stealthModify / 100);
            }
            if ((GameObject.Find("Player").transform.position - transform.position).magnitude <= 3 && (GameObject.Find("Player").transform.position - transform.position).magnitude >= 0)
            {
                if (gameObject.tag == "VillageGuard" || gameObject.tag == "Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
                    GetComponent<GuardAI>().detection = 100;
                if (gameObject.tag == "SimplePeople" || gameObject.tag == "Civilian")
                    GetComponent<CivilianAI>().detection = 100;
                GameObject.Find("Player").GetComponent<PlayerController>().isDetected = true;
            }
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().SetValueForStealth();
        yield return new WaitForSeconds(1f);
        GetComponent<ConeOfView>().addToDetected = false;
    }
    //Minus points to detection slider in addict to destination length
    private IEnumerator MinusPointsToDetectedValue()
    {
        if (gameObject.tag == "SimplePeople" || gameObject.tag == "Civilian")
            GetComponent<CivilianAI>().detection -= (10+ 10 * GameObject.Find("SkillManager").GetComponent<SkillManager>().stealthModify / 100);
        if (gameObject.tag == "VillageGuard" || gameObject.tag == "Bandit" || gameObject.tag == "Undead" || gameObject.tag == "Royalist" || gameObject.tag == "Republican")
            GetComponent<GuardAI>().detection -= (10+10 * GameObject.Find("SkillManager").GetComponent<SkillManager>().stealthModify / 100);
        GameObject.Find("GameManager").GetComponent<GameManager>().SetValueForStealth();
        yield return new WaitForSeconds(1f);
        GetComponent<ConeOfView>().minusToDetected = false;
    }
}
