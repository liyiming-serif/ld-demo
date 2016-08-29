using UnityEngine;
using System.Collections;

public class MoveUpAndDown : MonoBehaviour {

    [SerializeField]
    float distance;
    [SerializeField]
    private float m_MoveSpeed = 3; // How fast the target will move to keep up the target's position


    private int numFrames;
    private int counter;

    Vector3 destPosition;

	// Use this for initialization
	void Start () {
        numFrames = (int)(distance / (Time.deltaTime * m_MoveSpeed));
        counter = 0;
 	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(counter < numFrames)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * m_MoveSpeed), transform.position.z);
            counter += 1;
        }else if(counter < 2 * numFrames)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * m_MoveSpeed), transform.position.z);
            counter += 1;
        }
        else if(counter == 2 * numFrames)
        {
            counter = 0;
        }
        else
        {
            Debug.Log("Not supposed to reach here.");
        }
    }
}
