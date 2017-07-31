using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI
{
    public partial class MultimediaWindow
    {
        private readonly DispatcherTimer _controlPanelTimer;

        private FilmUIO _film;
        public FilmUIO Film
        {
            get { return _film; }
            set { _film = value; }
        }

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

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public MultimediaWindow()
        {
            InitializeComponent();
            _controlPanelTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher) { Interval = new TimeSpan(0, 0, 0, 2, 0) };
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
            string completeName = "";
            string url = "";

            if (Tag == "Episode")
            {
                completeName = String.Format("{0}x{1} - {2}.{3}", _episode.Season, _episode.Number.ToString("d2"), _episode.Name, _episode.FormatName);
                url = String.Format("{0}/{1}/{2}/{3}/T{4}/{5}", PeplexConfig.Instance.RootMainLocal, PeplexConfig.Instance.RootVideosLocal, PeplexConfig.Instance.RootSeriesLocal, _title, _episode.Season, completeName);
            }
            else
            {
                completeName = String.Format("{0} - {1}.{2}", _film.Title, _film.Subtitle, _film.FormatName);
                url = String.Format("{0}/{1}/{2}/{3}", PeplexConfig.Instance.RootMainLocal, PeplexConfig.Instance.RootVideosLocal, PeplexConfig.Instance.RootFilmsLocal, completeName);
            }

            MediaPlayer.Source = new Uri(url);
            MediaPlayer.Play();
        }

        // Play the media.

        private void BtnPlayClick(object sender, RoutedEventArgs e)
        {
            // The Play method will begin the media if it is not currently active or 
            // resume media if it is paused. This has no effect if the media is
            // already running.
            MediaPlayer.Play();

            // Initialize the MediaElement property values.
            InitializePropertyValues();
        }

        // Pause the media.
        private void BtnPauseClick(object sender, RoutedEventArgs e)
        {

            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            MediaPlayer.Pause();
        }

        // Stop the media.
        private void BtnStopClick(object sender, RoutedEventArgs e)
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            MediaPlayer.Stop();
        }

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            MediaPlayer.Volume = (double)volumeSlider.Value;
        }

        // Change the speed of the media.
        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            MediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void MediaPlayerOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void MediaPlayerEnded(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
        }

        // Jump to different parts of the media (seek to). 
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            MediaPlayer.Position = ts;
        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            MediaPlayer.Volume = (double)volumeSlider.Value;
            MediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void MouseMoveGMain(object sender, MouseEventArgs e)
        {
            Start();
        }

        private void Start()
        {
            ControlPanel.Visibility = Visibility.Visible;
            _controlPanelTimer.Tick += HandleAnimationTick;
            _controlPanelTimer.Start();
        }

        private void Stop()
        {
            _controlPanelTimer.Stop();
            _controlPanelTimer.Tick -= HandleAnimationTick;
        }

        private void HandleAnimationTick(object sender, EventArgs e)
        {
            ControlPanel.Visibility = Visibility.Collapsed;
            Stop();
        }
    }
}
