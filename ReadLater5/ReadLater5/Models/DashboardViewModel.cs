using System.Collections.Generic;

namespace ReadLater5.Models
{
    public class DashboardViewModel
    {
        public (int, int) BookmarkStats { get; set; }
        public int TotalBookmarks { get; set; }
        public List<Entity.Bookmark> TopBookmarks { get; set; }
    }
}
