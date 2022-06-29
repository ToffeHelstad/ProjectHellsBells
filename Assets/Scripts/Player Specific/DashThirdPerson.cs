using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashThirdPerson : MonoBehaviour
{

    NEWINPUTThirdPerson moveScript;                                                         //Reference to the Third Person movement script

    [Header("Dash time variables")]
    [Tooltip("Sets how fast the dash should be. Default is 20.")]
    public float dashSpeed = 20f;                                                           //Public variable for Dash speed
    [Tooltip("Decides how long dash will last. Default is 0.25")]
    public float dashTime = 0.25f;                                                          //Public variable for how long Dash will last
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<NEWINPUTThirdPerson>();                                   //Grabs movement script at start frame
    }

    public void StartDash()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        particles.Play();

        while (Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            
            Debug.Log("Dashing");

            yield return null;
        }
        
    }
}
