using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CircleCollider2D))]
[RequireComponent(typeof (CircleCollider2D))]

public class Target : MonoBehaviour {

	[SerializeField] private CircleCollider2D innerSight;
	[SerializeField] private CircleCollider2D outerSight;


	//death sequence when hit by arrow
	protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Projectile") {
			Die();
		}
		else if(other.gameObject.tag == "Player") {
			Panic();
		}
	}

	protected virtual void Die(){
		Destroy(gameObject);
	}

	protected virtual void Panic() {
	}
}
