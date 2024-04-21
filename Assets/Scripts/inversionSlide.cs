using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inversionSlide : MonoBehaviour
{

    public bool startInversion;
    public MasterObject master;

    public GameObject triggeredInverter;
    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("master").GetComponent<MasterObject>();
    }

    // Update is called once per frame
    void Update()
    {

        if (startInversion){
            Debug.Log("master inversion is " + master.getInversion());
            transform.position += new Vector3(0,.3f * master.getInversion(), 0);
            if (transform.localPosition.y > 3){
                Debug.Log(" is " + transform.localPosition.y);
                startInversion = false;
                transform.localPosition = new Vector3(0,2 , 0);
            }
            if (transform.localPosition.y < -27){
                Debug.Log("stopping at " + transform.localPosition.y);
                startInversion = false;
                transform.localPosition = new Vector3(0,-25 , 0);
            }

            if (Mathf.Abs(transform.position.y - triggeredInverter.transform.position.y) < .5f){
                Destroy(triggeredInverter);
            }
        }

    }
}
