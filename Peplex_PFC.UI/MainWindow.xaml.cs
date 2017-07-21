using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class MainWindow
    {
        private WaitCursor _wc;
        private MenuSlideWindow _menu;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    //PeplexConfig.Instance.Save();
                    Application.Current.Shutdown();
                    break;
            }
        }

        #region
        private void MainWindowLoad(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _wc = new WaitCursor();

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataOnDoWork;
            bw.RunWorkerCompleted += LoadDataRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void LoadDataOnDoWork(object sender, DoWorkEventArgs e)
        {
            var proxyContext = new ProxyContext();

            var films = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().FindAll(proxyContext).ToList();
            var series = CompositionRoot.Instance.Resolve<ISerieServiceProxy>().FindAll(proxyContext).ToList();

            if (proxyContext.HasErrors)
                e.Result = proxyContext;
            else
                e.Result = new Tuple<List<FilmUIO>,List<SerieUIO>>(films, series);
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            if (e.Result is ProxyContext)
            {
                (e.Result as ProxyContext).ShowErrors(Window.GetWindow(Parent));
                return;
            }

            var result = e.Result as Tuple<List<FilmUIO>,List<SerieUIO>>;

            if (result == null)
                return;

            Control01.Films = result.Item1.OrderByDescending(r => r.DownloadDate).Take(6).ToList();
            Control01.StrLbl01 = "Últimas";
            Control01.StrLbl02 = "Películas";
            Control02.Series = result.Item2.OrderByDescending(r => r.DownloadDate).Take(6).ToList();
            Control02.StrLbl01 = "Últimas";
            Control02.StrLbl02 = "Series";
            Control03.Films = result.Item1.OrderByDescending(r => r.Note).Take(6).ToList();
            Control03.StrLbl01 = "Mejores";
            Control03.StrLbl02 = "Películas";
            Control04.Series = result.Item2.OrderByDescending(r => r.Note).Take(6).ToList();
            Control04.StrLbl01 = "Mejores";
            Control04.StrLbl02 = "Series";
        }
        #endregion

        private void GMainMouseMove(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;

            int c = Grid.GetColumn(element);

            if (c == 1)
            {
                _menu = new MenuSlideWindow { Owner = this };
                var result = _menu.ShowDialog();

                _menu.Close();

                if (result != true)
                    return;

                if (!String.IsNullOrWhiteSpace(_menu.EnteredOption))
                    menuGoto(_menu.EnteredOption);
            }
        }

        private void menuGoto(string command)
        {
            switch (command)
            {
                case "Film":
                    var childFilm = new MultimediaInfoWindow { Owner = this, StrTag = "Film"};
                    childFilm.ShowDialog();
                    break;
                case "Serie":
                    var childSerie = new MultimediaInfoWindow { Owner = this, StrTag = "Serie" };
                    childSerie.ShowDialog();
                    break;
                case "Config":
                    var childConfig = new ConfigWindow { Owner = this };
                    childConfig.ShowDialog();
                    break;
            }
        }
    }
}
