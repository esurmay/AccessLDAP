using AccessLDAP.BL;
using AccessLDAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceLDAP
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SSO : ISSO
    {

        LDAPBusinessLayer bussinesLayer;

        public bool ChangePassword(string userName, string oldPaassword, string newPassword)
        {
            bussinesLayer = new LDAPBusinessLayer();
            AuthenticatedUser User = new AuthenticatedUser();
            User.CreatedDate = DateTime.Now;
            User.UserName = userName;
            User.oldPassword = oldPaassword;
            User.newPassword = newPassword;
            //User.IsActive = isActive;
            return bussinesLayer.ChangePassword(User);
        }

        public bool CreateUser(string userName, string password, string eMail, bool isActive)
        {
            bussinesLayer = new LDAPBusinessLayer();
            UserDTO User = new UserDTO();
            User.CreatedDate = DateTime.Now;
            User.UserName = userName;
            User.Password = password;
            User.Email = eMail;
            User.IsActive = isActive;
            return bussinesLayer.CreateUser(User);
        }


        public bool DeleteUser(string userName)
        {
            bussinesLayer = new LDAPBusinessLayer();
            UserDTO User = new UserDTO();
            User.UserName = userName;
            return bussinesLayer.DeleteUser(User);
        }

        public List<UserDTO> FindUsersByEmail(string EmailAddress)
        {
            bussinesLayer = new LDAPBusinessLayer();
             return bussinesLayer.FindUsersByEmail(EmailAddress).ToList();
        }

        public List<UserDTO> FindUsersByName(string UserName)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.FindUsersByName(UserName);
        }

     

        public int GetOnlineCount()
        {
            throw new NotImplementedException();
        }

        public AuthenticatedUser GetUserByEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserByID(int id)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.GetUserByID(id);
        }

        public AuthenticatedUser GetUserByName(string userName)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetUsers(int index, int count)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.GetUsers(index, count);
        }
        public List<UserDTO> GetAllUsers()
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.GetAllUsers();
        }

        public bool IsAuthenticated(string userName)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.IsAuthenticated(userName);
        }

        public PasswordResetResultType ResetPassword(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool SignIn(string userName, string password)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.SignIn(new AuthenticatedUser() { UserName = userName, Password = password });
        }

        public bool SignOut(string userName)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.SignOut(new AuthenticatedUser() { UserName = userName});
        }

        public bool UpdateUser(string userName, string password, string eMail, bool isActive)
        {
            bussinesLayer = new LDAPBusinessLayer();
            UserDTO User = new UserDTO();
            User.CreatedDate = DateTime.Now;
            User.UserName = userName;
            User.Password = password;
            User.Email = eMail;
            User.IsActive = isActive;
            return bussinesLayer.UpdateUser(User);
        }

        public bool UserExists(string userName)
        {
            bussinesLayer = new LDAPBusinessLayer();
            return bussinesLayer.UserExists(userName);
        }
    }
}
