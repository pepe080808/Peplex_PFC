using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Panels;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI
{
    public partial class SerieWindow
    {
        private SerieUIO _serie;
        public SerieUIO Serie
        {
            get { return _serie; }
            set { _serie = value; UpdateUI(); }
        }

        public SerieWindow()
        {
            InitializeComponent();
        }

        private void SerieWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void SerieWindowLoad(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateUI()
        {
            if (Serie.Cover != null)
            {
                var memStream = new MemoryStream();
                memStream.Write(Serie.Cover, 0, Serie.Cover.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = memStream;
                img.EndInit();

                ImgCover.Source = img;
            }
            else
                ImgCover.Source = null;

            if (Serie.Background != null)
            {
                var memStream = new MemoryStream();
                memStream.Write(Serie.Background, 0, Serie.Background.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = memStream;
                img.EndInit();

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource = img;
                Background = myBrush;
            }
            else
                ImgCover.Source = null;

            LblTitle.Content = Serie.Title;
            TxtSynopsis.Text = Serie.Synopsis;

            CalculateNoteStar();

            var strMusic = PeplexConfig.Instance.RootMainLocal + PeplexConfig.Instance.RootMusicLocal + Serie.Title + ".mp3";
            MeMusic.Source = new Uri(strMusic);
            MeMusic.Play();

            EyeControl.Value = PeplexConfig.Instance.CurrentUser.SerieSeen.Contains(_serie.Id);

            CheckSeenEpisode();

            dgEpisode.ItemsSource = _serie.Episodes;
        }

        private void CalculateNoteStar()
        {
            var note = Serie.Note;
            for (var i = 0; i < 5; i++)
            {
                var control = FindName(String.Format("NoteStar{0}", (i + 1)));
                if (control != null)
                {
                    var noteStar = (NoteControl)control;
                    if (note >= 2)
                        noteStar.Value = 2;
                    else
                        noteStar.Value = note;
                }

                note -= 2;
            }
        }

        private void CheckSeenEpisode()
        {
            foreach (var ep in _serie.Episodes)
                ep.Seen = PeplexConfig.Instance.CurrentUser.EpisodeSeen.Contains(String.Format("{0};{1};{2}", ep.SerieId, ep.SerieId, ep.Number));
        }

        private void SerieWindowClosed(object sender, EventArgs e)
        {
            MeMusic.Stop();
        }

        private void SerieWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PeplexConfig.Instance.CurrentUser.SerieSeen.Remove(_serie.Id);

            if (EyeControl.Value)
                PeplexConfig.Instance.CurrentUser.SerieSeen.Add(_serie.Id);
        }

        private void SoundControlClick(object sender, MouseButtonEventArgs e)
        {
            if (MeMusic.Tag.ToString().Equals("Play"))
            {
                MeMusic.Stop();
                MeMusic.Tag = "Stop";
                SoundControl.Value = false;
            }
            else
            {
                MeMusic.Play();
                MeMusic.Tag = "Play";
                SoundControl.Value = true;
            }
        }

        private void EyeControlClick(object sender, MouseButtonEventArgs e)
        {
            EyeControl.Value = !EyeControl.Value;
        }

        private void BtnPlayClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            var currentdata = (EpisodeUIO)btn.DataContext;

            PeplexConfig.Instance.CurrentUser.EpisodeSeen.Remove(String.Format("{0};{1};{2}", currentdata.SerieId, currentdata.SerieId, currentdata.Number));
            PeplexConfig.Instance.CurrentUser.EpisodeSeen.Add(String.Format("{0};{1};{2}", currentdata.SerieId, currentdata.SerieId, currentdata.Number));

            currentdata.Seen = true;

            var child = new MultimediaWindow {Owner = this, Title = _serie.Title, Episode = currentdata};
            child.ShowDialog();
        }

        private void BtnSeenClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var currentdata = (EpisodeUIO)btn.DataContext;

            if (currentdata.Seen)
                PeplexConfig.Instance.CurrentUser.EpisodeSeen.Remove(String.Format("{0};{1};{2}", currentdata.SerieId, currentdata.SerieId, currentdata.Number));
            else
                PeplexConfig.Instance.CurrentUser.EpisodeSeen.Add(String.Format("{0};{1};{2}", currentdata.SerieId, currentdata.SerieId, currentdata.Number));

            currentdata.Seen = !currentdata.Seen;
        }
    }
}
