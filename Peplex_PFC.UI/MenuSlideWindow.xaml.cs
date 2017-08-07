using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class MenuSlideWindow
    {
        private bool _closing;

        public Generic.MenuSlideCommandType EnteredOption { get; set; }

        private static readonly Dictionary<Key, Generic.MenuSlideCommandType> CommandKey = new Dictionary<Key, Generic.MenuSlideCommandType>
        {
            { Key.S, Generic.MenuSlideCommandType.SerieCommand },
            { Key.F, Generic.MenuSlideCommandType.FilmCommand },
            { Key.C, Generic.MenuSlideCommandType.ConfigCommand },
            { Key.P, Generic.MenuSlideCommandType.ProfileCommand },
            { Key.Escape, Generic.MenuSlideCommandType.EscCommand }
        };


        public MenuSlideWindow()
        {
            InitializeComponent();
        }

        private void PadWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_closing)
                return;

            if (e.GetPosition(PadGrid).X < PadGrid.ActualWidth - MenuGrid.ActualWidth)
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

        private void ProcessCommand(Generic.MenuSlideCommandType command)
        {
            EnteredOption = command;
            switch (command)
            {
                case Generic.MenuSlideCommandType.EscCommand:
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
            Close();
            //DialogResult = false;
        }

        private void MenuMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var tag = sender.GetType().GetProperty("Tag").GetValue(sender).ToString();
            switch (tag)
            {
                case "Film":
                    ProcessCommand(Generic.MenuSlideCommandType.FilmCommand);
                    break;
                case "Serie":
                    ProcessCommand(Generic.MenuSlideCommandType.SerieCommand);
                    break;
                case "Search":
                    ProcessCommand(Generic.MenuSlideCommandType.SearchCommand);
                    break;
                case "Config":
                    ProcessCommand(Generic.MenuSlideCommandType.ConfigCommand);
                    break;
            }
        }

        private void PadWindowLoaded(object sender, RoutedEventArgs e)
        {
            var photo = PeplexConfig.Instance.CurrentUser.Photo;

            ProfileControl.Img = PeplexUtils.ConvertByteArrayToBitmapImage(photo);
            ProfileControl.StrNick = PeplexConfig.Instance.CurrentUser.NickName ?? "";
        }
    }
}
