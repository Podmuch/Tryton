using UnityEngine;

/// <summary>
/// Ruch ryb w wodzie
/// </summary>
public class FishScript : MonoBehaviour
{
	// Przesunięcie położenia
	static public Vector3 trans= new Vector3(0,0,0);
	// Prędkości
	public Vector2 wave_speed = new Vector2(0.2f, 0.2f);
	// Tempo falowania
	public Vector2 wave_time = new Vector2(5,3);

	void Update()
	{
		// Uwzględnienie ruchu wody w położeniu
		transform.localPosition -= trans;
		trans = new Vector3(wave_speed.x * Mathf.Sin(Time.time * wave_time.x),
		                    wave_speed.y * Mathf.Sin(Time.time * wave_time.y),
		                    0);
		transform.localPosition += trans; 
	}
}
