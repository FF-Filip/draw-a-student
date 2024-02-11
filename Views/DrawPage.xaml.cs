using LosowanieUcznia.Models;
using System.Diagnostics;

namespace LosowanieUcznia.Views
{
    public partial class DrawPage : ContentPage
    {
        List<Student> allStudentsList = new List<Student>();
        
        public DrawPage()
        {
            InitializeComponent();
            allStudentsList.Clear();
            allStudentsList = Utilities.Utils.ReadItems();
        }

        private void LosujButton_Clicked(object sender, EventArgs e)
        {
            List<Student> studentsToDraw = allStudentsList.Where(st => st.Class == MainPage.selectedClass).ToList();
            Random rnd = new Random();

            int index;
            do
            {
                index = rnd.Next(0, studentsToDraw.Count);
            } while (studentsToDraw[index].DrawsSinceChosen != 0);
            
            OutputLabel.Text = studentsToDraw[index].Name + " " + studentsToDraw[index].Surname + ", klasa: " + studentsToDraw[index].Class;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Utilities.Utils.SaveItems(allStudentsList);
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Utilities.Utils.DeleteItems();
        }
    }
}