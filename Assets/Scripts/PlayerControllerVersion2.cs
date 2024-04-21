using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerVersion2 : MonoBehaviour
{
	// Variables
	public float speed = 5.0f; // maxSpeed
	public float acceleration = 2f; // acceleration
	public float jumpSpeed = 10.0f; // jumpForce multiplier
	public float doubleJumpSpeed = 10.0f; // doubleJumpForce multiplier
	public float raycastDistance = 0.2f; // raycast distance - for checking if the player is grounded
	public float gravity = 3.0f; // gravity
	public float snappiness = 0.50f; // snappiness of the player's movement
	public MasterObject master;
	// Private variables
	private bool hasDoubleJumped = false;
	private Rigidbody2D rb;
	private BoxCollider2D bc;

	public AudioSource jumpSound;
	// Start function
	void Start()
	{
		master = GameObject.Find("master").GetComponent<MasterObject>();
		// Get the Rigidbody2D component
		rb = GetComponent<Rigidbody2D>();
		// Set the gravity scale of the rigidbody
		rb.gravityScale = gravity;
		// Get the BoxCollider2D component
		bc = GetComponent<BoxCollider2D>();
	}
	// Update function
	void Update()
	{
		float move = Input.GetAxis("Horizontal");
		// if the player stopped pressing the movement keys
		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			// set the velocity to 0
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(move * speed * acceleration, rb.velocity.y);
		}
		// if velocity > speed, set velocity to speed
		if (rb.velocity.x > speed && move != 0)
		{
			rb.velocity = new Vector2(speed, rb.velocity.y);
		}
		// if velocity < -speed, set velocity to -speed
		else if (rb.velocity.x < -speed && move != 0)
		{
			rb.velocity = new Vector2(-speed, rb.velocity.y);
		}
		// if move is greater than 0
		if (move > 0)
		{
			// if raycast to the right is not hitting anything
			RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right / 1.8f, Vector3.right, raycastDistance);
			// run 2nd raycast on lower right corner
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right / 1.8f + Vector3.down / 2, Vector3.right, raycastDistance);
			// debug raycast
			Debug.DrawRay(transform.position + Vector3.right / 1.8f, Vector3.right * raycastDistance, Color.red);
			Debug.DrawRay(transform.position + Vector3.right / 1.8f + Vector3.down / 2, Vector3.right * raycastDistance, Color.red);
			if (hit.collider == null && hit2.collider == null)
			{
				// Snap the player's position to the nearest grid position, rounded to 2nd decimal place
				SnapMovement();
			}
		}
		// if move is less than 0
		else if (move < 0)
		{
			// if raycast to the left is not hitting anything
			RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left / 1.8f, Vector3.left, raycastDistance);
			// run 2nd raycast on lower left corner
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.left / 1.8f + Vector3.down / 2, Vector3.left, raycastDistance);
			// debug raycast
			Debug.DrawRay(transform.position + Vector3.left / 1.8f, Vector3.left * raycastDistance, Color.red);
			Debug.DrawRay(transform.position + Vector3.left / 1.8f + Vector3.down / 2, Vector3.left * raycastDistance, Color.red);
			// if raycast to the left is not hitting anything
			if (hit.collider == null && hit2.collider == null)
			{
				// Snap the player's position to the nearest grid position, rounded to 2nd decimal place
				SnapMovement();
			}
		}
		// // Add force to the rigidbody based on the player's input
		// rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * speed * acceleration);
		// // Limit the velocity of the rigidbody to the speed variable
		// if (rb.velocity.x > speed)
		// {
		// 	// If the velocity is greater than the speed, set the velocity to the speed
		// 	rb.velocity = new Vector2(speed, rb.velocity.y);
		// }
		// // Limit the velocity of the rigidbody to the negative speed variable
		// else if (rb.velocity.x < -speed)
		// {
		// 	// If the velocity is less than the negative speed, set the velocity to the negative speed
		// 	rb.velocity = new Vector2(-speed, rb.velocity.y);
		// }
		// Check if the player is grounded
		//if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		//removed up arrow to keep it just W Key for now
		if (Input.GetKeyDown(KeyCode.W))
		{
			// If the player is grounded, jump, if not then DoubleJump
			Jump();
		}
		if (Input.GetKeyDown(KeyCode.S) && master.getInversion() == -1)
		{
			// If the player is grounded, jump, if not then DoubleJump
			Jump();
		}
	}
	// SnapMovement function
	private void SnapMovement()
	{
		// Snap the player's position to the nearest grid position, rounded to 2nd decimal place
		transform.position = new Vector2(Mathf.Round(transform.position.x * snappiness) / snappiness, transform.position.y);
	}

	// Jump function
	void Jump()
	{
		// Check if the player is grounded
		if (IsGrounded())
		{
			jumpSound.Play();
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed * master.getInversion());

			// // If the player is grounded, add force to the rigidbody
			// rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			// // Set hasDoubleJumped to false
			hasDoubleJumped = false;
		}
		// If the player is not grounded and has not double jumped
		else if (!hasDoubleJumped)
		{
			jumpSound.Play();
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed) * master.getInversion();
			// Add force to the rigidbody
			//rb.AddForce(Vector2.up * doubleJumpSpeed, ForceMode2D.Impulse);
			// Set hasDoubleJumped to true
			hasDoubleJumped = true;
		}
	}
	// Check if the player is grounded
	bool IsGrounded()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector3.down, raycastDistance);
		if (master.getInversion() == -1){
		// Shoot a raycast down from the player
			hit = Physics2D.Raycast(transform.position + Vector3.up, Vector3.up, raycastDistance);
		}

		// If the raycast hits anything
		if (hit.collider != null)
		{
			// If the raycast hits anything return true
			return true;
		}
		// If the raycast does not hit anything, return false
		return false;
	}
}