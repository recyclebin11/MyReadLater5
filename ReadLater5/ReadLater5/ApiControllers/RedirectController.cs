using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;

namespace ReadLater5.ApiControllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/redirect")]
    public class RedirectController : ControllerBase
    {
        private readonly IBookmarksService _bookmarksService;
        public RedirectController(IBookmarksService bookmarksService)
        {
            _bookmarksService = bookmarksService;
        }

        [Route("redirectandtrack")]
        [HttpGet]
        public async Task<IActionResult> RedirectAndTrack([FromQuery] int id)
        {
            var bookmark = await _bookmarksService.GetBookmark(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            bookmark.ClickCounter++;
            await _bookmarksService.UpdateBookmark(bookmark);

            return Redirect(bookmark.URL);
        }

        [Route("generateshorturl")]
        [HttpGet]
        public async Task<IActionResult> GenerateShortUrl([FromQuery] int id)
        {
            var bookmark = await _bookmarksService.GetBookmark(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            // Assuming the base URL is known and fixed
            var baseUrl = $"{Request.Scheme}://{Request.Host}/api/redirect/redirectandtrack?id=";
            var shortUrl = baseUrl + bookmark.ID;

            return Ok(new { ShortUrl = shortUrl });
        }
    }
}
