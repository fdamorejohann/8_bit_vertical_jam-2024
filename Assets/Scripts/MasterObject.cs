using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterObject : MonoBehaviour
{

    public bool destruction = false;
    public GameObject[] Tetrominoes;

    public float previousTime;
    public float riseTime = 10f;

    public bool incrementing = false;

    public Vector3 newPosition;

    public GameObject Background;
    public Transform[,] grid = new Transform[100,200];
    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
        previousTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if( Time.time - previousTime > riseTime && incrementing == false){
            incrementing = true;
            newPosition = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
        }

        if ( incrementing == true ){
            transform.position += new Vector3 (0,.00002f,0);
            Background.GetComponent<Transform>().position += new Vector3(0,.00002f,0);

            if (transform.position.y == newPosition.y){
                incrementing = false;
                previousTime = Time.time;
            }
        }
    }


    public void NewTetromino(){
        GameObject New = Instantiate(Tetrominoes[Random.Range(0,Tetrominoes.Length)], transform.position, Quaternion.identity);
        New.GetComponent<TetrisBlock>().master = this;
    }

    public void DestroyGrid(Transform t){
        Debug.Log("destroying grid values");
        foreach (Transform children in t){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if(grid[roundedX,roundedY] != null){
                Destroy(grid[roundedX,roundedY].gameObject);
                grid[roundedX, roundedY] = null;
            }
        }
        Destroy(t.gameObject);
    }

    public void AddToGrid(Transform t){

        if (destruction == true){
            DestroyGrid(t);
            return;
        }
        foreach (Transform children in t){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;
        }
    }

    public bool checkGrid(int x, int y){
        if (destruction == true){
            return false;
        }
        if (grid[x,y] != null){
            return true;
        }
        return false;

    }
}
