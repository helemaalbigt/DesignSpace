using UnityEngine;
using System.Collections;

public class LookAtPointRecorder : MonoBehaviour {



    [SerializeField]
    private float _frameRecordInterval;

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
        _lookPathAffected.AddLookState(_timeSinceStartRecording, _trackedTransform.position, _trackedTransform.position + transform.forward);

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
