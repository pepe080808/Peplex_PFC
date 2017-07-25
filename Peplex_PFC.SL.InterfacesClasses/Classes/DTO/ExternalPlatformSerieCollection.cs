using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class ExternalPlatformSerieCollection
    {
        [DataMember]
        public int Result { get; set; }

        [DataMember]
        public string ResultString { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public List<ExternalPlatformSerie> Series { get; private set; }

        public ExternalPlatformSerieCollection()
        {
            Series = new List<ExternalPlatformSerie>();
        }
    }

    [DataContract]
    public class ExternalPlatformSerie
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Synopsis { get; set; }

        [DataMember]
        public decimal Note { get; set; }

        [DataMember]
        public string Base64Background { get; set; }

        [DataMember]
        public string Base64Cover { get; set; }
    }
}