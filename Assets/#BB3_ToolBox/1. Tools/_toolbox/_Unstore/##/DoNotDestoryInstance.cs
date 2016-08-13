using UnityEngine;
using System.Collections;
using System;
public class DoNotDestoryInstance: MonoBehaviour {

	public double creationTime;

	public void Awake(){
		creationTime = DateTime.Now.TimeOfDay.TotalMilliseconds;	
		DontDestroyOnLoad (transform.gameObject);
	}
	public void OnLevelWasLoaded (int sceneNum)
	{

        GameObject[] otherSingleton = GameObject.FindObjectsOfType(typeof(DoNotDestoryInstance)) as GameObject[];
		KillAllOthersSingletonIn (otherSingleton);
	}

	void KillAllOthersSingletonIn (GameObject[] otherSingleton)
	{
		if(otherSingleton!=null)
		foreach (GameObject os in otherSingleton) {
			//If two singleton are in the same place and declare them king of the scene, the blood with flow
			if (HasSameName (os) && IsNotCurrent (os)) {
				// Battle can start
				if (IsYounger (os)) {
					//Debug.Break();
					ClaimTerritory (this.gameObject);
					Destroy (os);
				}
				else {
					
					//Debug.Break();
					ClaimTerritory (os);
					Destroy (this.gameObject);
				}
			}
		}
	}

	bool HasSameName (GameObject os)
	{
		if (os == null)
						return false;
		return os.name.Equals (this.gameObject.name);
	}
	
	bool IsYounger (GameObject os)
	{
		if (os == null)
			return true;
        DoNotDestoryInstance singleton = os.GetComponent<DoNotDestoryInstance>();
		if (singleton == null)
						return true;

		return singleton.creationTime > creationTime;
	}
	
	bool IsNotCurrent (GameObject os)
	{
		return os != this.gameObject;
	}

	void ClaimTerritory (GameObject gameObject)
	{
		Debug.LogWarning ("You are a younger singleton then me (" + gameObject + "), die bitch !!! Ha ha ha, I am the real king");
	}
}
