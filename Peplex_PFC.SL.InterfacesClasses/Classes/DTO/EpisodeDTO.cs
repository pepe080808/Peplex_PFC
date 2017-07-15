using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class EpisodeDTO
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
        public int FormatId { get; set; }

        [DataMember]
        public string FormatName { get; set; }

        [DataMember]
        public string QualityName { get; set; }
    }
}
