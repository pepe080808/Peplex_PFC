using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using EmitMapper;
using Microsoft.Practices.ObjectBuilder2;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Proxies
{
    public class SerieServiceProxy : ISerieServiceProxy
    {
        private readonly ChannelFactory<ISerieService> _channelFactory;
        private readonly ObjectsMapper<SerieDTO, SerieUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<SerieDTO, SerieUIO>();
        private readonly ObjectsMapper<SerieUIO, SerieDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<SerieUIO, SerieDTO>();
        private readonly ObjectsMapper<EpisodeDTO, EpisodeUIO> _mapEpDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeDTO, EpisodeUIO>();
        private readonly ObjectsMapper<EpisodeUIO, EpisodeDTO> _mapEpUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeUIO, EpisodeDTO>();

        public SerieServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<ISerieService>(binding, "Peplex_PFCSerieService").CreateChannelFactory();
        }

        public bool Insert(ProxyContext context, SerieUIO entity)
        {
            using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
                proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Update(ProxyContext context, SerieUIO entity)
        {
            using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
                proxy.Channel.Update(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Delete(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
                proxy.Channel.Delete(pk);

            return true;
        }

        public SerieUIO Single(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
            {
                var dto = proxy.Channel.Single(pk);
                if (dto == null)
                    return null;

                var uio = _mapDTO2UIO.Map(dto);

                dto.Episodes.ForEach(d => uio.Episodes.Add(_mapEpDTO2UIO.Map(d)));

                return uio;
            }
        }

        public IEnumerable<SerieUIO> FindAll(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
            {
                var uios = new List<SerieUIO>();
                var dtos = proxy.Channel.FindAll();

                dtos.ForEach(dto =>
                {
                    uios.Add(_mapDTO2UIO.Map(dto));
                    dto.Episodes.ForEach(d => uios.Last().Episodes.Add(_mapEpDTO2UIO.Map(d)));
                });

                return uios;
            }
        }

        public int MaxPK(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
                return proxy.Channel.MaxPK();
        }
    }
}