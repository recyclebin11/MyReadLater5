using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookmarksService
    {
        Task<Bookmark> CreateBookmark(Bookmark bookmark);
        Task DeleteBookmark(Bookmark bookmark);
        Task<Bookmark> GetBookmark(int Id);
        Task<List<Bookmark>> GetBookmarks();
        Task UpdateBookmark(Bookmark bookmark);
    }
}