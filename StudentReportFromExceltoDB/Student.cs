using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReportFromExceltoDB
{
    public class Student
    {
        public string Name;

        public int ID;

        public List<Subjects> Marks = new List<Subjects>();
        
    }

}
