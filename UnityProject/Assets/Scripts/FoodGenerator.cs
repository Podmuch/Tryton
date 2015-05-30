using UnityEngine;
using System.Collections;

public class FoodGenerator: MonoBehaviour {
	#region CLASS SETTINGS

    private float RESPAWN_TIME = 2.0f;
	
	#endregion
	
	#region RESOURCE REFERENCES
	
	public Food[] FoodPrefabs;
	
	#endregion
	
	private bool isWorking = false;
	private float timer = 0;
	
	private void Update()
	{
		if(isWorking)
		{
			timer += Time.deltaTime;
			if(timer>=RESPAWN_TIME)
			{
				timer = 0;
                GameObject newFood = (GameObject)Instantiate(UnityEngine.Random.Range(0, 10.0f) < 1.5f ? FoodPrefabs[1].gameObject : FoodPrefabs[0].gameObject);
				newFood.transform.parent = transform;
				newFood.transform.localScale = Vector3.one;
				Food foodComponent = newFood.GetComponent<Food>();
				foodComponent.Init();
			}
		}
	}
	
	public void StartGenerator()
	{
		isWorking = true;
		timer = 0;
	}
	
	public void StopGenerator()
	{
		isWorking = false;
	}
}