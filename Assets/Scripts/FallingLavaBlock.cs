using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLavaBlock : MonoBehaviour
{
    private float previousTime;
    private float spawnTime;
    public float fallTime = .5f;
    public  int height = 20;
    public  int width = 10;

    public Vector3 mousePosition;

    public MasterObject master;

    public Vector3 rotationPoint;

    public bool inverted;
    // Start is called before the first frame update
    void Start(){
        spawnTime = Time.time;
        master = GameObject.Find("master").GetComponent<MasterObject>();
    }


    // Update is called once per frame
    void Update()
    {

        if (Time.time - spawnTime > 40){
            Destroy(gameObject);
        }

        if (Time.time - previousTime > fallTime){
            transform.position += new Vector3(0,-1,0);
            previousTime = Time.time;
            if(!ValidMove()){
                master.DestroyGrid(transform);
            }
        }

        if (transform.position.y < 0){
            Destroy(gameObject);
        }



    }

    bool ValidMove(){
        foreach (Transform children in transform){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0){
                return false;
            }
            if (master.checkGrid(roundedX, roundedY)){
                return false;
            }
        }

        return true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("KILLING!");
                master.death();
                var collider = GetComponent<Collider2D>();
                Destroy(collider);
        }
    }

}
