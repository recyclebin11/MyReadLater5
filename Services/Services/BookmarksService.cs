using Data.Repositories;
using Data.Repositories.Base;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarksService : IBookmarksService
    {
        private readonly IBookmarksRepository _bookmarkRepository;

        public BookmarksService(IBookmarksRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<Bookmark> CreateBookmark(Bookmark bookmark)
        {
            return await _bookmarkRepository.CreateEntity(bookmark);
        }
        public async Task UpdateBookmark(Bookmark bookmark)
        {
            await _bookmarkRepository.UpdateEntity(bookmark);
        }
        public async Task<List<Bookmark>> GetBookmarks()
        {
            return await _bookmarkRepository.GetEntities();
        }
        public async Task<Bookmark> GetBookmark(int Id)
        {
            return await _bookmarkRepository.GetEntity(Id);
        }
        public async Task DeleteBookmark(Bookmark bookmark)
        {
            await _bookmarkRepository.DeleteEntity(bookmark);
        }
    }
}
