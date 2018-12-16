using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoombatBehaviour : MonoBehaviour {
	
	// Variables for the speed movement of the Woombat
	public float speed = 5f;
	bool facingRight = true;
	
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
		
	}
	
	void FixedUpdate () {
		
		// Moving left or right
		rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
		anim.SetFloat("Speed", Mathf.Abs(speed));
		
	}
}
