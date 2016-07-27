using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSpawner : MenuButton {


	public Transform _GlobalWrapper;
	private Sprite _Image;

	private int _NoInfinity = 0;

	public void Start(){
		_Image = GetComponent<Image> ().sprite;
	}

	// Update is called once per frame
	public void HoverOn(WandController C){
		if (C.triggerDown)
		{
			InputController.inUse = true;
			StartCoroutine(Spawn (C));
		}
	}

	IEnumerator Spawn(WandController C){
		
		yield return null;

		GameObject imageSpawn = new GameObject();
		imageSpawn.transform.parent = C.cursor.transform;
		imageSpawn.transform.forward = C.hitNorm;
		imageSpawn.transform.localPosition = new Vector3(0,0.05f,0);
		float f = 1000f;
		imageSpawn.transform.localScale = new Vector3 (-f,f,0.1f);

		SpriteRenderer SR = imageSpawn.AddComponent<SpriteRenderer>();
		SR.sprite = _Image;

		imageSpawn.layer = LayerMask.NameToLayer ("Model");
		imageSpawn.tag = "Image";

		while (!C.triggerDown)
		{
            yield return null;
		}

		if (C.hitTag == "Pinboard" || C.hitTag == "Image")
		{
			imageSpawn.transform.parent =  FindParent(C.hitObj.transform);
		} else
		{
			imageSpawn.transform.parent = _GlobalWrapper;
		}
			
		BoxCollider BC = imageSpawn.AddComponent<BoxCollider> ();
		BC.size = new Vector3 (BC.size.x, BC.size.y, 0.1f);

		imageSpawn.AddComponent<Geometry> ();

		InputController.inUse = false;
	}

	private Transform FindParent(Transform hit){
		if (hit.tag == "Pinboard")
		{
			_NoInfinity = 0;
			return hit;
		} else
		{
			_NoInfinity++;
			if (_NoInfinity > 10)
			{
				_NoInfinity = 0;
				return hit;
			}

			return FindParent (hit.parent);
		}
	}
}
