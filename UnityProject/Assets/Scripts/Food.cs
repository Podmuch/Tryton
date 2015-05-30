using UnityEngine;

public class Food : MonoBehaviour
{
	#region CLASS SETTINGS
	
	private const float UP_BORDER = 1.9f;
	private const float DOWN_BORDER = -1.9f;
	private const float HEIGHT = 4.0f;

	private const float BAD_FOOD_RATE = 0.1f;
	
	#endregion
	
	#region SCENE REFERENCES
	
	public Rigidbody RigidbodyComponent;
	public SpriteRenderer Renderer;
	
	#endregion

	public float minVelocity = 2;
	public float maxVelocity = 4;

	Vector3 velocity;
	bool good_food = true;

	public void Init() {
		if (Random.Range (0, 1) < BAD_FOOD_RATE) {
			good_food = false;
		}

		transform.localPosition = new Vector3(Random.Range(-HEIGHT, HEIGHT), UP_BORDER);
		velocity= new Vector3(0, Random.Range(minVelocity, maxVelocity), 0);
		RigidbodyComponent.velocity = velocity;
	}
	
	private void Update()
	{
		RigidbodyComponent.velocity = velocity;

		if (transform.localPosition.y < DOWN_BORDER)
			Destroy (this);
	}
}