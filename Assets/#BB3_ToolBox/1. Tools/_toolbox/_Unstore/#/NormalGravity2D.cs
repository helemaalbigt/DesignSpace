using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class NormalGravity2D : MonoBehaviour {

    public Checker hasGround;
    public float decelerationWhenGoUp = 9.81f;
    public float decelerationWhenGoDown = 9.81f;

    void Start() 
    {
        if (!hasGround) { Destroy(this); return; }
    
    }

    void Update () {
        Rigidbody2D rig2D = GetComponent<Rigidbody2D>() as Rigidbody2D ;
        if (!hasGround) { return; }
        if (hasGround.IsColliding2D())
        {
            rig2D.gravityScale = 1f;
            return;
        }
        if (rig2D.velocity.y > 0f)
        {
            rig2D.gravityScale += decelerationWhenGoUp * Time.deltaTime;
        }
        else rig2D.gravityScale += decelerationWhenGoDown * Time.deltaTime;
    
	
	}
}
