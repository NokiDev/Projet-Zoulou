using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject followedEntity;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(followedEntity.transform.position.x, followedEntity.transform.position.y, gameObject.transform.position.z);
	}
}
