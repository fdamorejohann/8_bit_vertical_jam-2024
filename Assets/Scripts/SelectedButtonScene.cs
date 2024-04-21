using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedButtonScene : MonoBehaviour
{

    public string scene;

    public bool isScene;

    public bool isSubMenu;

    public bool isDifficultySetting;

    public MenuWalker menu;

    public GameSettings gameSettings;

    public GameObject subMenu;

    public string difficulty;


    void Start(){
    }


    public void enact(){
        if (isScene){
            Debug.Log("loading scene!");
            SceneManager.LoadScene(scene);
        }
        else if (isSubMenu){
            menu = GameObject.Find("menu").GetComponent<MenuWalker>();
            menu.loadSubMenu(subMenu);
        }
        else if (isDifficultySetting){
            gameSettings = GameObject.Find("gameSettings").GetComponent<GameSettings>();
            gameSettings.difficulty = difficulty;

            SceneManager.LoadScene(scene);
        }
    }

}
