using UnityEngine;
using System.Collections;

public class bouger : MonoBehaviour {
    //si vous pensez pouvoir ameliorer les script, ne vous genez pas
    public int vitesse=3 ;
    private Vector2 velocity = new Vector2(0, 0);
    private Vector2 velocity2 = new Vector2(0, 0);

    private int mvt=-1;
    private float place;
    private Transform prout;
    private GameObject trans;
    private Transform temp;
    private int p = 0;
    private float local;


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        trans = GameObject.Find("invisible"+p);
        Debug.Log(trans);
        velocity.x = (Time.deltaTime * vitesse)*mvt;
        this.gameObject.transform.position = new Vector2(gameObject.transform.position.x +velocity.x, gameObject.transform.position.y);
        miniJump(this.gameObject, trans);
    }
 
   void miniJump(GameObject temp, GameObject enemi)
    {
        
        if (temp.transform.position.x > enemi.transform.position.x - 0.1 && temp.transform.position.x < enemi.transform.position.x + 0.1 && temp.transform.position.y < 0.4  )

        {
            //temp.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + velocity.x*3 );
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 210.0f));
            p++;
            if(p>1)
            {
                p = 0;
            }
        }
    }
        

    

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Mur")
        {
            mvt = -mvt;
        }
    }


}
