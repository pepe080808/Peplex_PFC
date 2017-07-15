using System;

namespace Utils
{
    public class ServiceEndpointDescriptor
    {
        public Type ServiceType { get; set; }
        public Type InterfaceType { get; set; }
        public string ServiceName { get; set; }
    }
}
