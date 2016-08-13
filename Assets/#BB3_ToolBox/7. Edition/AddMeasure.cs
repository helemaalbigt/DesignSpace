

 #if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddMeasure : EditorWindow {
    

    [MenuItem("Window/Black Box 3/Create/Initial Project Folders")]
    private static void CreateProjectStructure()
    {
        throw new BlackBox.Exceptions.RefactoringException();
    }

    [MenuItem("Window/Black Box 3/Instanciate/Tracker/WWW Signal")]
    private static void AddWWWTacker()
    {
        
        /*WWW signal is a small object with class. Each time a scene it check if internet is on.
            If internet is on, it ping a web page. with Post info: ProjectName, Company, Version, When, Scene Name
        */

        throw new BlackBox.Exceptions.RefactoringException();
    }
    [MenuItem("Window/Black Box 3/Instanciate/Network/Server")]
    private static void AddUdpServer()
    {
        throw new BlackBox.Exceptions.RefactoringException();
    }
    [MenuItem("Window/Black Box 3/Instanciate/Network/Server Link")]
    private static void AddUdpServerLink()
    {
        throw new BlackBox.Exceptions.RefactoringException();
    }
    [MenuItem("Window/Black Box 3/Instanciate/Debug/FPS Viewer Tool")]
	private static void AddFPSViewer()
    {
        throw new BlackBox.Exceptions.RefactoringException();
    }

    [MenuItem("Window/Black Box 3/Instanciate/Mesure/Mesureing Tape %#m")]
	private static void AddMeasurePointInCameraFront()
	{
        //add a mesureing tape tool. 
        //If two transform are selected. Link the measuring too to it.
        throw new BlackBox.Exceptions.RefactoringException();
	}

	[MenuItem("Window/Black Box 3/Links/Credit/1. About")]
	private static void OpenWiki ()
	{
		System.Diagnostics.Process.Start ("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
	}

	[MenuItem("Window/Black Box 3/Links/Credit/2. Donate")]
	private static void Donation ()
	{
		System.Diagnostics.Process.Start ("http://www.stree.be/eloi/donate");
	}

    [MenuItem("Window/Black Box 3/Links/Credit/3. Contact")]
    private static void OpenContact()
    {
        System.Diagnostics.Process.Start(@"http://www.stree.be/eloi/");
    }

    public void CreateResourcesObject(string objectName, out GameObject createdObject)
    {

        if (objectName == null)
            throw new System.ArgumentException("Object Referent name shoud not be null");
        createdObject = Instantiate(Resources.Load(objectName, typeof(GameObject))) as GameObject;
        if (createdObject = null)
            throw new System.ArgumentNullException("The resource object you try to load do not existe in Resources folders");
        createdObject.name = objectName;
        if (Camera.current != null)
        {
            Camera cam = Camera.current.GetComponent<Camera>();
            if (cam != null)
            {
                createdObject.transform.position = cam.transform.position + cam.transform.forward * 5f;
            }
        }
    }

}

#endif