using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BlackBox.Tools
{

    public class WebAccessor
    {
        public static WebPageUnityLoader LoadPage(string url, bool autoStart = true) {
            return LoadPage(url, null, null, null,autoStart);
        }
        public static WebPageUnityLoader LoadPage(string url, List<ParamsProperty> properties, bool autoStart = true)
        {
            return LoadPage(url, properties, null, null,autoStart);
        }
        public static WebPageUnityLoader LoadPage(string url, List<ParamsProperty> properties, string user, string password, bool autoStart=true)
        {
            GameObject pageLoader = new GameObject("> Page Loader: "+url);
            WebPageUnityLoader unityPageLoad = pageLoader.AddComponent<WebPageUnityLoader>();

            unityPageLoad.SetLink(url);
            if(properties!=null && properties.Count>0)
                unityPageLoad.AddParams(properties);
            if(! string.IsNullOrEmpty(user) || password!=null)
                unityPageLoad.SetUser(user, password);

            if (autoStart)
                unityPageLoad.LoadWithCoroutine();
            return unityPageLoad;
        }

        [System.Serializable]
        public class ParamsProperty
        {
            public string key = "key";
            public string value = "value";
            public enum SendType { GET, POST }
            public SendType type = SendType.POST;
            public ParamsProperty(string key, string value, SendType sendType)
            {
                this.key = key;
                this.value = value;
                this.type = sendType;
            }
        }

    }

    public class WebPageUnityLoader : MonoBehaviour
    {

        public string _url = "http://www.google.com";
        public string Url
        {
            get { return _url; }
            set
            {
                if (!IsUrlValide(value))
                    Debug.LogWarning("The URI is not well formed !!", this);
                _url = value;
            }
        }

        public  static bool IsUrlValide(string value)
        {
            Uri uriResult;
            return Uri.TryCreate(value, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;   
        }

        internal bool IsLoading()
        {
            return _loading;
        }

        public List<WebAccessor.ParamsProperty> _fields = new List<WebAccessor.ParamsProperty>();

        public List<WebAccessor.ParamsProperty> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        internal bool HasErrorOccured()
        {
            return !string.IsNullOrEmpty(error);
        }

        [Header("Password dont work with POST !")]
        [Tooltip("The user name use in the HTTP header to log in")]
        [SerializeField]
        private string _user;

        public void AutoDestruction()
        {
            RemoveOfScene();
        }

        internal void RemoveOfScene()
        {
            Destroy(this.gameObject);
        }

        [Tooltip("The password use in the HTTP header to log in")]
        [SerializeField]
        private string _password;

        [Tooltip("DEBUG: Is the page loading")]
        [SerializeField]
        private bool _loading;
        public bool Loading
        {
            get { return _loading; }
            private set { _loading = value; }
        }

        [Tooltip("The text that has been loaded")]
        [SerializeField]
        private string _textLoaded = "";

        public string TextLoaded
        {
            get { return _textLoaded; }
            private set { _textLoaded = value; }
        }


        [Tooltip("The message error return if the page was not correclty loaded")]
        [SerializeField]
        private string error = "";

        public string Error
        {
            get { return error; }
            private set { error = value; }
        }

        public WWWForm GetWWWForm()
        {
            WWWForm form = new WWWForm();
            int iPost = 0;
            foreach (WebAccessor.ParamsProperty gp in _fields)
                if (gp.type == WebAccessor.ParamsProperty.SendType.POST)
                {
                    iPost++;
                    form.AddField(gp.key, gp.value);
                }
            return iPost > 0 ? form : null;
        }
        public string GetUrl(bool url = true, bool getParams = true)
        {
            int iGet = 0;
            string GET = "";
            foreach (WebAccessor.ParamsProperty gp in _fields)
                if (gp.type == WebAccessor.ParamsProperty.SendType.GET)
                {
                    iGet++;
                    GET += gp.key + "=" + gp.value + "&";
                }
            if (url && getParams)
                return this.Url + (iGet > 0 ? "?" + GET : "");
            else if (url)
                return this.Url;
            else
                return GET;
        }

        public WWW GetWWW(bool withPost, bool withGet)
        {
            string url = GetUrl(true, withGet);
            WWWForm form = GetWWWForm();
            bool isUsingPassword = !string.IsNullOrEmpty(_user) && !string.IsNullOrEmpty(_password);



            #region TO REFACTOR CODE
            ///           Shame Code   
            /// Twitter description:
            /// This code is dirty because I not sure about why I can't see post with a header
            /// What happened: Do not know how to send post with the header
            /// Ashame of my code:     50 % 
            /// What to do (?): Check the doc about www and make test. 
            #endregion
            WWW www = null;
            if (isUsingPassword)
            {
                Dictionary<string, string> headers = GetHeaderWithUserLogIn();
                www = new WWW(url, null, headers);

            }
            else if (withPost && form != null)
            {
                www = new WWW(url, form);
            }
            else
            {

                www = new WWW(url);
            }
            return www;
        }

        private Dictionary<string, string> GetHeaderWithUserLogIn()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string key = "Authorization";
            string val = "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _user, _password)));
            headers.Add(key, val);
            return headers;
        }

        public void LoadWithCoroutine(bool withPost = true, bool withGet = true)
        {
            StartCoroutine(Load(withPost, withGet));
        }

        public IEnumerator Load(bool withPost = true, bool withGet = true)
        {

            WebPageUnityLoader pageLoader = this;
            WWW www = GetWWW(withPost, withGet);
            string url = www.url;
            Loading = true;
            if (onStartPageLoading != null)
                onStartPageLoading(url, ref pageLoader);

            yield return www;

            Loading = false;
            Error = www.error;
            TextLoaded = www.text;

            if (onEndPageLoading != null)
                onEndPageLoading(url, _textLoaded, error,ref pageLoader);

            yield return null;
        }



        public void SetParams(string key, string value)
        {
            foreach (WebAccessor.ParamsProperty gp in _fields)
            {
                if (gp.key.Equals(key))
                    gp.value = value;
            }

        }
        public void SetParams(string key, WebAccessor.ParamsProperty.SendType sendType)
        {
            foreach (WebAccessor.ParamsProperty gp in _fields)
            {
                if (gp.key.Equals(key))
                    gp.type = sendType;
            }

        }
        public void SetParams(string key, string value, WebAccessor.ParamsProperty.SendType sendType)
        {
            foreach (WebAccessor.ParamsProperty gp in _fields)
            {
                if (gp.key.Equals(key))
                {
                    gp.value = value;
                    gp.type = sendType;
                }
            }

        }

        public void AddParams(string key, string value, WebAccessor.ParamsProperty.SendType sendType)
        {
            Fields.Add(new WebAccessor.ParamsProperty(key, value, sendType));
        }
        public void AddParams(WebAccessor.ParamsProperty property)
        {
            if (property == null)
                return;
            Fields.Add(property);
        }
        public void AddParams(List<WebAccessor.ParamsProperty> properties)
        {
            if (properties == null)
                return;
            for (int i = 0; i < properties.Count; i++)
            {
                AddParams(properties[i]);
            }
        }

        public delegate void OnStartPageLoading(string urlSent, ref WebPageUnityLoader info);
        public delegate void OnEndPageLoading(string urlSent, string textReceived, string error, ref WebPageUnityLoader info);

        public OnStartPageLoading onStartPageLoading;
        public OnEndPageLoading onEndPageLoading;


        public void ResetParams()
        {
            _fields.Clear();
        }

        internal void SetLink(string urlLink)
        {
            if (string.IsNullOrEmpty(urlLink))
                throw new System.ArgumentException("The url to load can't be null !");
            if (!IsUrlValide(urlLink))
                throw new System.ArgumentException("The url is not valide !");
            _url = urlLink;
        }

        public void SetUser(string user, string password)
        {
            if (string.IsNullOrEmpty(user))
                throw new System.ArgumentException("If the web accessor has a user, the name can't be null or empty");
            if (password == null) password = "";
            _user = user;
            _password = password;
        }
    }
}