
 #if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using BB = BlackBox.Beans.Basic;

using System.Reflection;
using Object = UnityEngine.Object;

/// <summary>
/// _g : shortcut normal
/// %  : ctrl
/// #  : majuscule
/// &  : alt
/// </summary>
public class Shortcuts 
{

    //[MenuItem("Window/Black Box 3/Shortcut/Views/Top %UP")]
    //[MenuItem("Window/Black Box 3/Shortcut/Views/Left %LEFT")]
    //[MenuItem("Window/Black Box 3/Shortcut/Views/Front %DOWN")]
    //[MenuItem("Window/Black Box 3/Shortcut/Views/Turn Left #LEFT")]
    //[MenuItem("Window/Black Box 3/Shortcut/Views/Turn Right #RIGHT")]
    //[MenuItem("Window/Black Box 3/Shortcut/Views/Turn Up #UP")]
    //[MenuItem("Window/Black Box 3/Shortcut/Views/Turn Down #DOWN")]
    //static void SetViewToLeft()
    //{

    //    throw new BlackBox.Exceptions.RefactoringException();
    //}

    [MenuItem("Window/Black Box 3/Shortcut/Version/0.0.x+1 %v")]
    static void UpgrateVersion()
    {
        BB.Version version = BB.Version.CreateFromUnity();

        Debug.Log("Version 0.0.x++");
        Debug.Log(version.ToString());
        throw new BlackBox.Exceptions.RefactoringException();
    }
    [MenuItem("Window/Black Box 3/Shortcut/Just Do It %j")]
    static void JustDoIt()
    {
        Debug.LogWarning("JUST DO IT !!!");
        System.Diagnostics.Process.Start("http://www.dailymotion.com/video/xc5gkq_eye-of-the-tiger_music");


    }
    [MenuItem("Window/Black Box 3/Shortcut/Google Translate %g")]
    static void GoogleTranslate()
    {
        System.Diagnostics.Process.Start("https://translate.google.com/");
    }
    [MenuItem("Window/Black Box 3/Shortcut/Google %&g")]
    static void GoogleIt()
    {
        System.Diagnostics.Process.Start("https://www.google.com/");
    }
    [MenuItem("Window/Black Box 3/Shortcut/Unity API #,")]
    static void ScriptManual()
    {
        System.Diagnostics.Process.Start("http://docs.unity3d.com/ScriptReference/");
    }
    
    [MenuItem("Window/Black Box 3/Shortcut/Version/0.x+1.0 %&v")]
    static void UpgrateStableVersion()
    {
        BB.Version version = BB.Version.CreateFromUnity();
        Debug.Log("Version 0.x++.0");
        //Set i3 to 0
        //Add one to i2;
        throw new BlackBox.Exceptions.RefactoringException();
    }
    [MenuItem("Window/Black Box 3/Shortcut/Create Empty Point %#e")]
	static void GoCreateObjectInParent() {
		CreateEmptyPointInParent ("Empty Point");
	}


	static GameObject CreateEmptyPointInParent(string name){
		GameObject go = new GameObject(name);
		if(Selection.activeTransform != null)
			go.transform.parent = Selection.activeTransform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
		return go;

	}

	//[MenuItem("Window/Black Box 3/Shortcut/Group Object %#g")]
	//static void GroupMeThat() {

	//	//Create the group
	//	GameObject group  = CreateEmptyPointInParent ("Group");
 //       //Get the destination
 //       //		Transform destination = null; 
 //       //		Transform lastSelected = Selection.activeTransform;
 //       Vector3 averagePosition;
 //       GameObject [] selectedObject = Selection.objects as GameObject[];

 //       for (int i = 0; i < selectedObject.Length; i++)
 //       {
 //           selectedObject[i].transform.parent = group;
 //       }
 //       foreach (GameObject selectedObject in Selection.objects)
 //       {
 //           selectedObject.transform.parent = group.transform;

 //       }
 //   }

//	}


	private static EditorWindow _mouseOverWindow;
	[MenuItem("Window/Black Box 3/Shortcut/Lock Inspect. %#w")]
	static void ToggleInspectorLock()
	{
		
		if (_mouseOverWindow == null)	
		{
			
			if (!EditorPrefs.HasKey("LockableInspectorIndex"))
				
				EditorPrefs.SetInt("LockableInspectorIndex", 0);
			
			int i = EditorPrefs.GetInt("LockableInspectorIndex");
			
			
			
			Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
			
			Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
			
			_mouseOverWindow = (EditorWindow)findObjectsOfTypeAll[i];
			
		}
		
		
		
		if (_mouseOverWindow != null && _mouseOverWindow.GetType().Name == "InspectorWindow")
			
		{
			
			Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
			
			PropertyInfo propertyInfo = type.GetProperty("isLocked");
			
			bool value = (bool)propertyInfo.GetValue(_mouseOverWindow, null);
			
			propertyInfo.SetValue(_mouseOverWindow, !value, null);
			
			_mouseOverWindow.Repaint();
			
		}
		
	}
}
#endif