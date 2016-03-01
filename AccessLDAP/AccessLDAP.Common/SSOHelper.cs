using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccessLDAP.Common
{
    public class SSOHelper
    {
        public static string HashPassword(string password)
        {
            return password;
        }

        public static string GeneratePlainPassword()
        {
            throw new NotImplementedException();
        }
    }
}