using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Peplex_PFC.UI.Annotations;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Panels;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class MultimediaSearchWindow
    {
        public class CheckListCustom : UserInterfaceObject
        {
            private int _id { get; set; }
            private string _label { get; set; }
            private bool _checked { get; set; }

            public int Id
            {
                get { return _id; }
                set { _id = value; SendPropertyChanged(); }
            }

            public string Label
            {
                get { return _label; }
                set { _label = value; SendPropertyChanged(); }
            }

            public bool Checked
            {
                get { return _checked; }
                set { _checked = value; SendPropertyChanged(); }
            }
        }

        public IList<CheckListCustom> Genres { get; set; } = new List<CheckListCustom>();
        public List<int> GenresIds { get { return Genres.Where(g => g.Checked).Select(g => g.Id).ToList(); } } 

        public const int MAX_COLUMN = 6;
        private bool _loadData;
        private WaitCursor _wc;

        public class MultimediaData
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public Generic.MultimediaType MultimediaType { get; set; }
            public byte[] Cover { get; set; }
            public int GenreId01 { get; set; }
            public int GenreId02 { get; set; }
            public decimal Note { get; set; }
            public DateTime PremiereDate { get; set; }
            public DateTime DownloadDate { get; set; }
        }

        private List<MultimediaData> _filterData { get; set; }
        private List<MultimediaData> _allData { get; set; }

        public string Sort
        {
            get
            {
                if (SortTitle.IsChecked == true)
                    return "Title";
                if (SortPremiereDate.IsChecked == true)
                    return "PremiereDate";
                if (SortNote.IsChecked == true)
                    return "Note";
                if (SortDownloadDate.IsChecked == true)
                    return "DownloadDate";
                return "Title";
            }
        }

        public string SortType { get { return SortTypeAsc.IsChecked == true ? "asc" : "desc";} }

        public MultimediaSearchWindow()
        {
            _filterData = new List<MultimediaData>();
            _allData = new List<MultimediaData>();
            InitializeComponent();
        }

        private void MultimediaSearchActivated(object sender, EventArgs e)
        {
            if (!_loadData)
            {
                _loadData = true;
                LoadData();
            }
        }

        private void MultimediaSearchWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        #region Load Data
        private void LoadData()
        {
            _wc = new WaitCursor();
            GMain.IsEnabled = false;

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataOnDoWork;
            bw.RunWorkerCompleted += LoadDataRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void LoadDataOnDoWork(object sender, DoWorkEventArgs e)
        {
            var films = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().FindAll().ToList();
            var series = CompositionRoot.Instance.Resolve<ISerieServiceProxy>().FindAll().ToList();
            var genres = CompositionRoot.Instance.Resolve<IGenreServiceProxy>().FindAll().ToList();

            e.Result = new Tuple<List<FilmUIO>, List<SerieUIO>, List<GenreUIO>>(films, series, genres);
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ GMain.IsEnabled = true; }), DispatcherPriority.ApplicationIdle);

            var result = e.Result as Tuple<List<FilmUIO>, List<SerieUIO>, List<GenreUIO>>;

            if (result == null)
                return;

            _allData.Clear();

            foreach (var film in result.Item1)
            {
                _allData.Add(new MultimediaData
                {
                    Id = film.Id,
                    Title = film.Title.ToUpper(),
                    MultimediaType = Generic.MultimediaType.FilmType,
                    Cover = film.Cover,
                    GenreId01 = film.GenreId01,
                    GenreId02 = film.GenreId02,
                    Note = film.Note,
                    PremiereDate = film.PremiereDate,
                    DownloadDate = film.DownloadDate
                });
            }

            foreach (var serie in result.Item2)
            {
                _allData.Add(new MultimediaData
                {
                    Id = serie.Id,
                    Title = serie.Title.ToUpper(),
                    MultimediaType= Generic.MultimediaType.SerieType,
                    Cover = serie.Cover,
                    GenreId01 = serie.GenreId01,
                    GenreId02 = serie.GenreId02,
                    Note = serie.Note,
                    PremiereDate = serie.PremiereDate,
                    DownloadDate = serie.DownloadDate
                });
            }

            DataContext = this;

            foreach (var elem in result.Item3)
                Genres.Add(new CheckListCustom { Id = elem.Id , Label = elem.Name, Checked = true});

            FilterData("asc", "ALL", "Title");
        }
        #endregion

        private void FilterData(string order, string filter, string sort)
        {
            // Filtrar
            _filterData.Clear();

            switch (filter)
            {
                case "A":
                case "B":
                case "C":
                case "D":
                case "E":
                case "F":
                case "G":
                case "H":
                case "I":
                case "J":
                case "K":
                case "L":
                case "M":
                case "N":
                case "O":
                case "P":
                case "Q":
                case "R":
                case "S":
                case "T":
                case "U":
                case "V":
                case "W":
                case "X":
                case "Y":
                case "Z":
                    _allData.ForEach(d => {if(d.Title.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase) && BelongsGenre(d)) _filterData.Add(d);});
                    break;
                case "ALL":
                    _allData.ForEach(d => _filterData.Add(d));
                    break;
                case "0-9":
                    _allData.ForEach(d => { if (d.Title.StartsWith("0") || d.Title.StartsWith("1") || d.Title.StartsWith("2") || d.Title.StartsWith("3") || d.Title.StartsWith("4") || d.Title.StartsWith("5") || d.Title.StartsWith("6") || d.Title.StartsWith("7") || d.Title.StartsWith("8") || d.Title.StartsWith("9") && BelongsGenre(d)) _filterData.Add(d); });
                    break;
                default:
                    _allData.ForEach(d => { if (d.Title.Contains(filter.ToUpper()) && BelongsGenre(d)) _filterData.Add(d); });
                    break;
            }

            // Ordenar
            var lstTupleOrderBy = new List<Tuple<string, string>>();

            if (!String.IsNullOrWhiteSpace(sort))
                lstTupleOrderBy.Add(new Tuple<string, string>(sort, order));
            else
                lstTupleOrderBy.Add(new Tuple<string, string>("Title", "asc"));

            _filterData = new List<MultimediaData>(_filterData.MultipleSort(lstTupleOrderBy).ToList());

            UpdateInfo();
        }

        private bool BelongsGenre(MultimediaData data)
        {
            return GenresIds.Contains(data.GenreId01) || GenresIds.Contains(data.GenreId02);
        }

        private void UpdateInfo()
        {
            GSearchResult.Children.Clear();
            GSearchResult.ColumnDefinitions.Clear();
            GSearchResult.RowDefinitions.Clear();

            var f = 0;
            var c = 0;

            var plus = _filterData.Count % MAX_COLUMN != 0 ? 1 : 0; // el resto no es cero se añade un fila más al grid
            for (var i = 0; i < _filterData.Count / MAX_COLUMN + plus; i++)
                GSearchResult.RowDefinitions.Add(new RowDefinition { Height = new GridLength(160) });

            for (var i = 0; i < MAX_COLUMN; i++)
                GSearchResult.ColumnDefinitions.Add(new ColumnDefinition());

            foreach (var data in _filterData)
            {
                var cover = new CoverControl();

                cover.Id = data.Id;
                cover.Img = PeplexUtils.ConvertByteArrayToBitmapImage(data.Cover);
                cover.StrTitle = data.Title ?? "";
                cover.MultimediaType = data.MultimediaType;
                

                cover.SetValue(Grid.RowProperty, f);
                cover.SetValue(Grid.ColumnProperty, c);

                GSearchResult.Children.Add(cover);

                c++;

                if (c == MAX_COLUMN)
                {
                    c = 0;
                    f++;
                }

            }
        }

        private void KeyUpTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && GMain.IsEnabled)
            {
                    BtnOk.Focus();
            }
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            FilterData(SortType, TxtSearch.Text, Sort);
        }

        private void TextBlockClick(object sender, MouseButtonEventArgs e)
        {
            var textBlock = (TextBlock) sender;
            FilterData(SortType, textBlock.Text, Sort);
        }

        private void BtnAllClick(object sender, RoutedEventArgs e)
        {
            foreach (var elem in Genres)
                elem.Checked = true;
        }

        private void BtnNoneClick(object sender, RoutedEventArgs e)
        {
            foreach (var elem in Genres)
                elem.Checked = false;
        }
    }
}
