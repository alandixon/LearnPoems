using Android.App;
using LearnPoems.Version;

// https://xamarinhelp.com/xamarin-forms-dependency-injection/
[assembly: Xamarin.Forms.Dependency(typeof(LearnPoems.Droid.Version.Version))]
namespace LearnPoems.Droid.Version
{
    public class Version : IVersion
    {
        string IVersion.VersionName
        {
            get
            {
                return Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
            }
        }
    }
}