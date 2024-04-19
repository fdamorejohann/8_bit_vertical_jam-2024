using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWalker : MonoBehaviour
{
    public GameObject [] settings;
    public GameObject [] selectedSettings;

    public int currentSelectedOption;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectedOption = 0;

        foreach (GameObject go in selectedSettings)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in settings)
        {
            go.SetActive(true);
        }

        settings[currentSelectedOption].SetActive(false);
        selectedSettings[currentSelectedOption].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            if (currentSelectedOption > 0){
                settings[currentSelectedOption].SetActive(true);
                selectedSettings[currentSelectedOption].SetActive(false);
                currentSelectedOption --;
                Debug.Log("setting option " + currentSelectedOption);
                settings[currentSelectedOption].SetActive(false);
                selectedSettings[currentSelectedOption].SetActive(true);
            }
        }

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            if (currentSelectedOption < settings.Length - 1 ){
                settings[currentSelectedOption].SetActive(true);
                selectedSettings[currentSelectedOption].SetActive(false);
                currentSelectedOption ++;
                Debug.Log("setting option " + currentSelectedOption);
                settings[currentSelectedOption].SetActive(false);
                selectedSettings[currentSelectedOption].SetActive(true);
            }
        }

        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)){
            string s = selectedSettings[currentSelectedOption].GetComponent<SelectedButtonScene>().scene;
            SceneManager.LoadScene(s);
        }

    }
}
