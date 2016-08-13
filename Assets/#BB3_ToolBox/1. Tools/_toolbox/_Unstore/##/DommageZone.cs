using UnityEngine;
using System.Collections;

public class DommageZone : MonoBehaviour {

    public LayerMask layerObjectAffected;
    public float dommage = 1f;
    public float dommageRadius = 1f;
    public bool exploseFirstTime;
    private bool firstTime=true;

    public void OnEnable()
    {
     
        Explode();

    }

    private void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, dommageRadius, layerObjectAffected);
        foreach (Collider col in cols)
        {
            if (col)
            {
				LifeComponent life;
                if (col.attachedRigidbody){
                	life = col.attachedRigidbody.GetComponent<LifeComponent>();
				}else{
					life = FindLifeInParents( col.transform );
				}

                if (life)
					life.AddLife(-dommage);
				else Debug.Log ("no LIfeComponent found");
            }
        }
    }

	LifeComponent FindLifeInParents(Transform trans)
	{
		var life = trans.GetComponent<LifeComponent>();
		if(life) return life;
		if(trans.parent == null) return null;
		return FindLifeInParents(trans.parent);
	}
}
