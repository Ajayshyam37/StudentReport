using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace StudentReportFromExceltoDB
{
    class Statistics
    {
        public int totalId;
        public string totalName;
        public int totalvalue;
        public string BiologyMax;
        public string BiologyName;
        public string BiologyId;
        public string ChemMax;
        public string ChemName;
        public string ChemId;
        public string PhyMax;
        public string PhyName;
        public string PhyId;
        public string SocMax;
        public string SocName;
        public string SocId;
        public string MathsMax;
        public string MathsName;
        public string MathsId;
        public string CompMax;
        public string CompName;
        public string CompId;




        public Statistics()
        {
            GetMaxMarksfromDB();
            GetTotal();
        }
        private void GetTotal()
        {
            DatabaseConnection db = new DatabaseConnection();
            SqlConnection connection = new SqlConnection();
            connection = db.GetDbConnection();
            SqlCommand command = new SqlCommand("GetTotal", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            totalId = Convert.ToInt32(reader[1]);
            totalName = reader[0].ToString();
            totalvalue = Convert.ToInt32(reader[2]);
            connection.Close();
        }

        
        private void GetMaxMarksfromDB()
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection();
                SqlConnection connection;
                connection = db.GetDbConnection();
                //Chemistry", "Physics", "Biology", "Social", "Mathematics", "Computers
                SubjectToppers(connection, "Biology");
                SubjectToppers(connection, "Chemistry");
                SubjectToppers(connection, "Physics");
                SubjectToppers(connection, "Social");
                SubjectToppers(connection, "Mathematics");
                SubjectToppers(connection, "Computers");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private void SubjectToppers(SqlConnection connection , string subject)
        {
            SqlCommand GetMax = new SqlCommand("Subjecttopper", connection);
            GetMax.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@sub",
                Value = subject
            };
            GetMax.Parameters.Add(param);

            SqlDataReader reader = GetMax.ExecuteReader();
            reader.Read();
            if(subject == "Biology")
            {
                BiologyId = Convert.ToString(reader[0]);
                BiologyName = Convert.ToString(reader[1]);
                BiologyMax = Convert.ToString(reader[3]);
                reader.Close();
            }
            if (subject == "Chemistry")
            {
                ChemId = Convert.ToString(reader[0]);
                ChemName = Convert.ToString(reader[1]);
                ChemMax = Convert.ToString(reader[3]);
                reader.Close();

            }
            if (subject == "Physics")
            {
                PhyId = Convert.ToString(reader[0]);
                PhyName = Convert.ToString(reader[1]);
                PhyMax = Convert.ToString(reader[3]);
                reader.Close();
            }
            if (subject == "Social")
            {
                SocId = Convert.ToString(reader[0]);
                SocName = Convert.ToString(reader[1]);
                SocMax = Convert.ToString(reader[3]);
                reader.Close();
            }
            if (subject == "Mathematics")
            {
                MathsId = Convert.ToString(reader[0]);
                MathsName = Convert.ToString(reader[1]);
                MathsMax = Convert.ToString(reader[3]);
                reader.Close();
            }
            if (subject == "Computers")
            {
                CompId = Convert.ToString(reader[0]);
                CompName = Convert.ToString(reader[1]);
                CompMax = Convert.ToString(reader[3]);
                reader.Close();
            }
        }
    }
}
