/**
 * Stores reference to controller that hit this menu last
 * 
 * Used for runctions that need to knwo what cursor to use
 */

using UnityEngine;
using System.Collections;

public class MenuControllerReference : MonoBehaviour {

    public WandController controller;
	
	// Update is called once per frame
	public void HoverOn (WandController C) {
        controller = C;
	}
}
