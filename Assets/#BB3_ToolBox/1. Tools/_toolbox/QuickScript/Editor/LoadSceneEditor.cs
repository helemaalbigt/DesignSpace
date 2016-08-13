using UnityEngine;
using System.Collections;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace BlackBox.Tools
{
    [CustomEditor(typeof(LoadScene))]
    public class LoadsceneEditor : Editor
    {
        
        public static void PlayInitScene(string sceneName)
        {
            if (EditorApplication.isPlaying == true)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            //            EditorSceneManager.OpenScene("Assets/StartScene.unity");
            Scene  sceneToLoad=  EditorSceneManager.GetSceneByName(sceneName);
            Debug.Log("Name:" + sceneToLoad.name);
            Debug.Log("Path:" + sceneToLoad.path);
            EditorSceneManager.OpenScene(sceneToLoad.path);
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            LoadScene sceneloader = (LoadScene)target;
            if (GUILayout.Button("Load", GUILayout.MaxWidth(80), GUILayout.Height(20)))
            {
                PlayInitScene(sceneloader.sceneNameToLoad);
            }
            
        }

    }

}
