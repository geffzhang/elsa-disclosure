using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.UserTask.Activities;
using Elsa.Builders;
using Elsa.DisclosureApproval.Web.Activities;
using Elsa.DisclosureApproval.Web.Models;
using Elsa.Models;

namespace Elsa.DisclosureApproval.Web
{
    public class DisclosureSigningWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithContextType<XWorkflow>(WorkflowContextFidelity.Burst)
                .WithName("DisclosureSigning")
                .WithDisplayName("Disclosure Signing Workflow")             
                .Then<DisclosureCreated>(/*activity =>
                {
                    //activity.WithContextType<XWorkflow>();
                    //activity.SaveWorkflowContext(true);
                }*/)
                .SignalReceived("DisclosureSigned")
                .Then<DisclosureSigned>(/* activity =>
                {
                    activity.LoadWorkflowContext(true);
                }*/)
                .SignalReceived("DisclosureReviewed")
                .Then<UserTask>(
                    activity => activity.Set(x => x.Actions, new[] { "Accept", "Reject", "NeedsWork" }),
                    userTask =>
                    {
                        userTask
                            .When("Accept")
                            .WriteLine("Great! Your work has been accepted.")
                            .ThenNamed("DisclosureExit");

                        userTask
                            .When("Reject")
                            .WriteLine("Sorry! Your work has been rejected.")
                            .ThenNamed("DisclosureExit");

                        userTask
                            .When("NeedsWork")
                            .WriteLine("So close! Your work needs a little bit more work.")
                            .ThenNamed("DisclosureExit");
                    }
                )
                .WriteLine("Disclosure Workflow finished.").WithName("DisclosureExit");
        }
    }
}
