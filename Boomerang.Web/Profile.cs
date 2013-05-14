using Boomerang.Web.Data;
using System;
using System.Web.Security;

namespace Boomerang.Web
{
    public class Profile
    {

        #region Fields

        private MembershipUser _CurrentUser;

#endregion
        
        #region Properties

        public string UserName { get; set; }
        public object ProviderUserKey { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public DateTime LastLockedOutDate { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public bool IsOnline
        {
            get
            {
                if (LastLoginDate > DateTime.Now.AddMinutes(-10))
                    return true;

                return false;
            }
        }

        #endregion

        #region Constructors

        public Profile(MembershipUser CurrentUser)
        {
            this.UserName = CurrentUser.UserName;
            this.ProviderUserKey = CurrentUser.ProviderUserKey;
            this.Email = CurrentUser.Email;
            this.PasswordQuestion = CurrentUser.PasswordQuestion;
            this.Comment = CurrentUser.Comment;
            this.IsApproved = CurrentUser.IsApproved;
            this.IsLockedOut = CurrentUser.IsLockedOut;
            this.CreationDate = CurrentUser.CreationDate;
            this.LastLoginDate = CurrentUser.LastLoginDate;
            this.LastActivityDate = CurrentUser.LastActivityDate;
            this.LastPasswordChangedDate = CurrentUser.LastPasswordChangedDate;
            this.LastLockedOutDate = CurrentUser.LastLockoutDate;

            this._CurrentUser = CurrentUser;
        }

        public Profile(string providername,
                    string UserName,
                    object ProviderUserKey,
                    string Email,
                    string PasswordQuestion,
                    string Comment,
                    bool IsApproved,
                    bool IsLockedOut,
                    DateTime CreationDate,
                    DateTime LastLoginDate,
                    DateTime LastActivityDate,
                    DateTime LastPasswordChangedDate,
                    DateTime LastLockedOutDate,
                    string RoleId,
                    string RoleName)
        {
            this.UserName = UserName;
            this.ProviderUserKey = ProviderUserKey;
            this.Email = Email;
            this.PasswordQuestion = PasswordQuestion;
            this.Comment = Comment;
            this.IsApproved = IsApproved;
            this.IsLockedOut = IsLockedOut;
            this.CreationDate = CreationDate;
            this.LastLoginDate = LastLoginDate;
            this.LastActivityDate = LastActivityDate;
            this.LastPasswordChangedDate = LastPasswordChangedDate;
            this.LastLockedOutDate = LastLockedOutDate;
            this.RoleId = RoleId;
            this.RoleName = RoleName;

            _CurrentUser = new MembershipUser("DefaultMembershipProvider", this.UserName, this.ProviderUserKey, this.Email, this.PasswordQuestion, this.Comment, this.IsApproved, this.IsLockedOut, this.CreationDate, this.LastLoginDate, this.LastActivityDate, this.LastPasswordChangedDate, this.LastLockedOutDate);
        }

        #endregion

        #region Public methods

        public bool Save()
        {
            try
            {
                Profiles.Save(this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangePassword(string OldPassword, string newPassword)
        {
            return _CurrentUser.ChangePassword(OldPassword, newPassword);
        }

        #endregion
    }
}
