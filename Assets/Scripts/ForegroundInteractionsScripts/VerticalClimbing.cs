using UnityEngine;
using System.Collections;

public class VerticalClimbing : MonoBehaviour {


    private PlayerScript m_Player;      //Référence au script playerScript
    [SerializeField]
    private LayerMask m_Mask;           //Masque pour le calque du joueur

    private Transform m_AtTopCheck;     // Position ou l'on teste si le joueur touche le sol
    const float k_AtTopRadius = .2f;    // Rayon du cercle de collision pour savoir si le joueur touche le sol
    [SerializeField]
    private bool m_AtTop;

    private Transform m_AtBotCheck;     // Position ou l'on teste si le joueur touche le sol
    const float k_AtBotRadius = .2f;    // Rayon du cercle de collision pour savoir si le joueur touche le sol
    [SerializeField]
    private bool m_AtBot;               //Indique si le joueur est en bas de l'échelle
    [SerializeField]
    private bool m_GoTop = false;       //Indique si le joueur va en haut
    [SerializeField]
    private bool m_GoBot = false;       //Indique si le joueur va en bas
    [SerializeField]
    private bool m_OnLadder = false;    //Indque si le joueur est sur l'echelle

	// Use this for initialization
	void Awake () {
        //Initialise les références
        m_Player = GameObject.Find("Character").GetComponent<PlayerScript>();
        m_AtTopCheck = transform.Find("Top");
        m_AtBotCheck = transform.Find("Bot");
    }

    void FixedUpdate()
    {
        //Verifie si le joueur est en haut
        m_AtTop = Physics2D.OverlapCircle(m_AtTopCheck.position, k_AtTopRadius, m_Mask);
        //Verifie si le joueur est en bas
        m_AtBot = Physics2D.OverlapCircle(m_AtBotCheck.position, k_AtBotRadius, m_Mask);
        //Verifie si le joueur est sur l'echelle
        m_OnLadder = Physics2D.Linecast(m_AtBotCheck.position, m_AtTopCheck.position, m_Mask);

        //Si le joueur est en haut et ne vas pas en bas, 
        //ou si le joueur est en bas mais ne vas pas en haut ou si le joueur n'est pas sur l'echelle
        if ((m_AtTop && !m_GoBot) || (m_AtBot && !m_GoTop) || !m_OnLadder)
        {
            //On arrete de grimper
            StopClimbing();
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        float h = Input.GetAxis("Vertical");
        //Le joueur ne peut grimper que si il est sur l'echelle...
        if (m_OnLadder)
        {
            //... et qu'il va en haut ou en bas
            m_GoBot = false;
            m_GoTop = false;
            if (h > 0 && !m_AtTop)
            {
                m_Player.Climb();
                m_GoTop = true;
            }
            else if (h < 0 && !m_AtBot)
            {
                m_Player.Climb();
                m_GoBot = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //Arrete de grimper lorsqu'il sort du trigger
        StopClimbing();
    }

    void StopClimbing()
    {
        m_GoBot = false;
        m_GoTop = false;
        m_Player.StopClimbing();
    }
}