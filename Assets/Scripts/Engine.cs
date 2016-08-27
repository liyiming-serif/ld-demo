using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * Engine responsible for tracking global variables and constants, managing the camera target,
 * and overseeing the player mode.
 */
public class Engine : MonoBehaviour{
	public static Engine singleton;

	[SerializeField]private GameObject mainCameraRig; //reference Camera.main's grandfather
	private AutoCamera camScript;

    // Use this for initialisation
    void Awake(){
		singleton = this;
		Object.DontDestroyOnLoad(singleton); //game engine preserves game state between scenes

		camScript = mainCameraRig.GetComponent<AutoCamera>();
	}

    void Update()
    {

    }

	/** Sets the camera's offset to whatever called this function's offset
	 *  if the camera is focused on the caller.
	 */
	public void ChangeCameraOffset(GameObject caller, Vector2 off) {
		if(caller.transform == camScript.Target)
			camScript.SetOffset(off);
	}

	public void Reload() {
	}
}
