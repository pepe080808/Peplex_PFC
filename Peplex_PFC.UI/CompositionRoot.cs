using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Practices.Unity;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;

namespace Peplex_PFC.UIO
{
    public class CompositionRoot
    {
        public IUnityContainer Container { get; private set; }

        private readonly CustomBinding _binding;
        public CustomBinding CustomBinding
        {
            get { return _binding; }
        }

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

            var baseBinding = new NetTcpBinding(SecurityMode.None) { MaxReceivedMessageSize = 2147483647 };
            var bindingElements = baseBinding.CreateBindingElements();

            _binding = new CustomBinding(bindingElements) { Name = "GZipAndEncryptionBinding", Namespace = "http://tempuri.org/bindings", };
            _binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            _binding.SendTimeout = new TimeSpan(0, 10, 0);

            //Container.RegisterType<IDatabaseServiceProxy, DatabaseServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            //Container.RegisterType<ISuppliersCRUDServiceProxy, SuppliersCRUDServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<IGenreServiceProxy, GenreServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<IUserServiceProxy, UserServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<IFormatServiceProxy, FormatServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<IFilmServiceProxy, FilmServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<ISerieServiceProxy, SerieServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<IEpisodeServiceProxy, EpisodeServiceProxy>(new InjectionConstructor(new object[] { _binding }));
            Container.RegisterType<PrerequisitesCheckServiceProxy>(new InjectionConstructor(new object[] { _binding }));
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        #endregion
    }
}
