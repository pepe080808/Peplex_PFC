using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IExternalPlatformManager
    {
        List<FilmBO> GetFilms(IUnitOfWork unitOfWork);
        FilmBO GetFilm(IUnitOfWork unitOfWork, int filmId);
        List<SerieBO> GetSeries(IUnitOfWork unitOfWork);
        SerieBO GetSerie(IUnitOfWork unitOfWork, int serieId);
        List<UserBO> GetUsers(IUnitOfWork unitOfWork);
        UserBO GetUser(IUnitOfWork unitOfWork, int userId);
        List<GenreBO> GetGenres(IUnitOfWork unitOfWork);
    }
}
