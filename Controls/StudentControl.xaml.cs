using LosowanieUcznia.Models;
using System.Diagnostics;

namespace LosowanieUcznia.Controls;

public partial class StudentControl : ContentView
{
	public StudentControl()
	{
		InitializeComponent();
		BindingContext = new Student();
	}

    private void IsPresent_Changed(object sender, CheckedChangedEventArgs e)
    {
		Student student = BindingContext as Student;

		List<Student> students = AllStudents.allStudents;
		int index = students.IndexOf(student);
		bool isPresent = students[index].IsPresent;
        students[index].IsPresent = !isPresent;

		Debug.WriteLine(students[index].IsPresent);

		Utilities.Utils.SaveStudents(students);
    }
}