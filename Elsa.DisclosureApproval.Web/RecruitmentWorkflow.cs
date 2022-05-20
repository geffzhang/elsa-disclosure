using Elsa.Activities.ControlFlow;
using Elsa.Activities.Workflows;
using Elsa.Builders;
using static Elsa.Activities.Workflows.RunWorkflow;

namespace Elsa.DisclosureApproval.Web
{
    public class RecruitmentWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithDisplayName("Contributor Recruitment Workflow")
                .Then<Fork>(activity => activity.WithBranches("Contract", "Disclosure"), fork =>
                {
                    fork
                        .When("Contract")
                        .RunWorkflow<ContractSigningWorkflow>(RunWorkflowMode.Blocking)
                        .ThenNamed("Join");

                    fork
                        .When("Disclosure")
                        .RunWorkflow<DisclosureSigningWorkflow>(RunWorkflowMode.Blocking)
                        .ThenNamed("Join");

                })
                .Add<Join>(join => join.WithMode(Join.JoinMode.WaitAll)).WithName("Join");
        }
    }
}
