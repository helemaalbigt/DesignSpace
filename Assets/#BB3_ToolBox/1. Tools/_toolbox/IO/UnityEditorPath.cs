
// BlenderCameraControls.cs


// by Marc Kusters (Nighteyes)
//
// Usage: Select any object to use the camera hotkeys. 
//
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Transform)), CanEditMultipleObjects]
public class BlenderCameraControls : Editor
{
    UnityEditor.SceneView sceneView;

    private Vector3 eulerAngles;
    private Event current;
    private Quaternion rotHelper;

    public void OnSceneGUI()
    {

        current = Event.current;

        if (!current.isKey || current.type != EventType.keyDown)
            return;

        sceneView = UnityEditor.SceneView.lastActiveSceneView;
        eulerAngles = sceneView.camera.transform.rotation.eulerAngles;
        rotHelper = sceneView.camera.transform.rotation;

        switch (current.keyCode)
        {
            case KeyCode.Keypad1:
                if (current.control == false)
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 360f, 0f)));
                else
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
                break;
            case KeyCode.Keypad2:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, rotHelper * Quaternion.Euler(new Vector3(-15f, 0f, 0f)));
                break;
            case KeyCode.Keypad3:
                if (current.control == false)
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 270f, 0f)));
                else
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                break;
            case KeyCode.Keypad4:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y + 15f, eulerAngles.z)));
                break;
            case KeyCode.Keypad5:
                sceneView.orthographic = !sceneView.orthographic;
                break;
            case KeyCode.Keypad6:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y - 15f, eulerAngles.z)));
                break;
            case KeyCode.Keypad7:
                if (current.control == false)
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(90f, 0f, 0f)));
                else
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(270f, 0f, 0f)));
                break;
            case KeyCode.Keypad8:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, rotHelper * Quaternion.Euler(new Vector3(15f, 0f, 0f)));
                break;
            case KeyCode.KeypadPeriod:
                if (Selection.transforms.Length == 1)
                    sceneView.LookAtDirect(Selection.activeTransform.position, sceneView.camera.transform.rotation);
                else if (Selection.transforms.Length > 1)
                {
                    Vector3 tempVec = new Vector3();
                    for (int i = 0; i < Selection.transforms.Length; i++)
                    {
                        tempVec += Selection.transforms[i].position;
                    }
                    sceneView.LookAtDirect((tempVec / Selection.transforms.Length), sceneView.camera.transform.rotation);
                }
                break;
            case KeyCode.KeypadMinus:
                SceneView.RepaintAll();
                sceneView.size *= 1.1f;
                break;
            case KeyCode.KeypadPlus:
                SceneView.RepaintAll();
                sceneView.size /= 1.1f;
                break;
        }
    }
}
#endif