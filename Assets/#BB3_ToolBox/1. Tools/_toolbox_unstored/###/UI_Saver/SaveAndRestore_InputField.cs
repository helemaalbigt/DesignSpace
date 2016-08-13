using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SaveAndRestore_InputField : SaveAndRestore {

    [SerializeField]
    private InputField _inputFieldTargeted;

    public override string GetInfoToSaveFromSource()
    {
        return _inputFieldTargeted.text;
    }

    public override void SetInfoToOrignalSource(string text)
    {
        _inputFieldTargeted.text = text;
    }
}
