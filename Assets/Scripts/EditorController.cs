using UnityEngine;
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
	void Start()
	{
		// Set the fall time to the current time
		fallTime = Time.time;
		// Set the physics to not start in colliders
		Physics2D.queriesStartInColliders = false;
	}
	void Update()
	{
		// if the user presses any of the arrow keys
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			// If the game has not started falling
			if (!startedFalls)
			{
				// Start the game falling
				startedFalls = true;
			}
			// If there is a falling square
			if (fallingSquare != null)
			{
				// If the user presses the left arrow key
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					// raycast to see if the square is touching anything
					RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.left, 1);
					// If the square is not touching anything
					if (hit.collider == null)
					{
						// Move the square left
						fallingSquare.position += new Vector3(-1, 0, 0);
					}
				}
				// If the user presses the right arrow key
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					// raycast to see if the square is touching anything
					RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.right, 1);
					// If the square is not touching anything
					if (hit.collider == null)
					{
						// Move the square right
						fallingSquare.position += new Vector3(1, 0, 0);
					}
				}
				// If the user presses the down arrow key
				if (Input.GetKey(KeyCode.DownArrow))
				{
					// reduce the fall time
					fallTime -= 0.1f;
				}
			}
		}
		// if it should be falling
		if (startedFalls)
		{
			// If there is no falling square
			if (fallingSquare == null && blockCount > 0)
			{
				// Create a new square
				fallingSquare = Instantiate(squarePrefab, new Vector3(-0.5f, 5.5f, 0), Quaternion.identity).transform;
				// set parent to CustomShape variable
				fallingSquare.parent = customShapeParent;
			}
			// if block count is 0
			else if (blockCount == 0)
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
				// set block count to 5
				blockCount = 5;
				// set startedFalls to false
				startedFalls = false;
				// push the custom shape down
				customShapeParent.position = new Vector3(10, 0, 0);
				// set new custom shape parent
				customShapeParent = new GameObject().transform;
				// set name of new custom shape parent
				customShapeParent.name = "CustomShape" + (5 - maxCustomShapes);
			}
			// if there is a falling square
			else
			{
				if (Time.time - fallTime > 1)
				{
					// Move the square down
					fallingSquare.position += new Vector3(0, -1, 0);
					// Raycast to see if the square is touching anything
					RaycastHit2D hit = Physics2D.Raycast(fallingSquare.position, Vector2.down, 1);
					// If the square is touching something
					if (hit.collider != null)
					{
						// Set the falling square to null
						fallingSquare = null;
						// Decrement the block count
						blockCount--;
					}
					// Set the fall time to the current time
					fallTime = Time.time;
				}
			}
		}
	}
}