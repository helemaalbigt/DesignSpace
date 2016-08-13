using UnityEngine;
using System.Collections;

public class LineChecker : Checker {

    public Transform endLinePoint;
    protected override Collider2D[] RetrieveCollider2D()
    {   
        
        if(endLinePoint==null) return new Collider2D[0];
        RaycastHit2D[] rayhits=null;
        rayhits = Physics2D.LinecastAll(transform.position,endLinePoint.position, inlayers);
        if (rayhits==null ) return new Collider2D[0];
        Collider2D[] cols = new Collider2D[rayhits.Length];
        for(int i=0; i<rayhits.Length;i++){
            cols[i] = rayhits[i].collider;
        }
        return cols;

    }
    protected override Collider[] RetrieveCollider3D()
    {
       

        if (endLinePoint) return new Collider[0];
        RaycastHit[] rayhits = null;
        
        Vector3 dir = endLinePoint.position - transform.position;
        float d = Vector3.Distance(endLinePoint.position, transform.position);
        rayhits = Physics.RaycastAll(transform.position, dir, d, inlayers);
        
        if (rayhits == null) return new Collider[0];
        Collider[] cols = new Collider[rayhits.Length];
        for (int i = 0; i < rayhits.Length; i++)
        {
            cols[i] = rayhits[i].collider;
        }
        return cols;
    }

}
