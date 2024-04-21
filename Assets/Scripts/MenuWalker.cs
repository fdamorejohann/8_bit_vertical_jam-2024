using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWalker : MonoBehaviour
{
    public GameObject [] settings;
    public GameObject [] selectedSettings;

    public int currentSelectedOption;

    public SubMenu subMenu;

    public AudioSource move;
    public AudioSource select;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        initialize();
    }

    public void initialize(){

        Debug.Log("setting to awake");
        subMenu.gameObject.SetActive(true);

        currentSelectedOption = 0;

        foreach (GameObject go in subMenu.selectedSettings)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in subMenu.settings)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in subMenu.descriptions){
            go.SetActive(false);
        }

        subMenu.settings[currentSelectedOption].SetActive(false);
        subMenu.selectedSettings[currentSelectedOption].SetActive(true);

        if (subMenu.descriptions.Length > 0){
            subMenu.descriptions[currentSelectedOption].SetActive(true);
        }

    }


    public void loadSubMenu(GameObject g){
        subMenu.gameObject.SetActive(false);
        subMenu = g.GetComponent<SubMenu>();
        initialize();
    }

    // Update is called once per frame
    void Update()
    {


		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
            if (currentSelectedOption > 0){
                move.Play();
                subMenu.settings[currentSelectedOption].SetActive(true);
                subMenu.selectedSettings[currentSelectedOption].SetActive(false);
                if (subMenu.descriptions.Length > 0){
                    subMenu.descriptions[currentSelectedOption].SetActive(false);
                }
                currentSelectedOption --;
                Debug.Log("setting option " + currentSelectedOption);
                subMenu.settings[currentSelectedOption].SetActive(false);
                subMenu.selectedSettings[currentSelectedOption].SetActive(true);
                if (subMenu.descriptions.Length > 0){
                    subMenu.descriptions[currentSelectedOption].SetActive(true);
                }
            }
        }

		if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            move.Play();
            if (currentSelectedOption < settings.Length - 1 ){
                subMenu.settings[currentSelectedOption].SetActive(true);
                subMenu.selectedSettings[currentSelectedOption].SetActive(false);
                if (subMenu.descriptions.Length > 0){
                    subMenu.descriptions[currentSelectedOption].SetActive(false);
                }
                currentSelectedOption ++;
                Debug.Log("setting option " + currentSelectedOption);
                subMenu.settings[currentSelectedOption].SetActive(false);
                subMenu.selectedSettings[currentSelectedOption].SetActive(true);
                if (subMenu.descriptions.Length > 0){
                    subMenu.descriptions[currentSelectedOption].SetActive(true);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space)){
            select.Play();
            subMenu.selectedSettings[currentSelectedOption].GetComponent<SelectedButtonScene>().enact();
            //string s = subMenu.selectedSettings[currentSelectedOption].GetComponent<SelectedButtonScene>().scene;
            //SceneManager.LoadScene(s);
        }

    }



}
