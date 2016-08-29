using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Actor))]
/**
 * Controls the chick target. The chick has 2 AI modes: moving and stationary.
 * Turn on stationary by childing LeftCheck and RightCheck.
 */
public class ChickAIControlled : Target {

	[SerializeField] private bool stationary; //flag checking for AI mode.
	private Actor actor;
	Transform sideCheck; //check the space directly in front of chick
	private Animator animate;
	[SerializeField] private float flightSpeed;

	// Use this for initialization
	void Awake () {
		actor = GetComponent<Actor>();
		sideCheck = transform.Find("SideCheck");
		if(sideCheck == null)
			stationary = true;
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(stationary == false && panicking == false && dead == false)
		{
			if(Physics2D.Linecast(transform.position, sideCheck.position, 1 << LayerMask.NameToLayer("Solid")))
				actor.TurnAround();
			
			if(actor.dir == Actor.Face.Right) {
				actor.Move(1f);
			}
			else if(actor.dir == Actor.Face.Left) {
				actor.Move(-1f);
			}
		}
	}

	protected override void Die()
	{
		dead = true;
		actor.Fly(Vector2.zero);
		animate.SetBool("die", true);
		sightTrigger.enabled = false;
		GetComponent<Rigidbody2D>().gravityScale *= actor.fastFall;
		DestroySights();
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
			StartCoroutine(DestroySightsDelayed());
		}
	}

	IEnumerator DestroySightsDelayed()
	{
		ChangeSights();
		yield return new WaitForSeconds(.7f);
		DestroySights();
	}
}
