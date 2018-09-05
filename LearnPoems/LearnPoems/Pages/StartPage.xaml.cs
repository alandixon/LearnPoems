using LearnPoems.Logging;
using System;
using Xamarin.Forms;

namespace LearnPoems.Pages
{
    public partial class StartPage : ContentPage
	{
        private static ILog Log = DependencyService.Get<ILog>();
        private static string logTag = "StartPage";

        public StartPage()
		{
			InitializeComponent();
            // Remove Nav bar
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async System.Threading.Tasks.Task ChooseButton_ClickedAsync(object sender, EventArgs e)
        {
            Log.Debug(logTag, "ChooseButton_ClickedAsync()");
            await Navigation.PushAsync(App.ChoosePoemPage);
        }

        private async System.Threading.Tasks.Task HelpButton_ClickedAsync(object sender, EventArgs e)
        {
            Log.Debug(logTag, "HelpButton_ClickedAsync()");
            await Navigation.PushAsync(App.HelpPage);
        }

        private async void RecentButton_ClickedAsync(object sender, EventArgs e)
        {
            Log.Debug(logTag, "RecentButton_ClickedAsync()");
            await Navigation.PushAsync(App.RecentPage);
        }
    }
}
