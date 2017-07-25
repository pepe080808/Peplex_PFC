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
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI.Panels
{
    public partial class ConfigFilmControl
    {
        private List<FilmUIO> _films;

        public List<FilmUIO> Films
        {
            get { return _films; }
            set
            {
                _films = value;
                UpdateUI();
            }
        }

        public List<GenreUIO> Genre { get; set; }

        private BitmapImage _currentBitmapImageCover;
        private BitmapImage _currentBitmapImageBackground;

        public ConfigFilmControl()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(TxtNote, TextBoxNotePastingEventHandler);
            DataObject.AddPastingHandler(TxtDuration, TextBoxDurationPastingEventHandler);
        }

        private void ConfigFilmControlLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateUI()
        {
            if (Films != null)
            {
                // Rellenamos el ComboBox con los Title de las películas
                CbTitle.ItemsSource = Films;
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
            TxtNote.Text = Films[CbTitle.SelectedIndex].Note.ToString();
            TxtSynopsis.Text = Films[CbTitle.SelectedIndex].Synopsis;
            TxtTrailer.Text = Films[CbTitle.SelectedIndex].TrailerURL;
            var indexGenre01 = Genre.FindIndex(g => g.Name.Equals(Films[CbTitle.SelectedIndex].GenreName01, StringComparison.CurrentCultureIgnoreCase));
            CbGenre01.SelectedIndex = indexGenre01;
            var indexGenre02 = Genre.FindIndex(g => g.Name.Equals(Films[CbTitle.SelectedIndex].GenreName02, StringComparison.CurrentCultureIgnoreCase));
            CbGenre02.SelectedIndex = indexGenre02;
            TxtDuration.Text = Films[CbTitle.SelectedIndex].DurationMin.ToString();
            TxtPremierDate.SelectedDate = Films[CbTitle.SelectedIndex].PremiereDate;
            ImgCover.Source = PeplexUtils.ConvertByteArrayToBitmapImage(Films[CbTitle.SelectedIndex].Cover);
            _currentBitmapImageCover = PeplexUtils.ConvertByteArrayToBitmapImage(Films[CbTitle.SelectedIndex].Cover);
            ImgBackground.Source = PeplexUtils.ConvertByteArrayToBitmapImage(Films[CbTitle.SelectedIndex].Background);
            _currentBitmapImageBackground = PeplexUtils.ConvertByteArrayToBitmapImage(Films[CbTitle.SelectedIndex].Background);
        }

        private void BntEditClick(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (Validate().Any())
            {
                MessageBoxWindow.Show(Window.GetWindow(Parent), "AVISO", DialogIcon.Warning, new[] { DialogButton.Accept }, String.Format("Campos obligatorios: {0}", String.Join(",", Validate())));
                return;
            }

            var indexGenreId01 = Genre.FindIndex(g => g.Name.Equals(CbGenre01.SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase));
            var indexGenreId02 = Genre.FindIndex(g => g.Name.Equals(CbGenre02.SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase));

            var editedFilm = new FilmUIO
            {
                Id = Films[CbTitle.SelectedIndex].Id,
                Title = Films[CbTitle.SelectedIndex].Title,
                Subtitle = Films[CbTitle.SelectedIndex].Subtitle,
                Synopsis = TxtSynopsis.Text,
                Note = Convert.ToDecimal(TxtNote.Text),
                GenreId01 = Genre[indexGenreId01].Id,
                GenreId02 = Genre[indexGenreId02].Id,
                FormatId = Films[CbTitle.SelectedIndex].FormatId,
                TrailerURL = TxtTrailer.Text,
                DownloadDate = Films[CbTitle.SelectedIndex].PremiereDate,
                PremiereDate = TxtPremierDate.SelectedDate ?? Films[CbTitle.SelectedIndex].PremiereDate,
                DurationMin = Convert.ToInt32(TxtDuration.Text),
                Cover = PeplexUtils.ConvertBitmapImageToByteArray(_currentBitmapImageCover),
                Background= PeplexUtils.ConvertBitmapImageToByteArray(_currentBitmapImageBackground)
            };

            CompositionRoot.Instance.Resolve<IFilmServiceProxy>().Update(new ProxyContext(), editedFilm);

            MessageBoxWindow.Show(Window.GetWindow(Parent), "INFO", DialogIcon.Info, new[] { DialogButton.Accept }, "Película actualizada con éxito.");
        }

        private List<string> Validate()
        {
            var result = new List<string>();

            if (String.IsNullOrWhiteSpace(TxtNote.Text))
                result.Add("Nota");
            if (String.IsNullOrWhiteSpace(TxtSynopsis.Text))
                result.Add("Sinópsis");
            if (String.IsNullOrWhiteSpace(TxtTrailer.Text))
                result.Add("Url trailer");
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
                MessageBoxWindow.Show(Window.GetWindow(Parent), "ERROR", DialogIcon.CommError, new[] { DialogButton.Accept }, ex.Message + "\nEl archivo seleccionado no es un tipo de imagen válido");
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
                MessageBoxWindow.Show(Window.GetWindow(Parent), "ERROR", DialogIcon.CommError, new[] { DialogButton.Accept }, ex.Message + "\nEl archivo seleccionado no es un tipo de imagen válido");
            }
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
    }
}
