using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public CombatScript wc;
    public float hitPause = 0.1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {

            Debug.Log("Hit!");
            StartCoroutine(HitDelay());
        }
    }

    IEnumerator HitDelay()
    {
        wc.animationController.speed = 0;
        yield return new WaitForSeconds(hitPause);
        wc.animationController.speed = 1;
    }

}
