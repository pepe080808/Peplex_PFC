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
    public class UserServiceProxy : IUserServiceProxy
    {
        private readonly ChannelFactory<IUserService> _channelFactory;
        private readonly ObjectsMapper<UserDTO, UserUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<UserDTO, UserUIO>();
        private readonly ObjectsMapper<UserUIO, UserDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<UserUIO, UserDTO>();

        public UserServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IUserService>(binding, "Peplex_PFCUserService").CreateChannelFactory();
        }

        public bool Insert(ProxyContext context, UserUIO entity)
        {
            using (var proxy = new ServiceProxy<IUserService>(_channelFactory))
                proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Update(ProxyContext context, UserUIO entity)
        {
            using (var proxy = new ServiceProxy<IUserService>(_channelFactory))
                proxy.Channel.Update(_mapUIO2DTO.Map(entity));

            return true;
        }

        public bool Delete(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<IUserService>(_channelFactory))
                proxy.Channel.Delete(pk);

            return true;
        }

        public UserUIO Single(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<IUserService>(_channelFactory))
            {
                var dto = proxy.Channel.Single(pk);
                if (dto == null)
                    return null;

                var uio = _mapDTO2UIO.Map(dto);
                return uio;
            }
        }

        public IEnumerable<UserUIO> FindAll(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IUserService>(_channelFactory))
            {
                var uios = new List<UserUIO>();
                var dtos = proxy.Channel.FindAll();

                dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                return uios;
            }
        }

        public int MaxPK(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IUserService>(_channelFactory))
                return proxy.Channel.MaxPK();
        }
    }
}