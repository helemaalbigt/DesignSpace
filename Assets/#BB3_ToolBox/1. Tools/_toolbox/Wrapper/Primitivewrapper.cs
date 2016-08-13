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
/// (Created:  1/24/2016 6:42:27 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace BlackBox.Tools
{
    public class PrimitiveWrapper<T> where T : struct
    {
        public static implicit operator T(PrimitiveWrapper<T> w)
        {
            return w.Value;
        }

        public PrimitiveWrapper(T val)
        {
            _wrappedValue = val;
        }

        public T Value
        {
            get
            {
                return _wrappedValue;
            }

            set
            {
                _wrappedValue = value;
            }
        }

        public override string ToString()
        {
            return _wrappedValue.ToString();
        }

        private T _wrappedValue;
    }
}