namespace LosowanieUcznia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.AddStudent), typeof(Views.AddStudent));
            Routing.RegisterRoute(nameof(Views.DrawPage), typeof(Views.DrawPage));
        }
    }
}