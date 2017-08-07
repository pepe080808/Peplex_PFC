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
    public class EpisodeServiceProxy : IEpisodeServiceProxy
    {
        private readonly ChannelFactory<IEpisodeService> _channelFactory;
        private readonly ObjectsMapper<EpisodeDTO, EpisodeUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeDTO, EpisodeUIO>();
        private readonly ObjectsMapper<EpisodeUIO, EpisodeDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeUIO, EpisodeDTO>();

        public EpisodeServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IEpisodeService>(binding, "Peplex_PFCEpisodeService").CreateChannelFactory();
        }

        public bool Insert(EpisodeUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
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

        public bool Update(EpisodeUIO entity)
        {
            try
            {
                using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
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

        public bool Delete(int serieId, int season, int number)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
                    proxy.Channel.Delete(serieId, season, number);

                return true;
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return false;
            }
        }

        public EpisodeUIO Single(int serieId, int season, int number)
        {
            try
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
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return null;
            }
        }

        public IEnumerable<EpisodeUIO> FindAll()
        {
            try
            {
                using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
                {
                    var uios = new List<EpisodeUIO>();
                    var dtos = proxy.Channel.FindAll();

                    dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                    return uios;
                }
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return new List<EpisodeUIO>();
            }
        }

        public int MaxPK()
        {
            try
            {
                using (var proxy = new ServiceProxy<IEpisodeService>(_channelFactory))
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