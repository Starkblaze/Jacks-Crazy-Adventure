using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Controls : MonoBehaviour {

    // Variables for the spped movement of the character
    public float maxSpeed = 5f;
    bool facingRight = true;

    // Variables for the 
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;

    Animator anim;
    Rigidbody2D rigid2D;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

        rigid2D = GetComponent<Rigidbody2D>();
    }
	
    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigid2D.AddForce(new Vector2(0, jumpForce));
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rigid2D.velocity.y);

        float move = Input.GetAxis("Horizontal");

        rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
