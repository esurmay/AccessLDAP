using AccessLDAP.Common;
using AccessLDAP.DAL;
using AutoMapper;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLDAP.BL
{

    public class LDAPBusinessLayer : ISSO
    {
        private readonly ITicketsRepository ticketsRepository;
        private readonly IUsersRepository usersRepository;

        public LDAPBusinessLayer()
        {
            ticketsRepository = new TIcketsRepository();
            usersRepository = new UsersRepository();
            AutoMapperConfiguration.Configure();
        }


        #region NO TOCAR
        public bool CreateUser(UserDTO user)
        {
            try
            {
                if (!UserExists(user.UserName))
                {
                    Mapper.CreateMap<UserDTO, User>();
                    var _user = Mapper.Map<User>(user);
                    _user.Password = Security.Encrypt(user.Password);
                    usersRepository.Add(_user);
                    return true;
                }
                //return "username already exists. try with another one";
                return false;

            }
            catch (Exception ex)
            {

                return false;
                // return "Error: " + ex.Message;
            }
        }

        public bool ChangePassword(AuthenticatedUser User)
        {

            bool result = false;
            var user = new UserDTO() { UserName = User.UserName };
            UserDTO Updateuser = GetUserByName(user);
            if (Security.Decrypt(Updateuser.Password) == User.oldPassword)
            {
                Updateuser.Password = Security.Encrypt(User.newPassword);
                UpdateUser(Updateuser);
                result = true;
            }
            return result;

        }


        public bool DeleteUser(AuthenticatedUser User)
        {
            try
            {
                var user = AutoMapperConfiguration.configurationTOEntity.Map<AuthenticatedUser, User>(User);
                usersRepository.DeleteUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

        }

        public bool DeleteUser(UserDTO User)
        {
            try
            {
                var _user = usersRepository.GetSingle(d => d.UserName.Equals(User.UserName));
                if (_user != null)
                {
                    usersRepository.Remove(_user);
                    return true;
                }
            }
            catch (Exception ex)
            {

                //throw ex;
            }
            return false;
        }


        public IList<UserDTO> FindUsersByEmail(string EmailAddress)
        {
            IList<UserDTO> listUsers;
            try
            {
                IList<User> users = usersRepository.Find(d => d.Email.Equals(EmailAddress)).ToList();
                Mapper.CreateMap<User, UserDTO>();
                listUsers = Mapper.Map<IList<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listUsers;
        }

        public List<UserDTO> FindUsersByName(string UserName)
        {
            List<UserDTO> listUsers;
            try
            {
                IList<User> users = usersRepository.Find(d => d.UserName.Equals(UserName)).ToList();
                Mapper.CreateMap<User, UserDTO>();
                listUsers = Mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listUsers;
        }

        public int GetOnlineUsersCount()
        {
            return usersRepository.GetOnlineUsersCount();
        }

        public AuthenticatedUser GetUserByEmail(string emailAddress)
        {
            return Mapper.Map<User, AuthenticatedUser>(usersRepository.GetUserByEmail(emailAddress));
        }

        public UserDTO GetUserByID(int id)
        {
            Mapper.CreateMap<User, UserDTO>();
            return Mapper.Map<UserDTO>(usersRepository.GetFirstOrDefault(d => d.Id.Equals(id)));
        }

        public UserDTO GetUserByName(UserDTO User)
        {

            UserDTO userDto;
            try
            {
                User user = usersRepository.GetFirstOrDefault(d => d.UserName.Equals(User.UserName));
                Mapper.CreateMap<User, UserDTO>();
                userDto = Mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userDto;
        }

        public List<UserDTO> GetUsers(int index, int count)
        {
            List<UserDTO> listUsers;
            try
            {
                if (index == 0) index = 1;
                if (count == 0) index = 100;

                List<User> users = usersRepository.GetAll().Skip(index).Take(count).ToList();
                Mapper.CreateMap<User, UserDTO>();
                listUsers = Mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listUsers;
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> listUsers;
            try
            {
                IList<User> users = usersRepository.GetAll().ToList();
                Mapper.CreateMap<User, UserDTO>();
                listUsers = Mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listUsers;
        }

        public bool IsAuthenticated(string userName)
        {
            return usersRepository.IsAuthenticated(userName);
        }

        public PasswordResetResultType ResetPassword(string emailAddress)
        {
            return usersRepository.ResetPassword(emailAddress);
        }

        public string RetrievePassword(string userName)
        {
            throw new NotImplementedException();
        }

        public bool SignIn(AuthenticatedUser user)
        {
            Mapper.CreateMap<AuthenticatedUser, User>();
            var User = Mapper.Map<User>(user);
            //User.Password = Security.Encrypt(user.Password);
            return usersRepository.SignIn(User);
        }

        public bool SignOut(AuthenticatedUser user)
        {
            try
            {
                Mapper.CreateMap<AuthenticatedUser, User>();
                var User = Mapper.Map<User>(user);
                usersRepository.SignOut(User);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public void UpdateUser(AuthenticatedUser user)
        {
            var listUsers = Mapper.Map<AuthenticatedUser, User>(user);
            User[] users = new User[] { listUsers };
            usersRepository.Update(users);
        }
        public bool UpdateUser(UserDTO user)
        {
            try
            {
               
                if (UserExists(user.UserName))
                {
                    Mapper.CreateMap<UserDTO, User>();
                   // user = GetUserByName(user);
                    var User = Mapper.Map<User>(user);
                    //User[] users = new User[] { listUsers };
                    usersRepository.UpdateUser(User);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }

        public bool UserExists(string userName)
        {
            return usersRepository.UserExists(userName);
        }


        #endregion


    }
}
