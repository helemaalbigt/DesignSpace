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
/// Code for: Ouat (HakoBio One) / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Created:  2/12/2016 5:31:05 PM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace Undefine { 
	public class TaggedObject : MonoBehaviour {


        [SerializeField]
        [Tooltip("Transform that represent the central view of the player")]
        private Transform _taggedObject;
        public bool _readOnly;
        #region Setter/Getter (playerView)
        /// <summary>
        /// Getter of playerView
        /// Twitter description: Transform that represent the central view of the player
        /// </summary>
        public Transform GetTaggedTransform()
        {
            return _taggedObject;
        }

        public Vector3 GetPosition(bool asLocal = false)
        {
            if (asLocal)
                return _taggedObject.localPosition;
            else
                return _taggedObject.position;
        }
        public Quaternion GetRotation(bool asLocal = false)
        {
            if (asLocal)
                return _taggedObject.localRotation;
            else
                return _taggedObject.rotation;
        }

        public void SetPosition(Vector3 playerViewPosition, bool asLocal = false)
        {
            if (_readOnly) { Debug.LogWarning("Read Only!", this.gameObject); return; }
            if (asLocal)
                _taggedObject.localPosition = playerViewPosition;
            else
                _taggedObject.position = playerViewPosition;
        }
        public void SetRotation(Quaternion playerViewRotation, bool asLocal = false)
        {
            if (_readOnly) { Debug.LogWarning("Read Only!", this.gameObject); return; }
            if (asLocal)
                _taggedObject.localRotation = playerViewRotation;
            else
                _taggedObject.rotation = playerViewRotation;
        }
        #endregion
        protected virtual void Awake()
        {
            if (_taggedObject == null)
            {
                _taggedObject = GetComponent<Transform>() as Transform;
            }
            if (_taggedObject == null)
            {
                Debug.LogError("This tagged object is invalide. It has to be linked to a transform.");
                Destroy(this);
                return;
            }

            _tagsInScene.Add(this);

        }


        private static List<TaggedObject> _tagsInScene = new List<TaggedObject>();

        public static List<TaggedObject> TagsInScene
        {
            get { return _tagsInScene; }
        }
        
    }
}