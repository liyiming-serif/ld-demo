﻿using UnityEngine;
using System.Collections;

/** Arrow class. Requires a rigidbody2d and a sprite renderer initally attached to gameObject.
 */
public class Arrow : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 v; //rigidbody2d's instantaneous velocity
    GameObject bowOrigin; //the bow that fired this arrow instance

    bool hit; //flag to check if the arrow has hit anything

	Animator animate;

    AudioSource speaker;
    [SerializeField]
    AudioClip whiz; //sound of the arrow getting launched
    [SerializeField]
    AudioClip thunk; //sound of the arrow hitting ANY barrier
	[SerializeField]
	AudioClip boing;

    // Use this for initialization
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        hit = false;
        speaker = GetComponent<AudioSource>();
		animate = GetComponent<Animator>();
    }

    //control arrow's image based on trajectory. stops when arrow sticks.
    IEnumerator TrackAngle()
    {
        while (hit == false)
        {
            v = body.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }
    }

    //Arrow constructor. This is called by bow as soon as its created.
    public void FireArrow(Vector2 initVelocity, GameObject bow)
    {
        bowOrigin = bow;
        speaker.PlayOneShot(whiz);
        body.velocity = initVelocity;
        StartCoroutine(TrackAngle());
    }

    //runs when arrow hits barrier
    void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.tag == "Barrier" || other.gameObject.tag == "Target")
        {
            hit = true;
            animate.SetTrigger("triggerWarning");
            speaker.PlayOneShot(thunk);
            Engine.singleton.Reload(bowOrigin);
            Engine.singleton.AlertAll(other.contacts[0].point);
			if(other.gameObject.tag == "Target") {
				gameObject.transform.SetParent(other.gameObject.transform);
			}
        }
		if(other.gameObject.tag != "Bouncy") {
			if(body != null) {
				Destroy(body);
				foreach(Component coll in GetComponents<Collider2D>())
					Destroy(coll);
			}
		} else {
			speaker.PlayOneShot(boing);
		}
    }
}
