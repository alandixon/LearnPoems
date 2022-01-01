﻿using LearnPoems.Logging;
using LearnPoems.Text;
using LearnPoems.Version;
using System.ComponentModel;
using Xamarin.Forms;

namespace LearnPoems.Model
{
    public class Model : INotifyPropertyChanged
    {
        #region Logging
        private ILog Log = DependencyService.Get<ILog>();
        private string logTag = typeof(Model).FullName;
        #endregion Logging

        #region Versioning
        private IVersion version = DependencyService.Get<IVersion>();
        #endregion Versioning

        public Model()
        {
        }

        public string AppName
        {
            get { return App.AppName; }
        }

        /// <summary> Google version name (not version number/code), generated by the droid project from the manifest </summary>
        public string AppVersionName
        {
            get { return version.VersionName; }
        }

        public string CopyrightText
        {
            get { return HelpPageText.Copyright; }
        }

        public string CreditText
        {
            get { return HelpPageText.Credits; }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion INotifyPropertyChanged
    }
}
