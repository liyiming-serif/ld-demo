﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Challenge2 : ChallengeScript {

    [SerializeField]
    private Target target;

    void Update()
    {
        if (target.dead)
        {
            nextChallenge.SetActive(true);
            statsResult.text = (1.0f / Engine.singleton.arrowsUsed * 100).ToString("#.##") + "%";
        }
    }
}
