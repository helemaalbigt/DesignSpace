using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EarthRotationInteraction : MonoBehaviour {

    public float _forceMultiplier = 400f;

    private Vector3 _grabPos; //the point where the earth was grabbed
    private Rigidbody _rigidBody;

    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void HoverOn(WandController C)
    {
        if (C.triggerDown)
        {
            InputController.inUse = true;
            StartCoroutine(DragEarth(C));
        }
    }

    IEnumerator DragEarth(WandController C)
    {
        _grabPos = transform.InverseTransformPoint(C.cursor.curPos);

        while (C.triggerPress)
        {

            Vector3 curLocalCursorPos = transform.InverseTransformPoint(C.cursor.curPos);
            Vector3 posDelta = curLocalCursorPos - _grabPos;
            posDelta = transform.TransformVector(posDelta) * _forceMultiplier;
            Vector3 grabPosWorld = transform.TransformPoint(_grabPos);

            _rigidBody.AddForceAtPosition(posDelta, grabPosWorld);
            //_rigidbody.velocity = posDelta * _VelocityFactor * Time.fixedDeltaTime;
            yield return null;
        }

        InputController.inUse = false;
        yield return null;
    }
}
