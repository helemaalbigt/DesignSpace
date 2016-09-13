using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeypadSpawner : MonoBehaviour {

    public GameObject _keypadPrefab;
    private KeypadController _keypadController;
    private GameObject _keypad;

    public void SpawnKeyPad()
    {
        _keypad = Instantiate(_keypadPrefab, transform) as GameObject;
        _keypadController = _keypad.GetComponent<KeypadController>();

        _keypadController._activeInputField = GetComponent<InputField>();

        _keypad.transform.parent = transform;
        _keypad.transform.localPosition = Vector3.zero;
        _keypad.transform.localRotation = Quaternion.identity;
    }

    public void DestroyKeyPad()
    {
        if(_keypad != null)
            Destroy(this._keypad);
    }
}
