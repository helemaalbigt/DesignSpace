using UnityEngine;
using System.Collections.Generic;
using System;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// Give a name and a description to a object
/// 
/// Proud:     ?? % 
/// Clean:     ?? %
/// Documentd: ?? %
/// Documented:10 %
/// Reusable:  ?? %
/// Readable:  ?? %
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


namespace BlackBox.Beans.Basic
{
    [System.Serializable]
    public class NamedDescription
    {
        public NamedDescription() {
            _name = "";
            _description = "";
        }

        internal static NamedDescription Create(string name, string description)
        {
            return new NamedDescription(name, description);
        }

        public NamedDescription(string name, string twitterDescription) {
            SetDescriptionName(name);
            SetDescription(twitterDescription);
        }

        [SerializeField]
        [Tooltip("What is it ?")]
        private string _name;

        [SerializeField]
        [Tooltip("Twitter description.")]
        [Multiline(4)]
        private string _description;

        public void SetDescriptionName(string name) {
            if (name == null) name = "";
            _name = name;
        }

        public string GetDescriptionName(string description)
        {
            if (description == null) description = "";

            return _name;
        }
        public string GetDescription()
        {
            return _description;
        }
        public void SetDescription(string description) {
            _description = description;
        }


    }

    [System.Serializable]
    public class Description
    {
        public Description()
        {
            _description = "";
        }

        internal static Description Create( string description)
        {
            return new Description( description);
        }

        public Description( string twitterDescription)
        {
            SetDescription(twitterDescription);
        }

        
        [SerializeField]
        [Tooltip("Twitter description.")]
        [Multiline(4)]
        private string _description;
        public string GetDescription()
        {
            return _description;
        }
        public void SetDescription(string description)
        {
            _description = description;
        }

    }
}