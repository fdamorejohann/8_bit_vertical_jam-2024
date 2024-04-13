using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    private float fallTime = 1;
    public static int height = 20;
    public static int width = 10;

    public MasterObject master;

    public Vector3 rotationPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.position += new Vector3 (-1,0,0);
            if(!ValidMove()){
                transform.position -= new Vector3 (-1,0,0);
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.position += new Vector3 (1,0,0);
            if(!ValidMove()){
                transform.position -= new Vector3 (1,0,0);
            }
        }


        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 : fallTime)){
            transform.position += new Vector3(0,-1,0);
            previousTime = Time.time;
            if(!ValidMove()){
                transform.position -= new Vector3 (0,-1,0);
                master.AddToGrid(transform);
                master.NewTetromino();
                this.enabled = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),90);
            if(!ValidMove()){
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),-90);
            }

        }

    }

    bool ValidMove(){
        foreach (Transform children in transform){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
                return false;
            }
            if (master.checkGrid(roundedX, roundedY)){
                return false;
            }
        }

        return true;
    }
}
