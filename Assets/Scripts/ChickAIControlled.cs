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

	// Use this for initialization
	void Awake () {
		actor = GetComponent<Actor>();
		leftCheck = transform.Find("LeftCheck");
		rightCheck = transform.Find("RightCheck");
		if(leftCheck == null || rightCheck == null) {
			stationary = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onCollisionEnter2D(Collision2D other){
			
	}

	protected override void Die()
	{
	}

	protected override void Panic()
	{
	}
}
