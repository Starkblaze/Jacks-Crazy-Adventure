using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoombirdBehaviour : MonoBehaviour {

	// Variables for the ground check and jumping system
	bool grounded = false;
	float groundRadius = 0.1f;
	float jumpForce = 250f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	
	// Timer variables
	bool TimerStarted = false;
	private float timer = 0f;
	float TimeIWantInSeconds = 2f;
	
	// Variables for the smashing behaviour when dying
	bool smashed = false;
	float smashRadius = 0.1f;
	public Transform smashCheck;
	public LayerMask whatCanSmash;
	
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
			TimerStarted = true;
		}
		
		if(TimerStarted)
		{
			timer += Time.deltaTime;
			print(timer);
		}	
		
		//Checking variables for jumping
		if (grounded && timer >= TimeIWantInSeconds)
		{		
			rigid2D.AddForce(new Vector2(0, jumpForce));
			TimerStarted = false;
			timer = 0;
		}
		
		if (smashed)
		{
			anim.SetBool("Smashed", smashed);
			timer = 0;
			Destroy(gameObject, 0.5f);
		}
		
	}
	
	void FixedUpdate (){
		
		// Jumping
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("VerticalSpeed", rigid2D.velocity.y);
		
		// Smashed detector
		smashed = Physics2D.OverlapCircle(smashCheck.position, smashRadius, whatCanSmash);
		//anim.SetBool("Smashed", smashed);
	}
}
