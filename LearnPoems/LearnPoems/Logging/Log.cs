using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearnPoems.Logging
{
    public enum LogType
    {
        Undefined = 0,
        Debug,
        Info,
        Warning,
        Error
    }

    public class Log : ILog
    {
        public void Debug(string tag, string message)
        {
            //throw new NotImplementedException();
        }

        public void Debug(string tag, string message, bool mark)
        {
            //throw new NotImplementedException();
        }

        public void Error(string tag, string message)
        {
            //throw new NotImplementedException();
        }

        public void Error(string tag, string message, bool mark)
        {
            //throw new NotImplementedException();
        }

        public void Info(string tag, string message)
        {
            //throw new NotImplementedException();
        }

        public void Info(string tag, string message, bool mark)
        {
            //throw new NotImplementedException();
        }

        public void Warn(string tag, string message)
        {
            //throw new NotImplementedException();
        }

        public void Warn(string tag, string message, bool mark)
        {
            //throw new NotImplementedException();
        }
    }
}
