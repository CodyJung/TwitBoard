using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tweetinvi.Utils;

namespace Tweetinvi
{
    public class Geo
    {
        #region Private Attributes

        private string _type;
        private Coordinates _coordinates;

        #endregion

        #region Public Attributes

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Coordinates coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        #endregion
    }
}
