using Elsa.Activities.Signaling.Services;
using Elsa.DisclosureApproval.Web.Models;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using Elsa.Services.WorkflowStorage;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.DisclosureApproval.Web.Endpoints.Disclosures
{
    [ApiController]
    [Route("signal")]
    public class DisclosureSignal : Controller
    {
        private readonly IBuildsAndStartsWorkflow _workflowRunner;
        private readonly ISignaler _signaler;
        private readonly IWorkflowStorageService _workflowStorageService;
        private readonly IWorkflowTriggerInterruptor _workflowTriggerInterruptor;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;

        public DisclosureSignal(
            IBuildsAndStartsWorkflow workflowRunner,
            ISignaler signaler,
            IWorkflowStorageService workflowStorageService,
            IWorkflowTriggerInterruptor workflowTriggerInterruptor,
            IWorkflowInstanceStore workflowInstanceStore)
        {
            _workflowRunner = workflowRunner;
            _signaler = signaler;
            _workflowStorageService = workflowStorageService;
            _workflowTriggerInterruptor = workflowTriggerInterruptor;
            _workflowInstanceStore = workflowInstanceStore;
        }

        [HttpGet]
        [HttpGet("disclosure/launch")]
        public async Task<IActionResult> Launch()
        {
            var contributor = new XContributor
            {
                ContributorId = "contributor_1",
                Disclosure = new XDisclosure
                {
                    DisclosureId = "disclosure_2",
                }
            };

            await _workflowRunner.BuildAndStartWorkflowAsync<DisclosureSigningWorkflow>(
                input: new WorkflowInput(contributor));
            
            // Returning empty (the workflow will write an HTTP response).
            return new EmptyResult();
        }

        [HttpGet("{workflowInstanceId}/disclosure/signed")]
        public async Task<IActionResult> SignalSigned(string workflowInstanceId, CancellationToken cancellationToken)
        {
            var contributor = new XContributor
            {
                ContributorId = "contributor_1",
                Disclosure = new XDisclosure
                {
                    DisclosureId = "disclosure_2",
                }
            };

            var startedWorkflows = await _signaler.TriggerSignalAsync(
                "DisclosureSigned",
                input: contributor,
                workflowInstanceId: workflowInstanceId,
                cancellationToken: cancellationToken);
            return Ok(startedWorkflows);
        }

        [HttpGet("{workflowInstanceId}/disclosure/reviewed")]
        public async Task<IActionResult> SignalReviewed(string workflowInstanceId, CancellationToken cancellationToken)
        {
            var contributor = new XContributor
            {
                ContributorId = "contributor_1",
                Disclosure = new XDisclosure
                {
                    DisclosureId = "disclosure_2",
                }
            };

            var startedWorkflows = await _signaler.TriggerSignalAsync(
                "DisclosureReviewed",
                input: contributor,
                workflowInstanceId: workflowInstanceId,
                cancellationToken: cancellationToken);
            return Ok(startedWorkflows);
        }

        [HttpGet("{workflowInstanceId}/disclosure/task")]
        public async Task<IActionResult> CompleteTask(string workflowInstanceId, CancellationToken cancellationToken)
        {
            var availableActions = new[]
            {
                "Accept",
                "Reject",
                "NeedsWork"
            };

            var workflowInstance = await _workflowInstanceStore.FindByIdAsync(workflowInstanceId);

            // Workflow is now halted on the user task activity. Ask user for input:
            // Console.WriteLine($"What action will you take? Choose one of: {string.Join(", ", availableActions)}");
            // var userAction = Console.ReadLine();
            var userAction = availableActions[0];
            var currentActivityId = workflowInstance.BlockingActivities.Select(i => i.ActivityId).First();

            // Update the workflow instance with input.
            await _workflowStorageService.UpdateInputAsync(workflowInstance, new WorkflowInput(userAction));
            await _workflowTriggerInterruptor.InterruptActivityAsync(workflowInstance, currentActivityId);

            return new EmptyResult();
        }
    }
}
