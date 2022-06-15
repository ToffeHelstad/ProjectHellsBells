using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
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

    [Header("Rotation")]
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Header("Camera")]
    public Transform cam;

    Vector3 direction;

    void Start()
    {
        cc = GetComponent<CharacterController>();       //Fetches character controller component
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer); //isGrounded is decided through a spherecast

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
        Vector3 direction = new Vector3(x, 0f, z).normalized;


        if (Input.GetKey(KeyCode.LeftShift) && direction.magnitude >= 0.1f)
        {
            Run();
        }
        else if(direction.magnitude >= 0.1f)
        {
            Move();
        }
    }

    private void Move()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 motion = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        cc.Move(motion.normalized*walkSpeed*Time.deltaTime);
        
    }

    private void Run()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 motion = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        cc.Move(motion.normalized * runSpeed * Time.deltaTime);
    }
}
