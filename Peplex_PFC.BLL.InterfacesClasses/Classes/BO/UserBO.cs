﻿using System.Collections.Generic;

namespace Peplex_PFC.BLL.InterfacesClasses.Classes.BO
{
    [AcgClass]
    public class UserBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }
        public byte[] PhotoOpt { get; set; }
        public int Permissions { get; set; }

        public List<int> FilmSeen { get; set; }
        public List<int> SerieSeen { get; set; }
        public List<string> EpisodeSeen { get; set; }
        public Dictionary<int, int> FilmTime { get; set; }
        public Dictionary<string, int> EpisodeTime { get; set; }

        public UserBO()
        {
            FilmSeen = new List<int>();
            SerieSeen = new List<int>();
            EpisodeSeen = new List<string>();
            FilmTime = new Dictionary<int, int>();
            EpisodeTime = new Dictionary<string, int>();
        }
    }
}
