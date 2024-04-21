using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    public float fallTime = .5f;
    public static int height = 20;
    public static int width = 10;

    public Vector3 mousePosition;

    public MasterObject master;

    public Vector3 rotationPoint;

    public bool inverted;
    // Start is called before the first frame update
    void Start()
    {

        // if (master.difficulty == "easy"){
        //     fallTime = .8f;
        // }

        // if (master.difficulty == "medium"){
        //     fallTime = .5f;
        // }

        // if (master.difficulty == "medium"){
        //     fallTime = .2f;
        // }

    }

    // Update is called once per frame
    void Update()
    {

        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // int roundedX = Mathf.RoundToInt(mousePosition.x);
        // int roundedY = Mathf.RoundToInt(mousePosition.y);

        // if (ValidMove(mousePosition.x, mousePosition.y)){
        //     transform.position = new Vector3(roundedX,transform.position.y,transform.position.z);
        // }


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

            if (roundedX < 0 || roundedX >= width || roundedY < 0){
                return false;
            }
            if (master.checkGrid(roundedX, roundedY)){
                return false;
            }
        }

        return true;
    }



    // bool ValidMove(float x, float y){
    //     foreach (Transform children in transform){
    //         Vector3 childLocalPosition = transform.InverseTransformPoint(children.transform.position);

    //         int roundedX = Mathf.RoundToInt(x + children.transform.position.x);
    //         int roundedY = Mathf.RoundToInt(y + children.transform.position.y);

    //         //int roundedX = Mathf.RoundToInt(x);
    //         //int roundedY = Mathf.RoundToInt(y);

    //         Debug.Log("checking " + roundedX + "," + roundedY);

    //         if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
    //             return false;
    //         }
    //         if (master.checkGrid(roundedX, roundedY)){
    //             return false;
    //         }
    //     }

    //     return true;
    // }

}
