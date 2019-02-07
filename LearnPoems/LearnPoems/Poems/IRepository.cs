using System.Collections.ObjectModel;

namespace LearnPoems.Poems
{
    interface IRepository
    {
        bool TryGetPoem(string name, out Poem poem);

        RepositoryType RepositoryType { get; set; }

        string Path { get; set; }

        ObservableCollection<Poem> Poems { get; set; }

        Poem SelectedPoem { get; set; }
    }
}
