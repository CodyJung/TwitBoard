using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tweetinvi.Utils
{
    public class Coordinates
    {
        #region Private Attributes
        private double _lattitude;
        private double _longitude; 
        #endregion

        #region Public Attributes
        public double lattitude
        {
            get { return _lattitude; }
            set { _lattitude = value; }
        }

        public double longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        } 
        #endregion
    }
}
