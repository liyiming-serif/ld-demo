using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Challenge1 : ChallengeScript {

    [SerializeField]
    Target target;

    void Update()
    {
        if (target.dead)
        {
            nextChallenge.SetActive(true);
            statsResult.text = (1.0f / Engine.singleton.arrowsUsed * 100).ToString("#.##") + "%";
        }
    }
}
