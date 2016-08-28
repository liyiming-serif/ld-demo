using UnityEngine;
using System.Collections.Generic;

public class Bow : MonoBehaviour
{

    private SpriteRenderer spriteRend;
    private int nowTag = 6;
    private int count = 0;

    [SerializeField]
    private Sprite[] sprites;

    //
    // void Start()
    //
    // Use this for initilisation
    //
    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // public void PullString()
    //
    // When the player pulls out the string
    //
    public void PullString(int frameNo, float angleZ)
    {
        spriteRend.sprite = sprites[frameNo];
        transform.eulerAngles = new Vector3(0, 0, angleZ);
    }

    public void FireArrow()
    {
        spriteRend.sprite = sprites[0];
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
