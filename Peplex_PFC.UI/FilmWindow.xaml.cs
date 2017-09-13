using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Panels;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class FilmWindow
    {
        private FilmUIO _film;
        public FilmUIO Film
        {
            get { return _film; }
            set { _film = value;  UpdateUI();}
        }

        public FilmWindow()
        {
            InitializeComponent();
        }

        private void FilmWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void UpdateUI()
        {
            ImgCover.Source = PeplexUtils.ConvertByteArrayToBitmapImage(Film.Cover);

            var img = PeplexUtils.ConvertByteArrayToBitmapImage(Film.Background);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = img;
            Background = myBrush;

            LblTitle.Content = Film.Title;
            LblSubtitle.Content = Film.Subtitle;
            TxtSynopsis.Text = Film.Synopsis;

            Wbtrailer.GetYoutubeVideo(Film.TrailerURL);

            CalculateNoteStar();

            var strMusic = (PeplexConfig.Instance.RootLocal ? "" : "http://") + (PeplexConfig.Instance.RootLocal ? PeplexConfig.Instance.RootMainLocal : PeplexConfig.Instance.ServiceAddress) + "/" + PeplexConfig.Instance.RootMusicLocal + Film.Title.Replace(" ", PeplexConfig.Instance.RootLocal ? " " : "%20") + ".mp3";
            MeMusic.Source = new Uri(strMusic);
            MeMusic.Play();

            EyeControl.Value = PeplexConfig.Instance.CurrentUser.FilmSeen.Contains(_film.Id);
        }

        private void CalculateNoteStar()
        {
            var note = Film.Note;
            for (var i = 0; i < 5; i++)
            {
                var control = FindName(String.Format("NoteStar{0}", (i + 1)));
                if (control != null)
                {
                    var noteStar = (NoteControl) control;
                    if (note >= 2)
                        noteStar.Value = 2;
                    else
                        noteStar.Value = note;
                }

                note -= 2;
            }
        }

        private void ImgCoverMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                PeplexConfig.Instance.CurrentUser.FilmSeen.Remove(_film.Id);
                PeplexConfig.Instance.CurrentUser.FilmSeen.Add(_film.Id);

                _film.Seen = true;

                MeMusic.Stop();
                MeMusic.Tag = "Stop";
                SoundControl.Value = false;

                var child = new MultimediaWindow { Owner = this, MultimediaType = Generic.MultimediaType.FilmType, Title = _film.Title, Film = _film };
                child.ShowDialog();
            }
        }

        private void FilmWindowClosed(object sender, EventArgs e)
        {
            MeMusic.Stop();
            Wbtrailer.NavigateToString("https://google.es");
        }

        private void FilmWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PeplexConfig.Instance.CurrentUser.FilmSeen.Remove(_film.Id);

            if(EyeControl.Value)
                PeplexConfig.Instance.CurrentUser.FilmSeen.Add(_film.Id);
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
    }
}
