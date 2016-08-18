using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

    Vector3 offset;
    Arrow arrow;

    //
    // void Start()
    //
    // Use this for initilisation
    //
    void Start()
    {
        offset = new Vector3(0, 0, 0);
    }

    //
    // void Update()
    //
    // It is called on every frame
    //
    void Update()
    {

    }

    //
    // public void SpawnArrow()
    //
    // Spawn a new arrow
    //
    public void SpawnArrow()
    {
        //TODO sound effect
        // instantiate a new arrow
        transform.localRotation = Quaternion.identity;
        arrow = Instantiate(Resources.Load("arrowPrefab"), Vector3.zero, Quaternion.identity) as Arrow;
        arrow.name = "arrow";
        arrow.transform.localScale = transform.localScale;
        arrow.transform.localPosition = transform.position + offset;
        arrow.transform.localRotation = transform.localRotation;
        arrow.transform.parent = transform;
        //TODO transmit the reference
    }

    // public void PullString()
    //
    // When the player pulls out the string
    //
    public void PullString()
    {

    }

    //
    // public void ShootArrow()
    //
    // player released the arrow
    // get the bows rotation and acc.
    //
    public void ShootArrow()
    {

    }
}
