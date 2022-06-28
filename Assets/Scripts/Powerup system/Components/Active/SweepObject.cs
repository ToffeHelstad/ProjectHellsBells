using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepObject : MonoBehaviour
{
    [HideInInspector] public int dmgValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EntityHealth>().DamageMe(dmgValue);
        }
    }
}
