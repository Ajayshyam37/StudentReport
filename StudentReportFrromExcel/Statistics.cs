using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace StudentReportFromExcel
{
    class Statistics
    {
        public int total;
        public string TotName;
        public int Bio;
        public string BioName;
        public int Chem;
        public string ChemName;
        public int Phy;
        public string PhyName;
        public int Social;
        public string SocName;
        public int Maths;
        public string MathName;
        public int Comp;
        public string CompName;

        public void Compute(Student s)
        {
            total = ComputeTotal(s);
            ComputeSub(s);
        }

        private void ComputeSub(Student s)
        {
            if(Bio < s.Marks[0].Biology)
            {
                Bio = s.Marks[0].Biology;
                BioName = s.Name;
            }
            if (Chem < s.Marks[0].Chemistry)
            {
                Chem = s.Marks[0].Chemistry;
                ChemName = s.Name;

            }
            if (Phy < s.Marks[0].Physics)
            {
                Phy = s.Marks[0].Physics;
                PhyName = s.Name;
            }
            if (Social < s.Marks[0].Social)
            {
                Social = s.Marks[0].Social;
                SocName = s.Name;
            }
            if (Maths < s.Marks[0].Mathametics)
            {
                Maths = s.Marks[0].Mathametics;
                MathName = s.Name;
            }
            if (Comp < s.Marks[0].Computers)
            {
                Comp = s.Marks[0].Computers;
                CompName = s.Name;
            }
        }

        private int ComputeTotal(Student s)
        {
            int temp1 = 0;
            temp1 += s.Marks[0].Biology;
            temp1 += s.Marks[0].Chemistry;
            temp1 += s.Marks[0].Computers;
            temp1 += s.Marks[0].Mathametics;
            temp1 += s.Marks[0].Physics;
            temp1 += s.Marks[0].Social;
            if (temp1 > total)
            {
                total = temp1;
                TotName = s.Name;
            }

            return total;
        }

        public Statistics()
        {
            total = int.MinValue;
        }

    }
}
