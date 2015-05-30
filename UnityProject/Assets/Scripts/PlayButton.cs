using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    #region CLASS SETTINGS

    private const float HIDING_ANIMATION_TIME = 0.4f;
    private const float CLICK_ANIMATION_TIME = 0.2f;

    #endregion

    #region SCENE REFERENCES

    public Text PlayText;
    public Text PlayTextShadow;
    public Image ButtonImage;

    #endregion

    private Color playTextColor;
    private Color playTextShadowColor;
    private Color buttonImageColor;
    private bool isHidden;
    private bool isClicked;
    private float animationTime;

    #region MONO BEHAVIOUR

    private void Awake()
    {
        playTextColor = PlayText.color;
        playTextShadowColor = PlayTextShadow.color;
        buttonImageColor = ButtonImage.color;
    }

    private void Update()
    {
        if (isClicked && animationTime< CLICK_ANIMATION_TIME)
        {
            animationTime += Time.deltaTime;
            //ButtonImage.color = new Color()
            if (animationTime >= CLICK_ANIMATION_TIME)
            {
                ButtonImage.color = buttonImageColor;
                PlayText.color = playTextColor;
                PlayTextShadow.color =playTextShadowColor;
                isClicked = false;
                isHidden = true;
                animationTime = 0;
            }
        }
        if(isHidden && animationTime < HIDING_ANIMATION_TIME)
        {
            animationTime += Time.deltaTime;
            float factor = 1-animationTime/HIDING_ANIMATION_TIME;
            ButtonImage.color = new Color(ButtonImage.color.r, ButtonImage.color.g, ButtonImage.color.b, buttonImageColor.a * factor);
            PlayText.color = new Color(PlayText.color.r, PlayText.color.g, PlayText.color.b, playTextColor.a * factor);
            PlayTextShadow.color = new Color(PlayTextShadow.color.r, PlayTextShadow.color.g, PlayTextShadow.color.b, playTextShadowColor.a * factor);
            if(animationTime>=HIDING_ANIMATION_TIME)
            {
                ButtonImage.color = new Color(ButtonImage.color.r, ButtonImage.color.g, ButtonImage.color.b, 0);
                PlayText.color = new Color(PlayText.color.r, PlayText.color.g, PlayText.color.b, 0);
                PlayTextShadow.color = new Color(PlayTextShadow.color.r, PlayTextShadow.color.g, PlayTextShadow.color.b, 0);
                gameObject.SetActive(false);
            }
        }
    }

    #endregion

    public void Init()
    {
        PlayText.color = playTextColor;
        PlayTextShadow.color = playTextShadowColor;
        ButtonImage.color = buttonImageColor;
        isHidden = false;
        isClicked = false;
        gameObject.SetActive(true);
        animationTime = 0;
    }

    public void PlayButtonOnClick()
    {
        if(!isHidden&&!isClicked)
        {
            isClicked = true;
            animationTime = 0;
            GameController.Instance.StartGame();
        }
    }
}