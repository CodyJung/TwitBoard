using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Tweetinvi.Utils;

namespace Tweetinvi.TwittertEntities
{
    public class UserMentionEntity
    {
        #region Private Attributes
        
        private long? _id;
        private string _id_str;
        private string _screen_name;
        private string _name;
        private ObservableCollection<int> _indices;

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

        public string screen_name
        {
            get { return _screen_name; }
            set { _screen_name = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ObservableCollection<int> indices
        {
            get { return _indices; }
            set { _indices = value; }
        } 

        #endregion

        #region Constructors
        public UserMentionEntity() { }

        public UserMentionEntity(Dictionary<String, object> user_mention_entity)
        {
            id = user_mention_entity.GetProp("id") as long?;
            id_str = user_mention_entity.GetProp("id_str") as string;
            screen_name = user_mention_entity.GetProp("screen_name") as string;
            name = user_mention_entity.GetProp("name") as string;

            indices = new ObservableCollection<int>();
            foreach (int indice in user_mention_entity.GetProp("indices") as object[])
            {
                indices.Add(indice);
            }
        } 
        #endregion
    }
}
