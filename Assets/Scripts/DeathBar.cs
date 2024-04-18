using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBar : MonoBehaviour
{
    public bool disableIncrementing;
    public bool incrementing = false;
	public MasterObject master;

    public int bottomHeight;
    public int currentHeight = -1;

    public float riseTime;

    public float startTime;

    public bool setDestruction;

    public float startYPosition;

    public int incrementor;

    public float elapsedTime;

    public Vector3 initialPosition;

    public Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        incrementor = 1;
        startTime = Time.time;
        master = GameObject.Find("master").GetComponent<MasterObject>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        updateLocation();
        // targetPosition = new Vector3(initialPosition.x , initialPosition.y + 1, initialPosition.z);
        // elapsedTime += Time.deltaTime;
        // transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / riseTime));
        // //transform.position += new Vector3(0,1/(riseTime /incrementor), 0);
        // // transform.position = new Vector3(transform.position.x, startYPosition + (Time.time - startTime) / (riseTime / incrementor) ,0);

        if (setDestruction == true){
            bottomHeight = Mathf.RoundToInt(transform.position.y);
            if (bottomHeight > currentHeight){
                currentHeight = bottomHeight;
                Debug.Log("removing line");
                if (bottomHeight > 1){
                    master.RemoveLine(bottomHeight - 1);
                    master.RemoveLine(bottomHeight - 2);
                }
            }
        }

    }

    public void updateLocation(){
        targetPosition = new Vector3(initialPosition.x , initialPosition.y + 1, initialPosition.z);
        elapsedTime += Time.deltaTime;
        transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / riseTime));

        if (elapsedTime >  riseTime){
            elapsedTime = 0;
            initialPosition = transform.position;
        }
    }

            // if (transform.position.y < Player.transform.position.y - 7){
            //     transform.position += new Vector3(0, .5f,0);

            // }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("KILLING!");
                master.death();
                var collider = GetComponent<Collider2D>();
                Destroy(collider);
                // Apply the modified center back to the collider
                //GetComponent<Collider2D>().isTrigger = false;

        }
    }
}
