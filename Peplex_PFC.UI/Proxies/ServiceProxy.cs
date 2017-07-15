using System;
using System.ServiceModel;

namespace Peplex_PFC.UI.Proxies
{
    public class ServiceProxy<T> : IDisposable
    {
        public T Channel { get; private set; }

        public ServiceProxy(ChannelFactory<T> channelFactory)
        {
            if (channelFactory == null)
                throw new ArgumentException("channelFactory");

            Channel = channelFactory.CreateChannel();
        }

        public void Dispose()
        {
            (Channel as IDisposable).Dispose();
        }
    }
}
