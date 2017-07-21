using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Peplex_PFC.UI.Config;
using Utils;

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

            ProfileControl.Img = PeplexUtils.ConvertByteArrayToBitmapImage(photo);
            ProfileControl.StrNick = PeplexConfig.Instance.CurrentUser.NickName ?? "";
        }
    }
}
