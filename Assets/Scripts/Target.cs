using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	[SerializeField] protected float innerSight = 80f;
	[SerializeField] protected float outerSight = 200f;
	protected CircleCollider2D sightTrigger;
	[SerializeField] protected GameObject sightParticle;
	protected GameObject[] sightParticles;

	public bool dead { get; set;}
	protected bool panicking;

	void Start()
	{
		sightTrigger = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
		sightTrigger.radius = outerSight;
		sightTrigger.isTrigger = true;
		drawSights(outerSight);

		panicking = false;
		dead = false;
	}

	//death sequence when hit by arrow
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Projectile") {
			Die();
		}
		else if(other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player") {
			Panic(other.gameObject.transform.position);
		}
	}

	void drawSights(float rad)
	{
		//calculate amount of particles needed
		float circ = Mathf.PI*2f*rad;
		int parts = Mathf.RoundToInt(circ / 32f);
		sightParticles = new GameObject[parts];

		//place particles
		for(int i = 0; i <= parts; i++) {
			GameObject part = Instantiate(sightParticle, transform) as GameObject;
			float angle = (float) i/parts *2f*Mathf.PI;
			part.transform.localPosition = new Vector2(rad*Mathf.Sin(angle),rad*Mathf.Cos(angle));
			sightParticles[i] = part;
			//Instantiate(sightParticle, pos, Quaternion.identity, transform);
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
		if(dead == false) {
			panicking = true;
			Debug.Log("?");
		}
	}
}
