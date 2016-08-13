using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class SetRendererOrder : MonoBehaviour
{
    public Renderer renderer;
    public enum ShaderOrder : int { Default = -1, Custom = -2, Farest_0 = 0, Normal_2000 = 2000, Close_10000 = 10000, Closer_20000 = 20000, VeryCloser_9000000 = 9000000 }
    public ShaderOrder rendererOrder = ShaderOrder.Default;
    public int customOrder = -1;
    public int oldSharderOrder = -1;
    void Awake()
    {
        Init();

	}

    public void Init()
    {
        if(renderer==null)
        renderer = GetComponent<Renderer>();
        oldSharderOrder = renderer.material.renderQueue;
        
        if(rendererOrder == ShaderOrder.Custom)
            SetOrder( customOrder);
        else
            SetOrder((int)rendererOrder);
    }
    public void SetOrder(int order) {
        Renderer renderer = GetComponent<Renderer>();
        if (!renderer || !renderer.sharedMaterial )
            return;
        renderer.sharedMaterial.renderQueue = order;
        for (int i = 0;  i < renderer.sharedMaterials.Length; i++)
            renderer.sharedMaterials[i].renderQueue = order;
    
    }
    //public int queue = 1;

    //public int[] queues;

    //protected void Start()
    //{
    //    Renderer renderer = GetComponent<Renderer>();
    //    if (!renderer || !renderer.sharedMaterial || queues == null)
    //        return;
    //    renderer.sharedMaterial.renderQueue = queue;
    //    for (int i = 0; i < queues.Length && i < renderer.sharedMaterials.Length; i++)
    //        renderer.sharedMaterials[i].renderQueue = queues[i];
    //}
}
