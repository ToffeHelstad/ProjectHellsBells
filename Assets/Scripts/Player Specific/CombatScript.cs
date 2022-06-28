using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public Animator animationController;
    public Collider sword;
    
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwordAttack();
        }
    }

    public void SwordAttack()
    {
        CanAttack = false;
        animationController.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }


  
    //listen i've been at this for 6 hours now and there's a naruto event in fortnite, so i'mma take a break
}
