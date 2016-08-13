using UnityEngine;
using System.Collections;

public class SurferIk : MonoBehaviour {


    public bool ikActive = true;
    public Animator animator;

    public Transform headPoint;

    public Transform handLeftPoint;
    public Transform elbowLeftHint;

    public Transform handRightPoint;
    public Transform elbowRightHint;

    public Transform footLeftPoint;
    public Transform kneeLeftHint;

    public Transform footRightPoint;
    public Transform kneeRightHint;


	
	void OnAnimatorIK () {




        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1f);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1f);
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);

        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetLookAtWeight(1f);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, footLeftPoint.position);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, footRightPoint.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, handLeftPoint.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, handRightPoint.position);

        animator.SetIKHintPosition(AvatarIKHint.LeftKnee, kneeLeftHint.position);
        animator.SetIKHintPosition(AvatarIKHint.RightKnee, kneeRightHint.position);
        animator.SetIKHintPosition(AvatarIKHint.LeftElbow, elbowLeftHint.position);
        animator.SetIKHintPosition(AvatarIKHint.RightElbow, elbowRightHint.position);

        animator.SetIKRotation(AvatarIKGoal.LeftFoot, footLeftPoint.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, footRightPoint.rotation);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, handLeftPoint.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRightPoint.rotation);

        animator.SetLookAtPosition(headPoint.position);
	}
}
