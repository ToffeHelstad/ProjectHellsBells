using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject sweepPrefab;
    [SerializeField] private float sweepCooldown;
    [SerializeField] private float sweepForce;
    [SerializeField] private float sweepDuration;
    [SerializeField] private int sweepDamage;
    private float sweepTimer;

    private float defaultSpeed = 25;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.parent.GetComponentInParent<ThirdPersonBrackeys>().walkSpeed = 0;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            //Do something so it aims :))
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObject sweepClone = Instantiate(sweepPrefab, transform.position + new Vector3(0, 0, 0), Camera.main.transform.rotation);
            sweepClone.GetComponent<Rigidbody>().AddForce(sweepClone.transform.forward * sweepForce, ForceMode.Impulse);
            sweepClone.GetComponent<SweepObject>().dmgValue = sweepDamage;
            Destroy(sweepClone, sweepDuration);
            transform.parent.GetComponentInParent<ThirdPersonBrackeys>().walkSpeed = defaultSpeed;
        }
    }
}
