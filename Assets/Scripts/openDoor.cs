using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {


    private bool displayText;
    public float animTimer = 2.0f;
    public string dest = "";

	// Use this for initialization
	void Start () {
        displayText = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void onTriggerEnter2D(Collision2D coll)
    {

    }

    void onTriggerStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if (Input.GetKey("up"))
            {
                open();
            }
        }
    }

    void onTriggerExit2D(Collision2D coll)
    {

    }


    void open()
    {

    
    }
}
