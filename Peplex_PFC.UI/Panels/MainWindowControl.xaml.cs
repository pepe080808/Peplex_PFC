using System;
using System.Collections.Generic;
using System.Linq;
using Peplex_PFC.UI.UIO;
using Utils;

namespace Peplex_PFC.UI.Panels
{
    public partial class MainWindowControl
    {
        private string _strLbl01;
        public string StrLbl01 {
            get { return _strLbl01; }
            set { _strLbl01 = value; Lbl01.Content = _strLbl01; }
        }

        private string _strLbl02;
        public string StrLbl02
        {
            get { return _strLbl02; }
            set { _strLbl02 = value; Lbl02.Content = _strLbl02; }
        }

        private List<FilmUIO> _films;
        public List<FilmUIO> Films
        {
            get { return _films; }
            set { _films = value; UpdateFilms(); }
        }

        private List<SerieUIO> _series;
        public List<SerieUIO> Series
        {
            get { return _series; }
            set { _series = value; UpdateSeries(); }
        }

        public MainWindowControl()
        {
            _films = new List<FilmUIO>();
            _series = new List<SerieUIO>();

            InitializeComponent();
        }

        private void MainWindowControlLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void UpdateFilms()
        {
            if(Films.Any())
            {
                var i = 1;
                foreach (var film in Films)
                {
                    var currentControl = FindName(String.Format("CoverControl{0}", i.ToString("d2")));

                    i++;

                    if (currentControl == null) continue;

                    ((CoverControl)currentControl).Id = film.Id;
                    ((CoverControl) currentControl).Img = PeplexUtils.ConvertByteArrayToBitmapImage(film.Cover);
                    ((CoverControl) currentControl).StrTitle = film.Title ?? "";
                    ((CoverControl)currentControl).MultimediaType = Generic.MultimediaType.FilmType;
                }
            }
        }

        private void UpdateSeries()
        {
            if (Series.Any())
            {
                var i = 1;
                foreach (var serie in Series)
                {
                    var currentControl = FindName(String.Format("CoverControl{0}", i.ToString("d2")));

                    i++;

                    if (currentControl == null) continue;


                    ((CoverControl)currentControl).Id = serie.Id;
                    ((CoverControl)currentControl).Img = PeplexUtils.ConvertByteArrayToBitmapImage(serie.Cover);
                    ((CoverControl)currentControl).StrTitle = serie.Title ?? "";
                    ((CoverControl)currentControl).MultimediaType = Generic.MultimediaType.SerieType;
                }
            }
        }
    }
}
