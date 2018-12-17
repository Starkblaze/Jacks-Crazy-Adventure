using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoombatBehaviour : MonoBehaviour {
	
	// Variables for the speed movement of the Woombat
	public float speed = 1f;
	public float smashedSpeed = 0.5f;
	bool facingRight = true;
	
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
		
		//Checking variables for jumping
		if (smashed)
		{
			rigid2D.velocity = new Vector2(smashedSpeed, rigid2D.velocity.y);
			smashed = true;
			Destroy(gameObject, 1);
		}
		
	}
	
	void FixedUpdate () {
		
		
		if(!smashed)
		{
			// Moving left or right
			rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
			anim.SetFloat("Speed", Mathf.Abs(speed));
			
			// Smashed behaviour
			smashed = Physics2D.OverlapCircle(smashCheck.position, smashRadius, whatCanSmash);
			anim.SetBool("Smashed", smashed);
		}		
	}
}
