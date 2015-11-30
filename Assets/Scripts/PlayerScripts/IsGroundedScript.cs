using UnityEngine;
using System.Collections;

public class IsGroundedScript : MonoBehaviour {

    public Transform groundCheck;
    public bool grounded;
    public LayerMask groundMask;
    public float groundCheckRadius = 0.2f;
	
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
    }
}
