using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI
{
    public partial class MultimediaInfoWindow
    {
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

        private void CheckSeenSerie()
        {
            foreach (var se in PeplexConfig.Instance.Series)
                se.Seen = PeplexConfig.Instance.CurrentUser.SerieSeen.Contains(se.Id);
        }

        private void CheckSeenFilm()
        {
            foreach (var fi in PeplexConfig.Instance.Films)
                fi.Seen = PeplexConfig.Instance.CurrentUser.FilmSeen.Contains(fi.Id);
        }

        private void MultimediaInfoWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void MultimediainfoActivated(object sender, System.EventArgs e)
        {
            switch (StrTag)
            {
                case "Film":
                    CheckSeenFilm();
                    dgMultimedia.ItemsSource = PeplexConfig.Instance.Films;
                    break;
                case "Serie":
                    CheckSeenSerie();
                    dgMultimedia.ItemsSource = PeplexConfig.Instance.Series;
                    break;
            }
        }
    }
}
