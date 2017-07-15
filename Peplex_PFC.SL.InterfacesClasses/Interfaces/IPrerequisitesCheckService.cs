using System.ServiceModel;

namespace Peplex_PFC.SL.InterfacesClasses.Interfaces
{
    [ServiceContract]
    public interface IPrerequisitesCheckService
    {
        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        bool IsAlive();

        [OperationContract]
        [FaultContract(typeof(Peplex_PFCServiceFault))]
        bool UpdateDataBase();
    }
}