using LosowanieUcznia.Models;

namespace LosowanieUcznia.Views;

public partial class MainPage : ContentPage
{
    public static string selectedClass = String.Empty;

    public MainPage()
	{
		InitializeComponent();
        List<Student> studentsList = Utilities.Utils.ReadItems();
        List<string> classes = new List<string>();

        foreach (Student student in studentsList)
        {
            if(!classes.Contains(student.Class))
            {
                classes.Add(student.Class);
            }
        }
        ClassPicker.ItemsSource = classes;
	}

    private void ClassPicker_Changed(object sender, EventArgs e)
    {
        selectedClass = ClassPicker.SelectedItem as string;
    }

    private async void ShowClassButton_Clicked(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Uwaga", "Proszê wybraæ klasê do losowania", "Ok");
            return;
        }
        await Navigation.PushAsync(new DrawPage());
    }
}