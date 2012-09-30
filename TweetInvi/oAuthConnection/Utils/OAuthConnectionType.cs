using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace oAuthConnection.Utils
{
    [DataContract]
    public enum OAuthConnectionType
    {
        [EnumMember]
        Authorization_Headers,
        [EnumMember]
        Base_HTML_String
    }
}
