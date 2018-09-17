using LearnPoems.Logging;
using LearnPoems.Poems;
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

        /// <summary> A poem has been selected </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PoemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            App.FileRepository.SelectedPoem = e.Item as Poem;
            App.PoemViewer.StartLoadPoem(App.FileRepository.SelectedPoem);
            Log.Debug(logTag, "PoemListView_ItemTapped() on poem " + App.FileRepository.SelectedPoem.Name);
            await Navigation.PushAsync(App.ViewPoemPage);
        }
    }

}