using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using EmitMapper;
using Microsoft.Practices.ObjectBuilder2;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;
using Utils;

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

        public bool Insert(FilmUIO entity)
        {
            try
            {
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                    proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

                return true;
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] {DialogButton.Accept}, msg);
                return false;
            }
        }

        public bool Update(FilmUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                    proxy.Channel.Update(_mapUIO2DTO.Map(entity));

                return true;
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return false;
            }
        }

        public bool Delete(int pk)
        {
            try
            {
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                    proxy.Channel.Delete(pk);

                return true;
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return false;
            }
        }

        public FilmUIO Single(int pk)
        {
            try
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
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return null;
            }
        }

        public IEnumerable<FilmUIO> FindAll()
        {
            try
            { 
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                {
                    var uios = new List<FilmUIO>();
                    var dtos = proxy.Channel.FindAll();

                    dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                    return uios;
                }
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return new List<FilmUIO>();
            }
        }

        public int MaxPK()
        {
            try
            {
                using (var proxy = new ServiceProxy<IFilmService>(_channelFactory))
                    return proxy.Channel.MaxPK();
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] {DialogButton.Accept}, msg);
                return 0;
            }

        }
    }
}