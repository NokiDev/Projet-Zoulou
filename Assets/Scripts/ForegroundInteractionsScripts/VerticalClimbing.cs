using UnityEngine;
using System.Collections;

public class VerticalClimbing : MonoBehaviour {


    private GameObject player;
    private IsGroundedScript isGrounded;

    private bool climbLock;

    private bool climbing = false;
    public float speed = 3f;

	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Character");
        isGrounded = player.GetComponent<IsGroundedScript>();
	}

    void Update()
    {
        if (isGrounded.grounded)
        {
            StopClimb();
        }
    }
    
    void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log(" TriggerStay2D");
        float v = Input.GetAxis("Vertical");
        print(v);
        if (Mathf.Abs(v) > 0 && climbLock == false)
        {
            climbing = true;
            Collider2D[] cols = player.GetComponents<Collider2D>();
            foreach (Collider2D c in cols)
            {
                c.isTrigger = true;
            }
            Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
            player.GetComponent<Animator>().SetBool("IsClimbing", climbing);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -2f);
            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.gravityScale = 0.5f;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed * v);
        }
        climbLock = false;
    }

    void OnTriggerExit2D(Collider2D collisionInfo)
    {
        Debug.Log(" OnTriggerExit2D  ");
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        climbing = false;
        player.GetComponent<Animator>().SetBool("IsClimbing", climbing);
        rigidBody.velocity = new Vector2(0f, 0f);
        rigidBody.gravityScale = 1f;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        Collider2D[] cols = player.GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = false;
        }
        climbLock = true;
    }

    void StopClimb()
    {
        climbing = false;
        player.GetComponent<Animator>().SetBool("IsClimbing", climbing);
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 1f;
        player.GetComponent<PlayerControl>().enabled = true;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        Collider2D[] cols = player.GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = false;
        }
    }
}
