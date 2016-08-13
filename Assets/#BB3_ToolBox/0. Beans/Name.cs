using UnityEngine;
using System.Collections.Generic;
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
/// Code for: Ouat (HakoBioVR) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  1/24/2016 8:47:41 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace BlackBox.Beans.Basic { 
	public class Name  {

        [SerializeField]
        [Tooltip("What is the name of it ?")]
        private string _name;

        public string Value { get { return _name; } set { _name = value; } }

        public Name(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentException("Name can't be null or empty");
            _name = name;
        }
        #region Setter/Getter (name)
        /// <summary>
        /// Getter of name
        /// Twitter description: What is the name of it ?
        /// </summary>
        public string GetName()
        {
          return  GetName(FormattedText.None);
        }
        public enum FormattedText { Name, NAME, name, None}
        public string GetName(FormattedText wantedFormat) {

            string nResult = _name;
            switch (wantedFormat)
            {
                case FormattedText.Name:
                    string val = "";
                    if (nResult.Length >= 1)
                        val += ("" + nResult[0]).ToUpper();
                    if (nResult.Length >= 2)
                        val += (nResult.Substring(1)).ToLower();
                    return val;
                case FormattedText.NAME:
                    return nResult.ToUpper();
                case FormattedText.name:
                    return nResult.ToLower();
                default:
                    return nResult;
            }
        }
        /// <summary>
        /// Setter of name
        /// Twitter description: What is the name of it ?
        /// </summary>
        public void SetName(string name, bool onlyAlphaNumeric=true)
        {
            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentException("Name should not be set as null or empty");
            //TODO Check if the name is alpahnumeric only
            _name = name;
        }
        #endregion
        

    }
}