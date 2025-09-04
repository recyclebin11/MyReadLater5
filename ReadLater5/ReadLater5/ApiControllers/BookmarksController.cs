using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;

namespace ReadLater5.ApiControllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "JWT")]
    [Route("api/bookmarks")]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarksService _bookmarksService;

        public BookmarksController(IBookmarksService bookmarksService)
        {
            _bookmarksService = bookmarksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookmarks()
        {
            var bookmarks = await _bookmarksService.GetBookmarks();
            return Ok(bookmarks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookmark(int id)
        {
            var bookmark = await _bookmarksService.GetBookmark(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            return Ok(bookmark);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBookmark(Bookmark bookmark)
        {
            var createdBookmark = await _bookmarksService.CreateBookmark(bookmark);
            return CreatedAtAction(nameof(GetBookmark), new { id = createdBookmark.ID }, createdBookmark);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookmark(int id, Bookmark bookmark)
        {
            if (id != bookmark.ID)
            {
                return BadRequest();
            }
            await _bookmarksService.UpdateBookmark(bookmark);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(int id)
        {
            var bookmark = await _bookmarksService.GetBookmark(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            await _bookmarksService.DeleteBookmark(bookmark);
            return NoContent();
        }
    }
}
