using System;

namespace Peplex_PFC.UI.UIO
{
    public class FilmUIO : UserInterfaceObject
    {
        private int _id;
        private string _title;
        private string _subtitle;
        private int _formatId;
        private string _formatName;
        private string _qualityName;
        private int _genreId01;
        private string _genreName01;
        private int _genreId02;
        private string _genreName02;
        private string _synopsis;
        private int _durationMin;
        private DateTime _downloadDate;
        private DateTime _premiereDate;
        private string _trailerURL;
        private decimal _note;
        private byte[] _background;
        private byte[] _cover;
        private bool _seen;

        public int Id
        {
            get { return _id; }
            set { _id = value; SendPropertyChanged(); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; SendPropertyChanged(); }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; SendPropertyChanged(); }
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

        public int GenreId01
        {
            get { return _genreId01; }
            set { _genreId01 = value; SendPropertyChanged(); }
        }

        public string GenreName01
        {
            get { return _genreName01; }
            set { _genreName01 = value; SendPropertyChanged(); }
        }

        public int GenreId02
        {
            get { return _genreId02; }
            set { _genreId02 = value; SendPropertyChanged(); }
        }

        public string GenreName02
        {
            get { return _genreName02; }
            set { _genreName02 = value; SendPropertyChanged(); }
        }

        public string Synopsis
        {
            get { return _synopsis; }
            set { _synopsis = value; SendPropertyChanged(); }
        }

        public int DurationMin
        {
            get { return _durationMin; }
            set { _durationMin = value; SendPropertyChanged(); }
        }

        public DateTime DownloadDate
        {
            get { return _downloadDate; }
            set { _downloadDate = value; SendPropertyChanged(); }
        }

        public DateTime PremiereDate
        {
            get { return _premiereDate; }
            set { _premiereDate = value; SendPropertyChanged(); }
        }

        public string TrailerURL
        {
            get { return _trailerURL; }
            set { _trailerURL = value; SendPropertyChanged(); }
        }

        public decimal Note
        {
            get { return _note; }
            set { _note = value; SendPropertyChanged(); }
        }

        public byte[] Background
        {
            get { return _background; }
            set { _background = value; SendPropertyChanged(); }
        }

        public byte[] Cover
        {
            get { return _cover; }
            set { _cover = value; SendPropertyChanged(); }
        }

        public bool Seen
        {
            get { return _seen; }
            set { _seen = value; SendPropertyChanged(); }
        }
    }
}
