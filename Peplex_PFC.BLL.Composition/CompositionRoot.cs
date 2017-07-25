using System;
using Microsoft.Practices.Unity;
using Peplex_PFC.BLL.Composition.Config;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.BLL.Manager;
using Peplex_PFC.DAL;

namespace Peplex_PFC.BLL.Composition
{
    public class CompositionRoot
    {
        public IUnityContainer Container { get; private set; }

        #region Singleton construction, Load and Save methods

        private static readonly object SyncRoot = new Object();

        private static volatile CompositionRoot _instance;

        public static CompositionRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new CompositionRoot();
                        }
                    }
                }

                return _instance;
            }
        }

        private CompositionRoot()
        {
            Container = new UnityContainer();

            Container.RegisterType<IUnitOfWork, UnitOfWork>(new InjectionConstructor(ServiceConfig.Instance.ConnectionString));

            Container.RegisterType<IGenreManager, GenreManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGenreRepository, GenreRepository>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IFormatManager, FormatManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFormatRepository, FormatRepository>(new ContainerControlledLifetimeManager());

            Container.RegisterType<ISerieManager, SerieManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISerieRepository, SerieRepository>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IFilmManager, FilmManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFilmRepository, FilmRepository>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IUserManager, UserManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IEpisodeManager, EpisodeManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEpisodeRepository, EpisodeRepository>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IExternalPlatformManager, ExternalPlatformManager>(new ContainerControlledLifetimeManager());
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        #endregion
    }
}
