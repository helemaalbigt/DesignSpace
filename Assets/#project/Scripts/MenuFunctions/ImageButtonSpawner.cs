using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageButtonSpawner : MonoBehaviour {

	public Transform _GlobalWrapper;
	public GameObject _ImagePrefab;
	public Transform _Parent;

	// Use this for initialization
	void Start(){
		
		//Texture2D[] textures = (Texture2D[]) Resources.LoadAll("Images");
		Sprite[] sprites = Resources.LoadAll <Sprite> ("Images");

		foreach (Sprite tex in sprites)
		{
			GameObject img = Instantiate (_ImagePrefab as Object, Vector3.zero, Quaternion.identity) as GameObject;
			img.transform.SetParent (_Parent,false);

			Image sprite = img.GetComponent<Image> ();
			sprite.sprite = tex;

			ImageSpawner imgSpawner = img.GetComponent<ImageSpawner>();
			imgSpawner._GlobalWrapper = _GlobalWrapper;
		}
	}

}
