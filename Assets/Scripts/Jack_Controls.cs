using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Controls : MonoBehaviour {

    // Variables for the speed movement of the character
	float maxSpeed = 2f;
	bool facingRight = true;
	float move = 1f;

    // Variables for the ground check and jumping system
    bool grounded = false;
    float groundRadius = 0.1f;
	float jumpForce = 250f;
	float smashJump = 40f;
    public Transform groundCheck;
	public LayerMask whatIsGround;
    
	// Variables for checking if an enemy has been stomped
	bool smashed = false;
	float smashRadius = 0.1f;
	public Transform smashCheck;
	public LayerMask whatCanSmash;
	
	// Variables for checking if a wall is being touched
	bool walled = false;
	float walledRadius = 0.1f;
	public Transform walledCheck;
	public LayerMask whatIsWall;
	float smallWallJumpForce = 280f;
	float midWallJumpForce = 480f;
	float strongWallJumpForce = 600f;
	
	// Variables for checking time in ground
	float timer = 0f;
	bool TimerStarted = false;	    
	float TimeIWantInSeconds = 0.7f;
	
	// Variables for cheking in an enemy hit us
	bool killedLeft = false;
	bool killedRight = false;
	float killedRadius = 0.1f;
	public Transform killedCheckRight;
	public Transform killedCheckLeft;
	public LayerMask whatCanKill;
    
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
        // Checking variables for jumping
	    if (grounded && Input.GetKeyDown(KeyCode.Space))
	    {
		    anim.SetBool("Ground", false);
	    	rigid2D.AddForce(new Vector2(0 , (jumpForce)));
	    }
        
	    // Cheking if a woombat has ben smashed
	    if (smashed)
	    {
	    	rigid2D.AddForce(new Vector2(0, smashJump));
	    }
	    
	    // Cheking if the player is sliding in a wall
	    else if (walled && Input.GetKeyDown(KeyCode.Space) && facingRight && !grounded)
	    {
	    	Flip();
	    	move = -1; 	
	    	if (rigid2D.velocity.y >= -2.0)
	    	{
	    		rigid2D.AddForce(new Vector2(0, smallWallJumpForce));
	    	}
	    	else if(rigid2D.velocity.y <= -2.01 && rigid2D.velocity.y >= -5.99)
	    	{
	    		rigid2D.AddForce(new Vector2(0, midWallJumpForce));
	    	}
	    	else if(rigid2D.velocity.y <= -6.0)
	    	{
	    		rigid2D.AddForce(new Vector2(0, strongWallJumpForce));
	    	}
	    }
	    
	    else if(walled && Input.GetKeyDown(KeyCode.Space) && !facingRight && !grounded)
	    {
	    	Flip();
	    	move = 1;
		    if (rigid2D.velocity.y >= -2.0)
	    	{
	    		rigid2D.AddForce(new Vector2(0, smallWallJumpForce));
	    	}
	    	else if(rigid2D.velocity.y <= -2.01 && rigid2D.velocity.y >= -5.99)
	    	{
	    		rigid2D.AddForce(new Vector2(0, midWallJumpForce));
	    	}
	    	else if(rigid2D.velocity.y <= -6.0)
	    	{
	    		rigid2D.AddForce(new Vector2(0, strongWallJumpForce));
	    	}
	    }
	    else if(grounded && !facingRight)
	    { 	
	    	TimerStarted = true;
	    	move = -0.5f;
	    	move = 0;
		    if(TimerStarted && timer < (TimeIWantInSeconds + 0.1))
		    {
			    timer += Time.deltaTime;
			    print(timer);
			    if (timer >= TimeIWantInSeconds)
			    {
				    move = 1;
				    TimerStarted = false;
				    timer = 0;				    
			    }
		    }		
	    }
	    else if (killedLeft || killedRight)
	    {
	    	move = 0;
	    	Destroy(gameObject);
	    }
    }

	// Update is called once per frame
	void FixedUpdate () {
			
        // Jumping
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
		anim.SetFloat("VerticalSpeed", rigid2D.velocity.y);

        // Moving left or right
		rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);
		anim.SetFloat("Speed", Mathf.Abs(move));

        // Flipping sides
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
	        Flip();
            
		// Smashed detector
		smashed = Physics2D.OverlapCircle(smashCheck.position, smashRadius, whatCanSmash);
		
		// Walled detector
		walled = Physics2D.OverlapCircle(walledCheck.position, walledRadius, whatIsWall);
		anim.SetBool("WalledRight", walled);
		
		// Kill detector
		killedLeft = Physics2D.OverlapCircle(killedCheckLeft.position, killedRadius, whatCanKill);
		killedRight = Physics2D.OverlapCircle(killedCheckRight.position, killedRadius, whatCanKill);
		
		

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
