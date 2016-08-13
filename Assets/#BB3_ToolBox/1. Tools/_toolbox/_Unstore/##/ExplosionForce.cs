using UnityEngine;
using System.Collections;

public class ExplosionForce : MonoBehaviour {

    public LayerMask layerObjectAffected;
    public float explosionForce=10f;
    public float explosionRadius=3f;

    public void OnEnable() {
        Explode();
    
    }

    private void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, layerObjectAffected);
        foreach (Collider col in cols) { 
            if(col){
            Rigidbody rigBody = col.GetComponent<Rigidbody>();
                if(rigBody)
                    rigBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
