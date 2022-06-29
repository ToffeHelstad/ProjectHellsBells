using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]                             //Adds character controller if its not there already
[RequireComponent(typeof(CombatScript))]                                    //Adds combat script if its not there already
[RequireComponent(typeof(DashThirdPerson))]                                 //Adds Dash script if its not there already
public class NEWINPUTThirdPerson : MonoBehaviour
{
    [Header("Gravity")]
    [Tooltip("Adds gravity. Default is: 1")]
    public float gravityMultiplier = 1f;                                    //Sets a gravity multiplier
    [Tooltip("Drag GroundCheck object here")]
    public Transform groundCheck;                                           //Reference to object that checks if the ground is under the player
    [Tooltip("Select layer that is set as ground level")]
    public LayerMask groundLayer;                                           //Reference to which layer is "ground"
    [Tooltip("Is the player on the ground?")]
    public bool isGrounded;                                                 //bool that confirms if the player is grounded

    [Header("Speed")]
    [Tooltip("Sets walkspeed. Default is: 6")]
    public float walkSpeed = 6f;                                            //Variable for walkSpeed

    //Gravity Variables
    private float gravityConstant = -9.71f;                                 //Sets a constant for gravity calculations
    private Vector3 velocity;                                               //Variables for velocity

    [Header("Rotation")]
    [Tooltip("How smooth the playerObject rotates with the camerarotation. Defaults to 0.1")]
    public float turnSmoothTime = 0.1f;                                     //Sets how fast Player turns when camera rotates
    private float turnSmoothVelocity;                                       //Honestly unsure what this one does lol
    [HideInInspector]
    public Vector3 moveDir;                                                 //Creates cordinates of what direction player is facing

    [Header("Jump")]
    [Tooltip("How high the player jumps.")]
    public float jumpHeight;                                                //Sets how high player jumps                                               

    [Header("Camera")]
    [Tooltip("Drag in Main Camera here.")]
    public Transform MainCamera;
    [HideInInspector]
    public CharacterController controller;

    private PlayerChar playerChar;
    private InputAction movement;

    private Animator moveAnim;
    private bool moving;

    //Other Scripts (Assigned automatically)
    private CombatScript combatScript;
    private DashThirdPerson dashScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveAnim = GetComponentInChildren<Animator>();

        combatScript = GetComponent<CombatScript>();
        dashScript = GetComponent<DashThirdPerson>();

    }



    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        else
        {
            velocity.y += gravityConstant * gravityMultiplier * Time.deltaTime;
        }


        if (isGrounded == false)
        {
            moveAnim.SetBool("Falling", true);
        }
        else
        {
            moveAnim.SetBool("Falling", false);
        }
    }

    private void Awake()
    {
        playerChar = new PlayerChar();
    }

    private void OnEnable()
    {
        movement = playerChar.Player.Movement;
        movement.Enable();

        playerChar.Player.Jump.performed += DoJump;
        playerChar.Player.Jump.Enable();

        playerChar.Player.Attack.performed += DoAttack;
        playerChar.Player.Attack.Enable();

        playerChar.Player.Dash.performed += DoDash;
        playerChar.Player.Dash.Enable();
    }

    private void DoDash(InputAction.CallbackContext obj)
    {
        dashScript.StartDash();
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        combatScript.SwordAttack();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Jump();
    }

    private void OnDisable()
    {
        movement.Disable();
        playerChar.Player.Jump.Disable();
        playerChar.Player.Attack.Disable();
        playerChar.Player.Dash.Disable();
    }

    void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jumping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityConstant * gravityMultiplier);
            moveAnim.SetTrigger("Jump");
        }
    }

    public void Move()
    {
        controller.Move(velocity * Time.deltaTime);

        float horizontal = movement.ReadValue<Vector2>().x;
        float vertical = movement.ReadValue<Vector2>().y;                              //Input for vertical movement
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;       //Creates a directional variable bazed on which axes is used and normalizes it

        if (direction.magnitude >= 0.1f)                                             //if the direction variable is greater than 0.1
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.eulerAngles.y;      //returns rotational value
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);                    //Moves the character controller
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving == true)
        {
            moveAnim.SetBool("Running", true);
        }
        else
        {
            moveAnim.SetBool("Running", false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
}
