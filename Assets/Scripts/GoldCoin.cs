using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{

    public MasterObject master;

    private float previousTime;
    public float fallTime = .5f;

    void Start(){
        previousTime = 0;
        master = GameObject.Find("master").GetComponent<MasterObject>();
    }

    void Update(){
        if (Time.time - previousTime > fallTime){
            transform.position += new Vector3(0,-1,0);
            previousTime = Time.time;
        }
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

                master.incrementScore();
                Destroy(gameObject);
            }
        }
}


