using System.Windows;
using System.Windows.Media.Imaging;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;

namespace Peplex_PFC.UI.Panels
{
    public partial class CoverControl
    {
        private Generic.MultimediaType _multimediaType;
        public Generic.MultimediaType MultimediaType
        {
            get { return _multimediaType; }
            set { _multimediaType = value; UpdateTitle(); }
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
            switch (_multimediaType)
            {
                case Generic.MultimediaType.FilmType:
                    var film = CompositionRoot.Instance.Resolve<IFilmServiceProxy>().Single(Id);
                    var childFilm = new FilmWindow { Owner = Window.GetWindow(Parent), Film = film};
                    childFilm.ShowDialog();
                    break;
                case Generic.MultimediaType.SerieType:
                    var serie= CompositionRoot.Instance.Resolve<ISerieServiceProxy>().Single(Id);
                    var childSerie = new SerieWindow { Owner = Window.GetWindow(Parent), Serie = serie };
                    childSerie.ShowDialog();
                    break;
            }
        }
    }
}
