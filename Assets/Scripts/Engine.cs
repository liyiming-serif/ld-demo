using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour{
	public static Engine singleton;
	public static int score{ get; set;} // score
	public static int ammo{ get; set; } // amount of arrows

    [SerializeField]
    Bow bow;

    States gameState;

	void Awake(){
		singleton = this;
		score = 0;
        ammo = 20;
        gameState = States.menu;
		Object.DontDestroyOnLoad(singleton); //game engine preserves game state between scenes
	}

	public void Notch(){
		Debug.Log("notched");
		ammo--;
	}
    
    //
    // void ResetGame()
    //
    // Resets data
    void ResetGame()
    {
        score = 0;
        ammo = 20;
        if(GameObject.Find("arrow") == null)
        {
            bow.SpawnArrow();   
        }
    }


    //
    // public void setPoints()
    //
    // This method is called from the arrow script
    // and sets the points
    //

    public void setPoints(int points)
    {
        score += points;
    }

    //
    // public void StartGame()
    // 
    // starts game can be triggered by main menu
    //
    public void StateGame()
    {
        gameState = States.game;
    }


}
