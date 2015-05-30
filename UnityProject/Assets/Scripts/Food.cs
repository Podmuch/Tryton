using UnityEngine;

public class Food : MonoBehaviour
{
	#region CLASS SETTINGS
	
	private const float UP_BORDER = 20f;
	private const float DOWN_BORDER = -20f;
	private const float HEIGHT = 32.0f;
	
	#endregion
	
	#region SCENE REFERENCES
	
	public Rigidbody RigidbodyComponent;
	public SpriteRenderer Renderer;
	
	#endregion

	public float minVelocity = 2;
	public float maxVelocity = 4;

	Vector3 velocity;

	public void Init() {

		transform.localPosition = new Vector3(Random.Range(-HEIGHT, HEIGHT), UP_BORDER);
		velocity= -new Vector3(0, Random.Range(minVelocity, maxVelocity), 0);
		RigidbodyComponent.velocity = velocity;
	}
	
	private void Update()
	{
		RigidbodyComponent.velocity = velocity;

        if (transform.localPosition.y < DOWN_BORDER)
        {
            Destroy(gameObject);
        }
	}
}