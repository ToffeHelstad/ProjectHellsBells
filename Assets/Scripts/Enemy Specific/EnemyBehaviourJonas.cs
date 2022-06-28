using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityHealth))] //Requires an EntityHealth component to work. Attaches itself to an object when this script is attached
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
        //Subscribes to EntityHealth events!
        myHealth.OnDamage.AddListener(OnDamage);
        myHealth.OnDead.AddListener(OnDead);
        myHealth.OnHeal.AddListener(OnHeal);
    }

    public void FindPlayerTarget() //When instantiated, find the player and makes that its target
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        agent.destination = target.position; //follow player plz
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
