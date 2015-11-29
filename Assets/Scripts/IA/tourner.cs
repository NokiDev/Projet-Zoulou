using UnityEngine;
using System.Collections;

public class tourner : MonoBehaviour {
    private int mvt = 1;
    private Vector2 velocity=new Vector2(0,0);
    private int vitesse = 3;
    private int var = 0;
    private GameObject point;
    public GameObject Feu;
    private double timer = 0;


    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        velocity.x = Time.deltaTime *vitesse * mvt;
        timer += Time.deltaTime;
        point = GameObject.Find("changementPoint"+var);
        this.transform.position = new Vector2(gameObject.transform.position.x + velocity.x, gameObject.transform.position.y);
        changement(this.gameObject, point);
        Debug.Log(timer);
        if(timer >= 2 )
        {
            creation();
            timer = 0;
        }
    }

    void creation()
    {
        GameObject g=Instantiate(Feu, this.gameObject.transform.localPosition, Quaternion.identity) as GameObject;
    }






    void changement(GameObject volant, GameObject checkpoint)
    {
        if (volant.transform.position.x > checkpoint.transform.position.x - 0.1 && volant.transform.position.x < checkpoint.transform.position.x + 0.1)
        {
            mvt = -mvt;
            var++;
            if(var>1)
            {
                var = 0;
            }
        }
    }

}
