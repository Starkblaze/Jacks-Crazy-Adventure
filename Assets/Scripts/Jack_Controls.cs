using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Controls : MonoBehaviour {

    // Variables for the speed movement of the character
    public float maxSpeed = 5f;
    bool facingRight = true;

    // Variables for the ground check and jumping system
    bool grounded = false;
    float groundRadius = 0.1f;
    public float jumpForce = 250f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    
    // Initialize Animator and Rigidbody 
    Animator anim;
    Rigidbody2D rigid2D;

    // Initialization of the code
    void Start () {

        // Initialize Animator and Rigidbody
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
    }
	
    // Constant update of the code
    void Update()
    {
        //Checking variables for jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigid2D.AddForce(new Vector2(0, jumpForce));
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        // Jumping
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigid2D.velocity.y);

        // Moving left or right
        float move = Input.GetAxis("Horizontal");
        rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        // Flipping sides
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

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
