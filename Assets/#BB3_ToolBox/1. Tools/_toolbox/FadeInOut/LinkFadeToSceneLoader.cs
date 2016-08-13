using UnityEngine;
using System.Collections;

[RequireComponent(typeof(QuickFadeInOut))]
public class LinkFadeToSceneLoader : MonoBehaviour {

     QuickFadeInOut fader;

    
    public void Start() {
        Init();  
    }
    public void OnDestroy() {

        LoadScene.OnStartLoadWithDelay -= FadeOut;
    }

    void Init() {
        if (fader != null)
            return;

        fader = GetComponent<QuickFadeInOut>();
        LoadScene.OnStartLoadWithDelay += FadeOut;

    }

    private void FadeOut(string currentScene, string nextScene, float delay)
    {
        if (fader)
            fader.SetBlack();
    }
}
