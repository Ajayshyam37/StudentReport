using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReport
{

    class Program
    {

        static void Main()
        {

            List<Student> students = new List<Student>();

            Console.WriteLine("Welcome To Quaterly Report Calculator !!");
            Console.WriteLine(" ");

            Addstudents(students);

            ShowStats(students);
        }

        private static void Addstudents(List<Student> students)
        {
            string choice = "y";
            do
            {
                Console.WriteLine("Add a student y/N ?");
                choice = Validateoption();

                if (choice == "N")
                {
                    break;
                }
                else
                {
                    Console.Clear();

                    Console.WriteLine("Enter Name : ");
                    String name = Console.ReadLine();

                    Console.WriteLine("Enter ID (100-199)");
                    int id = FindId();

                    List<Subjects> marks = new List<Subjects>();
                    AddSubjectmarks(marks);

                    students.Add(new Student
                    {
                        Name = name,
                        ID = id,
                        Marks = marks
                    });
                }
            } while (choice == "y");
        }

        private static string Validateoption()
        {
            string choice;
            do
            {
                choice = Console.ReadLine();
                if (choice != "y" && choice != "N")
                {
                    Console.WriteLine("Invalid Option!!!!");
                    Console.WriteLine("Add a student y/N ?");
                }
            } while (choice != "y" && choice != "N");
            return choice;
        }

        private static int FindId()
        {
            int id = 0;
            while (true)
            {
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id > 100 && id < 200)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Id Should be in the range(100 -199)");
                    }

                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input String was not valid enter again..");
                }
            }

            return id;
        }

        private static void ShowStats(List<Student> students)
        {
            var result = new Statistics();
            foreach (Student s in students)
            {

                result.Compute(s);
            }
            if(result.total == int.MinValue)
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

        private static void AddSubjectmarks(List<Subjects> marks)
        {
            Console.WriteLine("Marks Secured in Chemistry?");
            int chem = AddMarks();
            Console.WriteLine("Marks Secured in Physics?");
            int phy = AddMarks();
            Console.WriteLine("Marks Secured in Biology?");
            int bio = AddMarks();
            Console.WriteLine("Marks Secured in Social?");
            int soc = AddMarks();
            Console.WriteLine("Marks Secured in Mathametics?");
            int math = AddMarks();
            Console.WriteLine("Marks Secured in Computers?");
            int comp = AddMarks();
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

        private static int AddMarks()
        {
            while (true)
            {
                try
                {
                    int m = Convert.ToInt32(Console.ReadLine());

                    if (m <= 100 && m >= 1)
                    {
                        return m;

                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input String was not valid enter again..");
                }
            }
        }
    }
}
