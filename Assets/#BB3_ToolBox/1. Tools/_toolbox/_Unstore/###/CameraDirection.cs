using UnityEngine;
using System.Collections;

public class CameraDirection : MonoBehaviour {


    public Transform setTransDirection;

    void Update()
    {
        //if (setTransDirection==null) return;
        //if (Camera.main == null || Camera.allCamerasCount <= 0) return;
       
        Quaternion mainCameraOrientation = Camera.main.transform.rotation;

        Vector3 cameraPosition = Vector3.zero;
        int cameraActive=0;
		var allCamerasCache = Camera.allCameras;

		for (int i = 0; i < allCamerasCache.Length; i++)
        {
			Camera cam = allCamerasCache[i];
			//cam != null && cam.isActiveAndEnabled && 
            if (cam.gameObject.activeInHierarchy && cam.gameObject.activeSelf && cam.CompareTag("MainCamera")) { 
                //if (cameraPosition == Vector3.zero)
                //     cameraPosition = cam.transform.position;
                //else
                cameraPosition += cam.transform.position;
                cameraActive++;
                // Debug.Log(cameraActive + ": ", cam.gameObject);
            }
        }
        cameraPosition/=cameraActive;
        setTransDirection.position = cameraPosition;
        setTransDirection.rotation = mainCameraOrientation;
	
	}
}
