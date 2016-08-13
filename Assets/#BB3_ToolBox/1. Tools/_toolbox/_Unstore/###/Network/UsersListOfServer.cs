using UnityEngine;
using System.Collections.Generic;
using System;
using BlackBox.Beans.Basic;
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
	public class UsersListOfServer : MonoBehaviour {

        public delegate void NewUserDetected(UserIpLink newUser);
        public NewUserDetected onNewUnknowUserDetected;

        internal void Flush()
        {
            _users.Clear();
            _usersInOrder.Clear();
        }

        public NewUserDetected onUserIdentify;

        public UserIpLink [] GetUsers()
        {
            UserIpLink[] users = new UserIpLink[_users.Values.Count];
            _users.Values.CopyTo(users,0);
            return users;
        }

        private List<UserIpLink> _usersInOrder = new List<UserIpLink>();
        public int GetPlayerType(IpIdRef ipIdRef)
        {
            for (int i = 0; i < _usersInOrder.Count; i++)
            {
                if (_usersInOrder[i].GetIp().Equals(ipIdRef.GetIpLink().GetIp()))
                    return i;
            }
            return -1;
        }

        public UDP_Server _server;
        public Dictionary<string,UserIpLink> _users = new Dictionary<string,UserIpLink>();
        [Tooltip("The prefix of the command that pocess player info")]
        public string _playerPrefix= "PlayerAccount";

        [Tooltip("Launch the research of some new player at the script start")]
        public bool _lookForPlayerAtStart=true;
        public float _stopLookingAfter = 15;
        public void Awake() {
            ActivateResearchOfPlayer();

        }
        void OnLevelWasLoaded(int level)
        {
            ActivateResearchOfPlayer();

        }
        public void ActivateResearchOfPlayer() {

            SetResearchForNewPlayerAs(_lookForPlayerAtStart);
            //Invoke("StopLooking", _stopLookingAfter);
            
        }

        private void CheckForNewPlayer(UDP_Receiver from, string message, string adresse, int port)
        {
            if (!_users.ContainsKey(adresse)) {
                AddNewUser(adresse, port);
            }
            if (message.StartsWith(_playerPrefix)) {
                SetUserInfo(adresse, message);
            }
        }

        private void SetUserInfo(string adresse, string message)
        {
           
            UserInfo info = new UserInfo();


            ////*      {(>_<)}    */    
            /// Twitter description:
            /// This code is dirty because I lead the beans to the conversion string so can use the conversion independantly of the beans
            /// What happened: I am stupid -__- !
            /// Ashame of my code:     70 % 
            /// What to do (?): Extract the converstion type and linked to a special conversion class. 
            //info.SetWithGlobalStringCommand(message);
            UserIpLink linkedIp = _users[adresse];
            linkedIp.SetPlayerAccountInformation(info);
             Debug.Log("Add " + adresse + ":  " + info.GetEmail());
            if (onUserIdentify != null)
                onUserIdentify(linkedIp);

        }

        private void AddNewUser(string adresse, int port)
        {
            if (!_users.ContainsKey(adresse))
            {
                UserIpLink ipLink = new UserIpLink();
                ipLink.SetIp(adresse);
                ipLink.SetPort(port);
                _users.Add(adresse, ipLink);
                _usersInOrder.Add(ipLink);
                //Debug.Log(adresse + " : "+port);
                if (onNewUnknowUserDetected != null)
                    onNewUnknowUserDetected(ipLink);
            }

        }

        public void SetResearchForNewPlayerAs(bool on) {
            if(on)
                _server.receiver.onPackageReceived += CheckForNewPlayer;
            else
                _server.receiver.onPackageReceived -= CheckForNewPlayer;
        }
    }
}