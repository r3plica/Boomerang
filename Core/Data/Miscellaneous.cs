using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data
{
    class Miscellaneous
    {
        public static Collection<GenericType> Get()
        {
            Collection<GenericType> WorkSkillTypes = new Collection<GenericType>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetWorkSkillTypes";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                WorkSkillTypes.Add(Parse(dr));
            }
            dr.Close();
            return WorkSkillTypes;
        }

        public static GenericType Parse(SqlDataReader dr)
        {
            GenericType List = new SkillWorkType();

            List.Id = Convert.ToInt32(dr["Id"]);
            List.Name = Convert.ToString(dr["Name"]);
            List.Order = Convert.ToInt32(dr["Order"]);

            return List;
        }
    }

    class TransportTypes
    {
        public static int Create(GenericType Type)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateTransportType";
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 100).Value = Type.Name;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Update(GenericType Type)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateTransportType";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Type.Id;
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 100).Value = Type.Name;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static int Move(int Order, bool Direction)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "MoveTransportType";
            SQLCmd.Parameters.Add("Order", SqlDbType.Int).Value = Order;
            SQLCmd.Parameters.Add("Direction", SqlDbType.Bit).Value = Direction;
            SQLCmd.Parameters.Add("NewOrder", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["NewOrder"].Value);
        }

        public static Collection<GenericType> Get()
        {
            Collection<GenericType> TransportTypes = new Collection<GenericType>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetTransportTypes";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                TransportTypes.Add(Parse(dr));
            }
            dr.Close();
            return TransportTypes;
        }

        public static GenericType Get(int Id)
        {
            GenericType TransportType = new TransportType();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetTransportType";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                TransportType = Parse(dr);
            }
            dr.Close();
            return TransportType;
        }

        public static GenericType Parse(SqlDataReader dr)
        {
            GenericType List = new TransportType();

            List.Id = Convert.ToInt32(dr["Id"]);
            List.Name = Convert.ToString(dr["Name"]);
            List.Order = Convert.ToInt32(dr["Order"]);

            return List;
        }
    }

    class SalaryRates
    {
        public static int Create(GenericType Rate)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateSalaryRate";
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 100).Value = Rate.Name;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Update(GenericType Rate)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateSalaryRate";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Rate.Id;
            SQLCmd.Parameters.Add("Name", SqlDbType.NVarChar, 100).Value = Rate.Name;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static int Move(int Order, bool Direction)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "MoveSalaryRate";
            SQLCmd.Parameters.Add("Order", SqlDbType.Int).Value = Order;
            SQLCmd.Parameters.Add("Direction", SqlDbType.Bit).Value = Direction;
            SQLCmd.Parameters.Add("NewOrder", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["NewOrder"].Value);
        }

        public static Collection<GenericType> Get()
        {
            Collection<GenericType> SalaryRates = new Collection<GenericType>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetSalaryRates";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                SalaryRates.Add(Parse(dr));
            }
            dr.Close();
            return SalaryRates;
        }

        public static GenericType Get(int Id)
        {
            GenericType SalaryRate = new SalaryRate();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetSalaryRate";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                SalaryRate = Parse(dr);
            }
            dr.Close();
            return SalaryRate;
        }

        public static GenericType Parse(SqlDataReader dr)
        {
            GenericType List = new SalaryRate();

            List.Id = Convert.ToInt32(dr["Id"]);
            List.Name = Convert.ToString(dr["Name"]);
            List.Order = Convert.ToInt32(dr["Order"]);

            return List;
        }
    }
}
