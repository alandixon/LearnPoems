using LearnPoems.Logging;
using LearnPoems.Poems;
using System;
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

        private async void DeleteAllPoems_ClickedAsync(object sender, EventArgs e)
        {
            Log.Debug(logTag, "DeleteAllPoems_ClickedAsync()");

            var answer = await DisplayAlert(string.Format("There are {0} Poems", App.FileRepository.Poems.Count), "Delete all Poems?", "Yes", "No");
            if (answer)
            {
                App.FileRepository.ClearAllPoems();
            }
        }



        /// <summary> A poem has been selected </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PoemList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            App.FileRepository.SelectedPoem = e.ItemData as Poem;
            App.Settings.LastPoem = e.ItemData as Poem;
            App.Model.LastPoemName = App.Settings.LastPoem.Name;
            App.PoemViewer.StartLoadPoem(App.FileRepository.SelectedPoem);
            Log.Debug(logTag, "PoemListView_ItemTapped() on poem " + App.FileRepository.SelectedPoem.Name);
            await Navigation.PushAsync(App.ViewPoemPage);
        }

    }

}