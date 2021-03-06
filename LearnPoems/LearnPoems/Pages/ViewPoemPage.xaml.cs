using LearnPoems.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnPoems.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPoemPage : ContentPage
    {
        private static ILog Log = DependencyService.Get<ILog>();
        private static string logTag = "ViewPoemPage";

        public ViewPoemPage()
        {
            InitializeComponent();
            // Remove Nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.Model;

            // Initialise the poem viewer
            App.PoemViewer = new PoemViewer(PoemLayout);
        }

        private async void BackToStart_Clicked(object sender, System.EventArgs e)
        {
            Log.Debug(logTag, "BackToStart_Clicked()");
            await Navigation.PopAsync();
        }
    }

}