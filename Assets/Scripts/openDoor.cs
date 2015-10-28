using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {


    private bool displayText;
    public float animTimer = 0.0f;
    public string dest = "";

	// Use this for initialization
	void Start () {
        displayText = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(displayText)
        {
            Animation spr = gameObject.GetComponentInChildren<Animation>();
            spr.enabled = true;
            spr.Play();
        }
        else
        {
            Animation spr = gameObject.GetComponentInChildren<Animation>();
            
            spr.enabled = false;
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            displayText = true;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if (Input.GetKey("up"))
            {
                open();
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            displayText = false;
        }
    }


    void open()
    {
        Application.LoadLevel(dest);
    }
}
