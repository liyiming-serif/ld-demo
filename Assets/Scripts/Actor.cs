using UnityEngine;
using System.Collections;

/**
 * Basic functionality for 2d platformer actor. Attach to all actors in 2d platformer.
 * Can walk, jump, hop, and fastfall on sloped surfaces.
 * Can be extended to flying and crawling characters.
 * REQUIRES gameObject to have a GroundCheck as its child.
 * Every Actor MUST agree on this Animator fsm schema.
 */
[RequireComponent(typeof (Rigidbody2D))]

public class Actor : MonoBehaviour {

	public float jumpHeight;
	[Range(0,1)] public float hopHeight;
	public float moveSpeed;
	public float fastFall = 1; //gravity multiplier
	public Vector2 cameraOffset; //a buffer for the following camera.

	//helper members
	bool onGround;
	Rigidbody2D body;
	public enum Face{Left, Right};
	[HideInInspector] public Face dir;
	Animator animate;
	Transform groundCheck; //check if this actor is on ground
	[HideInInspector] public bool flying = false;
	float ogGravity; //preserves initial gravity for when flying units come back down

	// Use this for initialization
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("GroundCheck");
		onGround = false;
		dir = Face.Right;
		animate = GetComponent<Animator>();
		ogGravity = body.gravityScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!flying)
			onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Solid"));
		else
			onGround = false;

		//ANIMATOR: handle everything in the air
		if(!onGround) {
			animate.SetBool("onGround", false);
			if(body.velocity.y > 0)
				animate.SetBool("falling", false);
			else
				animate.SetBool("falling", true);
		}
		else
			animate.SetBool("onGround", true);
	}

	/**Called every FixedUpdate of the controller
	 */
	public void Move(float xTilt) { //NOTE: xTilt must be calculated for enemy AI.
		body.velocity = new Vector2(xTilt * moveSpeed, body.velocity.y);

		if(xTilt != 0) {
			//ANIMATOR: start/maintain walk cycle if on ground
			if(onGround) {
				animate.SetTrigger("startWalk");
				animate.speed = Mathf.Abs(xTilt);
			}
			else {
				animate.speed = 1f;
			}

			//Turn Around
			if(Mathf.Sign(xTilt) != Mathf.Sign(transform.localScale.x)) {
				TurnAround();
			}
		}
		else {
			animate.SetTrigger("stopWalk");
		}
	}

	public void TurnAround() {
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

	/**Wrapper method to start or stop the flying coroutine. Enter the zero vector to stop flying.
	 */
	public void Fly(Vector2 trajectory)
	{
		if(trajectory != Vector2.zero) { //start flying
			StartCoroutine("CoFly", trajectory);
		}
		else { //stop flying
			StopCoroutine("CoFly");
			flying = false;
			body.gravityScale = ogGravity;
		}
	}

	public IEnumerator CoFly(Vector2 trajectory) {
		body.gravityScale = 0;
		while(GetComponent<SpriteRenderer>().isVisible){
			flying = true;
			transform.position = new Vector2(transform.position.x + trajectory.x, transform.position.y + trajectory.y);
			yield return null;

			//Turn Around
			if(Mathf.Sign(trajectory.x) != Mathf.Sign(transform.localScale.x)) {
				TurnAround();
			}

		}
	}
}
