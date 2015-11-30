using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    // Use this for initialization

    [HideInInspector]
    public bool facingRight = true; // Utilisé pour savoir de quel coté le joueur avance, pour changer l'animation aussi
    [HideInInspector]
    public bool first_jump = false; //Si le joueur est en saut
    public bool has_double_jump = false; //Si le joueur peut effectuer un double saut

    public float moveForce = 365f; // Force ajouté pour bouger le joueur a gauche et à droite
    public float maxSpeed = 5f; //Vitesse maximum pour le joueur
    public float jump_force = 500.0f; //Force ajouté pour que le joueur saute

    private Animator anim; //Reference a l'animator du joueur
    private Rigidbody2D rigidBody2D;

    private IsGroundedScript isGrounded;

	void Awake () {
        first_jump = true;
        anim = GetComponent<Animator>();
        isGrounded = GetComponent<IsGroundedScript>();
        rigidBody2D = GetComponent<Rigidbody2D>();
	}

    // Fonction de mise à jour de l'état des objects
    void Update() {

        if (Input.GetButtonDown("Jump") && (isGrounded.grounded || first_jump))
        {
            if (has_double_jump && first_jump && !isGrounded.grounded)
                first_jump = false;
            rigidBody2D.AddForce(new Vector2(0f, jump_force));
            anim.SetBool("Grounded", false);
        }
    }


    /*Fonction de mise à jour pour les objets physiques (rigibody etc...)**/
    void FixedUpdate() {

        anim.SetBool("Grounded", isGrounded.grounded);

        if (isGrounded.grounded)
            first_jump = true;

        //Récupère la valeur de l'input horizontal (joystick, ou fleche gauche et droite)
        float h = Input.GetAxis("Horizontal");

        //Donne a l'animator une information sur la vittesse du joueur
        anim.SetFloat("Speed", Mathf.Abs(h));
        //Vitesse verticale
        anim.SetFloat("vSpeed", rigidBody2D.velocity.y);

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
