using System;
using System.Collections.Generic;

namespace Peplex_PFC.UI.UIO
{
    public class SerieUIO : UserInterfaceObject
    {
        private int _id;
        private string _title;
        private int _genreId01;
        private string _genreName01;
        private int _genreId02;
        private string _genreName02;
        private string _synopsis;
        private int _durationMin;
        private DateTime _downloadDate;
        private DateTime _premiereDate;
        private decimal _note;
        private byte[] _background;
        private byte[] _cover;
        private byte[] _backgroundOpt;
        private byte[] _coverOpt;
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

        public byte[] BackgroundOpt
        {
            get { return _backgroundOpt; }
            set { _backgroundOpt = value; SendPropertyChanged(); }
        }

        public byte[] CoverOpt
        {
            get { return _coverOpt; }
            set { _coverOpt = value; SendPropertyChanged(); }
        }

        public List<EpisodeUIO> Episodes { get; private set; }

        public SerieUIO()
        {
            Episodes = new List<EpisodeUIO>();
        }

        public bool Seen
        {
            get { return _seen; }
            set { _seen = value; SendPropertyChanged(); }
        }
    }
}
