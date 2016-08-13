using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LifeComponent))]
public class LifeComponentDestruction : MonoBehaviour {

    private LifeComponent life;
    public EventActivator activator;
    public bool deactivatedOnDeath;
    public bool destroyOnDeath;

	void Start () {

        life = GetComponent<LifeComponent>();
        life.onNoLifeAnymore+=DestrucitonOfElementManagement;
	}

    private void DestrucitonOfElementManagement(LifeComponent obj)
    {
        activator.Activate();
        if (deactivatedOnDeath) gameObject.SetActive(false);
        if (destroyOnDeath) Destroy(this);
    }
	
	
}
