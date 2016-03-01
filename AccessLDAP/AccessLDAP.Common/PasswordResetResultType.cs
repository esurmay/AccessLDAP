using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace AccessLDAP.Common
{
    [DataContract]
    public enum PasswordResetResultType
    {
        [EnumMember]
        EmailNotFound =1,

        [EnumMember]
        Successful =2,

        [EnumMember]
        Error = 3
    }
}