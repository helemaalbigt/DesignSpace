using UnityEngine;
using System.Collections;
using System;

public class LoadScene : MonoBehaviour {

    public string sceneNameToLoad ="";
    public float withDelay=2f;

    public delegate void DoBeforeLoadNextScene(string currentScene, string nextScene);
    public static DoBeforeLoadNextScene WhatToDoBeforeLoadNextScene;
    public delegate void DoAtLoadWithDelayNextScene(string currentScene, string nextScene,float delay);
    public static DoAtLoadWithDelayNextScene OnStartLoadWithDelay;


  

    public void LoadSelectedSceneWithDelay()
    {
        if (withDelay > 0){
            if (OnStartLoadWithDelay != null)
                OnStartLoadWithDelay(Application.loadedLevelName, sceneNameToLoad, withDelay);
            Invoke("LoadSelectedScene", withDelay);
        }
        else LoadSelectedScene();
    }
    
    public void LoadSelectedScene()
    {
        LoadNextScene(sceneNameToLoad);
    }
    public void LoadCurrentScene()
    {
        LoadNextScene(Application.loadedLevelName);
    }
    public void LoadSelectedScene(string sceneName)
    {
        LoadNextScene(sceneName);
    }
    public static void LoadNextScene(string nextScene) {
        try {
            if (WhatToDoBeforeLoadNextScene != null)
                WhatToDoBeforeLoadNextScene(Application.loadedLevelName, nextScene);
            if (string.IsNullOrEmpty(nextScene))
                Application.Quit();
            else
                Application.LoadLevel(nextScene);

        }
        catch(Exception e)
        {
            Debug.LogWarning("Impossible to load next scene:" + e);
        }
    }


    internal static void ReloadScene(float delay=3)
    {
        GameObject nextSceneLoader = new GameObject("Scene Reloader !");
        LoadScene loader =  nextSceneLoader.AddComponent<LoadScene>();
        loader.withDelay = delay;
        loader.SetSceneToLoadAsCurrent();

        loader.LoadSelectedSceneWithDelay();
      

    }

    private void SetSceneToLoadAsCurrent()
    {
        this.sceneNameToLoad = Application.loadedLevelName;
    }
}
