using UnityEngine;
using System.Collections.Generic;

public class Bow : MonoBehaviour {


    //
    // void Start()
    //
    // Use this for initilisation
    //
    void Start()
    {

    }

    //
    // public void SpawnArrow()
    //
    // Spawn a new arrow
    //
    public void SpawnArrow()
    {
        ////TODO sound effect
        //// instantiate a new arrow
        //transform.localRotation = Quaternion.identity;
        //Debug.Log("here");
        //arrow = (Instantiate(Resources.Load("arrowPrefab"), Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Arrow>();
        //if(arrow == null)
        //{
        //    Debug.Log("Instantiation failed.");
        //}
        //arrow.transform.name = "arrow";
        //arrow.transform.localScale = transform.localScale;
        //arrow.transform.localPosition = transform.position;
        //arrow.transform.localRotation = transform.localRotation;
        //arrow.transform.parent = transform;
        //Engine.singleton.arrowShot = false;
        //Engine.singleton.arrowPrepared = false;
    }

    // public void PullString()
    //
    // When the player pulls out the string
    //
    public void PullString(float angleZ)
    {
        transform.eulerAngles = new Vector3(0, 0, angleZ);
    }

}
