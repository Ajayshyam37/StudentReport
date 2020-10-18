using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace StudentReportFromExceltoDB
{   
    class Program
    {
        //Add Nuget Package Excel Data Reader
        static void Main()
        {

            string filelocation = ReadFile();
            ReadStudents(filelocation);
            Console.ReadLine();
            ShowStats();
        }

        private static string ReadFile()
        {
          
        notfound:
            Console.WriteLine("Welcome To Quaterly Report Calculator !!");
            Console.WriteLine(" ");
            Console.WriteLine("Enter the file location as (eg :- C:\\filelocation.xlsx)");

            string filelocation = Console.ReadLine();
            FileInfo fi = new FileInfo(filelocation);
            if (File.Exists(filelocation))
            {
                if(fi.Extension != ".xlsx" && fi.Extension != ".xls")
                {
                    Console.Clear();
                    Console.WriteLine("The File Selected Must be .xls or xlsx");
                    Console.WriteLine("");
                    goto notfound;
                }
                Console.WriteLine("File has been found");
                Console.WriteLine(" ");
                return filelocation;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("******File Not Found please try again  {0}*****", filelocation);
                Console.WriteLine(" ");
                goto notfound;
            }
        }

        private static void  ReadStudents( string filelocation)
        {
            Application excelread = new Application();
            if (excelread == null)
            {
                Console.WriteLine("Excel is not installed");
                return ;
            }
            
            Workbook excelBook = excelread.Workbooks.Open(filelocation);
            _Worksheet excelSheet = excelBook.Sheets[1];
            Range excelRange = excelSheet.UsedRange;

            int rows = excelRange.Rows.Count;
            int cols = excelRange.Columns.Count;

            //excel are not zero based
            for (int i = 2; i <= rows; i++)
            {
                string[] values = new string[9];
                for (int j = 1; j <= cols; j++)
                {
                    if (excelRange[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                    {
                        values[j] = excelRange.Cells[i, j].Value2.ToString();
                    }
                }
                AddDetailsToDB(values);
            }
            excelread.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelread);
            Console.WriteLine("Reading From Excel Successful click to compute");
        }


        private static void ShowStats()
        {
            var result = new Statistics();
            
            if (result.totalId == int.MinValue)
            {
                Console.WriteLine("No Students Added to Calculate Please Add Students & Try Again..");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The Topper Of the class is {1}({0}) with {2} marks", result.totalId, result.totalName,result.totalvalue);
                Console.WriteLine("The Topper in Biology is {1}({0}) with {2} marks", result.BiologyId, result.BiologyName, result.BiologyMax);
                Console.WriteLine("The Topper in Chemistry is {1}({0}) with {2} marks", result.ChemId, result.ChemName, result.ChemMax);
                Console.WriteLine("The Topper in Physics is {1}({0}) with {2} marks", result.PhyId, result.PhyName, result.PhyMax);
                Console.WriteLine("The Topper in Social is {1}({0}) with {2} marks", result.SocId, result.SocName, result.SocMax);
                Console.WriteLine("The Topper in Mathematics is {1}({0}) with {2} marks", result.MathsId, result.MathsName, result.MathsMax);
                Console.WriteLine("The Topper in Computers is {1}({0}) with {2} marks", result.CompId, result.CompName, result.CompMax);
                Console.ReadLine();
            
            }

        }

        private static void  AddDetailsToDB(string[] val)
        {
            List<string> subs = new List<string> { "Chemistry", "Physics", "Biology", "Social", "Mathematics", "Computers" };
            int rollno = Int32.Parse(val[2]);
            for (int i = 0; i < subs.Count; i++)
            {
                int m = Convert.ToInt32(val[i + 3]);
                DatabaseOPerations(val, rollno,subs[i], m);
                
            }
        }

        public static bool DatabaseOPerations(string[] val, int rollno, string sub ,int marks) 
        {
            DatabaseConnection db = new DatabaseConnection();
            SqlConnection connection = db.GetDbConnection();

            string strCommand = "insert into StudentData (Name , Id ,Subject , Marks ) values('" + val[1] + "','" + rollno + "','" + sub + "','" + marks +  "')";
            SqlCommand commmand = new SqlCommand();
            SqlTransaction transaction;
            transaction = connection.BeginTransaction("InsertionTransaction");
            commmand.CommandText = strCommand;
            commmand.Connection = connection;
            commmand.Transaction = transaction;

            SqlCommand check_User_Name = new SqlCommand("UserExists", connection);
            check_User_Name.CommandType = CommandType.StoredProcedure;
            SqlParameter UserID = new SqlParameter("@rollno", rollno);
            SqlParameter UserSub = new SqlParameter("@subject", sub);
            check_User_Name.Parameters.Add(UserID);
            check_User_Name.Parameters.Add(UserSub);
            check_User_Name.Transaction = transaction;
            int UserExist = (int)check_User_Name.ExecuteScalar();
            
            if (UserExist > 0)
            {
                Console.WriteLine("Data Already Exists for the student {0} {1} ", rollno, val[1]);
                transaction.Rollback();
                connection.Close();
                return false;
            }
            else
            {
                int querycheck = commmand.ExecuteNonQuery();
                if (querycheck == 1)
                {
                    transaction.Commit();
                    connection.Close();
                    return true; 
                }
                else
                {
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
        }

    }
}

