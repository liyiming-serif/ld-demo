using UnityEngine;
using System.Collections;

/**
 * Basic functionality for 2d platformer actor. Attach to all actors in 2d platformer.
 * Can walk, jump, and hop on sloped surfaces.
 * Can be extended to flying and crawling characters.
 * REQUIRES gameObject to have a GroundCheck as its child.
 */
[RequireComponent(typeof (Rigidbody2D))]

public class Actor : MonoBehaviour {

	//helper members
	bool onGround;
	Rigidbody2D body;
	public enum Face{Left, Right};
	[HideInInspector] public Face dir;
	Animator animation;

	//grants spatial awareness
	Transform groundCheck; //check if this actor is on ground

	public float jumpHeight;
	[Range(0,1)] public float hopHeight;
	public float moveSpeed;
	public Vector2 cameraOffset; //a buffer for the following camera.

	// Use this for initialization
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("GroundCheck");
		onGround = false;
		dir = Face.Right;
		animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Solid"));

	}

	/**Called every FixedUpdate of the controller
	 */
	public void Move(float xTilt) { //NOTE: xTilt must be calculated for enemy AI.
		body.velocity = new Vector2(xTilt * moveSpeed, body.velocity.y);
		if(xTilt != 0) {
			animation.SetTrigger("startWalk");
		} else {
			animation.SetTrigger("stopWalk");
		}

		//Turn Around
		if(Mathf.Sign(xTilt) != Mathf.Sign(transform.localScale.x) && xTilt != 0) {
			Vector3 temp = transform.localScale;
			temp.x *= -1;
			transform.localScale = temp;

			cameraOffset.x *= -1;
			Engine.singleton.ChangeCameraOffset(gameObject, cameraOffset);

			if(temp.x > 0)
				dir = Face.Right;
			else if(temp.x < 0)
				dir = Face.Left;
		}
	}

	public void Jump() {
		if(onGround) {
			body.velocity = new Vector2(body.velocity.x, jumpHeight);
		}
	}

	/** Dampens jump, turning it into a short hop.
	 */
	public void Hop() {
		if(body.velocity.y > jumpHeight * hopHeight) {
			body.velocity = new Vector2(body.velocity.x, jumpHeight*hopHeight);
		}
	}
}
