using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Peplex_PFC.UI.UIO
{
    public class UserInterfaceObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SendPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Dictionary<string, object> _foreignTableFields = new Dictionary<string, object>();
        public Dictionary<string, object> ForeignTableFields
        {
            get { return _foreignTableFields; }
        }

        protected Object ForeignTableField(string path)
        {
            object result;
            return _foreignTableFields.TryGetValue(path, out result) ? result : null;
        }
    }
}
