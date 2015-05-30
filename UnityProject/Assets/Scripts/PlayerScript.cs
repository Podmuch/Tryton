using System.Collections;
using UnityEngine;

/// <summary>
/// Poruszanie graczem
/// </summary>
public class PlayerScript : MonoBehaviour
{
    #region CLASS SETTINGS

    private const float LEFT_BORDER = -4.0f;
    private const float RIGHT_BORDER = 4.0f;
    private const float WIDTH = 1.9f;
    private const float Y_POSITION_CHANGING_FACTOR = 3;
    private const float FLOATING_ANIMATOR_MIN_VALUE = -0.5f;
    private const float FLOATING_ANIMATOR_MAX_VALUE = 0.5f;
    private const float SHOW_ANIMATION_TIME = 0.5f;
    private const float INTOUCHABLE_TIME = 2.0f;
	private static Vector2 SPEEED = new Vector2(10, 10);

    #endregion

    #region SCENE REFERENCES

    public Rigidbody RigidbodyComponent;
    public SpriteRenderer Renderer;
    public Animator Animator;
    #endregion

    private Vector3 velocity;
    private float floatingAnimator;
    private float floatingDirection;
    private bool isInit = false;
    private bool isTurnAnimationPlay = false;
    private int framesAfterTurnAnimation = 0;
    private bool inTouchable;
    private float inTouchableTimer = 0.0f;
    public void Init()
    {
        isInit = true;
        isTurnAnimationPlay = false;
        inTouchable = false;
        inTouchableTimer = 0.0f;
        framesAfterTurnAnimation = 0;
        floatingAnimator = Random.Range(FLOATING_ANIMATOR_MIN_VALUE, FLOATING_ANIMATOR_MAX_VALUE);
        floatingDirection = Random.Range(0, 10) < 5 ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        for (float showTime = 0; showTime < SHOW_ANIMATION_TIME;showTime+=Time.deltaTime)
        {
            Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, showTime / SHOW_ANIMATION_TIME);
            yield return 0;
        }
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 1);
    }

	private void Update()
	{
        if(isInit)
        {
	        // Retrieve axis information
	        float inputX = Input.GetAxis("Horizontal");
	        float inputY = Input.GetAxis("Vertical");

            if ((inputX > 0 && velocity.x < 0) || (inputX < 0 && velocity.x > 0))
            {
                Animator.Play("turn");
                isTurnAnimationPlay = true;
                framesAfterTurnAnimation = 0;
            }
            if (isTurnAnimationPlay)
            {
                framesAfterTurnAnimation++;
                if(framesAfterTurnAnimation>=5)
                {
                    framesAfterTurnAnimation = 0;
                    isTurnAnimationPlay = false;
                    Animator.Play("btri_r");
                }
            }
	        // Movement per direction
	        velocity = new Vector2(
		        SPEEED.x * inputX,
		        SPEEED.y * inputY);
            transform.localRotation = Quaternion.Euler(new Vector3(0, velocity.x < 0 ? 180 : velocity.x > 0 ? 0 : transform.localRotation.eulerAngles.y, 0));
            if (floatingAnimator >= FLOATING_ANIMATOR_MAX_VALUE)
            {
                floatingDirection = Random.Range(-1, -0.5f);
            }
            else if (floatingAnimator <= FLOATING_ANIMATOR_MIN_VALUE)
            {
                floatingDirection = Random.Range(0.5f, 1);
            }
            floatingAnimator += floatingDirection * Time.deltaTime;
        }
        velocity.y += Y_POSITION_CHANGING_FACTOR * floatingAnimator;
        RigidbodyComponent.velocity = velocity;
        if(inTouchable&&inTouchableTimer<INTOUCHABLE_TIME)
        {
            inTouchableTimer += Time.deltaTime;
            if(inTouchableTimer>=INTOUCHABLE_TIME)
            {
                inTouchable = false;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Fish" && !inTouchable)
        {
            GameController.Instance.LoseLife();
            inTouchable = true;
            inTouchableTimer = 0.0f;
        }
    }
}
