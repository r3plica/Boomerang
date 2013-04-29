using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.Data
{
    class Experiences
    {
        public static int Create(Experience pExperience)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateExperience";
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pExperience.CandidateId;
            SQLCmd.Parameters.Add("TypeId", SqlDbType.Int).Value = (int)pExperience.TypeId;
            SQLCmd.Parameters.Add("Name", SqlDbType.VarChar, 100).Value = pExperience.Name;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Experience pExperience)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateExperience";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pExperience.Id;
            SQLCmd.Parameters.Add("Name", SqlDbType.VarChar, 100).Value = pExperience.Name;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int ExperienceId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteExperience";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = ExperienceId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Collection<Experience> Get(int CandidateId)
        {
            Collection<Experience> Experiences = new Collection<Experience>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetExperience";
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = CandidateId;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Experiences.Add(Parse(dr));
            }
            dr.Close();
            return Experiences;
        }

        public static Experience Parse(SqlDataReader dr)
        {
            Experience Experience = new Experience();

            Experience.Id = Convert.ToInt32(dr["Id"]);
            Experience.CandidateId = Convert.ToInt32(dr["CandidateId"]);
            Experience.TypeId = (ExperienceType)Convert.ToInt32(dr["TypeId"]);

            Experience.Name = Convert.ToString(dr["Name"]);
            Experience.Type = Convert.ToString(dr["Type"]);

            return Experience;
        }
    }
}
