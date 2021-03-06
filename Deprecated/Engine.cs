﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * Engine responsible for tracking global variables and constants, managing the camera target,
 * and overseeing the player mode.
 */
public class Engine : MonoBehaviour
{

    public static Engine singleton;

    [SerializeField]
    private Bow playersBow;
    [SerializeField]
    private Actor playersActor;
    [SerializeField]
    private AutoCamera mainCameraRig;


    public static float[] randValues;
    public int numRandV;

    // to determine the mouse position, we need a raycast
    private Vector2 downPosition;

    // position of the raycast on the screen
    private float posX;
    private float posY;

    // 
    private int frameNo;
    private int count;

	private GameObject[] targets;

    // Use this for initialisation
    void Awake()
    {
        singleton = this;
		targets = GameObject.FindGameObjectsWithTag("Target");
        numRandV = 4;
        randValues = new float[numRandV];
        //Object.DontDestroyOnLoad(singleton); //game engine preserves game state between scenes
    }

    /** Sets the camera's offset to whatever called this function's offset
	 *  if the camera is focused on the caller.
	 */
    public void ChangeCameraOffset(GameObject caller, Vector2 off)
    {
        if (caller.transform == mainCameraRig.Target)
            mainCameraRig.SetOffset(off);
    }

    public void Reload(GameObject bow)
    {

    }

    public void AnimateBow(float distance)
    {
        if (distance < 100)
        {
            frameNo = 0;
        }
        else if (distance < 150)
        {
            frameNo = 1;
        }
        else if (distance < 200)
        {
            frameNo = 2;
        }
        else if (distance < 250)
        {
            frameNo = 3;
        }
        else if (distance < 300)
        {
            frameNo = 4;
        }
        else if (distance < 350)
        {
            frameNo = 5;
        }
        else
        {
            if (count == 0 && frameNo != 6)
            {
                frameNo = 6;
                count += 1;
            }
            else if (count == 5 && frameNo != 7)
            {
                frameNo = 7;
                count += 1;
            }
            else if (count > 10)
            {
                count = 0;
            }
            else
            {
                count += 1;
            }
        }
    }

    public void Update()
    {
        //Gen new random values
        for(int i = 0; i < randValues.Length; i++)
        {
           randValues[i] = Random.value;
        }
        
        //Cal the distance = Mouse drag - Mouse down 
        if (Input.GetMouseButtonDown(0))
        {
            // Record the pos of Mouse down
            downPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currMousePos = Input.mousePosition;
            Vector2 dragDistance = currMousePos - downPosition;
            float angleZ = Mathf.Atan2(dragDistance.y, dragDistance.x) * Mathf.Rad2Deg;
            if ((angleZ > -90) && (angleZ < 70))
            {
                AnimateBow(dragDistance.magnitude);
                playersBow.PullString(frameNo, angleZ);
                if (playersActor.dir == Actor.Face.Right)
                {
                    playersActor.TurnAround();
                }
            }
            else if (((angleZ <= -90) && (angleZ >= -180)) ||  ((angleZ > 110) && (angleZ < 180)))
            {
                AnimateBow(dragDistance.magnitude);
                playersBow.PullString(frameNo, angleZ + 180);
                if (playersActor.dir == Actor.Face.Left)
                {
                    playersActor.TurnAround();
                }
            }
            else
            {
                frameNo = 0;
                playersBow.FireArrow();
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (frameNo > 2)
            {
                Vector2 currMousePos = Input.mousePosition;
                Vector2 dragDistance = currMousePos - downPosition;
                float angleZ = Mathf.Atan2(dragDistance.y, dragDistance.x) * Mathf.Rad2Deg;
                float constSpeed = 4.0f;
                if ((angleZ > -90) && (angleZ < 70))
                { 
                    constSpeed = -4.0f;
                }
                Vector3 initVelocity = Quaternion.Euler(playersBow.transform.rotation.eulerAngles) * new Vector2(constSpeed * Mathf.Min(dragDistance.magnitude, 350.0f), 0);
                // FireArrow
                Arrow newArrow = ((GameObject)Instantiate(Resources.Load("arrowPrefab"), playersBow.transform.position, playersBow.transform.rotation)).GetComponent<Arrow>();
                newArrow.FireArrow(initVelocity, playersBow.gameObject);
            }
            playersBow.FireArrow();
        }
    }

	public void AlertAll(Vector2 landingSpot){
		foreach(GameObject t in targets) {
			t.BroadcastMessage("Alert", landingSpot);
		}
	}
}
