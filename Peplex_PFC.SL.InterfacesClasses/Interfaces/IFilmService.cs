using System.Collections.Generic;
using System.ServiceModel;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface IFilmService
    {
        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Insert(FilmDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Update(FilmDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Delete(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        FilmDTO Single(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        IEnumerable<FilmDTO> FindAll();

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        int MaxPK();
    }
}