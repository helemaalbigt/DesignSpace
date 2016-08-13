using UnityEngine;
using System.Collections;
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
/// Clean:     40 %
/// Documented:10 %
/// Reusable:  60 %
/// Readable:  20 %
/// Quick Tested: none
/// Stress Tested: none
/// 
/// Improve list: 
/// - do more tools methode code for manipulate distance,
/// - finish refactor,
/// - documentation,
/// - test & verify,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Last update:  dd/mm/yyyy  )
/// (Version: 0.0)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	

namespace BlackBox.Beans.Basic{
 
public class Distance
{

        public Distance() { }
        public Distance(MetricUnit metricUnit)
        {
            _usedMetricFactor = (int)metricUnit;
        }
        public Distance(MetricUnitName metricUnit)
        {
            _usedMetricFactor = (int)metricUnit;
        }

        [SerializeField]
    [Tooltip("Distance that is measured")]
    private float _distanceValue = 0f;
    [SerializeField]
        [Tooltip("Distance multiplicator: distance * 10 pow X")]
        private int _usedMetricFactor = 0;

        [SerializeField]
        private int test;   

        public int MyProperty
        {
            get { return test; }
            set { test = value; }
        }

        public enum MetricUnitName : int { Yoctometre = -24, Zeptometre = -21, Attometre = -18, Femtometre = -15, Picometre = -12, Nanometre = -9, Micrometre = -6, Millimetre = -3, Centimetre = -2, Decimetre = -1,Meter=0, Decametre = 1, Hectometre = 2, Kilometre = 3, Megametre = 6, Gigametre = 9, Terametre = 12, Petametre = 15, Exametre = 18, Zettametre = 21, Yottametre = 24 };
    public enum MetricUnit : int { ym = -24, zm = -21, am = -18, fm = -15, pm = -12, nm = -9, pim = -6, mm = -3, cm = -2, dm = -1,m, dam = 1, hm = 2, Km = 3, Mm = 6, Gm = 9, Tm = 12, Pm = 15, Em = 18, Zm = 21, Ym = 24 };



    public void SetDistance(float distance) { _distanceValue = distance; }

    public void SetMetricUnity(int metricFactor) { _usedMetricFactor = metricFactor; }
    public void SetMetricUnity(MetricUnit metricFactor) { _usedMetricFactor = (int)metricFactor; }
    public void SetMetricUnity(MetricUnitName metricFactor) { _usedMetricFactor = (int)metricFactor; }

    public void SetDistance(float distance, int metricFactor)
    {
        SetDistance(distance);
        SetMetricUnity(metricFactor);
    }
    public void SetDistance(float distance, MetricUnit metricFactor)
    { SetDistance(distance, (int)metricFactor); }
    public void SetDistance(float distance, MetricUnitName metricFactorName)
    { SetDistance(distance, (int)metricFactorName); }




    public float GetDistance() { return _distanceValue * Mathf.Pow(10, _usedMetricFactor); }


    //    Value SI symbol Name    Value SI symbol Name
    //10−1 m dm  decimetre	    101 m dam decametre
    //10−2 m cm  centimetre	    102 m hm  hectometre
    //10−3 m mm  millimetre 	103 m km  kilometre
    //10−6 m µm  micrometre 	106 m Mm  megametre
    //10−9 m nm  nanometre	    109 m Gm  gigametre
    //10−12 m pm  picometre	    1012 m Tm  terametre
    //10−15 m fm  femtometre	1015 m Pm  petametre
    //10−18 m am  attometre 	1018 m Em  exametre
    //10−21 m zm  zeptometre	1021 m Zm  zettametre
    //10−24 m ym  yoctometre	1024 m Ym  yottametre
}
}