using LearnPoems.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LearnPoems
{
    public partial class App : Application
	{
        public static string AppName = "LearnPoems";

        public static Settings.Settings Settings { get; set; }
        public static Model.Model Model { get; set; }

        // Pages
        public static StartPage StartPage { get; set; }
        public static ViewPoemPage ViewPoemPage { get; set; }
        public static ChoosePoemPage ChoosePoemPage { get; set; }
        public static HelpPage HelpPage { get; set; }
        public static RecentPage RecentPage { get; set; }

        static App()
        {
            Settings = new Settings.Settings();
            Model = new Model.Model();

            StartPage = new StartPage();
            ViewPoemPage = new ViewPoemPage();
            ChoosePoemPage = new ChoosePoemPage();
            HelpPage = new HelpPage();
            RecentPage = new RecentPage();
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(StartPage);
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
