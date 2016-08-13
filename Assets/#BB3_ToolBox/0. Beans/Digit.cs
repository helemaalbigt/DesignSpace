using UnityEngine;
using System.Collections;
using System.Collections.Generic;


///     \,,/(◣_◢)\,,/       
/// Proud:     40 % 
/// Clean:     60 %
/// Documented:10 %
/// Reusable:  70 %
/// Readable:  60 %
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
    [SerializeField]
    public class Digit  {


        [SerializeField]
        public byte _value;
        public Digit(int value_0_9)
        {
            SetDigitTo(value_0_9);
        }
        public void SetDigitTo(int value_0_9) { _value = (byte)Mathf.Clamp(value_0_9, 0, 9); }
        public void SetDigitTo(short value_0_9) { _value = (byte)Mathf.Clamp(value_0_9, 0, 9); }
        public int GetAsIntValue() { return (int)_value; }
        public int GetAsCharValue(){
            switch (_value)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                default: return '0';
            }
        }

        public static string GetDigitAsString(Digit[] digits)
        {

            string digitResult = "";
            for (int i = 0; i < digits.Length; i++)
            {
                digitResult += digits[i].GetAsCharValue();

            }
            return digitResult;
        }

    }

}