using AccessLDAP.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceLDAP
{

    [ServiceKnownType(typeof(PasswordResetResultType))]
    [ServiceKnownType(typeof(IList))]
    [ServiceKnownType(typeof(SSOUserAlreadyExistsException))]
    [ServiceKnownType(typeof(SSOUserDoesNotExistException))]
    [ServiceKnownType(typeof(SSOInvalidPasswordException))]
    [ServiceKnownType(typeof(AuthenticatedUser))]
 
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISSO
    {
        [OperationContract]
        [FaultContract(typeof(SSOUserAlreadyExistsException))]
        bool CreateUser(string userName, string password, string eMail, bool isActive);
        //void CreateUser(AuthenticatedUserType User);

        [OperationContract]
        bool DeleteUser(string userName);

        [OperationContract]
        bool UserExists(string userName);

        [OperationContract]
        bool ChangePassword(string userName, string oldPaassword, string newPassword);

        [OperationContract]
        List<UserDTO> GetUsers(int index, int count);

        [OperationContract]
        List<UserDTO> GetAllUsers();

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        AuthenticatedUser GetUserByName(string userName);


        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        AuthenticatedUser GetUserByEmail(string emailAddress);

        [OperationContract]
        //[FaultContract(typeof(SSOUserDoesNotExistException))]
        UserDTO GetUserByID(int id);


        [OperationContract]
        List<UserDTO> FindUsersByEmail(string EmailAddress);

        [OperationContract]
        List<UserDTO> FindUsersByName(string UserName);

        [OperationContract]
        [FaultContract(typeof(SSOInvalidPasswordException))]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        bool SignIn(string userName, string password);

        [OperationContract]
        bool SignOut(string userName);

        [OperationContract]
        bool IsAuthenticated(string userName);

        [OperationContract]
        PasswordResetResultType ResetPassword(string emailAddress);

        [OperationContract]
        int GetOnlineCount();


        [OperationContract]
        bool UpdateUser(string userName, string password, string eMail, bool isActive);

    }

    [DataContract]
    public class AuthenticatedUserType
    {
        private AuthenticatedUserType() : base()
        {

        }
        public AuthenticatedUserType(object identifier)
        {

            Key = identifier;
        }

        [DataMember]
        public Object Key
        {
            get;
            set;
        }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

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
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

}
