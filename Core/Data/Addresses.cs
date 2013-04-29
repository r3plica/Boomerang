using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data
{
    class Addresses
    {
        public static int Create(Address pAddress)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateAddress";
            if (pAddress.ClientId > 0) SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pAddress.ClientId;
            if (pAddress.CandidateId > 0) SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pAddress.CandidateId;
            SQLCmd.Parameters.Add("HouseNumber", SqlDbType.NVarChar, 50).Value = pAddress.HouseNumber;
            SQLCmd.Parameters.Add("Street", SqlDbType.NVarChar, 100).Value = pAddress.Street;
            SQLCmd.Parameters.Add("Area", SqlDbType.NVarChar, 100).Value = pAddress.Area;
            SQLCmd.Parameters.Add("Town", SqlDbType.NVarChar, 100).Value = pAddress.Town;
            SQLCmd.Parameters.Add("County", SqlDbType.NVarChar, 100).Value = pAddress.County;
            SQLCmd.Parameters.Add("PostCode", SqlDbType.NVarChar, 10).Value = pAddress.PostCode;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Address pAddress)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateAddress";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pAddress.Id;
            if (pAddress.ClientId > 0) SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pAddress.ClientId;
            if (pAddress.CandidateId > 0) SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pAddress.CandidateId;
            SQLCmd.Parameters.Add("HouseNumber", SqlDbType.NVarChar, 50).Value = pAddress.HouseNumber;
            SQLCmd.Parameters.Add("Street", SqlDbType.NVarChar, 100).Value = pAddress.Street;
            SQLCmd.Parameters.Add("Area", SqlDbType.NVarChar, 100).Value = pAddress.Area;
            SQLCmd.Parameters.Add("Town", SqlDbType.NVarChar, 100).Value = pAddress.Town;
            SQLCmd.Parameters.Add("County", SqlDbType.NVarChar, 100).Value = pAddress.County;
            SQLCmd.Parameters.Add("PostCode", SqlDbType.NVarChar, 10).Value = pAddress.PostCode;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int AddressId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteAddress";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = AddressId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Collection<Address> Get(int CandidateId, int ClientId)
        {
            Collection<Address> Addresss = new Collection<Address>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetAddresses";
            if (ClientId > 0) SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = ClientId;
            if (CandidateId > 0) SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = CandidateId;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Addresss.Add(Parse(dr));
            }
            dr.Close();
            return Addresss;
        }

        public static Address Get(int Id)
        {
            Address Address = new Address();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetAddress";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Address = Parse(dr);
            }
            dr.Close();
            return Address;
        }

        public static Address Parse(SqlDataReader dr)
        {
            Address Address = new Address();

            Address.Id = Convert.ToInt32(dr["Id"]);
            if (dr["ClientId"] != DBNull.Value) Address.ClientId = Convert.ToInt32(dr["ClientId"]);
            if (dr["CandidateId"] != DBNull.Value) Address.CandidateId = Convert.ToInt32(dr["CandidateId"]);

            if (dr["HouseNumber"] != DBNull.Value) Address.HouseNumber = Convert.ToString(dr["HouseNumber"]);
            if (dr["Street"] != DBNull.Value) Address.Street = Convert.ToString(dr["Street"]);
            if (dr["Area"] != DBNull.Value) Address.Area = Convert.ToString(dr["Area"]);
            if (dr["Town"] != DBNull.Value) Address.Town = Convert.ToString(dr["Town"]);
            if (dr["County"] != DBNull.Value) Address.County = Convert.ToString(dr["County"]);
            if (dr["PostCode"] != DBNull.Value) Address.PostCode = Convert.ToString(dr["PostCode"]);

            return Address;
        }
    }
}
