using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2{ get; set; }
        public DateTime DayOfBirthd { get; set; }
        public string Telephone { get; set; }
        public string GroupNumber { get; set; }
        public string FamilyMemberSum { get; set; }
        public string FullName => $"{Name} {Name2}";

    }
}
