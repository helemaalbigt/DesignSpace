using UnityEngine;
using System.Collections;
using System;


///     \,,/(◣_◢)\,,/       
/// Proud:     65 % 
/// Clean:     40 %
/// Documented:10 %
/// Reusable:  10 %
/// Readable:  20 %
/// Improve list: 
/// - finish refactor,
/// - documentation,
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
    public class Loading
    {
        [SerializeField]
        protected bool _isLoading;
        public Loading() { 
        
        }

        public delegate void LoadingChangeState(Loading loading, bool isLoading);
        public LoadingChangeState onLoadingChangeState;
        public bool IsLoading() { return _isLoading; }
        public void SetLoadingState(bool isLoading)
        {
            if (isLoading == _isLoading) return;
            _isLoading = isLoading;
            if (onLoadingChangeState != null)
                onLoadingChangeState(this, _isLoading);
        }
    }

    /// <summary>
    /// Loading info is a boolean loading state with pourcent and a text as information associated
    /// </summary>
    /// 
    [System.Serializable]
    public class LoadingWithInfo : Loading
    {


        [SerializeField]
        private float _pourcentLoading;
        [SerializeField]
        private string _informativeMessage;



        #region Delegates
        public delegate void LoadingInfoChange(LoadingWithInfo loading, bool isLoading, string informativeMessage, float pourcentState);
        public LoadingInfoChange onLoadingNewInformation;


        public void SetLoadingState(bool isLoading, bool changePourcentState = true, bool changeInformativeMessage = true)
        {

            if (changePourcentState)
            {
                SetPourcentLoaded(isLoading ? 0f : 1f);
            }
            if (changeInformativeMessage)
            {
                SetInformativeMessage(isLoading ? "Loading" : "Loaded");
            }
            base.SetLoadingState(isLoading);
        }

        private void NotifyInformationChange()
        {
            if (onLoadingNewInformation != null)
                onLoadingNewInformation(this, _isLoading, _informativeMessage, _pourcentLoading);
        }
        #endregion


        #region Getter and Setter



        public void SetInformativeMessage(string stateOfLoading)
        {
            _informativeMessage = stateOfLoading;
            NotifyInformationChange();
        }
        public string GetInformationOnState() { return _informativeMessage; }

        public void SetPourcentLoaded(float pourcent)
        {
            _pourcentLoading = pourcent;
            NotifyInformationChange();
        }
        public float GetPourcentLoaded() { return _pourcentLoading; }
        #endregion


    }
}
