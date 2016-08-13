using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Debug_DrawLookPath : MonoBehaviour {

    public LookPathToJSON _lookPathToJson;
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.L)) {
            List<LookPath> paths = _lookPathToJson.RecoverAllPaths();
            foreach (LookPath path in paths)
            {
                foreach (TimeLinkedLookState state in path._lookStatePath)
                {
                    DebugPlus.DrawGizmoCrossAt(state._lookState._rootPosition,0.1f, Color.red, 100);
                    DebugPlus.DrawGizmoCrossAt(state._lookState._lookAtPosition, 0.1f, Color.cyan, 100);
                    Debug.DrawLine(state._lookState._rootPosition, state._lookState._lookAtPosition, Color.grey, 100);
                }
            }



            foreach (LookPath path in paths)
                for (int i = 1; i < path._lookStatePath.Count; i++)
                {
                    Vector3 from = path._lookStatePath[i-1]._lookState._rootPosition;
                    Vector3 to = path._lookStatePath[i]._lookState._rootPosition;
                    Debug.DrawLine(from, to, Color.green, 100);
                }
        }
	
	}
}
