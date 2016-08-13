using UnityEngine;
using System.Collections.Generic;
using System;
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
/// Code for: Ouat (HakoBio One) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  2/12/2016 5:28:20 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace Undefine { 
	public class FollowTaggedObject : FollowPoint {


        public new void Update() {
            base.Update();
            if (pointFollowed != null) return;
            TagPlayerCentralView playerView = TagPlayerCentralView.InstanceInScene;
            
            if (playerView == null)
                return;

            SetFollowed(playerView.GetTaggedTransform());
        }

    }
}