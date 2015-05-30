using UnityEngine;

/// <summary>
/// Poruszanie graczem
/// </summary>
public class PlayerScript : MonoBehaviour
{
	// Zmiana położenia
	Vector2 movement;

	// Prędkość gracza
	Vector2 speed = new Vector2(10, 10);
	void Update()
	{
		// Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// Movement per direction
		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY); 
	}
	
	void FixedUpdate()
	{
		// Move the game object
		rigidbody2D.velocity = movement;
	}
}
