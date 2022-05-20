using Elsa.DisclosureApproval.Web.Models;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.DisclosureApproval.Web.Providers.WorkflowContexts
{
    public class DisclosureSigningWorkflowContextProvider : WorkflowContextRefresher<XWorkflow>
    {
        public DisclosureSigningWorkflowContextProvider()
        {
        }

        /// <summary>
        /// Loads a BlogPost entity from the database.
        /// </summary>
        public override async ValueTask<XWorkflow?> LoadAsync(LoadWorkflowContext context, CancellationToken cancellationToken = default)
        {
            //var Id = context.ContextId;
            var Id = Guid.NewGuid().ToString("N");

            var input = context.WorkflowExecutionContext.Input;

            var wf = new XWorkflow { Id = Id };

            context.WorkflowExecutionContext.WorkflowContext = wf;
            context.WorkflowExecutionContext.ContextId = wf.Id;
            
            return wf;
        }

        /// <summary>
        /// Updates a BlogPost entity in the database.
        /// If there's no actual workflow context, we will get it from the input. This works because we know we have a workflow that starts with an HTTP Endpoint activity that receives BlogPost models.
        /// This is a design choice for this particular demo. In real world scenarios, you might not even need this since your workflows may be dealing with existing entities, or have (other) workflows that handle initial entity creation.
        /// The key take away is: you can do whatever you want with these workflow context providers :) 
        /// </summary>
        public override async ValueTask<string?> SaveAsync(SaveWorkflowContext<XWorkflow> context, CancellationToken cancellationToken = default)
        {
            return "";
        }
    }
}
