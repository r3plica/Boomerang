using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data
{
    class Candidates
    {
        public static int Create(Candidate pCandidate)
        {
            Candidate oCandidate = new Candidate();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateCandidate";
            SQLCmd.Parameters.Add("Forename", SqlDbType.NVarChar, 255).Value = pCandidate.Forename;
            SQLCmd.Parameters.Add("Surname", SqlDbType.NVarChar, 255).Value = pCandidate.Surname;
            SQLCmd.Parameters.Add("Telephone", SqlDbType.NVarChar, 50).Value = pCandidate.Telephone;
            SQLCmd.Parameters.Add("Mobile", SqlDbType.NVarChar, 50).Value = pCandidate.Mobile;
            SQLCmd.Parameters.Add("Email", SqlDbType.NVarChar, 255).Value = pCandidate.Email;
            SQLCmd.Parameters.Add("DOB", SqlDbType.Date).Value = pCandidate.DOB;
            SQLCmd.Parameters.Add("Active", SqlDbType.Bit).Value = pCandidate.Active;
            SQLCmd.Parameters.Add("LastContactDate", SqlDbType.Date).Value = pCandidate.LastContactDate;
            SQLCmd.Parameters.Add("NiNumber", SqlDbType.NVarChar, 50).Value = pCandidate.NiNumber;
            SQLCmd.Parameters.Add("CRB", SqlDbType.Bit).Value = pCandidate.CRB;
            SQLCmd.Parameters.Add("References", SqlDbType.Bit).Value = pCandidate.References;
            SQLCmd.Parameters.Add("Interviewed", SqlDbType.Bit).Value = pCandidate.Interviewed;
            SQLCmd.Parameters.Add("TransportId", SqlDbType.Int).Value = pCandidate.TransportId;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pCandidate.UserId);
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Candidate pCandidate)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateCandidate";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pCandidate.Id;
            SQLCmd.Parameters.Add("Forename", SqlDbType.NVarChar, 255).Value = pCandidate.Forename;
            SQLCmd.Parameters.Add("Surname", SqlDbType.NVarChar, 255).Value = pCandidate.Surname;
            SQLCmd.Parameters.Add("Telephone", SqlDbType.NVarChar, 50).Value = pCandidate.Telephone;
            SQLCmd.Parameters.Add("Mobile", SqlDbType.NVarChar, 50).Value = pCandidate.Mobile;
            SQLCmd.Parameters.Add("Email", SqlDbType.NVarChar, 255).Value = pCandidate.Email;
            SQLCmd.Parameters.Add("DOB", SqlDbType.Date).Value = pCandidate.DOB;
            SQLCmd.Parameters.Add("Active", SqlDbType.Bit).Value = pCandidate.Active;
            SQLCmd.Parameters.Add("LastContactDate", SqlDbType.Date).Value = pCandidate.LastContactDate;
            SQLCmd.Parameters.Add("NiNumber", SqlDbType.NVarChar, 50).Value = pCandidate.NiNumber;
            SQLCmd.Parameters.Add("CRB", SqlDbType.Bit).Value = pCandidate.CRB;
            SQLCmd.Parameters.Add("References", SqlDbType.Bit).Value = pCandidate.References;
            SQLCmd.Parameters.Add("Interviewed", SqlDbType.Bit).Value = pCandidate.Interviewed;
            SQLCmd.Parameters.Add("TransportId", SqlDbType.Int).Value = pCandidate.TransportId;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pCandidate.UserId);
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int CandidateId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteCandidate";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = CandidateId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Candidate Get(int Id)
        {
            Candidate oCandidate = new Candidate();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetCandidate";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                oCandidate = Parse(dr);
            }
            dr.Close();
            return oCandidate;
        }

        public static Collection<Candidate> Get()
        {
            Collection<Candidate> oCandidate = new Collection<Candidate>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetCandidates";
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                oCandidate.Add(Parse(dr));
            }
            dr.Close();
            return oCandidate;
        }

        public static Candidate Parse(SqlDataReader dr)
        {
            Candidate Candidate = new Candidate();

            Candidate.Id = Convert.ToInt32(dr["Id"]);
            Candidate.Forename = Convert.ToString(dr["Forename"]);
            Candidate.Surname = Convert.ToString(dr["Surname"]);
            if (dr["Telephone"] != DBNull.Value) Candidate.Telephone = Convert.ToString(dr["Telephone"]);
            if (dr["Mobile"] != DBNull.Value) Candidate.Mobile = Convert.ToString(dr["Mobile"]);
            if (dr["Email"] != DBNull.Value) Candidate.Email = Convert.ToString(dr["Email"]);
            if (dr["DOB"] != DBNull.Value) Candidate.DOB = Convert.ToDateTime(dr["DOB"]).ToShortDateString();
            Candidate.Active = Convert.ToBoolean(dr["Active"]);
            if (dr["LastContactDate"] != DBNull.Value) Candidate.LastContactDate = Convert.ToDateTime(dr["LastContactDate"]).ToShortDateString();
            if (dr["NiNumber"] != DBNull.Value) Candidate.NiNumber = Convert.ToString(dr["NiNumber"]);
            Candidate.CRB = Convert.ToBoolean(dr["CRB"]);
            Candidate.References = Convert.ToBoolean(dr["References"]);
            Candidate.Interviewed = Convert.ToBoolean(dr["Interviewed"]);
            Candidate.DateCreated = Convert.ToDateTime(dr["DateCreated"]);
            Candidate.TransportId = Convert.ToInt32(dr["TransportId"]);
            Candidate.TransportType = Convert.ToString(dr["TransportType"]);

            Candidate.UserId = (dr["UserId"] != DBNull.Value) ? Convert.ToString(dr["UserId"]) : null;
            Candidate.UserName = (dr["UserName"] != DBNull.Value) ? Convert.ToString(dr["UserName"]) : null;

            return Candidate;
        }
    }
}
