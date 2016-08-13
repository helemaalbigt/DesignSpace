using UnityEngine;
using System.Collections;

public class SphereChecker : Checker {

   public  float radius = 1f;
   
    protected override Collider2D[] RetrieveCollider2D()
    {
        return Physics2D.OverlapCircleAll(transform.position, radius, inlayers);
    }
    protected override Collider[] RetrieveCollider3D()
    {
        return Physics.OverlapSphere(transform.position, radius, inlayers);
    }

}
