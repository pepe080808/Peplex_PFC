namespace Peplex_PFC.BLL.InterfacesClasses.Classes.BO
{
    [AcgClass]
    public class EpisodeBO
    {
        public int SerieId { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int FormatId { get; set; }
        public string FormatName { get; set; }
        public string QualityName { get; set; }
    }
}
