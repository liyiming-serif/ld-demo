using UnityEngine;
using System.Collections.Generic;

public class Bow : MonoBehaviour {

    public GameObject bowString;
    public Arrow arrow;

    // to determine the mouse position, we need a raycast
    Ray mouseRay;
    RaycastHit rayHit;

    // position of the raycast on the screen
    float posX;
    float posY;

    // to determine the string pullout
    float arrowStartX;
    float length;
    
    // some status vars
    bool arrowShot;
    bool arrowPrepared;
    
    Vector3 offset;


    // the bowstring is a line renderer
    List<Vector3> bowStringPosition;
    LineRenderer bowStringLinerenderer;

    // position of the line renderers middle part 
    Vector3 stringPullout;
    Vector3 stringRestPosition = new Vector3(-0.44f, -0.06f, 2f);

    //
    // void Start()
    //
    // Use this for initilisation
    //
    void Start()
    {
        arrowShot = false;
        arrowPrepared = false;

        offset = new Vector3(0, 0, 0);

        // setup the line renderer representing the bowstring
        bowStringLinerenderer = bowString.AddComponent<LineRenderer>();
        bowStringLinerenderer.SetVertexCount(3);
        bowStringLinerenderer.SetWidth(0.05F, 0.05F);
        bowStringLinerenderer.useWorldSpace = false;
        bowStringLinerenderer.material = Resources.Load("Materials/bowStringMaterial") as Material;
        bowStringPosition = new List<Vector3>();
        bowStringPosition.Add(new Vector3(-0.44f, 1.43f, 2f));
        bowStringPosition.Add(new Vector3(-0.44f, -0.06f, 2f));
        bowStringPosition.Add(new Vector3(-0.43f, -1.32f, 2f));
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, bowStringPosition[1]);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);
        bowStringLinerenderer.sortingLayerName = "Animator";
        bowStringLinerenderer.sortingOrder = 10;
        arrowStartX = 0.7f;
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
        arrow = (Instantiate(Resources.Load("arrowPrefab"), Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Arrow>();
        if(arrow == null)
        {
            Debug.Log("Instantiation failed.");
        }
        arrow.transform.name = "arrow";
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
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out rayHit, 1000f) && arrowShot == false)
        {
            // determine the position on the screen
            posX = rayHit.point.x;
            posY = rayHit.point.y;
            // set the bows angle to the arrow
            Vector2 mousePos = new Vector2(transform.position.x - posX, transform.position.y - posY);
            float angleZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angleZ);
            // determine the arrow pullout
            length = mousePos.magnitude / 3f;
            length = Mathf.Clamp(length, 0, 1);
            // set the bowstrings line renderer
            stringPullout = new Vector3(-(0.44f + length), -0.06f, 2f);
            // set the arrows position
            Vector3 arrowPosition = arrow.transform.localPosition;
            arrowPosition.x = (arrowStartX - length);
            arrow.transform.localPosition = arrowPosition;
        }
        arrowPrepared = true;
    }

    //
    // public void ShootArrow()
    //
    // player released the arrow
    // get the bows rotation and acc.
    //
    public void ShootArrow()
    {
        arrow.FireArrow(Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)) * new Vector2(25f * length, 0));
        arrowPrepared = false;
        stringPullout = stringRestPosition;
    }

    //
    // public void DrawString()
    //
    // set the bowstrings line renderer position
    //
    public void DrawString()
    {
        bowStringLinerenderer = bowString.GetComponent<LineRenderer>();
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, stringPullout);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);
    }
}
