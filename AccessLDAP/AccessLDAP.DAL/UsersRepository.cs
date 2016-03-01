using AccessLDAP.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AccessLDAP.DAL
{
    public class UsersRepository : GenericDataRepository<User>, IUsersRepository
    {
        public bool UpdateUser(User user)
        {
            bool result = false;
            using (SSOEntities c = new SSOEntities())
            {
                User SOuser = c.Users.FirstOrDefault(x => x.UserName.ToLower() == user.UserName.ToLower());
                if (SOuser != null)
                {
                    try
                    {

                        SOuser.Password = Security.Encrypt(user.Password);
                        SOuser.Email = user.Email;                        
                        SOuser.UserName = user.UserName;
                        //SOuser.IsActive = true;
                        c.SaveChanges();
                        result = true;

                    }
                    catch (Exception ex)
                    {
                        throw new FaultException<Exception>(ex);
                    }
                }
                else
                    throw new FaultException<SSOUserDoesNotExistException>(
                        new SSOUserDoesNotExistException("The given user name does not exists."));
            }
            return result;
        }
        public bool SignIn(User user)
        {
            bool result = false;
            using (SSOEntities c = new SSOEntities())
            {
                User SOuser = c.Users.FirstOrDefault(x => x.UserName.ToLower() == user.UserName.ToLower());
                if (SOuser != null)
                {
                    try
                    {
                        if (Security.Decrypt(SOuser.Password) == user.Password)
                        {

                            SignOut(user);
                            Ticket ticket = new Ticket() { UserID = SOuser.Id, LoggedIn = DateTime.Now, TokenID = Guid.NewGuid() };
                            SOuser.Tickets.Add(ticket);
                            //SOuser.IsActive = true;
                            c.SaveChanges();
                            result = true;
                        }
                        else
                            throw new FaultException<SSOInvalidPasswordException>(
                                new SSOInvalidPasswordException("The username and password does not match"));
                    }
                    catch (Exception ex)
                    {
                        throw new FaultException<Exception>(ex);
                    }
                }
                else
                    throw new FaultException<SSOUserDoesNotExistException>(
                        new SSOUserDoesNotExistException("The given user name does not exists."));
            }
            return result;
        }

        public void SignOut(User user)
        {
            using (SSOEntities c = new SSOEntities())
            {
                User SOuser = c.Users.FirstOrDefault(x => x.UserName.ToLower() == user.UserName.ToLower());
                if (SOuser != null)
                {
                    if (SOuser.Tickets.Any())
                    {
                        foreach (Ticket t in SOuser.Tickets.ToList())
                            c.Tickets.Remove(t);
                        c.SaveChanges();
                    }
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (SSOEntities c = new SSOEntities())
            {
                var instance = c.Users.FirstOrDefault(x => x.UserName.ToLower() == user.UserName.ToLower());
                if (instance != null)
                {
                    c.Users.Remove(instance);
                    c.SaveChanges();
                }
                else
                    throw new FaultException<SSOUserDoesNotExistException>(
                        new SSOUserDoesNotExistException(string.Format("User Name '{0}' does not exist", user.UserName)));
            }
        }

        public bool UserExists(string userName)
        {
            User user = new User();
            user.UserName = userName;
            return UserExists(user);
        }

        public bool UserExists(User user)
        {
            using (SSOEntities c = new SSOEntities())
            {
                return c.Users.Any(x => x.UserName.ToLower() == user.UserName.ToLower());
            }
        }

        public PasswordResetResultType ResetPassword(string emailAddress)
        {
            PasswordResetResultType result = PasswordResetResultType.Successful;
            try
            {
                using (SSOEntities c = new SSOEntities())
                {
                    User userObject = GetUserByEmail(emailAddress);
                    if (userObject != null)
                    {
                        string newPassword = SSOHelper.GeneratePlainPassword();
                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage msg = new MailMessage() { Subject = "New Password", Body = newPassword };
                        msg.To.Add(emailAddress);
                        smtpClient.SendAsync(msg, userObject.Id);
                        userObject.Password = SSOHelper.HashPassword(newPassword);
                        c.SaveChanges();
                    }
                    else
                        result = PasswordResetResultType.EmailNotFound;
                }
            }
            catch
            {
                result = PasswordResetResultType.Error;
            }
            return result;
        }

        public User GetUserByEmail(string emailAddress)
        {
            return GetSingle(d => d.Email.ToLower().Equals(emailAddress));
        }

        public bool IsAuthenticated(string userName)
        {

            int timeOutMins = int.Parse(ConfigurationManager.AppSettings["SessionTimeOutMinutes"]);
            bool result = false;
            using (SSOEntities c = new SSOEntities())
            {
                User user = c.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                if (user != null)
                {
                    Ticket tc = user.Tickets.FirstOrDefault();
                    if (tc != null)
                    {
                        result = true;
                        //if (tc.LoggedIn.AddMinutes(timeOutMins) <= DateTime.Now)
                        //    result = true;
                    }
                }
                else
                    throw new FaultException<SSOUserDoesNotExistException>(new SSOUserDoesNotExistException("The given user name does not exists."));
            }
            return result;
        }

        public int GetOnlineUsersCount()
        {
            int result = 0;
            using (SSOEntities c = new SSOEntities())
            {
                result = c.Tickets.Count();
            }
            return result;
        }
    }
}
