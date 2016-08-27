using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	[SerializeField]
	int value; //points player gets for hitting this target
	AudioSource speaker;
	[SerializeField]
	AudioClip thunk; //sound when ANY projectile hits this
	// Use this for initialization
	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//increment points when hit by arrow
	void OnCollisionEnter2D(Collision2D other)
	{
		speaker.PlayOneShot(thunk);
		if(other.gameObject.tag == "Projectile") {
			//Engine.singleton.score += value;
		}
	}
}
