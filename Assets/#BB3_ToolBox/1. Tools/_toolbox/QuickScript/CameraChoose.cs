using UnityEngine;
using System.Collections;

public class CameraChoose : MonoBehaviour {

    [Header("Choose if you want VR, FPS view in game")]
    public CameraType cameraType = CameraType.TPS;
    public enum CameraType { VR, FPS, TPS }
    public Transform cameraVR;
    public Transform cameraTPS;
    public Transform cameraFPS;

    void Awake() {

        switch (cameraType)
        {   

                //Code like pig;
            case CameraType.VR:
                if(cameraVR)
                   cameraVR.gameObject.SetActive(true);

                if (cameraTPS)
                   cameraTPS.gameObject.SetActive(false);

                if (cameraFPS)
                   cameraFPS.gameObject.SetActive(false);
                break;
            case CameraType.FPS:

                if (cameraVR)
                   cameraVR.gameObject.SetActive(false);
                if (cameraTPS)
                   cameraTPS.gameObject.SetActive(false);
                if (cameraFPS)
                   cameraFPS.gameObject.SetActive(true);
                break;
            case CameraType.TPS:

                if (cameraVR)
                   cameraVR.gameObject.SetActive(false);
                if (cameraTPS)
                   cameraTPS.gameObject.SetActive(true);
                if (cameraFPS)
                   cameraFPS.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
