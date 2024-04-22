using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterObject : MonoBehaviour
{

    public bool dead = false;
    public GameObject[] Tetrominoes;
    [SerializeField] private GameObject gameOverPanel;
    public GameObject Background;
    public GameObject Player;
    public GameObject deathBar;
    public GameObject squarePrefab;

    public string difficulty;

    public AudioSource mainMusic;
    public AudioSource invertedMainMusic;
    public AudioSource deathSound;
    public AudioSource gotGoldAudio;

    public AudioSource inversionAudio;

    public GameObject box;

    public float riseTime = .0002f;
    public float score;
    public float fallTime = .25f;
    public bool destruction = false;
    public bool mapInverted = false;
    public int bottomHeight = 0;
    public GameObject currentBlock;
    public int rows;
    public int columns;
    public bool allowNewTetromino = true;
    public (Transform, bool)[,] grid; // = new (Transform, bool)[rows, columns];
    public GameObject cam;
    public int incrementor;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public float elapsedTime;
    public float timer;
    public bool isRKeyPressed;

    public float scoreIncrementer = 10;

    public float scoreIncrementerIncrementer = 10;

    public scoreMaster ScoreMaster;

    public GameObject[] countDowns;

    void Start()
    {
        Time.timeScale = 0;
        setDifficulty();

        gameOverPanel.SetActive(false);
        score = 0;

        StartCoroutine(CountDown());

        deathBar.GetComponent<DeathBar>().riseTime = riseTime;


        grid = new (Transform, bool)[rows, columns];

        GameObject [] newArray = new GameObject [Tetrominoes.Length + GameObject.Find("gameSettings").transform.childCount];
        for (int i = 0; i < Tetrominoes.Length; i++)
        {
            newArray[i] = Tetrominoes[i];
        }
        int l = 0;
        foreach (Transform child in  GameObject.Find("gameSettings").transform)
        {
            newArray[Tetrominoes.Length + l] = child.gameObject;
            l += 1;
        }

        Tetrominoes = newArray;
        NewTetromino();


        initialPosition = transform.position;

        StartCoroutine(IncreaseScoreCoroutine());






    }

    public void setDifficulty(){
        difficulty = GameObject.Find("gameSettings").GetComponent<GameSettings>().difficulty;

        if (difficulty == "easy"){
            riseTime = 14;
        }
        else if (difficulty == "medium"){
            riseTime = 8;
        }
        else if (difficulty == "hard"){
            riseTime = 5;
        }
        else{
            riseTime = 10;
        }

        if (difficulty == "easy"){
            fallTime = .3f;
        }

        if (difficulty == "medium"){
            fallTime = .2f;
        }

        if (difficulty == "hard"){
            fallTime = .1f;
        }

        if (difficulty == "easy"){
            scoreIncrementerIncrementer = 10;

        }

        if (difficulty == "medium"){
            scoreIncrementerIncrementer = 20;
        }

        if (difficulty == "hard"){
            scoreIncrementerIncrementer = 30;
        }

        scoreIncrementer = scoreIncrementerIncrementer;
    }

    IEnumerator IncreaseScoreCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            incrementScore(scoreIncrementer); // Update the score text
        }
    }

    public string getDifficulty(){
        return difficulty;
    }


    // Update is called once per frame
    void Update()
    {


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

    }

    IEnumerator CountDown(){
        foreach(GameObject obj in countDowns)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in countDowns)
        {
            obj.SetActive(true);
            yield return new WaitForSecondsRealtime(.7f);
            obj.SetActive(false);

        }
        Time.timeScale = 1;
        float clipLength = mainMusic.clip.length;

        // Generate a random time within the duration of the audio clip
        float randomTime = UnityEngine.Random.Range(0f, clipLength);


        mainMusic.Play();
        invertedMainMusic.Play();
        invertedMainMusic.volume = 0;
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

    public void incrementIncrementor(){
        gotGoldAudio.Play();
        scoreIncrementer += scoreIncrementerIncrementer;
    }
    public void incrementScore(float n){
        score += n;
        ScoreMaster.presentScore((int)score);
    }

    public void death(){
        if (!dead){
            mainMusic.Pause();
            deathSound.Play();
            dead = true;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(restart());
        }

    }

    IEnumerator restart(){
        Debug.Log("co routine started");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("timescale");
        Time.timeScale = 1;
        Debug.Log("loading scene");
        SceneManager.LoadScene("Menu");
        Debug.Log("finished loading scene");
    }


    public void upsideRotation(){
        inversionAudio.Play();
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
            mainMusic.volume = 0;
            invertedMainMusic.volume = 0.05f ;
            Debug.Log("setting ap inverted to true "+ mapInverted);
            //cam.transform.rotation = Quaternion.Euler(0f, 0f, 180);
            //box.transform.rotation = Quaternion.Euler(0f, 0f, 180);
            Background.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1); // Set to white
            mapInverted = true;
        }
        else{
            mainMusic.volume = 0.05f;
            invertedMainMusic.volume = 0;
            Debug.Log("setting ap inverted to false "+ mapInverted);
            //cam.transform.rotation = Quaternion.Euler(0f, 0f, 0);
           // box.transform.rotation = Quaternion.Euler(0f, 0f, 0);
            Background.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1); // Set to black
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

        New.GetComponent<TetrisBlock>().asleep = false;

        currentBlock = New;


        if (mapInverted == true){
            foreach (Transform children in New.transform){

                children.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); // Set to opaque black
            }
        }
    }

    public int getInversion(){
       // Debug.Log("map inversion is " + mapInverted);
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
            if (roundedY >= transform.position.y){
                death();
            }
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
