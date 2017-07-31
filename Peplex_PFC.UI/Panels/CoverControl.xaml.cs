using System.Windows;
using System.Windows.Media.Imaging;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UIO;

namespace Peplex_PFC.UI.Panels
{
    public partial class CoverControl
    {
        private string _strTag;
        public string StrTag
        {
            get { return _strTag; }
            set { _strTag = value; UpdateTitle(); }
        }

        private string _strTitle ;
        public string StrTitle
        {
            get { return _strTitle; }
            set { _strTitle = value; UpdateTitle(); }
        }

        private BitmapImage _img;
        public BitmapImage Img
        {
            get { return _img; }
            set { _img = value; UpdateImg(); }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value;}
        }

        public CoverControl()
        {
            InitializeComponent();
        }

        private void UpdateImg()
        {
            if (Img != null)
                ImgCover.Source = Img;
        }

        private void UpdateTitle()
        {
            if (StrTitle != null)
                TxtCover.Text = StrTitle;
        }

        private void ImgCoverPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            switch (StrTag)
            {
                case "Film":
                    var film = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().Single(new ProxyContext(), Id);
                    var childFilm = new FilmWindow { Owner = Window.GetWindow(Parent), Film = film};
                    childFilm.ShowDialog();
                    break;
                case "Serie":
                    var serie= CompositionRoot.Instance.Resolve<ISerieServiceProxy>().Single(new ProxyContext(), Id);
                    var childSerie = new SerieWindow { Owner = Window.GetWindow(Parent), Serie = serie };
                    childSerie.ShowDialog();
                    break;
            }
        }
    }
}
