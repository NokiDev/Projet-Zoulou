using UnityEngine;
using System.Collections;

public class CharacterLife : MonoBehaviour {
	public int healthMax ;
	private int currentHealth ;
	private bool death ;
	public int damage;
	// Use this for initialization
	void Start () {
		death = false;
		healthMax=currentHealth;
	
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (healthMax < 0) 
		{
			death = true;
		} 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag != "decor")
			currentHealth = currentHealth - damage;
	}
}
