using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	Rigidbody2D body; //rigidbody once arrow is fired
	Vector2 v; //velocity once rigidbody is attached

	bool hit; //flag to check if the arrow has hit anything

	AudioSource speaker;
	[SerializeField]
	AudioClip whiz; //sound of the arrow getting launched
	[SerializeField]
	AudioClip thunk; //sound of the arrow hitting ANY barrier

	// Use this for initialization
	void Start() {
		hit = false;
		speaker = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update() {
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

	//arrow is fired from bow
	public void FireArrow(Vector2 initVelocity) {
		if(body == null) {
			speaker.PlayOneShot(whiz);
			body = gameObject.AddComponent<Rigidbody2D>();
			body.velocity = initVelocity;
			StartCoroutine(TrackAngle());
		}
	}

	//runs when arrow hits barrier
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Barrier") {
			hit = true;
			speaker.PlayOneShot(thunk);
			Engine.singleton.Notch();
			if(body != null) {
				Destroy(body);
			}
		}
	}
}
