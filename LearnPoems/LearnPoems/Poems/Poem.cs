using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace LearnPoems.Poems
{
    [DebuggerDisplay("{Chunks.Length} chunks, Name = {Name}")]
    public class Poem
    {
        public string Name { get { return Chunks[0]; } }

        public List<string> Chunks { get; set; }

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

        /// <summary> Convert a string to a poem</summary>
        /// <param name="poemString"></param>
        /// <returns>poem</returns>
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
                poem.Chunks = lines.ToList();
            }

            return poem;
        }

        /// <summary>Convert a string to a list of poems. Poems delimited by BulkLoadDelimiter</summary>
        /// <param name="poemsString"></param>
        /// <returns>List of poems</returns>
        public static List<Poem> GetPoemsFromString(string poemsString)
        {
            List<Poem> poems = new List<Poem>();

            int startIdx = 0;
            int endIdx = 0;

            MatchCollection matches = Regex.Matches(poemsString, App.BulkLoadDelimiter);

            // If no BulkLoadDelimiter, there's just one poem
            if (matches.Count == 0)
            {
                poems.Add(GetPoemFromString(poemsString));
            }
            else
            {
                Poem poem = null;
                // Create a poem from the string before each delimiter
                foreach (Match match in matches)
                {
                    endIdx = match.Index;
                    poem = GetPoemFromString(poemsString.Substring(startIdx, endIdx - startIdx));
                    if (poem != null)
                    {
                        poems.Add(poem);
                    }
                    startIdx = endIdx + App.BulkLoadDelimiter.Length;
                }
                // Add the poem after the last delimiter
                poem = GetPoemFromString(poemsString.Substring(startIdx, poemsString.Length - startIdx));
                if (poem != null)
                {
                    poems.Add(poem);
                }
            }
            return poems;
        }




        ///// <summary>Convert a string to a list of poems. Poems delimited by BulkLoadDelimiter</summary>
        ///// <param name="poemsString"></param>
        ///// <returns>List of poems</returns>
        //public static List<Poem> GetPoemsFromString(string poemsString)
        //{
        //    List<Poem> poems = new List<Poem>();

        //    Poem poem = null;

        //    if (!string.IsNullOrWhiteSpace(poemsString))
        //    {
        //        // Split into lines with all possible CRLF variations
        //        string[] lines = Regex.Split(poemsString, "\r\n|\r|\n");

        //        // Remove any blank lines at the top so we get a title
        //        while (string.IsNullOrWhiteSpace(lines[0]))
        //        {
        //            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
        //        }

        //        poem = new Poem();
        //        foreach (string line in lines)
        //        {
        //            if (line.Contains(BulkLoadDelimiter))
        //            {
        //                if (poem.Chunks.Count > 0)
        //                {
        //                    poems.Add(poem);
        //                }
        //                poem = new Poem();
        //            }
        //            else
        //            {
        //                poem.Chunks.Add(line);
        //            }
        //        }
        //        if (poem.Chunks.Count > 0)
        //        {
        //            poems.Add(poem);
        //        }

        //    }
        //    return poems;
        //}

        public override string ToString()
        {
            return Name;
        }
    }
}
