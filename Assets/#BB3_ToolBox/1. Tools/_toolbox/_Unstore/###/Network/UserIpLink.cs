using UnityEngine;
using System.Collections.Generic;
//using System;
using BlackBox.Beans.Basic;

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
    [System.Serializable]
	public class UserIpLink {//: MonoBehaviour {



        [SerializeField]
        [Tooltip("Ip of the User")]
        private string _ip;
        #region Setter/Getter (ip)
        /// <summary>
        /// Getter of ip
        /// Twitter description: Ip of the User
        /// </summary>
        public string GetIp()
        {
            return _ip;
        }
        /// <summary>
        /// Setter of ip
        /// Twitter description: Ip of the User
        /// </summary>
        public void SetIp(string ip)
        {
            // check validity of the ip 

            _ip = ip;
        }
        #endregion



        [SerializeField]
        [Tooltip("Port of the user")]
        private int _port;
        #region Setter/Getter (port)
        /// <summary>
        /// Getter of port
        /// Twitter description: Port of the user
        /// </summary>
        public int GetPort()
        {
            return _port;
        }
        /// <summary>
        /// Setter of port
        /// Twitter description: Port of the user
        /// </summary>
        public void SetPort(int port)
        {
            _port = port;
        }
        #endregion








        [SerializeField]
        [Tooltip("Player Information")]
        private UserInfo _playerAccount;
        #region Setter/Getter (playerAccount)
        /// <summary>
        /// Getter of playerAccount
        /// Twitter description: Player Information
        /// </summary>
        public UserInfo GetPlayerAccountInformation()
        {
            return _playerAccount;
        }
        /// <summary>
        /// Setter of playerAccount
        /// Twitter description: Player Information
        /// </summary>
        public void SetPlayerAccountInformation(UserInfo playerAccount)
        {
            _playerAccount = playerAccount;
        }
        #endregion








    }
}