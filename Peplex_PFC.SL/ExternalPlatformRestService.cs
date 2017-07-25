using System;
using System.ServiceModel;
using Peplex_PFC.BLL.Composition;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.SL
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ExternalPlatformRestService : IExternalPlatformRestService
    {
        private readonly IExternalPlatformManager _manager = CompositionRoot.Instance.Resolve<IExternalPlatformManager>();

        public ExternalPlatformFilmCollection GetFilms()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformFilmCollection();

                    foreach (var b in _manager.GetFilms(uow))
                    {
                        var film = new ExternalPlatformFilm {};

                        // Mapeo de datos

                        result.Films.Add(film);
                    }

                    result.Count = result.Films.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformFilmCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformSerieCollection GetSeries()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformSerieCollection();

                    foreach (var b in _manager.GetSeries(uow))
                    {
                        var serie = new ExternalPlatformSerie { };

                        // Mapeo de datos

                        result.Series.Add(serie);
                    }

                    result.Count = result.Series.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformSerieCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformEpisodeCollection GetEpisodes(string serieId)
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformEpisodeCollection();

                    foreach (var b in _manager.GetEpisodes(uow, Convert.ToInt32(serieId)))
                    {
                        var episode = new ExternalPlatformEpisode {};

                        // Mapeo de datos

                        result.Episodes.Add(episode);
                    }

                    result.Count = result.Episodes.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformEpisodeCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformUserCollection GetUsers()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformUserCollection();

                    foreach (var b in _manager.GetUsers(uow))
                    {
                        var user = new ExternalPlatformUser { };

                        // Mapeo de datos

                        result.Users.Add(user);
                    }

                    result.Count = result.Users.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformUserCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }
    }
}
