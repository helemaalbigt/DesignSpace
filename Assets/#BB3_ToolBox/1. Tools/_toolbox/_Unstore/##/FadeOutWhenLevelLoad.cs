using UnityEngine;
using System.Collections;

public class FadeOutWhenLevelLoad : MonoBehaviour {

    public QuickFadeInOut fadeInOut;

    void Start()
    {
        if (fadeInOut == null) fadeInOut = GameObject.FindObjectOfType<QuickFadeInOut>();
        LoadScene.OnStartLoadWithDelay += FadeOut;
    }
    void OnDestroy()
    {
        LoadScene.OnStartLoadWithDelay -= FadeOut;
    }

    private void FadeOut(string currentScene, string nextScene, float delay)
    {
        if (fadeInOut)
            fadeInOut.SetBlack();
    }

	
}
