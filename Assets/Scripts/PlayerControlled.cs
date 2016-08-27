using UnityEngine;
using System.Collections;

/**
 * Basic functionality for 2d platformer character
 * Can walk and jump and crouch on sloped surfaces.
 * Can be modified for flying and crawling characters.
 */
[RequireComponent(typeof(Actor))]
public class PlayerControlled : MonoBehaviour {

	private Actor actor;

	// Use this for initialization
	void Awake () {
		actor = GetComponent<Actor>();
	}

	// Update is called once per frame
	void Update () {

		//Jump!
		if(Input.GetButtonDown("JumpKey"))
			actor.Jump();
		//Hop!
		if(Input.GetButtonUp("JumpKey"))
			actor.Hop();
	}

	// FixedUpdate handles calls to Rigidbody2D.AddForce
	void FixedUpdate(){
		//Move!
		float m = Input.GetAxis("Horizontal");
		actor.Move(m);
	}
}
	