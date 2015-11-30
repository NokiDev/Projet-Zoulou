using UnityEngine;
using System.Collections;


public class SwordScript : DamageSource {

    private PlayerControl playerCtrl;
    private Animator anim;


	// Use this for initialization
	void Awake () {
        playerCtrl = GameObject.Find("Character").GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerCtrl.facingRight)
            range = 2f;
        else
            range = 0.3f;

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
