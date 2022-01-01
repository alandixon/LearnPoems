using LearnPoems.Logging;
using LearnPoems.Poems;
using LearnPoems.Text;
using System;
using Xamarin.Forms;

namespace LearnPoems.Pages
{
    public enum PoemViewerStatus
    {
        Undefined = 0,
        LineOnePlushelp
    }

    public enum ChunkType
    {
        Undefined = 0,
        Title,
        HelpText,
        NormalPoem
    }

    public class PoemViewer
    {
        #region Logging
        private ILog Log = DependencyService.Get<ILog>();
        private string logTag = typeof(PoemViewer).FullName;
        #endregion Logging

        public PoemViewer(StackLayout stackLayout)
        {
            StackLayout = stackLayout;
        }

        // Todo: Getting a log message: requestLayout() improperly called by ... NavigationPageRenderer
        // https://stackoverflow.com/questions/24598977/android-requestlayout-improperly-called
        // Specifically this answer: https://stackoverflow.com/questions/24598977/android-requestlayout-improperly-called/24756631#24756631

        /// <summary> Add a complete poem into the viewer</summary>
        /// <param name="poem"></param>
        public void LoadWholePoem(Poem poem)
        {
            StackLayout.Children.Clear();

            DisplayedChunkIndex = 0;
            foreach (string chunk in poem.Chunks)
            {
                ExtendedLabel label = new ExtendedLabel();
                label.Text = chunk;
                if (DisplayedChunkIndex==0)
                {
                    label.FontAttributes = FontAttributes.Bold;
                }
                StackLayout.Children.Add(label);
                DisplayedChunkIndex++;
            }
        }

        /// <summary> The start of the normal poem-view process
        /// Shows the first line and some help text below it</summary>
        /// <param name="poem"></param>
        public void StartLoadPoem(Poem poem)
        {
            StackLayout.Children.Clear();
            DisplayedChunkIndex = 0;
            // Load the title line
            StackLayout.Children.Add(GetChunkToExtendedLabel(poem, DisplayedChunkIndex, ChunkType.Title));

            // Load help lines
            // Note that we don't increment DisplayedChunkIndex, because this isn't poem we're displaying, it's help text
            ExtendedLabel helpLabel1 = GetFormattedLabel(ChunkType.HelpText);
            helpLabel1.Text = PoemViewerHelpText.NextLineTap_Help;
            StackLayout.Children.Add(helpLabel1);
            helpLabel1.tapAction = () => LoadWholePoem(poem);

            ExtendedLabel helpLabel2 = GetFormattedLabel(ChunkType.HelpText);
            helpLabel2.Text = PoemViewerHelpText.TitleTap_Help;
            StackLayout.Children.Add(helpLabel2);

            ExtendedLabel helpLabel3 = GetFormattedLabel(ChunkType.HelpText);
            helpLabel3.Text = PoemViewerHelpText.PreviousLineTap_Help;
            StackLayout.Children.Add(helpLabel3);

            // and define what we're doing
            Status = PoemViewerStatus.LineOnePlushelp;
        }

        /// <summary> Get the identified chunk and load it into a UI ExtendedLabel component </summary>
        /// <param name="poem"></param>
        /// <param name="chunkIndex"></param>
        /// <param name="chunkType"></param>
        /// <returns>The ExtendedLabel</returns>
        private ExtendedLabel GetChunkToExtendedLabel(Poem poem, int chunkIndex, ChunkType chunkType)
        {
            ExtendedLabel label = null;
            try
            {
                label = GetFormattedLabel(chunkType);
                if (poem.Chunks.Length > chunkIndex)
                {
                    label.Text = poem.Chunks[chunkIndex];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Log.Warn(logTag, string.Format("GetChunkToExtendedLabel() failed on chunkIndex={0} of {1} because {2}", chunkIndex, poem.Name, ex.Message));
            }
            return label;
        }

        private ExtendedLabel GetFormattedLabel(ChunkType chunkType)
        {
            ExtendedLabel label = new ExtendedLabel();
            switch (chunkType)
            {
                case ChunkType.Title:
                    label.FontAttributes = FontAttributes.Bold;
                    break;
                case ChunkType.HelpText:
                    label.FontAttributes = FontAttributes.Italic;
                    break;
                case ChunkType.NormalPoem:
                default:
                    break;
            }
            return label;
        }


        /// <summary> The UI (Xaml) StackLayout holding the UI ExtendedLabel components, one per dispayed chunk</summary>
        public StackLayout StackLayout { get; set; }

        /// <summary> Zero based index defining the last (furthest into the poem) chunk being displayed
        /// At startup, this is 0 and displays only the first line (chunk 0)
        /// As the poem is gradually revealed this increments until the highest value of n-1 is reached
        ///  where n is the number of chunks in the poem (including blank lines)</summary>
        public int DisplayedChunkIndex { get; set; }

        public PoemViewerStatus Status { get; set; }
    }
}
