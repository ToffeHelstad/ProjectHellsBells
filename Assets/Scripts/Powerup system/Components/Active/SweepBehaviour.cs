using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepBehaviour : MonoBehaviour
{
    //The behavior that launches the active powerup sweep

    [SerializeField] private GameObject sweepPrefab; //The prefab to launch
    [SerializeField] private float sweepCooldown; //Cooldown time
    [SerializeField] private float sweepForce; //The force of sweep launch
    [SerializeField] private float sweepDuration; //How long the sweep lasts
    [SerializeField] private int sweepDamage; //How much damage does the sweep do
    private float sweepTimer;

    private float defaultSpeed = 25; //Default movement speed. Should not do it this way, i was just lazy

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //When Q is pressed down, set walk speed to 0
        {
            transform.parent.GetComponentInParent<ThirdPersonBrackeys>().walkSpeed = 0;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            //Do something so it aims :))
        }
        if (Input.GetKeyUp(KeyCode.Q)) //When releasing Q, spawn the object, add force to its rigidbody, destroy it after X amount of time and then set player speed back to default
        {
            GameObject sweepClone = Instantiate(sweepPrefab, transform.position + new Vector3(0, 0, 0), Camera.main.transform.rotation);
            sweepClone.GetComponent<Rigidbody>().AddForce(sweepClone.transform.forward * sweepForce, ForceMode.Impulse);
            sweepClone.GetComponent<SweepObject>().dmgValue = sweepDamage;
            Destroy(sweepClone, sweepDuration);
            transform.parent.GetComponentInParent<ThirdPersonBrackeys>().walkSpeed = defaultSpeed;
        }
    }
}
