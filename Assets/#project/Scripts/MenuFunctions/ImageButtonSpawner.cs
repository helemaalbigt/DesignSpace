using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ImageButtonSpawner : MonoBehaviour {

	public Transform _GlobalWrapper;
	public GameObject _ImagePrefab;
	public Transform _Parent;

	public Text _NoImageText;

	private string _FilePath;

	// Use this for initialization
	void Start(){

		if (Application.isEditor) {
			_FilePath = Application.dataPath + "/Resources/Images/"; 			//persistentDataPath
		} else {
			_FilePath = Application.dataPath + "/Resources/Images/"; 
		}
			
		//create directory if not exists
		if (!System.IO.Directory.Exists (_FilePath))
			System.IO.Directory.CreateDirectory (_FilePath);

		//get all images from directory
		List<Sprite> spriteList = new List<Sprite>();
		var info = new DirectoryInfo(_FilePath);
		var fileInfo = info.GetFiles();
		foreach(var file in fileInfo){
			if (file.Extension == ".jpg" || file.Extension == ".png")
			{
				//load image into texture and then into sprite 
				var bytes = System.IO.File.ReadAllBytes(file.FullName);
				Texture2D image = new Texture2D (1, 1);
				image.LoadImage (bytes);
				spriteList.Add (Sprite.Create(image, new Rect(0,0,image.width, image.height), Vector2.zero));
			}
		}

		//Sprite[] sprites = Resources.LoadAll <Sprite> ("Images"); //old

		if (spriteList.Count == 0)
		{
			_NoImageText.text = "Place reference images in: \n\n" + "/Resources/Images/";// _FilePath;
		}

		foreach (Sprite tex in spriteList)
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
