using Elsa.DisclosureApproval.Web.Invokers.Interfaces;
using Elsa.DisclosureApproval.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.DisclosureApproval.Web.Endpoints.Disclosures
{
    [ApiController]
    [Route("disclosures")]
    public class Post : Controller
    {
        private readonly IDisclosureSignedInvoker _invoker;

        public Post(IDisclosureSignedInvoker invoker)
        {
            _invoker = invoker;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var contributor = new XContributor
            {
                ContributorId = "contributor_1",
                Disclosure = new XDisclosure
                {
                    DisclosureId = "disclosure_2",
                }
            }; 
            var collectedWorkflows = await _invoker.DispatchWorkflowsAsync(contributor);
            return Ok(collectedWorkflows.ToList());
        }
    }
}
