using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class ExternalPlatformUserCollection
    {
        [DataMember]
        public int Result { get; set; }

        [DataMember]
        public string ResultString { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public List<ExternalPlatformUser> Users { get; private set; }

        public ExternalPlatformUserCollection()
        {
            Users = new List<ExternalPlatformUser>();
        }
    }

    [DataContract]
    public class ExternalPlatformUser
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
        public string Base64Photo { get; set; }

        [DataMember]
        public List<int> FilmSeen { get; set; }

        [DataMember]
        public List<int> SerieSeen { get; set; }

        [DataMember]
        public List<string> EpisodeSeen { get; set; }

        public ExternalPlatformUser()
        {
            FilmSeen = new List<int>();
            EpisodeSeen = new List<string>();
            SerieSeen = new List<int>();
        }
    }
}