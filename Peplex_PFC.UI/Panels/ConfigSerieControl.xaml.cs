using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI.Panels
{
    public partial class ConfigSerieControl
    {
        private List<SerieUIO> _series;

        public List<SerieUIO> Series
        {
            get { return _series; }
            set
            {
                _series = value;
                UpdateUI();
            }
        }

        public List<GenreUIO> Genre { get; set; }

        private BitmapImage _currentBitmapImageCover;
        private BitmapImage _currentBitmapImageBackground;

        public ConfigSerieControl()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(TxtNote, TextBoxNotePastingEventHandler);
            DataObject.AddPastingHandler(TxtDuration, TextBoxDurationPastingEventHandler);
        }

        private void ConfigSerieControlLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateUI()
        {
            if (Series != null)
            {
                // Rellenamos el ComboBox con los Title de las películas
                CbTitle.ItemsSource = Series;
                CbTitle.DisplayMemberPath = "Title";

                CbTitle.SelectedIndex = 0;

                AssignData();
            }
        }

        private void CbTitleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssignData();
        }

        private void AssignData()
        {
            TxtNote.Text = Series[CbTitle.SelectedIndex].Note.ToString();
            TxtSynopsis.Text = Series[CbTitle.SelectedIndex].Synopsis;
            var indexGenre01 = Genre.FindIndex(g => g.Name.Equals(Series[CbTitle.SelectedIndex].GenreName01, StringComparison.CurrentCultureIgnoreCase));
            CbGenre01.SelectedIndex = indexGenre01;
            var indexGenre02 = Genre.FindIndex(g => g.Name.Equals(Series[CbTitle.SelectedIndex].GenreName02, StringComparison.CurrentCultureIgnoreCase));
            CbGenre02.SelectedIndex = indexGenre02;
            TxtDuration.Text = Series[CbTitle.SelectedIndex].DurationMin.ToString();
            TxtPremierDate.SelectedDate = Series[CbTitle.SelectedIndex].PremiereDate;
            ImgCover.Source = PeplexUtils.ConvertByteArrayToBitmapImage(Series[CbTitle.SelectedIndex].Cover);
            _currentBitmapImageCover = PeplexUtils.ConvertByteArrayToBitmapImage(Series[CbTitle.SelectedIndex].Cover);
            ImgBackground.Source = PeplexUtils.ConvertByteArrayToBitmapImage(Series[CbTitle.SelectedIndex].Background);
            _currentBitmapImageBackground = PeplexUtils.ConvertByteArrayToBitmapImage(Series[CbTitle.SelectedIndex].Background);
        }

        private void TxtNotePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Convert.ToDecimal(e.Text);
            }
            catch
            {
                e.Handled = true;
            }
        }

        private void TextBoxNotePastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            string clipboard = e.DataObject.GetData(typeof(string)) as string;
            try
            {
                Convert.ToDecimal(clipboard);
            }
            catch
            {
                e.CancelCommand();
                e.Handled = true;
            }
        }

        private void TxtDurationPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Convert.ToInt32(e.Text);
            }
            catch
            {
                e.Handled = true;
            }
        }

        private void TextBoxDurationPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            string clipboard = e.DataObject.GetData(typeof(string)) as string;
            try
            {
                Convert.ToInt32(clipboard);
            }
            catch
            {
                e.CancelCommand();
                e.Handled = true;
            }
        }

        private void BntEditClick(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (Validate().Any())
            {
                MessageBox.Show(String.Format("Campos obligatorios: {0}", String.Join(",", Validate())), "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var indexGenreId01 = Genre.FindIndex(g => g.Name.Equals(CbGenre01.SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase));
            var indexGenreId02 = Genre.FindIndex(g => g.Name.Equals(CbGenre02.SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase));

            var editedFilm = new FilmUIO
            {
                Id = Series[CbTitle.SelectedIndex].Id,
                Title = Series[CbTitle.SelectedIndex].Title,
                Synopsis = TxtSynopsis.Text,
                Note = Convert.ToDecimal(TxtNote.Text),
                GenreId01 = Genre[indexGenreId01].Id,
                GenreId02 = Genre[indexGenreId02].Id,
                DownloadDate = Series[CbTitle.SelectedIndex].PremiereDate,
                PremiereDate = TxtPremierDate.SelectedDate ?? Series[CbTitle.SelectedIndex].PremiereDate,
                DurationMin = Convert.ToInt32(TxtDuration.Text),
                Cover = PeplexUtils.ConvertBitmapImageToByteArray(_currentBitmapImageCover),
                Background = PeplexUtils.ConvertBitmapImageToByteArray(_currentBitmapImageBackground)
            };

            CompositionRoot.Instance.Resolve<IFilmServiceProxy>().Update(new ProxyContext(), editedFilm);

            MessageBox.Show("Película actualizada con éxito.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<string> Validate()
        {
            var result = new List<string>();

            if (String.IsNullOrWhiteSpace(TxtNote.Text))
                result.Add("Nota");
            if (String.IsNullOrWhiteSpace(TxtSynopsis.Text))
                result.Add("Sinópsis");
            if (String.IsNullOrWhiteSpace(TxtDuration.Text))
                result.Add("Duración");

            return result;
        }

        private void ImgCoverClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog { Filter = "PNG|*.png JPG|*jpg", Title = "Seleccíone imagen para la carátula", FilterIndex = 1 };
                if (dialog.ShowDialog() != true)
                    return;

                string strImagen = dialog.FileName;
                var bitmapImage = new BitmapImage(new Uri(strImagen));

                ImgCover.Source = bitmapImage;
                _currentBitmapImageCover = bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl archivo seleccionado no es un tipo de imagen válido");
            }
        }

        private void ImgBackgroundClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog { Filter = "PNG|*.png JPG|*jpg", Title = "Seleccíone imagen de fondo", FilterIndex = 1 };
                if (dialog.ShowDialog() != true)
                    return;

                string strImagen = dialog.FileName;
                var bitmapImage = new BitmapImage(new Uri(strImagen));

                ImgBackground.Source = bitmapImage;
                _currentBitmapImageBackground = bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl archivo seleccionado no es un tipo de imagen válido");
            }

        }
    }
}
