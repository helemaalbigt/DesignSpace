using UnityEngine;
using System.Collections.Generic;
using BlackBox.Beans.Basic;
//using System;
//using BlackBox.Beans.Basic;
using BlackBox.Tools.IO;
using System;

#region \,,/(◣_◢)\,,/
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Proud:     ?? % 
/// Clean:     ?? %
/// Reusable:  ?? %
/// Readable:  ?? %
/// Quick Tested: none
/// Stress Tested: none
/// 
/// Improve list: 
/// - finish first version,
/// - finish refactor,
/// - documentation,
/// - test & verify,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat (HakoBioVR) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  2/1/2016 12:49:05 AM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



#region CODE TO IMPROVE
////*      ( >_< )    */    
/// Twitter description:
/// This class should be three class, one dealing with create URL, two child one( UrlToCreate , URlFromProperties)  
/// What happened: I am stupid -__- !
/// Ashame of my code:     90 % 
/// What to do (?): Create several generic class from this one. 
#endregion

namespace Undefine { 
	public class CreateProjectLinkedURL : MonoBehaviour {

        public ProjectFilePath _configurationAccessPath;
        public ProjectDirectoryPath _linkBasicPattern;
        public List<UrlToCreate> _linkedUrl = new List<UrlToCreate>();
        
        public string[] _propertyKeyName = new string[] { "WebSite", "ServerUrl" };
        public void Start() {
            
            Properties prop = Properties.CreateWith(_configurationAccessPath.LoadText());

            if (_configurationAccessPath.Exist())
            { 
                for (int i = 0; i < _propertyKeyName.Length; i++)
                {
                    AddUrlFromProperties(_propertyKeyName[i], ref prop);

                }
            }


            for (int i = 0; i < _linkedUrl.Count; i++)
            {
                if (  !string.IsNullOrEmpty(_linkedUrl[i]._name) && !string.IsNullOrEmpty(_linkedUrl[i]._url.GetUrl()))
                {
                    ProjectFilePath file = ProjectFilePath.CreateFrom(_linkBasicPattern);
                    file.SetFileName(_linkedUrl[i]._name);
                    file.SetFileType("URL");

                    ProjectPathTools.SaveFileAt(string.Format("[InternetShortcut]\n URL={0}\n  IDList =\n HotKey=0\n  IconIndex = 0\n", _linkedUrl[i]._url),file,true);
                    
                }
            }
                
        }

        public void AddUrlFromProperties(string key, ref Properties properties)
        {
            string webSite = properties.Get(key);
            if (webSite != null)
            {
                try
                {
                    WebURL url = WebURL.Create(webSite);
                    _linkedUrl.Add(new UrlToCreate() { _name = key, _url = url });
                }
                catch (Exception e) { Debug.Log("No website found:" + e.StackTrace.ToString()); }
            }
        }

        [Serializable]
        public struct UrlToCreate
        {
            public string _name;
            public WebURL _url;
        }

    }
}