using System.Runtime.Serialization;

namespace Peplex_PFC.SL.InterfacesClasses
{
    public enum Peplex_PFCServiceFaultCode
    {
        General = 0,
        PkViolation = 100,
        FkReferenced = 200
    }

    [DataContract]
    public class Peplex_PFCServiceFault
    {
        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
