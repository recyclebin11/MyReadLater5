using Microsoft.AspNetCore.Mvc;
using ReadLater5.Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IBookmarksService _bookmarksService;

        public DashboardController(IBookmarksService bookmarksService)
        {
            _bookmarksService = bookmarksService;
        }

        public async Task<IActionResult> Index()
        {
            var bookmarks = await _bookmarksService.GetBookmarks();

            if (!bookmarks.Any())
            {
                return View(new DashboardViewModel
                {
                    BookmarkStats = (0, 0),
                    TotalBookmarks = 0,
                    TopBookmarks = new List<Entity.Bookmark>()
                });
            }

            // Order by click count descending
            var orderedBookmarks = bookmarks.OrderByDescending(b => b.ClickCounter).ToList();

            // Get top bookmark stats
            var topBookmark = orderedBookmarks.First();
            var bookmarkStats = (topBookmark.ID, topBookmark.ClickCounter);

            var viewModel = new DashboardViewModel
            {
                BookmarkStats = bookmarkStats,
                TotalBookmarks = bookmarks.Count(),
                TopBookmarks = orderedBookmarks.Take(5).ToList()
            };

            return View(viewModel);
        }
    }
}
