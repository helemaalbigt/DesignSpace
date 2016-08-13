/*
 * --------------------------BEER-WARE LICENSE--------------------------------
 * PrIMD42@gmail.com wrote this file. As long as you retain this notice you
 * can do whatever you want with this code. If you think
 * this stuff is worth it, you can buy me a beer in return, 
 *  S. E.
 * Donate a beer: http://www.primd.be/donate/ 
 * Contact: http://www.primd.be/
 * ----------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	public GameObject player;
	public float cameraVelocity=1f;
	public float height = 10f;
	public float prevision=1.5f;
	public float readjustement=1f;

	public Camera actualCamera;
	public void Update(){

		if(player!=null)
		{
			if(actualCamera==null)
				actualCamera = Camera.main;
			if(actualCamera!=null){
			Vector3 actualPos = actualCamera.transform.position;

				actualPos+= (player.transform.position -actualPos)*Time.deltaTime*cameraVelocity;
			actualPos.y = player.transform.position.y + height;
			actualCamera.transform.position= actualPos;
			
			}
		}
	}

}
