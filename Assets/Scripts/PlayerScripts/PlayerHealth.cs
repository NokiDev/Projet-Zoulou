using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float health = 100f; //Vie du joueur
    public float invicibilityTime = 2f;//Temps entre deux coups

    public float hurtForce = 10f; //Force ajouté lorsque le joueur prends un coup

    public float damageAmount = 10f; //Nombre de dégat pris (REWORK SOON)

    private SpriteRenderer healthBar; //Référence au sprite de la barre de vie
    private float lastHitTime;//Dernière fois ou le joueur s'est fait touché
    private Vector3 healthScale;//Scale pour la bare de vie
    private PlayerControl playerControl;//Référence au script de control 
    private Animator anim;//Référence a l'animator
    private Rigidbody2D rigidBody2D; //Référence au rigidbody

	// Use this for initialization
	void Awake () {
        playerControl = GetComponent<PlayerControl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        healthScale = healthBar.transform.localScale;
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        // Si le gameobject a le tag Enemy
        if (col.gameObject.tag == "Enemy")
        {
            // ... Et si le temps est supérieur au temps du dernier coup + temps d'invincibilité
            if (Time.time > lastHitTime + invicibilityTime)
            {
                //Si le joueur a toujours de la vie
                if (health > 0f)
                {
                    // ... prends des dégat et reset le temps du dernier coup pris
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                }
                // Si le joueur n'as plus de vie, il tombe (façon mario)
                else
                {
                    // Trouve tout les colliders 2D du gameobject et les mets en trigger (permet de tomber)
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols)
                    {
                        c.isTrigger = true;
                    }

                    //Deplace tout les sprite du gameObject au premier plan
                    SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer s in spr)
                    {
                        s.sortingLayerName = "UI";
                    }

                    // ... Desactive les script de controle
                    GetComponent<PlayerControl>().enabled = false;

                    // ... Active l'état de mort dans l'animator
                    anim.SetTrigger("Die");
                }
            }
        }
    }

    void TakeDamage(Transform enemy)
    {
        // Desactive le bool de jump
        playerControl.jump = false;

        // Creer un vecteur de l'enemi jusqu'au joueur plus un boost up
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

        // Ajoute une force en direction du vecteur multiplié par la force de dégats
        rigidBody2D.AddForce(hurtVector * hurtForce);

        // Réduit la vie du joueur de 10
        health -= damageAmount;

        // Update la barre de vie
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        //Set la couleur de la barre en fonction de la vie du joueur 
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

        // Set le scale de la barre de vie proportionnellement a ses points de vie
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);

    }
}
