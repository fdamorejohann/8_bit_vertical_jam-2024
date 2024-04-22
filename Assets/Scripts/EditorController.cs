using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EditorController : MonoBehaviour
{
	// Variables
	public GameObject squarePrefab; // The square prefab
	public Transform customShapeParent; // The parent of the custom shape
	public int blockCount = 5; // The number of blocks
	public int maxCustomShapes = 7; // The maximum number of blocks
	private Transform fallingSquare; // The square that is falling
	private bool startedFalls = false; // Has it started falling
	private float fallTime; // The timer on when it should fall

	public AudioSource dropBlock;

	public AudioSource saveBlock;

	public AudioSource move;


	public bool hasBlock = false;

	public bool fallingBlock = false;

	void Start()
	{
		// Set the fall time to the current time
		fallTime = Time.time;
		// Set the physics to not start in colliders
		Physics2D.queriesStartInColliders = false;
	}
    IEnumerator reloadMain()
    {
		saveBlock.Play();
        yield return new WaitForSecondsRealtime(1); // Wait for 3 seconds
		Time.timeScale = 1;
		customShapeParent.GetComponent<TetrisBlock>().asleep = true;
		SceneManager.LoadScene("Menu");

    }


	void Update()
	{

		if (hasBlock == false){
			fallingSquare = Instantiate(squarePrefab, new Vector3(4f, 12.5f, 0), Quaternion.identity).transform;
			hasBlock = true;
		}

		// If the user presses the left arrow key
		if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.timeScale == 1)
		{
			move.Play();
			Debug.Log("hit left");
			// raycast to see if the square is touching anything
			RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.left, 1);
			// If the square is not touching anything
			if (hit.collider == null)
			{
				Debug.Log("moving");
				// Move the square left
				fallingSquare.position += new Vector3(-1, 0, 0);
			}
			else{
				Debug.Log("missed");
			}
		}

		// If the user presses the right arrow key
		if (Input.GetKeyDown(KeyCode.RightArrow) && Time.timeScale == 1)
		{
			move.Play();
			// raycast to see if the square is touching anything
			RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.right, 1);
			// If the square is not touching anything
			if (hit.collider == null)
			{
				// Move the square right
				fallingSquare.position += new Vector3(1, 0, 0);
			}
		}

		if (Input.GetKeyUp(KeyCode.DownArrow) && fallingBlock == false && Time.timeScale == 1){
			dropBlock.Play();
			fallingBlock = true;
		}




		if (Time.time - fallTime > .002 && fallingBlock == true && Time.timeScale == 1)
			{
			// Move the square down
			fallingSquare.position += new Vector3(0, -1, 0);
			// Raycast to see if the square is touching anything
			RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.down, 1);
			// If the square is touching something
			if (hit.collider != null)
			{
				fallingBlock  = false;
				hasBlock = false;
				// Decrement the block count
				blockCount--;
				fallingSquare.position = new Vector3(fallingSquare.position.x , Mathf.Ceil(fallingSquare.position.y), 0);
				fallingSquare.parent = customShapeParent.transform;
				// Set the falling square to null
				fallingSquare = null;

			}
			// Set the fall time to the current time
			fallTime = Time.time;
			}
			if (Input.GetKeyUp(KeyCode.Space))
			{
				// add CustomShape to GameSettings
				GameObject.Find("gameSettings").GetComponent<GameSettings>().addTetromino(customShapeParent.gameObject);
				// decrement maxCustomShapes
				maxCustomShapes--;
				// if maxCustomShapes is 0
				if (maxCustomShapes == 0)
				{
					// load the game scene
					//UnityEngine.SceneManagement.SceneManager.LoadScene("8bit");
				}

				GameObject fakeNewShape = Instantiate(customShapeParent.gameObject, customShapeParent.position, Quaternion.identity);
				foreach (Transform child in fakeNewShape.transform)
				{
					// Check if the child has a SpriteRenderer component
					child.GetComponent<SpriteRenderer>().color = new Color(1f, 0.843f, 0f); // Set to black

				}
				// push the custom shape down

				customShapeParent.position = new Vector3(10, 0, 0);

				// set name of new custom shape parent
				customShapeParent.name = "CustomShape" + (5 - maxCustomShapes);
				Time.timeScale = 0;
				DontDestroyOnLoad(customShapeParent.gameObject);
				customShapeParent.position = new Vector3 (0,0,0);
				customShapeParent.parent = GameObject.Find("gameSettings").transform;

				customShapeParent.gameObject.SetActive(false);


				StartCoroutine(reloadMain());

		}

		if (Input.GetKeyUp(KeyCode.Return))
			{
				StartCoroutine(reloadMain());
			}
	}


	// 	// // if the user presses any of the arrow keys
	// 	// if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
	// 	// {
	// 		// If the game has not started falling
	// 		if (!startedFalls)
	// 		{
	// 			// Start the game falling
	// 			startedFalls = true;
	// 		}
	// 		// If there is a falling square
	// 		if (fallingSquare != null)
	// 		{
	// 			// If the user presses the left arrow key
	// 			if (Input.GetKeyDown(KeyCode.LeftArrow))
	// 			{
	// 				// raycast to see if the square is touching anything
	// 				RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.left, 1);
	// 				// If the square is not touching anything
	// 				if (hit.collider == null)
	// 				{
	// 					// Move the square left
	// 					fallingSquare.position += new Vector3(-1, 0, 0);
	// 				}
	// 			}
	// 			// If the user presses the right arrow key
	// 			if (Input.GetKeyDown(KeyCode.RightArrow))
	// 			{
	// 				// raycast to see if the square is touching anything
	// 				RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.right, 1);
	// 				// If the square is not touching anything
	// 				if (hit.collider == null)
	// 				{
	// 					// Move the square right
	// 					fallingSquare.position += new Vector3(1, 0, 0);
	// 				}
	// 			}
	// 			// If the user presses the down arrow key
	// 			if (Input.GetKey(KeyCode.DownArrow))
	// 			{
	// 				// reduce the fall time
	// 				fallTime -= 0.1f;
	// 			}
	// 		// }
	// 	}
	// 	// if it should be falling
	// 	if (startedFalls)
	// 	{
	// 		// If there is no falling square
	// 		if (fallingSquare == null && blockCount > 0)
	// 		{
	// 			// Create a new square
	// 			fallingSquare = Instantiate(squarePrefab, new Vector3(-0.5f, 5.5f, 0), Quaternion.identity).transform;
	// 			// set parent to CustomShape variable
	// 			fallingSquare.parent = customShapeParent;
	// 		}
	// 		// if block count is 0
	// 		else if (blockCount == 0 || Input.GetKeyUp(KeyCode.Return))
	// 		{
	// 			// add CustomShape to GameSettings
	// 			GameObject.Find("gameSettings").GetComponent<GameSettings>().addTetromino(customShapeParent.gameObject);
	// 			// decrement maxCustomShapes
	// 			maxCustomShapes--;
	// 			// if maxCustomShapes is 0
	// 			if (maxCustomShapes == 0)
	// 			{
	// 				// load the game scene
	// 				//UnityEngine.SceneManagement.SceneManager.LoadScene("8bit");
	// 			}
	// 			// set block count to 5
	// 			blockCount = 5;
	// 			// set startedFalls to false
	// 			startedFalls = false;
	// 			fallingSquare = null;

	// 			GameObject fakeNewShape = Instantiate(customShapeParent.gameObject, customShapeParent.position, Quaternion.identity);
	// 			foreach (Transform child in fakeNewShape.transform)
	// 			{
	// 				// Check if the child has a SpriteRenderer component
	// 				child.GetComponent<SpriteRenderer>().color = new Color(1f, 0.843f, 0f); // Set to black

	// 			}
	// 			// push the custom shape down

	// 			customShapeParent.position = new Vector3(10, 0, 0);

	// 			// set new custom shape parent
	// 			customShapeParent = new GameObject().transform;
	// 			// set name of new custom shape parent
	// 			customShapeParent.name = "CustomShape" + (5 - maxCustomShapes);
	// 		}
	// 		// if there is a falling square
	// 		else if (fallingSquare != null)
	// 		{
	// 			if (Time.time - fallTime > .002)
	// 			{
	// 				// Move the square down
	// 				fallingSquare.position += new Vector3(0, -1, 0);
	// 				// Raycast to see if the square is touching anything
	// 				RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.down, 1);
	// 				// If the square is touching something
	// 				if (hit.collider != null)
	// 				{
	// 					// Set the falling square to null
	// 					fallingSquare = null;
	// 					// Decrement the block count
	// 					blockCount--;
	// 				}
	// 				// Set the fall time to the current time
	// 				fallTime = Time.time;
	// 			}
	// 		}
	// 		else if (Input.GetKeyUp(KeyCode.Return) && fallingSquare == null){
	// 			SceneManager.LoadScene("Menu");
	// 		}
	// 	}
	// }
}