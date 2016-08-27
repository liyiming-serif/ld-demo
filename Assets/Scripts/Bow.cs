using UnityEngine;
using System.Collections.Generic;

public class Bow : MonoBehaviour {

    // to determine the mouse position, we need a raycast
    Ray mouseRay;
    RaycastHit rayHit;

    // position of the raycast on the screen
    float posX;
    float posY;

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
    public void PullString()
    {
        //mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(mouseRay, out rayHit, 1000f) && Engine.singleton.arrowShot == false)
        //{
        //    //TODO max turning radius should not be over +\-90.
        //    // determine the position on the screen
        //    posX = rayHit.point.x;
        //    posY = rayHit.point.y;
        //    // set the bows angle to the arrow
        //    Vector2 mousePos = new Vector2(transform.position.x - posX, transform.position.y - posY);
        //    float angleZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //    transform.eulerAngles = new Vector3(0, 0, angleZ);
        //    // determine the arrow pullout
        //    length = mousePos.magnitude / 3f;
        //    length = Mathf.Clamp(length, 0, 1);
        //    // set the bowstrings line renderer
        //    stringPullout = new Vector3(-(length - 0.2f), -0.06f, 2f);
        //    // set the arrows position
        //    Vector3 arrowPosition = arrow.transform.localPosition;
        //    arrowPosition.x = (arrowStartX - length);
        //    arrow.transform.localPosition = arrowPosition;
        //}
        //Engine.singleton.arrowPrepared = true;
    }

    //
    // public void FireArrow()
    //
    // player released the arrow
    // get the bows rotation and acc.
    //
    public void FireArrow()
    {
        //arrow.transform.parent = Engine.singleton.transform;
        //arrow.FireArrow(Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)) * new Vector2(25f * length, 0));
        //Engine.singleton.arrowPrepared = false;
        //stringPullout = stringRestPosition;
    }

}
