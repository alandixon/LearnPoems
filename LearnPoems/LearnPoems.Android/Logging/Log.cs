using LearnPoems.Logging;

// https://xamarinhelp.com/xamarin-forms-dependency-injection/
[assembly: Xamarin.Forms.Dependency(typeof(LearnPoems.Droid.Logging.Log))]
namespace LearnPoems.Droid.Logging
{
    public class Log : ILog
    {
        // If requested, the prefix chars to mark the log message
        private string markString = "## ";

        // the uiLog is the log structure in the ui project
        private LearnPoems.Logging.Log uiLog;

        public Log()
        {
            uiLog = new LearnPoems.Logging.Log(); 
        }

        public void Debug(string tag, string message)
        {
            Android.Util.Log.Debug(tag, message);
            uiLog.Debug(tag, message);
        }

        public void Debug(string tag, string message, bool mark)
        {
            string newMessage = mark ? markString + message : markString;
            Debug(tag, newMessage);
        }


        public void Info(string tag, string message)
        {
            Android.Util.Log.Info(tag, message);
            uiLog.Info(tag, message);
        }

        public void Info(string tag, string message, bool mark)
        {
            string newMessage = mark ? markString + message : markString;
            Info(tag, newMessage);
        }


        public void Warn(string tag, string message)
        {
            Android.Util.Log.Warn(tag, message);
            uiLog.Warn(tag, message);

        }

        public void Warn(string tag, string message, bool mark)
        {
            string newMessage = mark ? markString + message : markString;
            Warn(tag, newMessage);
        }


        public void Error(string tag, string message)
        {
            Android.Util.Log.Error(tag, message);
            uiLog.Error(tag, message);
        }

        public void Error(string tag, string message, bool mark)
        {
            string newMessage = mark ? markString + message : markString;
            Error(tag, newMessage);
        }
    }
}