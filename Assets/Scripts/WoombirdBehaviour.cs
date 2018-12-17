using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoombirdBehaviour : MonoBehaviour {

	// Variables for the ground check and jumping system
	bool grounded = false;
	float groundRadius = 0.1f;
	public float jumpForce = 250f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	
	// Timer variables
	bool TimerStarted = true;
	private float timer = 0f;
	public float TimeIWantInSeconds = 1.5f;
	
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
		if(TimerStarted)
		{
			timer += Time.deltaTime;
		}	
		
		//Checking variables for jumping
		if (grounded && timer >= TimeIWantInSeconds)
		{
			//anim.SetBool("Ground", false);		
			rigid2D.AddForce(new Vector2(0, jumpForce));
			timer = 0;
		}
		
	}
	
	void FixedUpdate (){
		
		// Jumping
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("VerticalSpeed", rigid2D.velocity.y);
	}
}
