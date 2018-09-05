using LearnPoems.Logging;
using System;
using Xamarin.Forms;

namespace LearnPoems.Helpers
{
    public class PageHelper
    {
        #region Logging
        private static ILog Log = DependencyService.Get<ILog>();
        private static string logTag = typeof(PageHelper).FullName;
        #endregion Logging

        public static void EnableLabelUri(Label label)
        {
            try
            {
                label.TextColor = Color.Blue;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    Device.OpenUri(new Uri(((Label)s).Text));
                };
                label.GestureRecognizers.Add(tapGestureRecognizer);
            }
            catch (Exception ex)
            {
                Log.Warn(logTag, string.Format("Couldn't click {0} because {1}", label, ex.Message ));
            }
        }
    }
}
