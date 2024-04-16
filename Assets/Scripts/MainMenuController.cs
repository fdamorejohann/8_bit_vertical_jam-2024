using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
	// Variables
	public Button play;
	public Button editor;
	public Button quit;
	public GameObject gameModePanel;
	public Button[] gameModes;
	void Start()
	{
		// load scenes
		play.onClick.AddListener(() => { gameModePanel.SetActive(!gameModePanel.activeSelf); });
		editor.onClick.AddListener(() => { SceneManager.LoadScene("Editor"); });
		quit.onClick.AddListener(() => { Application.Quit(); });

		// load game modes
		for (int i = 0; i < gameModes.Length; i++)
		{
			// PlayerPrefs.SetInt("gamemode", i);// playerPref -> gameMode -> [i] -> onClick -> LoadScene -> 8bit
			gameModes[i].onClick.AddListener(() => { SceneManager.LoadScene("8bit"); });
		}
	}
}