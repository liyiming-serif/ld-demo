using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	//Rigidbody2D rigidbody;
	Vector2 v; //velocity once rigidbody is attached
	//flags to track the state of the arrow
	bool notched;
	bool stuck;

	// Use this for initialization
	void Start() {
		notched = true;
		stuck = false;
	}
	
	// Update is called once per frame
	void Update() {
		//listen for when arrow has been fired
		if(gameObject.GetComponent<Rigidbody2D>()!=null && notched==true) {
			notched = false;
			StartCoroutine(TrackAngle());
		}
	}

	IEnumerator TrackAngle() {
		while(stuck == false) {
			v = gameObject.GetComponent<Rigidbody2D>().velocity;
			float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
			gameObject.transform.eulerAngles = new Vector3(0,0,angle);
			yield return null;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Barrier") {
			stuck = true;
			foreach(Component rb in gameObject.GetComponents<Rigidbody2D>())
				Destroy(rb);
			//TODO: tell engine to reload bow and decrement ammo.
		}
	}
}
