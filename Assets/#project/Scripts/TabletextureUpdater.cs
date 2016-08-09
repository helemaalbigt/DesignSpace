using UnityEngine;
using System.Collections;

public class TabletextureUpdater : MonoBehaviour {

    public Transform _GlobalWrapper;

    private Renderer _Rend;

    void Start()
    {
        _Rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float factor = 10f;
        _Rend.material.SetTextureOffset("_MainTex", new Vector2(_GlobalWrapper.localPosition.x * factor, -_GlobalWrapper.localPosition.y * factor));
    }
}
