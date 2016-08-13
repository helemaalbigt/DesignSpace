using UnityEngine;
using System.Collections.Generic;
using System;
//using System;


///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
///
/// Code by: Eloi Strée
/// Code for: Curious Craft / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
///



namespace CanonMoles { 
	public class PourcentZone : MonoBehaviour {

        public Transform rootPoint;
        public float xDist = 16;
        public float yDist = 9;

        public LayerMask allowLayers;

        public Vector3 GetPosition(Vector2 coordPourcent) {

            Vector3 finalPosition;
            //Get horizontal point;
            Vector3 position = rootPoint.position;
            position.x += xDist * coordPourcent.x;
            position.z += yDist * coordPourcent.y;
            position.y = 15f;

            finalPosition = position;
            //Get ground point;

            Ray ray = new Ray(position, Vector3.down);
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit, float.MaxValue, allowLayers)) {
                finalPosition = rayhit.point;
            }

            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);
            Debug.DrawLine(position, finalPosition, Color.red,1f);
            return finalPosition;

        }

        internal Vector2 GetPositionInPourcent(Vector3 where)
        {
            if (rootPoint == null) return Vector2.zero;
            where -= rootPoint.position;
            where.x /= xDist;
            where.z /= yDist;
            where.y = 0;

            return new Vector2(where.x, where.z);

        }
    }
}