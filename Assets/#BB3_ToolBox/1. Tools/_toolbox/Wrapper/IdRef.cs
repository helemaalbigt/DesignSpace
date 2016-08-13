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
/// (Created:  1/24/2016 2:08:13 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace BlackBox.Tools {
    [System.Serializable]
    public class IdRef
    {
        public string Value
        {
            get
            {
                return _idReference;
            }

            set
            {
                _idReference = value;
            }
        }
        public string GetId() { return Value; }
        public bool IsIntId() { return GetIntId() != -1; }
        public int GetIntId()
        {
            int id;
            if (int.TryParse(Value, out id))
                return id;
            return -1;
        }
        public override string ToString()
        {
            return _idReference.ToString();
        }

        [SerializeField]
        [Tooltip("Value stored as unique reference")]
        private string _idReference;
        
        public IdRef(string linkedId)
        {

            if (string.IsNullOrEmpty(linkedId))
                throw new System.ArgumentException("Id can't be null or empty");
            this._idReference = linkedId;
        }
    }
}