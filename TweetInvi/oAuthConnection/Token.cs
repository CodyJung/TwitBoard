using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace oAuthConnection
{
    /// <summary>
    /// Token class is divided in 2 files
    /// Token.cs contains the basic information of a Token
    /// TokenBuilder.cs contains the function that allows you to create a webRequest
    /// </summary>
    [DataContract]
    public partial class Token : INotifyPropertyChanged
    {
        #region Private Attributes
        /// <summary>
        /// These 4 information are the basic information required for Twitter authentication
        /// </summary>

        private string _accessToken;
        private string _accessTokenSecret;
        private string _consumerKey;
        private string _consumerSecret;
        private DateTime _resetTime;

        #endregion

        #region Public Attributes

        [DataMember]
        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                if (_accessToken != value)
                {
                    _accessToken = value;
                    OnPropertyChanged("AccessToken");
                }
            }
        }

        [DataMember]
        public string AccessTokenSecret
        {
            get { return _accessTokenSecret; }
            set
            {
                if (_accessTokenSecret != value)
                {
                    _accessTokenSecret = value;
                    OnPropertyChanged("AccessTokenSecret");
                }
            }
        }

        [DataMember]
        public string ConsumerKey
        {
            get { return _consumerKey; }
            set
            {
                if (_consumerKey != value)
                {
                    _consumerKey = value;
                    OnPropertyChanged("ConsumerKey");
                }
            }
        }

        [DataMember]
        public string ConsumerSecret
        {
            get { return _consumerSecret; }
            set
            {
                if (_consumerSecret != value)
                {
                    _consumerSecret = value;
                    OnPropertyChanged("ConsumerSecret");
                }
            }
        }

        [DataMember]
        public DateTime ResetTime
        {
            get { return _resetTime; }
            set { _resetTime = value; }
        }

        #endregion

        #region Constructor/Destructor

        /// <summary>
        /// Create a basic Token
        /// </summary>
        public Token()
        {
            _accessToken = "";
            _accessTokenSecret = "";
            _consumerKey = "";
            _consumerSecret = "";
        }

        /// <summary>
        /// Create a Token with pre-filled attributes
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenSecret"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        public Token(string accessToken, string accessTokenSecret, string consumerKey, string consumerSecret)
            : this()
        {
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
        }

        #endregion

        #region INotifyPropertyChanged
        /// <summary>
        /// INotify Property Changed has been implemented to allow Propery binding
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
