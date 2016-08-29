using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Challenge5 : ChallengeScript {

    [SerializeField]
    private Target target;

    public override void ChallengeFailed()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (target.dead)
        {
            nextChallenge.SetActive(true);
            statsResult.text = (1.0f / Engine.singleton.arrowsUsed * 100).ToString("#.##") + "%";
        }
    }
}
