using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface IGetMerchantDefinedDataService
    {
        Task<IList<MerchantDefinedData>> GetMerchantDefinedData(PaymentOrderProcessRequest request, PaymentOrderProcessRequest originalRequest, string serviceInterface);
    }
}
