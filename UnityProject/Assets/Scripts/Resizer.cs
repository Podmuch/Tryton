using UnityEngine;
using UnityEngine.UI;

public class Resizer : MonoBehaviour
{
    #region CLASS SETTINGS

    private const float WIDTH_TO_HEIGHT_RATIO = 1.6f;

    #endregion

    #region SCENE REFERENCES

    public RectTransform ResizerTransform;

    #endregion

    private float savedScreenWidth = 0;
    private Vector3 originalScale;
    private void Start()
    {
        originalScale = ResizerTransform.localScale;
        Resize();
    }

    private void Update()
    {
        if(Screen.width!=Mathf.RoundToInt(savedScreenWidth))
        {
            Resize();
        }
    }

    private void Resize()
    {
        float screenWidth = (float)Screen.width;
        float screenHeight = (float)Screen.height;
        float currentWidth = screenHeight * WIDTH_TO_HEIGHT_RATIO;
        ResizerTransform.localScale = new Vector3((screenWidth / currentWidth) * originalScale.x, originalScale.y, originalScale.z);
        savedScreenWidth = Screen.width;
    }
}