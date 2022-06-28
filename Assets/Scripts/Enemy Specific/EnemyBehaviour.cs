using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Other Objects")]
    [Tooltip("Pull in particle system for when enemy gets hit here.")]
    public ParticleSystem hitParticles;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Enemy Variables")]
    [Tooltip("Speed of enemy")]
    public int speed;

    [Header("Despawn range")]
    [Tooltip("How far away the player has to be from the enemy before it despawns.")]
    public float range;

    [Header("Gravity")]
    public float gravityMultiplier = 1f;
    private float gravityConstant = -9.71f;

    [Header("Health")]
    [Tooltip("Health that enemy starts with starts with")]
    public int startingHP;
    [Tooltip("How much HP the enemy currently has")]
    public int currentHP;
    [Tooltip("How much health the enemy has taken")]
    public int damageTaken = 0;

    //Private variables
    private GameObject player;
    private Vector3 velocity;
    private Vector3 movementDir;

    private bool waiting = false;
    private GameObject self;

    

    void Start()
    {
        self = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        currentHP = startingHP;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);
        velocity.y += Vector3.Distance(player.transform.position, transform.position);

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if(dist > range && !waiting)
        {
            waiting = true;
            StartCoroutine(WaitThenDestroy());
        }

        if(!waiting && dist < range)
        {
            Move();
        }

        if(waiting && dist < range)
        {
            waiting = false;
            StopAllCoroutines();
            Move();
        }

        currentHP = startingHP - damageTaken;

        if (currentHP == 0)
        {
            Destroy(self);
        }
    }

    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(self);

        yield return new WaitForSeconds(0);
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 direction = Vector3.RotateTowards(Vector3.forward, player.transform.position - transform.position, 3f, 0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword")
        {
            hitParticles.Play();
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        damageTaken += 100;
    }
}
