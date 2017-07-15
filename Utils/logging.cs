using System;
using System.Collections.Generic;

namespace Utils
{
    public class Logging
    {
        private readonly List<ILoggingStorage> _storages;

        #region Singelton construction, Load and Save methods
        private static readonly object InstanceAccess = new Object();

        private static volatile Logging _instance;
        public static Logging Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceAccess)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logging();
                        }
                    }
                }

                return _instance;
            }
        }
        #endregion

        public Logging()
        {
            _storages = new List<ILoggingStorage>();
        }

        public void AttachStorage(ILoggingStorage storage)
        {
            _storages.Add(storage);
        }

        public void Log(LoggingEvent loggingEvent)
        {
            foreach (var storage in _storages)
                storage.Write(loggingEvent);
        }
    }
}
