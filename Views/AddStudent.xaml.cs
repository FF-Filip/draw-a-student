using LosowanieUcznia.Models;
using LosowanieUcznia.Utilities;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LosowanieUcznia.Views;

public partial class AddStudent : ContentPage
{
	public Student editedStudent = null;

	public AddStudent()
	{
		InitializeComponent();
		Classes.allClasses = Utils.ReadClasses();
		ClassPicker.ItemsSource = Classes.allClasses;
	}

    public AddStudent(Student student)
    {
        InitializeComponent();
        Classes.allClasses = Utils.ReadClasses();
        ClassPicker.ItemsSource = Classes.allClasses;

		editedStudent = student;

		NumberEntry.Text = student.Number.ToString();
        NameEntry.Text = student.Name;
		SurnameEntry.Text = student.Surname;
        ClassPicker.SelectedItem = student.Class;
    }

    private async void AddNewStudent_Clicked(object sender, EventArgs e)
    {
		string newStudentClass = ClassPicker.SelectedItem.ToString();
		
		List<Student> newStudentsList = Utils.ReadStudents().Where(st => st.Class == newStudentClass).ToList();
		int newStudentNumber = Int32.Parse(NumberEntry.Text);
            
        if (newStudentsList.Where( s => s.Number == newStudentNumber).Count() > 0)
		{
			if(editedStudent == null)
			{
                await DisplayAlert("Uwaga", "Inny uczeń o takim numerku już istnieje", "OK");
                return;
            } 

			if (editedStudent != newStudentsList.Where(s => s.Number == newStudentNumber).FirstOrDefault())
			{
                await DisplayAlert("Uwaga", "Inny czeń o takim numerku już istnieje", "OK");
                return;
            }

            newStudentsList.Remove(editedStudent);
            AllStudents.allStudents.Remove(editedStudent);
            Utils.SaveStudents(AllStudents.allStudents);
        }

        Student newStudent = new Student(Int32.Parse(NumberEntry.Text), NameEntry.Text, SurnameEntry.Text, newStudentClass);
		newStudentsList = Utils.ReadStudents();

		newStudentsList.Add(newStudent);
		Utils.SaveStudents(newStudentsList.OrderBy( s => s.Number).ToList());

		await Shell.Current.GoToAsync("..");
    }

    private async void AddNewClass_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Dodaj klasę", "Podaj nazwę klasy:");

		Classes.allClasses = Utils.ReadClasses();
		if (Classes.allClasses.Contains(result))
		{
			await DisplayAlert("Uwaga", "Taka klasa już istnieje.", "OK");
		}
        Classes.allClasses.Add(result);
		Utils.SaveClasses(Classes.allClasses);

        ClassPicker.ItemsSource = Classes.allClasses;
        ClassPicker.SelectedItem = result;
    }
	
    private void TextEntry_Changed(object sender, TextChangedEventArgs e)
    {
		try
		{
            if (!Regex.IsMatch(e.NewTextValue, "^[\\p{L}\\-]+$") && e.NewTextValue != "")
            {
                (sender as Entry).Text = e.OldTextValue;
            }
        }
		catch (Exception ex)
		{
			Debug.WriteLine($"Exception occured: {ex.StackTrace}\n{ex.Message}");
		}
    }
}
