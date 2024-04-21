using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : MonoBehaviour
{

    public MasterObject master;
    public bool inverted = false;

    private float previousTime;
    public float fallTime = .2f;

    public float inversion;

    public inversionSlide inversionSlider;

    void Start(){
        master = GameObject.Find("master").GetComponent<MasterObject>();
        inversion = master.getInversion();

        inversionSlider = GameObject.Find("inversionSlider").GetComponent<inversionSlide>();

    }
    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen

    void Update(){
        if (Time.time - previousTime > fallTime){
            transform.position += new Vector3(0,-1 * inversion,0);
            previousTime = Time.time;
        }

        // if(inversion != master.getInversion()){
        //     Destroy(gameObject);
        // }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("INVERTTING!!");
            if (inverted == false){
                master.upsideRotation();
                inverted = true;

                // Apply the modified center back to the collider
                GetComponent<Collider2D>().offset = new Vector2(GetComponent<Collider2D>().offset.x,0);
                //GetComponent<Collider2D>().isTrigger = false;

                inversionSlider.triggeredInverter = gameObject;
                inversionSlider.startInversion = true;


                //master.FillLine(Mathf.RoundToInt(transform.position.y));
            }
        }
    }

}
