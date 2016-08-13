using UnityEngine;
using System.Collections.Generic;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;


///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Proud:     20 % 
/// Clean:     80 %
/// Documented:10 %
/// Reusable:  80 %
/// Readable:  60 %
/// Quick Tested: none
/// Stress Tested: none
/// 
/// Improve list: 
/// - do setter more friendly with float value,
/// - finish refactor,
/// - documentation,
/// - test & verify,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Last update:  11/01/2016  )
/// (Version: 0.1)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	


namespace BlackBox.Beans.Basic
{
    [System.Serializable]
    public class Volume
    {

        [SerializeField]
        private Distance _depth;
        private Distance _height;
        private Distance _width;

        public Distance GetDepthDistance() { return _depth; }
        public Distance GetHeightDistance() { return _height; }
        public Distance GetWidthDistance() { return _width; }
        public void SetDepthDistance(Distance depth) { _depth = depth; }
        public void SetHeightDistance(Distance height) { _height = height; }
        public void SetWidthDistance(Distance width) { _width = width; }


        public void SetPosition(Distance depth, Distance height, Distance width)
        {
            SetDepthDistance( depth );
            SetHeightDistance( height );
            SetWidthDistance( width );
        }


        public Vector3 GetUnityPosition()
        {
            Vector3 position = new Vector3(
                _depth.GetDistance(),
                _height.GetDistance(),
                _width.GetDistance()
                );
            return position;
        }

    }
}