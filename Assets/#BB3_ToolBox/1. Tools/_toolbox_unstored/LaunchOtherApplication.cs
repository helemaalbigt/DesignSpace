using UnityEngine;
using System.Collections;

public class LaunchOtherApplication : MonoBehaviour {

    public string _downloadLink = "http://hakobio.com";
    public string _bundleTargetedId ="be.hakobio.vx";
	public void LaunchApplication ()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
                bool fail = false;
                string bundleId = _bundleTargetedId; // your target bundle id
                AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

                AndroidJavaObject launchIntent = null;
                try
                {
                    launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
                }
                catch (System.Exception e)
                {
                    fail = true;
                }

                if (fail)
                { //open app in store
                    Application.OpenURL(_downloadLink);
                }
                else //open the app
                    ca.Call("startActivity", launchIntent);
                up.Dispose();
                ca.Dispose();
                packageManager.Dispose();
                launchIntent.Dispose();
        #endif
    }

}
