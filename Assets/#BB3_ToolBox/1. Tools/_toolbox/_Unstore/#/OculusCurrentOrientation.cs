using UnityEngine;
using System.Collections;

public class OculusCurrentOrientation : MonoBehaviour {


    public Transform root;
    public Transform center;
    public Transform leftEyee;
    public Transform rightEyee;
    public static OculusCurrentOrientation Instance;

	void OnEnable () {
        Instance = this;
	}

    public static bool IsDefined() { return Instance != null; }
    public static Quaternion GetOrientation() { return Instance.center.rotation; }
    public static Quaternion GetLocalOrientation() { return Instance.center.localRotation; }

    public static Quaternion GetRootOrientation() { return Instance.root.rotation; }
    public static Quaternion GetRootLocalOrientation() { return Instance.root.localRotation; }

    public static Vector3 GetRootPosition() { return Instance.root.position; }
    public static Vector3 GetCenterPosition() { return Instance.center.position; }
	
}
