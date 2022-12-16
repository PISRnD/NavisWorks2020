using PivdcSupportModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PivdcSupportModule
{
    public static class DatabaseManager
    {
        public static double timein_milisec = 0;
        public static string constr = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=sarit;pwd=sarit@2008"; //for durgapur
                                                                                                                        //public static string constr = "Data Source=10.1.20.27;Initial Catalog=AppDatabase;User ID=sarit;pwd=sarit@2008"; //for jaipur

        public static bool InsertUsages(string AddIn_Name, int AddInsId, string DTPName, string DomainName, long NoOfElementProcess, long TimeTaken, string version, string filename, bool isUAT)
        {
            long empid = Convert.ToInt32(Regex.Replace(SupportDatas.CurrentLoginInformation.EmployeeId, @"[^\d]", ""));
            bool b = true;
            try
            {
                using (SqlConnection con = new SqlConnection(SupportDatas.RevitToolConnectionString))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand("Sp_InsertUsagesTracking_New", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AddIn_Name", AddIn_Name));
                    command.Parameters.Add(new SqlParameter("@AddInsId", Convert.ToString(AddInsId)));
                    command.Parameters.Add(new SqlParameter("@DTPName", DTPName));
                    command.Parameters.Add(new SqlParameter("@DomainName", DomainName));
                    command.Parameters.Add(new SqlParameter("@NoOfElementProcess", NoOfElementProcess));
                    command.Parameters.Add(new SqlParameter("@TimeTaken", TimeTaken));
                    command.Parameters.Add(new SqlParameter("@TimeTaken_Milliseconds", timein_milisec));
                    command.Parameters.Add(new SqlParameter("@version", version));
                    command.Parameters.Add(new SqlParameter("@Filename", filename));
                    command.Parameters.Add(new SqlParameter("@Project", SupportDatas.ProjectId));
                    command.Parameters.Add(new SqlParameter("@EmpID", empid));
                    command.Parameters.Add(new SqlParameter("@isUATRecord", isUAT));
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// Start time for the tool operation and ends automatically when call the "DatabaseManager.InsertUsage()" Method
        /// </summary>
        public static void SetTime()
        {
            TimeCaptureInfo.DateFrom = DateTime.Now;
        }

        public static double GetDuration()
        {
            TimeSpan tspan = DateTime.Now.Subtract(TimeCaptureInfo.DateFrom);
            timein_milisec = tspan.TotalMilliseconds;
            return tspan.TotalSeconds;
        }
    }
}