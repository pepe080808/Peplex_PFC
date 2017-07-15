using System.Windows;
using System.Windows.Input;
using Peplex_PFC.UI.Config;

namespace Peplex_PFC.UI
{
    public partial class ConfigWindow
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void ConfigWindowLoad(object sender, RoutedEventArgs e)
        {
            if (PeplexConfig.Instance.CurrentUser.Permissions != 1)
            {
                TabItemFilm.Visibility = Visibility.Collapsed;
                TabItemSerie.Visibility = Visibility.Collapsed;
            }
        }

        private void ConfigWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
            }
        }

    
    }
}
