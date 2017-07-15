using System.Collections.Generic;
using System.ServiceModel;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Insert(UserDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Update(UserDTO entity);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        void Delete(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        UserDTO Single(int pk);

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        IEnumerable<UserDTO> FindAll();

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        int MaxPK();
    }
}