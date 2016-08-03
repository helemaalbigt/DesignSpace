using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModelSpawner : MenuButton {


	public Transform _GlobalWrapper;
	public string _ModelFilePath = "G:/01 My Documents/2016/Work/VR_DesignSpace/02 Files/DesignSpace/Assets/Resources/Models/treeobj.obj";

	private int _NoInfinity = 0;


	// Update is called once per frame
	public void HoverOn(WandController C){
		base.HoverOn (C);

		if (C.triggerDown)
		{
			InputController.inUse = true;
			StartCoroutine(Spawn (C));
		}
	}

	IEnumerator Spawn(WandController C){
		
		yield return null;

        //create and position parent gameobject
		GameObject modelSpawn = new GameObject ();
		yield return (modelSpawn = OBJLoader2.LoadOBJFile(_ModelFilePath));
		modelSpawn.transform.parent = C.cursor.transform;
		modelSpawn.transform.up = C.hitNorm;

		modelSpawn.transform.localPosition = Vector3.zero;
		modelSpawn.transform.localScale = new Vector3(-_GlobalWrapper.localScale.x*10000f, _GlobalWrapper.localScale.x*10000f, _GlobalWrapper.localScale.x*10000f);

        //wait for it to be placed
		while (!C.triggerDown)
		{
			yield return null;
		}

		InputController.inUse = false;

		modelSpawn.transform.parent = _GlobalWrapper;
        modelSpawn.transform.localScale = Vector3.one;
        NormalizeScaleAndChangeLayer(modelSpawn.transform);

        if(modelSpawn.AddComponent<MeshRenderer>() == null)
           modelSpawn.AddComponent<MeshRenderer>();

        if (modelSpawn.AddComponent<MeshFilter>() == null)
            modelSpawn.AddComponent<MeshFilter>();

        GetColliders(modelSpawn, modelSpawn);
        SetFirstMeshFilter(modelSpawn);

        modelSpawn.AddComponent<Geometry>();

        yield return null;
		InputController.inUse = false;
	}

    private void GetColliders(GameObject parentNode, GameObject node)
    {
        if (node.GetComponent<MeshFilter>())
        {
            MeshCollider newMesh = parentNode.AddComponent<MeshCollider>();
            newMesh.sharedMesh = node.GetComponent<MeshFilter>().mesh;
        }
        
        if(node.transform.childCount > 0)
        {
            foreach(Transform trans in node.transform)
            {
                GetColliders(parentNode, trans.gameObject);
            }
        }
    }

    //For the bounding box to work properly, the parent needs at least one meshfilter with a mesh
    //this method finds the first mesh in the parent nodes children and copys that
    private void SetFirstMeshFilter(GameObject parentNode)
    {
        if (parentNode.transform.childCount > 0)
        {
            if(parentNode.transform.GetChild(0).GetComponent<MeshFilter>() != null)
            {
                MeshFilter firstFoundMeshFilter = parentNode.transform.GetChild(0).GetComponent<MeshFilter>();
                parentNode.GetComponent<MeshFilter>().mesh = firstFoundMeshFilter.mesh;

                Material[] M = firstFoundMeshFilter.GetComponent<MeshRenderer>().materials;
                parentNode.GetComponent<MeshRenderer>().materials = M;

                Destroy(firstFoundMeshFilter.gameObject);
            }
            else
            {
                Debug.LogWarning("ModelSpawner - Could not find a meshfilter on models first child");
            }

        }
    }

    private void NormalizeScaleAndChangeLayer(Transform trans)
    {
        trans.localScale = Vector3.one;
        trans.gameObject.layer = LayerMask.NameToLayer("Model");

        foreach (Transform T in trans)
        {
            NormalizeScaleAndChangeLayer(T);
        }
    }
}
