using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Tweetinvi.Utils;

namespace Tweetinvi.TwittertEntities
{
    /// <summary>
    /// Class representing TweetEntities
    /// https://dev.twitter.com/docs/tweet-entities
    /// </summary>
    public class TweetEntities : TwitterObject
    {
        #region Private Attributes
        private ObservableCollection<MediaEntity> _media;
        private ObservableCollection<UrlEntity> _urls;
        private ObservableCollection<UserMentionEntity> _user_mentions;
        private ObservableCollection<HashTagEntity> _hashtags;
        #endregion

        #region Public Attributes
        public ObservableCollection<UrlEntity> urls
        {
            get { return _urls; }
            set
            {
                _urls = value;
                OnPropertyChanged("urls");
            }
        }

        public ObservableCollection<MediaEntity> media
        {
            get { return _media; }
            set
            {
                _media = value;
                OnPropertyChanged("media");
            }
        }

        public ObservableCollection<UserMentionEntity> user_mentions
        {
            get { return _user_mentions; }
            set { _user_mentions = value; }
        }

        public ObservableCollection<HashTagEntity> hashtags
        {
            get { return _hashtags; }
            set { _hashtags = value; }
        }
        #endregion

        #region Constructors
        public TweetEntities() { }

        public TweetEntities(Dictionary<String, object> entities)
        {
            urls = entities.GetProp("urls") != null ?
                (from x in (entities.GetProp("urls") as object[])
                 select new UrlEntity(x as Dictionary<String, object>)).ToObservableCollection() : null;

            media = entities.GetProp("media") != null ?
                (from x in (entities.GetProp("media") as object[])
                 select new MediaEntity(x as Dictionary<String, object>)).ToObservableCollection() : null;

            user_mentions = entities.GetProp("user_mentions") != null ?
                (from x in (entities.GetProp("user_mentions") as object[])
                 select new UserMentionEntity(x as Dictionary<String, object>)).ToObservableCollection() : null;

            hashtags = entities.GetProp("hashtags") != null ?
                (from x in (entities.GetProp("hashtags") as object[])
                 select new HashTagEntity(x as Dictionary<String, object>)).ToObservableCollection() : null;
        } 
        #endregion
    }
}
