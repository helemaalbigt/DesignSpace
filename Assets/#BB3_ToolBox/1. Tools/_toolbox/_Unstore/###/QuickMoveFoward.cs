using UnityEngine;
using System.Collections;

public class QuickMoveFoward : MonoBehaviour {

    public float speed;
    public float startSpeed=2f;
    public float maxSpeed=10f;
    public float acceleration=1f;
    public bool autoLaunch;

    void OnEnable() {
        Launch();
    }
    void OnDisable()
    {
        Stop();
    }
	// Update is called once per frame
	void Update () {

        if (speed == 0f) return;
        if (speed < maxSpeed) {
            speed += acceleration * Time.deltaTime;;
            if (speed > maxSpeed)
                speed = maxSpeed;
        }
        transform.Translate(Vector3.forward * (speed*Time.deltaTime));
	
	}
    public void Launch()
    {
        speed = startSpeed;
    }
    public void Stop()
    {
        speed = 0;
    }

    public void SetFoward(Vector3 direction) { transform.forward = direction; }

    internal void SetSpeed(float speed,float acceleration =0f)
    {
        this.startSpeed = speed;
        this.acceleration = acceleration;
    }
}
