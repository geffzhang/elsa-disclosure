namespace Elsa.DisclosureApproval.Web.Models
{
    public class XWorkflow
    {
        public string Id { get; set; }
        public XContributor Contributor { get; set; }
        public List<XTask> Tasks { get; set; }
    }
}
