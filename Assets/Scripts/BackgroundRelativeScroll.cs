using UnityEngine;
using System.Collections;

public class BackgroundRelativeScroll : MonoBehaviour
{

    private float timepassed;
    private float offset;

    // Use this for initialization
    void Start()
    {
        offset = 100;
        timepassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x + 300 - timepassed, transform.position.y, transform.position.z);
        // Move the object to the left relative to the camera 1 unit/second.
        //transform.position.Set(Camera.main.transform.position.x - timepassed, transform.position.y, transform.position.z);
        timepassed += Time.deltaTime;
    }
}