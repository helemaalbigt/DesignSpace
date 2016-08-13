using UnityEngine;
using System.Collections;

public class FollowPoint : MonoBehaviour {

    public enum FollowPositionType { Direct, Lerp, None }
    public FollowPositionType positionFollow;
    public enum FollowRotationType { Excate, Horizontal, None }
    public FollowRotationType rotationFollow;
    public bool jumpAtPositionOnSet=true;
    public Transform pointFollowed;
    public bool destroyIfNotFound=true;
    public void Awake() {

        if (CheckAndDestroy()) return;
        //if (positionFollow == FollowPositionType.Direct) {

        //    pointFollowed.position = pointFollowed.position;
        //}
        SetFollowed(pointFollowed);
    }

    public virtual bool CheckAndDestroy() { 
         if (destroyIfNotFound && pointFollowed == null  )
        {
            Destroy(this);
            return true;
        }
         return pointFollowed == null;
    }
    protected virtual void Update() {

        if (CheckAndDestroy()) return;
       
        if (!pointFollowed) return;
        if (positionFollow == FollowPositionType.Direct) {

            this.transform.position = pointFollowed.position;

        }else if (positionFollow == FollowPositionType.Lerp)
        {

            this.transform.position =Vector3.Lerp(this.transform.position, pointFollowed.position,Time.deltaTime);
        }

        if (rotationFollow == FollowRotationType.Excate)
        {

            this.transform.rotation = pointFollowed.rotation;

        }
        else if (rotationFollow == FollowRotationType.Horizontal)
        {
            Vector3 currentEuleurRot = pointFollowed.rotation.eulerAngles;
            currentEuleurRot.x = 0; currentEuleurRot.z = 0;
            this.transform.rotation = Quaternion.Euler(currentEuleurRot);
        }
    }



    protected void SetFollowed(Transform followed)
    {
        if (jumpAtPositionOnSet && followed)
        {
            this.transform.position = followed.position;
            this.transform.rotation = followed.rotation;
        }
        pointFollowed = followed;
    }
}
