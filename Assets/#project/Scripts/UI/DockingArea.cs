using UnityEngine;
using System.Collections;

public class DockingArea : MonoBehaviour {

    public RectTransform _MenuDock;

    private float _DockingAreaThickness = 20f;

	// Use this for initialization
	void Start () {
        DeactivateDockingArea();
        ResizeDockingArea();
	}

    public void DeactivateDockingArea()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void ActivateDockingArea()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void ResizeDockingArea()
    {
        transform.localScale = new Vector3(
            _MenuDock.rect.width,
            _MenuDock.rect.height,
            _DockingAreaThickness
        );

        transform.localPosition = new Vector3(
            _MenuDock.rect.width / 2,
            -_MenuDock.rect.height / 2,
             0f
        );
    }
}
