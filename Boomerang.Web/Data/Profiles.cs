using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace Boomerang.Web.Data
{
    class Profiles
    {
        public static void Save(Profile Profile)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateProfile";
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = Profile.ProviderUserKey;
            SQLCmd.Parameters.Add("UserName", SqlDbType.NVarChar, 255).Value = Profile.UserName;
            SQLCmd.Parameters.Add("Email", SqlDbType.NVarChar, 255).Value = Profile.Email;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Collection<Profile> Get()
        {
            Collection<Profile> Profile = new Collection<Profile>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetProfiles";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Profile.Add(Parse(dr));
            }
            dr.Close();
            return Profile;
        }

        public static Profile Parse(SqlDataReader dr)
        {
            Profile Profile = new Profile(
                "DefaultMembershipProvider",
                Convert.ToString(dr["UserName"]),
                new Guid(Convert.ToString(dr["UserId"])),
                Convert.ToString(dr["Email"]),
                Convert.ToString(dr["PasswordQuestion"]),
                Convert.ToString(dr["Comment"]),
                Convert.ToBoolean(dr["IsApproved"]),
                Convert.ToBoolean(dr["IsLockedOut"]),
                Convert.ToDateTime(dr["CreateDate"]),
                Convert.ToDateTime(dr["LastLoginDate"]),
                Convert.ToDateTime(dr["LastActivityDate"]),
                Convert.ToDateTime(dr["LastPasswordChangedDate"]),
                Convert.ToDateTime(dr["LastLockoutDate"]),
                Convert.ToString(dr["RoleId"]),
                Convert.ToString(dr["RoleName"])
            );

            return Profile;
        }
    }
}
