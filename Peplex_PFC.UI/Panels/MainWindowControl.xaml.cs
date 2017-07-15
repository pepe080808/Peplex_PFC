using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.UIO;

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

                    ((CoverControl) currentControl).Film = film;    

                    if (film.Cover != null)
                    {
                        var memStream = new MemoryStream();
                        memStream.Write(film.Cover, 0, film.Cover.Length);
                        memStream.Seek(0, SeekOrigin.Begin);

                        var img = new BitmapImage();
                        img.BeginInit();
                        img.StreamSource = memStream;
                        img.EndInit();

                        ((CoverControl)currentControl).Img = img;
                    }
                    else
                        ((CoverControl)currentControl).ImgCover = null;

                    ((CoverControl) currentControl).StrTitle = film.Title ?? "";
                    ((CoverControl)currentControl).StrTag = "Film";
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

                    ((CoverControl)currentControl).Serie = serie;

                    if (serie.Cover != null)
                    {
                        var memStream = new MemoryStream();
                        memStream.Write(serie.Cover, 0, serie.Cover.Length);
                        memStream.Seek(0, SeekOrigin.Begin);

                        var img = new BitmapImage();
                        img.BeginInit();
                        img.StreamSource = memStream;
                        img.EndInit();

                        ((CoverControl)currentControl).Img = img;
                    }
                    else
                        ((CoverControl)currentControl).ImgCover = null;

                    ((CoverControl)currentControl).StrTitle = serie.Title ?? "";
                    ((CoverControl)currentControl).StrTag = "Serie";
                }
            }
        }
    }
}
