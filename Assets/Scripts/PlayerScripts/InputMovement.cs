using UnityEngine;
using System.Collections;

public class InputMovement : MonoBehaviour {

    // Use this for initialization
    public float jump_force = 500.0f;

    public bool has_double_jump = false;
    private bool has_jumped = false;
    private bool can_jump = true;
    private Rigidbody2D rigidBody;

	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("left"))
            gameObject.transform.position += Vector3.left * 0.1f;
        if (Input.GetKey("right"))
            gameObject.transform.position += Vector3.right * 0.1f;
        if (Input.GetKeyDown("space"))
        {
            if(can_jump)
            {
                if (!has_jumped)
                {
                    can_jump = false;
                    has_jumped = true;
                    if (has_double_jump)
                    {
                        can_jump = true;
                    }
                }
                else
                    can_jump = false;
                rigidBody.AddForce(new Vector2(0.0f, jump_force));
            }
        } 
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ground")
        {
            if (has_jumped)
            {
                can_jump = true;
                has_jumped = false;
            }
              
        }

    }


}
