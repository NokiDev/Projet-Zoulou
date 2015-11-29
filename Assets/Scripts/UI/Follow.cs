using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject followedEntity;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = followedEntity.transform.position + offset;
	}
}
