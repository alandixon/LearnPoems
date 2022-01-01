using System;
using Xamarin.Forms;

namespace LearnPoems.Pages
{
    /// <summary> An extension of the standard Xamarin.Forms.Label
    /// allowing us to attach extra properties such as what to do when it's tapped
    /// </summary>
    public class ExtendedLabel : Label
    {
        public ExtendedLabel()
        {
            // Identify a tap on the label with an Action to take
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => tapAction()),
            });
        }

        // The action that should occur when this label is tapped
        public Action tapAction { get; set; }

    }
}
