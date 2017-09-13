using System;
using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses.Classes.DTO
{
    [DataContract]
    public class FilmDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Subtitle { get; set; }

        [DataMember]
        public int FormatId { get; set; }

        [DataMember]
        public string FormatName { get; set; }

        [DataMember]
        public string QualityName { get; set; }

        [DataMember]
        public int GenreId01 { get; set; }

        [DataMember]
        public string GenreName01 { get; set; }

        [DataMember]
        public int GenreId02 { get; set; }

        [DataMember]
        public string GenreName02 { get; set; }

        [DataMember]
        public string Synopsis { get; set; }

        [DataMember]
        public int DurationMin { get; set; }

        [DataMember]
        public DateTime DownloadDate { get; set; }

        [DataMember]
        public DateTime PremiereDate { get; set; }

        [DataMember]
        public string TrailerURL { get; set; }

        [DataMember]
        public decimal Note { get; set; }

        [DataMember]
        public byte[] Background { get; set; }

        [DataMember]
        public byte[] Cover { get; set; }

        [DataMember]
        public byte[] BackgroundOpt { get; set; }

        [DataMember]
        public byte[] CoverOpt { get; set; }
    }
}
