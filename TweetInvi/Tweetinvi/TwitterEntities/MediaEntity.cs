using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Tweetinvi.Utils;

namespace Tweetinvi.TwittertEntities
{
    public class MediaEntitySize
    {
        #region Private Attributes

        private int? _w;
        private string _resize;
        private int? _h;

        #endregion

        #region Public Attributes
        public int? w
        {
            get { return _w; }
            set { _w = value; }
        }

        public string resize
        {
            get { return _resize; }
            set { _resize = value; }
        }

        public int? h
        {
            get { return _h; }
            set { _h = value; }
        }
        #endregion

        public MediaEntitySize() { }

        public MediaEntitySize(Dictionary<String, object> media_entity_size)
        {
            w = media_entity_size.GetProp("w") as int?;
            resize = media_entity_size.GetProp("resize") as string;
            h = media_entity_size.GetProp("h") as int?;
        }
    }

    public class MediaEntity
    {
        #region Private Attributes

        private long? _id;
        private string _id_str;
        private string _media_url;
        private string _media_url_https;
        private UrlEntity _url;
        private string _type;
        private Dictionary<String, MediaEntitySize> _sizes;

        #endregion

        #region Public Attributes
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

        public string media_url
        {
            get { return _media_url; }
            set { _media_url = value; }
        }

        public string media_url_https
        {
            get { return _media_url_https; }
            set { _media_url_https = value; }
        }

        public UrlEntity url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Dictionary<String, MediaEntitySize> sizes
        {
            get { return _sizes; }
            set { _sizes = value; }
        }
        #endregion

        #region Constructors
        public MediaEntity() { }

        public MediaEntity(Dictionary<String, object> media_entity)
        {
            id = media_entity.GetProp("id") as long?;
            id_str = media_entity.GetProp("id_str") as string;
            media_url = media_entity.GetProp("media_url") as string;
            media_url_https = media_entity.GetProp("media_url_https") as string;

            ObservableCollection<int> indices = new ObservableCollection<int>();

            foreach (int indice in media_entity.GetProp("indices") as object[])
            {
                indices.Add(indice);
            }


            url = new UrlEntity()
            {
                url = media_entity.GetProp("url") as string,
                display_url = media_entity.GetProp("display_url") as string,
                expanded_url = media_entity.GetProp("expanded_url") as string,
                indices = indices
            };

            type = media_entity.GetProp("type") as string;

            sizes = new Dictionary<string, MediaEntitySize>();
            Dictionary<String, object> tmp_sizes = media_entity.GetProp("sizes") as Dictionary<String, object>;

            foreach (object size in tmp_sizes)
            {
                KeyValuePair<String, object> pair = (KeyValuePair<String, object>)size;
                sizes.Add(pair.Key, new MediaEntitySize(pair.Value as Dictionary<String, object>));
            }
        }
        #endregion
    }
}
