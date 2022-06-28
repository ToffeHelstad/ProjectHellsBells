using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    //Max health and current health
    [SerializeField] private int maxHealth, currentHealth;

    //Events for tracking damage, death and heal
    [HideInInspector] public UnityEvent<int> OnDamage, OnDead, OnHeal;

    [SerializeField] private string TagToCheckFor;

    private void Awake()
    {
        if (OnDamage == null) OnDamage = new UnityEvent<int>();
        if (OnDead == null) OnDead = new UnityEvent<int>();
        if (OnHeal == null) OnHeal = new UnityEvent<int>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamageMe(int dmgValue)
    {
        currentHealth -= dmgValue;
        OnDamage.Invoke(dmgValue);

        if (currentHealth <= 0) OnDead.Invoke(dmgValue);
    }

    public void HealMe(int healValue)
    {
        currentHealth += healValue;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        OnHeal.Invoke(healValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagToCheckFor))
        {
            //TESTING VALUE. SHOULD BE RETRIEVED FROM A WEAPON SCRIPT OR ANYTHING ELSE ON PLAYER
            DamageMe(100);
        }
    }
}
