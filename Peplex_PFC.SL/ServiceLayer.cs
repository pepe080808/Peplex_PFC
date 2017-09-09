using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Peplex_PFC.BLL.Composition;
using Peplex_PFC.BLL.Composition.Config;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Utils;

namespace Peplex_PFC.SL
{
    public static class ServiceLayer
    {
        public static void SetupRegularEndpoints()
        {
            var services = new List<ServiceEndpointDescriptor>
            {
                new ServiceEndpointDescriptor {ServiceType = typeof (GenreService), InterfaceType = typeof (IGenreService), ServiceName = "Peplex_PFCGenreService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (UserService), InterfaceType = typeof (IUserService), ServiceName = "Peplex_PFCUserService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (FormatService), InterfaceType = typeof (IFormatService), ServiceName = "Peplex_PFCFormatService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (FilmService), InterfaceType = typeof (IFilmService), ServiceName = "Peplex_PFCFilmService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (SerieService), InterfaceType = typeof (ISerieService), ServiceName = "Peplex_PFCSerieService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (EpisodeService), InterfaceType = typeof (IEpisodeService), ServiceName = "Peplex_PFCEpisodeService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (PrerequisitesCheckService), InterfaceType = typeof (IPrerequisitesCheckService), ServiceName = "AvelonRMSPrerequisitesCheckService"},
            };

            BuildEndpoints(services.ToArray(), ServiceConfig.Instance.ServiceLayerPort, "");
        }

        public static void BuildEndpoints(ServiceEndpointDescriptor[] endpointDescriptors, int servicePort, string certificateCommonName)
        {
            var baseBinding = new NetTcpBinding(SecurityMode.None) { MaxReceivedMessageSize = 2147483647 };
            var bindingElements = baseBinding.CreateBindingElements();

            var binding = new CustomBinding(bindingElements) { Name = "GZipAndEncryptionBinding", Namespace = "http://tempuri.org/bindings", };
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 10, 0);

            foreach (var service in endpointDescriptors)
            {
                var contractDesc = ContractDescription.GetContract(service.InterfaceType);
                var serviceHost = new ServiceHost(service.ServiceType);

                // Add tcp endpoint
                //var tcpEndpoint = new ServiceEndpoint(contractDesc, binding, new EndpointAddress(new Uri(String.Format("net.tcp://localhost:{0}/{1}", servicePort, service.ServiceName))));
                var tcpEndpoint = new ServiceEndpoint(contractDesc, binding, new EndpointAddress(new Uri(String.Format("net.tcp://{2}:{0}/{1}", servicePort, service.ServiceName, ServiceConfig.Instance.ServiceAddress))));

                serviceHost.AddServiceEndpoint(tcpEndpoint);

                serviceHost.Open();
            }
        }

        public static void SetupRestEndpoints()
        {
            var services = new[]
            {
                new ServiceEndpointDescriptor {ServiceType = typeof (ExternalPlatformRestService), InterfaceType = typeof (IExternalPlatformRestService), ServiceName = "PeplexExternalPlatformRestService"},
            };

            BuildEndpointsRest(services, ServiceConfig.Instance.ExternalPlatformRestApiPort, "");
        }

        public static void BuildEndpointsRest(ServiceEndpointDescriptor[] endpointDescriptors, int servicePort, string sslCertificateCommonName)
        {
            var custom = new CustomBinding(new WebHttpBinding(WebHttpSecurityMode.Transport));
            (custom.Elements[1] as TransportBindingElement).MaxReceivedMessageSize = int.MaxValue;

            foreach (var service in endpointDescriptors)
            {
                //var baseAddress = String.Format("https://localhost:{0}/{1}", servicePort, service.ServiceName);
                var baseAddress = String.Format("https://{2}:{0}/{1}", servicePort, service.ServiceName, ServiceConfig.Instance.ServiceAddress);
                var host = new ServiceHost(service.ServiceType, new Uri(baseAddress));
                host.AddServiceEndpoint(service.InterfaceType, custom, "").Behaviors.Add(new WebHttpBehavior());
                host.Open();

                Cert(servicePort, sslCertificateCommonName);
            }
        }

        private static void Cert(int servicePort, string sslCertificateCommonName)
        {
            var storex = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                storex.Open(OpenFlags.ReadOnly);

                var certificatesx = storex.Certificates.Find(X509FindType.FindBySubjectName, sslCertificateCommonName, false);

                if (certificatesx.Count == 0)
                    throw new Exception(String.Format("Certificate {0} not found", sslCertificateCommonName));

                // Netsh http delete sslcert ipport=0.0.0.0:<port>
                var unbindPortToCertificate = new Process
                {
                    StartInfo =
                        {
                            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "netsh.exe"),
                            Arguments = string.Format("Netsh http delete sslcert ipport=0.0.0.0:{0}", servicePort),
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                };

                unbindPortToCertificate.Start();
                unbindPortToCertificate.WaitForExit();

                // netsh http add sslcert ipport=0.0.0.0:<port> certhash={<thumbprint>} appid={<some GUID>} clientcertnegotiation=enable
                var bindPortToCertificate = new Process
                {
                    StartInfo =
                        {
                            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "netsh.exe"),
                            Arguments = string.Format("http add sslcert ipport=0.0.0.0:{0} certhash={1} appid={{{2}}} clientcertnegotiation=enable", servicePort, certificatesx[0].Thumbprint, Assembly.GetExecutingAssembly().GetType().GUID),
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                };

                bindPortToCertificate.Start();
                bindPortToCertificate.WaitForExit();
            }
            finally
            {
                storex.Close();
            }
        }
    }
}
