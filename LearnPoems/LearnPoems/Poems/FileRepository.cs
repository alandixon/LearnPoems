using LearnPoems.Logging;
using System.IO;
using Xamarin.Forms;

namespace LearnPoems.Poems
{
    public class FileRepository : Repository
    {
        #region Logging
        private ILog Log = DependencyService.Get<ILog>();
        private string logTag = typeof(FileRepository).FullName;
        #endregion Logging

        private string repoFilePath;
        private string[] files;

        public FileRepository(DirectoryInfo filePath)
        {
            repoFilePath = filePath.ToString();
            RefreshPoemList();
        }

        public void RefreshPoemList()
        {
            files = Directory.GetFiles(repoFilePath);

            Poems.Clear();
            foreach (string filePath in files)
            {
                Poems.Add(ReadPoemFromFile(filePath));
            }
        }

        private Poem ReadPoemFromFile(string filePath)
        {
            Poem poem = new Poem();
            try
            {
                poem.Chunks = File.ReadAllLines(filePath);
                if (poem.Chunks.Length == 0)
                {
                    throw new FileLoadException(string.Format("0 lines found in file {0}", filePath));
                }
                poem.Name = poem.Chunks[0];
            }
            catch (System.Exception ex)
            {
                Log.Error(logTag, string.Format("Couldn't read poem from file {0} because {1}", filePath, ex.Message));
            }
            return poem;
        }

        /// <summary> Try to fetch the poem identified by 'name'</summary>
        /// <param name="name"></param>
        /// <param name="poem"></param>
        /// <returns>true if successful</returns>
        public override bool TryGetPoem(string name, out Poem poem)
        {
            poem = null;
            return false;
        }
    }
}
