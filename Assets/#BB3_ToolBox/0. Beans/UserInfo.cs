using UnityEngine;
using System.Collections;
using System;

///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
/// Proud:     40 % 
/// Clean:     20 %
/// Documented:10 %
/// Reusable:  80 %
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
/// (Last update:  11/01/2016  )
/// (Version: 0.1)
///
/// In aim to have clean and reusable code:
/// Please try to beat the score.	
/// If you have better code, feel free to comment
/// and send it to the owner.	

namespace BlackBox.Beans.Basic
{
    [System.Serializable]
    public class UserInfo 
    {

        public static UserInfo Create(string username,string lastname, string firstname,string email, string password="", params int [] digitNumber) {
            UserInfo info = new UserInfo();
            info.SetUserName(username);
            info.SetLastName(lastname);
            info.SetFirstName(firstname);
            info.SetEmail(email);

            info.SetPassword(password);
            //TODO
            //info.SetDigitPassword(digitNumber);
            return info;
        }
        public UserInfo() { }

        [SerializeField]
        private string _userName = "";
        [SerializeField]
        private string _firstName = "";
        [SerializeField]
        private string _lastName = "";

        [SerializeField]
        private string _email = "";

        [SerializeField]
        private string _password = "";
        [SerializeField]
        private Digit[] _digitShortPassword = new Digit[] { new Digit(0), new Digit(0), new Digit(0), new Digit(0) };

        public string GetUserName() { return _userName; }
        public void SetUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new System.ArgumentNullException("!!!");
            if (userName != null)
                _userName = userName;
        }
        public string GetFirstName() { return _firstName; }
        public void SetFirstName(string firstName) { if (firstName != null) _firstName = firstName; }



        public string GetLastName() { return _lastName; }
        public void SetLastName(string lastName) { if (lastName != null) _lastName = lastName; }

        public static UserInfo Create(string userAccountName, string password)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.SetUserName(userAccountName);
            userInfo.SetPassword(password);
            return userInfo;
        }

        public string GetEmail() { return _email; }
        public void SetEmail(string email) { if (email != null) _email = email; }

        public bool HasPassword() { return string.IsNullOrEmpty(_password); }
        public bool HasDigitPassword() { return _digitShortPassword != null && _digitShortPassword.Length > 0; }

        
        public void SetDigitPassword(Digit[] digitalCode) { if (digitalCode != null) _digitShortPassword = digitalCode; }
        public Digit[] GetDigitPassword() { return _digitShortPassword; }


        public void SetPassword(string password) {
            if(password!=null)
            _password = password; }
        public string GetPassword() { return _password; }
        public string GetEncryptedPassword() { throw new System.NotImplementedException("Should return the password but with a standar encryption first"); }

        public bool IsParamsDefined() { return IsParamsDefined(true, true, true, true, true); }
        public bool IsParamsDefined(bool hasUserame, bool hasLastName, bool hasFirstName, bool hasEmail, bool hasPassword) {

            if (hasUserame && string.IsNullOrEmpty(GetUserName()))
                return false;

            if (hasLastName && string.IsNullOrEmpty(GetLastName()))
                return false;

            if (hasFirstName && string.IsNullOrEmpty(GetFirstName()))
                return false;

            if (hasEmail && string.IsNullOrEmpty(GetEmail()))
                return false;

            if (hasPassword && string.IsNullOrEmpty(GetPassword()))
                return false;
            return true;
        }

        public override string ToString()
        {
            return "User: " +
             GetUserName() + " - " +
             GetLastName() + " - " +
             GetFirstName() + " - " +
             GetEmail() + " - " +
             GetPassword();
        }
    }
}