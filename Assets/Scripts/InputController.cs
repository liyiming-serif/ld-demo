using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    private float moveSpeed = 2f;
    private float maxMoveSpeed = 4f;
    private float jumpHeight = 10f;

    private float bowCharge = 0;
    private float bowBuild = 0.1f;
    private float bowMax = 10f;

    Rigidbody2D body;


	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.A))
        {
            if (body.velocity.x > -maxMoveSpeed)
                body.velocity += new Vector2(-moveSpeed, 0);
        }


        if (Input.GetKey(KeyCode.D))
        {
            if (body.velocity.x < maxMoveSpeed)
                body.velocity += new Vector2(moveSpeed, 0);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            body.velocity += new Vector2(0, jumpHeight);
        }
        
        if (Input.GetMouseButton(0))
        {
            if (bowCharge < bowMax)
                bowCharge += bowBuild;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 tmp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            FireArrow(body.position, Camera.main.ScreenToWorldPoint(tmp), bowCharge);
            bowCharge = 0;
        }

        
    }

    private void FireArrow(Vector2 origin, Vector2 dest, float power)
    {
        // TODO: insert your arrow shenanigans here
        Debug.Log("Shooting arrow from " + origin + " to " + dest + " with " + power + " power.");
    }
}
