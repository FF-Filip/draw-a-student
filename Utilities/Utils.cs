using LosowanieUcznia.Models;
using System.Diagnostics;

namespace LosowanieUcznia.Utilities
{
    internal class Utils
    {
        public static void SaveStudents(List<Student> studentsList)
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.students_data.txt");
            Debug.WriteLine(filePath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    foreach (Student student in studentsList)
                    {
                        outputFile.WriteLine(student.Number + "," + student.Name + "," + student.Surname + "," + student.Class + "," + student.DrawsSinceChosen + "," + student.IsPresent);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public static void DeleteStudents()
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.students_data.txt");

            if(File.Exists(filePath))
                File.Delete(filePath);
        }

        public static List<Student> ReadStudents()
        {
            List<Student> students = new List<Student>();

            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.students_data.txt");

            if (!File.Exists(filePath))
                SaveStudents(new List<Student>());

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
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }

            return students;
        }

        public static void SaveClasses(List<string> allClasses)
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.classes_data.txt");

            if (File.Exists(filePath))
                File.Delete(filePath);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    foreach (string className in allClasses)
                    {
                        outputFile.WriteLine(className);
                    }
                }
            }
            catch(Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public static List<string> ReadClasses()
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string filePath = Path.Combine(appDataPath, "DrawStudentApp.classes_data.txt");
            List<string> allClasses = new List<string>();

            if (!File.Exists(filePath))
                SaveClasses(new List<string>());

            try
            {
                IEnumerable<string> lines = File.ReadLines(filePath);
                foreach (string line in lines)
                {
                    allClasses.Add(line);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }

            return allClasses;
        }

        /*
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
        */
    }
}
