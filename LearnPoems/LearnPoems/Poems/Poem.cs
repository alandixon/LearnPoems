using System.Diagnostics;

namespace LearnPoems.Poems
{
    [DebuggerDisplay("{Chunks.Length} chunks, Name = {Name}")]
    public class Poem
    {
        public string Name { get { return Chunks[0]; } }

        public string[] Chunks { get; set; }

        // AllChunks is a single string holding the whole poem
        private string allChunks = null;
        public string AllChunks
        {
            get
            {
                if (allChunks == null)
                {
                    allChunks = string.Join("\n", Chunks);
                }
                return allChunks;
            }
        }

        public bool IsEmpty { get; set; }

        //public string ShortName
        //{
        //    get
        //    {

        //    }
        //}

        public override string ToString()
        {
            return Name;
        }
    }
}
