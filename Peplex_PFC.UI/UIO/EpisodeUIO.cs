namespace Peplex_PFC.UI.UIO
{
    public class EpisodeUIO : UserInterfaceObject
    {
        private int _serieId;
        private int _season;
        private int _number;
        private string _name;
        private int _formatId;
        private string _formatName;
        private string _qualityName;
        private bool _seen;

        public int SerieId
        {
            get { return _serieId; }
            set { _serieId = value; SendPropertyChanged(); }
        }

        public int Season
        {
            get { return _season; }
            set { _season = value; SendPropertyChanged(); }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; SendPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; SendPropertyChanged(); }
        }

        public int FormatId
        {
            get { return _formatId; }
            set { _formatId = value; SendPropertyChanged(); }
        }

        public string FormatName
        {
            get { return _formatName; }
            set { _formatName = value; SendPropertyChanged(); }
        }

        public string QualityName
        {
            get { return _qualityName; }
            set { _qualityName = value; SendPropertyChanged(); }
        }

        public bool Seen
        {
            get { return _seen; }
            set { _seen = value; SendPropertyChanged(); }
        }
    }
}
