using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BlackBox.Tools
{
    [CustomEditor(typeof(WebPageUnityLoader))]
    public class WebPageUnityLoader_Inspector : Editor
    {
        
        private bool showtextLoaded;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            WebPageUnityLoader getPost = (WebPageUnityLoader)target;
            if (GUILayout.Button("Load", GUILayout.MaxWidth(80), GUILayout.Height(20)))
            {
                getPost.LoadWithCoroutine(true, true);
            }

            if (GUILayout.Button((showtextLoaded ? "Hide Loaded" : "Show Loaded"), GUILayout.MaxWidth(110), GUILayout.Height(20)))
            {
                showtextLoaded = !showtextLoaded;
            }
            if (showtextLoaded)
            {
                string text = getPost.TextLoaded;
                if (text != null && text.Length > 0)
                {
                    EditorGUILayout.LabelField("Text loaded");
                    EditorGUILayout.TextArea(text, GUILayout.MinWidth(100), GUILayout.MinHeight(120));
                }
                string error = getPost.Error;
                if (error != null && error.Length > 0)
                {
                    EditorGUILayout.LabelField("Error detected");
                    EditorGUILayout.TextArea(error, GUILayout.MinWidth(100), GUILayout.MinHeight(50));
                }
            }
        }

    }
}