using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterObject : MonoBehaviour
{

    public bool destruction = false;
    public GameObject[] Tetrominoes;

    public float previousTime;
    public float riseTime = .0002f;

    public float score;

    public float fallTime = .25f;
    public Vector3 newPosition;

    public GameObject Background;

    public GameObject Player;

    public GameObject deathBar;
    public GameObject topDeathBar;
    public GameObject squarePrefab;
    public bool mapInverted = false;

    public int bottomHeight = 0;
    public bool invertingMap = false;

    public GameObject currentBlock;

    public bool spawn;

    public int rows;
    public int columns;

    public bool allowNewTetromino = true;

    //public Transform[,] grid = new Transform[10,40];
    // Start is called before the first frame update
    public (Transform, bool)[,] grid; // = new (Transform, bool)[rows, columns];

    public (Transform, bool)[,] secondGrid;

    public GameObject cam;

    public int incrementor;

    public Vector3 initialPosition;
    public Vector3 targetPosition;

    public float elapsedTime;

    [SerializeField] private GameObject gameOverPanel;

    public float timer;
    public bool isRKeyPressed;
    //public Tuple<Transform, bool>[,] grid = new Tuple<Transform, bool>[10, 40];

    void Start()
    {
        gameOverPanel.SetActive(false);
        score = 0;
        grid = new (Transform, bool)[rows, columns];
        secondGrid = new (Transform, bool)[rows, columns];
        NewTetromino();
        previousTime = Time.time;
        deathBar.GetComponent<DeathBar>().riseTime = riseTime;
        topDeathBar.GetComponent<DeathBar>().riseTime = riseTime;

        initialPosition = transform.position;


    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - Player.transform.position.y < 10){
            incrementor = 2;
        }
        else{
            incrementor = 1;
        }
        deathBar.GetComponent<DeathBar>().incrementor = incrementor;
        topDeathBar.GetComponent<DeathBar>().incrementor = incrementor;

        updateLocation();

        if (Input.GetKeyDown(KeyCode.R))
                {
                    isRKeyPressed = true;
                    timer = 0f;
                }

                if (Input.GetKey(KeyCode.R))
                {
                    if (isRKeyPressed)
                    {
                        timer += Time.deltaTime;
                        if (timer >= 1)
                        {
                            // R key has been held for more than 1 second
                            Debug.Log("R key held for more than 1 second");
                            string currentSceneName = SceneManager.GetActiveScene().name;
                            SceneManager.LoadScene(currentSceneName);
                            // Do whatever you need to do here
                        }
                    }
                }

                if (Input.GetKeyUp(KeyCode.R))
                {
                    isRKeyPressed = false;
                    timer = 0f; // Reset timer when key is released
                }

       // transform.position = new Vector3(transform.position.x, 19 + (Time.time - previousTime) / (riseTime / incrementor)  ,0);
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

    public void incrementScore(){
        score += 100;
    }

    public void death(){
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void restart(){

    }


    public void upsideRotation(){
        Debug.Log("in upside rotation, current inversion status is "+ mapInverted);
        Destroy(currentBlock);
        Player.GetComponent<Rigidbody2D>().gravityScale = Player.GetComponent<Rigidbody2D>().gravityScale * -1f;
        if (mapInverted == false){
            Debug.Log("in upside rotation,setting to 255 "+ mapInverted);
            Background.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1); // Set to black
        }
        else{
            Debug.Log("in upside rotation,setting to 0 "+ mapInverted);
            Background.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1); // Set to white
        }

        if (mapInverted == false){
            Debug.Log("setting ap inverted to true "+ mapInverted);
            //cam.transform.rotation = Quaternion.Euler(0f, 0f, 0);
            Background.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1); // Set to black
            mapInverted = true;
        }
        else{
            Debug.Log("setting ap inverted to false "+ mapInverted);
           // cam.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Background.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1); // Set to white
            mapInverted = false;
        }



        for (int k = 0; k < grid.GetLength(0); k++){
            for (int l = 0; l < grid.GetLength(1); l++){
                if (grid[k,l].Item1 != null){
                    if (mapInverted == true){
                      //  Debug.Log("setting to black...");
                        grid[k,l].Item1.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); // Set to opaque black
                    }
                    else{
                        grid[k,l].Item1.gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1); // Set to opaque black
                    }
                }
            }

        }
        NewTetromino();
    }


    public void NewTetromino(){
        if (allowNewTetromino == false){
            return;
        }

        Vector3 spawnLocation = new Vector3 (transform.position.x, Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(Tetrominoes[UnityEngine.Random.Range(0,Tetrominoes.Length)], spawnLocation, Quaternion.identity);
        New.GetComponent<TetrisBlock>().master = this;
        //disableChildrenCollider(New.transform);
        New.GetComponent<TetrisBlock>().inverted = false;//mapInverted;
        New.GetComponent<TetrisBlock>().fallTime = fallTime;

        currentBlock = New;


        if (mapInverted == true){
            foreach (Transform children in New.transform){

                children.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); // Set to opaque black
            }
        }
    }

    public int getInversion(){
        return 1;
        if(mapInverted == true){
            return -1;
        }
        return 1;
    }

    public void disableChildrenCollider(Transform t){
        foreach (Transform children in t){
            children.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void disableCollider(Transform t){
        t.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableCollider(Transform t){
        foreach (Transform children in t){
            children.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    // Function to invert the original 2D array
    private void InvertArray()
    {
        Debug.Log("Inverting Array");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns / 2; j++)
            {
                // Swap elements across the vertical axis
                var temp = grid[i, j];
                grid[i, j] = grid[i, columns - 1 - j];
                grid[i, columns- 1 - j] = temp;
            }
        }

    }


    public void InvertMap(){
       // InvertArray();
        for (int k = 0; k < grid.GetLength(0); k++){
            for (int l = 0; l < grid.GetLength(1); l++){
                if (grid[k,l].Item1 != null){
                    Debug.Log("found collider");
                    if (mapInverted == true){
                        disableCollider(grid[k,l].Item1);
                    //grid[k,l].Item2 = mapInverted;
                    Debug.Log("disabled collider");
                    }
                    else{
                        Destroy(grid[k,l].Item1.gameObject);
                        grid[k, l].Item1 = null;
                    }
                }
                else{
                    GameObject New = Instantiate(squarePrefab, new Vector3(k,l,0), Quaternion.identity);
                    grid[k, l] = (New.transform, mapInverted);
                    if (mapInverted == true){
                      //  Debug.Log("setting to black...");
                        New.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); // Set to opaque black
                    }
                    else{
                        New.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1); // Set to opaque black
                    }
                }
            }
        }
        if (mapInverted == true){
            Background.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1); // Set to black
        }
        else{
            Background.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1); // Set to white
        }

        if (mapInverted == true){
            Player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1); // Set to black
        }
        else{
            Player.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1);  // Set to white
        }

       // Player.GetComponent<Rigidbody2D>().gravityScale = Player.GetComponent<Rigidbody2D>().gravityScale * -1f;

    }

    public void DestroyGrid(Transform t){
        Debug.Log("destroying grid values");
        foreach (Transform children in t){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if(grid[roundedX,roundedY].Item1 != null){
                Destroy(grid[roundedX,roundedY].Item1.gameObject);
                grid[roundedX, roundedY].Item1 = null;
            }
        }

    }

    public void RemoveLine(int line){
        Debug.Log(" Removing line...");
        bottomHeight = line;
        for (int l = 0; l < grid.GetLength(0); l++){
            Debug.Log("checking at " + l + " " + line);
            if (grid[l,line].Item1 != null){
                Debug.Log("destroyign at " + l + " " + line);
                Destroy(grid[l,line].Item1.gameObject);
                Debug.Log("setting to null at " + l + " " + line);
                grid[l,line].Item1 = null;
            }
        }
    }

    public void FillLine(int line){
        Debug.Log(" filling line...");
        bottomHeight = line;
        for (int l = 0; l < grid.GetLength(0); l++){
            Debug.Log("checking at " + l + " " + line);
            if (grid[l,line].Item1 == null){
                GameObject New = Instantiate(squarePrefab, new Vector3(l,line,5), Quaternion.identity);
                grid[l,line] = (New.transform, false);
            }
        }
    }

    public int returnBottomHeight(){
        return bottomHeight;
    }

    public void AddToGrid(Transform t){

        // if (mapInverted == true){
        //     DestroyGrid(t);
        // }
        foreach (Transform children in t){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            //Debug.Log("setting grid at " + roundedX + ", " + roundedY);
            grid[roundedX, roundedY].Item1 = children;
            grid[roundedX,roundedY].Item2 = false;
        }
        enableCollider(t);

        // if (mapInverted == false){
        //     enableCollider(t);
        // }

    }

    public bool checkGrid(int x, int y){
        x = x % rows;
        y = y % columns;
        if (y > columns){
            return false;
        }
        if (y < bottomHeight){
            return true;
        }
        if (destruction == true){
            return false;
        }
        if (grid[x,y].Item1 != null &&  grid[x,y].Item2 == false){
            return true;
        }
        return false;

    }
}
