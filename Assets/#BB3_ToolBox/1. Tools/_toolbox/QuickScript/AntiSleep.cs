using UnityEngine;
using System.Collections;

public class AntiSleep : MonoBehaviour {

	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
}
