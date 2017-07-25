using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class ExternalPlatformEpisodeCollection
    {
        [DataMember]
        public int Result { get; set; }

        [DataMember]
        public string ResultString { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public List<ExternalPlatformEpisode> Episodes { get; private set; }

        public ExternalPlatformEpisodeCollection()
        {
            Episodes = new List<ExternalPlatformEpisode>();
        }
    }

    [DataContract]
    public class ExternalPlatformEpisode
    {
        [DataMember]
        public int SerieId { get; set; }

        [DataMember]
        public int Season { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
}