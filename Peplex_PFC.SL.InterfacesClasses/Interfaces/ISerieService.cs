using System.Collections.Generic;
using System.ServiceModel;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface ISerieService
    {
        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Insert(SerieDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Update(SerieDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Delete(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        SerieDTO Single(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        IEnumerable<SerieDTO> FindAll();

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        int MaxPK();
    }
}