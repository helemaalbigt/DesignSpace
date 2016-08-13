using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FPSDisplay : MonoBehaviour {
    public Text tFpsDisplay;

    public int frame;
    public int frameCountSinceRefresh;
    public double timePast;
    public GUIStyle textStyle;
    public double fpsAverage;
    public double lastRefresh;
    // Use this for initialization
    void Start()
    {
        Refresh();
        DontDestroyOnLoad(this);
        InvokeRepeating("Refresh", 0, 0.25f);
    }
    void Refresh()
    {
        double oldTime = lastRefresh;
        double newTime = GetTime();

        timePast = (newTime - oldTime)/1000f;
        double newFPSvalue = (double)((double)frameCountSinceRefresh) / timePast;

        fpsAverage = (3f * fpsAverage + (double)newFPSvalue) / 4f;
        frameCountSinceRefresh = 0;


        lastRefresh = newTime;

    
    }
    void Update()
    {
        frame++;
        frameCountSinceRefresh++;

        tFpsDisplay.text = fpsAverage.ToString();
    }

    public double GetTime() {
        TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        return span.TotalMilliseconds;
    }
}
