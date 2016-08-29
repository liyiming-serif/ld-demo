using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Challenge1 : MonoBehaviour {
    [SerializeField]
    Target target;
    [SerializeField]
    GameObject nextChallenge;
    [SerializeField]
    Text statsResult;
    void Update()
    {
        if (target.dead)
        {
            nextChallenge.SetActive(true);
            statsResult.text = (1.0f / Engine.singleton.arrowsUsed * 100).ToString() + "%";
        }
    }
}
