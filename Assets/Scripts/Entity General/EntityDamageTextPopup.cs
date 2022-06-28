using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageTextPopup : MonoBehaviour
{
    //This script simply exists to make dmg popups look at camera, and destroy itself afer a given time

    void Start()
    {
        Destroy(this.gameObject, .6f);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
