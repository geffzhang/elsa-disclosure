using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.DisclosureApproval.Web.Models;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.DisclosureApproval.Web.Activities
{
    [Trigger(
        Category = "Conport Workflows",
        Description = "Triggers when a disclosure is signed"
    )]
    public class DisclosureSignedBlocking : Activity
    {
        /*
        [ActivityInput(
            Label = "Contributor ID",
            Hint = "The ID of the contributor to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public string ContributrId { get; set; } = default!;

        [ActivityInput(
            Label = "Disclosure ID",
            Hint = "The ID of the disclosure to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public string DisclosureId { get; set; } = default!;
        */
        [ActivityInput(
            Label = "Contributor model",
            Hint = "The contributor to load",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid }
        )]
        public XContributor Contributor { get; set; } = default!;

        [ActivityOutput] 
        public XContributor Output { get; set; }

        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            return context.WorkflowExecutionContext.IsFirstPass ? OnExecuteInternal(context) : Suspend();
        }

        protected override IActivityExecutionResult OnResume(ActivityExecutionContext context)
        {
            return OnExecuteInternal(context);
        }

        private IActivityExecutionResult OnExecuteInternal(ActivityExecutionContext context)
        {
            var contributor = context.GetInput<XContributor>();
            Output = contributor;
            return Done();
        }
    }
}
