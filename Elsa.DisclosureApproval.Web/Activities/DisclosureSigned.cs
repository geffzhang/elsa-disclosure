using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.DisclosureApproval.Web.Models;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.DisclosureApproval.Web.Activities
{
    [Action(
        Category = "Conport Workflows",
        Description = "Triggers when a disclosure is signed"
    )]
    public class DisclosureSigned : Activity
    {
        public DisclosureSigned()
        {
            
        }

        [ActivityInput(
            Label = "Contributor model",
            Hint = "The contributor to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public XContributor Contributor { get; set; } = default!;

        [ActivityOutput] 
        public XContributor Output { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var contributor = context.GetInput<XContributor>();

            var wf = context.WorkflowExecutionContext.WorkflowContext;
            // update task status here

            Output = contributor;
            return Done();
        }
    }
}
