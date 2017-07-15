using System.Collections.Generic;
using System.ServiceModel;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface IEpisodeService
    {
        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Insert(EpisodeDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Update(EpisodeDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Delete(int serieId, int season, int number);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        EpisodeDTO Single(int serieId, int season, int number);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        IEnumerable<EpisodeDTO> FindAll();

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        int MaxPK();
    }
}