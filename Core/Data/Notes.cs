using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data
{
    class Notes
    {
        public static int Create(Note pNote)
        {
            Note oNote = new Note();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateNote";

            if (pNote.ClientId > 0)
                SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pNote.ClientId;
            if (pNote.CandidateId > 0) 
                SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pNote.CandidateId;

            SQLCmd.Parameters.Add("TypeId", SqlDbType.Int).Value = (int)pNote.TypeId;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pNote.UserId);
            SQLCmd.Parameters.Add("UserName", SqlDbType.NVarChar, 255).Value = pNote.UserName;
            SQLCmd.Parameters.Add("Message", SqlDbType.Text).Value = pNote.Message;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Note pNote)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateNote";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pNote.Id;
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pNote.ClientId;
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pNote.CandidateId;
            SQLCmd.Parameters.Add("TypeId", SqlDbType.Int).Value = pNote.TypeId;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pNote.UserId);
            SQLCmd.Parameters.Add("UserName", SqlDbType.NVarChar, 255).Value = pNote.UserName;
            SQLCmd.Parameters.Add("Message", SqlDbType.Text).Value = pNote.Message;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int NoteId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteNote";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = NoteId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Note Get(int id)
        {
            Note note = new Note();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetNote";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                note = Parse(dr);
            }
            dr.Close();
            return note;
        }

        public static Collection<Note> Get(int CandidateId, int ClientId)
        {
            Collection<Note> Notes = new Collection<Note>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetNotes";
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = CandidateId;
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = ClientId;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Notes.Add(Parse(dr));
            }
            dr.Close();
            return Notes;
        }

        public static Note Parse(SqlDataReader dr)
        {
            Note Note = new Note();

            Note.Id = Convert.ToInt32(dr["Id"]);
            Note.TypeId = (NoteType)Convert.ToInt32(dr["TypeId"]);;
            if (dr["ClientId"] != DBNull.Value) Note.ClientId = Convert.ToInt32(dr["ClientId"]);
            if (dr["CandidateId"] != DBNull.Value) Note.CandidateId = Convert.ToInt32(dr["CandidateId"]);
            Note.DateCreated = Convert.ToDateTime(dr["DateCreated"]);

            Note.UserId = Convert.ToString(dr["UserId"]);
            Note.UserName = Convert.ToString(dr["UserName"]);
            Note.Message = Convert.ToString(dr["Message"]);

            return Note;
        }
    }
}
