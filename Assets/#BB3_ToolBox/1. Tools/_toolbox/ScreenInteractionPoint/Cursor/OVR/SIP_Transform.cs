using UnityEngine;
using System.Collections;
using System;

public class SIP_Transform : MonoBehaviour, IScreenCursorInfoSource
{

    public Transform rootRef;
    public Camera screenCameraRef;
    public Vector3 ScreenCursorPosition;
    public LayerMask layerMaks;
    public float maxDistance= 10f;
    public bool CursorActive;   
    public Transform debugCursorObj;
    public string unityPressInput = "Fire1";

    public bool isCursorHasMoveOnce;
    public Vector3 initPoint;
    public void Start() {
        Reset();
    }
    public void Reset() {

        ScreenCursorPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        initPoint = ScreenCursorPosition;
        isCursorHasMoveOnce = false;
        if (screenCameraRef == null) {
            screenCameraRef = Camera.main;
            rootRef = screenCameraRef.transform;
        }
    }

   
    public void Update() {
        if (screenCameraRef == null)
            Reset();
        else {
            CursorActive = IsOneCursorsActive();
            ScreenCursorPosition = GetScreenPosition(rootRef);
            if (isCursorHasMoveOnce == false)
            {
                isCursorHasMoveOnce = initPoint != ScreenCursorPosition;
            }
        }
     }

    private Vector3 GetScreenPosition(Transform rootRef)
    {
        RaycastHit hit ;
        if (Physics.Raycast(rootRef.position, rootRef.forward, out hit, maxDistance, layerMaks))
        {
            Vector3 pos = hit.point;
            if (debugCursorObj != null)
            {
                debugCursorObj.gameObject.SetActive(true);
                debugCursorObj.position = pos;
                debugCursorObj.rotation = rootRef.rotation;
            }
            return screenCameraRef.WorldToScreenPoint(pos);


        }
        else
            debugCursorObj.gameObject.SetActive(false);
        return screenCameraRef.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f));
    }

    public virtual bool IsOneCursorsActive() {
        return Input.GetMouseButton(0) || Input.touchCount > 0 || (!string.IsNullOrEmpty(unityPressInput) && Input.GetButton(unityPressInput));
    }
   

    Vector3 IScreenCursorInfoSource.GetScreenPosition(ref Camera camera)
    {
        camera = screenCameraRef;
        return ScreenCursorPosition;
    }

    bool IScreenCursorInfoSource.IsCursorActive()
    {
        return CursorActive;
    }

    public bool IsCursorCanBeUse()
    {
        return isCursorHasMoveOnce;
    }
}