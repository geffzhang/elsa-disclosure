using Elsa.DisclosureApproval.Web.Activities;
using Elsa.Services;

namespace Elsa.DisclosureApproval.Web.Bookmarks
{
    public class DisclosureSignedBookmarkProvider : BookmarkProvider<DisclosureSignedBookmark, DisclosureSignedBlocking>
    {
        public override IEnumerable<BookmarkResult> GetBookmarks(BookmarkProviderContext<DisclosureSignedBlocking> context)
        {
            return new[] { Result(new DisclosureSignedBookmark()) };
        }
    }
}
