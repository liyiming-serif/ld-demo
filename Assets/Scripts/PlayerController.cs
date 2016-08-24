using UnityEngine;
using System.Collections;

/** Basic controls for a side-scrolling protagonist. Attach to any GameObject controlled by the player.
 *  Implements: moving and jumping
 *  Requires: Rigidbody2D, Collider2D
 */
public class PlayerController : MonoBehaviour {

	enum Face {Left, Right};
	Face direction;
	bool onGround;
	Rigidbody2D body;
	Animator animator;
	AudioSource speaker;

	//bad coding practice, I apologize :(
	public float jumpHeight;
	public float moveAcceleration;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		body = gameObject.GetComponent<Rigidbody2D>();
		direction = Face.Right;
		if(direction == Face.Right)
			Debug.Log("!");
	}
	
	// Update is called once per frame
	void Update () {

		//Jump!
		if(Input.GetButtonDown("JumpKey"))
			body.velocity = new Vector2(body.velocity.x, jumpHeight);
	}

	// FixedUpdate handles calls to Rigidbody2D.AddForce
	void FixedUpdate(){
		//Move!
		float m = Input.GetAxis("Horizontal");
		if(Mathf.Abs(m * body.velocity.x) < moveSpeed)
			body.AddForce(Vector2.right * m * moveAcceleration);
		if(Mathf.Abs(body.velocity.x) > moveSpeed)
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x), body.velocity.y);
	}
}
