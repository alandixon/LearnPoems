using LearnPoems.Logging;
using LearnPoems.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void ClearAllPoems()
        {
            Poems.Clear();
            foreach (string filePath in files)
            {
                File.Delete(filePath);
            }
        }

        private Poem ReadPoemFromFile(string filePath)
        {
            Poem poem = new Poem();
            try
            {
                poem.Chunks = File.ReadAllLines(filePath).ToList();
                if (poem.Chunks.Count == 0)
                {
                    poem.Chunks = new List<string> { MiscText.EmptyPoemText };
                    poem.IsEmpty = true;
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(logTag, string.Format("Couldn't read poem from file {0} because {1}", filePath, ex.Message));
            }
            return poem;
        }

        public void SavePoemsToFiles(List<Poem> poems)
        {
            foreach (var poem in poems)
            {
                SavePoemToFile(poem);
            }
        }

        private void SavePoemToFile(Poem poem)
        {
            // Todo: Check whether file name is already used and postfix with time based hash if needed to prevent duplication.

            string fullPath = System.IO.Path.Combine(App.PoemFolderPath, poem.Name);
            File.WriteAllLines(fullPath, poem.Chunks);
            RefreshPoemList();
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
