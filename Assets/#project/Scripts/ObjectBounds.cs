using UnityEngine;
using System.Collections;

public class ObjectBounds : MonoBehaviour {
	
	public Color _ColorHover;
	public Color _ColorSelected;
	public Material _BoundMaterial;
	public float _BoundWidth;

	private Transform _LastSelected;
	private GameObject _BoundingBox = null;


	public void DisableBounds(Transform parent){
		GameObject bounds = parent.Find("BoundingBox").gameObject;
		bounds.SetActive (false);

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
			DrawBounds (parent.GetComponent<Renderer> (), col);
		}

		_LastSelected = parent;
	}

	private void DrawBounds(Renderer rend, Color col){

		//create parent object
		_BoundingBox = new GameObject("BoundingBox");
		_BoundingBox.transform.parent = rend.transform;

		//generate combined bounds
		Bounds combinedBounds = rend.bounds;
		Renderer[] renderers = rend.transform.gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer R in renderers) {
			if (R != rend) combinedBounds.Encapsulate(R.bounds);
		}

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

		Debug.Log (rend);

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

	private void drawLine(Vector3 p1, Vector3 p2, Color col, float lineWidth, GameObject parent){

		GameObject line = new GameObject("Line");
		line.transform.parent = parent.transform;
		line.AddComponent<LineRenderer>();
		LineRenderer lineRenderer = line.GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = false;
		lineRenderer.material = _BoundMaterial; 
		lineRenderer.material.SetColor("_Color",col);
		lineRenderer.SetColors(col,col);
		lineRenderer.SetWidth(lineWidth, lineWidth);
		lineRenderer.SetVertexCount(2);

		lineRenderer.SetPosition(0, p1);
		lineRenderer.SetPosition(1, p2);
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
