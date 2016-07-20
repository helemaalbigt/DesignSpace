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

		GameObject modelSpawn = new GameObject ();
		yield return (modelSpawn = OBJLoader2.LoadOBJFile(_ModelFilePath));
		modelSpawn.transform.parent = C.cursor.transform;
		modelSpawn.transform.up = C.hitNorm;

		modelSpawn.transform.localPosition = Vector3.zero;
		modelSpawn.transform.localScale = new Vector3(_GlobalWrapper.localScale.x*10000f, _GlobalWrapper.localScale.x*10000f, _GlobalWrapper.localScale.x*10000f);

		while (!C.triggerDown)
		{
			yield return null;
		}

		InputController.inUse = false;

		modelSpawn.transform.parent = _GlobalWrapper;
		modelSpawn.transform.localScale = Vector3.one;
		//Todo be able to handle loading in nested meshes
		Transform modelSpawnChild = modelSpawn.transform.Find ("Model");
		modelSpawnChild.gameObject.AddComponent<Geometry> ();
		modelSpawnChild.gameObject.layer = LayerMask.NameToLayer ("Model");
		/*
		MeshCollider mc = modelSpawnChild.gameObject.AddComponent<MeshCollider> ();
		mc.sharedMesh = modelSpawnChild.GetComponent<MeshFilter> ().mesh;*/
		BoxCollider bc = modelSpawnChild.gameObject.AddComponent<BoxCollider> ();
		modelSpawnChild.parent = _GlobalWrapper;
		Destroy (modelSpawn);
		/*
		MeshRenderer mr = modelSpawn.AddComponent<MeshRenderer> ();
		MeshFilter mf = modelSpawn.AddComponent<MeshFilter>();
		*/
		yield return null;
		InputController.inUse = false;
	}
}
