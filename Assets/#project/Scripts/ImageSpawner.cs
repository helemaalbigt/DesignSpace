using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSpawner : MenuButton {


	public Transform _GlobalWrapper;
	private Sprite _Image;

	public void Start(){
		_Image = GetComponent<Image> ().sprite;
	}

	// Update is called once per frame
	public void HoverOn(WandController C){
		if (C.triggerDown)
		{
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
		imageSpawn.transform.localScale = new Vector3 (f,f,f);

		SpriteRenderer SR = imageSpawn.AddComponent<SpriteRenderer>();
		SR.sprite = _Image;

		imageSpawn.layer = LayerMask.NameToLayer ("Model");

		while (!C.triggerDown)
		{
			yield return null;
		}
			
		imageSpawn.transform.parent = _GlobalWrapper;
		BoxCollider BC = imageSpawn.AddComponent<BoxCollider> ();

		imageSpawn.AddComponent<Geometry> ();
	}
}
