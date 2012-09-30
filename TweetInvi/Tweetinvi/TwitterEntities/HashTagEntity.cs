using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Tweetinvi.Utils;

namespace Tweetinvi.TwittertEntities
{
    public class HashTagEntity
    {
        #region Private Attributes

        private string _text;
        private ObservableCollection<int> _indices;

        #endregion

        #region Public Attributes

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public ObservableCollection<int> indices
        {
            get { return _indices; }
            set { _indices = value; }
        }

        #endregion

        #region Constructors

        public HashTagEntity() { }

        public HashTagEntity(Dictionary<String, object> hash_tag_entity)
        {
            text = hash_tag_entity.GetProp("text") as string;
            indices = new ObservableCollection<int>();

            foreach (int indice in hash_tag_entity.GetProp("indices") as object[])
            {
                indices.Add(indice);
            }
        }

        #endregion
    }
}
