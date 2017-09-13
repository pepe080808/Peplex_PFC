using System;

namespace Peplex_PFC.BLL.InterfacesClasses.Classes.BO
{
    [AcgClass]
    public class FilmBO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int FormatId { get; set; }
        public string FormatName { get; set; }
        public string QualityName { get; set; }
        public int GenreId01 { get; set; }
        public string GenreName01 { get; set; }
        public int GenreId02 { get; set; }
        public string GenreName02 { get; set; }
        public string Synopsis { get; set; }
        public int DurationMin { get; set; }
        public DateTime DownloadDate { get; set; }
        public DateTime PremiereDate { get; set; }
        public string TrailerURL { get; set; }
        public decimal Note { get; set; }
        public byte[] Background { get; set; }
        public byte[] Cover { get; set; }
        public byte[] BackgroundOpt { get; set; }
        public byte[] CoverOpt { get; set; }
    }
}
