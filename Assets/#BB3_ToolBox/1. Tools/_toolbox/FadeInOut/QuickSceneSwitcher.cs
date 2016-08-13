using UnityEngine;
using System.Collections;
using System;

public class QuickSceneSwitcher : MonoBehaviour {


    public QuickFadeInOut fadeInOut;
    public float timeBeforeSwitch=2f;
    private Transform targetToFollow;
    private int nextScene;
    

	void Start () {
        Init();
    }
    void Awake() {
        Init();
    }
    void Init() {
        fadeInOut.SetTransparent();
        targetToFollow = Camera.main.transform;
    }

    // Update is called once per frame
   protected virtual void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            CallNextSceneWithDelay();
        }
        transform.position = targetToFollow.position;
    }

    protected void CallNextSceneWithDelay() {
        nextScene = Application.loadedLevel + 1;
        nextScene = nextScene % Application.levelCount;
        fadeInOut.SetBlack();
        Invoke("LoadNextScene", timeBeforeSwitch);
    }

    protected void LoadNextScene()
    {
        Application.LoadLevel(nextScene);
    }
}
