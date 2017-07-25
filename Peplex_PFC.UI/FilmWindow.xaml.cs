using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Panels;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;

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

        private void FilmWindowLoad(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateUI()
        {
            if (Film.Cover != null)
            {
                var memStream = new MemoryStream();
                memStream.Write(Film.Cover, 0, Film.Cover.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = memStream;
                img.EndInit();

                ImgCover.Source = img;
            }
            else
                ImgCover.Source = null;

            if (Film.Background != null)
            {
                var memStream = new MemoryStream();
                memStream.Write(Film.Background, 0, Film.Background.Length);
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

            LblTitle.Content = Film.Title;
            LblSubtitle.Content = Film.Subtitle;
            TxtSynopsis.Text = Film.Synopsis;

            //Wbtrailer.ShowYouTubeVideo(Film.TrailerURL);
            Wbtrailer.GetYoutubeVideo(Film.TrailerURL);

            CalculateNoteStar();

            var strMusic = PeplexConfig.Instance.RootMainLocal + PeplexConfig.Instance.RootMusicLocal + Film.Title + ".mp3";
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
                MessageBoxWindow.Show(this, "PLAY", DialogIcon.Info, new[] { DialogButton.Accept }, "REPRODUCIR.");
                EyeControl.Value = true;
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

            // Save el tiempo de visualizacion de la pelicula
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
