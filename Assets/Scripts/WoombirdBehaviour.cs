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
		
		//Checking variables for jumping
		if (grounded)
		{
			//anim.SetBool("Ground", false);
			rigid2D.AddForce(new Vector2(0, jumpForce));
		}
		
	}
	
	void FixedUpdate (){
		
		// Jumping
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		
	}
}
