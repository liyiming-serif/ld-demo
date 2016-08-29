using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Challenge5 : ChallengeScript {

    [SerializeField]
    private Target[] targets;

    void Update()
    {
        foreach(Target t in targets)
        {
            if (!t.dead)
            {
                return;
            }
        }
        nextChallenge.SetActive(true);
        statsResult.text = (1.0f / Engine.singleton.arrowsUsed * 100).ToString("#.##") + "%";
    }
}
