using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageTextPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, .6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
