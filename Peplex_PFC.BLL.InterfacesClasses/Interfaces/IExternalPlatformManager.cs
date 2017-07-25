using System.Collections.Generic;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IExternalPlatformManager
    {
        List<ExternalPlatformFilm> GetFilms(IUnitOfWork unitOfWork);
        List<ExternalPlatformSerie> GetSeries(IUnitOfWork unitOfWork);
        List<ExternalPlatformEpisode> GetEpisodes(IUnitOfWork unitOfWork, int serieId);
        List<ExternalPlatformUser> GetUsers(IUnitOfWork unitOfWork);
    }
}
