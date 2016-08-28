using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Actor))]
/**
 * Controls the chick target. The chick has 2 AI modes: moving and stationary.
 * Turn on stationary by childing LeftCheck and RightCheck.
 */
public class ChickAIControlled : Target {

	private bool stationary; //flag checking for AI mode.
	private Actor actor;
	Transform leftCheck;
	Transform rightCheck;
	private Animator animate;
	[SerializeField] private float flightSpeed;

	// Use this for initialization
	void Awake () {
		actor = GetComponent<Actor>();
		leftCheck = transform.Find("LeftCheck");
		rightCheck = transform.Find("RightCheck");
		if(leftCheck == null || rightCheck == null)
			stationary = true;
		else
			stationary = false;
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(stationary == false) {
			if(actor.dir == Actor.Face.Right)
				actor.Move(1f);
			else if(actor.dir == Actor.Face.Left)
				actor.Move(-1f);
		}
	}

	protected override void Die()
	{
		dead = true;
		actor.Fly(Vector2.zero);
		animate.SetBool("die", true);
		sightTrigger.enabled = false;
		GetComponent<Rigidbody2D>().gravityScale *= actor.fastFall;
	}

	protected override void Panic(Vector2 incursion)
	{
		if(dead == false) {
			Vector2 traj;
			if((transform.position.x - incursion.x) > 0)
				traj = new Vector2(flightSpeed, flightSpeed);
			else
				traj = new Vector2(-flightSpeed, flightSpeed);
			actor.Fly(traj);
			sightTrigger.enabled = false;
		}
	}
}
