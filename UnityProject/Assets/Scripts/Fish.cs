using UnityEngine;

public class Fish : MonoBehaviour
{
    #region CLASS SETTINGS

    private const float LEFT_BORDER = -4.0f;
    private const float RIGHT_BORDER = 4.0f;
    private const float WIDTH = 1.9f;
    private const float Y_POSITION_CHANGING_FACTOR = 5;

    #endregion

    #region SCENE REFERENCES

    public Rigidbody RigidbodyComponent;
    public SpriteRenderer Renderer;

    #endregion

    private Vector3 velocity;
    private float floatingAnimator;
    private float floatingDirection;
    private bool isLeft;
    public float floatingAnimatorMinValue = -0.5f;
    public float floatingAnimatorMaxValue = 0.5f;
    public float minVelocity = 2;
    public float maxVelocity = 4;

    public void Init()
    {
        Renderer.sortingOrder = Random.Range(5,200);
        isLeft = Random.Range(0, 10) < 5;
        transform.localRotation = Quaternion.Euler(new Vector3(0, isLeft ? 180 : 0, 0));
        transform.localPosition = new Vector3(isLeft?LEFT_BORDER:RIGHT_BORDER, Random.Range(-WIDTH, WIDTH));
        velocity= new Vector3(isLeft?Random.Range(minVelocity, maxVelocity):Random.Range(-maxVelocity, -minVelocity), 0);
        floatingAnimator = Random.Range(floatingAnimatorMinValue, floatingAnimatorMaxValue);
        floatingDirection = Random.Range(0, 10) < 5 ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
    }

    private void Update()
    {
        if (floatingAnimator >= floatingAnimatorMaxValue)
        {
            floatingDirection = Random.Range(-1, -0.5f);
        }
        else if (floatingAnimator <= floatingAnimatorMinValue)
        {
            floatingDirection = Random.Range(0.5f, 1);
        }
        if ((isLeft && transform.localPosition.x > RIGHT_BORDER)||
            (!isLeft && transform.localPosition.x < LEFT_BORDER))
        {
            Destroy(gameObject);
        }
        floatingAnimator += floatingDirection * Time.deltaTime;
        velocity.y = Y_POSITION_CHANGING_FACTOR * floatingAnimator;
        RigidbodyComponent.velocity = velocity;
    }
}