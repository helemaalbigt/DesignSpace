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
/// (Created:  1/27/2016 12:04:22 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace Undefine { 
	public class FollowPointNamed : FollowPoint {


        public new void Awake()
        {
            GameObject toFollow = GameObject.Find(_nameToFollow);
            if(toFollow)
                pointFollowed = toFollow.transform;
            base.Awake();

        }

        [SerializeField]
        [Tooltip("Name of the point to follow in scene if non linked")]
        private string _nameToFollow;
        #region Setter/Getter (nameToFollow)
        /// <summary>
        /// Getter of nameToFollow
        /// Twitter description: Name of the point to follow in scene if non linked
        /// </summary>
        public string GetNameFollowed()
        {
            return _nameToFollow;
        }
        /// <summary>
        /// Setter of nameToFollow
        /// Twitter description: Name of the point to follow in scene if non linked
        /// </summary>
        public void SetNameFollowed(string nameToFollow)
        {
             if (string.IsNullOrEmpty(nameToFollow))
            	   throw new System.ArgumentException("nameToFollow should not be null or empty!");
            _nameToFollow = nameToFollow;
        }
        #endregion




    }
}