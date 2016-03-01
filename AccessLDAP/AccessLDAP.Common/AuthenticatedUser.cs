using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AccessLDAP.Common
{
    [DataContract]
    public class AuthenticatedUser 
    {

        
        [DataMember]
        public long Id
        {
            get;
            set;
        }

        [DataMember]
        public string UserName {get;set;}

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime LastLoginDate { get; set; }

        [DataMember]
        public DateTime LastPasswordChangeDate { get; set; }

        [DataMember]
        public DateTime LastLockOutDate { get; set; }

        [DataMember]
        public string oldPassword { get; set; }

        [DataMember]
        public string newPassword { get; set; }

    }
}
