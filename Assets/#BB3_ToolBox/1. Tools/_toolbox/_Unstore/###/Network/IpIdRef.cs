using UnityEngine;
using System.Collections.Generic;
//using System;


///     \,,/(◣_◢)\,,/       
/// 
/// Twitter description:
/// No description has been set yet. 
/// Please proceed !
/// 
///
/// Code by: Eloi Strée
/// Code for: Curious Craft / Me
/// Contact: www.stree.be/eloi/ - streeeloi@gmail.com
///



namespace CanonMoles { 
	public class IpIdRef : MonoBehaviour {
        public UserIpLink _userIpLink;
        public UsersListOfServer _usersList;

        public void SetIdRef(UserIpLink userIpLink, UsersListOfServer usersList) {
            _userIpLink = userIpLink;
            _usersList = usersList;
        }
        public UserIpLink GetIpLink() { return _userIpLink; }
        public UsersListOfServer GetUsers() { return _usersList; }
        public int GetPlayerType() { return _usersList.GetPlayerType(this); }
    }
}