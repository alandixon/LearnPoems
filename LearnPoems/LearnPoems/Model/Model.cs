﻿using LearnPoems.Logging;
using LearnPoems.Poems;
using LearnPoems.Text;
using LearnPoems.Version;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
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
            Initialise();
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


        public Repository Repository
        {
            get { return App.FileRepository; }
        }

        public Settings.Settings Settings
        {
            get { return App.Settings; }
        }

        public string lastPoemName;
        public string LastPoemName
        {
            get { return lastPoemName; }
            set
            {
                lastPoemName = value;
                NotifyPropertyChanged(LastPoemName);
            }
        }

        public string PoemFolder
        {
            get { return App.PoemFolderPath; }
            set
            {
                App.PoemFolderPath = value;
                NotifyPropertyChanged(PoemFolder);
            }
        }


        private void Initialise()
        {
            try
            {
                // Get system folder
                // ToDo: This did crash initially, and then magically stopped. It's worth keeping an eye on what happens with a clean install
                App.SystemFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);  // /data/user/0/org.alandixon.LearnPoems/files/.config

                // Get settings
                App.Settings = LearnPoems.Settings.Settings.GetSettings(Path.Combine(App.SystemFolderPath, App.SettingsFileName));

                // Get poem folder
                //App.PoemFolderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), App.PoemFolderName);  // /data/user/0/org.alandixon.LearnPoems/files/.local/share/Poems
                App.PoemFolderPath = Path.Combine(FileSystem.AppDataDirectory, App.PoemFolderName);    // /data/user/0/org.alandixon.LearnPoems/files/Poems  

                DirectoryInfo poemFolder = new DirectoryInfo(App.PoemFolderPath);
                // Determine whether the directory exists.
                if (!poemFolder.Exists)
                {
                    Log.Info(logTag, "Creating new poemFolder");
                    poemFolder.Create();
                }

                // Set repository (but only fetch poem list as required)
                App.FileRepository = new FileRepository(poemFolder);
            }
            catch (System.Exception ex)
            {
                Log.Error(logTag, string.Format("Initialise() failed: {0}", ex.Message));
            }
        }

        public void SavePoem(string poemString)
        {
            Poem NewPoem = Poem.GetPoemFromString(poemString);
            App.FileRepository.SavePoemToFile(NewPoem);
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
