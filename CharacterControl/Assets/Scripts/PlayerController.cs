using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    bool faceRight = true;
    private Rigidbody2D rb2d;

    bool grounded = false;
    public Transform groundedCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGroung;
    public float jumpForce = 700.0f;
    private Animator anim;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpMe();
        }
	}

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundedCheck.position, groundRadius, whatIsGroung);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rb2d.velocity.y);

        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
        //print(" Mathf.Abs(move) " + Mathf.Abs(move));
        anim.SetFloat("Speed", Mathf.Abs(move));
        
        if (move > 0 && !faceRight)
            Flip();
        else if (move < 0 && faceRight)
            Flip();


    }

    void Flip() {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ballon") {
            jumpMe();
            collision.collider.attachedRigidbody.position = new Vector2(-15, -25);
            //Destroy(collision.gameObject);
        }
        
    }

    void jumpMe() {
        anim.SetBool("Ground", false);
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(new Vector2(0, jumpForce));
    }
}
