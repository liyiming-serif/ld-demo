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
    public void PullString(float magnitude, float angleZ)
    {

        if (magnitude < 10)
        {
            spriteRend.sprite = sprites[0];
        }
        else if (magnitude < 20)
        {
            spriteRend.sprite = sprites[1];
        }
        else if (magnitude < 40)
        {
            spriteRend.sprite = sprites[2];
        }
        else if (magnitude < 60)
        {
            spriteRend.sprite = sprites[3];
        }
        else if (magnitude < 80)
        {
            spriteRend.sprite = sprites[4];
        }
        else if (magnitude < 100)
        {
            spriteRend.sprite = sprites[5];
        }
        else
        {
            if(nowTag == 6 && count == 3)
            {
                spriteRend.sprite = sprites[7];
                nowTag = 7;
                count = 0;
            }
            else if(nowTag == 7 && count == 3)
            {
                spriteRend.sprite = sprites[6];
                nowTag = 6;
                count = 0;
            }
            else
            {
                count += 1;
            }
        }
        transform.eulerAngles = new Vector3(0, 0, angleZ);
    }

    public void FireArrow()
    {
        spriteRend.sprite = sprites[0];
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
