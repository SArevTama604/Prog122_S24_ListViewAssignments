using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog122_S24_ListViewAssignments
{
    public class Student
    {
        public string Name { get; set; }
        public double Grade1 { get; set; }
        public double Grade2 { get; set; }

        // Method to calculate the average grade
        public double GetAverageGrade()
        {
            return (Grade1 + Grade2) / 2;
        }
    }
}