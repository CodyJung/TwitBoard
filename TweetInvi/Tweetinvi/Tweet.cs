using System;
using System.Globalization;
using System.Collections.Generic;
using Tweetinvi.Utils;
using oAuthConnection;
using Tweetinvi.TwittertEntities;
using System.Collections.ObjectModel;

namespace Tweetinvi
{
    /// <summary>
    /// Class representing a Tweet
    /// https://dev.twitter.com/docs/api/1/get/statuses/show/%3Aid
    /// </summary>
    [Serializable]
    public class Tweet : TwitterObject, ICloneable
    {
        #region Private Attributes

        #region Twitter API Attributes

        private DateTime _created_at;
        private long? _id;
        private string _id_str;
        private string _text;
        private string _source;
        private bool? _truncated;
        private long? _in_reply_to_status_id;
        private string _in_reply_to_status_id_str;
        private long? _in_reply_to_user_id;
        private string _in_reply_to_user_id_str;
        private string _in_reply_to_screen_name;
        private User _user;
        private Geo _geo;

        // Implement Coordinates
        private Coordinates _coordinates;

        // Implement Place
        private string _place;

        // Implement Contributors
        private int[] _contributors;

        private int? _retweet_count;
        private TweetEntities _entities;
        private bool? _favorited;
        private bool? _retweeted;
        private bool? _possibly_sensitive;

        #endregion

        #region Tweet API Attributes

        private Token _token;

        #endregion

        #endregion

        #region Public Attributes

        #region Twitter API Attributes

        public DateTime created_at
        {
            get { return _created_at; }
            set
            {
                if (_created_at != value)
                {
                    _created_at = value;
                    OnPropertyChanged("created_at");
                }
            }
        }

        public long? id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string id_str
        {
            get { return _id_str; }
            set { _id_str = value; }
        }

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string source
        {
            get { return _source; }
            set { _source = value; }
        }

        public bool? truncated
        {
            get { return _truncated; }
            set { _truncated = value; }
        }

        public long? in_reply_to_status_id
        {
            get { return _in_reply_to_status_id; }
            set { _in_reply_to_status_id = value; }
        }

        public string in_reply_to_status_id_str
        {
            get { return _in_reply_to_status_id_str; }
            set { _in_reply_to_status_id_str = value; }
        }

        public long? in_reply_to_user_id
        {
            get { return _in_reply_to_user_id; }
            set { _in_reply_to_user_id = value; }
        }

        public string in_reply_to_user_id_str
        {
            get { return _in_reply_to_user_id_str; }
            set { _in_reply_to_user_id_str = value; }
        }

        public string in_reply_to_screen_name
        {
            get { return _in_reply_to_screen_name; }
            set { _in_reply_to_screen_name = value; }
        }

        public User user
        {
            get { return _user; }
            set { _user = value; }
        }

        public Geo geo
        {
            get { return _geo; }
            set { _geo = value; }
        }

        public Coordinates coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        public int[] contributors
        {
            get { return _contributors; }
            set { _contributors = value; }
        }

        public int? retweet_count
        {
            get { return _retweet_count; }
            set { _retweet_count = value; }
        }

        public TweetEntities entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public bool? favorited
        {
            get { return _favorited; }
            set { _favorited = value; }
        }

        public bool? retweeted
        {
            get { return _retweeted; }
            set { _retweeted = value; }
        }

        public bool? possibly_sensitive
        {
            get { return _possibly_sensitive; }
            set { _possibly_sensitive = value; }
        }

        //public DateTime tweet_creation_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        //public long tweet_id { get; set; }
        //public string tweet_date { get; set; }
        //public string tweet_user { get; set; }
        //public string tweet_user_login { get; set; }
        //public string tweet_text { get; set; }
        //public string tweet_place { get; set; }
        //public string tweet_language { get; set; }
        #endregion

        #region Twitter API Accessors

        public ObservableCollection<HashTagEntity> Hashtags
        {
            get
            {
                if (entities != null)
                {
                    return entities.hashtags;
                }

                return null;
            }

            set
            {
                if (entities != null)
                {
                    entities.hashtags = value;
                }
            }
        }

        public ObservableCollection<UrlEntity> Urls
        {
            get
            {
                if (entities != null)
                {
                    return entities.urls;
                }

                return null;
            }

            set
            {
                if (entities != null)
                {
                    entities.urls = value;
                }
            }
        }

        public ObservableCollection<MediaEntity> Media
        {
            get
            {
                if (entities != null)
                {
                    return entities.media;
                }

                return null;
            }

            set
            {
                if (entities != null)
                {
                    entities.media = value;
                }
            }
        }

        public ObservableCollection<UserMentionEntity> User_mentions
        {
            get
            {
                if (entities != null)
                {
                    return entities.user_mentions;
                }

                return null;
            }

            set
            {
                if (entities != null)
                {
                    entities.user_mentions = value;
                }
            }
        }

        #endregion

        #region Tweet API Attributes

        public Token token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnPropertyChanged("Token");
            }
        }

        public DateTime tweet_creation_date = DateTime.Now;

        #endregion

        #endregion

        #region Tweet API Queries

        public string query_tweet_from_id = "https://api.twitter.com/1/statuses/show/{0}.json";
        public string query_tweet_from_id_with_entities = "https://api.twitter.com/1/statuses/show/{0}.json?include_entities=true";

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        private Tweet()
        {
            // Default constructor that should be called by all constructors
            // Basic initilization
        }

        #region Create Tweet from Id

        public Tweet(long? id, Token token = null, bool cleanString = true)
        {
            if (id == null)
                throw new Exception("id cannot be null!");
            _id = id;
            _id_str = id.ToString();

            if (token != null)
            {
                this.token = token;
                this.FillTweet(token, cleanString);
            }
        }

        public Tweet(long id, Token token = null, bool cleanString = true)
            : this((long?)id, token, cleanString) { }

        #endregion

        #region Create Tweet from dynamic response
        private Tweet(dynamic dynamicTweet)
            : this()
        {
            if (dynamicTweet is Dictionary<String, object>)
            {
                Dictionary<String, object> dTweet = dynamicTweet as Dictionary<String, object>;
                if (dTweet.GetProp("id") != null)
                {
                    fillTweet(dTweet);
                }
                else
                    throw new InvalidOperationException("Cannot create 'Tweet' if id does not exist");
            }
        }

        public static Tweet Create(object tweet_obj)
        {
            if (tweet_obj == null)
                return null;

            if (tweet_obj is String)
            {
                string jsonText = tweet_obj as String;
                dynamic dynamicTweet = Serializer.jss.Deserialize<dynamic>(jsonText);

                if (dynamicTweet == null || !(dynamicTweet is Dictionary<String, object>))
                    return null;

                try
                {
                    return new Tweet(dynamicTweet);
                }
                catch (InvalidOperationException) { return null; }
            }

            return null;
        } 
        #endregion

        #endregion

        #region Public Methods

        #region Fill Tweet

        public void FillTweet()
        {
            FillTweet(this.token);
        }

        public void FillTweet(Token token, bool cleanString = true)
        {
            if (token != null)
            {
                var query = "";
                if (_id != null)
                    query = String.Format(this.query_tweet_from_id_with_entities, id);
                else
                    return;

                // If 404 error throw Exception that Tweet has not been created
                var jsonResponse = token.ExecuteQuery(query);
                dynamic dynamicTweet = jsonResponse;
                if (dynamicTweet is Dictionary<String, object>)
                {
                    Dictionary<String, object> dTweet = dynamicTweet as Dictionary<String, object>;
                    fillTweet(dTweet, cleanString);
                }
            }
        }

        #endregion

        #region Override
        public override string ToString()
        {
            return String.Format("'{0}', {1}, '{2}', '{3}', '{4}', '{5}'",
                id, created_at, user.name, text, user.lang, tweet_creation_date);
        } 
        #endregion

        #endregion

        #region Private Methods

        private void fillTweet(Dictionary<String, object> dTweet, bool cleanString = true)
        {
            if (dTweet.GetProp("id") != null)
            {
                created_at = DateTime.ParseExact(dTweet.GetProp("created_at") as string,
                    "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
                id = dTweet.GetProp("id") as long?;
                id_str = dTweet.GetProp("id_str") as string;
                text = dTweet.GetProp("text") as string;
                source = dTweet.GetProp("source ") as string;
                truncated = dTweet.GetProp("truncated") as bool?;
                in_reply_to_status_id = dTweet.GetProp("in_reply_to_status_id") as int?;
                in_reply_to_status_id_str = dTweet.GetProp("in_reply_to_status_id_str") as string;
                in_reply_to_user_id = dTweet.GetProp("in_reply_to_user_id") as int?;
                in_reply_to_user_id_str = dTweet.GetProp("in_reply_to_user_id_str") as string;
                in_reply_to_screen_name = dTweet.GetProp("in_reply_to_screen_name") as string;
                user = User.Create(dTweet.GetProp("user") as object);
                // Create Geo
                var geo = dTweet.GetProp("geo");
                // Create Coordinates
                var coordinates = dTweet.GetProp("coordinates");
                // Create Place
                var place = dTweet.GetProp("place");
                // Create Contributors
                var contributors = dTweet.GetProp("contributors");
                retweet_count = dTweet.GetProp("retweet_count") as int?;
                entities = new TweetEntities(dTweet["entities"] as Dictionary<String, object>);
                favorited = dTweet.GetProp("favorited") as bool?;
                retweeted = dTweet.GetProp("retweeted") as bool?;
                possibly_sensitive = dTweet.GetProp("possibly_sensitive") as bool?;

                if (cleanString == true)
                    text = text.cleanString();
            }
        }

        #endregion

        #region ICloneable
        public object Clone()
        {
            Tweet clone = new Tweet();
            clone._in_reply_to_user_id_str = _in_reply_to_user_id_str;
            clone._text = _text;
            clone._id_str = _id_str;
            clone._favorited = favorited;
            clone._source = _source;
            clone._created_at = _created_at;
            clone._user = _user;
            clone._retweet_count = _retweet_count;
            clone._retweeted = _retweeted;

            return clone;
        }
        #endregion
    }
}
