using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using BlackBox;
using BlackBox.Tools.IO;
using BlackBox.Beans.Basic;

public class AppConfigurationProperties {



    [SerializeField]
    [Tooltip("Properties configuration of the application")]
    private static Properties _appProperties = new Properties();
    #region Setter/Getter (appProperties)
    /// <summary>
    /// Getter of appProperties
    /// Twitter description: Properties configuration of the application
    /// </summary>
    public static Properties GetProperties()
    {
        return _appProperties;
    }
    /// <summary>
    /// Setter of appProperties
    /// Twitter description: Properties configuration of the application
    /// </summary>
    public static void SetProperties(Properties appProperties)
    {
         if (appProperties == null) 
        	   throw new System.ArgumentException("Properties should not be null !");
        _appProperties = appProperties;
    }
    #endregion


}
public class Insp_PropertyConfig : MonoBehaviour
{
    
    public ProjectFilePath _filePath= new ProjectFilePath("HakoBio","Data", "Config","xml");
    void Awake()
    {
        string xmlText = ProjectPathTools.LoadFileAt(_filePath);
        Properties properties = Properties.CreateWith(xmlText);
        AppConfigurationProperties.SetProperties(properties);
        Debug.Log(properties.ToString());
        Destroy(this);

    }
}
#region OLD CODE
		
//public class ConfigProperties {
//    private static Dictionary<string,string> _propertiesLoaded = new Dictionary<string, string>();

//    public static Dictionary<string,string> PropertiesLoaded
//    {
//        get { return _propertiesLoaded; }
//    }

//    public static bool IsPropertyLoaded() { return _propertiesLoaded.Count>0; }

//    public static void SetProperties(Dictionary<string, string> properties) {
//        if (properties != null && properties.Count > 0)
//            _propertiesLoaded = properties;
//    }

//    public static void AddProperty(string key, string value) {
//        if (string.IsNullOrEmpty(key))
//            return;

//        if (_propertiesLoaded.ContainsKey(key))
//            _propertiesLoaded[key] = value;
//        else _propertiesLoaded.Add(key, value);
//    }

//    public static void DebugLog_ShowProperties() {
//        string[] keys = new string[_propertiesLoaded.Keys.Count];
//        _propertiesLoaded.Keys.CopyTo(keys, 0);
//        if (keys.Length == 0)
//            Debug.Log(">>>> No Properties found");
//        else { 
//            Debug.Log(">>>> Properties");
//            for (int i = 0; i < keys.Length; i++)
//            {
//                string key = keys[i];
//                Debug.Log(">> "+ key + " :"+ _propertiesLoaded[key]);

//            }
//        }
//    } 
//}

	#endregion