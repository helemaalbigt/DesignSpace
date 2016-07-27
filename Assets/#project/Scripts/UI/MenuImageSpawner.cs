using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MenuImageSpawner : MonoBehaviour {

    private Transform _GlobalWrapper;
    private Sprite _Image;

    private int _NoInfinity = 0;

    public void Start()
    {
        _GlobalWrapper = GameObject.FindGameObjectWithTag("GlobalWrapper").transform;
        _Image = GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    public void SpawnImage()
    {
        if (!InputController.inUse)
        {
            if (FindMenuControllerReference(transform) != null)
            {
                StartCoroutine(SpawnImageCR(FindMenuControllerReference(transform)));
            }
            else
            {
                Debug.Log("Either the parents or the parent's parent needs to have a collider and the 'MenuControllerReference' script attached");
            }
        }
    }

    public IEnumerator SpawnImageCR(WandController C)
    {

        yield return null;

        C.cursor.SetCursorState(CursorController.CursorState.maxDistance);
        InputController.inUse = true;

        GameObject imageSpawn = new GameObject();
        imageSpawn.transform.parent = null;
        /*imageSpawn.transform.forward = C.hitNorm;
        imageSpawn.transform.localPosition = new Vector3(0, 0.05f, 0);*/
        float f = 0.1f;
        imageSpawn.transform.localScale = new Vector3(-f, f, 0.1f);

        SpriteRenderer SR = imageSpawn.AddComponent<SpriteRenderer>();
        SR.sprite = _Image;

        imageSpawn.layer = LayerMask.NameToLayer("Model");
        imageSpawn.tag = "Image";

        while (!C.triggerDown)
        {
            //position image
            imageSpawn.transform.position = C.cursor.transform.position;
            imageSpawn.transform.forward = C.cursor.transform.up;

            yield return null;
        }

        if (C.hitTag == "Pinboard" || C.hitTag == "Image")
        {
            imageSpawn.transform.parent = FindParent(C.hitObj.transform);
        }
        else
        {
            imageSpawn.transform.parent = _GlobalWrapper;
        }

        BoxCollider BC = imageSpawn.AddComponent<BoxCollider>();
        BC.size = new Vector3(BC.size.x, BC.size.y, 0.1f);

        imageSpawn.AddComponent<Geometry>();

        C.cursor.SetCursorState(CursorController.CursorState.unlocked);
        InputController.inUse = false;
    }

    private Transform FindParent(Transform hit)
    {
        if (hit.tag == "Pinboard")
        {
            _NoInfinity = 0;
            return hit;
        }
        else
        {
            _NoInfinity++;
            if (_NoInfinity > 10)
            {
                _NoInfinity = 0;
                return hit;
            }

            return FindParent(hit.parent);
        }
    }

    private WandController FindMenuControllerReference(Transform trans)
    {
        if (trans.parent.GetComponent<MenuControllerReference>())
        {
            return trans.parent.GetComponent<MenuControllerReference>().controller;
        }
        else if (trans.parent.parent.GetComponent<MenuControllerReference>())
        {
            return trans.parent.parent.GetComponent<MenuControllerReference>().controller;
        }
        else
        {
            return null;
        }
    }
}
