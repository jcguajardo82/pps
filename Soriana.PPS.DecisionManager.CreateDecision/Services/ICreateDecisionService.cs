using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.CreateDecision.Services
{
    public interface ICreateDecisionService
    {
        Task<DecisionManager201Response> CreateDecision(DecisionManagerRequest decisionManagerCaseRequest);
    }
}
