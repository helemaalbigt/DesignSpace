using UnityEngine;
using System.Collections.Generic;
using System;
using BlackBox.Beans.Basic;
using System.Text.RegularExpressions;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Proud:     60 % 
/// Clean:     50 %
/// Reusable:  40 %
/// Readable:  30 %
/// Quick Tested: none
/// Stress Tested: none
/// 
/// Improve list: 
/// - finish refactor,
/// - documentation,
/// - test & verify,
/// - plugify.
///
/// Code by: Eloi Strée
/// Code for: Ouat / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
/// (Last update:  12/01/2016  )
/// (Version: 0.1)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	




namespace BlackBox.Beans.Basic { 
	[System.Serializable]
    public class Version
    {
        public static Version Create(string name, string description, params int[] versionNumbers)
        {
            return Create(NamedDescription.Create(name, description), versionNumbers);
        }

        public static Version CreateFromUnity() {

            //TIME MANAGEMENT
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            DateTime time = DateTime.Now;
            string date = string.Format("{0:00}/{1:00}/{2:0000}", time.Day, time.Month, time.Year);


            //TRY TO CONVERT VERSION
            int[] versionInNumbers = ConvertUnityVersionToIntTable(Application.version);

            //ACCESS NAME AND DESCRIPTION
            string versionName = Application.productName;
            string version = Version.GetVersionFormat(versionInNumbers," ", ".");
            string description = string.Format("Build of the application \"{0}\" (Version: {1}  - Date: {2}) ", versionName, version, date );


            return Create(NamedDescription.Create(Application.productName, description), versionInNumbers);

        }

        private static string GetVersionFormat(int[] versionInNumbers, string versionSpace=" ", string betweenNumbers=".")
        {
            string resultVersion = "V" + versionSpace;
            for (int i = 0; i < versionInNumbers.Length; i++)
            {
                if (i != 0)
                    resultVersion += betweenNumbers;
                resultVersion += versionInNumbers[i];
            }
            return resultVersion;
        }

        private static int[] ConvertUnityVersionToIntTable(string version)
        {
            int[] versionResult= new int[0];
            Regex regex = new Regex(@"\d+");
            MatchCollection matches = regex.Matches(version);
            versionResult =new int [matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                versionResult[i] = int.Parse(matches[i].Value);
            }
            return versionResult;
        }

        internal static Version Create()
        {
            return new Version();
        }

        public static Version Create(NamedDescription description, params int[] versionNumbers)
        {
            Version version = new Version();
            version.SetDescription(description);
            version.SetVersionNumbersTo(versionNumbers);
            return version;
        }

        private Version() {
            //TODO Application.version -> Version;
            _versionNumbers = new int[] { 0, 0, 0 };
            _versionDescription = new NamedDescription("Untitled", "This version has not description set up");
        }
        [SerializeField]
        [Tooltip("V_0_5_5, V.0.2.9 : What is the verison")]
        private int[] _versionNumbers;

        [SerializeField]
        [Tooltip("Minimum information on the version.")]
        private NamedDescription _versionDescription;

        
        
        public bool HasDescription() { return _versionDescription != null; }
        public NamedDescription GetLinkedDescription() { return _versionDescription ; }
        public void SetDescription(NamedDescription description) { _versionDescription= description; }

        public void SetVersionNumbersTo(int[] versionNumbers)
        {
            if (versionNumbers == null) return;
            _versionNumbers = versionNumbers;
        }
        public int[] GetVersionNumbers()
        {
            return _versionNumbers;
        }

        public string GetVersionAsString(char separator='_')
        {
            string description = "V";
            for (int i = 0; i < _versionNumbers.Length; i++)
            {
                description +=separator+ _versionNumbers[i];
            }
            return description;
        }


        public void SetVersionWithString(char separator, string versionText) {
            
            try
            {
                string[] tokens = versionText.Split(separator);
                
                if (tokens.Length <= 1) 
                    return;
                
                int[] numbers = new int[tokens.Length - 1];

                for (int i = 1; i < tokens.Length; i++)
                {
                    numbers[i-1] = int.Parse(tokens[i]);
                }

                SetVersionNumbersTo( numbers);
            }
            catch (Exception e) {
                Debug.Log(e.StackTrace);
                return;
            }
        }

		 
	}
}