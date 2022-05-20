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
        Description = "Triggers when a disclosure is created"
    )]
    public class DisclosureCreated : Activity
    {
        public DisclosureCreated()
        {
            //SaveWorkflowContext = true;
        }

        [ActivityInput(
            Label = "Contributor model",
            Hint = "The contributor to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public XContributor Contributor { get; set; } = default!;

        [ActivityOutput]
        public XWorkflow Output { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var contributor = context.GetInput<XContributor>();
            var wf = new XWorkflow
            {
                Contributor = contributor,
            };
            Output = wf;

            return Done();
        }
    }
}
