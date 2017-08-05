using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using EmitMapper;
using Microsoft.Practices.ObjectBuilder2;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Proxies
{
    public class FilmServiceProxy : IFilmServiceProxy
    {
        private readonly ChannelFactory<IFilmService> _channelFactory;
        private readonly ObjectsMapper<FilmDTO, FilmUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<FilmDTO, FilmUIO>();
        private readonly ObjectsMapper<FilmUIO, FilmDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<FilmUIO, FilmDTO>();

        public FilmServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IFilmService>(binding, "Peplex_PFCFilmService").CreateChannelFactory();
        }

        public bool Insert(ProxyContext context, FilmUIO entity)
        {
            try
            {
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                    proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

                return true;
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Show(null, "eo", DialogIcon.CommError, new[] {DialogButton.Accept}, "Error: Insert(); Servicio: Film; Descripción: " + ex.Message);
                return false;
            }
        }

        public bool Update(ProxyContext context, FilmUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                    proxy.Channel.Update(_mapUIO2DTO.Map(entity));

                return true;
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Show(null, "eo", DialogIcon.CommError, new[] {DialogButton.Accept}, "Error: Insert(); Servicio: Film; Descripción: " + ex.Message);
                return false;
            }
        }

        public bool Delete(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                proxy.Channel.Delete(pk);

            return true;
        }

        public FilmUIO Single(ProxyContext context, int pk)
        {
            using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
            {
                var dto = proxy.Channel.Single(pk);
                if (dto == null)
                    return null;

                var uio = _mapDTO2UIO.Map(dto);
                return uio;
            }
        }

        public IEnumerable<FilmUIO> FindAll(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
            {
                var uios = new List<FilmUIO>();
                var dtos = proxy.Channel.FindAll();

                dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                return uios;
            }
        }

        public int MaxPK(ProxyContext context)
        {
            using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                return proxy.Channel.MaxPK();
        }
    }
}