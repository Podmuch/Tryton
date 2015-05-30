using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    public Image BloodSprite;

    private const float ANIMATION_TIME = 1.0f;
    private float animationTime = 0.0f;

    private void Update()
    {
        if(animationTime<ANIMATION_TIME)
        {
            animationTime += Time.deltaTime;
            BloodSprite.color = new Color(BloodSprite.color.r, BloodSprite.color.g, BloodSprite.color.b, 1 - animationTime / ANIMATION_TIME);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}