using System.Windows.Media.Imaging;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Panels
{
    public partial class CoverControl
    {
        public FilmUIO Film { get; set; }
        public SerieUIO Serie { get; set; }

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
                    if (Film != null)
                    {
                        var child = new FilmWindow {Film = Film};
                        child.Show();
                    }
                    break;
                case "Serie":
                    if (Serie != null)
                    {
                        var child = new SerieWindow { Serie = Serie };
                        child.Show();
                    }
                    break;
            }
        }
    }
}
