using System.Threading.Tasks;

using Soriana.PPS.Common.DTO.ClosureOrder;

namespace Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Services
{
    public interface IResponseXMLService
    {
        Task<string> GetResponseXML(string Code, string Descripcion);
    }
}
