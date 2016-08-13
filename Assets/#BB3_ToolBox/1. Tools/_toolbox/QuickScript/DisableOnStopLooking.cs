using UnityEngine;
using System.Collections;

public class DisableOnStopLooking : MonoBehaviour {


    public float timeBetweenCheck = 1f;
    public float timeBeforeDisable = 1f;
    [Range(0f, 0.5f)]
    public float borderHorizontal = 0.1f;
    [Range(0f, 0.5f)]
    public float borderVertical = 0.1f;

    public delegate void OnOutOfView(float timeBeforeDisable);
    public OnOutOfView onOutOfView;

     void OnEnable() {

         StartCoroutine(RegularyCheckToDisable());

    }

     IEnumerator RegularyCheckToDisable() {

         while (true) {
             Vector3 view = Camera.main.WorldToViewportPoint(transform.position);
             borderVertical = Mathf.Clamp(borderVertical, 0f, 5f);
             borderHorizontal = Mathf.Clamp(borderHorizontal, 0f, 5f);
             if (view.x < borderHorizontal || view.x > 1f - borderHorizontal || view.y < borderVertical || view.y > 1f - borderVertical)
             {

                 if (onOutOfView != null)
                     onOutOfView(timeBeforeDisable);
                 yield return new WaitForSeconds(timeBeforeDisable);
                 gameObject.SetActive(false);

             }
             
             yield return new WaitForSeconds(timeBetweenCheck);
         }
     }


}
