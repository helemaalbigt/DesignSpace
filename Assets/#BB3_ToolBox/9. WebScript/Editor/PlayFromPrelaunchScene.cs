using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

#region \,,/(◣_◢)\,,/
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Code by: Eloi Strée
/// Code for: Ouat (HakoBioVR) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  1/27/2016 11:28:08 AM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace BlackBox.Tools { 
	public class PlayFromPrelaunchScene : Editor {

        [MenuItem("Window/Black Box 3/Shortcut/Play _F2")]
        public static void PlayInitScene()
        {
            if (EditorApplication.isPlaying == true)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            EditorApplication.SaveCurrentSceneIfUserWantsTo();
            EditorApplication.OpenScene("Assets/StartScene.unity");
            EditorApplication.isPlaying = true;
        }
    }
}