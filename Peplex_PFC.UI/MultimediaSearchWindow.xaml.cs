using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class MultimediaSearchWindow
    {
        private WaitCursor _wc;

        public class MultimediaData
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Tag { get; set; }
            public byte[] Cover { get; set; }
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
            LoadData();
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
            var proxyContext = new ProxyContext();

            var films = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().FindAll(proxyContext).ToList();
            var series = CompositionRoot.Instance.Resolve<ISerieServiceProxy>().FindAll(proxyContext).ToList();

            if (proxyContext.HasErrors)
                e.Result = proxyContext;
            else
                e.Result = new Tuple<List<FilmUIO>, List<SerieUIO>>(films, series);
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ GMain.IsEnabled = true; }), DispatcherPriority.ApplicationIdle);

            if (e.Result is ProxyContext)
            {
                (e.Result as ProxyContext).ShowErrors(this);
                return;
            }

            var result = e.Result as Tuple<List<FilmUIO>,List<SerieUIO>>;

            if (result == null)
                return;

            foreach (var film in result.Item1)
            {
                _allData.Add(new MultimediaData
                {
                    Id = film.Id,
                    Title = film.Title.ToUpper(),
                    Tag = "Film",
                    Cover = film.Cover
                });
            }

            foreach (var serie in result.Item2)
            {
                _allData.Add(new MultimediaData
                {
                    Id = serie.Id,
                    Title = serie.Title.ToUpper(),
                    Tag = "Serie",
                    Cover = serie.Cover
                });
            }

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
                    _allData.ForEach(d => {if(d.Title.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase)) _filterData.Add(d);});
                    break;
                case "ALL":
                    _allData.ForEach(d => _filterData.Add(d));
                    break;
                case "0-9":
                    _allData.ForEach(d => { if (d.Title.StartsWith("0") || d.Title.StartsWith("1") || d.Title.StartsWith("2") || d.Title.StartsWith("3") || d.Title.StartsWith("4") || d.Title.StartsWith("5") || d.Title.StartsWith("6") || d.Title.StartsWith("7") || d.Title.StartsWith("8") || d.Title.StartsWith("9")) _filterData.Add(d); });
                    break;
                default:
                    _allData.ForEach(d => { if (d.Title.Contains(filter.ToUpper())) _filterData.Add(d); });
                    break;
            }

            // Ordenar
            var lstTupleOrderBy = new List<Tuple<string, string>>();

            if (String.IsNullOrWhiteSpace(sort))
                lstTupleOrderBy.Add(new Tuple<string, string>(sort, order));

            _filterData = new List<MultimediaData>(_filterData.MultipleSort(lstTupleOrderBy).ToList());

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            GSearchResult.ColumnDefinitions.Clear();
            GSearchResult.RowDefinitions.Clear();

        }

        #region UpdateInfo
        #endregion

        private void KeyUpTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && GMain.IsEnabled)
            {
                    BtnOk.Focus();
            }
        }

        private void BtnOkClick(object sender, System.Windows.RoutedEventArgs e)
        {
            FilterData(SortType, "ALL", Sort);
        }

        private void TextBlockClick(object sender, MouseButtonEventArgs e)
        {
            var textBlock = (TextBlock) sender;
            FilterData(SortType, textBlock.Text, Sort);
        }
    }
}
