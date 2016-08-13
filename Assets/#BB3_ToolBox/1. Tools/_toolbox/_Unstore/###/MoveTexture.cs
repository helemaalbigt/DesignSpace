using UnityEngine;
using System.Collections;

public class MoveTexture : MonoBehaviour {

    public Renderer rendererTargeted;
    public Vector2 directionAndSpeed;
    public float speed=1f;

	void Update () {
        if(rendererTargeted!=null)
            rendererTargeted.material.SetTextureOffset("_MainTex", new Vector2(Time.time * directionAndSpeed.x * speed, Time.time * directionAndSpeed.y * speed));
	}
}
