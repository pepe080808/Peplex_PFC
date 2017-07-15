using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Utils;

namespace Peplex_PFC.UI.Proxies
{
    public class ProxyContextError
    {
        public Type ExceptionType { get; set; }
        public string Function { get; set; }
        public string CallStack { get; set; }
        public string Message { get; set; }
    }

    public class ProxyContext
    {
        public ObservableCollection<ProxyContextError> Errors { get; private set; }

        public bool HasErrors
        {
            get { return Errors.Any(); }
        }

        public ProxyContext()
        {
            Errors = new ObservableCollection<ProxyContextError>();
            Errors.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems.Cast<ProxyContextError>())
            {
                Logging.Instance.Log(new LoggingEvent
                {
                    Source = LoggingEventSource.ServiceGeneric,
                    Level = LoggingLevel.Error,
                    Message = item.Message,
                    ExceptionString = item.Message,
                    StackTrace = item.CallStack
                });
            }
        }

        public void Clear()
        {
            Errors.Clear();
        }

        public void ShowErrors(Window owner)
        {
            try
            {
                //var child = new ProxyErrorWindow(this) { Owner = owner };
                //child.ShowDialog();
                MessageBox.Show("ERROR");
            }
            catch
            {
                //var child = new ProxyErrorWindow(this);
                //child.ShowDialog();
                MessageBox.Show("ERROR");
            }
        }
    }
}
