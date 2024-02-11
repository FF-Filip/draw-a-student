using LosowanieUcznia.Models;
using System.Diagnostics;

namespace LosowanieUcznia.Views
{
    public partial class DrawPage : ContentPage
    {
        List<Student> allStudentsList = new List<Student>();
        string selectedClass = String.Empty;
        
        public DrawPage()
        {
            InitializeComponent();
            allStudentsList.Clear();
            allStudentsList = Utilities.Utils.ReadItems();
            // studentsList = Utilities.Utils.LoadDummyData();
        }

        private void LosujButton_Clicked(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, allStudentsList.Count);
            OutputLabel.Text = allStudentsList[index].Name + allStudentsList[index].Surname;
        }

        private void ClassPicker_Changed(object sender, EventArgs e)
        {
            selectedClass = (sender as Picker).SelectedItem as string;
            Debug.WriteLine(selectedClass);
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