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
    [Activity(Label = "LearnPoems", Icon = "@drawable/letterp128", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region Logging
        private LearnPoems.Droid.Logging.Log Log = new Logging.Log();
        private string logTag = typeof(MainActivity).FullName;
        #endregion Logging
        protected override void OnCreate(Bundle bundle)
        {
            //Register Syncfusion license
            // Based on https://www.syncfusion.com/kb/9179/syncfusion-license-register-without-hardcoded
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("==================================##SyncfusionLicense##==================================");
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // ToDo: Log this
            Log.Error(logTag, "CurrentDomain_UnhandledException");
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs utexargs)
        {
            // ToDo: Log this
            Log.Error(logTag, "TaskScheduler_UnobservedTaskException");
        }
    }
}
