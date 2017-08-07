using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Peplex_PFC.UI.Shared
{
    public enum DialogIcon
    {
        Info,
        Warning,
        Question,
        CommError
    }

    public enum DialogButton
    {
        Yes,
        No,
        Accept,
        Cancel
    }

    public enum DialogAction
    {
        Closed,
        Yes,
        No,
        Accept,
        Cancel
    }

    public partial class MessageBoxWindow
    {
        private DialogAction _action = DialogAction.Closed;

        public DialogAction Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        public static DialogAction Show(Window parent, string title, DialogIcon icon, DialogButton[] buttons, string message)
        {
            var d = new MessageBoxWindow
            {
                Title = title,
                Owner = parent,
                PathIconInfo = { Visibility = icon == DialogIcon.Info ? Visibility.Visible : Visibility.Collapsed },
                PathIconWarning = { Visibility = icon == DialogIcon.Warning ? Visibility.Visible : Visibility.Collapsed },
                PathIconQuestion = { Visibility = icon == DialogIcon.Question ? Visibility.Visible : Visibility.Collapsed },
                PathIconCommError = { Visibility = icon == DialogIcon.CommError ? Visibility.Visible : Visibility.Collapsed },
                ButtonYes = { Visibility = buttons.Contains(DialogButton.Yes) ? Visibility.Visible : Visibility.Collapsed },
                ButtonNo = { Visibility = buttons.Contains(DialogButton.No) ? Visibility.Visible : Visibility.Collapsed },
                ButtonAccept = { Visibility = buttons.Contains(DialogButton.Accept) ? Visibility.Visible : Visibility.Collapsed },
                ButtonCancel = { Visibility = !buttons.Contains(DialogButton.Cancel) ? Visibility.Collapsed : Visibility.Visible },
                TextBlockMessage = { Text = message }
            };

            d.ShowDialog();

            return d.Action;
        }

        private void ButtonYesClick(object sender, RoutedEventArgs e)
        {
            _action = DialogAction.Yes;
            Close();
        }

        private void ButtonNoClick(object sender, RoutedEventArgs e)
        {
            _action = DialogAction.No;
            Close();
        }

        private void ButtonAcceptClick(object sender, RoutedEventArgs e)
        {
            _action = DialogAction.Accept;
            Close();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            _action = DialogAction.Cancel;
            Close();
        }

        private void WindowPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (ButtonCancel.Visibility == Visibility.Visible)
                {
                    ButtonCancelClick(null, null);
                    e.Handled = true;
                }
                else if (ButtonNo.Visibility == Visibility.Visible)
                {
                    ButtonNoClick(null, null);
                    e.Handled = true;
                }
                else if (ButtonAccept.Visibility == Visibility.Visible)
                {
                    ButtonAcceptClick(null, null);
                    e.Handled = true;
                }
            }
        }
    }
}
