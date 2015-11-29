using UnityEngine;
using System.Collections;

public class DamageSource : MonoBehaviour {

    /**/
    public float range = 2f; //Portée de la source de dégats
    public float damage = 3f; //Dégats de la source
    public float hurtForce = 50f; //Force de poussée lors d'une prise de dégats
    public float delay = 1f; //Delai entre deux attaque

    protected bool attack;
    protected bool attackLocked;

    public enum effect {NORMAL, POISON, STUN, FIRE, ICE};

    private void UnlockAttack()
    {
        attackLocked = false;
    }

    protected void LockAttack()
    {
        attackLocked = true;
        Invoke("UnlockAttack", delay);
    }

}
