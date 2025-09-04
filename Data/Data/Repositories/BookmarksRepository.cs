using Data.Repositories.Base;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookmarksRepository : BaseRepository<Bookmark>, IBookmarksRepository
    {
        public BookmarksRepository(ReadLaterDataContext dbContext) : base(dbContext)
        {
        }


        public override async Task<List<Bookmark>> GetEntities()
        {
            return await _dbContext.Bookmark.Include(x => x.Category).ToListAsync();
        }

        public override async Task<Bookmark> GetEntity(int Id)
        {
            return await _dbContext.Bookmark.Include(x => x.Category).FirstOrDefaultAsync(b => b.ID == Id);
        }
    }
}
