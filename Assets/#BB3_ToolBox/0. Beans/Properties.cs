using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.IO;
//using System;
//using BlackBox.Beans.Basic;
//using BlackBox.Tools.*;

///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// Properties is a simple class that contain Key,Value. It could have been a dictonary.
/// But as its main purpuse is to be present in inspector and be serialized, it is better to use a simple list.
/// 
/// Proud:     40 % 
/// Clean:     30 %
/// Reusable:  50 %
/// Readable:  50 %
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
/// (Last update:  19/01/2016  )
/// (Version: 0.1)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	




namespace BlackBox.Beans.Basic { 
	[SerializeField]
    public class Properties {

        public struct Property
        {
            public Property(string key, string value) : this()
            {
              SetKey( key);
              SetValue( value);
            }

            [SerializeField]
        [Tooltip("Unique Key that define the property")]
        private string _key;
        #region Setter/Getter (key)
            /// <summary>
            /// Getter of key
            /// Twitter description: Unique Key that define the property
            /// </summary>
            public string GetKey() {
                return _key;
            }
            /// <summary>
            /// Setter of key
            /// Twitter description: Unique Key that define the property
            /// </summary>
            public void SetKey(string key){
                if (string.IsNullOrEmpty(key)) throw new System.ArgumentNullException() ;
                _key = key;
            }
        #endregion
		

		[SerializeField]
        [Tooltip("Value linked to the key of the property")]
        private string _value;
         
           
            #region Setter/Getter (value)
            /// <summary>
            /// Getter of value
            /// Twitter description: Value linked to the key of the property
            /// </summary>
            public string GetValue() {
                return _value;
            }
            /// <summary>
            /// Setter of value
            /// Twitter description: Value linked to the key of the property
            /// </summary>
            public void SetValue(string value)
            {
                if (value == null) value = "";
                _value = value;
            }
            #endregion

            public override string ToString()
            {
                return GetKey()+"="+GetValue();
            }
        }

        public static Properties CreateWith(string xmlText)
        {
            Properties properties;
            GetProperty(xmlText, out properties);

            return properties;
        }

        public Properties()
        {}

        public Properties(Dictionary<string, string> dictonaryAsProperties)
        {
            AddDictionaryValueToProperties(dictonaryAsProperties);
        }

        private void AddDictionaryValueToProperties(Dictionary<string, string> dictonaryAsProperties)
        {
            if (dictonaryAsProperties == null) return;

            string[] keys = new string[dictonaryAsProperties.Keys.Count];
            dictonaryAsProperties.Keys.CopyTo(keys, 0);

            for (int i = 0; i < keys.Length; i++)
            {
                Add(keys[i], dictonaryAsProperties[keys[i]]);
            }
        }
	
		[SerializeField]
        [Tooltip("Linked properties that describe the object linked.")]
        private List<Property> _properties = new List<Property>();

        public string Get(string key)
        {
            for (int i = 0; i < _properties.Count; i++)
            {
                if (_properties[i].GetKey().Equals(key))
                    return _properties[i].GetValue();
            }
            return null;
        }
        #region Setter/Getter (properties)
        /// <summary>
        /// Getter of properties
        /// Twitter description: Linked properties that describe the object linked.
        /// </summary>
        public List<Property> GetProperties() {
                return _properties;
            }
            /// <summary>
            /// Setter of properties
            /// Twitter description: Linked properties that describe the object linked.
            /// </summary>
            public void SetProperties(List<Property> properties){
                //if (properties != null) return;
                _properties = properties;
            }
        #endregion
        #region Add property

            public void Remove(Property property)
            {
                _properties.Remove(property);
            }
            public void Remove(string keyValue) {

                for (int i = 0; i < _properties.Count; i++)
                {
                    if (_properties[i].GetKey().Equals(keyValue))
                        _properties.Remove(_properties[i]);
                }
            }
        public void Add(string key, string value)
        {
            Property prop = new Property(key, value);
            Add(prop);
        }

        public void Add(Property property)
        {
            if (!IsKeyContained(property.GetKey()))
                _properties.Add(property);
        }
        public void Set(string key, string value)
        {

            Property prop = new Property(key, value);
            Set(prop);
        }

        public void Set(Property property)
        {
            Remove(property.GetKey());
            Add(property);
        }

        public bool IsKeyContained(string key) {
                bool keyPresent = false;
                for (int i = 0; i < _properties.Count; i++)
                {
                    if (_properties[i].GetKey().Equals(key))
                    {
                        keyPresent = true;
                        break;
                    }
                }
                return keyPresent;
            }

        
            public void Clear() { _properties.Clear(); }
        #endregion

        #region XML Convertion

        public static void GetProperty(string xmlText, out Properties properties) {
            properties = new Properties();
            Dictionary<string, string> xmlKeyValue;
            if (GetProperty(xmlText, out xmlKeyValue)) {
                string[] keys = new string [xmlKeyValue.Keys.Count];
                xmlKeyValue.Keys.CopyTo(keys, 0);
                string key;
                for (int i = 0; i < keys.Length; i++)
                {
                    key = keys[i];
                    if (!string.IsNullOrEmpty(key)) {
                        properties.Add(key, xmlKeyValue[key]);
                    }
                }
            }
        }

        public static bool GetProperty(string xmlText, out Dictionary<string, string> properties)
        {
            properties = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(xmlText))
            {
                try
                {

                    XmlReader reader = XmlReader.Create(new StringReader(xmlText));

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {

                            if (reader.Name == "property")
                            {
                                try { 
                                    string id = reader.GetAttribute("name");
                                    string content = DecodeHtmlChars(reader.ReadElementContentAsString());
                                    properties.Add(id, content);
                                }
                                catch (Exception e)
                                { Debug.LogWarning("Impossible to read value of xml element:"+e.StackTrace); }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }

            }
            return true;
        }
        #endregion


        public override string ToString()
        {
            string result = "Properties:";
            for (int i = 0; i < _properties.Count; i++)
            {
                if (i != 0)
                    result += ", ";
                result += _properties[i].ToString();
            }
            return result;
        }
        

        internal static string GetPropertyAsXml(Properties propertiesToConvert)
        {
            string propertyXml = "<properties>\n\r";
            foreach (Property prop in propertiesToConvert.GetProperties()) {
                propertyXml += string.Format("<property name=\"{0}\">{1}</property>\n\r", prop.GetKey(), EncodeHtmlChars(prop.GetValue()));
            }
            propertyXml += "</properties>\n\r";
            return propertyXml; 
        }

          public static string EncodeHtmlChars(string source) {

            return source.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">","&gt;").Replace("\"", "&quot;");

        }

        public static string DecodeHtmlChars( string source)
        {
            string[] parts = source.Split(new string[] { "&#x" }, StringSplitOptions.None);
            for (int i = 1; i < parts.Length; i++)
            {
                int n = parts[i].IndexOf(';');
                string number = parts[i].Substring(0, n);
                try
                {
                    int unicode = Convert.ToInt32(number, 16);
                    parts[i] = ((char)unicode) + parts[i].Substring(n + 1);
                }
                catch { }
            }
            return String.Join("", parts);
        }
    }
}