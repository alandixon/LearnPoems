namespace LearnPoems.Poems
{
    public class FileRepository : Repository
    {
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
