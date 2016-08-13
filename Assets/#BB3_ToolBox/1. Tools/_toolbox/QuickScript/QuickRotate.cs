using UnityEngine;
using System.Collections;

public class QuickRotate : MonoBehaviour {

    public Vector3 rotate= Vector3.up;
    public float speed=90f;
    public Space spaceType = Space.World;
	void Update () {

        transform.Rotate(rotate, speed * Time.deltaTime, spaceType);
	
	}
}
