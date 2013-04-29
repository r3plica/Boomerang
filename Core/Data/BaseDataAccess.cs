using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Core.Data
{
    public static class BaseDataAccess
    {
        private static SqlConnection mConn;
        private static string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        /// <summary>
        /// This is called for single database interactions
        /// </summary>
        /// <param name="mConn"></param>
        public static void OpenConnection()
        {
            mConn = new SqlConnection();
            mConn.ConnectionString = Connection;
            mConn.Open();
        }

        /// <summary>
        /// This should be used for multiple database interactions
        /// </summary>
        /// <param name="pCmd"></param>
        /// <param name="mConn"></param>
        public static void OpenConnection(SqlCommand pCmd)
        {
            mConn = new SqlConnection();
            mConn.ConnectionString = Connection;
            pCmd.Connection = mConn;
            mConn.Open();
        }

        public static void CloseConnection()
        {
            mConn.Close();            
        }

        public static DataSet FillDataSet(SqlCommand pSQLCmd)
		{
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter();
            if (mConn != null)
            {
				pSQLCmd.Connection = mConn;
				da.SelectCommand = pSQLCmd;
				da.Fill(ds);
				da.Dispose();
			} else {
                throw new System.ApplicationException("Connection String has not been set");
			}
			return ds;
		}

        public static string ExecuteScalar(SqlCommand pSQLCmd)
        {
            string scalar = "";
            if (mConn != null)
            {
                pSQLCmd.Connection = mConn;
                pSQLCmd.ExecuteScalar();
            }
            else
            {
                throw new System.ApplicationException("Connection String has not been set");
            }
            return scalar;
        }

        public static void ExecuteNonSelect(SqlCommand pSQLCmd)
        {
            if (mConn != null)
            {
				pSQLCmd.Connection = mConn;
				pSQLCmd.ExecuteNonQuery();
			} else {
                throw new System.ApplicationException("Connection String has not been set");
			}
        }
    }
}
