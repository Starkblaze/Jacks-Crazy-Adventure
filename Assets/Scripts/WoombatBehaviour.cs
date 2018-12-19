using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoombatBehaviour : MonoBehaviour {
	
	// Variables for the speed movement of the Woombat
	float speed = -1f;
	float smashedSpeed = 0.0f;
	bool facingRight = true;
	
	// Variables for the smashing behaviour when dying
	bool smashed = false;
	float smashRadius = 0.1f;
	public Transform smashCheck;
	public LayerMask whatCanSmash;
	
	// Variables for flipping when walls are hit
	bool wallTouchedLeft = false;
	float touchRadius = 0.001f;
	public Transform wallCheckLeft;
	public LayerMask whatCanTouch;
	
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
		if (smashed)
		{
			rigid2D.velocity = new Vector2(smashedSpeed, rigid2D.velocity.y);
			Destroy(gameObject, 0.5f);
		}
		else if (wallTouchedLeft)
		{
			speed = speed * -1;
			Flip();
		}
	}
	
	void FixedUpdate () {
		
		if(!smashed)
		{
			// Moving left or right
			rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
			anim.SetFloat("Speed", Mathf.Abs(speed));
			
			// Smashed detector
			smashed = Physics2D.OverlapCircle(smashCheck.position, smashRadius, whatCanSmash);
			anim.SetBool("Smashed", smashed);
			
			// Tocuh detector
			wallTouchedLeft = Physics2D.OverlapCircle(wallCheckLeft.position, touchRadius, whatCanTouch);
		}		
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
