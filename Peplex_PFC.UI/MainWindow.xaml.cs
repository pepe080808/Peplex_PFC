using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Peplex_PFC.UI.Config;

namespace Peplex_PFC.UI
{
    public partial class MainWindow
    {
        private MenuSlideWindow _menu;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    PeplexConfig.Instance.Save();
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void MainWindowLoad(object sender, RoutedEventArgs e)
        {
            //Owner.Hide();

            Control01.Films = PeplexConfig.Instance.Films.OrderByDescending(r => r.DownloadDate).Take(6).ToList();
            Control01.StrLbl01 = "Últimas";
            Control01.StrLbl02 = "Películas";
            Control02.Series = PeplexConfig.Instance.Series.OrderByDescending(r => r.DownloadDate).Take(6).ToList();
            Control02.StrLbl01 = "Últimas";
            Control02.StrLbl02 = "Series";
            Control03.Films = PeplexConfig.Instance.Films.OrderByDescending(r => r.Note).Take(6).ToList();
            Control03.StrLbl01 = "Mejores";
            Control03.StrLbl02 = "Películas";
            Control04.Series = PeplexConfig.Instance.Series.OrderByDescending(r => r.Note).Take(6).ToList();
            Control04.StrLbl01 = "Mejores";
            Control04.StrLbl02 = "Series";
        }

        private void GMainMouseMove(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;

            int c = Grid.GetColumn(element);
            int r = Grid.GetRow(element);

            if (c == 1)
            {
                _menu = new MenuSlideWindow { Owner = this };
                var result = _menu.ShowDialog();

                _menu.Close();

                if (result != true)
                    return;

                if (!String.IsNullOrWhiteSpace(_menu.EnteredOption))
                    menuGoto(_menu.EnteredOption);
            }
        }

        private void menuGoto(string command)
        {
            switch (command)
            {
                case "Film":
                    var childFilm = new MultimediaInfoWindow { StrTag = "Film"};
                    childFilm.ShowDialog();
                    break;
                case "Serie":
                    var childSerie = new MultimediaInfoWindow { StrTag = "Serie" };
                    childSerie.ShowDialog();
                    break;
            }
        }
    }
}
