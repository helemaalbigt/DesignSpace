using UnityEngine;
using System.Collections;

public class QuickRandomRotation : MonoBehaviour {

    public float minRotation=10f;
    public float maxRotation=80f;

    public Vector3 rotate = Vector3.forward;
    public float speed = 90f;

    void OnEnable() {
        rotate.x = Random.Range(-1f, 1f);
        rotate.y = Random.Range(-1f, 1f);
        rotate.z = Random.Range(-1f, 1f);
        speed = Random.Range(minRotation, maxRotation);
    }
    void Update()
    {

        transform.Rotate(rotate, speed * Time.deltaTime, Space.World);

    }
	
}
