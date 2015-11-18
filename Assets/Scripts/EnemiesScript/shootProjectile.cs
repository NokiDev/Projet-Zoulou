using UnityEngine;
using System.Collections;

public class shootProjectile : MonoBehaviour {

    public GameObject projectile; //Projectile shot prefab (fire ball, arrow etc..)
    // Use this for initialization
    public float shotDelay = 1.0f;
    private float shotTimer;
	void Start ()
    {
        shotTimer = shotDelay;   
	}
	
	// Update is called once per frame
	void Update ()
    {
        shotTimer += Time.deltaTime;
        if(shotTimer >= shotDelay)
        {
            shotTimer = 0.0f;
            Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        }
	}
}
