using System.ServiceModel;
using System.ServiceModel.Channels;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.UI.Proxies
{
    public class PrerequisitesCheckServiceProxy
    {
        private readonly ChannelFactory<IPrerequisitesCheckService> _channelFactory;

        public PrerequisitesCheckServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IPrerequisitesCheckService>(binding, "AvelonRMSPrerequisitesCheckService").CreateChannelFactory();
        }

        public bool IsAlive()
        {
            using (var proxy = new ServiceProxy<IPrerequisitesCheckService>(_channelFactory))
                return proxy.Channel.IsAlive();
        }

        public bool UpdateDataBase()
        {
            using (var proxy = new ServiceProxy<IPrerequisitesCheckService>(_channelFactory))
                return proxy.Channel.UpdateDataBase();
        }
    }
}
