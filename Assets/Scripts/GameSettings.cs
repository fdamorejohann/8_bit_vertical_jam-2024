using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{

    public List<GameObject> playerCreatedTetrominos = new List<GameObject>();

    public string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //SceneManager.LoadScene("8bit");
    }

    public void addTetromino(GameObject g){
        playerCreatedTetrominos.Add(g);
        g.transform.parent = transform.parent;
    }

}
