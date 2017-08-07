using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Insert(SerieUIO entity)
        {
            try
            {
                using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
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

        public bool Update(SerieUIO entity)
        {
            try
            {
                using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
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
                using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
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

        public SerieUIO Single(int pk)
        {
            try
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
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return null;
            }
        }

        public IEnumerable<SerieUIO> FindAll()
        {
            try
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
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return new List<SerieUIO>();
            }
        }

        public int MaxPK()
        {
            try
            {
                using (var proxy = new ServiceProxy<ISerieService>(_channelFactory))
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