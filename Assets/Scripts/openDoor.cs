using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {

    public string dest = "";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Animation anim = gameObject.GetComponentInChildren<Animation>();
        if (anim.enabled)
        {
            anim.Play();
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Animation anim = gameObject.GetComponentInChildren<Animation>();
            anim.enabled = true;
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
            Animation anim = gameObject.GetComponentInChildren<Animation>();
            anim.Stop();
            anim.enabled = false;
            SpriteRenderer[] sprs = anim.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spr in sprs)
            {
                spr.enabled = false;
            }
        }
    }


    void open()
    {
        Application.LoadLevel(dest);
    }
}
