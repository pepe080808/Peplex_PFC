﻿using System;
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
    public class GenreServiceProxy : IGenreServiceProxy
    {
        private readonly ChannelFactory<IGenreService> _channelFactory;
        private readonly ObjectsMapper<GenreDTO, GenreUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<GenreDTO, GenreUIO>();
        private readonly ObjectsMapper<GenreUIO, GenreDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<GenreUIO, GenreDTO>();

        public GenreServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IGenreService>(binding, "Peplex_PFCGenreService").CreateChannelFactory();
        }

        public bool Insert( GenreUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                    proxy.Channel.Insert(_mapUIO2DTO.Map(entity));

                return true;
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return false;
            }
        }

        public bool Update( GenreUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
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

        public bool Delete( int pk)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
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

        public GenreUIO Single( int pk)
        {
            try
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
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return null;
            }
        }

        public IEnumerable<GenreUIO> FindAll()
        {
            try
            { 
                using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                {
                    var uios = new List<GenreUIO>();
                    var dtos = proxy.Channel.FindAll();
                
                    dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                    return uios;
                }
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return new List<GenreUIO>();
            }
        }

        public int MaxPK()
        {
            try
            { 
                using (var proxy = new ServiceProxy<IGenreService>(_channelFactory))
                    return proxy.Channel.MaxPK();
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return 0;
            }
        }
    }
}
