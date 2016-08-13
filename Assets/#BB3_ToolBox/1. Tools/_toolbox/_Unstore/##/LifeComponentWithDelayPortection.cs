using UnityEngine;
using System.Collections;

public class LifeComponentWithDelayPortection : LifeComponent {


    public override float Life
    {
        get
        {
            return base.Life;
        }
        set
        {
            bool loseLife = value < base.Life;
			if (loseLife)
			{
				if (invisibleCooldown > 0f) return;
				else invisibleCooldown = invisibleTimeWhenHit;
			}
			base.Life = value;
        }
    }

    public float invisibleTimeWhenHit=1f;
    float invisibleCooldown;


    public void Update() {
        if (invisibleCooldown > 0f) {

            invisibleCooldown -= Time.deltaTime;
            if (invisibleCooldown < 0f) invisibleCooldown = 0f;
        }
        
    }
}
