using UnityEngine;
using System.Collections;

public class movingIA : MonoBehaviour {
    //si vous pensez pouvoir ameliorer les script, ne vous genez pas
    public int vitesse=3 ;
    private Vector2 velocity = new Vector2 (0,0);
    private int mvt=-1;
    public float saut = 200.0f;
    private float place;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity.x = (Time.deltaTime * vitesse)*mvt;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x+velocity.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Mur")
        {
            var collider = (coll.gameObject.GetComponent<Transform>().position.y) - gameObject.transform.position.y;
            Debug.Log(collider);
            mvt = -mvt;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Mur")
        {
            var height = coll.gameObject.transform.position.y - gameObject.transform.position.y * 2;
            if(height < saut)
            {
                Jump();
            }
        }
    }

    public void Jump()
    {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,saut));
    }
}
