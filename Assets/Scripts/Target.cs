using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	[SerializeField]
	int value; //points player gets for hitting this target

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Projectile") {
			Engine.score += value;

		}
	}
}
