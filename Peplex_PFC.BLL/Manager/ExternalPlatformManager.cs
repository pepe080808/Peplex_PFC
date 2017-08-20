using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.BLL.Manager
{
    public class ExternalPlatformManager : IExternalPlatformManager
    {
        private readonly IFilmManager _filmManager;
        private readonly ISerieManager _serieManager;
        private readonly IUserManager _userManager;
        private readonly IGenreManager _genreManager;

        public ExternalPlatformManager(IFilmManager filmManager, ISerieManager serieManager, IUserManager userManager, IGenreManager genreManager)
        {
            _filmManager = filmManager;
            _serieManager = serieManager;
            _userManager = userManager;
            _genreManager = genreManager;
        }

        public List<FilmBO> GetFilms(IUnitOfWork unitOfWork)
        {
            return _filmManager.FindAll(unitOfWork);
        }

        public FilmBO GetFilm(IUnitOfWork unitOfWork, int filmId)
        {
            return _filmManager.Single(unitOfWork, filmId);
        }

        public List<SerieBO> GetSeries(IUnitOfWork unitOfWork)
        {
            return _serieManager.FindAll(unitOfWork);
        }

        public SerieBO GetSerie(IUnitOfWork unitOfWork, int serieId)
        {
            return _serieManager.Single(unitOfWork, serieId);
        }

        public List<UserBO> GetUsers(IUnitOfWork unitOfWork)
        {
            return _userManager.FindAll(unitOfWork);
        }

        public UserBO GetUser(IUnitOfWork unitOfWork, int userId)
        {
            return _userManager.Single(unitOfWork, userId);
        }

        public List<GenreBO> GetGenres(IUnitOfWork unitOfWork)
        {
            return _genreManager.FindAll(unitOfWork);
        }
    }
}
