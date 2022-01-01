using System.Collections.Generic;

namespace LearnPoems.Poems
{
    interface IRepository
    {
        bool TryGetPoem(string name, out Poem poem);

        RepositoryType RepositoryType { get; set; }

        string Path { get; set; }

        List<Poem> Poems { get; set; }

    }
}
