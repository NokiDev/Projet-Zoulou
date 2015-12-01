using UnityEngine;
using System.Collections;

public class UserControllerScript : MonoBehaviour {

    private PlayerScript m_Character; // Référence au personnage
    private bool m_Jump; //Indique si le joueur saute


    private void Awake()
    {
        m_Character = GetComponent<PlayerScript>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown("Jump");
        }
    }

    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // Pass all parameters to the character control script.
        m_Character.Move(h, v, crouch, m_Jump);
        m_Jump = false;
    }
}
