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
	public class AutoLoadScene : MonoBehaviour {
        public LoadScene _sceneLoader;

        public float _timeToLoad;

        public void Start() {
            Invoke("LoadNext", _timeToLoad);
        }

        void LoadNext() {
            if(_sceneLoader!=null)
            _sceneLoader.LoadSelectedSceneWithDelay();
        }
	}
}