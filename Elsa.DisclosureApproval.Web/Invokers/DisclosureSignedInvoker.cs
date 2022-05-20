using Elsa.DisclosureApproval.Web.Activities;
using Elsa.DisclosureApproval.Web.Bookmarks;
using Elsa.DisclosureApproval.Web.Invokers.Interfaces;
using Elsa.DisclosureApproval.Web.Models;
using Elsa.Models;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.DisclosureApproval.Web.Invokers
{
    public class DisclosureSignedInvoker : IDisclosureSignedInvoker
    {
        private readonly IWorkflowLaunchpad _workflowLaunchpad;

        public DisclosureSignedInvoker(IWorkflowLaunchpad workflowLaunchpad)
        {
            _workflowLaunchpad = workflowLaunchpad;
        }

        public async Task<IEnumerable<CollectedWorkflow>> DispatchWorkflowsAsync(XContributor contributor, CancellationToken cancellationToken = default)
        {
            var context = new WorkflowsQuery(nameof(DisclosureSignedBlocking), new DisclosureSignedBookmark());
            return await _workflowLaunchpad.CollectAndDispatchWorkflowsAsync(context, new WorkflowInput(contributor), cancellationToken);
        }

        public async Task<IEnumerable<CollectedWorkflow>> ExecuteWorkflowsAsync(XContributor contributor, CancellationToken cancellationToken = default)
        {
            var context = new WorkflowsQuery(nameof(DisclosureSignedBlocking), new DisclosureSignedBookmark());
            return await _workflowLaunchpad.CollectAndExecuteWorkflowsAsync(context, new WorkflowInput(contributor), cancellationToken);
        }
    }
}
