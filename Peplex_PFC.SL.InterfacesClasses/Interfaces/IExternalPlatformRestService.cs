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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Film/{filmId}")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformFilmCollection GetFilm(string filmId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Series")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformSerieCollection GetSeries();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Serie/{serieId}")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformSerieCollection GetSerie(string serieId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Users")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformUserCollection GetUsers();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "User/{userId}")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformUserCollection GetUser(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Genres")]
        [return: MessageParameter(Name = "Result")]
        ExternalPlatformGenreCollection GetGenres();
    }
}
