using UnityEngine;
using System.Collections;

public class DetachOnDisable : MonoBehaviour {

    public Transform[]  targets;
    public Transform newParent;

    void Explode()
    {
        foreach (Transform targ in targets)
        {
            targ.parent = newParent;
            targ.gameObject.SetActive(true);
        }

    }
}
