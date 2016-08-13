using UnityEngine;
using System.Collections;

public class PoximityDetection : MonoBehaviour {

	public LayerMask explosifLayer;
	public float detectionRadius=5f;
	public float timeBetweenCheck=0.5f;

	public void OnEnable()
	{
		InvokeRepeating ("Explode", 0, timeBetweenCheck);
	}
	public void Explode(){
        Collider[] col = Physics.OverlapSphere(gameObject.transform.position, detectionRadius, explosifLayer);
		if(col!=null && col.Length>0)
		    gameObject.SetActive (false);

	}
}
