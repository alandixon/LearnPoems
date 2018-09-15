using LearnPoems.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnPoems.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosePoemPage : ContentPage
    {
        private static ILog Log = DependencyService.Get<ILog>();
        private static string logTag = "ChoosePoemPage";

        public ChoosePoemPage()
        {
            InitializeComponent();
            // Remove Nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.Model;
        }

        protected override void OnAppearing()
        {
            App.FileRepository.RefreshPoemList();
        }

        private async void BackToStart_Clicked(object sender, System.EventArgs e)
        {
            Log.Debug(logTag, "BackToStart_Clicked()");
            await Navigation.PopAsync();
        }
    }

}