using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player2 : MonoBehaviour {

    private const int MIN_TRANSISTORS = 10;
    private const int MAX_TRANSISTORS = 32;
    public Camera CameraObject;
    public Text TransistorCounter;
	public float Gravity = 0f;	//downward force
	public float TerminalVelocity = 200f;	//max downward speed
	public float JumpSpeed = 6000f;
	public float MoveSpeed = 1000f;
	
	public Vector3 MoveVector {get; set;}
	public float VerticalVelocity {get; set;}
	
	public Rigidbody2D RigidbodyComponent;
    public GameObject Teleport;

    private int transistorsCollected = 0;
    private bool isGrounded { get { return RigidbodyComponent.velocity.y < 1 && RigidbodyComponent.velocity.y > -1&&isRealGrounded; } }
    private bool isRealGrounded;
    private bool wasFalling = true; 
	// Use this for initialization
	void Awake () {
        isRealGrounded = true;
		RigidbodyComponent = gameObject.GetComponent("Rigidbody2D") as Rigidbody2D;
        Teleport.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (RigidbodyComponent.velocity.y < -1)
        {
            wasFalling = true;
        }
		checkMovement();
		HandleActionInput();
		processMovement();
        CameraObject.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}
	
	void checkMovement(){
		//move l/r
		var deadZone = 0.1f;
		VerticalVelocity = MoveVector.y;
		MoveVector = Vector3.zero;
		if(Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone){
			MoveVector += new Vector3(Input.GetAxis("Horizontal"),0,0);
		}
		//jump
		
	}
	
	void HandleActionInput(){
		if(Input.GetButton("Jump")){
			jump();
		}
	}
	
	void processMovement(){
		//transform moveVector into world-space relative to character rotation
		//MoveVector = transform.TransformDirection(MoveVector);
		
		//normalize moveVector if magnitude > 1
		if(MoveVector.magnitude > 1){
			MoveVector = Vector3.Normalize(MoveVector * Time.deltaTime);
		}
		
		//multiply moveVector by moveSpeed
		MoveVector *= MoveSpeed;
		
		//reapply vertical velocity to moveVector.y
		MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);
		
		//apply gravity
		applyGravity();
		
		//move character in world-space
        transform.localRotation = Quaternion.Euler(new Vector3(0, MoveVector.x < 0 ? 0 : MoveVector.x > 0 ? 180 : transform.localRotation.eulerAngles.y, 0));
		RigidbodyComponent.velocity = MoveVector ;
	}
	
	void applyGravity(){
		if(MoveVector.y > -TerminalVelocity){
			MoveVector = new Vector3(MoveVector.x, (MoveVector.y - Gravity * Time.deltaTime), MoveVector.z);
		}
		if(isGrounded && MoveVector.y < -1){
			MoveVector = new Vector3(MoveVector.x, (-1), MoveVector.z);
		}
	}
	
	public void jump(){
        if (isGrounded && wasFalling)
        {
            VerticalVelocity = JumpSpeed;
            wasFalling = false;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            if(collision.collider.transform.position.y>transform.position.y)
            {
                MoveVector = new Vector3(MoveVector.x, MoveVector.y > 0 ? 0 : MoveVector.y, MoveVector.z);
                VerticalVelocity = 0;
				isRealGrounded = false;
            }
			else
			{
				isRealGrounded = true;
			}
        }
        if(collision.collider.tag == "Cloud")
        {
            if (collision.collider.transform.position.y < transform.position.y)
            {
                MoveVector = new Vector3(MoveVector.x, MoveVector.y*(-2), MoveVector.z);
            }
            else
            {
                MoveVector = new Vector3(MoveVector.x, MoveVector.y > 0 ? 0 : MoveVector.y, MoveVector.z);
                VerticalVelocity = 0;
                isRealGrounded = false;
            }
        }
		if (collision.collider.tag == "Killer")
		{
            Application.LoadLevel(Application.loadedLevelName);
		}
        if (collision.collider.tag == "EndPoints")
        {
            if(transistorsCollected>=MIN_TRANSISTORS)
            {
                Application.LoadLevel("level3");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Transistor")
        {
            transistorsCollected++;
            TransistorCounter.text = transistorsCollected + "/" + MIN_TRANSISTORS + " Transistors";
			Destroy(other.gameObject);
			if(transistorsCollected>=MIN_TRANSISTORS)
			{
				Teleport.SetActive(true);
			}
        }
    }
}