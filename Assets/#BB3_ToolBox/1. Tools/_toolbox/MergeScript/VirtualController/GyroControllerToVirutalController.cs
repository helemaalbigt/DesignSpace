using UnityEngine;
using System.Collections;

public class GyroControllerToVirutalController : MonoBehaviour {

    public string controllerName;
    public VirtualController virtualController;
    public int orientationIndex;
    public GyroController gyroController;

    public Quaternion initialRotation;
    public Quaternion currentRotation;
    public bool horizontalOnly = true;

    public Quaternion LastOrientation;

    public bool hasGyro;
    void Start()
    {
        if (gyroController == null || virtualController == null)
        {
            Debug.Log("Params not initialized", this.gameObject);
            Destroy(this);
            return;
        }
        gyroController.AttachGyro();
        Invoke("ResetViewPosition", 1f);
        virtualController = VirtualController.Get(controllerName);
    }

    void Update()
    {
        if (gyroController == null || virtualController == null) 
            return;
        if (hasGyro) { 
            RefreshData();
            LastOrientation = Quaternion.Inverse(initialRotation) * currentRotation;
            virtualController.SetOrientationValue(orientationIndex, LastOrientation);
        }
        
    }

    private void RefreshData()
    {
        currentRotation = gyroController.LastGlobalRotation;
    }

    public void ResetViewPosition()
    {
        gyroController.AttachGyro();
        hasGyro = !( Input.gyro.attitude == Quaternion.identity);
        if (!hasGyro) {
            gyroController.DetachGyro();
            LastOrientation = currentRotation = initialRotation = Quaternion.Euler(0, 0, 0);
            virtualController.SetOrientationValue(orientationIndex, LastOrientation);
            Debug.Log("No gyro !");
        }
        else { 
            Quaternion rot = gyroController.LastGlobalRotation;
            if (horizontalOnly)
            {
                Vector3 eulerRot = rot.eulerAngles;
                eulerRot.x = 0;
                rot = Quaternion.Euler(eulerRot);


            }
            initialRotation = rot;
        }
    }
}
