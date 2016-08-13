using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BlackBox.Tools.IO;

/// <summary>
/// Warning durty code
/// </summary>
public class QuickFadeInOut : MonoBehaviour
{

    public Renderer fadeRenderer;
    public Image _imageRenderer;
    public enum FadeType { None, GoToFullColor, GoToTransparent }
    public FadeType _fadeState;
    public FadeType FadeState { get { return _fadeState; } set { _fadeState = value; } }// fadeRenderer.enabled =  value != FadeType.None; } }
    public float fadeSpeedInPct = 1f;
    public float startDelay=1f;
    [Header("Debug")]
    public Color currentColor; 

	void OnEnable()
	{
        SetTransparent(startDelay);
	}

	void Update()
	{
		if (FadeState == FadeType.None) return;
		if (fadeRenderer)
			currentColor = fadeRenderer.material.color;

		if (FadeState == FadeType.GoToTransparent)
		{
            if (currentColor.a <= 0f) {
                FadeState = FadeType.None;
                if(fadeRenderer!=null)
                fadeRenderer.enabled =false;
                if (_imageRenderer != null)
                    _imageRenderer.enabled = false;

            }

			currentColor.a = Mathf.Clamp(currentColor.a - fadeSpeedInPct * Time.deltaTime, 0f, 1f);

		}
		else if (FadeState == FadeType.GoToFullColor)
		{

			if (currentColor.a >= 1f)
				FadeState = FadeType.None;

			currentColor.a = Mathf.Clamp(currentColor.a + fadeSpeedInPct * Time.deltaTime, 0f, 1f);
        }
        if (fadeRenderer)
            fadeRenderer.material.color = currentColor;
        if (_imageRenderer)
            _imageRenderer.color = currentColor;

    }
    public void SetTransparent(float delay)
    {
        Invoke("SetTransparent", delay);
    }
    public void SetBlack(float delay)
    {

        Invoke("SetBlack", delay);
    }

	public void SetTransparent()
	{

        if (fadeRenderer)
            fadeRenderer.enabled = true;
        if (_imageRenderer)
            _imageRenderer.enabled = true;
        FadeState = FadeType.GoToTransparent;
	}
	public void SetBlack()
	{


        if (fadeRenderer)
            fadeRenderer.enabled = true;
        if (_imageRenderer)
            _imageRenderer.enabled = true;
        FadeState = FadeType.GoToFullColor;
	}

}
