using UnityEngine;
using System.Collections;

public class LookAtPointRecorder : MonoBehaviour {



    [SerializeField]
    private float _frameRecordInterval;
    [SerializeField]
    private Transform _rootReference;
    private LayerMask _layerAllowToCollideWithLook;

    [Header("Debug Viewer")]
    [SerializeField]
    private bool _recording;
    [SerializeField]
    private float _timeSinceStartRecording;

    public LookPath _lookPathAffected = new LookPath();
    public Transform _trackedTransform;
    public LookPathToJSON _lookPathConverter;

    void Start() {
        InvokeRepeating("RecordFrame", 0, _frameRecordInterval);
    }

    void RecordFrame() {
        if (_trackedTransform == null) return;
        if (!_recording) return;
   
        Vector3 rootPoint = _rootReference.InverseTransformPoint(_trackedTransform.position);

        RaycastHit hit;
        Vector3 lookAtPoint = Vector3.zero;
        if (Physics.Raycast(_trackedTransform.position, _trackedTransform.forward, out hit))
            lookAtPoint = hit.point;
        lookAtPoint = _rootReference.InverseTransformPoint(lookAtPoint);

        _lookPathAffected.AddLookState(_timeSinceStartRecording, rootPoint, lookAtPoint);

    }

    void Update() {
        if (_recording) {
            _timeSinceStartRecording += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Return))
            if (_recording)
                StopRecord();
            else
                StartRecord();
    }



    void StartRecord() {
        _recording = true;
        _lookPathAffected.Reset();

    }

    void StopRecord()
    {
        _recording = false;
        _lookPathConverter.RecordPath(_lookPathAffected);
    }
}
