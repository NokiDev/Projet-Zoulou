using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

    public float health = 10f; //Vie de l'entite
    public float invicibilityTime = 2f;//Temps entre deux coups

    public float hurtForce = 10f; //Force ajouté lorsque le joueur prends un coup

    private SpriteRenderer healthBar; //Référence au sprite de la barre de vie
    private float lastHitTime;//Dernière fois ou le joueur s'est fait touché
    private Vector3 healthScale;//Scale pour la bare de vie
    private Animator anim;//Référence a l'animator
    private Rigidbody2D rigidBody2D; //Référence au rigidbody


    void Awake()
    {
        healthBar = transform.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        healthScale = healthBar.transform.localScale;
    }

    public void TakeDamage(GameObject damageSource, float damageAmount)
    {
        if (Time.time > lastHitTime + invicibilityTime)
        {
            //Si le joueur a toujours de la vie
            if (health > 0f)
            {
                // ... prends des dégat et reset le temps du dernier coup pris
                // Creer un vecteur de l'enemi jusqu'au joueur plus un boost up
                Vector3 hurtVector = transform.position - damageSource.transform.position + Vector3.up * 5f;

                // Ajoute une force en direction du vecteur multiplié par la force de dégats
                rigidBody2D.AddForce(hurtVector * hurtForce);

                // Réduit la vie du joueur de 10
                health -= damageAmount;

                // Update la barre de vie
                UpdateHealthBar();
                lastHitTime = Time.time;
            }
            // Si le joueur n'as plus de vie, il tombe (façon mario)
            else
            {
                // ... Active l'état de mort dans l'animator
                anim.SetTrigger("Die");
            }
        }
    }

    public void UpdateHealthBar()
    {
        //Set la couleur de la barre en fonction de la vie du joueur 
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

        // Set le scale de la barre de vie proportionnellement a ses points de vie
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);

    }
}
