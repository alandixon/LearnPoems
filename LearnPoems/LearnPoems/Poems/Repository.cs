using System.Collections.Generic;

namespace LearnPoems.Poems
{
    public enum RepositoryType
    {
        Undefined = 0,
        Folder,
        ZipFile
    }

    public abstract class Repository : IRepository
    {
        public Repository()
        {
            Poems = new List<Poem>();
        }

        //public Repository(RepositoryType repositoryType, string path)
        //{
        //    RepositoryType = repositoryType;
        //    Path = path;
        //}

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
    }
}
