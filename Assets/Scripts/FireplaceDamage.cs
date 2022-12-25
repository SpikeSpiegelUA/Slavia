using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceDamage : MonoBehaviour
{
    private bool canDamage=true;
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.root.name == "Player" && canDamage)
        {
            collision.GetComponentInParent<PlayerController>().StartCoroutine("DamageScreenAppear");
            collision.GetComponentInParent<PlayerController>().currentHealth -= 5;
            StartCoroutine("Damage");
        }
    }
    private IEnumerator Damage()
    {
        canDamage = false;
        yield return new WaitForSeconds(1);
        canDamage = true;
    }
}
