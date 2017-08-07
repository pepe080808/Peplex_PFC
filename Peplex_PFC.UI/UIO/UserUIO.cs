using System.Collections.Generic;

namespace Peplex_PFC.UI.UIO
{
    public class UserUIO : UserInterfaceObject
    {
        private int _id;
        private string _name;
        private string _nickName;
        private string _email;
        private string _password;
        private byte[] _photo;
        private int _permissions;

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

        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; SendPropertyChanged(); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; SendPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; SendPropertyChanged(); }
        }

        public byte[] Photo
        {
            get { return _photo; }
            set { _photo = value; SendPropertyChanged(); }
        }

        public int Permissions
        {
            get { return _permissions; }
            set { _permissions = value; SendPropertyChanged(); }
        }

        public List<int> FilmSeen { get; set; }
        public List<int> SerieSeen { get; set; }
        public List<string> EpisodeSeen { get; set; }
        public Dictionary<int, int> FilmTime { get; set; }
        public Dictionary<string, int> EpisodeTime { get; set; }

        public UserUIO()
        {
            FilmSeen = new List<int>();
            SerieSeen = new List<int>();
            EpisodeSeen = new List<string>();
            FilmTime = new Dictionary<int, int>();
            EpisodeTime = new Dictionary<string, int>();
        }
    }
}
