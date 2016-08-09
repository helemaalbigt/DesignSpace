using UnityEngine;
using System.Collections;

public class ObjectBounds : MonoBehaviour {
	
	public Color _ColorHover;
	public Color _ColorSelected;
	public Material _BoundMaterial;
	public float _BoundWidth;
    public float _ImageBoundWidth;

    private Transform _LastSelected;
	private GameObject _BoundingBox = null;
    private string _ImageTag = "Image";


	public void DisableBounds(Transform parent){
        if (parent.Find("BoundingBox") != null)
        {
            GameObject bounds = parent.Find("BoundingBox").gameObject;
            bounds.SetActive(false);
        }else
        {
            Debug.LogWarning("Object '" + transform.gameObject.name + "'has no renderer - could not draw bounding box");
        }

		_LastSelected = null;
	}

	public void EnableBounds(Transform parent, Color col){

		if (hasChildWithName(parent, "BoundingBox")) {
			GameObject bounds = parent.Find("BoundingBox").gameObject;
			bounds.SetActive (true);
			LineRenderer[] lines = bounds.GetComponentsInChildren<LineRenderer> ();
			foreach (LineRenderer line in lines)
			{
				line.material.SetColor("_Color",col);
			}
		} else {
            if (parent.GetComponent<Renderer>() != null)
            {
                if(parent.gameObject.tag == _ImageTag)
                {
                    DrawBoundsImage(parent.GetComponent<Renderer>(), col);
                }
                else
                {
                    DrawBounds(parent.GetComponent<Renderer>(), col);
                }
            }
            else
            {
                Debug.LogWarning("Object '"+transform.gameObject.name+"'has no renderer - could not draw bounding box");
            }
		}

		_LastSelected = parent;
	}

	private void DrawBounds(Renderer rend, Color col){

        //generate combined bounds
        Bounds combinedBounds = rend.bounds;
        Renderer[] renderers = rend.transform.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in renderers)
        {
            if (R != rend) combinedBounds.Encapsulate(R.bounds);
        }

        //create parent object
        _BoundingBox = new GameObject("BoundingBox");
		_BoundingBox.transform.parent = rend.transform;

		_BoundingBox.transform.localPosition = Vector3.zero;
		_BoundingBox.transform.localRotation = Quaternion.identity;
		_BoundingBox.transform.localScale = Vector3.one;

		Vector3 v3Center = combinedBounds.center;
		Vector3 v3Extents = combinedBounds.extents;

		Vector3 v3FrontTopLeft     = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
		Vector3 v3FrontTopRight    = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
		Vector3 v3FrontBottomLeft  = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
		Vector3 v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
		Vector3 v3BackTopLeft      = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
		Vector3 v3BackTopRight     = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
		Vector3 v3BackBottomLeft   = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
		Vector3 v3BackBottomRight  = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner


		drawLine (v3FrontTopLeft, v3FrontTopRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackTopRight , v3FrontTopRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackTopRight , v3BackTopLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3FrontTopLeft , v3BackTopLeft, col, _BoundWidth, _BoundingBox);

		drawLine (v3FrontBottomLeft, v3FrontBottomRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomRight , v3FrontBottomRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomRight , v3BackBottomLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3FrontBottomLeft , v3BackBottomLeft, col, _BoundWidth, _BoundingBox);

		drawLine (v3FrontBottomLeft , v3FrontTopLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3FrontBottomRight , v3FrontTopRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomLeft , v3BackTopLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomRight , v3BackTopRight, col, _BoundWidth, _BoundingBox);
	}

	private void DrawBounds(Mesh rend, Color col, Transform parent){

		//create parent object
		_BoundingBox = new GameObject("BoundingBox");
		_BoundingBox.transform.parent = parent;

		_BoundingBox.transform.localPosition = Vector3.zero;
		_BoundingBox.transform.localRotation = Quaternion.identity;
		_BoundingBox.transform.localScale = Vector3.one;

		//generate combined bounds
		Bounds combinedBounds = rend.bounds;
		MeshFilter[] renderers = parent.gameObject.GetComponentsInChildren<MeshFilter>();
		foreach (MeshFilter M in renderers) {
			if (M != rend) combinedBounds.Encapsulate(M.mesh.bounds);
		}

		//Vector3 v3Center = combinedBounds.center;
		//Vector3 v3Extents = combinedBounds.extents;
		Vector3 v3Center = _BoundingBox.transform.InverseTransformPoint(combinedBounds.center);
		Vector3 v3Extents = _BoundingBox.transform.InverseTransformDirection(combinedBounds.extents);

		Vector3 v3FrontTopLeft     = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
		Vector3 v3FrontTopRight    = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
		Vector3 v3FrontBottomLeft  = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
		Vector3 v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
		Vector3 v3BackTopLeft      = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
		Vector3 v3BackTopRight     = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
		Vector3 v3BackBottomLeft   = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
		Vector3 v3BackBottomRight  = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner


		drawLine (v3FrontTopLeft, v3FrontTopRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackTopRight , v3FrontTopRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackTopRight , v3BackTopLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3FrontTopLeft , v3BackTopLeft, col, _BoundWidth, _BoundingBox);

		drawLine (v3FrontBottomLeft, v3FrontBottomRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomRight , v3FrontBottomRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomRight , v3BackBottomLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3FrontBottomLeft , v3BackBottomLeft, col, _BoundWidth, _BoundingBox);

		drawLine (v3FrontBottomLeft , v3FrontTopLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3FrontBottomRight , v3FrontTopRight, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomLeft , v3BackTopLeft, col, _BoundWidth, _BoundingBox);
		drawLine (v3BackBottomRight , v3BackTopRight, col, _BoundWidth, _BoundingBox);
	}

    private void DrawBoundsImage(Renderer rend, Color col){

        //create parent object
        _BoundingBox = new GameObject("BoundingBox");
        _BoundingBox.transform.parent = rend.transform;

        _BoundingBox.transform.localPosition = Vector3.zero;
        _BoundingBox.transform.localRotation = Quaternion.identity;
        _BoundingBox.transform.localScale = Vector3.one;

        //get bounds
        SpriteRenderer spriteRend = rend as SpriteRenderer;
        Vector2 v2Extents = new Vector2(spriteRend.sprite.rect.width, spriteRend.sprite.rect.height);
        v2Extents /= 100f; //convert pixels to units

        //draw bounds
        Vector3 v3TopLeft = new Vector3(0, v2Extents.y, 0);  // Front top left corner
        Vector3 v3TopRight = new Vector3(v2Extents.x, v2Extents.y, 0);  // Front top right corner
        Vector3 v3BottomLeft = new Vector3(v2Extents.x, 0, 0);  // Back top left corner
        Vector3 v3BottomRight = new Vector3(0,0,0);

        drawLine(v3TopLeft, v3TopRight, col, _ImageBoundWidth, _BoundingBox, false);
        drawLine(v3TopLeft, v3BottomRight, col, _ImageBoundWidth, _BoundingBox, false);
        drawLine(v3BottomRight, v3BottomLeft, col, _ImageBoundWidth, _BoundingBox, false);
        drawLine(v3BottomLeft, v3TopRight, col, _ImageBoundWidth, _BoundingBox, false);
    }


    private void drawLine(Vector3 p1, Vector3 p2, Color col, float lineWidth, GameObject parent, bool useWorldSpace = true){

		GameObject line = new GameObject("Line");
		line.transform.parent = parent.transform;
        line.transform.localPosition = Vector3.zero;
        line.transform.localRotation = Quaternion.identity;
        line.transform.localScale = Vector3.one;
        line.AddComponent<LineRenderer>();
		LineRenderer lineRenderer = line.GetComponent<LineRenderer> ();
		//lineRenderer.useWorldSpace = true;
		lineRenderer.useWorldSpace = false;
		lineRenderer.material = _BoundMaterial; 
		lineRenderer.material.SetColor("_Color",col);
		lineRenderer.SetColors(col,col);
		lineRenderer.SetWidth(lineWidth, lineWidth);
		lineRenderer.SetVertexCount(2);

        if (useWorldSpace)
        {
            lineRenderer.SetPosition(0, line.transform.InverseTransformPoint(p1));
            lineRenderer.SetPosition(1, line.transform.InverseTransformPoint(p2));
        }
        else
        {
            lineRenderer.SetPosition(0, p1);
            lineRenderer.SetPosition(1, p2);
        }
	}

	private bool hasChildWithName(Transform parent, string name){
		bool childFound = false;
		foreach (Transform t in parent) {
			if (t.name == name)
				childFound = true;
		}
		return childFound;
	}
}
