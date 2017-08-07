using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class ConfigWindow
    {
        private WaitCursor _wc;

        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void ConfigWindowLoad(object sender, RoutedEventArgs e)
        {
            if (PeplexConfig.Instance.CurrentUser.Permissions != 1)
            {
                TabItemFilm.Visibility = Visibility.Collapsed;
                TabItemSerie.Visibility = Visibility.Collapsed;
            }

            LoadData();
        }

        #region Load data
        private void LoadData()
        {
            _wc = new WaitCursor();

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataOnDoWork;
            bw.RunWorkerCompleted += LoadDataRunWorkerCompleted;
            bw.RunWorkerAsync(new
            {
               Permissions = PeplexConfig.Instance.CurrentUser.Permissions
            });
        }

        private void LoadDataOnDoWork(object sender, DoWorkEventArgs e)
        {
            var o = e.Argument as dynamic;

            List<FilmUIO> films = null;
            List<SerieUIO> series = null;
            List<UserUIO> users = null;
            List<GenreUIO> genres = null;

            if (o.Permissions == 1)
            {
                films = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().FindAll().OrderBy(f => f.Title).ToList();
                series = CompositionRoot.Instance.Resolve<ISerieServiceProxy>().FindAll().OrderBy(s => s.Title).ToList();
                users = CompositionRoot.Instance.Resolve<IUserServiceProxy>().FindAll().OrderBy(u => u.NickName).ToList();
                genres = CompositionRoot.Instance.Resolve<IGenreServiceProxy>().FindAll().OrderBy(u => u.Name).ToList();
            }

            e.Result = new Tuple<List<FilmUIO>, List<SerieUIO>, List<UserUIO>, List<GenreUIO>>(films, series, users, genres);
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            var result = e.Result as Tuple<List<FilmUIO>, List<SerieUIO>, List<UserUIO>, List<GenreUIO>>;

            if (result == null)
                return;

            if (PeplexConfig.Instance.CurrentUser.Permissions == 1)
            {
                ControlUser.Users = result.Item3;
                ControlFilm.Genre = result.Item4;
                ControlFilm.Films = result.Item1;
                ControlSerie.Genre = result.Item4;
                ControlSerie.Series = result.Item2;
            }
        }
        #endregion

        private void ConfigWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

    
    }
}
