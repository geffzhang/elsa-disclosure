using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.DisclosureApproval.Web.Activities
{
    [Action(
        Category = "Conport Workflows",
        Description = "Triggers when a disclosure is signed"
    )]
    public class ContractSigned : Activity
    {
        [ActivityInput(
            Label = "Contributor ID",
            Hint = "The ID of the contributor to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public string ContributrId { get; set; } = default!;

        [ActivityInput(
            Label = "Contract ID",
            Hint = "The ID of the disclosure to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public string ContractId { get; set; } = default!;

        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            return context.WorkflowExecutionContext.IsFirstPass ? Done() : Suspend();
        }

        protected override IActivityExecutionResult OnResume()
        {
            return Done();
        }
    }
}
