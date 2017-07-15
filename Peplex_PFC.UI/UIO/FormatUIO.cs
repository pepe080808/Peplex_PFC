namespace Peplex_PFC.UI.UIO
{
    public class FormatUIO : UserInterfaceObject
    {
        private int _id;
        private string _name;
        private string _quality;

        public int Id
        {
            get { return _id; }
            set { _id = value; SendPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; SendPropertyChanged(); }
        }

        public string Quality
        {
            get { return _quality; }
            set { _quality = value; SendPropertyChanged(); }
        }
    }
}
