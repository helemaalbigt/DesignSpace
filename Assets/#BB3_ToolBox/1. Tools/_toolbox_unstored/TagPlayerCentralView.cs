using UnityEngine;
using System.Collections;
using Undefine;
public class TagPlayerCentralView : TaggedObject {

    private static TagPlayerCentralView _instanceInScene;

    public static TagPlayerCentralView InstanceInScene
    {
        get { return _instanceInScene; }
        set { _instanceInScene = value; }
    }



    protected override void Awake() {
        base.Awake();
        if(_instanceInScene==null)
        _instanceInScene = this;
    }
    protected void OnDestroy() {
        if (_instanceInScene == this) {
            _instanceInScene = null;
        }
        ResearchNewOneInTheScene();

    }

    public void ResearchNewOneInTheScene()
    {
        TagPlayerCentralView[] tagPlayer = GameObject.FindObjectsOfType<TagPlayerCentralView>() as TagPlayerCentralView[];
        for (int i = 0; i < tagPlayer.Length; i++)
        {
            if (tagPlayer[i] != null && tagPlayer[i] != this)
                _instanceInScene = tagPlayer[i];
        }
    }


    


}
