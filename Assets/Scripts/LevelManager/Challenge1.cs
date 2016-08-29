using UnityEngine;
using System.Collections;

public class Challenge1 : MonoBehaviour {
    [SerializeField]
    Target target;
    [SerializeField]
    GameObject nextChallenge;
    void Update()
    {
        if (target.dead)
        {
            nextChallenge.SetActive(true);
        }
    }
}
