using Elsa.DisclosureApproval.Web.Models;
using Elsa.Services.Models;

namespace Elsa.DisclosureApproval.Web.Invokers.Interfaces
{
    public interface IDisclosureSignedInvoker
    {
        Task<IEnumerable<CollectedWorkflow>> DispatchWorkflowsAsync(XContributor contributor, CancellationToken cancellationToken = default);
        Task<IEnumerable<CollectedWorkflow>> ExecuteWorkflowsAsync(XContributor contributor, CancellationToken cancellationToken = default);
    }
}
