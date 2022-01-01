using LearnPoems.Poems;
using Xamarin.Forms;

namespace LearnPoems.Pages
{
    public class PoemViewer
    {
        public PoemViewer(StackLayout stackLayout)
        {
            StackLayout = stackLayout;
        }

        /// <summary> Add a poem into the viewer chunk by chunk </summary>
        /// <param name="poem"></param>
        public void AddPoem(Poem poem)
        {
            StackLayout.Children.Clear();

            int chunkCount = 0;
            foreach (string chunk in poem.Chunks)
            {
                Label label = new Label();
                label.Text = chunk;
                if (chunkCount==0)
                {
                    label.FontAttributes = FontAttributes.Bold;
                }
                StackLayout.Children.Add(label);
                chunkCount++;
            }
        }

        public StackLayout StackLayout { get; set; }

    }
}
