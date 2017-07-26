using System;
using System.Windows;
using System.Windows.Input;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI
{
    public partial class MultimediaWindow
    {
        private EpisodeUIO _episode;
        public EpisodeUIO Episode
        {
            get { return _episode; }
            set { _episode = value;}
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public MultimediaWindow()
        {
            InitializeComponent();
        }

        private void MultimediaWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void MultimediaWindowLoad(object sender, RoutedEventArgs e)
        {
            var completeName = String.Format("{0}x{1} - {2}.{3}", _episode.Season, _episode.Number.ToString("d2"), _episode.Name, _episode.FormatName);
            var url = String.Format("{0}/{1}/{2}/{3}/T{4}/{5}", PeplexConfig.Instance.RootMainLocal, PeplexConfig.Instance.RootVideosLocal, PeplexConfig.Instance.RootSeriesLocal, _title, _episode.Season, completeName);

            MediaPlayer.Source = new Uri(url);
            MediaPlayer.Play();
        }
    }
}
