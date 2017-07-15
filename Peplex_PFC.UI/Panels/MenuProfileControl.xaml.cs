using System.Windows.Media.Imaging;

namespace Peplex_PFC.UI.Panels
{
    public partial class MenuProfileControl
    {
        private string _strNick;
        public string StrNick
        {
            get { return _strNick; }
            set { _strNick = value; UpdateNick(); }
        }

        private BitmapImage _img;
        public BitmapImage Img
        {
            get { return _img; }
            set { _img = value; UpdateImg(); }
        }

        public MenuProfileControl()
        {
            InitializeComponent();
        }

        private void UpdateImg()
        {
            if (Img != null)
                ImgProfile.Source = Img;
        }

        private void UpdateNick()
        {
            if (StrNick != null)
                TxtNick.Text = StrNick;
        }
    }
}
