using UnityEngine;
using System.Collections;


public class SwordScript : DamageSource {

    private Animator anim;


	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if(Input.GetButton("Fire1") && !attackLocked)
        {
            attack = true;
        }

	}

    void FixedUpdate()
    {
        if(attack)
        {
            anim.SetTrigger("Attack");
            Attack();
            attack = false;
            LockAttack();
        }
    }

    void  Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
        foreach(Collider2D enemy in enemies)
        {
            HealthManager healthManager = enemy.gameObject.GetComponent<HealthManager>();
            healthManager.TakeDamage(this);
        }
    }
}
