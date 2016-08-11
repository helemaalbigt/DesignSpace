using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LineDrawerImage : MenuCheckbox
{
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
			C.cursor.SetCursorState(CursorController.CursorState.unlocked);
			InputController.inUse = true;

			targetC = C;
			InvokeRepeating("CreateLines",0f,0.015f);
		}

		if (!_IsSelected && C.triggerDown)
		{
			StopSketching ();
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
				obj.transform.parent = targetC.hitObj.transform;
                lr = obj.AddComponent<LineRenderer>();
                lr.SetWidth(lineWidth, lineWidth);
                lr.material = lineMat;
				lr.material.color = Color.red;
				lr.useWorldSpace = false;

				isClicked = true;
            }
				
			AddPoint(targetC.cursor.curPos, lr);
        }
        else
        {
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

	public void StopSketching(){
		InputController.inUse = false;

		CancelInvoke();
		isClicked = false;
	}
}
