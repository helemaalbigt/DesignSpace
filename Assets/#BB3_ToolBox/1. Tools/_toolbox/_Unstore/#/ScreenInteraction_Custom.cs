using UnityEngine;
using System.Collections;

public class ScreenInteraction_Custom : MonoBehaviour, IScreenInteractionCustom
 {

     public float stayTimeToBeActive=1f;
     public float hoverTimeToBeActive=3f;



    public float GetTimeToBeSubmitOnStay()
    {
        return stayTimeToBeActive;
    }

    public float GetTimeToBeSubmitOnOver()
    {
        return hoverTimeToBeActive;
    }

    public bool IsDisplayCursorNeeded()
    {
        return true;
    }
 }
