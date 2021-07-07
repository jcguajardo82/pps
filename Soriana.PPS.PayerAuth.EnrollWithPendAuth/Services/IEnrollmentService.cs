using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using System.Threading.Tasks;

namespace Soriana.PPS.PayerAuth.Enrollment.Services
{
    public interface IEnrollmentService
    {
        Task<Enrollment201Response> Enrollment(EnrollmentRequest enrollmentRequest);
    }
}
