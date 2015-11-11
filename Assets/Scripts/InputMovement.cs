using UnityEngine;
using System.Collections;

public class InputMovement : MonoBehaviour {

    // Use this for initialization
    public float jump_force = 300.0f;

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
            rigidBody.AddForce(new Vector2(0.0f,jump_force)); 
	}
}
