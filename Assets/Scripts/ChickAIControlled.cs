using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Actor))]
/**
 * Controls the chick target.
 */
public class ChickAIControlled : Target {
	

	[SerializeField] private bool stationary; //Target comes in 2 varieties: moving and stationary.
	private Actor actor;
	Transform leftCheck;
	Transform rightCheck;
	private Animator animate;
	[SerializeField] private float flightSpeed = 45;

	// Use this for initialization
	void Awake () {
		actor = GetComponent<Actor>();
		leftCheck = transform.Find("LeftCheck");
		rightCheck = transform.Find("RightCheck");
		if(leftCheck == null || rightCheck == null) {
			stationary = true;
		}
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void Die()
	{
		actor.StopCoroutine("Fly");
		actor.flying = false;
		animate.SetBool("die", true);
	}

	protected override void Panic(Vector2 incursion)
	{
		Vector2 traj = new Vector2(Mathf.Sign(transform.position.x-incursion.x),1);
		traj *= flightSpeed;
		actor.StartCoroutine("Fly", traj);
	}
}
