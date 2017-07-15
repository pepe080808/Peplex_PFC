using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public enum StartupResultCode
    {
        CheckUpdatedata,
        Success,
        ServiceNotAlive
    }

    public enum StartupTask
    {
        CheckServiceAvailability,
        CheckUpdatedata
    }

    public class StartupResult
    {
        public StartupResultCode ResultCode { get; set; }
        public Object Tag { get; set; }
    }

    public partial class SplashWindow
    {
        private StartupTask _currentTask;

        //private WaitIndicatorOnThread _bussy;
        private WaitCursor _wc;
        private bool _loadCompleted;

        public List<UserUIO> Users { get; set; }

        public SplashWindow()
        {
            Users = new List<UserUIO>();
            InitializeComponent();
        }

        private void SplashWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(_loadCompleted)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        var child = new LoggingWindow {Owner = this};
                        child.Show();
                        break;
                }
            }
        }

        private void SplashWindowLoad(object sender, RoutedEventArgs e)
        {
            _wc = new WaitCursor();
            RunBackgroundTasks(StartupTask.CheckServiceAvailability);
        }

        private void RunBackgroundTasks(StartupTask initialTask)
        {
            var bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork += StartupWorkerDoWork;
            bw.ProgressChanged += StartupWorkerProgressChanged;
            bw.RunWorkerCompleted += StartupWorkerCompleted;
            bw.RunWorkerAsync(initialTask);
        }

        private void StartupWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            _currentTask = (StartupTask)e.Argument;
            var worker = (BackgroundWorker)sender;
            var preReqService = CompositionRoot.Instance.Resolve<PrerequisitesCheckServiceProxy>();

            if (_currentTask == StartupTask.CheckServiceAvailability)
            {
                worker.ReportProgress(0, "Checking service...");

                var isAlive = false;
                for (var i = 0; i < 6; i++)
                {
                    try
                    {
                        preReqService.IsAlive(new ProxyContext());
                        isAlive = true;
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (!isAlive)
                {
                    e.Result = new StartupResult { ResultCode = StartupResultCode.ServiceNotAlive };
                    return;
                }

                _currentTask = StartupTask.CheckUpdatedata;
            }

            if (_currentTask == StartupTask.CheckUpdatedata)
            {
                worker.ReportProgress(0, "Updating Datas...");

                var updated = preReqService.UpdateDataBase(new ProxyContext());
                if (!updated)
                {
                    e.Result = new StartupResult { ResultCode = StartupResultCode.CheckUpdatedata };
                    return;
                }
            }

            e.Result = new StartupResult { ResultCode = StartupResultCode.Success };
        }

        private void StartupWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TextBlockFeedback.Text = (string)e.UserState;
        }

        private void StartupWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (StartupResult)e.Result;

            switch (result.ResultCode)
            {
                case StartupResultCode.ServiceNotAlive:
                    if (MessageBox.Show(this, "Service is not alive, retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                        Close();
                    else
                        RunBackgroundTasks(StartupTask.CheckServiceAvailability);
                    break;

                case StartupResultCode.CheckUpdatedata:
                    MessageBox.Show("ERROR: Al intentar actualizar los datos de la BD");
                    Close();
                    break;
            }

            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            _loadCompleted = true;
            TextBlockFeedback.Text = "¡Load completed! Pulse 'Enter' to continue.";
            EllipseFeedback.Visibility = Visibility.Hidden;
        }
    }
}
