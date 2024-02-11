using LosowanieUcznia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LosowanieUcznia.Utilities
{
    internal class Utils
    {
        public static void SaveItems(List<Student> studentsList)
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.data.txt");
            Debug.Write(filePath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                foreach (Student student in studentsList)
                {
                    outputFile.WriteLine(student.Number + "," + student.Name + "," + student.Surname + "," + student.Class + "," + student.DrawsSinceChosen + "," + student.IsPresent);
                }
            }
        }

        public static void DeleteItems()
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.data.txt");

            if(File.Exists(filePath))
                File.Delete(filePath);
        }

        public static List<Student> ReadItems()
        {
            List<Student> students = new List<Student>();

            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.data.txt");

            try
            {
                IEnumerable<string> lines = File.ReadLines(filePath);
                foreach (string line in lines)
                {
                    string[] attributes = line.Split(',');
                    Student student = new Student(Int32.Parse(attributes[0]), attributes[1], attributes[2], attributes[3], Int32.Parse(attributes[4]), Boolean.Parse(attributes[5]));
                    students.Add(student);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return students;
        }

        public static List<Student> LoadDummyData()
        {
            List<Student> readList = new List<Student>
            {
                new Student(1, "Filip", "Flak", "4F"),
                new Student(2, "Jan", "Kowal", "4F"),
                new Student(3, "Justyna", "Kowalczyk", "4F"),
                new Student(4, "Jakub", "Kuba", "4F"),
            };

            return readList;
        }
    }
}
