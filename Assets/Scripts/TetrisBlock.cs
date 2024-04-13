using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    // Variables
    private float previousTime;
    private float fallTime = .5f;
    public static int height = 20;
    public static int width = 10;
    public Vector3 mousePosition;
    public MasterObject master;
    public Vector3 rotationPoint;
    void Update()
    {
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // int roundedX = Mathf.RoundToInt(mousePosition.x);
        // int roundedY = Mathf.RoundToInt(mousePosition.y);

        // if (ValidMove(mousePosition.x, mousePosition.y)){
        //     transform.position = new Vector3(roundedX,transform.position.y,transform.position.z);
        // }
        
        // If the left arrow key is pressed
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            // Move the block left
            transform.position += new Vector3 (-1,0,0);
            // If the move is not valid
            if(!ValidMove()){
                // Move the block right
                transform.position -= new Vector3 (-1,0,0);
            }
        }
        // If the right arrow key is pressed
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            // Move the block right
            transform.position += new Vector3 (1,0,0);
            // If the move is not valid
            if(!ValidMove()){
                // Move the block left
                transform.position -= new Vector3 (1,0,0);
            }
        }
        // If the down arrow key is pressed
        if ((Time.time - previousTime) > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 : fallTime)){
            // Move the block down
            transform.position += new Vector3(0,-1,0);
            // Set the previous time to the current time
            previousTime = Time.time;
            // If the move is not valid
            if(!ValidMove()){
                // Move the block up
                transform.position -= new Vector3 (0,-1,0);
                // Add the block to the grid
                master.AddToGrid(transform);
                // Check if the grid is full
                master.NewTetromino();
                // Destroy the row
                this.enabled = false;
            }
        }
        // If the up arrow key is pressed
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            // Rotate the block
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),90);
            // If the move is not valid
            if(!ValidMove()){
                // Rotate the block
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),-90);
            }
        }
    }
    // Function to check if the move is valid
    bool ValidMove(){
        // For each child in the transform
        foreach (Transform children in transform){
            // Round the x and y values
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            // If the x value is less than 0 or greater than or equal to the width
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
                return false;
            }
            // If the grid value is not null
            if (master.checkGrid(roundedX, roundedY)){
                return false;
            }
        }
        // Return true
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