using UnityEngine;
using System.Collections;

public class PlatformActivator : MonoBehaviour {
    public GameObject [] objects;
    public enum Platform { 
        UNITY_EDITOR, UNITY_EDITOR_WIN,
        UNITY_EDITOR_OSX, UNITY_STANDALONE_OSX,
        UNITY_DASHBOARD_WIDGET, UNITY_STANDALONE_WIN,
        UNITY_STANDALONE_LINUX, UNITY_STANDALONE,
        UNITY_WEBPLAYER, UNITY_WII, UNITY_IPHONE,
        UNITY_ANDROID, UNITY_PS3, UNITY_XBOX360,
        UNITY_FLASH, UNITY_BLACKBERRY, UNITY_WP8,
        UNITY_METRO, UNITY_WINRT }
    public Platform[] activeOn;
    void Awake () {

        bool belong = BelongTo(ref activeOn);
        foreach(GameObject gamo in objects)
          gamo.SetActive(belong);
	}

    public static bool BelongTo(ref Platform[] elements)
    {
#if UNIT_EDITOR
            if (BelongTo(Platform.UNIT_EDITOR, ref elements)) return true;
#endif
#if UNITY_EDITOR_WIN
        if (BelongTo(Platform.UNITY_EDITOR_WIN, ref elements)) return true;
#endif
#if UNITY_EDITOR_OSX
            if (BelongTo(Platform.UNITY_EDITOR_OSX, ref elements)) return true;
#endif
#if UNITY_STANDALONE_OSX
            if (BelongTo(Platform.UNITY_STANDALONE_OSX, ref elements)) return true;
#endif
#if UNITY_DASHBOARD_WIDGET
            if (BelongTo(Platform.UNITY_DASHBOARD_WIDGET, ref elements)) return true;
#endif
#if UNITY_STANDALONE_WIN
            if (BelongTo(Platform.UNITY_STANDALONE_WIN, ref elements)) return true;
#endif
#if UNITY_STANDALONE_LINUX
            if (BelongTo(Platform.UNITY_STANDALONE_LINUX, ref elements)) return true;
#endif
#if UNITY_STANDALONE
            if (BelongTo(Platform.UNITY_STANDALONE, ref elements)) return true;
#endif
#if UNITY_WEBPLAYER
            if (BelongTo(Platform.UNITY_WEBPLAYER, ref elements)) return true;
#endif
#if UNITY_WII
            if (BelongTo(Platform.UNITY_WII, ref elements)) return true;
#endif
#if UNITY_IPHONE
            if (BelongTo(Platform.UNITY_IPHONE, ref elements)) return true;
#endif
#if UNITY_ANDROID
            if (BelongTo(Platform.UNITY_ANDROID, ref elements)) return true;
#endif
#if UNITY_PS3
            if (BelongTo(Platform.UNITY_PS3, ref elements)) return true;
#endif
#if UNITY_XBOX360
            if (BelongTo(Platform.UNITY_XBOX360, ref elements)) return true;
#endif
#if UNITY_FLASH
            if (BelongTo(Platform.UNITY_FLASH, ref elements)) return true;
#endif
#if UNITY_BLACKBERRY
            if (BelongTo(Platform.UNITY_BLACKBERRY, ref elements)) return true;
#endif
#if UNITY_WP8
        if (BelongTo(Platform.UNITY_WP8, ref elements)) return true;
#endif
#if UNITY_METRO
            if (BelongTo(Platform.UNITY_METRO, ref elements)) return true;
#endif
#if  UNITY_WINRT
        if (BelongTo(Platform.UNITY_WINRT, ref elements)) return true;
#endif


        return false;
    }

    public static bool BelongTo(Platform platform, ref Platform[] elements) 
    {
        foreach (Platform p in elements)
           if( p == platform)return true;
        return false;
    }
}
