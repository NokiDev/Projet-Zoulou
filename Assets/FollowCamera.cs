using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public Vector3 offset; //offset par rapport a la caméra

    private Transform mainCamera; //reference à la position de la Camera

    // Use this for initialization
    void Awake() {
        mainCamera = GameObject.Find("Main Camera").transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = mainCamera.position + offset;
	}
}
