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
    private ChallengeScript challengesc;

    [SerializeField]
    private Bow playersBow;
    [SerializeField]
    private Actor playersActor;
	private Vector2 originalOffset;
    [SerializeField]
    private AutoCamera mainCameraRig;


    public static float[] randValues;
    public int numRandV;

    // position of the raycast on the screen
    private float posX;
    private float posY;

    // for updating the Bow Sprite
    private int frameNo;
    private int count;
    private float timer;
    private bool listening;
    private float strength;

	private GameObject[] targets;
    public int arrowsUsed;

	private AudioSource speaker;

    // Use this for initialisation
    void Awake()
    {
		originalOffset = playersActor.cameraOffset;
        arrowsUsed = 0;
        singleton = this;
		targets = GameObject.FindGameObjectsWithTag("Target");
        numRandV = 4;
        randValues = new float[numRandV];
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"));
		speaker = GetComponent<AudioSource>();
    }

    /** Sets the camera's offset to whatever called this function's offset
	 *  if the camera is focused on the caller.
	 */
    public void ChangeCameraOffset(GameObject caller, Vector2 off)
    {
        if (caller.transform == mainCameraRig.Target)
            mainCameraRig.SetOffset(off);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bow"></param>
    public void Reload(GameObject bow)
    {

    }

    /// <summary>
    /// Tell Challenge script that they need to restart the challenge
    /// </summary>
    public void ChallengeFailed()
    {
        challengesc.ChallengeFailed();
    }

    public void AnimateBow(float timepassed)
    {
        if (timepassed < 0.1f)
        {
            frameNo = 0;
        }
        else if (timepassed < 0.2f)
        {
            frameNo = 1;
        }
        else if (timepassed < 0.3f)
        {
            frameNo = 2;
        }
        else if (timepassed < 0.4f)
        {
            frameNo = 3;
        }
        else if (timepassed < 0.5f)
        {
            frameNo = 4;
        }
        else if (timepassed < 0.6f)
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
            timer = 0.0f;
            listening = true;
			/*if(playersActor != null)
			{
				if(playersActor.dir == Actor.Face.Right)
					playersActor.cameraOffset = new Vector2(200f,0f);
				else
					playersActor.cameraOffset = new Vector2(-200f,0f);
				ChangeCameraOffset(playersActor.gameObject, playersActor.cameraOffset);
			}*/
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 dragDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playersBow.transform.position;
            float angleZ = Mathf.Atan2(dragDistance.y, dragDistance.x) * Mathf.Rad2Deg;
            if (timer < 3f) {
                if ((angleZ >= 90 && angleZ <= 180) || (angleZ < -110 && angleZ > -180))
                {   
                    AnimateBow(timer);
                    if (playersActor.dir == Actor.Face.Right)
                    {
                        playersActor.TurnAround();
                    }
                    playersBow.PullString(frameNo, angleZ + 180);
                }
                else if (angleZ > -70 && angleZ < 90)
                {
                    AnimateBow(timer);
                    if (playersActor.dir == Actor.Face.Left)
                    {
                        playersActor.TurnAround();
                    }
                    playersBow.PullString(frameNo, angleZ);
                }
                else
                {
                    frameNo = 0;
                    playersBow.FireArrow();
                }
                timer += Time.deltaTime;
            }else if(listening)
            {
                listening = false;
                //TellEveryoneTheArrowNeedsToBeFired();
                playersBow.FireArrow();
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (listening)
            {
                TellEveryoneTheArrowNeedsToBeFired();
            }
			if(playersActor != null)
			{
				if(playersActor.dir == Actor.Face.Right)
					playersActor.cameraOffset = new Vector2(originalOffset.x,originalOffset.y);
				else
					playersActor.cameraOffset = new Vector2(-originalOffset.x,originalOffset.y);
				ChangeCameraOffset(playersActor.gameObject, playersActor.cameraOffset);
			}
        }
    }

    public void TellEveryoneTheArrowNeedsToBeFired()
    {
        if (frameNo > 2)
        {
            Vector2 dragDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playersBow.transform.position;
            float angleZ = Mathf.Atan2(dragDistance.y, dragDistance.x) * Mathf.Rad2Deg;
            float constSpeed = -6.0f;
            if (angleZ > -70 && angleZ < 90)
            {
                constSpeed = 6.0f;
            }
            timer = Mathf.Min(timer, 0.7f);
            Vector3 initVelocity = Quaternion.Euler(playersBow.transform.rotation.eulerAngles) * new Vector2(constSpeed * 250.0f * (timer / 0.7f), 0);
            // FireArrow
            Arrow newArrow = ((GameObject)Instantiate(Resources.Load("arrowPrefab"), playersBow.transform.position, playersBow.transform.rotation)).GetComponent<Arrow>();
            newArrow.FireArrow(initVelocity, playersBow.gameObject);
            arrowsUsed += 1;
        }
        playersBow.FireArrow();
        listening = false;

    }

    public void AlertAll(Vector2 landingSpot){
		foreach(GameObject t in targets) {
			t.BroadcastMessage("Alert", landingSpot);
		}
	}
}
