using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Debug_DrawCube : MonoBehaviour {

    public LookPathToJSON _lookPathToJson;
    public GameObject _prefabPointPath;
    public GameObject _prefabPointLook;

    public List<GameObject> _createdCube;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            List<LookPath> paths = _lookPathToJson.RecoverAllPaths();
            
            foreach (LookPath path in paths)
            {
                foreach (TimeLinkedLookState state in path._lookStatePath)
                {
                    GameObject objCreated = GameObject.Instantiate(_prefabPointPath, state._lookState._rootPosition, Quaternion.identity) as GameObject;
                    _createdCube.Add(objCreated);
                    objCreated.GetComponent<Renderer>().material.SetColor("_MainTex", Color.green);

                    objCreated = GameObject.Instantiate(_prefabPointLook, state._lookState._lookAtPosition, Quaternion.identity) as GameObject;
                    _createdCube.Add(objCreated);
                    objCreated.GetComponent<Renderer>().material.SetColor("_MainTex", Color.cyan);

                    //Debug.DrawLine(state._lookState._rootPosition, state._lookState._lookAtPosition, Color.grey, 100);
                }
            }



            //foreach (LookPath path in paths)
            //    for (int i = 1; i < path._lookStatePath.Count; i++)
            //    {
            //        Vector3 from = path._lookStatePath[i - 1]._lookState._rootPosition;
            //        Vector3 to = path._lookStatePath[i]._lookState._rootPosition;
            //        Debug.DrawLine(from, to, Color.green, 100);
            //    }


        }

    }
}
