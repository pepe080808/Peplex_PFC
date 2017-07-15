using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Panels;

namespace Peplex_PFC.UI
{
    public partial class MenuSlideWindow
    {
        private bool _closing;

        public string EnteredOption { get; set; }

        private static readonly Dictionary<Key, string> CommandKey = new Dictionary<Key, string>
        {
            { Key.M, "Multimedia" },
            { Key.S, "Serie" },
            { Key.F, "Film" },
            { Key.C, "Config" },
            { Key.P, "Profile" },
            { Key.Escape, "ESC" }
        };


        public MenuSlideWindow()
        {
            InitializeComponent();
        }

        private void PadWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_closing)
                return;

            if (e.GetPosition(PadGrid).X < PadGrid.ActualWidth)
            {
                _closing = true;
                var animation = FindResource("PadHide") as Storyboard;
                animation.Completed += AnimationCompletedReturnFalse;
                animation.Begin();
            }
        }

        private void PadWindowPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (CommandKey.ContainsKey(e.Key))
            {
                ProcessCommand(CommandKey[e.Key]);
                e.Handled = true;
            }
        }

        private void ProcessCommand(string command)
        {
            EnteredOption = command;
            switch (command)
            {
                //case "Profile":
                //    MessageBox.Show("Profile");
                //    break;
                //case "Film":
                //    MessageBox.Show("Film");
                //    break;
                //case "Serie":
                //    MessageBox.Show("Serie");
                //    break;
                //case "Multimedia":
                //    MessageBox.Show("Multimedia");
                //    break;
                //case "Config":
                //    MessageBox.Show("Config");
                //    break;
                case "ESC":
                    {
                        if (_closing)
                            break;

                        _closing = true;

                        var animation = FindResource("PadHide") as Storyboard;
                        animation.Completed += AnimationCompletedReturnFalse;
                        animation.Begin();

                        break;
                    }
            }
            DialogResult = true;
        }

        void AnimationCompletedReturnFalse(object sender, EventArgs e)
        {
            //DialogResult = false;
        }

        private void MenuMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            ProcessCommand(sender.GetType().GetProperty("Tag").GetValue(sender).ToString());
        }

        private void PadWindowLoaded(object sender, RoutedEventArgs e)
        {
            var photo = PeplexConfig.Instance.CurrentUser.Photo;
            if (photo != null)
            {
                var memStream = new MemoryStream();
                memStream.Write(photo, 0, photo.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = memStream;
                img.EndInit();

                ProfileControl.Img = img;
            }
            else
                ProfileControl.Img = null;

            ProfileControl.StrNick = PeplexConfig.Instance.CurrentUser.NickName ?? "";
        }
    }
}
