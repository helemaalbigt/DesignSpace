using UnityEngine;
using System.Collections.Generic;
using BlackBox.Tools;
using BlackBox.Beans.Basic;
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
/// (Created:  1/27/2016 8:02:23 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace BlackBox.Tools { 
	public class NamedIdRef : IdRef  {



        public NamedIdRef(string id, string name):base(id) {
            SetNameOfTheId(new Name(name));
        }

        [SerializeField]
        [Tooltip("Named to display on the current id")]
        private Name _idName;
        #region Setter/Getter (idName)
        /// <summary>
        /// Getter of idName
        /// Twitter description: Named to display on the current id
        /// </summary>
        public Name GetNameOfTheId()
        {
            return _idName;
        }
        /// <summary>
        /// Setter of idName
        /// Twitter description: Named to display on the current id
        /// </summary>
        public void SetNameOfTheId(Name idName)
        {
             if (idName==null || string.IsNullOrEmpty(idName.Value))
            	   throw new System.ArgumentException("idName should not be null or empty!");
            _idName = idName;
        }
        #endregion



    }
}