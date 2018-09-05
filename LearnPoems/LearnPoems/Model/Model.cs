using LearnPoems.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LearnPoems.Model
{
    public class Model : INotifyPropertyChanged
    {
        public Model()
        {

        }

        //private string copyrightText;
        public string CopyrightText
        {
            get { return HelpPageText.Copyright; }
            //set { copyrightText = value; NotifyPropertyChanged("CopyrightText"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
