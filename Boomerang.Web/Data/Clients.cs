using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Boomerang.Web.Data
{
    class Clients
    {
        public static int Create(Client pClient)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateClient";
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pClient.UserId);
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 255).Value = pClient.Name;
            SQLCmd.Parameters.Add("Website", SqlDbType.NVarChar, 255).Value = pClient.Website;
            SQLCmd.Parameters.Add("Bio", SqlDbType.Text).Value = pClient.Bio;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Client pClient)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateClient";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pClient.Id;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pClient.UserId);
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 255).Value = pClient.Name;
            SQLCmd.Parameters.Add("Active", SqlDbType.Bit).Value = pClient.Active;
            SQLCmd.Parameters.Add("Website", SqlDbType.NVarChar, 255).Value = pClient.Website;
            SQLCmd.Parameters.Add("Bio", SqlDbType.Text).Value = pClient.Bio;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int ClientId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteClient";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = ClientId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Client Get(int Id)
        {
            Client oClient = new Client();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetClient";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                oClient = Parse(dr);
            }
            dr.Close();
            return oClient;
        }

        public static Collection<Client> Get()
        {
            Collection<Client> oClient = new Collection<Client>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetClients";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                oClient.Add(Parse(dr));
            }
            dr.Close();
            return oClient;
        }

        public static Client Parse(SqlDataReader dr)
        {
            Client Client = new Client();

            Client.Id = Convert.ToInt32(dr["Id"]);
            Client.Name = Convert.ToString(dr["Name"]);
            Client.Created = Convert.ToDateTime(dr["Created"]);
            Client.Active = Convert.ToBoolean(dr["Active"]);
            Client.Website = Convert.ToString(dr["Website"]);
            Client.Bio = Convert.ToString(dr["Bio"]);

            Client.UserId = Convert.ToString(dr["UserId"]);
            Client.UserName = Convert.ToString(dr["UserName"]);

            return Client;
        }
    }
}
