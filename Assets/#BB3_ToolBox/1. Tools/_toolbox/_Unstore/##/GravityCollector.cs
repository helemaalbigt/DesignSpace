using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GravityCollector : MonoBehaviour {


    public Transform gravityPoint;
    public float transformSpeed;
    public List<GameObject> gravityObjectAffected = new List<GameObject>();

    public delegate void OnCollectableAdd(GameObject objectCollected);
    public delegate void OnCollectableRemove(GameObject objectRemoved);

    public OnCollectableAdd onCollectableAdd;
    public OnCollectableRemove onCollectableRemove;

    public void Start() {
        if (!gravityPoint)
            gravityPoint = transform;
    }
    public void AddCollectableObject(GameObject objToCollect) {

        if (objToCollect && !gravityObjectAffected.Contains(objToCollect))
        {
            gravityObjectAffected.Add(objToCollect);
            if (onCollectableAdd!=null)
            onCollectableAdd(objToCollect);
        }
    }
	
	void LateUpdate () {
        for (int i = 0; i < gravityObjectAffected.Count; i++)
        {
            GameObject gamo = gravityObjectAffected[i];
            if (!gamo.activeInHierarchy || ! gameObject.activeSelf)
                Remove(gamo);
            else {
                Rigidbody rigBody = gamo.GetComponent<Rigidbody>();
                if(rigBody)
                    rigBody.velocity = Vector3.zero;
                gamo.transform.position = Vector3.MoveTowards(gamo.transform.position, gravityPoint.position, Time.deltaTime * transformSpeed);
                if (gamo.transform.position == gravityPoint.position)
                    Remove(gamo);

            }
        }
	
	}
    private void Remove(GameObject gamo)
    {
        gravityObjectAffected.Remove(gamo);
        if (onCollectableRemove != null)
            onCollectableRemove(gamo);
    }

    public void OnDisable() {
        for (int i = 0; i < gravityObjectAffected.Count; i++)
        {
            GameObject gamo = gravityObjectAffected[i];
            Remove(gamo);
        }
    
    }
}
