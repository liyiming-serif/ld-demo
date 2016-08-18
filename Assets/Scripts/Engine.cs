using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Engine : MonoBehaviour{
	public static Engine singleton;
	
    public int score{ get; set;} // score
	public int ammo{ get; set; } // amount of arrows

    public Bow bow;
    public States gameState;

    // referene to the text fields of game UI
    public Text arrowText;
    public Text scoreText;
    public Text endscoreText;
    public Text actualHighscoreText;
    public Text newHighscoreText;
    public Text newHighText;

    // Sound effects
    public AudioClip stringPull;
    public AudioClip stringRelease;
    public AudioClip arrowSwoosh;

    // has sound already be played
    bool stringPullSoundPlayed;
    bool stringReleaseSoundPlayed;
    bool arrowSwooshSoundPlayed;


    // to determine the string pullout
    float arrowStartX;
    float length;

    // some status vars
    bool arrowShot;
    bool arrowPrepared;


    // Use this for initialisation
    void Awake(){
		singleton = this;
		score = 0;
        ammo = 20;
        gameState = States.game;
		Object.DontDestroyOnLoad(singleton); //game engine preserves game state between scenes
	}

    //
    // public void Notch()
    //
    // This method decrements the ammo and reloads the bow
    //
    public void Notch(){
        Debug.Log("notched");
		ammo--;
		bow.SpawnArrow();
	}

    //
    // void ResetGame()
    //
    // Resets data
    //
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
    // public void StartGame()
    // 
    // starts game can be triggered by main menu
    //
    //public void StateGame()
    //{
    //    gameState = States.game;
    //}

    //public void showScore()
    //{
    //    scoreText.text = "Score: " + score.ToString();
    //}


    public void showArrows()
    {
        arrowText.text = "Arrows: " + ammo.ToString();
    }

    public void showMenu()
    {
    }


    void Update()
    {
        // check the game states
        switch (gameState)
        {
            case States.menu:
                //TODO add

            case States.game:
                // set UI related stuff
                //showArrows();
                //showScore();

                // game is steered via mouse
                // (also works with touch on android)
                if (Input.GetMouseButton(0))
                {
                    // the player pulls the string
                    if (!stringPullSoundPlayed)
                    {
                        // play sound
                        GetComponent<AudioSource>().PlayOneShot(stringPull);
                        stringPullSoundPlayed = true;
                    }
                    // detrmine the pullout and set up the arrow
                    bow.PullString();
                }

                // ok, player released the mouse
                // (player released the touch on android)
                if (Input.GetMouseButtonUp(0) && arrowPrepared)
                {
                    // play string sound
                    if (!stringReleaseSoundPlayed)
                    {
                        GetComponent<AudioSource>().PlayOneShot(stringRelease);
                        stringReleaseSoundPlayed = true;
                    }
                    // play arrow sound
                    if (!arrowSwooshSoundPlayed)
                    {
                        GetComponent<AudioSource>().PlayOneShot(arrowSwoosh);
                        arrowSwooshSoundPlayed = true;
                    }
                    // shot the arrow (rigid body physics)
                    bow.ShootArrow();
                }

                // in any case: update the bowstring line renderer
                bow.DrawString();
                break;
            case States.instructions:
                break;
            case States.over:
                break;
            case States.hiscore:
                break;
        }

    }
}
