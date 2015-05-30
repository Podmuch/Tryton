using System;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    #region CLASS SETTINGS

    private float MAX_RESPAWN_TIME = 1.5f;
    private float MIN_RESPAWN_TIME = 0.5f;
    private float RESPAWN_TIME_DELAY = 0.05f;

    #endregion

    #region RESOURCE REFERENCES

    public Fish[] FishPrefabs;

    #endregion

    private bool isWorking = false;
    private float timer = 0;
    private float respawnTime;

    private void Update()
    {
        if(isWorking)
        {
            timer += Time.deltaTime;
            if (timer >= respawnTime)
            {
                timer = 0;
                GameObject newFish = (GameObject)Instantiate(FishPrefabs[UnityEngine.Random.Range(0, FishPrefabs.Length)].gameObject);
                newFish.transform.parent = transform;
                newFish.transform.localScale = Vector3.one;
                Fish fishComponent = newFish.GetComponent<Fish>();
                fishComponent.Init();
                if (respawnTime > MIN_RESPAWN_TIME)
                {
                    respawnTime -= RESPAWN_TIME_DELAY;
                }
            }
        }
    }

    public void StartGenerator()
    {
        isWorking = true;
        timer = 0;
        respawnTime = MAX_RESPAWN_TIME;
    }

    public void StopGenerator()
    {
        isWorking = false;
    }
}