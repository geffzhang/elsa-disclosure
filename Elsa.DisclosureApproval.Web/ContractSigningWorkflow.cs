using Elsa.Activities.ControlFlow;
using Elsa.Builders;

namespace Elsa.DisclosureApproval.Web
{
    public class ContractSigningWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithName("ContractSigning")
                .WithDisplayName("Contract Signing Workflow")
                .SignalReceived("Contract Signed");
        }
    }
}
