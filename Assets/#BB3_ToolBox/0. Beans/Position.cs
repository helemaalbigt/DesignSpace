using UnityEngine;
using System.Collections;


///     \,,/(◣_◢)\,,/       
/// Proud:     30 % 
/// Clean:     20 %
/// Documented:10 %
/// Reusable:  20 %
/// Readable:  20 %
/// Improve list: 
/// - finish refactor,
/// - documentation,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Last update:  11/01/2016  )
/// (Version: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	


namespace BlackBox.Beans.Basic
{
    [System.Serializable]
    public class Position
    {

        [SerializeField]
        private Distance _forward;
        private Distance _up;
        private Distance _right;

        public Distance GetForwardDistance() { return _forward; }
        public Distance GetUpDistance() { return _up; }
        public Distance GetRightDistance() { return _right; }
        public void SetForwardDistance(Distance forwardDistance) { _forward = forwardDistance; }
        public void SetUpDistance(Distance upDistance) { _up = upDistance; }
        public void SetRightDistance(Distance widthDistance) { _right = widthDistance; }


        public void SetPosition(Distance right, Distance up, Distance forward)
        {
            _up = up;
            _right = right;
            _forward = forward;
        }


        public Vector3 GetUnityPosition()
        {
            Vector3 position = new Vector3(
                _up.GetDistance(),
                _forward.GetDistance(),
                _right.GetDistance()
                );
            return position;
        }

    }
}