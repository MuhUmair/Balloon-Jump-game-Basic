using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

    public float fallingSpeed = -0.1f;
    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, fallingSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        //rb2d.rotation += 1;
    }
    
}
