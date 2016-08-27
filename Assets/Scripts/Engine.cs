using UnityEngine;
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


    // to determine the mouse position, we need a raycast
    private Vector2 downPosition;

    // position of the raycast on the screen
    private float posX;
    private float posY;

    // Use this for initialisation
    void Awake()
    {
        singleton = this;
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

    public void Update()
    {
        //Cal the distance = Mouse drag - Mouse down 
        if (Input.GetMouseButtonDown(0))
        {
            // Record the pos of Mouse down
            downPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currMousePos = Input.mousePosition;
            Vector2 dragDistance =  currMousePos - downPosition;
            float angleZ = Mathf.Atan2(dragDistance.y, dragDistance.x) * Mathf.Rad2Deg;
            if(angleZ > -60)
            {
                playersBow.PullString(dragDistance.magnitude, angleZ);
                if (playersActor.dir == Actor.Face.Right)
                {
                    playersActor.TurnAround();
                }
            }
            else if (angleZ < -120)
            {
                playersBow.PullString(dragDistance.magnitude, angleZ + 180);
                if (playersActor.dir == Actor.Face.Left)
                {
                    playersActor.TurnAround();
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            float length = Input.mousePosition.magnitude / 3f;
            length = Mathf.Clamp(length, 0, 1);
            Vector3 initVelocity = Quaternion.Euler(new Vector3(playersBow.transform.rotation.eulerAngles.x, playersBow.transform.rotation.eulerAngles.y, playersBow.transform.rotation.eulerAngles.z)) * new Vector2(1000f, 0);
            // FireArrow
            Arrow newArrow = ((GameObject) Instantiate(Resources.Load("arrowPrefab"), playersBow.transform.position , playersBow.transform.rotation)).GetComponent<Arrow>();
            newArrow.FireArrow(initVelocity, playersBow.gameObject);
            playersBow.FireArrow();
        }
    }
}
