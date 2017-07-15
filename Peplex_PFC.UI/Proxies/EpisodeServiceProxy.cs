using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using EmitMapper;
using Microsoft.Practices.ObjectBuilder2;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Proxies
{
    public class EpisodeServiceProxy : IEpisodeServiceProxy
    {
        private readonly ChannelFactory<IEpisodeService> _channelFactory;
        private readonly ObjectsMapper<EpisodeDTO, EpisodeUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeDTO, EpisodeUIO>();
        private readonly ObjectsMapper<EpisodeUIO, EpisodeDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeUIO, EpisodeDTO>();

        public EpisodeServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IEpisodeService>(binding, "Peplex_PFCEpisodeService").CreateChannelFactory();
        }

        public bool Insert(ProxyContext context, EpisodeUIO entity)
        {
            using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
                proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Update(ProxyContext context, EpisodeUIO entity)
        {
            using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
                proxy.Channel.Update(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Delete(ProxyContext context, int serieId, int season, int number)
        {
            using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
                proxy.Channel.Delete(serieId, season, number);

            return true;
        }

        public EpisodeUIO Single(ProxyContext context, int serieId, int season, int number)
        {
            using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
            {
                var dto = proxy.Channel.Single(serieId, season, number);
                if (dto == null)
                    return null;

                var uio = _mapDTO2UIO.Map(dto);
                return uio;
            }
        }

        public IEnumerable<EpisodeUIO> FindAll(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
            {
                var uios = new List<EpisodeUIO>();
                var dtos = proxy.Channel.FindAll();

                dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                return uios;
            }
        }

        public int MaxPK(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
                return proxy.Channel.MaxPK();
        }
    }
}