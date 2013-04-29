using System;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data
{
    class Salaries
    {
        public static int Create(Salary pSalary)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "CreateSalary";
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pSalary.CandidateId;
            SQLCmd.Parameters.Add("FullTime", SqlDbType.Bit).Value = pSalary.FullTime;
            SQLCmd.Parameters.Add("PartTime", SqlDbType.Bit).Value = pSalary.PartTime;
            SQLCmd.Parameters.Add("Student", SqlDbType.Bit).Value = pSalary.Student;
            SQLCmd.Parameters.Add("Temp", SqlDbType.Bit).Value = pSalary.Temp;
            SQLCmd.Parameters.Add("NightShifts", SqlDbType.Bit).Value = pSalary.NightShifts;
            SQLCmd.Parameters.Add("DayShifts", SqlDbType.Bit).Value = pSalary.DayShifts;
            SQLCmd.Parameters.Add("Other", SqlDbType.Bit).Value = pSalary.Other;
            SQLCmd.Parameters.Add("TempWage", SqlDbType.Int).Value = pSalary.TempWage;
            SQLCmd.Parameters.Add("TempRateId", SqlDbType.Int).Value = pSalary.TempRateId;
            SQLCmd.Parameters.Add("PermWage", SqlDbType.Int).Value = pSalary.PermWage;
            SQLCmd.Parameters.Add("PermRateId", SqlDbType.Int).Value = pSalary.PermRateId;
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();

            return Convert.ToInt32(SQLCmd.Parameters["Id"].Value);
        }

        public static void Edit(Salary pSalary)
        {
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "UpdateSalary";
            SQLCmd.Parameters.Add("Id", SqlDbType.Int).Value = pSalary.Id;
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = pSalary.CandidateId;
            SQLCmd.Parameters.Add("FullTime", SqlDbType.Bit).Value = pSalary.FullTime;
            SQLCmd.Parameters.Add("PartTime", SqlDbType.Bit).Value = pSalary.PartTime;
            SQLCmd.Parameters.Add("Student", SqlDbType.Bit).Value = pSalary.Student;
            SQLCmd.Parameters.Add("Temp", SqlDbType.Bit).Value = pSalary.Temp;
            SQLCmd.Parameters.Add("NightShifts", SqlDbType.Bit).Value = pSalary.NightShifts;
            SQLCmd.Parameters.Add("DayShifts", SqlDbType.Bit).Value = pSalary.DayShifts;
            SQLCmd.Parameters.Add("Other", SqlDbType.Bit).Value = pSalary.Other;
            SQLCmd.Parameters.Add("TempWage", SqlDbType.Int).Value = pSalary.TempWage;
            SQLCmd.Parameters.Add("TempRateId", SqlDbType.Int).Value = pSalary.TempRateId;
            SQLCmd.Parameters.Add("PermWage", SqlDbType.Int).Value = pSalary.PermWage;
            SQLCmd.Parameters.Add("PermRateId", SqlDbType.Int).Value = pSalary.PermRateId;
            BaseDataAccess.OpenConnection(SQLCmd);
            BaseDataAccess.ExecuteNonSelect(SQLCmd);
            BaseDataAccess.CloseConnection();
        }

        public static Salary Get(int Id)
        {
            Salary Salary = new Salary();
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.CommandText = "GetSalary";
            SQLCmd.Parameters.Add("CandidateId", SqlDbType.Int).Value = Id;
            BaseDataAccess.OpenConnection(SQLCmd);
            SqlDataReader dr = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Salary = Parse(dr);
            }
            dr.Close();
            return Salary;
        }


        public static Salary Parse(SqlDataReader dr)
        {
            Salary Salary = new Salary();

            Salary.Id = Convert.ToInt32(dr["Id"]);
            Salary.CandidateId = Convert.ToInt32(dr["CandidateId"]);

            Salary.FullTime = Convert.ToBoolean(dr["FullTime"]);
            Salary.PartTime = Convert.ToBoolean(dr["PartTime"]);
            Salary.Student = Convert.ToBoolean(dr["Student"]);
            Salary.Temp = Convert.ToBoolean(dr["Temp"]);
            Salary.NightShifts = Convert.ToBoolean(dr["NightShifts"]);
            Salary.DayShifts = Convert.ToBoolean(dr["DayShifts"]);
            Salary.Other = Convert.ToBoolean(dr["Other"]);

            Salary.TempWage = Convert.ToInt32(dr["TempWage"]);
            Salary.TempRateId = Convert.ToInt32(dr["TempRateId"]);
            Salary.SetTempRate(Convert.ToString(dr["TempRate"]));
            Salary.PermWage = Convert.ToInt32(dr["PermWage"]);
            Salary.PermRateId = Convert.ToInt32(dr["PermRateId"]);
            Salary.SetPermRate(Convert.ToString(dr["PermRate"]));

            return Salary;
        }
    }
}
