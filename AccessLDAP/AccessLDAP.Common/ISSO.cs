
using System.Collections.Generic;
using System;
using System.Collections;
namespace AccessLDAP.Common
{
    
    public interface ISSO
    {
        bool CreateUser(UserDTO User);
        bool DeleteUser(AuthenticatedUser User);

        bool UserExists(string userName);
        void UpdateUser(AuthenticatedUser User);
        bool UpdateUser(UserDTO User);
        bool ChangePassword(AuthenticatedUser User);
        PasswordResetResultType ResetPassword(string emailAddress);
        string RetrievePassword(string userName);

        IList<UserDTO> FindUsersByEmail(string EmailAddress);
        //IList<AuthenticatedUser> FindUsersByName(string UserName);
        List<UserDTO> FindUsersByName(string UserName);
        List<UserDTO> GetUsers(int index, int count);
        List<UserDTO> GetAllUsers();
        int GetOnlineUsersCount();

        UserDTO GetUserByName(UserDTO User);
        AuthenticatedUser GetUserByEmail(string emailAddress);
        UserDTO GetUserByID(int id);


        bool SignIn(AuthenticatedUser user);
        bool SignOut(AuthenticatedUser user);
        bool IsAuthenticated(string userName);
    }
}
