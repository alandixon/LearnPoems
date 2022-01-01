using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace LearnPoems.Droid
{
    [Activity(Label = "LearnPoems", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;   

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs utexargs)
        {
            // Todo: Try https://forums.xamarin.com/discussion/91543/catch-all-exceptions-to-avoid-crash
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", utexargs.Exception);
            //newExc.ToLogUnhandledException();
        }
    }
}

