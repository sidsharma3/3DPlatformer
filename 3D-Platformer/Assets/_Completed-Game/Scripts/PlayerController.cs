using UnityEngine;
using UnityEngine.SceneManagement;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public Text countText;
	public Text winText;


	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
	}

	// Each physics step..
	void FixedUpdate ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
			// multiplying it by 'speed' - our public player speed that appears in the inspector
			rb.AddForce (movement * speed);
		} else {

			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);

			rb.AddForce (movement * speed);
			
		}
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}

		if (other.gameObject.CompareTag ("TrapDoor"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = -1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}

		if (other.gameObject.CompareTag ("Portal"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.transform.Translate(new Vector3 (90, 0, 0) * Time.deltaTime);
		}

	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12 && SceneManager.GetActiveScene().name == "Roll-a-ball") 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			SceneManager.LoadScene("stage2");
		} else if (count >= 12 && SceneManager.GetActiveScene().name == "stage2") 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			SceneManager.LoadScene("stage3");
		} else if (count >= 12 && SceneManager.GetActiveScene().name == "stage3") 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			SceneManager.LoadScene("stage4");
		} else if (count >= 12 && SceneManager.GetActiveScene().name == "stage4") 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			SceneManager.LoadScene("stage5");
		} else if (count >= 12 && SceneManager.GetActiveScene().name == "stage5") 
		{
			// Set the text value of our 'winText'
			winText.text = "Game Won!";
		}else if (count == -1) 
		{
			// Set the text value of our 'winText'
			winText.text = "Game Over!";
			SceneManager.LoadScene("Roll-a-ball");
		}
		//else if (count == 8) 
//		{
//			// Set the text value of our 'winText'
//			winText.text = "You Win!";
//			currentlevel = 4;
//			count = 0;
//			SceneManager.LoadScene("stage4");
//		} else if (count == 6) 
//		{
//			// Set the text value of our 'winText'
//			winText.text = "You Win!";
//			currentlevel = 5;
//			count = 0;
//			SceneManager.LoadScene("stage5");
//		}
	}
}