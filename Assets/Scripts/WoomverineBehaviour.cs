using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoomverineBehaviour : MonoBehaviour {

	// Variables for the speed movement of the Woomverine
	float speed = -0.5f;
	bool facingRight = true;
	
	// Variables for the ground check and jumping system
	bool grounded = false;
	float groundRadius = 0.1f;
	float jumpForce = 150f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	
	// Timer variables
	bool TimerStartedJump = false;
	bool TimerStartedFlip = true;
	private float timerJump = 0f;
	private float timerFlip = 0f;
	float TimeIWantInSecondsForJumping = 3f;
	float TimeIWantInSecondsToFlip = 1.2f;
	
	// Initialize Animator and Rigidbody 
	Animator anim;
	Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		
		// Initialize Animator and Rigidbody
		anim = GetComponent<Animator>();
		rigid2D = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Adding time to the timer for jumping
		if(grounded)
		{
			TimerStartedJump = true;
		}
		
		if(TimerStartedJump)
		{
			timerJump += Time.deltaTime;		
		}
		
		if(TimerStartedFlip)
		{
			timerFlip += Time.deltaTime;
		}
		
		// Checking variables for jumping
		if (grounded && timerJump >= TimeIWantInSecondsForJumping)
		{		
			rigid2D.AddForce(new Vector2(0, jumpForce));
			TimerStartedJump = false;
			timerJump = 0;
			TimeIWantInSecondsForJumping = Random.Range(3,7);
		}
		
		// Checking variables for flipping
		if (grounded && timerFlip >= TimeIWantInSecondsToFlip)			
		{
			speed = speed * -1;
			Flip();
			timerFlip = 0;
		}
	}
	
	void FixedUpdate () {
		
		// Jumping
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("VerticalSpeed", rigid2D.velocity.y);
		
		// Moving left or right
		rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
		anim.SetFloat("Speed", Mathf.Abs(speed));
	}
	
	// Function for flipping the character
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
