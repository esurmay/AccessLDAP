using AccessLDAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLDAP.DAL
{
    public interface IUsersRepository: IGenericDataRepository<User>
    {
        bool SignIn(User user);
        void SignOut(User user);

        void DeleteUser(User User);

        bool UserExists(User user);
        bool UserExists(string userName);

        PasswordResetResultType ResetPassword(string emailAddress);

        User GetUserByEmail(string emailAddress);

        bool IsAuthenticated(string userName);

        int GetOnlineUsersCount();

        bool UpdateUser(User user);

    }
}
