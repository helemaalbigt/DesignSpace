using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MenuImageButtonSpawner : MonoBehaviour
{

    public GameObject _ImagePrefab;
    public Transform _Parent;

    public Text _NoImageText;

    private string _FilePath;
    private Transform _GlobalWrapper;

    // Use this for initialization
    void Start()
    {
        _GlobalWrapper = GameObject.FindGameObjectWithTag("GlobalWrapper").transform;

        if (Application.isEditor)
        {
            _FilePath = Application.dataPath + "/Resources/Images/";            //persistentDataPath
        }
        else
        {
            _FilePath = Application.dataPath + "/Resources/Images/";
        }

        //create directory if not exists
        if (!System.IO.Directory.Exists(_FilePath))
            System.IO.Directory.CreateDirectory(_FilePath);

        //get all images from directory
        List<Sprite> spriteList = new List<Sprite>();
        List<string> files = new List<string>();
        var info = new DirectoryInfo(_FilePath);
        var fileInfo = info.GetFiles();

        foreach (var file in fileInfo)
        {
            if (file.Extension == ".jpg" || file.Extension == ".png")
            {
                //add path to file
                files.Add(file.FullName);

                //load image into texture and then into sprite 
                var bytes = System.IO.File.ReadAllBytes(file.FullName);
                Texture2D image = new Texture2D(1, 1);
                image.LoadImage(bytes);
                spriteList.Add(Sprite.Create(image, new Rect(0, 0, image.width, image.height), Vector2.zero));
            }
        }

        //Sprite[] sprites = Resources.LoadAll <Sprite> ("Images"); //old

        if (files.Count == 0)
        {
            _NoImageText.text = "Place reference images in: \n\n" + _FilePath;
        }
        else
        {
            //StartCoroutine(LoadButtons(files));
        }

        foreach (Sprite tex in spriteList)
        {
            GameObject img = Instantiate(_ImagePrefab as Object, Vector3.zero, Quaternion.identity) as GameObject;
            img.transform.SetParent(_Parent, false);

            Image sprite = img.GetComponent<Image>();
            sprite.sprite = tex;
        }

        RectTransform R = _Parent.GetComponent<RectTransform>();
        R.sizeDelta = new Vector2(R.sizeDelta.x, (40f + 5f) * ((Mathf.CeilToInt((spriteList.Count) / 3) + 1)));

        //Debug.Log (Mathf.CeilToInt((spriteList.Count + 1) / 3));
    }

    /*Load images in a coroutine - not working now*/
    IEnumerator LoadButtons(List<string> files)
    {
        foreach (var file in files)
        {
            byte[] bytes;
            yield return (bytes = System.IO.File.ReadAllBytes(file));
            Texture2D image = new Texture2D(1, 1);
            yield return (image.LoadImage(bytes));
            Sprite sprite = new Sprite();
            yield return (sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), Vector2.zero));

            GameObject img = Instantiate(_ImagePrefab as Object, Vector3.zero, Quaternion.identity) as GameObject;
            img.transform.SetParent(_Parent, false);
            img.layer = LayerMask.NameToLayer("Ignore Raycast");

            Image spr = img.GetComponent<Image>();
            spr.sprite = sprite;

            ImageSpawner imgSpawner = img.GetComponent<ImageSpawner>();
            imgSpawner._GlobalWrapper = _GlobalWrapper;
        }
    }

}
