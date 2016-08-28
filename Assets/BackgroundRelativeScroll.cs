using UnityEngine;
using System.Collections;

public class BackgroundRelativeScroll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        // Move the object to the right relative to the camera 1 unit/second.
        transform.Translate(Vector3.left *Time.deltaTime, Camera.main.transform);
    }
}
