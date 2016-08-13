using UnityEngine;
using System.Collections;

public abstract class Checker : MonoBehaviour, IChecker {

    public string[] withTags;
    public LayerMask inlayers;

    public Collider2D[] GetColliders2D()
    {

        return RetrieveCollider2D();
    }

    public Collider[] GetColliders3D()
    {
        return RetrieveCollider3D();
    }



    public bool IsColliding2D()
    {
        Collider2D[] cols = RetrieveCollider2D();
        if (HasNotCollider(ref cols)) return false;
        if (HasCollider(ref cols) && NotBasedOnTag()) return true;
        return HasColliderWithOneTag(ref cols, ref withTags);
    }



    public bool IsColliding3D()
    {
        Collider[] cols = RetrieveCollider3D();
        if (HasNotCollider(ref cols)) return false;
        if (HasCollider(ref cols) && NotBasedOnTag()) return true;
        return HasColliderWithOneTag(ref cols, ref withTags);
    }




    private bool HasColliderWithOneTag(ref Collider[] cols, ref string[] withTags)
    {
        foreach (Collider col in cols)
        {
            if (col == null) continue;
            foreach (string t in withTags)
                if (t != null && t.Equals(col.gameObject.tag)) return true;
        }
        return false;
    }


    private bool HasColliderWithOneTag(ref Collider2D[] cols, ref string[] withTags)
    {
        foreach (Collider2D col in cols)
        {
            if (col == null) continue;
            foreach (string t in withTags)
                if (t != null && t.Equals(col.gameObject.tag)) return true;
        }
        return false;
    }

    private static bool HasCollider(ref Collider[] cols)
    {
        return !HasNotCollider(ref cols);
    }

    private static bool HasNotCollider(ref Collider[] cols)
    {
        return cols == null || cols.Length <= 0;
    }

    private bool NotBasedOnTag()
    {
        return withTags == null || withTags.Length <= 0;
    }


    private static bool HasCollider(ref Collider2D[] cols)
    {
        return !HasNotCollider(ref cols);
    }

    private static bool HasNotCollider(ref Collider2D[] cols)
    {
        return cols == null || cols.Length <= 0;
    }



    protected abstract Collider2D[] RetrieveCollider2D();
    protected abstract Collider[] RetrieveCollider3D();

}

