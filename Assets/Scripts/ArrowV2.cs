using UnityEngine;
using System.Collections;

public class ArrowV2 : MonoBehaviour {

    public float lifetime = 5;

    Rigidbody2D body;
    public void Init(Vector2 og, Vector2 dest, float power)
    {
        body = GetComponent<Rigidbody2D>();
        body.position = og;

        Vector2 dir = (dest - og);
        dir.Normalize();
        body.velocity = dir * power*5;
        
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(this.gameObject);
    }
}
