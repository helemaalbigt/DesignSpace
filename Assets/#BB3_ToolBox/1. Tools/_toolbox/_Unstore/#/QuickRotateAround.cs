using UnityEngine;
using System.Collections;

public class QuickRotateAround : MonoBehaviour {

    public float rotate = 60f;
    public Transform gravity;
    void Update()
    {
        if(gravity)
            transform.RotateAround(gravity.position, Vector3.forward, rotate * Time.deltaTime);

    }
}
