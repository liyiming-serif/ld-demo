using UnityEngine;
using System.Collections;

public class BackgroundWave : MonoBehaviour {
    [SerializeField]
    private Transform[] trans;

    private int counter;


    void Start()
    {
        counter = 0;
    }

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < trans.Length; i++)
        {
            if (counter == 0)
            {
                trans[i].position = new Vector2(trans[i].position.x, Engine.randValues[i] * (i + 1) + 1);
                counter += 1;
            }
            else if (counter == 50)
            {
                trans[i].position = new Vector2(trans[i].position.x, 0);
                counter += 1;
            }
            else if (counter == 100)
            {
                counter = 0;
            }
            else
            {
                counter += 1;
            }
        }
        //for (int i = 0; i < trans.Length; i++)
        //{
        //    if (counter < 50)
        //    {
        //        trans[i].position += new Vector3(0, (i + 1) * 2f / 50f, 0);
        //        counter += 1;
        //    }
        //    else if (counter < 100)
        //    {
        //        trans[i].position -= new Vector3(0, (i + 1) * 2f / 50f, 0);
        //        counter += 1;
        //    }
        //    else
        //    {
        //        counter = 0;
        //    }
        //}
    }
}
