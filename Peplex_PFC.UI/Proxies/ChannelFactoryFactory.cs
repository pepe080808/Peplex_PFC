using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Peplex_PFC.UI.Config;

namespace Peplex_PFC.UI.Proxies
{
    public class ChannelFactoryFactory<T>
    {
        private readonly Binding _binding;
        private readonly EndpointAddress _endpointAddress;

        public ChannelFactoryFactory(Binding binding, string serviceName)
        {
            //var endpointUri = new Uri(String.Format("net.tcp://{0}:{1}/{2}", Config.HeadofficeConfig.Instance.SelectedServiceAddress, Config.HeadofficeConfig.Instance.ServiceLayerPort, serviceName));
            var endpointUri = new Uri(String.Format("net.tcp://{0}:{1}/{2}", PeplexConfig.Instance.ServiceAddress, PeplexConfig.Instance.ServiceLayerPort, serviceName));
            var endpointAddress = new EndpointAddress(endpointUri);

            _binding = binding;
            _endpointAddress = endpointAddress;
        }

        public ChannelFactory<T> CreateChannelFactory()
        {
            return new ChannelFactory<T>(_binding, _endpointAddress);
        }
    }
}
