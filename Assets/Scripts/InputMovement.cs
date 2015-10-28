using UnityEngine;
using System.Collections;

public class InputMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("left"))
            gameObject.transform.position += Vector3.left* 0.1f;
        if (Input.GetKey("right"))
            gameObject.transform.position += Vector3.right *0.1f;
	}
}
