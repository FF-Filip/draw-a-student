using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosowanieUcznia.Models
{
    public class Student
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Class { get; set; }
        public int DrawsSinceChosen { get; set; }
        public bool IsPresent { get; set; }

        public Student() { }

        public Student(int number, string name, string surname, string _class, int drawsSinceChosen = 0, bool isPresent = true)
        {
            Number = number;
            Name = name;
            Surname = surname;
            Class = _class;
            DrawsSinceChosen = drawsSinceChosen;
            IsPresent = isPresent;
        }
    }
}
