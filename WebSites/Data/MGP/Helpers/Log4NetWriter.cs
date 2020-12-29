using System.Text;
using log4net;

namespace GA.BDC.Data.MGP.Helpers
{
    public class Log4NetWriter : System.IO.TextWriter
    {
        protected ILog Log { get; set; }
        public Log4NetWriter()
        {
            Log = LogManager.GetLogger(typeof(Log4NetWriter));
        }
        public override void Write(string value)
        {
            Log.DebugFormat("SQL: {0}", value);
        }
        public override void Write(char[] buffer, int index, int count)
        {
            Log.DebugFormat("SQL: {0}", new string(buffer, index, count));
        }
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
