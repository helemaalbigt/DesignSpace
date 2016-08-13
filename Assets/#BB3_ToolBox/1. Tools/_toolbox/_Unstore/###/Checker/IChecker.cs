using System;
using UnityEngine;

    interface IChecker
    {
        Collider2D[] GetColliders2D();
        Collider[] GetColliders3D();
        bool IsColliding2D();
        bool IsColliding3D();
    }

