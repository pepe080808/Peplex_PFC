using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public enum LoggingEventSource
    {
        Generic,
        ServiceGeneric,
        ServiceCommsSync,
        ServiceCommsOnDemand,
        ThreadsAlive,
        Wms
    }

    public class LoggingEvent
    {
        public LoggingEventSource Source { get; set; }
        public LoggingLevel Level { get; set; }
        public string Message { get; set; }
        public string ExceptionString { get; set; }
        public string StackTrace { get; set; }
        public DateTime TimeStamp { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string Tag4 { get; set; }
        public string Tag5 { get; set; }

        public LoggingEvent()
        {
            Source = LoggingEventSource.Generic;
            Level = LoggingLevel.Verbose;
            TimeStamp = DateTime.Now;
            MachineName = Environment.MachineName;
            UserName = Environment.UserName;
        }
    }
}
