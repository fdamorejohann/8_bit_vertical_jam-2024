using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{

    public MasterObject master;

    private float previousTime;
    public float fallTime = .2f;

    public float inversion;

    void Start(){
        previousTime = 0;
        master = GameObject.Find("master").GetComponent<MasterObject>();
        inversion = master.getInversion();
    }

    void Update(){
        if (Time.time - previousTime > fallTime){
            transform.position += new Vector3(0,-1 * inversion,0);
            previousTime = Time.time;
        }
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
                master.incrementIncrementor();
                Destroy(gameObject);
            }
        }
}


