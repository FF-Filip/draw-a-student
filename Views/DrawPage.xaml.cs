using LosowanieUcznia.Models;
using System.Diagnostics;

namespace LosowanieUcznia.Views
{
    public partial class DrawPage : ContentPage
    {
        public DrawPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            AllStudents.allStudents.Clear();
            AllStudents.allStudents = Utilities.Utils.ReadStudents();
            List<Student> studentsToDraw =  AllStudents.allStudents.Where(st => st.Class == MainPage.selectedClass).ToList();
            studentsCollection.ItemsSource = studentsToDraw;

            List<int> numbersToChoose = new List<int>();
            numbersToChoose.Add(0);
            foreach (Student student in studentsToDraw)
            {
                numbersToChoose.Add(student.Number);
            }
            luckyNumber.ItemsSource = numbersToChoose;
        }

        private void LosujButton_Clicked(object sender, EventArgs e)
        {
            List<Student> studentsToDraw = AllStudents.allStudents.Where(st => st.Class == MainPage.selectedClass).ToList();

            if (studentsToDraw.Count == 0)
            {
                DisplayAlert("Brak uczniów w klasie!", "Ta klasa nie ma żadnych uczniów. Dodaj ich na stronie dodawania albo usuń klasę.", "OK");
                return;
            }

            int luckyN = 0;
            if (luckyNumber.SelectedIndex != -1)
            {
                luckyN = Int32.Parse(luckyNumber.SelectedItem.ToString());
            }

            List<Student> infiniteLoopProt  = new List<Student>();

            Random rnd = new Random();
            int index = -1;
            while(true)
            {
                if (infiniteLoopProt.Count == studentsToDraw.Count)
                {
                    break;
                }

                index = rnd.Next(0, studentsToDraw.Count);

                if (!infiniteLoopProt.Contains(studentsToDraw[index]))
                    infiniteLoopProt.Add(studentsToDraw[index]);

                if (studentsToDraw[index].DrawsSinceChosen == 0 && studentsToDraw[index].IsPresent && studentsToDraw[index].Number != luckyN)
                {
                    break;
                } else
                {
                    index = -1;
                }
                Debug.WriteLine("losowanko");
            }

            /*
            List<Student> studentsToEdit = studentsToDraw;
            foreach (Student student in studentsToDraw)
            {
                if (student.DrawsSinceChosen != 0)
                    student.DrawsSinceChosen -= 1;
            }

            if(index != -1)
            {
                studentsToDraw[index].DrawsSinceChosen = 3;
            }    

            foreach (Student student in studentsToEdit) 
            {
                AllStudents.allStudents.Remove(student);
            }

            foreach (Student student in studentsToDraw)
            {
                AllStudents.allStudents.Add(student);
            }
            */

            Utilities.Utils.SaveStudents(AllStudents.allStudents);

            if(index == -1)
            {
                OutputLabel.Text = "Brak uczniów do wylosowania";
                return;
            }

            OutputLabel.Text = studentsToDraw[index].Name + " " + studentsToDraw[index].Surname + ", klasa: " + studentsToDraw[index].Class;
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Utilities.Utils.DeleteStudents();
        }

        private async void StudentSelection_Changed(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as CollectionView).SelectedItem == null)
                return;

            Student selectedStudent = (sender as CollectionView).SelectedItem as Student;
            string choice = await DisplayActionSheet("Wybierz opcję:", "Cancel", null, "Edutyj ucznia", "Skasuj ucznia");

            switch (choice)
            {
                case "Edutyj ucznia":

                    AddStudent editPage = new AddStudent(selectedStudent);
                    await Navigation.PushAsync(editPage);
                    break;

                case "Skasuj ucznia":

                    AllStudents.allStudents.Remove(selectedStudent);
                    Utilities.Utils.SaveStudents(AllStudents.allStudents);
                    break;

                default:
                    break;
            }

            (sender as CollectionView).SelectedItem = null;
        }
    }
}