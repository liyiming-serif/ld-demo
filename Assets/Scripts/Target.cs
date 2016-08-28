using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	[SerializeField] protected float innerSight = 80f;
	[SerializeField] protected float outerSight = 200f;
	protected CircleCollider2D sightTrigger;
	protected bool dead;

	void Start()
	{
		sightTrigger = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
		sightTrigger.radius = outerSight;
		sightTrigger.isTrigger = true;
		dead = false;
	}

	//death sequence when hit by arrow
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Projectile") {
			Die();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
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
		dead = true;
	}

	protected virtual void Panic(Vector2 landingSpot) {
		if(dead==false)
			Debug.Log("?");
	}
}
