using UnityEngine;
using System.Collections;
using System;

public class LifeTime : MonoBehaviour {


    public float timeToLive=5f;
    public float countDown = 0;

    public Action doBeforeDeath;
	public bool withBroadCast;
    public bool deactivate=true;
    public bool destroy=true;
    public bool resetOnEnable=true;


    public void Reset() {
        countDown = timeToLive;
        this.gameObject.SetActive(true);
    }

    public void OnEnable() {
        if (resetOnEnable)
            Reset();
    }

	void Update () {
        if (countDown <= 0f) return;
        countDown -= Time.deltaTime;
        if (countDown <= 0f)
        {
			if(withBroadCast){
				SendMessage("OnDeath");
			}
            if (doBeforeDeath!=null)
                doBeforeDeath();
            if (deactivate)
                this.gameObject.SetActive(false);
            if (destroy)
               Destroy( this.gameObject );
            
        }
	
	}
}
