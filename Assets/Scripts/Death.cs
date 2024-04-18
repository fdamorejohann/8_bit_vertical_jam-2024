using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Button play;
    public Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(() => {
            Time.timeScale = 1;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
         });
        mainMenu.onClick.AddListener(() => {Time.timeScale = 1; SceneManager.LoadScene("MainMenu"); });
    }


}
