using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

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

        public string ShortName
        {
            get
            {
                return Helpers.StringHelper.RestrictStringLength(Name, 32);
            }
        }

        public static Poem GetPoemFromString(string poemString)
        {
            Poem poem = null;

            if (!string.IsNullOrWhiteSpace(poemString))
            {
                // Split into lines with all possible CRLF variations
                string[] lines = Regex.Split(poemString, "\r\n|\r|\n");

                // Remove any blank lines at the top so we get a title
                while (string.IsNullOrWhiteSpace(lines[0]))
                {
                    lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
                }

                poem = new Poem();
                poem.Chunks = lines;
            }

            return poem;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
