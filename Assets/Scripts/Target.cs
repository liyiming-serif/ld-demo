﻿using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	[SerializeField] private float innerSight = 80f;
	[SerializeField] private float outerSight = 200f;
	private CircleCollider2D sightTrigger;

	void Start()
	{
		sightTrigger = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
		sightTrigger.radius = outerSight;
		sightTrigger.isTrigger = true;
	}

	//death sequence when hit by arrow
	protected virtual void OnColliderEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Projectile") {
			Die();
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player") {
			Panic(other.gameObject.transform.position);
		}
	}

	public void Alert(Vector2 landingSpot) {
		float proximity = Vector2.Distance(landingSpot, transform.position);
		if(proximity <= innerSight)
			Panic(landingSpot);
	}

	protected virtual void Die(){
		Debug.Log("!");
	}

	protected virtual void Panic(Vector2 landingSpot) {
		Debug.Log("?");
	}
}
