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

public interface I_X360InitData {

	/**Refresh information on the x360 controller */
	void RefreshData();
	/**Get information on the actual state of the x360 controller*/
	I_X360Data GetData();
}
