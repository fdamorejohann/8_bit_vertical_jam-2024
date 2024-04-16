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


    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("master").GetComponent<MasterObject>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0,.0002f,0);
        bottomHeight = Mathf.RoundToInt(transform.position.y);
        if (bottomHeight > currentHeight){
            currentHeight = bottomHeight;
            Debug.Log("removing line");
            if (bottomHeight != 0){
                master.RemoveLine(bottomHeight - 1);
            }
            master.RemoveLine(bottomHeight);
        }

    }

            // if (transform.position.y < Player.transform.position.y - 7){
            //     transform.position += new Vector3(0, .5f,0);

            // }


}
