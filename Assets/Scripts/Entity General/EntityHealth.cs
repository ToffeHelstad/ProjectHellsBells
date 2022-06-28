using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour //Class for defining every entity HP.
{
    //Max health and current health
    [SerializeField] private int maxHealth, currentHealth;

    //Events for tracking damage, death and heal
    [HideInInspector] public UnityEvent<int> OnDamage, OnDead, OnHeal;

    //Which tag to check for when dealing damage
    [SerializeField] private string TagToCheckFor;

    //Prefab for instantiating dmg text
    [SerializeField] private GameObject dmgTextPopupPrefab;

    private void Awake() //Initializes every event
    {
        if (OnDamage == null) OnDamage = new UnityEvent<int>();
        if (OnDead == null) OnDead = new UnityEvent<int>();
        if (OnHeal == null) OnHeal = new UnityEvent<int>();
    }

    void Start()
    {
        currentHealth = maxHealth; //Sets current health to max health
    }

    public void DamageMe(int dmgValue) //Damages the entity by int dmgValue. Instantiates dmgPopup and checks if entity is dead
    {
        currentHealth -= dmgValue;
        OnDamage.Invoke(dmgValue); //Invokes damage-events. All entities needs to listen to this event to do OnDamage-stuff

        GameObject dmgText = Instantiate(dmgTextPopupPrefab, transform.position, Quaternion.identity);
        dmgText.GetComponentInChildren<TMPro.TextMeshPro>().text = dmgValue.ToString(); //Sets dmgPopup text to dmgValue
        //Destroy(dmgText, .6f);

        if (currentHealth <= 0) OnDead.Invoke(dmgValue); //Checks if dead, if so, do OnDead things
    }

    public void HealMe(int healValue) //Heals the entity
    {
        currentHealth += healValue;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        OnHeal.Invoke(healValue); //Do OnHeal things
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
