//using UnityEngine;
//using System.Collections;
//
//public class MyOVR_AmplifyRotation : MyOVR_Positionning
//{
//
//    public Transform rotateObject;
//    public float minRotationSpeed = 20f;
//    public float maxRotationSpeed = 130f;
//    public float minRotation = 5f;
//    public float maxRotation = 45f;
//
//    public float camDistance = 0.0333f;
//
//
//
//    protected override void PositionOculusInScene(OVRPose leftPose, OVRPose rightPose, Transform left, Transform center, Transform right, bool alreadyPositined)
//    {
//        if (!this.enabled) return;
//        if (left == null || center == null || right == null) return;
//        if (root == null)
//        {
//            Debug.Log("No root define !", this.gameObject);
//        }
//
//
//        Vector3 centerPosition = (leftPose.position + rightPose.position) / 2f;
//        Vector3 centerToRight = rightPose.position - centerPosition;
//        Vector3 centerToLeft = leftPose.position - centerPosition;
//
//        
//
//       ApplyPositionningAt(left, center, right, ref centerPosition, ref centerToRight, ref centerToLeft, ref leftPose.orientation, ref rightPose.orientation);
//
//    }
//
//    protected void ApplyPositionningAt(Transform leftOVREye, Transform OVRcenter, Transform rightOVREye, ref Vector3 centerPosition, ref Vector3 toGoToTheRight, ref Vector3 toGoToTheLeft, ref Quaternion orientationLeft, ref Quaternion orientationRight)
//    {
//        if (leftOVREye == null || OVRcenter == null || rightOVREye == null) return;
//
//        Quaternion ovrPlayerRotation = orientationLeft;
//        Vector3 eulerPlayerRotation = ovrPlayerRotation.eulerAngles;
//        eulerPlayerRotation = Convert0To360EuleurTo180Euleur(eulerPlayerRotation);
//
//        Quaternion currentPlayerOrientation = rotateObject.rotation;
//
//        Quaternion lookThowardOrientation = currentPlayerOrientation * ovrPlayerRotation;
//
//        float angleBetweenWantedAndCurrent = Quaternion.Angle(currentPlayerOrientation, lookThowardOrientation);
//        Quaternion finalRotationToApply;
//        if (angleBetweenWantedAndCurrent < minRotation)
//        {
//
//            float rotationPourcentPower = (angleBetweenWantedAndCurrent) / (minRotation);
//            rotationPourcentPower = Mathf.Clamp(rotationPourcentPower, 0f, 1f);
//            float rotationDegree = minRotationSpeed * rotationPourcentPower;
//
//            finalRotationToApply = Quaternion.RotateTowards(
//                currentPlayerOrientation,
//                lookThowardOrientation,
//                Mathf.Clamp(rotationDegree * Time.deltaTime, 0f, minRotationSpeed));
//        }
//        else
//        {
//
//
//        float rotationPourcentPower = (angleBetweenWantedAndCurrent - minRotation) / (maxRotation - minRotation);
//        rotationPourcentPower = Mathf.Clamp(rotationPourcentPower, 0f, 1f);
//        float rotationDegree = minRotationSpeed + (maxRotationSpeed - minRotationSpeed) * rotationPourcentPower;
//
//            finalRotationToApply = Quaternion.RotateTowards(
//                currentPlayerOrientation,
//                lookThowardOrientation,
//                Mathf.Clamp(rotationDegree * Time.deltaTime, 0f, maxRotationSpeed));
//        }
//        rotateObject.rotation = finalRotationToApply;
//
//      
//        OVRcenter.localRotation = Quaternion.identity;
//        camDistance = Vector3.Distance(toGoToTheRight, toGoToTheLeft) / 2f;
//        OVRcenter.localPosition = centerPosition;
//        leftOVREye.localPosition =Vector3.right * -camDistance;
//        rightOVREye.localPosition = Vector3.right * camDistance;
//
//    }
//
//    private static Vector3 Convert0To360EuleurTo180Euleur(Vector3 eulerPlayerRotation)
//    {
//        eulerPlayerRotation.z -= 180f;
//        eulerPlayerRotation.y -= 180f;
//        eulerPlayerRotation.x -= 180f;
//        eulerPlayerRotation.x *= -1f;
//
//        return eulerPlayerRotation;
//    }
//}
