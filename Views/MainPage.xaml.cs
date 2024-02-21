using LosowanieUcznia.Models;
using System.Diagnostics;

namespace LosowanieUcznia.Views;

public partial class MainPage : ContentPage
{
    public static string selectedClass = String.Empty;

    public MainPage()
	{
		InitializeComponent();
        Debug.WriteLine("Pliki aplikacji: " + FileSystem.Current.AppDataDirectory);
	}

    protected override void OnAppearing()
    {
        Classes.allClasses = Utilities.Utils.ReadClasses();

        ClassPicker.ItemsSource = Classes.allClasses;
    }

    private void ClassPicker_Changed(object sender, EventArgs e)
    {
        selectedClass = ClassPicker.SelectedItem as string;
    }

    private async void ShowClassButton_Clicked(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Uwaga", "Proszę wybrać klasę do losowania", "Ok");
            return;
        }
        await Shell.Current.GoToAsync(nameof(DrawPage));
    }

    private async void AddStudent_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddStudent));
    }
}