using UnityEngine;
using System.Collections;

public class HomidoUtility : MonoBehaviour {



    public static Vector2 GetAccelerationAsJoystick(float horizontalRange, float verticalRange, bool withClamp, bool useAxisIfStandAlone = false)
    {



        Vector2 result = Vector2.zero;

        if(useAxisIfStandAlone)
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            result.x = Input.GetAxis("Horizontal");
            result.y = Input.GetAxis("Vertical");    
            return result;
#endif
        }
        
        float ratio;

        ratio = 1f / Mathf.Abs(horizontalRange);
        result.x = Input.acceleration.x * ratio;


        ratio = 1f / Mathf.Abs(verticalRange);
        result.y = Input.acceleration.z * ratio;

        if (withClamp)
        {
            result.x = Mathf.Clamp(result.x, -1f, 1f);
            result.y = Mathf.Clamp(result.y,  - 1f, 1f);
        }
        return result;
    }


    public float GetDistanceBetween(float valueA, float valueB) {
        return Mathf.Abs(valueB-valueA);
    }

}
