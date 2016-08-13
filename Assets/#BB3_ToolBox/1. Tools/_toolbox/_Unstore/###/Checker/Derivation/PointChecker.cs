using UnityEngine;
using System.Collections;

public class PointChecker : Checker
{

    protected override Collider2D [] RetrieveCollider2D() {
        return Physics2D.OverlapPointAll(transform.position, inlayers); 
        }
    protected override Collider[] RetrieveCollider3D()
    {
        return Physics.OverlapSphere(transform.position, 0.001f, inlayers);
    }

}
