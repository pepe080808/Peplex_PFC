using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class MultimediaInfoWindow
    {
        private WaitCursor _wc;

        private string _strTag;
        public string StrTag
        {
            get { return _strTag; }
            set { _strTag = value; }
        }

        public MultimediaInfoWindow()
        {
            InitializeComponent();
        }

        #region Load data
        private void MultimediainfoActivated(object sender, System.EventArgs e)
        {
            switch (StrTag)
            {
                case "Film":
                    LoadDataFilms();
                    break;
                case "Serie":
                    LoadDataSeries();
                    break;
            }
        }

        private void CheckSeenSerie(List<SerieUIO> series)
        {
            foreach (var se in series)
                se.Seen = PeplexConfig.Instance.CurrentUser.SerieSeen.Contains(se.Id);
        }

        private void LoadDataFilms()
        {
            _wc = new WaitCursor();

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataFilmsOnDoWork;
            bw.RunWorkerCompleted += LoadDataFilmsRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void LoadDataFilmsOnDoWork(object sender, DoWorkEventArgs e)
        {
            var proxyContext = new ProxyContext();

            var films = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().FindAll(proxyContext).ToList();

            if (proxyContext.HasErrors)
                e.Result = proxyContext;
            else
                e.Result = new List<FilmUIO>(films);
        }

        private void LoadDataFilmsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            if (e.Result is ProxyContext)
            {
                (e.Result as ProxyContext).ShowErrors(this);
                return;
            }

            var result = e.Result as List<FilmUIO>;

            if (result == null)
                return;

            CheckSeenFilm(result);
            dgMultimedia.ItemsSource = result;
        }

        private void CheckSeenFilm(List<FilmUIO> films)
        {
            foreach (var fi in films)
                fi.Seen = PeplexConfig.Instance.CurrentUser.FilmSeen.Contains(fi.Id);
        }

        private void LoadDataSeries()
        {
            _wc = new WaitCursor();

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataSeriesOnDoWork;
            bw.RunWorkerCompleted += LoadDataSeriesRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void LoadDataSeriesOnDoWork(object sender, DoWorkEventArgs e)
        {
            var proxyContext = new ProxyContext();

            var series = CompositionRoot.Instance.Resolve<ISerieServiceProxy>().FindAll(proxyContext).ToList();

            if (proxyContext.HasErrors)
                e.Result = proxyContext;
            else
                e.Result = new List<SerieUIO>(series);
        }

        private void LoadDataSeriesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            if (e.Result is ProxyContext)
            {
                (e.Result as ProxyContext).ShowErrors(this);
                return;
            }

            var result = e.Result as List<SerieUIO>;

            if (result == null)
                return;

            CheckSeenSerie(result);
            dgMultimedia.ItemsSource = result;
        }
        #endregion

        #region Events
        private void MultimediaInfoWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void BtnPlayClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;

            switch (StrTag)
            {
                case "Film":
                    var currentdataFilm = (FilmUIO)btn.DataContext;

                    var childFilm = new FilmWindow { Film = currentdataFilm };
                    childFilm.Show();

                    break;
                case "Serie":
                    var currentdataSerie = (SerieUIO)btn.DataContext;

                    var childSerie = new SerieWindow { Serie = currentdataSerie };
                    childSerie.Show();

                    break;
            }
        }

        private void BtnSeenClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;

            switch (StrTag)
            {
                case "Film":
                    var currentdataFilm = (FilmUIO)btn.DataContext;

                    if (currentdataFilm.Seen)
                        PeplexConfig.Instance.CurrentUser.FilmSeen.Remove(currentdataFilm.Id);
                    else
                        PeplexConfig.Instance.CurrentUser.FilmSeen.Add(currentdataFilm.Id);

                    currentdataFilm.Seen = !currentdataFilm.Seen;

                    break;
                case "Serie":
                    var currentdataSerie = (SerieUIO)btn.DataContext;

                    if (currentdataSerie.Seen)
                        PeplexConfig.Instance.CurrentUser.SerieSeen.Remove(currentdataSerie.Id);
                    else
                        PeplexConfig.Instance.CurrentUser.SerieSeen.Add(currentdataSerie.Id);

                    currentdataSerie.Seen = !currentdataSerie.Seen;

                    break;
            }
        }
        #endregion

    }
}
