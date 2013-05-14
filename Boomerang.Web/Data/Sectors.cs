using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Boomerang.Web.Data
{
    class Sectors
    {
        public static int Create(Sector pSector)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateSector";
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pSector.ClientId;
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 255).Value = pSector.Name;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Sector pSector)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateSector";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pSector.Id;
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pSector.ClientId;
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 255).Value = pSector.Name;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int SectorId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteSector";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = SectorId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Collection<Sector> Get(int ClientId)
        {
            Collection<Sector> oSectors = new Collection<Sector>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetSectors";
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = ClientId;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                oSectors.Add(Parse(dr));
            }
            dr.Close();
            return oSectors;
        }

        public static Sector Parse(SqlDataReader dr)
        {
            Sector Sector = new Sector();

            Sector.Id = Convert.ToInt32(dr["Id"]);
            Sector.Name = Convert.ToString(dr["Name"]);

            return Sector;
        }
    }
}
