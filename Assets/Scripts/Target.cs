using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	//SIGHTS NO LONGER SUPPORT ANIMATIONS!
	[SerializeField] protected float innerSight = 80f;
	[SerializeField] protected float outerSight = 200f;
	protected CircleCollider2D sightTrigger;
	[SerializeField] protected GameObject sightParticle;
	[SerializeField] protected Sprite partTriggerImg;
	[SerializeField] protected Sprite parentTriggerImg;
	protected Sprite partWarningImg;
	protected Sprite parentWarningImg;
	protected Transform particleParent;

	public bool dead { get; set;}
	protected bool panicking;

	void Start()
	{
		sightTrigger = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
		sightTrigger.radius = outerSight;
		sightTrigger.isTrigger = true;
		particleParent = transform.Find("ParticleParent");
		if(particleParent != null && sightParticle != null) {
			drawSights(outerSight);
		}

		panicking = false;
		dead = false;
	}

	void Update()
	{
		//rotate sights
		if(particleParent != null && sightParticle != null)
			particleParent.Rotate(new Vector3(0, 0, 1));
	}

	//death sequence when hit by arrow
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Projectile") {
			Die();
		}
		else if(other.gameObject.tag == "Player" || other.gameObject.tag == "Target") {
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
		int parts = Mathf.RoundToInt(circ / 64f);

		//generate, scale, and place particles
		for(int i = 0; i <= parts; i++) {
			GameObject part = Instantiate(sightParticle, particleParent) as GameObject;
			float angle = (float) i/parts *2f*Mathf.PI;
			part.transform.localPosition = new Vector2(1.05f*Mathf.Sin(angle),1.05f*Mathf.Cos(angle));
			part.transform.localScale = new Vector2(1/rad, 1/rad);
			part.transform.localEulerAngles = new Vector3(0,0,-angle*360f/(2f*Mathf.PI));
		}

		//scale particle parent
		particleParent.localScale = new Vector2(rad,rad);

		//initialize warning sprites
		partWarningImg = particleParent.GetComponentInChildren<SpriteRenderer>().sprite;
		parentWarningImg = particleParent.GetComponent<SpriteRenderer>().sprite;
	}

	protected void changeSights(bool original=false)
	{
		if(particleParent != null && sightParticle != null) {
			if(original == false) {
				particleParent.GetComponent<SpriteRenderer>().sprite = parentTriggerImg;
				foreach(Transform part in particleParent.transform) {
					part.GetComponent<SpriteRenderer>().sprite = partTriggerImg;
				}
			}
			else {
				particleParent.GetComponent<SpriteRenderer>().sprite = parentWarningImg;
				foreach(Transform part in transform) {
					part.GetComponent<SpriteRenderer>().sprite = partWarningImg;
				}
			}
		}
	}

	protected void destroySights()
	{
		Destroy(particleParent.gameObject);
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
