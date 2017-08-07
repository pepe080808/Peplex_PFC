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
    public class FormatServiceProxy : IFormatServiceProxy
    {
        private readonly ChannelFactory<IFormatService> _channelFactory;
        private readonly ObjectsMapper<FormatDTO, FormatUIO> _mapDTO2UIO = ObjectMapperManager.DefaultInstance.GetMapper<FormatDTO, FormatUIO>();
        private readonly ObjectsMapper<FormatUIO, FormatDTO> _mapUIO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<FormatUIO, FormatDTO>();

        public FormatServiceProxy(Binding binding)
        {
            _channelFactory = new ChannelFactoryFactory<IFormatService>(binding, "Peplex_PFCFormatService").CreateChannelFactory();
        }

        public bool Insert(FormatUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IFormatService>(_channelFactory))
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

        public bool Update(FormatUIO entity)
        {
            try
            { 
                using (var proxy = new ServiceProxy<IFormatService>(_channelFactory))
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
                using (var proxy = new ServiceProxy<IFormatService>(_channelFactory))
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

        public FormatUIO Single(int pk)
        {
            try
            {
                using (var proxy = new ServiceProxy<IFormatService>(_channelFactory))
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

        public IEnumerable<FormatUIO> FindAll()
        {
            try
            {
                using (var proxy = new ServiceProxy<IFormatService>(_channelFactory))
                {
                    var uios = new List<FormatUIO>();
                    var dtos = proxy.Channel.FindAll();

                    dtos.ForEach(dto => uios.Add(_mapDTO2UIO.Map(dto)));

                    return uios;
                }
            }
            catch (Exception ex)
            {
                var msg = String.Format(Translations.msgServiceProxyError, PeplexUtils.GetCaller(), this.GetType().Name, ex.Message, ex.StackTrace);
                MessageBoxWindow.Show(null, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, msg);
                return new List<FormatUIO>();
            }
        }

        public int MaxPK()
        {
            try
            {
                using (var proxy = new ServiceProxy<IFormatService>(_channelFactory))
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