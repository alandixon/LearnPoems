using LearnPoems.Pages;
using LearnPoems.Poems;
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
        public static FileRepository FileRepository { get; set; }

        public static string SystemFolderPath { get; set; }
        public static string PoemFolderPath { get; set; }
        public static string SettingsFileName = "Settings";
        public static string PoemFolderName = "Poems";

        // Pages
        public static StartPage StartPage { get; set; }
        public static ViewPoemPage ViewPoemPage { get; set; }
        public static ChoosePoemPage ChoosePoemPage { get; set; }
        public static HelpPage HelpPage { get; set; }
        public static RecentPage RecentPage { get; set; }
        public static SavePoemPage SavePoemPage { get; set; }

        public static PoemViewer PoemViewer { get; set; }

        public static readonly double GoldenRatio = 1.6180339887;

        static App()
        {
            Settings = new Settings.Settings();
            Model = new Model.Model();

            StartPage = new StartPage();
            ViewPoemPage = new ViewPoemPage();
            ChoosePoemPage = new ChoosePoemPage();
            HelpPage = new HelpPage();
            RecentPage = new RecentPage();
            SavePoemPage = new SavePoemPage();
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
