using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        [DataMember]
        public byte[] PhotoOpt { get; set; }

        [DataMember]
        public int Permissions { get; set; }

        [DataMember]
        public List<int> FilmSeen { get; set; }

        [DataMember]
        public List<int> SerieSeen { get; set; }

        [DataMember]
        public List<string> EpisodeSeen { get; set; }

        [DataMember]
        public Dictionary<int, int> FilmTime { get; set; }

        [DataMember]
        public Dictionary<string, int> EpisodeTime { get;   set; }

        public UserDTO()
        {
            FilmSeen = new List<int>();
            SerieSeen = new List<int>();
            EpisodeSeen = new List<string>();
            FilmTime = new Dictionary<int, int>();
            EpisodeTime = new Dictionary<string, int>();
        }
    }
}
