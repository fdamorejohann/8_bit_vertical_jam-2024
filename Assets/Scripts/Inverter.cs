using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : MonoBehaviour
{

    public MasterObject master;
    public bool inverted = false;

    void Start(){
        master = GameObject.Find("master").GetComponent<MasterObject>();
    }
    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (inverted == false){
                master.upsideRotation();
                inverted = true;

                // Apply the modified center back to the collider
                GetComponent<Collider2D>().offset = new Vector2(GetComponent<Collider2D>().offset.x,0);
                GetComponent<Collider2D>().isTrigger = false;

                master.FillLine(Mathf.RoundToInt(transform.position.y));
            }
        }
    }

}
