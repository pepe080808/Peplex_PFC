using System.Collections.Generic;
using System.ServiceModel;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface IGenreService
    {
        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Insert(GenreDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Update(GenreDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Delete(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        GenreDTO Single(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        IEnumerable<GenreDTO> FindAll();

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        int MaxPK();
    }
}
