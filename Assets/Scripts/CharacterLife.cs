using UnityEngine;
using System.Collections;

public class CharacterLife : MonoBehaviour {
	public int healthMax ;
	private int currentHealth ;
	public int damage;
	// Use this for initialization
	void Start () {
		currentHealth=healthMax;
	
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (currentHealth <= 0) 
		{
			Destroy(gameObject);
		} 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "hostile")
			currentHealth = currentHealth - damage;
	}
}
