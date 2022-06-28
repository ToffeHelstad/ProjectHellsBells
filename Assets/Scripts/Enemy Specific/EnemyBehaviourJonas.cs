using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityHealth))]
public class EnemyBehaviourJonas : MonoBehaviour
{
    private NavMeshAgent agent; //NavMeshAgent
    private EntityHealth myHealth; //The entity health

    [SerializeField] private Transform target; //The target the agent moves towards

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myHealth = GetComponent<EntityHealth>();
    }

    void Start()
    {
        myHealth.OnDamage.AddListener(OnDamage);
        myHealth.OnDead.AddListener(OnDead);
        myHealth.OnHeal.AddListener(OnHeal);
    }

    void Update()
    {
        agent.destination = target.position;
    }

    private void OnDamage(int dmgValue)
    {
        Debug.Log(this.name + " hit for: " + dmgValue + " damage");
        //Do on damage things, like particles and text-popup
    }

    private void OnHeal(int healValue)
    {
        //Do on heal stuff, like particles and text-popup
    }

    private void OnDead(int dmgValue)
    {
        Debug.Log(this.name + " died");
        //Do on dead stuff
        Destroy(this.gameObject);
    }
}
