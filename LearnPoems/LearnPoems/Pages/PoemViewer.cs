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
        LineOnePlushelp,
        PartialPoem,
        CompletePoem
    }

    public enum ChunkType
    {
        Undefined = 0,
        Title,
        HelpText,
        CurrentLine,
        NormalPoem
    }

    public class PoemViewer
    {
        #region Logging
        private ILog Log = DependencyService.Get<ILog>();
        private string logTag = typeof(PoemViewer).FullName;
        #endregion Logging

        private static readonly double defaultLabelFontSize = (new Label()).FontSize;


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
                ChunkType chunkType = (DisplayedChunkIndex == 0) ? ChunkType.Title : ChunkType.NormalPoem;
                ExtendedLabel label = GetChunkToExtendedLabel(poem, DisplayedChunkIndex, chunkType);
                StackLayout.Children.Add(label);
                DisplayedChunkIndex++;
            }
        }

        /// <summary> The start of the normal poem-view process
        /// Shows the first line and some help text below it</summary>
        /// <param name="poem"></param>
        public void StartLoadPoem(Poem poem)
        {
            LoadTitleLine(poem);

            if (poem.IsEmpty)
            {
                // Remove clickability from Title line
                ((StackLayout.Children[0]) as ExtendedLabel).tapAction = null;
                // Set status
                Status = PoemViewerStatus.CompletePoem;
            }
            else
            {
                // Load help lines
                // Note that we don't increment DisplayedChunkIndex, because this isn't poem we're displaying, it's help text
                ExtendedLabel nextLineTap_HelpLabel = GetFormattedLabel(ChunkType.HelpText);
                nextLineTap_HelpLabel.Text = PoemViewerHelpText.NextLineTap_Help;
                StackLayout.Children.Add(nextLineTap_HelpLabel);
                nextLineTap_HelpLabel.tapAction = () => ShowNextChunk(poem, DisplayedChunkIndex, Status);

                ExtendedLabel TitleTap_HelpLabel = GetFormattedLabel(ChunkType.HelpText);
                TitleTap_HelpLabel.Text = PoemViewerHelpText.TitleTap_Help;
                StackLayout.Children.Add(TitleTap_HelpLabel);
                TitleTap_HelpLabel.tapAction = () => LoadWholePoem(poem);

                ExtendedLabel PreviousLineTap_HelpLabel = GetFormattedLabel(ChunkType.HelpText);
                PreviousLineTap_HelpLabel.Text = PoemViewerHelpText.PreviousLineTap_Help;
                StackLayout.Children.Add(PreviousLineTap_HelpLabel);

                // and define what we're doing
                Status = PoemViewerStatus.LineOnePlushelp;
            }
        }

        private void LoadTitleLine(Poem poem)
        {
            StackLayout.Children.Clear();
            DisplayedChunkIndex = 0;
            // Load the title line text
            ExtendedLabel titleLabel = GetChunkToExtendedLabel(poem, DisplayedChunkIndex, ChunkType.Title);
            // Enable clicking on it to load the whole poem
            titleLabel.tapAction = () => LoadWholePoem(poem);
            StackLayout.Children.Add(titleLabel);
            DisplayedChunkIndex++;
        }

        private void ShowNextChunk(Poem poem, int DisplayedChunkIndex, PoemViewerStatus status)
        {
            // Todo: After the last line, show a ~o~ or something similar

            // If we are displaying title and help, clear it out and show just a clean title
            if (Status == PoemViewerStatus.LineOnePlushelp)
            {
                LoadTitleLine(poem);
                Status = PoemViewerStatus.PartialPoem;
            }
            // Remove clickability and formatting from the last active line, as long as its not the title
            if (StackLayout.Children.Count > 1)
            {
                ((StackLayout.Children[StackLayout.Children.Count - 1]) as ExtendedLabel).tapAction = null;
                ((StackLayout.Children[StackLayout.Children.Count - 1]) as ExtendedLabel).FontAttributes = FontAttributes.None;
            }
            // Display the next line, pre-adding empty lines if necessary
            ExtendedLabel nextLine_Label;
            // do loop around adding empty lines without formatting or allowing tapping
            do
            {
                nextLine_Label = GetChunkToExtendedLabel(poem, DisplayedChunkIndex++, ChunkType.NormalPoem);
                StackLayout.Children.Add(nextLine_Label);
            } while (string.IsNullOrWhiteSpace(nextLine_Label.Text) && DisplayedChunkIndex < poem.Chunks.Length);

            // If this isn't the last line, format it (highlight it with bold) and allow tapping it to move on
            if (DisplayedChunkIndex < poem.Chunks.Length)
            {
                FormatLabel(nextLine_Label, ChunkType.CurrentLine);
                nextLine_Label.tapAction = () => ShowNextChunk(poem, DisplayedChunkIndex, Status);
            }
            else
            {
                // Poem is completely displayed
                Status = PoemViewerStatus.CompletePoem;
            }
            nextLine_Label.Focus();

            // Scroll this label into view if necessary
            (StackLayout.Parent as ScrollView).ScrollToAsync(nextLine_Label, ScrollToPosition.Center, true);
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

        /// <summary> Return an Extended label with formatting dependent on chunkType</summary>
        /// <param name="chunkType"></param>
        /// <returns></returns>
        private ExtendedLabel GetFormattedLabel(ChunkType chunkType)
        {
            ExtendedLabel label = new ExtendedLabel();
            FormatLabel(label, chunkType);
            return label;
        }

        private void FormatLabel(ExtendedLabel label, ChunkType chunkType)
        {
            switch (chunkType)
            {
                case ChunkType.Title:
                    label.FontAttributes = FontAttributes.Bold;
                    label.FontSize = defaultLabelFontSize * App.GoldenRatio;
                    break;
                case ChunkType.HelpText:
                    label.FontAttributes = FontAttributes.Italic;
                    break;
                case ChunkType.CurrentLine:
                    label.FontAttributes = FontAttributes.Bold;
                    break;
                case ChunkType.NormalPoem:
                default:
                    break;
            }
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
