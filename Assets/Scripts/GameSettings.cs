using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{

    public List<GameObject> playerCreatedTetrominos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("8bit");
    }

    void addTetromino(GameObject g){
        playerCreatedTetrominos.Add(g);
    }

}
