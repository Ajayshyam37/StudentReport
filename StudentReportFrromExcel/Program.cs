using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Office.Interop.Excel;

namespace StudentReportFromExcel
{

    class Program
    {
        //Add Nuget Package Excel Data Reader
        static void Main()
        {

            List<Student> students = new List<Student>();

            Console.WriteLine("Welcome To Quaterly Report Calculator !!");
            Console.WriteLine(" ");

            ReadStudents(students);

            ShowStats(students);
        }

        private static void ReadStudents(List<Student> students)
        {
            Application excelread = new Application();
            if(excelread == null)
            {
                Console.WriteLine("Excel is not installed");
                return;
            }
            Workbook excelBook = excelread.Workbooks.Open($@"C:\Users\DT228274\source\repos\StudentReport\StudentData.xlsx");
            _Worksheet excelSheet = excelBook.Sheets[1];
            Range excelRange = excelSheet.UsedRange;

            int rows = excelRange.Rows.Count;
            int cols = excelRange.Columns.Count;

            //excel are not zero based
            for(int i=2;i<=rows;i++)
            {
                string[] values =new string[9];
                for (int j=1;j<=cols;j++)
                {
                    if(excelRange[i,j] != null && excelRange.Cells[i,j].Value2 != null )
                    {
                        values[j] = excelRange.Cells[i, j].Value2.ToString();
                    }
                }
                CreateStudent(values ,students);
            }
            excelread.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelread);
            Console.WriteLine("Reading From Excel Successful click to compute");
            Console.ReadLine();
        }

        private static void CreateStudent(string[] values ,List<Student> students)
        {
            List<Subjects> marks = new List<Subjects>();
            AddSubjectmarks(marks, values);
            students.Add(new Student
            {
                Name = values[1],
                ID = Int32.Parse(values[2]),
                Marks = marks

            });
        }

        private static void ShowStats(List<Student> students)
        {
            var result = new Statistics();
            foreach (Student s in students)
            {

                result.Compute(s);
            }
            if (result.total == int.MinValue)
            {
                Console.WriteLine("No Students Added to Calculate Please Add Students & Try Again..");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The Highest Total Value is : {0} by {1}", result.total, result.TotName);
                Console.WriteLine("{1} has acehived the highest score in Biology({0})", result.Bio, result.BioName);
                Console.WriteLine("{1} has acehived the highest score in Chemistry({0})", result.Chem, result.ChemName);
                Console.WriteLine("{1} has acehived the highest score in Physics({0})", result.Phy, result.PhyName);
                Console.WriteLine("{1} has acehived the highest score in Soical({0})", result.Social, result.SocName);
                Console.WriteLine("{1} has acehived the highest score in Mathematics({0})", result.Maths, result.MathName);
                Console.WriteLine("{1} has acehived the highest score in Computers({0})", result.Comp, result.CompName);
                Console.ReadLine();
            }

        }

        private static void AddSubjectmarks(List<Subjects> marks ,string[] val)
        {
          
            int chem = Int32.Parse(val[3]);
        
            int phy = Int32.Parse(val[4]);
          
            int bio = Int32.Parse(val[5]);
           
            int soc = Int32.Parse(val[6]);
          
            int math = Int32.Parse(val[7]);
            
            int comp = Int32.Parse(val[8]);
            marks.Add(new Subjects
            {
                Chemistry = chem,
                Physics = phy,
                Biology = bio,
                Social = soc,
                Mathametics = math,
                Computers = comp
            });
        }

        
    }
}
