using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Gravity")]
    [Tooltip("Adds gravity. Default is: 1")]
    public float gravityMultiplier = 1f;                 //Sets a gravity multiplier
    [Tooltip("Drag GroundCheck object here")]
    public Transform groundCheck;                       //Reference to object that checks if the ground is under the player
    [Tooltip("Select layer that is set as ground level")]
    public LayerMask groundLayer;                       //Reference to which layer is "ground"
    [Tooltip("Is the player on the ground?")]
    public bool isGrounded;                             //bool that confirms if the player is grounded

    [Header("Speed")]
    [Tooltip("Sets walkspeed. Default is: 6")]
    public float walkSpeed = 6f;                                             //Variable for walkSpeed
    [Tooltip("Sets runspeed. Should generally be 2-3 above Walkspeed")]
    public float runSpeed = 8f;                                              //Variable for runSpeed

    private float gravityConstant = -9.71f;             //Sets a constant for gravity calculations
    private CharacterController cc;                     //Reference to Character Controller component on object
    private Vector3 velocity;                           //Variables for velocity

    [HideInInspector]
    public Vector3 movementDir;

    private void Start()
    {
        cc = GetComponent<CharacterController>();       //Fetches character controller component
    }


    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);          //isGrounded is decided through a spherecast

        if (isGrounded && velocity.y < 0)                                                   
        {
            velocity.y = -2;
        }
        else
        {
            velocity.y += gravityConstant * gravityMultiplier * Time.deltaTime;
        }

        cc.Move(velocity * Time.deltaTime);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movementDir = ((transform.right * x) + (transform.forward * z)).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else
        {
            Move();
        }
    }

    void Move()                                                                 //Function that allows movement
    {
        Vector3 motion = movementDir * walkSpeed * Time.deltaTime;
        cc.Move(motion);
    }

    void Run()                                                                  //Function that enables running
    {
        Vector3 motion = movementDir * runSpeed * Time.deltaTime;
        cc.Move(motion);
    }
}
