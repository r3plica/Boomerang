using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Boomerang.Web.Data
{
    class Contacts
    {
        public static int Create(Contact pContact)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateContact";
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pContact.ClientId;
            SQLCmd.Parameters.Add("Forename", SqlDbType.NVarChar, 255).Value = pContact.Forename;
            SQLCmd.Parameters.Add("Surname", SqlDbType.NVarChar, 255).Value = pContact.Surname;
            SQLCmd.Parameters.Add("Office", SqlDbType.NVarChar, 255).Value = pContact.Office;
            SQLCmd.Parameters.Add("Email", SqlDbType.NVarChar, 255).Value = pContact.Email;
            SQLCmd.Parameters.Add("EmailAlt", SqlDbType.NVarChar, 255).Value = pContact.EmailAlt;
            SQLCmd.Parameters.Add("JobTitle", SqlDbType.NVarChar, 100).Value = pContact.JobTitle;
            SQLCmd.Parameters.Add("Telephone", SqlDbType.NVarChar, 50).Value = pContact.Telephone;
            SQLCmd.Parameters.Add("TelephoneAlt", SqlDbType.NVarChar, 50).Value = pContact.TelephoneAlt;
            SQLCmd.Parameters.Add("Fax", SqlDbType.NVarChar, 50).Value = pContact.Fax;
            SQLCmd.Parameters.Add("CallbackDate", SqlDbType.NVarChar, 10).Value = pContact.CallbackDate;
            SQLCmd.Parameters.Add("Primary", SqlDbType.Bit).Value = pContact.Primary;
            SQLCmd.Parameters.Add("Bio", SqlDbType.Text).Value = pContact.Bio;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Contact pContact)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateContact";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pContact.Id;
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pContact.ClientId;
            SQLCmd.Parameters.Add("Forename", SqlDbType.NVarChar, 255).Value = pContact.Forename;
            SQLCmd.Parameters.Add("Surname", SqlDbType.NVarChar, 255).Value = pContact.Surname;
            SQLCmd.Parameters.Add("Office", SqlDbType.NVarChar, 255).Value = pContact.Office;
            SQLCmd.Parameters.Add("Email", SqlDbType.NVarChar, 255).Value = pContact.Email;
            SQLCmd.Parameters.Add("EmailAlt", SqlDbType.NVarChar, 255).Value = pContact.EmailAlt;
            SQLCmd.Parameters.Add("JobTitle", SqlDbType.NVarChar, 100).Value = pContact.JobTitle;
            SQLCmd.Parameters.Add("Telephone", SqlDbType.NVarChar, 50).Value = pContact.Telephone;
            SQLCmd.Parameters.Add("TelephoneAlt", SqlDbType.NVarChar, 50).Value = pContact.TelephoneAlt;
            SQLCmd.Parameters.Add("Fax", SqlDbType.NVarChar, 50).Value = pContact.Fax;
            SQLCmd.Parameters.Add("CallbackDate", SqlDbType.NVarChar, 10).Value = pContact.CallbackDate;
            SQLCmd.Parameters.Add("Primary", SqlDbType.Bit).Value = pContact.Primary;
            SQLCmd.Parameters.Add("Bio", SqlDbType.Text).Value = pContact.Bio;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int ContactId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteContact";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = ContactId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Contact Get(int Id)
        {
            Contact Contact = new Contact();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetContact";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Contact = Parse(dr);
            }
            dr.Close();
            return Contact;
        }

        public static Collection<Contact> GetClient(int ClientId)
        {
            Collection<Contact> Contacts = new Collection<Contact>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetContacts";
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = ClientId;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Contacts.Add(Parse(dr));
            }
            dr.Close();
            return Contacts;
        }

        public static Contact Parse(SqlDataReader dr)
        {
            Contact Contact = new Contact();

            Contact.Id = Convert.ToInt32(dr["Id"]);
            Contact.ClientId = Convert.ToInt32(dr["ClientId"]);
            Contact.Forename = Convert.ToString(dr["Forename"]);
            Contact.Surname = Convert.ToString(dr["Surname"]);
            Contact.Office = Convert.ToString(dr["Office"]);
            Contact.Email = Convert.ToString(dr["Email"]);
            Contact.EmailAlt = Convert.ToString(dr["EmailAlt"]);
            Contact.JobTitle = Convert.ToString(dr["JobTitle"]);
            Contact.Telephone = Convert.ToString(dr["Telephone"]);
            Contact.TelephoneAlt = Convert.ToString(dr["TelephoneAlt"]);
            Contact.Fax = Convert.ToString(dr["Fax"]);
            if (dr["Created"] != DBNull.Value) Contact.Created = Convert.ToDateTime(dr["Created"]);
            Contact.CallbackDate = Convert.ToString(dr["CallbackDate"]);
            Contact.Bio = Convert.ToString(dr["Bio"]);
            Contact.Primary = Convert.ToBoolean(dr["Primary"]);

            return Contact;
        }
    }
}
