using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

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
/// Code for: StreetGame (JJB66) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  1/25/2016 8:13:31 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion

namespace BlackBox.Beans.Basic
{ 
    [System.Serializable]
	public class WebURL  {

        public static WebURL Create(string url) { return new WebURL() { _url = url }; }

        [SerializeField]
        [Tooltip("Web url http://www.url.com/")]
        private string _url;

        
        #region Setter/Getter (url)
        /// <summary>
        /// Getter of url
        /// Twitter description: Web url http://www.url.com/
        /// </summary>
        public string GetUrl()
        {
            return _url;
        }
        /// <summary>
        /// Setter of url
        /// Twitter description: Web url http://www.url.com/
        /// </summary>
        public void SetUrl(string url)
        {
            if (string.IsNullOrEmpty(url) && ! IsValidPath(url))

                throw new System.ArgumentException("Url is not valide");
            
            _url = url;
        }

        public bool IsUrlDefined() {
            return ! string.IsNullOrEmpty(_url) && IsValidPath(_url);
        }
        #endregion




        private bool IsValidPath(string path)
        {
            Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
            if (!driveCheck.IsMatch(path.Substring(0, 3))) return false;
            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            if (containsABadCharacter.IsMatch(path.Substring(3, path.Length - 3)))
                return false;

            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(path));
            if (!dir.Exists)
                dir.Create();
            return true;
        }




        public override string ToString()
        {
            return GetUrl();
        }
    }
}