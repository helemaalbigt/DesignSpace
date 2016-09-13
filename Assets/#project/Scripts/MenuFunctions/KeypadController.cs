using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class KeypadController : MonoBehaviour {

    public InputField _activeInputField;

    public KeyValue[] _inputKeys;
    public MenuButton _backSpace;
    public MenuButton _submit;

    void OnEnable()
    {
        foreach(KeyValue key in _inputKeys)
        {
            key.OnKeyPress += AddToField;
        }

        _backSpace.OnClick += BackSpace;
        _submit.OnClick += Submit;
    }

    void OnDisable()
    {
        foreach (KeyValue key in _inputKeys)
        {
            key.OnKeyPress -= AddToField;
        }

        _backSpace.OnClick -= BackSpace;
        _submit.OnClick -= Submit;
    }
	
    private void AddToField(string s)
    {
        _activeInputField.text += s;
    }

    private void BackSpace()
    {
        _activeInputField.text = _activeInputField.text.Substring(0, _activeInputField.text.Length - 1);
    }

    private void Submit()
    {
        Destroy(this.gameObject);
        _activeInputField.onEndEdit.Invoke(_activeInputField.text);
        if (_activeInputField.transform.GetComponent<MenuRadioButton>() != null)
            _activeInputField.transform.GetComponent<MenuRadioButton>().Deselect();
    }
}
