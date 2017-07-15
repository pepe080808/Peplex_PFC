using System.Collections.Generic;
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
    public class GenreServiceProxy : IGenreServiceProxy
    {
        private readonly ChannelFactory<IGenreService> _channelFactory;
        private readonly ObjectsMapper<GenreDTO, GenreUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<GenreDTO, GenreUIO>();
        private readonly ObjectsMapper<GenreUIO, GenreDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<GenreUIO, GenreDTO>();

        public GenreServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IGenreService>(binding, "Peplex_PFCGenreService").CreateChannelFactory();
        }

        public bool Insert(ProxyContext context, GenreUIO entity)
        {
            using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Update(ProxyContext context, GenreUIO entity)
        {
            using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                proxy.Channel.Update(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Delete(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                proxy.Channel.Delete(pk);

            return true;
        }

        public GenreUIO Single(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
            {
                var dto = proxy.Channel.Single(pk);
                if (dto == null)
                    return null;

                var uio = _mapDTO2UIO.Map(dto);
                return uio;
            }
        }

        public IEnumerable<GenreUIO> FindAll(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
            {
                var uios = new List<GenreUIO>();
                var dtos = proxy.Channel.FindAll();
                
                dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                return uios;
            }
        }

        public int MaxPK(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                return proxy.Channel.MaxPK();
        }
    }
}
