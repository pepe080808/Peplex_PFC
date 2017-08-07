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
                    CompositionRoot.Instance.Resolve<IUserServiceProxy>().Update(PeplexConfig.Instance.CurrentUser);
                    Application.Current.Shutdown();
                    break;
            }
        }

        #region Load Data
        private void MainWindowLoad(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //_wc = new WaitCursor();

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataOnDoWork;
            bw.RunWorkerCompleted += LoadDataRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void LoadDataOnDoWork(object sender, DoWorkEventArgs e)
        {

            var films = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().FindAll().ToList();
            var series = CompositionRoot.Instance.Resolve<ISerieServiceProxy>().FindAll().ToList();

            e.Result = new Tuple<List<FilmUIO>,List<SerieUIO>>(films, series);
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { /*_wc.Dispose();*/ /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            var result = e.Result as Tuple<List<FilmUIO>,List<SerieUIO>>;

            if (result == null)
                return;

            Control01.Films = result.Item1.OrderByDescending(r => r.DownloadDate).Take(6).ToList();
            Control01.StrLbl01 = Translations.lblLast;
            Control01.StrLbl02 = Translations.lblFilms;
            Control02.Series = result.Item2.OrderByDescending(r => r.DownloadDate).Take(6).ToList();
            Control02.StrLbl01 = Translations.lblLast;
            Control02.StrLbl02 = Translations.lblSeries;
            Control03.Films = result.Item1.OrderByDescending(r => r.Note).Take(6).ToList();
            Control03.StrLbl01 = Translations.lblBest;
            Control03.StrLbl02 = Translations.lblFilms;
            Control04.Series = result.Item2.OrderByDescending(r => r.Note).Take(6).ToList();
            Control04.StrLbl01 = Translations.lblBest;
            Control04.StrLbl02 = Translations.lblSeries;
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

                //if (result != true)
                //    return;

                if (_menu.EnteredOption != Generic.MenuSlideCommandType.NoneCommand)
                    menuGoto(_menu.EnteredOption);
            }
        }

        private void menuGoto(Generic.MenuSlideCommandType command)
        {
            switch (command)
            {
                case Generic.MenuSlideCommandType.FilmCommand:
                    var childFilm = new MultimediaInfoWindow { Owner = this, MultimediaType = Generic.MultimediaType.FilmType};
                    childFilm.ShowDialog();
                    break;
                case Generic.MenuSlideCommandType.SerieCommand:
                    var childSerie = new MultimediaInfoWindow { Owner = this, MultimediaType =  Generic.MultimediaType.SerieType};
                    childSerie.ShowDialog();
                    break;
                case Generic.MenuSlideCommandType.SearchCommand:
                    var childSearch = new MultimediaSearchWindow { Owner = this };
                    childSearch.ShowDialog();
                    break;
                case Generic.MenuSlideCommandType.ConfigCommand:
                    var childConfig = new ConfigWindow { Owner = this };
                    childConfig.ShowDialog();
                    break;
            }
        }
    }
}
