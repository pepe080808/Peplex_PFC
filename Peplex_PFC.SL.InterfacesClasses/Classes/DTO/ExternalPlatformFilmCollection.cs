using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class ExternalPlatformFilmCollection
    {
        [DataMember]
        public int Result { get; set; }

        [DataMember]
        public string ResultString { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public List<ExternalPlatformFilm> Films { get; private set; }

        public ExternalPlatformFilmCollection()
        {
            Films = new List<ExternalPlatformFilm>();
        }
    }

    [DataContract]  
    public class ExternalPlatformFilm
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Subtitle { get; set; }

        [DataMember]
        public string Synopsis { get; set; }

        [DataMember]
        public decimal Note { get; set; }

        [DataMember]
        public string TrailerURL { get; set; }

        [DataMember]
        public string StrBackground { get; set; }

        [DataMember]
        public string StrCover { get; set; }

        [DataMember]
        public string FormatName { get; set; }

        [DataMember]
        public string GenreName01 { get; set; }

        [DataMember]
        public string GenreName02 { get; set; }

        [DataMember]
        public int DurationMin { get; set; }

        [DataMember]
        public string DownloadDate { get; set; }

        [DataMember]
        public string PremiereDate { get; set; }
    }
}
