using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour{
	public static Engine singleton;
	public static int score{ get; set;}
	public static int ammo{ get; set; }

	void Awake(){
		singleton = this;
		score = 0;
		Object.DontDestroyOnLoad(singleton); //game engine preserves game state between scenes
	}

	public void Notch(){
		Debug.Log("notched");
		ammo--;
	}
}
