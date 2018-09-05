using LearnPoems.Logging;
using System;
using Xamarin.Forms;

namespace LearnPoems.Pages
{
    public partial class RecentPage : ContentPage
	{
        private static ILog Log = DependencyService.Get<ILog>();
        private static string logTag = "RecentPage";

        public RecentPage()
		{
			InitializeComponent();
            // Remove Nav bar
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
