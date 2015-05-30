using System;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    #region CLASS SETTINGS

    private float RESPAWN_TIME = 0.5f;

    #endregion

    #region RESOURCE REFERENCES

    public Fish[] FishPrefabs;

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
                GameObject newFish = (GameObject)Instantiate(FishPrefabs[UnityEngine.Random.Range(0, FishPrefabs.Length)].gameObject);
                newFish.transform.parent = transform;
                newFish.transform.localScale = Vector3.one;
                Fish fishComponent = newFish.GetComponent<Fish>();
                fishComponent.Init();
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