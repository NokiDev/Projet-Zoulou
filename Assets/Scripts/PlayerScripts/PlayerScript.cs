using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    private float m_MaxSpeed = 10f;                    // Vittesse Maximum, sur l'axe des "x"
    [SerializeField]
    private float m_JumpForce = 400f;                  // Force Ajouté lors d'un saut
    [Range(0, 1)]
    [SerializeField]
    private float m_CrouchSpeed = .36f;  // %tage de la maxSpeed appliqué lorsque le joueur s'acroupi
    [SerializeField]
    private bool m_AirControl = false;                 // Indique si le joueur peut controler son saut;
    [SerializeField]
    private LayerMask m_WhatIsGround;                  // Masque qui determine les layer représentant le sol

    private Transform m_GroundCheck;    // Position ou l'on teste si le joueur touche le sol
    const float k_GroundedRadius = .2f; // Rayon du cercle de collision pour savoir si le joueur touche le sol
    private bool m_Grounded;            // Indique si le joueur touche le sol.
    private Transform m_CeilingCheck;   // Position ou l'on teste si le joueur touche le plafond ou non
    const float k_CeilingRadius = .01f; // Rayon du cercle de collision pour savoir si le joueur touche le plafond
    private Animator m_Anim;            // Référence à l'animator du joueur
    private Rigidbody2D m_Rigidbody2D;  // Référence au rigidbody du joueur
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private bool m_Climb = false;


    // Use this for initialization
    void Awake () {
        //Mise en place des références
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

   void FixedUpdate()
    {
        m_Grounded = false;

        // Le player touche le sol si le cercle de collision touche quelque chose qui fais parti du masque WhatIsGround
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set la vitesse verticale pour les sauts
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }


    //Appelé lorsque l'on veut déplacer le joueur
    //Params : 
    // - float move, movement sur l'axe des x
    // - bool crouch, indique si le joueur est accroupi
    // - bool jump, indique si le joueur saute
    public void Move(float moveH, float moveV, bool crouch, bool jump)
    {
        if (!m_Climb)
        {
            // Si le joueur est accroupi on vérifie si il peut se lever
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // Si le joueur est bloqué par le plafond, alors il reste accroupi
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }
        }
        else
            crouch = false;

        // Set si le joueur est accroupi dans l'animator
        m_Anim.SetBool("Crouch", crouch);

        //Controle seulement si le player touche le sol ou qu'il peut bouger en l'air
        if (m_Grounded || m_AirControl)
        {
            // Réduit la vitesse si le personnage est accroupi
            moveH = (crouch ? moveH * m_CrouchSpeed : moveH);

            // Set la vitesse(absolue) du joueur dans l'animator
            m_Anim.SetFloat("Speed", Mathf.Abs(moveH));

            m_Anim.SetFloat("vSpeed", Mathf.Abs(moveV));

            if (m_Climb)
                m_Rigidbody2D.velocity = new Vector2(0f, moveV*7f);
            else
                m_Rigidbody2D.velocity = new Vector2(moveH * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // Vérifie si il faut inverser l'image si l'input est droite et que le joueur regarde a gauche
            if (moveH > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Sinon si l'input est gauche et que le joueur regarde a droite
            else if (moveH < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // Si le player peut sauter
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            // On ajoute une force vertical au joueur pour le saut
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Anim.SetTrigger("Jump");
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public void Climb()
    {
        m_Climb = true;
        m_Rigidbody2D.gravityScale = 0f;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D coll in colliders)
        {
            coll.isTrigger = true;
        }
        m_Anim.SetBool("Climb", m_Climb);
    }

    public void StopClimbing()
    {
        m_Climb = false;
        m_Rigidbody2D.gravityScale = 3f;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D coll in colliders)
        {
            coll.isTrigger = false;
        }
        m_Anim.SetBool("Climb", m_Climb);
    }

    void Flip()
    {
        // Interverti la direction du regard Gauche -> Droite, Droite -> Gauche
        m_FacingRight = !m_FacingRight;

        // Multiplie le scale du joueur par -1 pour inverser l'image
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
	
}
