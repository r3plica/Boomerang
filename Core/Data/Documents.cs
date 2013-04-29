using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data
{
    class Documents
    {
        public static int Create(Document pDocument)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateDocument";
            if (pDocument.ClientId != 0)
                SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pDocument.ClientId;
            if (pDocument.CandidateId != 0)
                SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pDocument.CandidateId;
            SQLCmd.Parameters.Add("FileName", SqlDbType.NVarChar, 255).Value = pDocument.FileName;
            SQLCmd.Parameters.Add("FileSize", SqlDbType.Int).Value = pDocument.FileSize;
            SQLCmd.Parameters.Add("FilePath", SqlDbType.NVarChar, 255).Value = pDocument.FilePath;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pDocument.UserId);
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Document pDocument)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateDocument";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pDocument.Id;
            if (pDocument.ClientId != 0)
                SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = pDocument.ClientId;
            if (pDocument.CandidateId != 0)
                SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pDocument.CandidateId;
            SQLCmd.Parameters.Add("FileName", SqlDbType.NVarChar, 255).Value = pDocument.FileName;
            SQLCmd.Parameters.Add("FileSize", SqlDbType.Int).Value = pDocument.FileSize;
            SQLCmd.Parameters.Add("FilePath", SqlDbType.NVarChar, 255).Value = pDocument.FilePath;
            SQLCmd.Parameters.Add("UserId", SqlDbType.UniqueIdentifier).Value = new Guid(pDocument.UserId);
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static void Delete(int DocumentId)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "DeleteDocument";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = DocumentId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Document Get(int Id)
        {
            Document Document = new Document();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetDocument";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Document = Parse(dr);
            }
            dr.Close();
            return Document;
        }

        public static Collection<Document> Get(int CandidateId,int ClientId)
        {
            Collection<Document> oDocuments = new Collection<Document>();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetDocuments";
            SQLCmd.Parameters.Add("ClientId", SqlDbType.Int).Value = ClientId;
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = CandidateId;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                oDocuments.Add(Parse(dr));
            }
            dr.Close();
            return oDocuments;
        }

        public static Document Parse(SqlDataReader dr)
        {
            Document Document = new Document();

            Document.Id = Convert.ToInt32(dr["Id"]);
            Document.ClientId = (dr["ClientId"] != DBNull.Value) ? Convert.ToInt32(dr["ClientId"]) : 0;
            Document.CandidateId = (dr["CandidateId"] != DBNull.Value) ? Convert.ToInt32(dr["CandidateId"]) : 0;
            Document.FileName = Convert.ToString(dr["FileName"]);
            Document.FileSize = Convert.ToInt32(dr["FileSize"]);
            Document.FilePath = Convert.ToString(dr["FilePath"]);
            Document.Date = Convert.ToDateTime(dr["Date"]);

            Document.UserId = Convert.ToString(dr["UserId"]);
            Document.UserName = Convert.ToString(dr["UserName"]);

            return Document;
        }
    }
}
