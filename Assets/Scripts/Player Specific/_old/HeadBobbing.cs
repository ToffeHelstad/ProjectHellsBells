using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadBobbing : MonoBehaviour
{
    [Tooltip("Drag gameObject with Character Controller script in here")]
    public PlayerMovement controller;

    [Header("Bobbing")]
    [Tooltip("Speed of headbobbing. Should correspond with walkingspeed in charactercontroller. Default is: 14")]
    public float walkingBobbingSpeed = 14f;
    [Tooltip("Amount of bobbing. Defaults to 0.05")]
    public float bobbingAmount = 0.05f;
    

    private float defaultPosY = 0;
    private float timer = 0;

    
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(controller.movementDir.x) > 0.1f || Mathf.Abs(controller.movementDir.z) > 0.1f)
        {
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            //Player is idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
