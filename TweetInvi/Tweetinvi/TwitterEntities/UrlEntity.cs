using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Tweetinvi.Utils;

namespace Tweetinvi.TwittertEntities
{
    public class UrlEntity
    {
        #region Private Attributes

        private string _url;
        private string _display_url;
        private string _expanded_url;
        private ObservableCollection<int> _indices;

        #endregion

        #region Public Attributes

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string display_url
        {
            get { return _display_url; }
            set { _display_url = value; }
        }

        public string expanded_url
        {
            get { return _expanded_url; }
            set { _expanded_url = value; }
        }

        public ObservableCollection<int> indices
        {
            get { return _indices; }
            set { _indices = value; }
        }
        #endregion

        #region Constructors
        public UrlEntity() { }

        public UrlEntity(Dictionary<String, object> url_entity)
        {
            url = url_entity.GetProp("url") as string;
            display_url = url_entity.GetProp("display_url") as string;
            expanded_url = url_entity.GetProp("expanded_url") as string;
            indices = new ObservableCollection<int>();

            foreach (int indice in url_entity.GetProp("indices") as object[])
            {
                indices.Add(indice);
            }
        } 
        #endregion
    }
}
