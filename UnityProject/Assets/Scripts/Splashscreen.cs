using UnityEngine;
using System.Collections;

public class Splashscreen : MonoBehaviour {

    private const float ANIMATION_TIME = 30.0f;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public RectTransform SplashScreenObject;
	// Use this for initialization

    private float animationTime = 0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (animationTime < ANIMATION_TIME)
        {
            animationTime += Time.deltaTime;
            SplashScreenObject.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, animationTime / ANIMATION_TIME);
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Application.LoadLevel("mainscene");
        }
	}
}
