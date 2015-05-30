using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

    public Camera CameraObject;
	public float Gravity = 0f;	//downward force
	public float TerminalVelocity = 200f;	//max downward speed
	public float JumpSpeed = 6000f;
	public float MoveSpeed = 1000f;
	
	public Vector3 MoveVector {get; set;}
	public float VerticalVelocity {get; set;}
	
	public Rigidbody2D RigidbodyComponent;

	private bool wasFalling = true; 
	private bool isGrounded { get{ return RigidbodyComponent.velocity.y<1&&RigidbodyComponent.velocity.y>-1;}}
	// Use this for initialization
	void Awake () {
		RigidbodyComponent = gameObject.GetComponent("Rigidbody2D") as Rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
		if (RigidbodyComponent.velocity.y < -1) {
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
		if(isGrounded && wasFalling){
			VerticalVelocity = JumpSpeed;
			wasFalling = false;
		}
	}
}