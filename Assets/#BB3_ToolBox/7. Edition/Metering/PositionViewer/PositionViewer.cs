using UnityEngine;
using System.Collections.Generic;
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
/// (Created:  1/29/2016 2:44:16 AM  )
/// (Last update:  dd/mm/yyyy  )
/// (Vesrion: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	
#endregion



namespace HakoBio.Tools.Metering { 
    [ExecuteInEditMode]
	public class PositionViewer : MonoBehaviour {

        public Distance.MetricUnitName _metricType = Distance.MetricUnitName.Meter;
        public enum MeasureType { Local, Global}
        public MeasureType _measureType = MeasureType.Global;


        [SerializeField]
        [Tooltip("Distance from the container (global or local)")]
        public Distance _distanceFromRoot = new Distance(Distance.MetricUnitName.Meter);

        [SerializeField]
        [Tooltip("X Coordinate depending of global/localt")]
        public Distance _distanceOnX = new Distance(Distance.MetricUnitName.Meter);

        [SerializeField]
        [Tooltip("Y Coordinate depending of global/localt")]
        public Distance _distanceOnY = new Distance(Distance.MetricUnitName.Meter);
        [SerializeField]
        [Tooltip("Z Coordinate depending of global/localt")]
        public Distance _distanceOnZ = new Distance(Distance.MetricUnitName.Meter);

        private Vector3 _lastPosition;

        public virtual string GetDisplayDescription() {
            return string.Format("D(xy):{0:0.00} , {1:0.00}  Height:{2:0.00}   Dist:{3:0.00}", _distanceOnX.GetDistance(), _distanceOnZ.GetDistance(), _distanceOnY.GetDistance(), _distanceFromRoot.GetDistance()); 

        }

        public void Awake() {
        #if  ! UNITY_EDITOR
                DestroyImmediate(gameObject);
        #endif
        }

        public void Update()
        {
            Vector3 currentPosition = _measureType == MeasureType.Global ? transform.position : transform.localPosition;

            if (_lastPosition != currentPosition)
                _lastPosition = currentPosition;
            Quaternion currentRotation = _measureType == MeasureType.Global || transform.parent==null ? Quaternion.identity : transform.parent.rotation;


            _distanceFromRoot.SetDistance(currentPosition.magnitude, _metricType);
            _distanceOnX.SetDistance(currentPosition.x, _metricType);
            _distanceOnY.SetDistance(currentPosition.y, _metricType);
            _distanceOnZ.SetDistance(currentPosition.z, _metricType);

            gameObject.name = GetDisplayDescription();
            gameObject.transform.rotation = currentRotation;
            

        }



    }
}