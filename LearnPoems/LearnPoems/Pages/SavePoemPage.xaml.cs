using LearnPoems.Logging;
using System;
using Xamarin.Forms;

namespace LearnPoems.Pages
{
    public partial class SavePoemPage : ContentPage
	{
        private static ILog Log = DependencyService.Get<ILog>();
        private static string logTag = "RecentPage";

        public SavePoemPage()
		{
			InitializeComponent();
            // Remove Nav bar
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Log.Debug(logTag, "SaveButton_ClickedAsync()");
            App.Model.SavePoems(Editor.Text);
            Editor.Text = String.Empty;
        }

        // Todo: Clear Editor and return to main screen on Save
    }
}
