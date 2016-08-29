using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    
    public void ShowPanel(GameObject thisP)
    {
        thisP.SetActive(true);
    }

    public void HidePanel(GameObject thisP)
    {
        thisP.SetActive(false);
    }

	public void LoadNextChallenge(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
