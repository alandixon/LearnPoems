using System.Diagnostics;

namespace LearnPoems.Poems
{
    [DebuggerDisplay("{Chunks.Length} chunks, Name = {Name}")]
    public class Poem
    {
        public string Name { get { return Chunks[0]; } }

        public string[] Chunks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
