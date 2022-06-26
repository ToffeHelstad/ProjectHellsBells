using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScriptTonje : MonoBehaviour
{

    public GameObject player;
    public int minSpeed, maxSpeed;
    public int actualSpeed;
    public float gravityMultiplier;
    public float range;

    [Header("Groundcheck")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Private variables")]
    Vector3 velocity, movementDir;
    private float gravityConstant = -9.71f;
    GameObject self;

    bool waiting = false;

    void Start()
    {
        
        self = gameObject;
        actualSpeed = Random.Range(minSpeed, maxSpeed);

        //FlashlightGunScript.KillEnemy += Die();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);

        velocity.y += gravityConstant * gravityMultiplier * Time.deltaTime;


        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist > range && !waiting)
        {
            waiting = true;
            StartCoroutine(WaitThenDestroy());
        }

        if (!waiting && dist < range)
        {
            Move();
        }


        if (waiting && dist < range)
        {
            waiting = false;
            StopAllCoroutines();
            Move();
        }

    }

    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(5);
        actualSpeed = 0;
        Destroy(self);

        yield return new WaitForSeconds(0);
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, actualSpeed * Time.deltaTime);
        Vector3 direction = Vector3.RotateTowards(Vector3.forward, player.transform.position - transform.position, 3f, 0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Die()
    {

    }
}
