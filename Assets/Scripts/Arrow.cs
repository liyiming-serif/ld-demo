using UnityEngine;
using System.Collections;

/** Arrow class. Requires a rigidbody2d initally attached to gameObject.
 */
public class Arrow : MonoBehaviour {
	Rigidbody2D body;
	Vector2 v; //rigidbody2d's instantaneous velocity

	GameObject bowOrigin; //the bow that fired this arrow instance

	bool hit; //flag to check if the arrow has hit anything

	AudioSource speaker;
	[SerializeField]
	AudioClip whiz; //sound of the arrow getting launched
	[SerializeField]
	AudioClip thunk; //sound of the arrow hitting ANY barrier

	// Use this for initialization
	void Start() {
		body = gameObject.GetComponent<Rigidbody2D>();
		hit = false;
		speaker = gameObject.GetComponent<AudioSource>();
	}

	//control arrow's image based on trajectory. stops when arrow sticks.
	IEnumerator TrackAngle() {
		while(hit == false) {
			v = gameObject.GetComponent<Rigidbody2D>().velocity;
			float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
			gameObject.transform.eulerAngles = new Vector3(0,0,angle);
			yield return null;
		}
	}

	//Arrow constructor. This is called by bow as soon as its created.
	public void FireArrow(Vector2 initVelocity, GameObject bow) {
		bowOrigin = bow;
		speaker.PlayOneShot(whiz);
		body.velocity = initVelocity;
		StartCoroutine(TrackAngle());
	}

	//runs when arrow hits barrier
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Barrier") {
			hit = true;
			speaker.PlayOneShot(thunk);
			Engine.singleton.Reload(bowOrigin);
			}
		if(body != null) {
			Destroy(body);
			foreach(Component coll in GetComponents<Collider2D>())
				Destroy(coll);
		}
	}
}
