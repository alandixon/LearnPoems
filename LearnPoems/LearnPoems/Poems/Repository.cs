using System.Collections.Generic;
using System.ComponentModel;

namespace LearnPoems.Poems
{
    public enum RepositoryType
    {
        Undefined = 0,
        Folder,
        ZipFile
    }

    public abstract class Repository : IRepository, INotifyPropertyChanged
    {
        public Repository()
        {
            Poems = new List<Poem>();
        }

        /// <summary> Try to fetch the poem identified by 'name'</summary>
        /// <param name="name"></param>
        /// <param name="poem"></param>
        /// <returns>true if successful</returns>
        public virtual bool TryGetPoem(string name, out Poem poem)
        {
            poem = null;
            return false;
        }

        public RepositoryType RepositoryType { get; set; }

        public string Path { get; set; }

        public List<Poem> Poems { get; set; }

        private Poem selectedPoem;
        public Poem SelectedPoem
        {
            get { return selectedPoem; }
            set
            {
                selectedPoem = value;
                NotifyPropertyChanged("SelectedPoem");
            }
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
