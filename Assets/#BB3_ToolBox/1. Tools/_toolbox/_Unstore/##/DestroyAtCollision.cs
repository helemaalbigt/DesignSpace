using UnityEngine;
using System.Collections;

public class DestroyAtCollision : MonoBehaviour {
	
	public bool deactivateOnCollision=true;
	public bool destroyOnCollision;
	public LayerMask explodeWith;
    public bool triggerToo = true;
    public bool castExplosionMessgae = false;

    public void OnCollisionEnter(Collision col)
    {
        if (IsExplosiveLayer(col.gameObject.layer))
            Explose();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (!triggerToo) return;
        if (IsExplosiveLayer(col.gameObject.layer))
            Explose();
    }

	bool IsExplosiveLayer(int layer)
	{
		var layerMask = 1<<layer;
		int commonMask = layerMask & explodeWith.value;
		return commonMask != 0;
	}

	void Explose ()
	{
        if (castExplosionMessgae)
            gameObject.BroadcastMessage("Explode",SendMessageOptions.DontRequireReceiver);
		if(deactivateOnCollision)
			gameObject.SetActive (false);
		if(destroyOnCollision)
			Destroy(gameObject);
	}
}
