using System.ServiceModel;
using System.ServiceModel.Web;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface IExternalPlatformRestService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Films")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformFilmCollection GetFilms();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Films")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformSerieCollection GetSeries();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Episodes/{mail}")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformEpisodeCollection GetEpisodes(string serieId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Users")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformUserCollection GetUsers();
    }
}
