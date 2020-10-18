using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace StudentReport
{

    class Program
    {

        static void Main()
        {

            List<Student> students = new List<Student>();

            Console.WriteLine("Welcome To Quaterly Report Calculator !!");
            Console.WriteLine(" ");

            ReadStudents(students);

            ShowStats(students);

            
            //ShowStatsConsole(students);
        }

        private static void ReadStudents(List<Student> students )
        {
            string fileName = $@"C:\Users\DT228274\source\repos\StudentReport\input.txt";
            

            try
            {

                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                        string[] value;
                        string data = sr.ReadLine();
                        while (data != null)
                        {
                            value = data.Split(',');
                            string name = value[0];
                            int id = Int32.Parse(value[1]);
                            List<Subjects> marks = new List<Subjects>();
                            AddSubjectmarks(marks, value);
                            students.Add(new Student
                            {
                                Name = name,
                                ID = id,
                                Marks = marks
                            });                            

                            data = sr.ReadLine();
                        }
                      
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }
      
        private static void ShowStats(List<Student> students)
        {
            string fileName = $@"C:\Users\DT228274\source\repos\StudentReport\output.txt";
            var result = new Statistics();
            foreach (Student s in students)
            {

                result.Compute(s);
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    if (result.total == int.MinValue)
                    {
                        writer.WriteLine("No Students Added to Calculate Please Add Students & Try Again..");
                        Console.WriteLine("Failed!");
                        Console.ReadLine();
                    }
                    else
                    {
                        writer.WriteLine("The Highest Total Value is : {0} by {1}", result.total, result.TotName);
                        writer.WriteLine("{1} has acehived the highest score in Biology({0})", result.Bio, result.BioName);
                        writer.WriteLine("{1} has acehived the highest score in Chemistry({0})", result.Chem, result.ChemName);
                        writer.WriteLine("{1} has acehived the highest score in Physics({0})", result.Phy, result.PhyName);
                        writer.WriteLine("{1} has acehived the highest score in Soical({0})", result.Social, result.SocName);
                        writer.WriteLine("{1} has acehived the highest score in Mathematics({0})", result.Maths, result.MathName);
                        writer.WriteLine("{1} has acehived the highest score in Computers({0})", result.Comp, result.CompName);
                        writer.Close();
                        Console.WriteLine("Success!");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
            
            
        }
        
        private static void ShowStatsConsole(List<Student> students)
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
          
            int chem = Int32.Parse(val[2]);
        
            int phy = Int32.Parse(val[3]);
          
            int bio = Int32.Parse(val[4]);
           
            int soc = Int32.Parse(val[5]);
          
            int math = Int32.Parse(val[6]);
            
            int comp = Int32.Parse(val[7]);
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
