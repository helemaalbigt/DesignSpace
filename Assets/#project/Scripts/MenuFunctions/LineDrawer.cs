using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LineDrawer : MenuCheckbox
{
    public GameObject polylineWrapper;
    public float scale = 0.1f;
    public float lineWidth = 0.01f;
	public float minLineDist = 0.005f;
    public Material lineMat;

	private List<Vector3> positions;
    bool isClicked = false;
	private LineRenderer lr;
	private GameObject obj;
	private WandController targetC;

	public void HoverOn(WandController C){
		base.HoverOn(C);

		if (_IsSelected && C.triggerDown)
		{
			InputController.inUse = true;

			Debug.Log ("Start");
			targetC = C;
			C.cursor.SetDrawLock ();
			foreach(WandController cont in InputController.controllers){
				cont.cursor.SetDrawLock ();
			}
			InvokeRepeating("CreateLines",0.2f,0.015f);
		}

		if (!_IsSelected && C.triggerDown)
		{
			InputController.inUse = false;

			Debug.Log ("Stop");
			CancelInvoke();
			C.cursor.SetCursorState (CursorController.CursorState.unlocked);
			foreach(WandController cont in InputController.controllers){
				cont.cursor.SetCursorState (CursorController.CursorState.unlocked);
			}
			isClicked = false;
		}
	}

	private void CreateLines()
    {
		Debug.Log (targetC.triggerPress);
		if (targetC.triggerPress)
        {
            if (!isClicked)
            {
                positions = new List<Vector3>();

                obj = new GameObject();
                obj.transform.parent = polylineWrapper.transform;
                lr = obj.AddComponent<LineRenderer>();
                lr.SetWidth(lineWidth, lineWidth);
                lr.material = lineMat;
				lr.material.color = Color.red;
				lr.useWorldSpace = false;
				lr.transform.gameObject.layer = LayerMask.NameToLayer("Model");

				isClicked = true;
            }
				
			AddPoint(targetC.cursor.curPos, lr);
        }
        else
        {
			Debug.Log ("reset");
            isClicked = false;
        }
    }
		
	private void AddPoint(Vector3 pos, LineRenderer lr)
    {
		if (positions.Count > 0)
		{
			if(Vector3.Distance(positions[positions.Count-1], pos) < minLineDist){
				return;
			}
		}
        positions.Add(pos);
        lr.SetVertexCount(positions.Count);
        lr.SetPosition(positions.Count - 1, pos);
    }

	public void UpdateMaterialAll(Material mat)
	{
		GameObject[] components = polylineWrapper.GetComponentsInChildren<GameObject>();

		foreach (GameObject obj in components)
		{
			LineRenderer lr = obj.GetComponent<LineRenderer>();
			lr.material = mat;
		}
	}
}
