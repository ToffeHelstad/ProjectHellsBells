using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;

    public int speed;

    [Header("Gravity")]
    public float gravityMultiplier;
    private float gravityConstant = -9.71f;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;

    public float range;

    private Vector3 velocity;
    private Vector3 movementDir;

    private bool waiting = false;
    private GameObject self;

    void Start()
    {
        self = gameObject;
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

}
