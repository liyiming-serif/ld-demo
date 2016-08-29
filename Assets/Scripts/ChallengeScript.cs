using UnityEngine;
using UnityEngine.UI;

public abstract class ChallengeScript : MonoBehaviour {

    [SerializeField]
    protected GameObject nextChallenge;
    [SerializeField]
    protected Text statsResult;
    [SerializeField]
    protected GameObject redoChallenge;

    public abstract void ChallengeFailed();

}
