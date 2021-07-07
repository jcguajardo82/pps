using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Soriana.PPS.DecisionManager.Common.Helpers
{
    public class EndpointAddressHelper
    {
        public static EndpointAddress GetEndpointAddress(string url, string dnsName)
        {
            AddressHeaderCollection headers = new AddressHeaderCollection();
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(url), EndpointIdentity.CreateDnsIdentity(dnsName), headers);
            return endpointAddress;
        }
    }
}
