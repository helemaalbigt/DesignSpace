using UnityEngine;
using System.Collections.Generic;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

#region \,,/(◣_◢)\,,/
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Proud:     ?? % 
/// Clean:     ?? %
/// Reusable:  ?? %
/// Readable:  ?? %
/// Quick Tested: none
/// Stress Tested: none
/// 
/// Improve list: 
/// - finish first version,
/// - finish refactor,
/// - documentation,
/// - test & verify,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat (HakoBioVR) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  2/1/2016 1:52:00 AM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion


namespace BlackBox.Tools.Extensions
{
    public static class VectorTool
    {
        //public static Vector3 SetWithPlan2D(this Vector3 v, Vector2 plan2D, float height=0)
        //{
        //    return new Vector3(plan2D.x, height, plan2D.y) ;
        //}

        //public static Vector3 AsTopView3D(this Vector3 vector, Vector2 coordinateXY)
        //{
        //    return new Vector3(coordinateXY.x, 0, coordinateXY.y);
        //}
        //public static Vector2 AsTopView2D(this Vector3 vector, Vector3 coordinateXY)
        //{
        //    return new Vector2(coordinateXY.x, coordinateXY.z);
        //}

        public static Vector3 AsTopView3D(Vector2 coordinateXY,float height=0)
        {
            return new Vector3(coordinateXY.x, height, coordinateXY.y);
        }
        public static Vector2 AsTopView2D(Vector3 coordinateXY)
        {
            return new Vector2(coordinateXY.x, coordinateXY.z);
        }
    }
}