using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace oAuthConnection.Utils
{
    [DataContract]
    public enum HttpMethod
    {
        [EnumMember]
        GET,
        [EnumMember]
        POST,
        [EnumMember]
        DELETE
    }
}
