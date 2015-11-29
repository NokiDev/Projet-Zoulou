using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    // Use this for initialization

    [HideInInspector]
    public bool facingRight = true; // Utilisé pour savoir de quel coté le joueur avance, pour changer l'animation aussi
    [HideInInspector]
    public bool jump = false; //Si le joueur est en saut
    public bool has_double_jump = false; //Si le joueur peut effectuer un double saut

    public float moveForce = 365f; // Force ajouté pour bouger le joueur a gauche et à droite
    public float maxSpeed = 5f; //Vitesse maximum pour le joueur
    public float jump_force = 500.0f; //Force ajouté pour que le joueur saute

    private Transform groundCheck; //Permet de savoir si le joueur touche le sol ou non
    private bool grounded = false; //si le joueur est au sol ou non
    private Animator anim; //Reference a l'animator du joueur
    private Rigidbody2D rigidBody2D;

	void Awake () {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    void FixedUpdate() {
        
        //Récupère la valeur de l'input horizontal (joystick, ou fleche gauche et droite)
        float h = Input.GetAxis("Horizontal");

        //Donne a l'animator une information sur la vittesse du joueur
        anim.SetFloat("Speed", Mathf.Abs(h));

        //Si le joueur change de direction, ou qu'il n'as pas atteint sa vitesse Max
        if ((h * rigidBody2D.velocity.x) < maxSpeed)
        {
            //Ajoute une force en fonction de la puissance de l'input horizontal et de la force de movement
            rigidBody2D.AddForce(Vector2.right * h * moveForce);
        }
        //Si la velocité du joueur est supérieur a la vitesse maximum
        if (Mathf.Abs(rigidBody2D.velocity.x) > maxSpeed)
        {
            //Set la velocity par rapport a la vitesse Max
            rigidBody2D.velocity = new Vector2(Mathf.Sign(rigidBody2D.velocity.x) * maxSpeed, rigidBody2D.velocity.y);
        }
        //Vérifie si le joueur va a gauche ou a droite
        if (h > 0 && !facingRight)
        {
            //Retourne le joueur
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            //Retourne le joueur
            Flip();
        }
        if (jump)
        {
            //Lance l'animation de saut
            anim.SetTrigger("Jump");
            //Ajoute une force Verticale au joueur.
            rigidBody2D.AddForce(new Vector2(0f, jump_force));

            jump = false;
        } 
	}

    void Flip()
    {
        //Inverse le regard du joueur (Gauche -> Droite) et (Droite -> Gauche)
        facingRight = !facingRight;

        //Inverse le scale du joueur, et donc inverse l'image
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
