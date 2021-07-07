using System.Threading.Tasks;
using Soriana.PPS.Common.DTO.ClosureOrder;
using Soriana.PPS.Common.DTO.Cybersource.Payments;

namespace Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Services
{
    public interface IClosureOrderGroceryService
    {
         Task<string> ClosureGrocery(ClosureOrderGroceyRequest closureOrderGrocey);
    }
}
